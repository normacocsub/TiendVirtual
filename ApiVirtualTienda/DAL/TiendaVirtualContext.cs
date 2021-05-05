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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Detalle>()
            .HasOne<Producto>().WithMany()
            .HasForeignKey(p => p.ProductoId);

            modelBuilder.Entity<Factura>()
            .HasOne<UsuarioInteresado>().WithMany()
            .HasForeignKey( f => f.InteresadoId );

            modelBuilder.Entity<Factura>()
            .HasOne<Usuario>().WithMany()
            .HasForeignKey( f => f.UsuarioVentasId );
        }
    }
}
