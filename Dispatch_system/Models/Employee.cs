using System.ComponentModel.DataAnnotations;

namespace Dispatch_system.Models
{
    public class Employee
    {
        [Required]
        public int EmployeeId { get; set; }

        public int? PersonId { get; set; }

        [Required]
        public short BranchId { get; set; }

        [Required]
        public bool IsCourier { get; set; }
    }
}
