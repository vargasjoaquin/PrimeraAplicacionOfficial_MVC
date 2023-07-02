using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PrimeraAplicacionOfficial_MVC.Models
{
    public class EmpleadoCLS
    {
        public int IDEMPLEADO { get; set; }

        [Display(Name = "NOMBRE")]
        [Required]
        [StringLength(50)]

        public string NOMBRE { get; set; }

        [Display(Name = "APELLIDO")]
        [Required]
        [StringLength(50)]
        public string APELLIDO { get; set; }


        [Required]
        [Display(Name = "Fecha Contrato")]
        [DataType(DataType.Date)]//Esto es asi
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FECHACONTRATO { get; set; }

        [Display(Name = "DNI")]

        public string DNI { get; set; }



        [Required]
        public Nullable<decimal> SUELDO { get; set; }

        [Display(Name = "TIPO USUARIO")]
        public Nullable<int> IDTIPOUSUARIO { get; set; }

        [Display(Name = "TIPO CONTRATO")]
        public Nullable<int> IDTIPOCONTRATO { get; set; }

        [Display(Name = "SEXO")]
        [Required]
        public Nullable<int> IDSEXO { get; set; }
        public Nullable<int> HABILITADO { get; set; }
        public Nullable<int> TIENEUSUARIO { get; set; }

        [Display(Name = "USUARIO TIPO")]
        public string TIPOUSUARIO { get; set; }

        public virtual SexoCLS Sexo { get; set; }
    }
}