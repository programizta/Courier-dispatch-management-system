using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dispatch_system.Data;
using Dispatch_system.Models;
using Dispatch_system.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Dispatch_system.Services
{
    public class SQLBranchParcelService : IBranchParcelService
    {
        private readonly ApplicationDbContext context;

        public SQLBranchParcelService(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Ta metoda ma pobierać pełne dane o przesyłce i pracownik placówki przed nadaniem może je jeszcze zmodyfikować
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        public ClientParcelViewModel GetParcel(int parcelId)
        {
            var model = (from parcel in context.Parcels
                         join status in context.ParcelStatuses on parcel.ParcelStatusId equals status.ParcelStatusId
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

        public void MarkAsDelivered()
        {
            throw new NotImplementedException();
        }

        public List<ClientParcelViewModel> NotSentParcels(int branchId)
        {
            var parcelList = (from parcel in context.Parcels
                              join status in context.ParcelStatuses on parcel.ParcelStatusId equals status.ParcelStatusId
                              where parcel.LastBranchId == branchId && parcel.IsSent == false
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
            short branchId = context.Branches.FirstOrDefault(x => x.BranchCode == branchCode).BranchId;

            return branchId;
        }

        public void Post(ClientParcelViewModel parcelViewModel)
        {
            Parcel parcel = context.Parcels.FirstOrDefault(x => x.ParcelId == parcelViewModel.ParcelId);

            parcel.ParcelStatusId = 2;
            parcel.IsSent = true;
            parcel.ReceiverStreetName = parcelViewModel.ReceiverStreetName;
            parcel.ReceiverFlatNumber = parcelViewModel.ReceiverFlatNumber;
            parcel.ReceiverBlockNumber = parcelViewModel.ReceiverBlockNumber;
            parcel.ReceiverCity = parcelViewModel.ReceiverCity;
            parcel.ReceiverPostalCode = parcelViewModel.ReceiverPostalCode;

            short branchCode = short.Parse(new string(parcelViewModel.ReceiverPostalCode.Take(2).ToArray()));
            short branchId = GetBranchId(branchCode);

            context.Parcels.Update(parcel);
            context.SaveChanges();

            context.Database.ExecuteSqlCommand("AssignParcelToCourier @p0, @p1", branchId, parcelViewModel.ParcelId);
        }

        public int AssignParcelToCourier(Parcel parcel)
        {
            //var courierId = (from employee in context.Employees
            //                 from parcelQuery in context.Parcels
            //                 .Where(parcelQry => parcelQry.CourierId == employee.EmployeeId && employee.IsCourier == true && parcelQry.TargetBranchId == parcel.TargetBranchId).DefaultIfEmpty()
            //                 select new
            //                 {
            //                     employee.EmployeeId,
            //                     parcelQuery.CourierId.
            //                 });
            return 1;
        }
    }
}
