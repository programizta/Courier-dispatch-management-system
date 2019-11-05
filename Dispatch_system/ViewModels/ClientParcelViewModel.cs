using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.ViewModels
{
    public class ClientParcelViewModel
    {
        [Display(Name = "Id przesyłki")]
        public int ParcelId { get; set; }

        [Required(ErrorMessage = "Nazwa ulicy nadawcy jest wymagana")]
        [Display(Name = "Nazwa ulicy nadawcy")]
        public string SenderStreetName { get; set; }

        [Required(ErrorMessage = "Numer bloku nadawcy jest wymagany")]
        [Display(Name = "Numer bloku nadawcy")]
        public int SenderBlockNumber { get; set; }

        [Required(ErrorMessage = "Numer mieszkania nadawcy jest wymagany")]
        [Display(Name = "Numer mieszkania nadawcy")]
        public int SenderFlatNumber { get; set; }

        [Required(ErrorMessage = "Kod pocztowy nadawcy jest wymagany")]
        [Display(Name = "Kod pocztowy nadawcy")]
        [DataType(DataType.PostalCode)]
        public string SenderPostalCode { get; set; }

        [Required(ErrorMessage = "Miasto nadawcy jest wymagane")]
        [Display(Name = "Miasto nadawcy")]
        public string SenderCity { get; set; }

        [Required(ErrorMessage = "Nazwa ulicy odbiorcy jest wymagana")]
        [Display(Name = "Nazwa ulicy odbiorcy")]
        public string ReceiverStreetName { get; set; }

        [Required(ErrorMessage = "Numer bloku odbiorcy jest wymagany")]
        [Display(Name = "Numer bloku odbiorcy")]
        public int ReceiverBlockNumber { get; set; }

        [Required(ErrorMessage = "Numer mieszkania odbiorcy jest wymagany")]
        [Display(Name = "Numer mieszkania odbiorcy")]
        public int ReceiverFlatNumber { get; set; }

        [Required(ErrorMessage = "Kod pocztowy odbiorcy jest wymagany")]
        [Display(Name = "Kod pocztowy odbiorcy")]
        [DataType(DataType.PostalCode)]
        public string ReceiverPostalCode { get; set; }

        [Required(ErrorMessage = "Miasto odbiorcy jest wymagane")]
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
        public short SenderBranchId { get; set; }

        [Display(Name = "Id oddziału odbierającego")]
        public short ReceiverBranchId { get; set; }

        [Display(Name = "Nazwa statusu przesyłki")]
        public string Name { get; set; }

        [Display(Name = "Czy wysłana")]
        public bool IsSent { get; set; }
    }
}
