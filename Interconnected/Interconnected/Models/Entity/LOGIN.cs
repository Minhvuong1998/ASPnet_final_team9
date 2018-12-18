using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Interconnected.Models.Entity
{
    public class LOGIN
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}