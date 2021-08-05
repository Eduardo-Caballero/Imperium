using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imperium.Models
{
    public class Comentario
    {
        public int ComentarioId { get; set; }
        public string Descripcion { get; set; }
        public string Status { get; set; }
        public int ViviendaId { get; set; }
        public Vivienda Vivienda { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
