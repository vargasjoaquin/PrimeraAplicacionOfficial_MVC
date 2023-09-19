using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrimeraAplicacionOfficial_MVC.Models
{
    public class RolPaginaCLS
    {
        public int IDROLPAGINA { get; set; }

        public string IDROL { get; set; }

        public string IDPAGINA { get; set; }

        public Nullable<int> HABILITADO { get; set; }
    }
}