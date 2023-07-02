using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrimeraAplicacionOfficial_MVC.Models
{
    public class LoginCLS
    {
        public int ID { get; set; }
        [Required]
        public string USER { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PASS { get; set; }

        public Nullable<int> HABILITADO { get; set; }
    }
}