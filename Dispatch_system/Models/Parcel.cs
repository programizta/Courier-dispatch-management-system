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

        //[Required] - powiedzmy, że koszt przesyłki określi pracownik nadawczy
        public decimal Value { get; set; }

        [Required]
        public decimal Weight { get; set; }

        [Required]
        public decimal Volume { get; set; }

        public int? Insurance { get; set; }

        public short StatusId { get; set; }

        public short DeliveryAttempts { get; set; }

        public int? EmployeeId { get; set; }

        [Required]
        public short BranchId { get; set; }

        [Required]
        public bool IsSent { get; set; }

        [Required]
        public int ParcelAddressesId { get; set; }
    }
}
