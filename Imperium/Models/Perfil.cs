using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Imperium.Models
{
    public class Perfil
    {
        public int PerfilId { get; set; }
        public string Nombre { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}
