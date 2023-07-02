using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PrimeraAplicacionOfficial_MVC.Models
{
    public class SexoCLS
    {
        public int IDSEXO { get; set; }
        [Display(Name = "Sexo")]

        public string DESCRIPCION { get; set; }

        public int HABILITADO { get; set; }
    }
}