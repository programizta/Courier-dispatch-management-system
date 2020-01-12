using System.ComponentModel.DataAnnotations;

namespace Dispatch_system.ViewModels
{
    public class EmployeeViewModel : PersonDataViewModel
    {
        [Display(Name = "Id pracownika")]
        public int EmployeeId { get; set; }

        [Display(Name = "Nazwa oddziału")]
        public string BranchName { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Stanowisko")]
        public string Role { get; set; }

        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }
    }
}
