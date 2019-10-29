using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Pole \"E-mail\" jest wymagane")]
        [EmailAddress]
        [Display(Name = "Adres e-mail:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole \"Hasło\" jest wymagane")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło:")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }
    }
}
