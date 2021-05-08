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
        public DbSet<Detalle> Detalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Factura>()
            .HasDiscriminator<string>("factura_type")
            .HasValue<FacturaCompra>("factura_compra")
            .HasValue<FacturaVenta>("factura_venta");

            modelBuilder.Entity<Detalle>()
            .HasOne<Producto>().WithMany()
            .HasForeignKey(p => p.ProductoId);

            modelBuilder.Entity<FacturaVenta>()
            .HasOne<UsuarioInteresado>().WithMany()
            .HasForeignKey( f => f.InteresadoId );

            modelBuilder.Entity<FacturaVenta>()
            .HasOne<Usuario>().WithMany()
            .HasForeignKey( f => f.UsuarioVentasId );

            modelBuilder.Entity<Producto>()
            .HasOne<Proveedor>().WithMany()
            .HasForeignKey( f => f.ProveedorNIT );

            modelBuilder.Entity<Detalle>()
            .HasOne<Factura>().WithMany()
            .HasForeignKey( d => d.FacturaCodigo);
        }
    }
}
