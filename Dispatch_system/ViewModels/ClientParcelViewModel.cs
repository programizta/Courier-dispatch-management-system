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

        [Display(Name = "Waga przesyłki")]
        public decimal Weight { get; set; }

        [Display(Name = "Wysokość")]
        public decimal Height { get; set; }

        [Display(Name = "Szerokość")]
        public decimal Width { get; set; }

        [Display(Name = "Głębokość")]
        public decimal Depth { get; set; }

        [Display(Name = "Objętość przesyłki")] // właściwość do edycji tylko przez p.punktu nadawczego
        public decimal Volume { get; set; }

        [Display(Name = "Ubezpieczenie")]
        public int? Insurrance { get; set; }

        [Display(Name = "Cena przesyłki")] // właściwość do edycji tylko przez p.punktu nadawczego
        public decimal? Price { get; set; }

        [Display(Name = "Id ostatniego oddziału, w którym była paczka")]
        public short LastBranchId { get; set; }

        [Display(Name = "Id oddziału odbierającego")]
        public short TargetBranchId { get; set; }

        [Display(Name = "Nazwa statusu przesyłki")]
        public string StatusName { get; set; }

        [Display(Name = "Czy wysłana")]
        public bool IsSent { get; set; }
    }
}
