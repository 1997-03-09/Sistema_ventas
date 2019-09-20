using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sistem_Ventas.Data.Migrations
{
    public partial class Migration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TProveedores",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Proveedor = table.Column<string>(nullable: false),
                    Telefono = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Direccion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TProveedores", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TReportes_proveedores",
                columns: table => new
                {
                    ReportesID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Deuda = table.Column<string>(nullable: true),
                    FechaDeuda = table.Column<DateTime>(nullable: false),
                    Pago = table.Column<string>(nullable: true),
                    FechaPago = table.Column<DateTime>(nullable: false),
                    Ticket = table.Column<string>(nullable: true),
                    TProveedoresID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TReportes_proveedores", x => x.ReportesID);
                    table.ForeignKey(
                        name: "FK_TReportes_proveedores_TProveedores_TProveedoresID",
                        column: x => x.TProveedoresID,
                        principalTable: "TProveedores",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TReportes_proveedores_TProveedoresID",
                table: "TReportes_proveedores",
                column: "TProveedoresID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TReportes_proveedores");

            migrationBuilder.DropTable(
                name: "TProveedores");
        }
    }
}
