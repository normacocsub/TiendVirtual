using System;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class TiendaVirtualContext : DbContext
    {
        public TiendaVirtualContext(DbContextOptions options):base(options)
        {}

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioInteresado> Interesados { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Factura> Facturas { get; set; }

        
    }
}
