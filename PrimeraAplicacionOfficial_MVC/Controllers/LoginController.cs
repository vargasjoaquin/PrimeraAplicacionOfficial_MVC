using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PrimeraAplicacionOfficial_MVC.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

namespace PrimeraAplicacionOfficial_MVC.Controllers
{
    public class LoginController : Controller
    {
        
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginCLS objUser)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Entities2023())
                {
                    var obj = db.LOGIN.Where(a => a.USER.Equals(objUser.USER) && a.PASS.Equals(objUser.PASS)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserId"] = obj.ID.ToString();
                        Session["UserName"] = obj.USER.ToString();
                        return RedirectToAction("Index","Empleado");
                    }
                }
            }
            return View(objUser);
        }
        List<SelectListItem> listaUsuario;

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(LoginCLS oLoginCLS)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.lista = listaUsuario;
                return View(oLoginCLS);
            }
            else
            {

                using (var db = new Entities2023())
                {
                    LOGIN oLogin = new LOGIN();
                    oLogin.USER = oLoginCLS.USER;
                    oLogin.PASS = oLoginCLS.PASS;
                    db.LOGIN.Add(oLogin);
                    db.SaveChanges();
                }

            }
            return RedirectToAction("Login");
        }

        

    }
}