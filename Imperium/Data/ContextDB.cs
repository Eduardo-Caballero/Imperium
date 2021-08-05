using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Imperium.Models;

namespace Imperium.Data
{
    public class ContextDB:DbContext
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {
        }

        public DbSet<Ciudad> Ciudads { get; set; }
        public DbSet<Imagen> Imagens  { get; set; }
        public DbSet<Mision> Misions { get; set; }
        public DbSet<TipoVivienda> TipoViviendas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vision> Visions { get; set; }
        public DbSet<Vivienda> Viviendas { get; set; }
        public DbSet<Perfil> Perfils { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Imperium.Models.Comentario> Comentario { get; set; }
    }
}
