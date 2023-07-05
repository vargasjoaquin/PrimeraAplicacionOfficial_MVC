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
    public class EmpleadoController : Controller
    {

        public FileResult generarExcel()
        {
            byte[] buffer;

            using (MemoryStream ms = new MemoryStream())
            {
                ExcelPackage ep = new ExcelPackage();
                ep.Workbook.Worksheets.Add("Reporte Empleado");
                ExcelWorksheet ew = ep.Workbook.Worksheets[1];
                ew.Cells[1, 1].Value = "ID Empleado";
                ew.Cells[1, 2].Value = "Nombre";
                ew.Cells[1, 3].Value = "Apellido";
                ew.Cells[1, 4].Value = "DNI";
                ew.Cells[1, 5].Value = "Sueldo";
                

                ew.Column(1).Width = 20;
                ew.Column(2).Width = 60;
                ew.Column(3).Width = 60;
                ew.Column(4).Width = 60;
                ew.Column(5).Width = 90;
                
               
                using (var range = ew.Cells[1, 1, 1, 6])
                {
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkKhaki);
                }

                List<EmpleadoCLS> lista = (List<EmpleadoCLS>)Session["lista"];
                int nregistros = lista.Count;

                for (int i = 0; i < nregistros; i++)
                {
                    ew.Cells[i + 2, 1].Value = lista[i].IDEMPLEADO;
                    ew.Cells[i + 2, 2].Value = lista[i].NOMBRE;
                    ew.Cells[i + 2, 3].Value = lista[i].APELLIDO;
                    ew.Cells[i + 2, 4].Value = lista[i].DNI;
                    ew.Cells[i + 2, 6].Value = lista[i].SUELDO;
                    
                }

                ep.SaveAs(ms);
                buffer = ms.ToArray();
            }

            return File(buffer, "application/vnd.ms-excel");
        }


        public FileResult generarPDF()
        {
            Document doc = new Document();
            byte[] buffer;
            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                Paragraph title = new Paragraph("LISTADO DE EMPLEADOS");
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                Paragraph ESPACIO = new Paragraph("  ");
                doc.Add(ESPACIO);
                PdfPTable tabla = new PdfPTable(5);
                float[] values = new float[5] { 40, 40, 40, 40, 50 };
                tabla.SetWidths(values);

                PdfPCell celda1 = new PdfPCell(new Phrase("ID EMPLEADO"));
                celda1.BackgroundColor = new BaseColor(130, 130, 130);
                celda1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda1);

                PdfPCell celda2 = new PdfPCell(new Phrase("NOMBRE"));
                celda2.BackgroundColor = new BaseColor(130, 130, 130);
                celda2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda2);

                PdfPCell celda3 = new PdfPCell(new Phrase("APELLIDO"));
                celda3.BackgroundColor = new BaseColor(130, 130, 130);
                celda3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda3);

                PdfPCell celda4 = new PdfPCell(new Phrase("DNI"));
                celda4.BackgroundColor = new BaseColor(130, 130, 130);
                celda4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda4);

                PdfPCell celda5 = new PdfPCell(new Phrase("SUELDO"));
                celda5.BackgroundColor = new BaseColor(130, 130, 130);
                celda5.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda5);

                List<EmpleadoCLS> lista = (List<EmpleadoCLS>)Session["lista"];
                int num_reg = lista.Count();

                for (int i = 0; i < num_reg; i++)
                {
                    tabla.AddCell(lista[i].IDEMPLEADO.ToString());
                    tabla.AddCell(lista[i].NOMBRE);
                    tabla.AddCell(lista[i].APELLIDO);
                    tabla.AddCell(lista[i].DNI.ToString());
                    tabla.AddCell(lista[i].SUELDO.ToString());
                  
                }
                doc.Add(tabla);
                doc.Close();
                buffer = ms.ToArray();
                return File(buffer, "application/pdf");
            }
        }

        // GET: Empleado
        public ActionResult Index(EmpleadoCLS oEmpleadoCLS)
        {
            string apeEmp = oEmpleadoCLS.APELLIDO;
            List<EmpleadoCLS> listaEmpelados = null;

            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login","Login");
            }
            else
            {
                string userRole = Session["UserName"].ToString();

                if (userRole == "User")
                {
                    return RedirectToAction("IndexUser");
                }

                using (var db = new Entities2023())
                {
                    if (oEmpleadoCLS.APELLIDO == null)
                    {
                        listaEmpelados = (from empleado in db.EMPLEADO
                                          where empleado.HABILITADO == 1
                                          select new EmpleadoCLS
                                          {
                                              IDEMPLEADO = empleado.IDEMPLEADO,
                                              NOMBRE = empleado.NOMBRE,
                                              APELLIDO = empleado.APELLIDO,
                                              DNI = empleado.DNI,
                                              FECHACONTRATO = empleado.FECHACONTRATO,
                                              SUELDO = empleado.SUELDO,
                                              IDSEXO = empleado.IDSEXO,
                                          }).ToList();
                        Session["lista"] = listaEmpelados;
                    }
                    else
                    {
                        listaEmpelados = (from empleado in db.EMPLEADO
                                          where empleado.HABILITADO == 1 && empleado.APELLIDO.Contains(apeEmp)
                                          select new EmpleadoCLS
                                          {
                                              IDEMPLEADO = empleado.IDEMPLEADO,
                                              NOMBRE = empleado.NOMBRE,
                                              APELLIDO = empleado.APELLIDO,
                                              DNI = empleado.DNI,
                                              FECHACONTRATO = empleado.FECHACONTRATO,
                                              SUELDO = empleado.SUELDO,
                                              IDSEXO = empleado.IDSEXO,
                                          }).ToList();
                        Session["lista"] = listaEmpelados;
                    }
                }
                
                return View(listaEmpelados);
            }
        }

        public ActionResult IndexUser(EmpleadoCLS oEmpleadoCLS)
        {
            string apeEmp = oEmpleadoCLS.APELLIDO;
            List<EmpleadoCLS> listaEmpelados = null;



            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                string userRole = Session["UserName"].ToString();

                if (userRole == "Admin")
                {
                    return RedirectToAction("Index");
                }

                using (var db = new Entities2023())
                {
                    if (oEmpleadoCLS.APELLIDO == null)
                    {
                        listaEmpelados = (from empleado in db.EMPLEADO
                                          where empleado.HABILITADO == 1
                                          select new EmpleadoCLS
                                          {
                                              IDEMPLEADO = empleado.IDEMPLEADO,
                                              NOMBRE = empleado.NOMBRE,
                                              APELLIDO = empleado.APELLIDO,
                                              DNI = empleado.DNI,
                                              FECHACONTRATO = empleado.FECHACONTRATO,
                                              SUELDO = empleado.SUELDO,
                                              IDSEXO = empleado.IDSEXO,
                                          }).ToList();
                        Session["lista"] = listaEmpelados;
                    }
                    else
                    {
                        listaEmpelados = (from empleado in db.EMPLEADO
                                          where empleado.HABILITADO == 1 && empleado.APELLIDO.Contains(apeEmp)
                                          select new EmpleadoCLS
                                          {
                                              IDEMPLEADO = empleado.IDEMPLEADO,
                                              NOMBRE = empleado.NOMBRE,
                                              APELLIDO = empleado.APELLIDO,
                                              DNI = empleado.DNI,
                                              FECHACONTRATO = empleado.FECHACONTRATO,
                                              SUELDO = empleado.SUELDO,
                                              IDSEXO = empleado.IDSEXO,
                                          }).ToList();
                        Session["lista"] = listaEmpelados;
                    }
                }

                return View(listaEmpelados);
            }
        }



        List<SelectListItem> listaSexo;

        private void llenarSexo()
        {
            using (var db = new Entities2023())
            {
                listaSexo = (from sexo in db.SEXO
                            where sexo.HABILITADO ==1
                            select new SelectListItem
                            {
                                Text = sexo.DESCRIPCION,
                                Value = sexo.IDSEXO.ToString()
                            }
                            ).ToList();
                listaSexo.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
            }
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                llenarSexo();
                ViewBag.lista = listaSexo;
                return View();
            }
           
        }

        [HttpPost]
        public ActionResult Agregar(EmpleadoCLS oEmpleadoCLS)
        {
            if (!ModelState.IsValid)
            {
                llenarSexo(); //Llammos al metodo ya creado
                ViewBag.lista = listaSexo;
                return View(oEmpleadoCLS);
            }
            else
            {
                using (var db = new Entities2023())
                {

                    bool DNI_Existente = db.EMPLEADO.Any(e => e.DNI == oEmpleadoCLS.DNI);

                    if (DNI_Existente)
                    {
                        ModelState.AddModelError("DNI", "DNI Registrado");
                        llenarSexo();
                        ViewBag.lista = listaSexo;
                        return View(oEmpleadoCLS);
                    }
                    else
                    {
                        EMPLEADO oEmpleado = new EMPLEADO();
                        oEmpleado.NOMBRE = oEmpleadoCLS.NOMBRE;
                        oEmpleado.APELLIDO = oEmpleadoCLS.APELLIDO;
                        oEmpleado.DNI = oEmpleadoCLS.DNI;
                        oEmpleado.FECHACONTRATO = oEmpleadoCLS.FECHACONTRATO;
                        oEmpleado.SUELDO = oEmpleadoCLS.SUELDO;
                        oEmpleado.IDSEXO = oEmpleadoCLS.IDSEXO;
                        oEmpleado.HABILITADO = 1;
                        db.EMPLEADO.Add(oEmpleado);
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        
        public ActionResult Editar(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                llenarSexo();
                ViewBag.lista = listaSexo;
                EmpleadoCLS oEmpleadoCLS = new EmpleadoCLS();

                using (var db = new Entities2023())
                {
                    EMPLEADO oEmpleado = db.EMPLEADO.Where(p => p.IDEMPLEADO.Equals(id)).First();
                    oEmpleadoCLS.IDEMPLEADO = oEmpleado.IDEMPLEADO;
                    oEmpleadoCLS.NOMBRE = oEmpleado.NOMBRE;
                    oEmpleadoCLS.APELLIDO = oEmpleado.APELLIDO;
                    oEmpleadoCLS.DNI = oEmpleado.DNI;
                    oEmpleadoCLS.FECHACONTRATO = (DateTime)oEmpleado.FECHACONTRATO;
                    oEmpleadoCLS.SUELDO = (decimal)oEmpleado.SUELDO;
                    oEmpleadoCLS.IDSEXO = (int)oEmpleado.IDSEXO;
                }

                return View(oEmpleadoCLS);
            }
        }

        [HttpPost]
        public ActionResult Editar(EmpleadoCLS oEmpleadoCLS)
        {
            int id = oEmpleadoCLS.IDEMPLEADO;

            if (!ModelState.IsValid)
            {
                llenarSexo();
                ViewBag.lista = listaSexo;
                return View(oEmpleadoCLS);
            }
            else
            {
                using (var db = new Entities2023())
                {
                    
                    EMPLEADO oEmpleado = db.EMPLEADO.Where(p => p.IDEMPLEADO.Equals(id)).First();
                    oEmpleado.NOMBRE = oEmpleadoCLS.NOMBRE;
                    oEmpleado.APELLIDO = oEmpleadoCLS.APELLIDO;
                    oEmpleado.DNI = oEmpleadoCLS.DNI;
                    oEmpleado.FECHACONTRATO = oEmpleadoCLS.FECHACONTRATO;
                    oEmpleado.SUELDO = oEmpleadoCLS.SUELDO;
                    oEmpleado.IDSEXO = oEmpleadoCLS.IDSEXO;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");

            }
            
        }

        public ActionResult Eliminar(int id)
        {
            using (var db = new Entities2023())
            {
                EMPLEADO oEmpleado = db.EMPLEADO.Where(p => p.IDEMPLEADO.Equals(id)).First();
                oEmpleado.HABILITADO = 0;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult EmpleadosEliminados(EmpleadoCLS oEmpleadoCLS)
        {
            string apeEmp = oEmpleadoCLS.APELLIDO;
            List<EmpleadoCLS> listaEmpleados = null;

            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                string userRole = Session["UserName"].ToString();

                if (userRole == "User")
                {
                    return RedirectToAction("Index");
                }

                using (var db = new Entities2023())
                {
                    if (oEmpleadoCLS.APELLIDO == null)
                    {
                        listaEmpleados = (from empleado in db.EMPLEADO
                                          where empleado.HABILITADO == 0
                                          select new EmpleadoCLS
                                          {
                                              IDEMPLEADO = empleado.IDEMPLEADO,
                                              NOMBRE = empleado.NOMBRE,
                                              APELLIDO = empleado.APELLIDO,
                                              DNI = empleado.DNI,
                                              FECHACONTRATO = empleado.FECHACONTRATO,
                                              SUELDO = empleado.SUELDO,
                                              IDSEXO = empleado.IDSEXO,
                                          }).ToList();
                        Session["lista"] = listaEmpleados;
                    }
                    else
                    {
                        listaEmpleados = (from empleado in db.EMPLEADO
                                          where empleado.HABILITADO == 0 && empleado.APELLIDO.Contains(apeEmp)
                                          select new EmpleadoCLS
                                          {
                                              IDEMPLEADO = empleado.IDEMPLEADO,
                                              NOMBRE = empleado.NOMBRE,
                                              APELLIDO = empleado.APELLIDO,
                                              DNI = empleado.DNI,
                                              FECHACONTRATO = empleado.FECHACONTRATO,
                                              SUELDO = empleado.SUELDO,
                                              IDSEXO = empleado.IDSEXO,
                                          }).ToList();

                        Session["lista"] = listaEmpleados;
                    }
                }
                return View(listaEmpleados);
            }
        }

        public ActionResult RestaurarEmpleado(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login","Login");
            }
            else
            {
                string userRole = Session["UserName"].ToString();

                if (userRole ==  "UserName")
                {
                    return RedirectToAction("Index");
                }
            }
            using (var db = new Entities2023())
            {
                EMPLEADO oEmpleado = db.EMPLEADO.Where(p => p.IDEMPLEADO.Equals(id)).First();
                oEmpleado.HABILITADO = 1;
                db.SaveChanges();
            }
            return RedirectToAction("EmpleadosEliminados");
        }
    }


}