using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.ViewModels
{
    public class ParcelSummaryViewModel
    {
        public int ParcelId { get; set; }

        public string BranchName { get; set; }

        public string BranchAddress { get; set; }

        public string BranchCity { get; set; }
    }
}
