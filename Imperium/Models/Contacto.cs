using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Imperium.Models
{
    public class Contacto
    {
        public int ContactoId { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Numero { get; set; }
        public string Mensaje { get; set; }
    }
}
