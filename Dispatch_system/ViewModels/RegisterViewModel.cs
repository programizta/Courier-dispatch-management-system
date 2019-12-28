using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Pole \"Imię\" jest wymagane")]
        [Display(Name = "Imię:")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pole \"Nazwisko\" jest wymagane")]
        [Display(Name = "Nazwisko:")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Pole \"Nazwa ulicy\" jest wymagane")]
        [Display(Name = "Nazwa ulicy: ")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "Pole \"Numer bloku\" jest wymagane")]
        [Display(Name = "Numer bloku: ")]
        public int BlockNumber { get; set; }

        [Required(ErrorMessage = "Pole \"Numer mieszkania\" jest wymagane")]
        [Display(Name = "Numer mieszkania: ")]
        public int FlatNumber { get; set; }

        [Required(ErrorMessage = "Pole \"Kod pocztowy\" jest wymagane")]
        [Display(Name = "Kod pocztowy: ")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Pole \"Miasto\" jest wymagane")]
        [Display(Name = "Miasto: ")]
        public string City { get; set; }

        [Required(ErrorMessage = "Pole \"Numer telefonu\" jest wymagane")]
        [Display(Name = "Numer telefonu:")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Pole \"E-mail\" jest wymagane")]
        [EmailAddress]
        [Display(Name = "Adres e-mail:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole \"Hasło\" jest wymagane")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło:")]
        [Compare("Password",
            ErrorMessage = "Wprowadzone hasła są różne.")]
        public string ConfirmPassword { get; set; }
    }
}
