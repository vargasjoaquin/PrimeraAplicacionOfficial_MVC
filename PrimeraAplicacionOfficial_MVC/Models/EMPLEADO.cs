//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PrimeraAplicacionOfficial_MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EMPLEADO
    {
        public int IDEMPLEADO { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public Nullable<System.DateTime> FECHACONTRATO { get; set; }
        public Nullable<decimal> SUELDO { get; set; }
        public string DNI { get; set; }
        public Nullable<int> IDTIPOUSUARIO { get; set; }
        public Nullable<int> IDTIPOCONTRATO { get; set; }
        public Nullable<int> IDSEXO { get; set; }
        public Nullable<int> HABILITADO { get; set; }
        public Nullable<int> TIENEUSUARIO { get; set; }
        public string TIPOUSUARIO { get; set; }
    
        public virtual SEXO SEXO { get; set; }
    }
}
