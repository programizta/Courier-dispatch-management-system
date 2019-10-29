using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Models
{
    public class ParcelHistory
    {
        [Required]
        public int ParcelHistoryId { get; set; }

        [Required]
        public Parcel ParcelId { get; set; }

        [Required]
        public ParcelStatus StatusId { get; set; }

        [Required]
        public DateTime DateOfEvent { get; set; }
    }
}
