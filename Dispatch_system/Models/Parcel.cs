using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Models
{
    public class Parcel
    {
        [Required]
        public int ParcelId { get; set; }

        [Required]
        public string SenderStreetName { get; set; }

        [Required]
        public int SenderBlockNumber { get; set; }

        [Required]
        public int SenderFlatNumber { get; set; }

        [Required]
        public string SenderPostalCode { get; set; }

        [Required]
        public string SenderCity { get; set; }

        [Required]
        public string ReceiverStreetName { get; set; }

        [Required]
        public int ReceiverBlockNumber { get; set; }

        [Required]
        public int ReceiverFlatNumber { get; set; }

        [Required]
        public string ReceiverPostalCode { get; set; }

        [Required]
        public string ReceiverCity { get; set; }

        //[Required] - powiedzmy, że koszt przesyłki określi pracownik nadawczy
        public decimal? Price { get; set; }

        [Required]
        public decimal Weight { get; set; }

        [Required]
        public decimal Volume { get; set; }

        // to określa pracownik oddziału
        public int? Insurance { get; set; }

        public short ParcelStatusId { get; set; }

        public short DeliveryAttempts { get; set; }

        public int? EmployeeId { get; set; }

        [Required]
        public bool IsSent { get; set; }

        [Required]
        public short SenderBranchId { get; set; }

        [Required]
        public short ReceiverBranchId { get; set; }
    }
}
