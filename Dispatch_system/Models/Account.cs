using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Models
{
    /// <summary>
    /// ta klasa się raczej nie przyda
    /// </summary>
    public class Account
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        public Person PersonId { get; set; }
    }
}
