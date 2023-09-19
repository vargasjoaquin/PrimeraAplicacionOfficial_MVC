using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using PrimeraAplicacionOfficial_MVC.Models;
using iTextSharp.text;
using OfficeOpenXml;
using System.Drawing;
using iTextSharp.text.pdf;


namespace PrimeraAplicacionOfficial_MVC.Controllers
{
    public class RolPaginaController : Controller
    {
        // GET: RolPagina
        public ActionResult Index()
        {
            List<RolPaginaCLS> listaRolPagina = null;
            using (var db = new Entities2023())
            {
                listaRolPagina = (from rolPagina in db.ROL_PAGINA
                            where rolPagina.HABILITADO == 1
                            select new RolPaginaCLS
                            {
                               IDROLPAGINA = rolPagina.IDROLPAGINA,
                               IDROL = rolPagina.IDROL.ToString(),
                               IDPAGINA = rolPagina.IDPAGINA.ToString(),
                                

                            }).ToList();
            }
            return View(listaRolPagina);
        }
    }
}