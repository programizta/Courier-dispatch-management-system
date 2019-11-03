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
            var queryModel = (from parcel in context.Parcels
                              join parcelAddresses in context.ParcelsAddresses on parcel.ParcelAddressesId equals parcelAddresses.ParcelAddressesId
                              join status in context.ParcelStatuses on parcel.StatusId equals status.ParcelStatusId
                              where parcel.ParcelId == parcelId
                              select new ClientParcelViewModel
                              {
                                  SenderAddress = parcelAddresses.SenderAddress,
                                  SenderCity = parcelAddresses.SenderCity,
                                  ReceiverAddress = parcelAddresses.ReceiverAddress,
                                  ReceiverCity = parcelAddresses.ReceiverCity,
                                  Name = status.StatusName
                              });

            ClientParcelViewModel model = new ClientParcelViewModel
            {
                SenderAddress = queryModel.FirstOrDefault().SenderAddress,
                SenderCity = queryModel.FirstOrDefault().SenderCity,
                ReceiverAddress = queryModel.FirstOrDefault().ReceiverAddress,
                ReceiverCity = queryModel.FirstOrDefault().ReceiverCity,
                Name = queryModel.FirstOrDefault().Name
            };

            return model;
        }

        public void ParcelPost(ClientParcelViewModel clientParcelViewModel)
        {
            ParcelAddresses parcelAddresses = new ParcelAddresses
            {
                SenderAddress = clientParcelViewModel.SenderAddress,
                SenderCity = clientParcelViewModel.SenderCity,
                SenderPostalCode = clientParcelViewModel.SenderPostalCode,
                ReceiverAddress = clientParcelViewModel.ReceiverAddress,
                ReceiverCity = clientParcelViewModel.ReceiverCity,
                ReceiverPostalCode = clientParcelViewModel.ReceiverPostalCode
            };

            context.Add(parcelAddresses);
            context.SaveChanges();

            int parcelAddressesId = context.ParcelsAddresses.FirstOrDefault().ParcelAddressesId;
            decimal volume = clientParcelViewModel.Width * clientParcelViewModel.Height * clientParcelViewModel.Depth;
            int branchCode = int.Parse(new string(clientParcelViewModel.SenderPostalCode.Take(2).ToArray()));

            short branchId = context.Branches.First(x => x.BranchCode == branchCode).BranchId;

            Parcel parcel = new Parcel
            {
                Weight = clientParcelViewModel.Weight,
                Volume = volume,
                Insurance = clientParcelViewModel.Insurrance,
                IsSent = false,
                BranchId = branchId,
                ParcelAddressesId = parcelAddressesId,
                StatusId = 5 // "przesyłka nadana online"
            };

            context.Add(parcel);
            context.SaveChanges();
        }
    }
}
