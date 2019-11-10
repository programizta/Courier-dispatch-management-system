using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dispatch_system.Data;
using Dispatch_system.Models;
using Dispatch_system.ViewModels;

namespace Dispatch_system.Services
{
    public class SQLClientParcelService : IClientParcelService
    {
        private readonly ApplicationDbContext context;

        public SQLClientParcelService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ClientParcelViewModel CheckStatus(int parcelId)
        {
            var model = (from parcel in context.Parcels
                         join status in context.ParcelStatuses on parcel.ParcelStatusId equals status.ParcelStatusId
                         where parcel.ParcelId == parcelId
                         select new ClientParcelViewModel
                         {
                             IsSent = parcel.IsSent,
                             ParcelId = parcel.ParcelId,
                             SenderStreetName = parcel.SenderStreetName,
                             SenderBlockNumber = parcel.SenderBlockNumber,
                             SenderFlatNumber = parcel.SenderFlatNumber,
                             SenderCity = parcel.SenderCity,
                             SenderPostalCode = parcel.SenderPostalCode,
                             ReceiverStreetName = parcel.ReceiverStreetName,
                             ReceiverBlockNumber = parcel.ReceiverBlockNumber,
                             ReceiverFlatNumber = parcel.ReceiverFlatNumber,
                             ReceiverCity = parcel.ReceiverCity,
                             ReceiverPostalCode = parcel.ReceiverPostalCode,
                             Name = status.StatusName
                         });

            return model.First();
        }

        public void ParcelPost(ClientParcelViewModel clientParcelViewModel)
        {
            decimal volume = clientParcelViewModel.Width * clientParcelViewModel.Height * clientParcelViewModel.Depth;
            int senderBranchCode = int.Parse(new string(clientParcelViewModel.SenderPostalCode.Take(2).ToArray()));
            int receiverBranchCode = int.Parse(new string(clientParcelViewModel.ReceiverPostalCode.Take(2).ToArray()));

            short senderBranchId = context.Branches.First(x => x.BranchCode == senderBranchCode).BranchId;
            short receiverBranchId = context.Branches.First(x => x.BranchCode == receiverBranchCode).BranchId;

            Parcel parcel = new Parcel
            {
                SenderStreetName = clientParcelViewModel.SenderStreetName,
                SenderBlockNumber = clientParcelViewModel.SenderBlockNumber,
                SenderFlatNumber = clientParcelViewModel.SenderFlatNumber,
                SenderCity = clientParcelViewModel.SenderCity,
                SenderPostalCode = clientParcelViewModel.SenderPostalCode,
                ReceiverStreetName = clientParcelViewModel.ReceiverStreetName,
                ReceiverBlockNumber = clientParcelViewModel.ReceiverBlockNumber,
                ReceiverFlatNumber = clientParcelViewModel.ReceiverFlatNumber,
                ReceiverCity = clientParcelViewModel.ReceiverCity,
                ReceiverPostalCode = clientParcelViewModel.ReceiverPostalCode,
                Weight = clientParcelViewModel.Weight,
                Volume = volume,
                IsSent = false,
                SenderBranchId = senderBranchId,
                ReceiverBranchId = receiverBranchId,
                ParcelStatusId = 6 // "przesyłka nadana online"
            };

            context.Add(parcel);
            context.SaveChanges();
        }

        public ClientParcelViewModel ValidateParcelData(int parcelId)
        {
            throw new NotImplementedException();
        }

        public List<ClientParcelViewModel> NotSentParcels(int branchId)
        {
            var parcelList = (from parcel in context.Parcels
                              join status in context.ParcelStatuses on parcel.ParcelStatusId equals status.ParcelStatusId
                              where parcel.SenderBranchId == branchId && parcel.IsSent == false
                              select new ClientParcelViewModel
                              {
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
                                  ReceiverCity = parcel.ReceiverCity
                              }
                ).ToList();

            return parcelList;
        }

        public ClientParcelViewModel GetParcel(int parcelId)
        {
            var parcelData = CheckStatus(parcelId);


            parcelData.Weight = context.Parcels.FirstOrDefault(x => x.ParcelId == parcelId).Weight;
            parcelData.Volume = context.Parcels.FirstOrDefault(x => x.ParcelId == parcelId).Volume;

            // te wartości będą obliczane gdy będę przechodził do podsumowania przesyłki
            //parcelData.Insurrance = context.Parcels.FirstOrDefault(x => x.ParcelId == parcelId).Insurance;
            //parcelData.Price = 8 + weight * (decimal)0.1 + volume * (decimal)0.02;

            return parcelData;
        }
    }
}
