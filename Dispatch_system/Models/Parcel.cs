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
        public string SenderAddress { get; set; }

        [Required]
        public int SenderPostalCode { get; set; }

        [Required]
        public string SenderCity { get; set; }

        [Required]
        public string ReceiverAddress { get; set; }

        [Required]
        public int ReceiverPostalCode { get; set; }

        [Required]
        public string ReceiverCity { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public decimal Weight { get; set; }

        [Required]
        public decimal Volume { get; set; }


        public int Insurance { get; set; }


        public ParcelStatus StatusId { get; set; }


        public short DeliveryAttempts { get; set; }

        public Employee EmployeeId { get; set; }

        public Branch BranchId { get; set; }
    }
}
