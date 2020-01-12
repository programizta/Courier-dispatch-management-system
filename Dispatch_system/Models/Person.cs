using System.ComponentModel.DataAnnotations;

namespace Dispatch_system.Models
{
    public class Person
    {
        [Required]
        public int PersonId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}