using System.ComponentModel.DataAnnotations;

namespace Dispatch_system.ViewModels
{
    public class PersonDataViewModel
    {
        [Display(Name = "Id osoby")]
        public int PersonId { get; set; }

        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "Nazwa ulicy")]
        public string StreetName { get; set; }

        [Display(Name = "Numer bloku")]
        public int BlockNumber { get; set; }

        [Display(Name = "Numer mieszkania")]
        public int FlatNumber { get; set; }

        [Display(Name = "Kod pocztowy")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Numer telefonu")]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
