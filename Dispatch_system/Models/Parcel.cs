using System.ComponentModel.DataAnnotations;

namespace Dispatch_system.Models
{
    public class Parcel
    {
        [Required]
        public int ParcelId { get; set; }

        public int? CourierId { get; set; }

        public short ParcelStatusId { get; set; }

        [Required]
        public short LastBranchId { get; set; }

        [Required]
        public short TargetBranchId { get; set; }

        [Required]
        public string SenderStreetName { get; set; }

        [Required]
        public int SenderBlockNumber { get; set; }

        [Required]
        public int SenderFlatNumber { get; set; }

        [Required]
        public string SenderCity { get; set; }

        [Required]
        public string SenderPostalCode { get; set; }

        [Required]
        public string ReceiverStreetName { get; set; }

        [Required]
        public int ReceiverBlockNumber { get; set; }

        [Required]
        public int ReceiverFlatNumber { get; set; }

        [Required]
        public string ReceiverCity { get; set; }

        [Required]
        public string ReceiverPostalCode { get; set; }

        [Required]
        public decimal? Price { get; set; }

        [Required]
        public decimal Weight { get; set; }

        [Required]
        public decimal Volume { get; set; }

        public short DeliveryAttempts { get; set; }

        [Required]
        public bool IsSent { get; set; }

        [Required]
        public bool VisibleForCourier { get; set; }
    }
}
