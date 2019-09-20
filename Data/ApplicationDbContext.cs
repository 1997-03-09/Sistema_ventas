using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sistem_Ventas.Areas.Cajas.Models;
using Sistem_Ventas.Areas.Clientes.Models;
using Sistem_Ventas.Areas.Compras.Models;
using Sistem_Ventas.Areas.Productos.Models;
using Sistem_Ventas.Areas.Proveedores.Models;
using Sistem_Ventas.Areas.Usuarios.Models;
using Sistem_Ventas.Areas.Ventas.Models;
using Sistem_Ventas.Models;

namespace Sistem_Ventas.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TUsuarios> TUsuarios { get; set; }
        public DbSet<TClientes> TClientes { get; set; }
        public DbSet<TReportes_clientes> TReportes_clientes { get; set; }
        public DbSet<TCreditos> TCreditos { get; set; }
        public DbSet<TTickets> TTickets { get; set; }
        public DbSet<TProveedores> TProveedores { get; set; }
        public DbSet<TReportes_proveedores> TReportes_proveedores { get; set; }
        public DbSet<TCompras_temp> TCompras_temp { get; set; }
        public DbSet<TCompras> TCompras { get; set; }
        public DbSet<TProductos> TProductos { get; set; }
        public DbSet<TBodega> TBodega { get; set; }
        public DbSet<TCajas> TCajas { get; set; }
        public DbSet<TCajas_registros> TCajas_registros { get; set; }
        public DbSet<TTempo_ventas> TTempo_ventas { get; set; }
        public DbSet<TTempo> TTempo { get; set; }
        public DbSet<TVentas> TVentas { get; set; }
    }
}
