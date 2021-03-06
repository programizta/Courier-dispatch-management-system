﻿using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Id osoby")]
        public int PersonId { get; set; }
    }
}
