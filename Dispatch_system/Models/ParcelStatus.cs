using System.ComponentModel.DataAnnotations;

namespace Dispatch_system.Models
{
    public class ParcelStatus
    {
        [Required]
        public short ParcelStatusId { get; set; }

        [Required]
        public string StatusName { get; set; }
    }
}