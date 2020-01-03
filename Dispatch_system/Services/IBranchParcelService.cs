using Dispatch_system.Models;
using Dispatch_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Services
{
    public interface IBranchParcelService
    {
        List<ClientParcelViewModel> NotSentParcels(int branchId);
        ClientParcelViewModel GetParcel(int parcelId);
        void MarkAsDelivered(); // metoda do oznaczenia paczki jako dostarczona
        void Post(int parcelId);
        int AssignParcelToCourier(Parcel parcel);
    }
}
