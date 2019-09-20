using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sistem_Ventas.Data.Migrations
{
    public partial class Migration12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Asignada",
                table: "TCajas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Usuario",
                table: "TCajas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TCajas_registros",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdUsuario = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Apellido = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    IdCaja = table.Column<int>(nullable: false),
                    Caja = table.Column<int>(nullable: false),
                    Estado = table.Column<bool>(nullable: false),
                    Hora = table.Column<string>(nullable: true),
                    Dia = table.Column<int>(nullable: false),
                    Mes = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    Fecha = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCajas_registros", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TCajas_registros");

            migrationBuilder.DropColumn(
                name: "Asignada",
                table: "TCajas");

            migrationBuilder.DropColumn(
                name: "Usuario",
                table: "TCajas");
        }
    }
}
