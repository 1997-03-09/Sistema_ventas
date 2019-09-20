using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sistem_Ventas.Data.Migrations
{
    public partial class Migration8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TCompras",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Precio = table.Column<string>(nullable: false),
                    Importe = table.Column<string>(nullable: true),
                    IdProveedor = table.Column<int>(nullable: false),
                    Proveedor = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    IdUsuario = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Dia = table.Column<int>(nullable: false),
                    Mes = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    Fecha = table.Column<string>(nullable: true),
                    Codigo = table.Column<string>(nullable: true),
                    Credito = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCompras", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TCompras_temp",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    Cantidad = table.Column<int>(nullable: false),
                    Precio = table.Column<string>(nullable: true),
                    Importe = table.Column<string>(nullable: true),
                    IdProveedor = table.Column<int>(nullable: false),
                    Proveedor = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Fecha = table.Column<string>(nullable: true),
                    Credito = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCompras_temp", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TCompras");

            migrationBuilder.DropTable(
                name: "TCompras_temp");
        }
    }
}
