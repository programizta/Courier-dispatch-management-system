using Dispatch_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.ViewModels
{
    public class ParcelStatusViewModel : ParcelViewModel
    {
        public List<ParcelHistory> Histories { get; set; }
        public bool IsSent { get; set; }

        public ParcelStatusViewModel()
        {
            Histories = new List<ParcelHistory>();
        }
    }
}
