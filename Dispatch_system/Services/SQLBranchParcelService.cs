using Dispatch_system.Data;
using Dispatch_system.Models;
using Dispatch_system.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Dispatch_system.Services
{
    public class SQLBranchParcelService : IBranchParcelService
    {
        private readonly ApplicationDbContext dbContext;

        public SQLBranchParcelService(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        /// <summary>
        /// Ta metoda ma pobierać pełne dane o przesyłce i pracownik placówki przed nadaniem może je jeszcze zmodyfikować
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        public ClientParcelViewModel GetParcel(int parcelId)
        {
            var model = (from parcel in dbContext.Parcels
                         join status in dbContext.ParcelStatuses on parcel.ParcelStatusId equals status.ParcelStatusId
                         where parcel.ParcelId == parcelId
                         select new ClientParcelViewModel
                         {
                             IsSent = parcel.IsSent,
                             ParcelId = parcel.ParcelId,
                             SenderStreetName = parcel.SenderStreetName,
                             SenderBlockNumber = parcel.SenderBlockNumber,
                             SenderFlatNumber = parcel.SenderFlatNumber,
                             SenderPostalCode = parcel.SenderPostalCode,
                             SenderCity = parcel.SenderCity,
                             ReceiverStreetName = parcel.ReceiverStreetName,
                             ReceiverBlockNumber = parcel.ReceiverBlockNumber,
                             ReceiverFlatNumber = parcel.ReceiverFlatNumber,
                             ReceiverPostalCode = parcel.ReceiverPostalCode,
                             ReceiverCity = parcel.ReceiverCity,
                             StatusName = status.StatusName
                         });

            if (model.FirstOrDefault() != null)
            {
                return model.First();
            }

            return null;
        }

        public void MarkAsDelivered(int parcelId)
        {
            var parcel = dbContext.Parcels.FirstOrDefault(x => x.ParcelId == parcelId);

            parcel.ParcelStatusId = 9; // Przesyłka dostarczona
            dbContext.Parcels.Update(parcel);
            dbContext.SaveChanges();
        }

        public List<ClientParcelViewModel> NotSentParcels(short branchId)
        {
            var parcelList = (from parcel in dbContext.Parcels
                              join status in dbContext.ParcelStatuses on parcel.ParcelStatusId equals status.ParcelStatusId
                              where parcel.LastBranchId == branchId && parcel.IsSent == false
                              && parcel.ParcelStatusId == 6 // status: przesyłka nadana on-line
                              select new ClientParcelViewModel
                              {
                                  ParcelId = parcel.ParcelId,
                                  SenderStreetName = parcel.SenderStreetName,
                                  SenderBlockNumber = parcel.SenderBlockNumber,
                                  SenderFlatNumber = parcel.SenderFlatNumber,
                                  SenderPostalCode = parcel.SenderPostalCode,
                                  SenderCity = parcel.SenderCity,
                                  ReceiverStreetName = parcel.ReceiverStreetName,
                                  ReceiverBlockNumber = parcel.ReceiverBlockNumber,
                                  ReceiverFlatNumber = parcel.ReceiverFlatNumber,
                                  ReceiverPostalCode = parcel.ReceiverPostalCode,
                                  ReceiverCity = parcel.ReceiverCity
                              }
                ).ToList();

            return parcelList;
        }

        public short GetBranchId(int branchCode)
        {
            short branchId = dbContext.Branches.FirstOrDefault(x => x.BranchCode == branchCode).BranchId;

            return branchId;
        }

        public void Post(ClientParcelViewModel parcelViewModel)
        {
            Parcel parcel = dbContext.Parcels.FirstOrDefault(x => x.ParcelId == parcelViewModel.ParcelId);

            parcel.ParcelStatusId = 1; // "status: przesyłka odebrana od nadawcy"
            parcel.IsSent = true;
            parcel.ReceiverStreetName = parcelViewModel.ReceiverStreetName;
            parcel.ReceiverFlatNumber = parcelViewModel.ReceiverFlatNumber;
            parcel.ReceiverBlockNumber = parcelViewModel.ReceiverBlockNumber;
            parcel.ReceiverCity = parcelViewModel.ReceiverCity;
            parcel.ReceiverPostalCode = parcelViewModel.ReceiverPostalCode;

            short branchCode = short.Parse(new string(parcelViewModel.ReceiverPostalCode.Take(2).ToArray()));
            short branchId = GetBranchId(branchCode);

            dbContext.Parcels.Update(parcel);
            dbContext.SaveChanges();

            dbContext.Database.ExecuteSqlCommand("AssignParcelToCourier @p0, @p1", branchId, parcelViewModel.ParcelId);
        }

        public void SendParcelsToMainBranch(short branchId)
        {
            dbContext.Database.ExecuteSqlCommand("SendParcelsToMainBranch @p0", branchId);
        }

        public void RegisterParcel(ClientParcelViewModel parcelViewModel)
        {
            decimal volume = parcelViewModel.Width * parcelViewModel.Height * parcelViewModel.Depth;
            int senderBranchCode = int.Parse(new string(parcelViewModel.SenderPostalCode.Take(2).ToArray()));
            int receiverBranchCode = int.Parse(new string(parcelViewModel.ReceiverPostalCode.Take(2).ToArray()));

            short lastBranchId = dbContext.Branches.First(x => x.BranchCode == senderBranchCode).BranchId;
            short targetBranchId = dbContext.Branches.First(x => x.BranchCode == receiverBranchCode).BranchId;

            Parcel parcel = new Parcel
            {
                SenderStreetName = parcelViewModel.SenderStreetName,
                SenderBlockNumber = parcelViewModel.SenderBlockNumber,
                SenderFlatNumber = parcelViewModel.SenderFlatNumber,
                SenderPostalCode = parcelViewModel.SenderPostalCode,
                SenderCity = parcelViewModel.SenderCity,
                ReceiverStreetName = parcelViewModel.ReceiverStreetName,
                ReceiverBlockNumber = parcelViewModel.ReceiverBlockNumber,
                ReceiverFlatNumber = parcelViewModel.ReceiverFlatNumber,
                ReceiverPostalCode = parcelViewModel.ReceiverPostalCode,
                ReceiverCity = parcelViewModel.ReceiverCity,
                Weight = parcelViewModel.Weight,
                Volume = volume,
                IsSent = true,
                LastBranchId = lastBranchId,
                TargetBranchId = targetBranchId,
                VisibleForCourier = false,
                DeliveryAttempts = 0,
                ParcelStatusId = 1 // "status: przesyłka odebrana od nadawcy"
            };

            dbContext.Add(parcel);
            dbContext.SaveChanges();
            dbContext.Database.ExecuteSqlCommand("AssignParcelToCourier @p0, @p1", parcel.TargetBranchId, parcel.ParcelId);
        }

        public List<ParcelViewModel> ParcelsInBranchWarehouse(short branchId)
        {
            var parcelsInWarehouse = (from parcel in dbContext.Parcels
                                      where parcel.TargetBranchId == branchId
                                      select new ParcelViewModel
                                      {
                                          ParcelId = parcel.ParcelId,
                                          ReceiverStreetName = parcel.ReceiverStreetName,
                                          ReceiverBlockNumber = parcel.ReceiverBlockNumber,
                                          ReceiverFlatNumber = parcel.ReceiverFlatNumber,
                                          ReceiverPostalCode = parcel.ReceiverPostalCode,
                                          ReceiverCity = parcel.ReceiverCity
                                      }).ToList();

            return parcelsInWarehouse;
        }

        public List<ParcelViewModel> ParcelsToSend(short branchId)
        {
            var parcelsToSend = (from parcel in dbContext.Parcels
                                 where parcel.LastBranchId == branchId
                                 && parcel.ParcelStatusId == 1 // status: przesyłka odebrana od nadawcy
                                 && parcel.IsSent == true
                                 select new ParcelViewModel
                                 {
                                     ParcelId = parcel.ParcelId,
                                     SenderStreetName = parcel.SenderStreetName,
                                     SenderBlockNumber = parcel.SenderBlockNumber,
                                     SenderFlatNumber = parcel.SenderFlatNumber,
                                     SenderPostalCode = parcel.SenderPostalCode,
                                     SenderCity = parcel.SenderCity,
                                     ReceiverStreetName = parcel.ReceiverStreetName,
                                     ReceiverBlockNumber = parcel.ReceiverBlockNumber,
                                     ReceiverFlatNumber = parcel.ReceiverFlatNumber,
                                     ReceiverPostalCode = parcel.ReceiverPostalCode,
                                     ReceiverCity = parcel.ReceiverCity
                                 }).ToList();

            return parcelsToSend;
        }
    }
}
