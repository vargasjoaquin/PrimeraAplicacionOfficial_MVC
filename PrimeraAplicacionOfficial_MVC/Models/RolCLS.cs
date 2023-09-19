using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrimeraAplicacionOfficial_MVC.Models
{
    public class RolCLS
    {
        public int IDROL { get; set; }

        public  string NOMBRE { get; set; }

        public string DESCRIPCION { get; set; }

        public Nullable<int> HABILITADO { get; set; }

        public string nombreFiltro { get; set; }
    }
}