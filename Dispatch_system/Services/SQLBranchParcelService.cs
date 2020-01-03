using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dispatch_system.Data;
using Dispatch_system.Models;
using Dispatch_system.ViewModels;

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

        public void Post(int parcelId)
        {
            Parcel parcelQuery = (from parcel in context.Parcels
                                  where parcel.ParcelId == parcelId
                                  select new Parcel
                                  {
                                      //CourierId = parcel.CourierId,
                                      CourierId = 8, // na razie na sztywno
                                      ParcelStatusId = 2,
                                      IsSent = true,
                                      TargetBranchId = parcel.TargetBranchId
                                  }).ToList().First();

            context.Parcels.Update(parcelQuery);
            context.SaveChanges();
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
