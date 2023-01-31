using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LandingPage.Models;

namespace LandingPage.Controllers
{
    public class SeccionesController : Controller
    {
        // GET: Secciones
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Actualizar()
        {
            using (LandingPageEntities db = new LandingPageEntities())
            {
                var lista = db.Seccion.OrderBy(x=> x.numero).ToList();
                List<object[]> tabla = new List<object[]>();
                int contador = 1;
                foreach (var item in lista)
                {
                    string estado = "";
                    string btnEstado = "";
                    if (item.estado == true)
                    {
                        estado = "Habilitado";
                        btnEstado = "<button  type='button'class='btn btn-sm btn-link' onclick='Deshabilitar(" + item.id + ")'><i class='fas fa-times'></i></button></div>";
                    }
                    else
                    {
                        estado = "Deshabilitado";
                        btnEstado = "<button type='button' class='btn btn-sm btn-link' onclick='Habilitar(" + item.id + ")'><i class='fas fa-check'></i></button></div>";
                    }
                    string btnEditar = "<div style='float:left'><button  type='button'class='btn btn-sm btn-link' data-bs-toggle='modal' data-bs-target='#modalSecciones' onclick='Mostrar(" + item.id + ")'><i class='fa fa-pen'></i></button>";
                    object[] obj = { contador, "<img src='" + item.imagen + "' width='40' height='20' />", item.titulo, item.subTitulo, estado, btnEditar + btnEstado };
                        tabla.Add(obj);
                    contador++;
                }
                return Json(tabla, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Mostrar(int id)
        {
            LandingPageEntities db = new LandingPageEntities();
            var seccion = db.Seccion.Single(x => x.id == id);
            object o = new { id = seccion.id, titulo = seccion.titulo, subTitulo = seccion.subTitulo, descripcion = seccion.descripcion, imagen = seccion.imagen, numero = seccion.numero };
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Guardar(Seccion seccion)
        {
            Status s = new Status();
            try
            {
                using (LandingPageEntities db = new LandingPageEntities())
                {
                    if (seccion.titulo == null)
                    {
                        s.Tipo = 2;
                        s.Msj = "El titulo de la seccion no debe estar vacio.";
                    }
                    if (seccion.subTitulo == null)
                    {
                        s.Tipo = 2;
                        s.Msj = "El titulo de la seccion no debe estar vacio.";
                    }
                    if (seccion.numero == 0)
                    {
                        s.Tipo = 2;
                        s.Msj = "Debe seleccionar el numero de su seccion";
                    }
                    if (s.Msj == null)
                    {
                        if (seccion.id == 0)
                        {
                            var existeSeccion = db.Seccion.SingleOrDefault(x => x.numero == seccion.numero);
                            if (existeSeccion == null)
                            {
                                seccion.estado = true;
                                db.Seccion.Add(seccion);
                                db.SaveChanges();
                                s.Tipo = 1;
                                s.Msj = "Seccion agregada";
                            }
                            else
                            {
                                s.Tipo = 2;
                                s.Msj = "Ya existe una seccion con ese numero";
                            }
                        }
                        else
                        {
                            var existeSeccion = db.Seccion.SingleOrDefault(x => x.numero == seccion.numero && x.id != seccion.id);
                            if (existeSeccion == null)
                            {
                                Seccion sec = db.Seccion.Single(x => x.id == seccion.id);
                                sec.id = seccion.id;
                                sec.titulo = seccion.titulo;
                                sec.subTitulo = seccion.subTitulo;
                                sec.descripcion = seccion.descripcion;
                                sec.imagen = seccion.imagen;
                                sec.numero = seccion.numero;
                                db.SaveChanges();
                                s.Tipo = 1;
                                s.Msj = "Seccion editada";
                            }
                            else
                            {
                                s.Tipo = 2;
                                s.Msj = "Ya existe una seccion con ese numero";
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                s.Tipo = 3;
                s.Msj = "Se produjo un error comuniquese con el administrador";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Deshabilitar(int id)
        {
            Status s = new Status();
            try
            {
                using (LandingPageEntities db = new LandingPageEntities())
                {
                    Seccion se = db.Seccion.Single(x => x.id == id);
                    se.estado = false;
                    db.SaveChanges();
                    s.Tipo = 1;
                    s.Msj = "La seccion a sido deshabilitada";
                }
            }
            catch
            {
                s.Tipo = 3;
                s.Msj = "Se produjo un error comuniquese con el administrador";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Habilitar(int id)
        {
            Status s = new Status();
            try
            {
                using (LandingPageEntities db = new LandingPageEntities())
                {
                    Seccion se = db.Seccion.Single(x => x.id == id);
                    se.estado = true;
                    db.SaveChanges();
                    s.Tipo = 1;
                    s.Msj = "La seccion a sido habilitada";
                }
            }
            catch
            {
                s.Tipo = 3;
                s.Msj = "Se produjo un error comuniquese con el administrador";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }
    }
}