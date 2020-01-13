using Dispatch_system.ViewModels;

namespace Dispatch_system.Services
{
    public interface IClientParcelService
    {
        void PostOnline(ClientParcelViewModel clientParcelViewModel);
        ParcelStatusViewModel CheckStatus(int parcelId);
    }
}
