using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Imperium.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public string Telefono { get; set; }
        public int CiudadId { get; set; }
        public Ciudad Ciudad { get; set; }
        public string Password { get; set; }
        public int PerfilId { get; set; }
        public Perfil Perfil { get; set; }
        public string Fotografia { get; set; }
        public List<Comentario> Comentarios { get; set; }
    }
}
