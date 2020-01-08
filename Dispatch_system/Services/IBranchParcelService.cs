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
        List<ClientParcelViewModel> NotSentParcels(short branchId);
        ClientParcelViewModel GetParcel(int parcelId);
        void MarkAsDelivered(int parcelId); // metoda do oznaczenia paczki jako dostarczona
        void Post(ClientParcelViewModel parcelViewModel);
    }
}
