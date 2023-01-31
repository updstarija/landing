using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandingPage.Models
{
    public class MensajeClass
    {
        private string nombre;
        private string correo;
        private string celular;
        private string mensaje;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Celular { get => celular; set => celular = value; }
        public string Mensaje { get => mensaje; set => mensaje = value; }
    }
}