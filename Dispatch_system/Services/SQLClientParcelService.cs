using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dispatch_system.Data;
using Dispatch_system.Models;
using Dispatch_system.ViewModels;

namespace Dispatch_system.Services
{
    public class SQLClientParcelService : IClientParcelService
    {
        private readonly ApplicationDbContext dbContext;

        public SQLClientParcelService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ClientParcelViewModel CheckStatus(int parcelId)
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

        public void PostOnline(ClientParcelViewModel clientParcelViewModel)
        {
            decimal volume = clientParcelViewModel.Width * clientParcelViewModel.Height * clientParcelViewModel.Depth;
            int senderBranchCode = int.Parse(new string(clientParcelViewModel.SenderPostalCode.Take(2).ToArray()));
            int receiverBranchCode = int.Parse(new string(clientParcelViewModel.ReceiverPostalCode.Take(2).ToArray()));

            short lastBranchId = dbContext.Branches.First(x => x.BranchCode == senderBranchCode).BranchId;
            short targetBranchId = dbContext.Branches.First(x => x.BranchCode == receiverBranchCode).BranchId;

            Parcel parcel = new Parcel
            {
                SenderStreetName = clientParcelViewModel.SenderStreetName,
                SenderBlockNumber = clientParcelViewModel.SenderBlockNumber,
                SenderFlatNumber = clientParcelViewModel.SenderFlatNumber,
                SenderPostalCode = clientParcelViewModel.SenderPostalCode,
                SenderCity = clientParcelViewModel.SenderCity,
                ReceiverStreetName = clientParcelViewModel.ReceiverStreetName,
                ReceiverBlockNumber = clientParcelViewModel.ReceiverBlockNumber,
                ReceiverFlatNumber = clientParcelViewModel.ReceiverFlatNumber,
                ReceiverPostalCode = clientParcelViewModel.ReceiverPostalCode,
                ReceiverCity = clientParcelViewModel.ReceiverCity,
                Weight = clientParcelViewModel.Weight,
                Volume = volume,
                IsSent = false,
                LastBranchId = lastBranchId,
                TargetBranchId = targetBranchId,
                VisibleForCourier = false,
                ParcelStatusId = 6 // "status: przesyłka nadana online"
            };

            dbContext.Add(parcel);
            dbContext.SaveChanges();
        }
    }
}
