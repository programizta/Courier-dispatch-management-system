using System;
using System.ComponentModel.DataAnnotations;

namespace Dispatch_system.Models
{
    public class ParcelHistory
    {
        [Required]
        public int ParcelHistoryId { get; set; }

        [Required]
        public string DateTime { get; set; }

        [Required]
        public int ParcelId { get; set; }

        [Required]
        public string StatusName { get; set; }

        [Required]
        public string BranchName { get; set; }
    }
}
