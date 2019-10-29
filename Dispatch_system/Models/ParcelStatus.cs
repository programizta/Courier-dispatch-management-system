using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Models
{
    public class ParcelStatus
    {
        [Required]
        public int ParcelStatusId { get; set; }

        [Required]
        public string StatusName { get; set; }
    }
}
