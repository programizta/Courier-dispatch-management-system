using Dispatch_system.Data;
using Dispatch_system.Models;
using Dispatch_system.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Dispatch_system.Services
{
    public class SQLClientParcelService : IClientParcelService
    {
        private readonly ApplicationDbContext dbContext;

        public SQLClientParcelService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ParcelStatusViewModel CheckStatus(int parcelId)
        {
            var model = (from parcel in dbContext.Parcels
                         where parcel.ParcelId == parcelId
                         select new ParcelStatusViewModel
                         {
                             IsSent = parcel.IsSent,
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
                         }).FirstOrDefault();

            var histories = (from history in dbContext.ParcelHistories
                             where history.ParcelId == parcelId
                             select new ParcelHistory
                             {
                                 ParcelHistoryId = history.ParcelHistoryId,
                                 DateTime = history.DateTime,
                                 StatusName = history.StatusName,
                                 BranchName = history.BranchName
                             }).ToList().OrderByDescending(x => x.ParcelHistoryId);

            foreach (var history in histories)
            {
                model.Histories.Add(history);
            }

            if (model != null)
            {
                return model;
            }

            return null;
        }

        public void PostOnline(ClientParcelViewModel clientParcelViewModel)
        {
            decimal volume = clientParcelViewModel.Width * clientParcelViewModel.Height * clientParcelViewModel.Depth;
            int senderBranchCode = int.Parse(new string(clientParcelViewModel.SenderPostalCode.Take(2).ToArray()));
            int receiverBranchCode = int.Parse(new string(clientParcelViewModel.ReceiverPostalCode.Take(2).ToArray()));

            short lastBranchId = dbContext.Branches.First(x => x.BranchCode == senderBranchCode).BranchId;
            short targetBranchId = dbContext.Branches.First(x => x.BranchCode == receiverBranchCode).BranchId;

            Parcel parcel = new Parcel
            {
                SenderStreetName = clientParcelViewModel.SenderStreetName,
                SenderBlockNumber = clientParcelViewModel.SenderBlockNumber,
                SenderFlatNumber = clientParcelViewModel.SenderFlatNumber,
                SenderPostalCode = clientParcelViewModel.SenderPostalCode,
                SenderCity = clientParcelViewModel.SenderCity,
                ReceiverStreetName = clientParcelViewModel.ReceiverStreetName,
                ReceiverBlockNumber = clientParcelViewModel.ReceiverBlockNumber,
                ReceiverFlatNumber = clientParcelViewModel.ReceiverFlatNumber,
                ReceiverPostalCode = clientParcelViewModel.ReceiverPostalCode,
                ReceiverCity = clientParcelViewModel.ReceiverCity,
                Weight = clientParcelViewModel.Weight,
                Volume = volume,
                IsSent = false,
                LastBranchId = lastBranchId,
                TargetBranchId = targetBranchId,
                VisibleForCourier = false,
                ParcelStatusId = 6 // "status: przesyłka nadana online"
            };

            dbContext.Add(parcel);
            dbContext.SaveChanges();
        }
    }
}
