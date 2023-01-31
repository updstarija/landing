using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LandingPage.Models;

namespace LandingPage.Controllers
{
    public class ConsultasController : Controller
    {
        // GET: Consultas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Actualizar()
        {
            LandingPageEntities db = new LandingPageEntities();
            var btn = "";
            string estado = "";
            var lista = db.Consultas.OrderByDescending(x=> x.fecha).ToList();
            List<object[]> tabla = new List<object[]>();
            int i = 1;
            foreach (var item in lista)
            {
                if (item.idEstado == 1)
                {
                    estado = "<p class='text-secondary text-center'>Pendiente</p>";
                }
                else
                {
                    estado = "<i class='text-success fas fa-check'></i>";
                }    
                btn = "<div style='float:left'><button  type='button' class='btn btn-outline-success' onclick='ResponderWhatsapp(" + item.id + ")'><i class='fab fa-whatsapp'></i></button>"+
                    "<div style='float:left'><button  type='button' class='btn btn-outline-info me-2' data-bs-toggle='modal' data-bs-target='#modalMensaje' onclick='MostrarConsulta(" + item.id + ")'><i class='fa fa-eye'></i></button>";
                string mensaje = item.mensaje;
                int caracteres = mensaje.Length;
                if (caracteres > 50)
                {
                    mensaje = item.mensaje.Substring(0,50) + "...";
                }
                object[] obj = { i, item.fecha.ToString(),  item.nombre, item.correo, item.celular, mensaje, estado, btn };
                tabla.Add(obj);
                i++;
            }
            return Json(tabla, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CambiarEstado(int id)
        {
            LandingPageEntities db = new LandingPageEntities();
            Status s = new Status();
            try
            {
                var c = db.Consultas.Single(x => x.id == id);
                c.idEstado = 2;
                db.SaveChanges();
                s.Tipo = 1;
                s.Msj = c.celular;
            }
            catch
            {
                s.Tipo = 3;
                s.Msj = "Se produjo un error comuniquese con el administrador";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MostrarConsulta(int id)
        {
            LandingPageEntities db = new LandingPageEntities();
            var c = db.Consultas.SingleOrDefault(x => x.id == id);
            object o = new { id = c.id, fecha = c.fecha.ToString(), nombre = c.nombre, correo = c.correo, celular = c.celular, mensaje = c.mensaje };
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult ListarSelect()
        //{
        //    LandingPageEntities db = new LandingPageEntities();
        //    List<object> lista = new List<object>();
        //    foreach (var item in db.TipoProrroga.OrderBy(x => x.nombre).Where(x => x.estado == true))
        //    {
        //        object o = new { id = item.id, nombre = item.nombre.ToUpper() };
        //        lista.Add(o);
        //    }
        //    return Json(lista, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult Guardar(TipoProrroga c)
        //{
        //    LandingPageEntities db = new LandingPageEntities();
        //    Status s = new Status();
        //    try
        //    {
        //        if (c.nombre == "" || c.nombre == null)
        //        {
        //            s.Tipo = 2;
        //            s.Msj = "Debe asignar un nombre";
        //        }
        //        if (s.Msj == null)
        //        {
        //            if (c.id == 0)
        //            {
        //                var existeTipoProrroga = db.TipoProrroga.SingleOrDefault(x => x.nombre == c.nombre);
        //                if (existeTipoProrroga == null)
        //                {
        //                    c.estado = true;
        //                    db.TipoProrroga.Add(c);
        //                    db.SaveChanges();
        //                    s.Tipo = 1;
        //                    s.Msj = "Tipo de prórroga registrada";
        //                }
        //                else
        //                {
        //                    s.Tipo = 2;
        //                    s.Msj = "El tipo de prórroga ya existe";
        //                }
        //            }
        //            else
        //            {
        //                var existeTipoProrroga = db.TipoProrroga.SingleOrDefault(x => x.nombre == c.nombre && x.id != c.id);
        //                if (existeTipoProrroga == null)
        //                {
        //                    var cat = db.TipoProrroga.SingleOrDefault(x => x.id == c.id);
        //                    cat.nombre = c.nombre;
        //                    cat.descripcion = c.descripcion;
        //                    db.SaveChanges();
        //                    s.Tipo = 1;
        //                    s.Msj = "Tipo de prórroga modificada";
        //                }
        //                else
        //                {
        //                    s.Tipo = 2;
        //                    s.Msj = "El tipo de prórroga ya existe";
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        s.Tipo = 3;
        //        s.Msj = "Se produjo un error comuniquese con el administrador";
        //    }
        //    return Json(s, JsonRequestBehavior.AllowGet);
        //}



        //public ActionResult Habilitar(int id)
        //{
        //    LandingPageEntities db = new LandingPageEntities();
        //    Status s = new Status();
        //    try
        //    {
        //        var c = db.TipoProrroga.SingleOrDefault(x => x.id == id);
        //        c.estado = true;
        //        db.SaveChanges();
        //        s.Tipo = 1;
        //        s.Msj = "Tipo de prórroga habilitada";
        //    }
        //    catch
        //    {
        //        s.Tipo = 3;
        //        s.Msj = "Se produjo un error comuniquese con el administrador";
        //    }
        //    return Json(s, JsonRequestBehavior.AllowGet);
        //}
    }
}