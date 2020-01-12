using System.ComponentModel.DataAnnotations;

namespace Dispatch_system.ViewModels
{
    public class ParcelViewModel
    {
        [Display(Name = "Id przesyłki")]
        public int ParcelId { get; set; }

        [Display(Name = "Nazwa ulicy nadawcy")]
        public string SenderStreetName { get; set; }

        [Display(Name = "Numer mieszkania nadawcy")]
        public int SenderFlatNumber { get; set; }

        [Display(Name = "Numer bloku nadawcy")]
        public int SenderBlockNumber { get; set; }

        [Display(Name = "Kod pocztowy nadawcy")]
        [DataType(DataType.PostalCode)]
        public string SenderPostalCode { get; set; }

        [Display(Name = "Miasto nadawcy")]
        public string SenderCity { get; set; }

        [Display(Name = "Nazwa ulicy odbiorcy")]
        public string ReceiverStreetName { get; set; }

        [Display(Name = "Numer mieszkania odbiorcy")]
        public int ReceiverFlatNumber { get; set; }

        [Display(Name = "Numer bloku odbiorcy")]
        public int ReceiverBlockNumber { get; set; }

        [Display(Name = "Kod pocztowy odbiorcy")]
        [DataType(DataType.PostalCode)]
        public string ReceiverPostalCode { get; set; }

        [Display(Name = "Miasto odbiorcy")]
        public string ReceiverCity { get; set; }
    }
}