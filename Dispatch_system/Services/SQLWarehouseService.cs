using Dispatch_system.Data;
using Dispatch_system.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Services
{
    public class SQLWarehouseService : IWarehouseService
    {
        private readonly ApplicationDbContext dbContext;

        public SQLWarehouseService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Metoda rejestrująca nowe przesyłki, które przybyły do głównego węzła sortowniczego
        /// </summary>
        /// <param name="branchId"></param>
        public void AcceptSentParcels(int branchId)
        {
            dbContext.Database.ExecuteSqlCommand("AcceptParcels @p0", branchId);
        }

        /// <summary>
        /// Metoda zwracająca listę przesyłek przypisanych do kuriera
        /// </summary>
        /// <param name="courierId"></param>
        /// <returns></returns>
        public List<ParcelViewModel> CourierParcels(int courierId)
        {
            var sentParcels = (from parcel in dbContext.Parcels
                               where parcel.ParcelStatusId == 7 // przesyłka w węźle
                               && parcel.VisibleForCourier == false
                               && parcel.CourierId == courierId
                               select new ParcelViewModel
                               {
                                   ParcelId = parcel.ParcelId,
                                   ReceiverStreetName = parcel.ReceiverStreetName,
                                   ReceiverBlockNumber = parcel.ReceiverBlockNumber,
                                   ReceiverFlatNumber = parcel.ReceiverFlatNumber,
                                   ReceiverPostalCode = parcel.ReceiverPostalCode,
                                   ReceiverCity = parcel.ReceiverCity,
                               }).ToList();

            return sentParcels;
        }

        /// <summary>
        /// Metoda odpowiedzialna za wydanie przesyłek kurierowi przez pracownika węzła ekspedycyjno-sortującego
        /// </summary>
        /// <param name="courierId"></param>
        public void GiveCourierParcels(int courierId)
        {
            dbContext.Database.ExecuteSqlCommand("TransferParcelsToCourier @p0", courierId);
        }

        /// <summary>
        /// Metoda zwracająca listę kurierów, którzy mają zgodny id placówki z id placówki magazyniera
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
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
                                        LastName = person.LastName
                                    }).ToList();

            return couriersInBranch;
        }

        /// <summary>
        /// Metoda zwracająca listę przesyłek nowo wysłanych z określonego oddziału do węzła
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public List<ParcelViewModel> NewParcelsToRegister(int branchId)
        {
            var newParcels = (from parcel in dbContext.Parcels
                              where parcel.TargetBranchId == branchId
                              && parcel.VisibleForCourier == false
                              && parcel.ParcelStatusId == 10 // status: przesyłka w drodze do węzła
                              select new ParcelViewModel
                              {
                                  ParcelId = parcel.ParcelId,
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
