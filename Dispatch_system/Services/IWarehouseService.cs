using Dispatch_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Services
{
    public interface IWarehouseService
    {
        void AcceptSentParcels(int branchId);
        void GiveCourierParcels(int courierId);
        List<ParcelViewModel> CourierParcels(int courierId);
        List<EmployeeViewModel> CouriersInBranch(int branchId);
        List<ParcelViewModel> NewParcelsToRegister(int branchId);
    }
}
