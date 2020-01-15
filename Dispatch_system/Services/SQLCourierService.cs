using Dispatch_system.Data;
using Dispatch_system.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Dispatch_system.Services
{
    public class SQLCourierService : ICourierService
    {
        private readonly ApplicationDbContext dbContext;

        public SQLCourierService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void FailedToDeliever(int parcelId)
        {
            var parcel = dbContext.Parcels.FirstOrDefault(x => x.ParcelId == parcelId);

            parcel.DeliveryAttempts++;
            parcel.ParcelStatusId = 8; // status: nieudana próba doręczenia

            dbContext.Parcels.Update(parcel);
            dbContext.SaveChanges();
        }

        public void ParcelDelivered(int parcelId)
        {
            var parcel = dbContext.Parcels.FirstOrDefault(x => x.ParcelId == parcelId);

            parcel.DeliveryAttempts++;
            parcel.ParcelStatusId = 9; // status: przesyłka dostarczona
            dbContext.Parcels.Update(parcel);
            dbContext.SaveChanges();
        }

        public List<ParcelViewModel> ParcelsToDeliever(int courierId)
        {
            var parcelsToDeliever = (from parcel in dbContext.Parcels
                                     where parcel.CourierId == courierId
                                     && parcel.DeliveryAttempts < 2
                                     && parcel.ParcelStatusId == 3 // status: przesyłka wydana kurierowi
                                     && parcel.VisibleForCourier == true
                                     select new ParcelViewModel
                                     {
                                         ParcelId = parcel.ParcelId,
                                         ReceiverStreetName = parcel.ReceiverStreetName,
                                         ReceiverBlockNumber = parcel.ReceiverBlockNumber,
                                         ReceiverFlatNumber = parcel.ReceiverFlatNumber,
                                         ReceiverPostalCode = parcel.ReceiverPostalCode,
                                         ReceiverCity = parcel.ReceiverCity
                                     }).ToList();

            return parcelsToDeliever;
        }

        public List<ParcelViewModel> ParcelsToReturn(int courierId)
        {
            var parcelsToReturn = (from parcel in dbContext.Parcels
                                   where parcel.CourierId == courierId
                                   && parcel.DeliveryAttempts == 2
                                   && parcel.ParcelStatusId == 8 // status: nieudana próba doręczenia
                                   && parcel.VisibleForCourier == true
                                   select new ParcelViewModel
                                   {
                                       ParcelId = parcel.ParcelId,
                                       ReceiverStreetName = parcel.ReceiverStreetName,
                                       ReceiverBlockNumber = parcel.ReceiverBlockNumber,
                                       ReceiverFlatNumber = parcel.ReceiverFlatNumber,
                                       ReceiverPostalCode = parcel.ReceiverPostalCode,
                                       ReceiverCity = parcel.ReceiverCity
                                   }).ToList();

            return parcelsToReturn;
        }

        public void ReturnParcelsToBranch(int courierId)
        {
            short branchId = dbContext.Employees
                                .First(x => x.EmployeeId == courierId)
                                .BranchId;

            dbContext.Database.ExecuteSqlCommand("ReturnParcelsToBranch @p0, @p1", courierId, branchId);
        }

        public List<ParcelViewModel> ToDelieverNextDay(int courierId)
        {
            var toDeliever = (from parcel in dbContext.Parcels
                              where parcel.CourierId == courierId
                              && parcel.DeliveryAttempts < 2
                              && parcel.VisibleForCourier == true
                              && parcel.ParcelStatusId == 8 // status: nieudana próba doręczenia
                              select new ParcelViewModel
                              {
                                  ParcelId = parcel.ParcelId,
                                  ReceiverStreetName = parcel.ReceiverStreetName,
                                  ReceiverBlockNumber = parcel.ReceiverBlockNumber,
                                  ReceiverFlatNumber = parcel.ReceiverFlatNumber,
                                  ReceiverPostalCode = parcel.ReceiverPostalCode,
                                  ReceiverCity = parcel.ReceiverCity
                              }).ToList();

            return toDeliever;
        }
    }
}
