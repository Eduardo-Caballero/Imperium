using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Imperium.Models
{
    public class TipoVivienda
    {
        public int TipoViviendaId { get; set; }
        public string Nombre { get; set; }
        public List<Vivienda> Viviendas { get; set; }
    }
}
