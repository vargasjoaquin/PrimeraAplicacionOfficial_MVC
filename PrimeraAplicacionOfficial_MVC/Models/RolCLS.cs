using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrimeraAplicacionOfficial_MVC.Models
{
    public class RolCLS
    {
        public int IDROL { get; set; }

        [Display (Name = "NOMBRE ROL")]
        [Required]
        [StringLength (50, ErrorMessage = "La longitud maxima es de 50 caracteres")]
        public  string NOMBRE { get; set; }


        [Display(Name = "DESCRIPCION")]
        [Required]
        [StringLength(50, ErrorMessage = "La longitud maxima es de 50 caracteres")]
        public string DESCRIPCION { get; set; }

        public Nullable<int> HABILITADO { get; set; }

        public string nombreFiltro { get; set; }
    }
}