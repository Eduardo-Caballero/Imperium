using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Imperium.Models
{
    public class Vivienda
    {
        public int ViviendaId { get; set; }
        
        [Required]
        public string Nombre { get; set; }
        public List<Imagen> Imagen { get; set; }
        
        public string Transaccion { get; set; }
        
        [Required]
        public double Precio { get; set; }
        
        public int TipoViviendaId { get; set; }
        public TipoVivienda TipoVivienda { get; set; }
        
        [Required]
        public double Superficie { get; set; }
        
        public string Descripcion { get; set; }
        public string Caracteristicas { get; set; }
        
        [Required]
        public string Ubicacion { get; set; }

        public int CiudadId { get; set; }
        public Ciudad Ciudad { get; set; }

        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
        public string Imagen4 { get; set; }

        public List<Comentario> Comentarios { get; set; }
    }
}
