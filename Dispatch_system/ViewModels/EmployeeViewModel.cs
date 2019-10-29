using Dispatch_system.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.ViewModels
{
    public class EmployeeViewModel
    {
        [Display(Name = "Id")]
        public int EmployeeId { get; set; }

        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Display(Name = "Numer telefonu")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "Adres e-mail:")]
        [EmailAddress]
        public string Email { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Stanowisko")]
        public string Role { get; set; }
    }
}
