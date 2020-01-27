using Dispatch_system.Data;
using Dispatch_system.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Dispatch_system.Services
{
    public class SQLWarehouseService : IWarehouseService
    {
        private readonly ApplicationDbContext dbContext;

        public SQLWarehouseService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AcceptSentParcels(int branchId)
        {
            dbContext.Database.ExecuteSqlCommand("AcceptParcels @p0", branchId);
        }

        public CourierParcelsViewModel CourierParcels(int courierId)
        {
            var courierData = (from employee in dbContext.Employees
                               join person in dbContext.People on employee.PersonId equals person.PersonId
                               where employee.EmployeeId == courierId
                               select new CourierParcelsViewModel
                               {
                                   CourierId = courierId,
                                   FirstName = person.FirstName,
                                   LastName = person.LastName,
                                   PhoneNumber = person.PhoneNumber
                               }).First();

            var courierParcels = (from parcel in dbContext.Parcels
                                  where parcel.CourierId == courierId
                                  && parcel.VisibleForCourier == false
                                  && parcel.ParcelStatusId == 7 // status: przesyłka w węźle
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

            foreach (var parcel in courierParcels)
            {
                courierData.ListOfParcels.Add(parcel);
            }

            return courierData;
        }

        public void GiveCourierParcels(int courierId)
        {
            dbContext.Database.ExecuteSqlCommand("TransferParcelsToCourier @p0", courierId);
        }

        public List<EmployeeViewModel> CouriersInBranch(int branchId)
        {
            var couriersInBranch = (from employee in dbContext.Employees
                                    join person in dbContext.People on employee.PersonId equals person.PersonId
                                    join branch in dbContext.Branches on employee.BranchId equals branch.BranchId
                                    where employee.IsCourier == true && employee.BranchId == branchId
                                    select new EmployeeViewModel
                                    {
                                        EmployeeId = employee.EmployeeId,
                                        FirstName = person.FirstName,
                                        LastName = person.LastName,
                                        PhoneNumber = person.PhoneNumber,
                                    }).ToList();

            return couriersInBranch;
        }

        public List<ParcelViewModel> NewParcelsToRegister(int branchId)
        {
            var newParcels = (from parcel in dbContext.Parcels
                              where parcel.LastBranchId == branchId
                              && parcel.VisibleForCourier == false
                              && parcel.ParcelStatusId == 10 // status: przesyłka w drodze do węzła
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
                                  ReceiverCity = parcel.ReceiverCity,
                              }).ToList();

            return newParcels;
        }
    }
}
