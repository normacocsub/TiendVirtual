﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(TiendaVirtualContext))]
    partial class TiendaVirtualContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entity.Detalle", b =>
                {
                    b.Property<string>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<decimal>("Descuento")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("FacturaCodigo")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("IVA")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Codigo");

                    b.HasIndex("FacturaCodigo");

                    b.HasIndex("ProductoId");

                    b.ToTable("Detalle");
                });

            modelBuilder.Entity("Entity.Factura", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<decimal>("Descuento")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("IVA")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("InteresadoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Codigo");

                    b.HasIndex("InteresadoId");

                    b.ToTable("Facturas");
                });

            modelBuilder.Entity("Entity.Producto", b =>
                {
                    b.Property<string>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Descuento")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("IVA")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProveedorNIT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ValorDescontado")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Codigo");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("Entity.Proveedor", b =>
                {
                    b.Property<string>("NIT")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NIT");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("Entity.Usuario", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Entity.UsuarioInteresado", b =>
                {
                    b.Property<string>("NIT")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UsuarioEmail")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("NIT");

                    b.HasIndex("UsuarioEmail");

                    b.ToTable("Interesados");
                });

            modelBuilder.Entity("Entity.Detalle", b =>
                {
                    b.HasOne("Entity.Factura", null)
                        .WithMany("Detalles")
                        .HasForeignKey("FacturaCodigo");

                    b.HasOne("Entity.Producto", null)
                        .WithMany()
                        .HasForeignKey("ProductoId");
                });

            modelBuilder.Entity("Entity.Factura", b =>
                {
                    b.HasOne("Entity.UsuarioInteresado", null)
                        .WithMany()
                        .HasForeignKey("InteresadoId");
                });

            modelBuilder.Entity("Entity.UsuarioInteresado", b =>
                {
                    b.HasOne("Entity.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioEmail");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Entity.Factura", b =>
                {
                    b.Navigation("Detalles");
                });
#pragma warning restore 612, 618
        }
    }
}
