using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sistem_Ventas.Data.Migrations
{
    public partial class Migration10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBodega",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(nullable: true),
                    Existencia = table.Column<int>(nullable: false),
                    Dia = table.Column<int>(nullable: false),
                    Mes = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    Fecha = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBodega", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TProductos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: false),
                    Precio = table.Column<string>(nullable: false),
                    Departamento = table.Column<string>(nullable: false),
                    Categoria = table.Column<string>(nullable: false),
                    Descuento = table.Column<string>(nullable: true),
                    Dia = table.Column<int>(nullable: false),
                    Mes = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    Fecha = table.Column<string>(nullable: true),
                    Compra = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TProductos", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBodega");

            migrationBuilder.DropTable(
                name: "TProductos");
        }
    }
}
