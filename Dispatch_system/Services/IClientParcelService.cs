using Dispatch_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Services
{
    public interface IClientParcelService
    {
        void ParcelPost(ClientParcelViewModel clientParcelViewModel);
        ClientParcelViewModel CheckStatus(int parcelId);
    }
}
