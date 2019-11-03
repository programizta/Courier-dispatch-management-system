using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Models
{
    public class ParcelAddresses
    {
        [Required]
        public int ParcelAddressesId { get; set; }

        [Required]
        public string SenderAddress { get; set; }

        [Required]
        public string SenderPostalCode { get; set; }

        [Required]
        public string SenderCity { get; set; }

        [Required]
        public string ReceiverAddress { get; set; }

        [Required]
        public string ReceiverPostalCode { get; set; }

        [Required]
        public string ReceiverCity { get; set; }
    }
}
