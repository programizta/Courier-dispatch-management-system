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
    public class DatabaseRole
    {
        [Required]
        public int DatabaseRoleId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
