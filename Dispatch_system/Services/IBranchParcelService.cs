using Dispatch_system.ViewModels;
using System.Collections.Generic;

namespace Dispatch_system.Services
{
    public interface IBranchParcelService
    {
        List<ClientParcelViewModel> NotSentParcels(short branchId); // przesyłki nadane przez klientów online
        ClientParcelViewModel GetParcel(int parcelId); // metoda pobierająca dane o przesyłce
        void MarkAsDelivered(int parcelId); // metoda do oznaczenia paczki jako dostarczona
        void Post(ClientParcelViewModel parcelViewModel); // wprowadzenie przesyłki do systemu - niejednoznaczność???
        void RegisterParcel(ClientParcelViewModel parcelViewModel); // rejestracja przesyłki w systemie - niejednoznaczność???
        List<ParcelViewModel> ParcelsToSend(short branchId);
        void SendParcelsToMainBranch(short branchId); // wyślij nowo nadane przesyłki do węzła
        List<ParcelViewModel> ParcelsInBranchWarehouse(short branchId); // przesyłki w magazynie, które nie udało się dostarczyć przez kuriera
    }
}
