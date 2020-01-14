using Dispatch_system.ViewModels;
using System.Collections.Generic;

namespace Dispatch_system.Services
{
    public interface IWarehouseService
    {
        void AcceptSentParcels(int branchId);
        void GiveCourierParcels(int courierId);
        CourierParcelsViewModel CourierParcels(int courierId);
        List<EmployeeViewModel> CouriersInBranch(int branchId);
        List<ParcelViewModel> NewParcelsToRegister(int branchId);
    }
}
