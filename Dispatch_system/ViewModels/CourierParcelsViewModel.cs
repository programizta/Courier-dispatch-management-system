using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.ViewModels
{
    public class CourierParcelsViewModel
    {
        public int CourierId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public List<ParcelViewModel> ListOfParcels { get; set; }

        public CourierParcelsViewModel()
        {
            ListOfParcels = new List<ParcelViewModel>();
        }
    }
}
