using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Models
{
    public class UserAddress
    {
        [Required]
        public int UserAddressId { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public string StreetName { get; set; }

        [Required]
        public int FlatNumber { get; set; }

        [Required]
        public int BlockNumber { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string City { get; set; }
    }
}
