using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.ViewModels
{
    public class BranchDataViewModel
    {
        [Display(Name = "Nazwa oddziału")]
        public string BranchName { get; set; }

        [Display(Name = "Adres oddziału")]
        public string BranchAddress { get; set; }

        [Display(Name = "Miasto")]
        public string BranchCity { get; set; }
    }
}
