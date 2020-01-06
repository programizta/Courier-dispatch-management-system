using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dispatch_system.Data;
using Dispatch_system.ViewModels;
using Microsoft.AspNetCore.Identity;

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
            parcel.ParcelStatusId = 8; // Nieudana próba doręczenia

            dbContext.Parcels.Update(parcel);
            dbContext.SaveChanges();
        }

        public void ParcelDelivered(int parcelId)
        {
            var parcel = dbContext.Parcels.FirstOrDefault(x => x.ParcelId == parcelId);

            parcel.DeliveryAttempts++;
            parcel.ParcelStatusId = 9; // Przesyłka dostarczona
            dbContext.Parcels.Update(parcel);
            dbContext.SaveChanges();
        }

        public List<ParcelViewModel> ParcelsToDeliever(int courierId)
        {
            var parcelsToDeliever = (from parcel in dbContext.Parcels
                                     where parcel.CourierId == courierId
                                     && parcel.DeliveryAttempts < 2
                                     && parcel.ParcelStatusId == 2
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
                                   && parcel.ParcelStatusId == 2
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
    }
}
