using System.ComponentModel.DataAnnotations;

namespace Dispatch_system.ViewModels
{
    public class ClientParcelViewModel : ParcelViewModel
    {
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
