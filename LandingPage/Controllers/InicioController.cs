using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LandingPage.Models;

namespace LandingPage.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EnviarMensaje(MensajeClass m)
        {
            Status s = new Status();
            try
            {
                if (m.Nombre == null)
                {
                    s.Tipo = 2;
                    s.Msj = "Su nombre no debe estar vacío.";
                }
                if (m.Correo == null)
                {
                    s.Tipo = 2;
                    s.Msj = "Su correo no debe estar vacío.";
                }
                if (m.Celular == null)
                {
                    s.Tipo = 2;
                    s.Msj = "Su celular no debe estar vacío.";
                }
                if (m.Mensaje == null)
                {
                    s.Tipo = 2;
                    s.Msj = "Debe escribir un mensaje.";
                }
                if (s.Msj == null)
                {
                    using (LandingPageEntities db = new LandingPageEntities())
                    {
                        Consultas c = new Consultas();
                        c.fecha = DateTime.Now;
                        c.nombre = m.Nombre;
                        c.correo = m.Correo;
                        c.celular = m.Celular;
                        c.mensaje = m.Mensaje;
                        c.idEstado = 1;
                        db.Consultas.Add(c);
                        db.SaveChanges();
                        s.Tipo = 1;
                    }
                }
            }
            catch (Exception)
            {
                s.Tipo = 3;
                s.Msj = "Ha ocurrido un problema, intente más tarde.";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }


    }
}