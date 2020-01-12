using System.ComponentModel.DataAnnotations;

namespace Dispatch_system.Models
{
    public class Branch
    {
        [Required]
        public short BranchId { get; set; }

        [Required]
        public string BranchName { get; set; }

        [Required]
        public string BranchAddress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public short BranchCode { get; set; }
    }
}
