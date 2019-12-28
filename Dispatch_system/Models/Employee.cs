using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Models
{
    public class Employee
    {
        [Required]
        public int EmployeeId { get; set; }

        public int? PersonId { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public bool IsCourier { get; set; }
    }
}
