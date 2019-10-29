﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Models
{
    public class Branch
    {
        [Required]
        public int BranchId { get; set; }

        [Required]
        public string BranchAddress { get; set; }

        [Required]
        public short BranchCode { get; set; }
    }
}
