using PrimeraAplicacionOfficial_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PrimeraAplicacionOfficial_MVC.Controllers
{
    public class RolController : Controller
    {
        // GET: Rol
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
             List<RolCLS> listaRol = new List<RolCLS>();
                using (var db = new Entities2023())
                {
                    listaRol = (from rol in db.ROL

                                select new RolCLS
                                {
                                    IDROL = rol.IDROL,
                                    NOMBRE = rol.NOMBRE,
                                    DESCRIPCION = rol.DESCRIPCION,
                                    HABILITADO = (int)rol.HABILITADO
                                }).ToList();
                }
                return View(listaRol);
            }
        }


        public ActionResult Filtrar(RolCLS oRolCLS)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                string nombreRol = oRolCLS.nombreFiltro;
                List<RolCLS> listaRol = new List<RolCLS>();
                using (var db = new Entities2023())
                {
                    if (nombreRol == null)
                    {
                        listaRol = (from rol in db.ROL
                                    select new RolCLS
                                    {
                                        IDROL = rol.IDROL,
                                        NOMBRE = rol.NOMBRE,
                                        DESCRIPCION = rol.DESCRIPCION
                                    }).ToList();
                    }
                    else
                    {
                        listaRol = (from rol in db.ROL
                                    where rol.NOMBRE.Contains(nombreRol)
                                    select new RolCLS
                                    {
                                        IDROL = rol.IDROL,
                                        NOMBRE = rol.NOMBRE,
                                        DESCRIPCION = rol.DESCRIPCION
                                    }).ToList();
                    }

                }
                return PartialView("_TablaRol", listaRol);
            }
        }

        public int Guardar(RolCLS oRolCLS, int titulo)
        {
            int respuesta = 0;


            using (var db = new Entities2023())
            {
                if (titulo == 1)
                {
                    ROL oRol = new ROL();
                    oRol.IDROL = oRolCLS.IDROL;
                    oRol.NOMBRE = oRolCLS.NOMBRE;
                    oRol.DESCRIPCION = oRolCLS.DESCRIPCION;
                    oRol.HABILITADO = 1;
                    db.ROL.Add(oRol);
                    respuesta = db.SaveChanges();
                }
            }
            return respuesta;
        }

        public string Guardar2(RolCLS oRolCLS, int titulo)//Guardar o editar
        {
            string rpta = "";

            try
            {
                if (!ModelState.IsValid)
                {
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();
                    rpta += "<ul class = 'list-group'>";
                    foreach (var item in query)
                    {
                        rpta += "<li class='list-group-item'>" + item + "</li>";
                    }
                    rpta += "</ul>";
                }
                else
                { 
                    using (var db = new Entities2023())
                    {
                        if (titulo == -1)
                        {
                            ROL oRol = new ROL();
                            oRol.IDROL = oRolCLS.IDROL;
                            oRol.NOMBRE = oRolCLS.NOMBRE;
                            oRol.DESCRIPCION = oRolCLS.DESCRIPCION;
                            oRol.HABILITADO = 1;
                            db.ROL.Add(oRol);
                            rpta = db.SaveChanges().ToString();
                            if (rpta == "0") rpta = "";
                        }
                        else
                        {
                            ROL oRol = db.ROL.Where(p => p.IDROL == titulo).First();
                            oRol.NOMBRE = oRolCLS.NOMBRE;
                            oRol.DESCRIPCION = oRolCLS.DESCRIPCION;
                            rpta = db.SaveChanges().ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = "";
            }

            return rpta;
        }
        
        public string Eliminar(RolCLS oRolCLS)
        {
            string respuesta = "";

            try
            {
                int id = oRolCLS.IDROL;
                using (var db = new Entities2023())
                {
                    ROL oRol = db.ROL.Where(p => p.IDROL == id).First();
                    db.ROL.Remove(oRol);
                    respuesta = db.SaveChanges().ToString();
                }
            }
            catch (Exception)
            {
                respuesta = "";
            }
            return respuesta;
        }
        
        public JsonResult RellenarCampos(int titulo)
        {
            RolCLS oRolCLS = new RolCLS();
            using (var db = new Entities2023())
            {
                ROL oRol = db.ROL.Where(p => p.IDROL == titulo).First();
                oRolCLS.NOMBRE = oRol.NOMBRE;
                oRolCLS.DESCRIPCION = oRol.DESCRIPCION;
                oRolCLS.HABILITADO = 1;
            }
            return Json(oRolCLS,JsonRequestBehavior.AllowGet);
        }
    }
}
