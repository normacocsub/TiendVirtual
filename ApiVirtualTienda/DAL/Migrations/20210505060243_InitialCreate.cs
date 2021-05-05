using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProveedorNIT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IVA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorDescontado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    NIT = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.NIT);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Interesados",
                columns: table => new
                {
                    NIT = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsuarioEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interesados", x => x.NIT);
                    table.ForeignKey(
                        name: "FK_Interesados_Usuarios_UsuarioEmail",
                        column: x => x.UsuarioEmail,
                        principalTable: "Usuarios",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IVA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InteresadoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UsuarioVentasId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Facturas_Interesados_InteresadoId",
                        column: x => x.InteresadoId,
                        principalTable: "Interesados",
                        principalColumn: "NIT",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facturas_Usuarios_UsuarioVentasId",
                        column: x => x.UsuarioVentasId,
                        principalTable: "Usuarios",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detalle",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IVA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FacturaCodigo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalle", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Detalle_Facturas_FacturaCodigo",
                        column: x => x.FacturaCodigo,
                        principalTable: "Facturas",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalle_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_FacturaCodigo",
                table: "Detalle",
                column: "FacturaCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_ProductoId",
                table: "Detalle",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_InteresadoId",
                table: "Facturas",
                column: "InteresadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_UsuarioVentasId",
                table: "Facturas",
                column: "UsuarioVentasId");

            migrationBuilder.CreateIndex(
                name: "IX_Interesados_UsuarioEmail",
                table: "Interesados",
                column: "UsuarioEmail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalle");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Interesados");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
