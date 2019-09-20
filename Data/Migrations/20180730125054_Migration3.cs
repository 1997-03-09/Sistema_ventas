using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sistem_Ventas.Data.Migrations
{
    public partial class Migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "TClientes",
            //    columns: table => new
            //    {
            //        Nombre = table.Column<string>(nullable: false),
            //        Apellido = table.Column<string>(nullable: false),
            //        NID = table.Column<string>(nullable: false),
            //        Telefono = table.Column<string>(nullable: false),
            //        Email = table.Column<string>(nullable: false),
            //        Direccion = table.Column<string>(nullable: false),
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TClientes", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TReportes_clientes",
            //    columns: table => new
            //    {
            //        ReportesID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Deuda = table.Column<string>(nullable: true),
            //        FechaDeuda = table.Column<string>(nullable: true),
            //        Pago = table.Column<string>(nullable: true),
            //        FechaPago = table.Column<string>(nullable: true),
            //        Ticket = table.Column<string>(nullable: true),
            //        TClientesID = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TReportes_clientes", x => x.ReportesID);
            //        table.ForeignKey(
            //            name: "FK_TReportes_clientes_TClientes_TClientesID",
            //            column: x => x.TClientesID,
            //            principalTable: "TClientes",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_TReportes_clientes_TClientesID",
            //    table: "TReportes_clientes",
            //    column: "TClientesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TReportes_clientes");

            migrationBuilder.DropTable(
                name: "TClientes");
        }
    }
}
