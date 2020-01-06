using Dispatch_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Services
{
    public interface ICourierService
    {
        List<ParcelViewModel> ParcelsToDeliever(int courierId); // przesyłki do dostarczenia
        List<ParcelViewModel> ParcelsToReturn(int courierId); // przesyłki do zwrotu do placówki
        void FailedToDeliever(int parcelId);
        void ParcelDelivered(int parcelId);
    }
}
