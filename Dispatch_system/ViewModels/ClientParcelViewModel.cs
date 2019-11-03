using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.ViewModels
{
    public class ClientParcelViewModel
    {
        [Display(Name = "Adres nadawcy")]
        public string SenderAddress { get; set; }

        [Display(Name = "Kod pocztowy nadawcy")]
        public string SenderPostalCode { get; set; }

        [Display(Name = "Miasto nadawcy")]
        public string SenderCity { get; set; }

        [Display(Name = "Adres odbiorcy")]
        public string ReceiverAddress { get; set; }

        [Display(Name = "Kod pocztowy odbiorcy")]
        public string ReceiverPostalCode { get; set; }

        [Display(Name = "Miasto odbiorcy")]
        public string ReceiverCity { get; set; }

        [Display(Name = "Waga przesyłki")]
        public decimal Weight { get; set; }

        [Display(Name = "Wysokość")]
        public decimal Height { get; set; }

        [Display(Name = "Szerokość")]
        public decimal Width { get; set; }

        [Display(Name = "Głębokość")]
        public decimal Depth { get; set; }

        [Display(Name = "Ubezpieczenie")]
        public int? Insurrance { get; set; }

        [Display(Name = "Id oddziału wysyłającego")]
        public short BranchId { get; set; }

        [Display(Name = "Nazwa statusu przesyłki")]
        public string Name { get; set; }
    }
}
