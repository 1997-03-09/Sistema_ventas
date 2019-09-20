using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sistem_Ventas.Data.Migrations
{
    public partial class Migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "FechaPago",
            //    table: "TReportes_clientes",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "FechaDeuda",
            //    table: "TReportes_clientes",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldNullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Creditos",
            //    table: "TClientes",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creditos",
                table: "TClientes");

            migrationBuilder.AlterColumn<string>(
                name: "FechaPago",
                table: "TReportes_clientes",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "FechaDeuda",
                table: "TReportes_clientes",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
