using System.ComponentModel.DataAnnotations;

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
