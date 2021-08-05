using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Imperium.Models
{
    public class Imagen
    {
        public int ImagenId { get; set; }
        public string Nombre { get; set; }
        public string Archivo { get; set; }
        public int ViviendaId { get; set; }
        public Vivienda Vivienda { get; set; }

        public string Contenido { get; set; }
    }
}
