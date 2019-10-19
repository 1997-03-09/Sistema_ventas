using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sistem_Ventas.Areas.Compras.Models;
using Sistem_Ventas.Areas.Proveedores.Models;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;
using Sistem_Ventas.Models;
using Sistem_Ventas.Servicios.Interface;

namespace Sistem_Ventas.Areas.Compras.Pages.Compras
{
    public class DetallesModel : PageModel
    {
        private IListObject _objeto;
        private static DataCompras _model;
        private static TCompras_temp _TCompras_temp;
        private static TProveedores tProveedores;
        private static TReportes_proveedores proveedore_Report;
        private string dia = DateTime.Now.ToString("dd");
        private string mes = DateTime.Now.ToString("MMM");
        private string year = DateTime.Now.ToString("yyy");
        public IFormFile AvatarImage;
        private string fecha = DateTime.Now.ToString("dd/MMM/yyy");
        public static String Deuda, FechaDeuda, Pago, FechaPago, Ticket;

        public DetallesModel(IListObject objeto, ApplicationDbContext context, UserManager<IdentityUser> UserManager)
        {
            _objeto = objeto;
            objeto._context = context;
            objeto._userManager = UserManager;
        }
        public void OnGet(DataCompras model)
        {
            Valor = true;
            if (model.IdProveedor.Equals(0))
            {
                var producto = _objeto._context.TCompras.Where(c => c.ID.Equals(model.ID)).ToList().ElementAt(0);
                var proveedor = _objeto._context.TProveedores.Where(c => c.ID.Equals(producto.IdProveedor)).ToList().ElementAt(0);
                model.Descripcion = producto.Descripcion;
                model.Cantidad = producto.Cantidad;
                model.Precio = producto.Precio;
                model.Importe = producto.Importe;
                model.IdProveedor = producto.IdProveedor;
                model.Proveedor = producto.Proveedor;
                model.Email = producto.Email;
                model.Direccion = proveedor.Direccion;
                model.Telefono = proveedor.Telefono;
                model.Fecha = producto.Fecha;
                model.Credito = producto.Credito;
                model.Codigo = producto.Codigo;
                Valor = false;
            }
            _model = model;
            _TCompras_temp = model;
            AvatarImage = model.AvatarImage;
            
            tProveedores = new TProveedores
            {
                ID = _model.IdProveedor,
                Proveedor = _model.Proveedor,
                Telefono = _model.Telefono,
                Email = _model.Email,
                Direccion = _model.Direccion,

            };
            proveedore_Report = _objeto._context.TReportes_proveedores.Where(r => r.TProveedores.Equals(tProveedores)).ToList().ElementAt(0);
            var deuda1 = Convert.ToDecimal(_model.Importe.Replace("$", ""));
            var deuda2 = Convert.ToDecimal(proveedore_Report.Deuda.Replace("$", ""));
            Deuda = String.Format("${0:#,###,###,##0.00####}", deuda1 + deuda2);
            FechaDeuda = fecha;
            Pago = proveedore_Report.Pago;
            FechaPago = proveedore_Report.FechaPago.ToString("dd/MMM/yyy");
            
            Ticket = new Codigos(_objeto._context).codigosTickets("Proveedor", _model.Email, "Tickets");
            Input = new InputModel
            {
                TComprasTemp = model,
                Proveedor = _model.Proveedor,
                Deuda = Deuda,
                FechaDeuda = fecha,
                Pago = proveedore_Report.Pago,
                FechaPago = proveedore_Report.FechaPago.ToString("dd/MMM/yyy"),
                Ticket = Ticket
            };
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public bool Valor { get; set; }
        public class InputModel : TCompras_temp
        {
            [TempData]
            public string ErrorMessage { get; set; }
            //public IFormFile AvatarImage { get; set; }
            public TCompras_temp TComprasTemp { get; set; }
            public String Deuda { get; set; }
            public String FechaDeuda { get; set; }
            public String Pago { get; set; }
            public String FechaPago { get; set; }
            public String Ticket { get; set; }

        }
        public async Task<IActionResult> OnPostAsync()
        {
            var valor = false;
            var strategy = _objeto._context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _objeto._context.Database.BeginTransaction())
                {
                    try
                    {
                        var idUser = _objeto._userManager.GetUserId(User);
                        var dataUser = _objeto._context.TUsuarios.Where(u => u.IdUser.Equals(idUser)).ToList().ElementAt(0);
                        var user = await _objeto._userManager.FindByIdAsync(idUser);
                        var role = await _objeto._userManager.GetRolesAsync(user);

                        _objeto._context.Add(_model);
                        await _objeto._context.SaveChangesAsync();
                        
                        var compra = new TCompras
                        {
                            Descripcion = _model.Descripcion,
                            Cantidad = _model.Cantidad,
                            Precio = _model.Precio,
                            Importe = _model.Importe,
                            IdProveedor = _model.IdProveedor,
                            Proveedor = _model.Proveedor,
                            Email = _model.Email,
                            IdUsuario = idUser,
                            Usuario = dataUser.Nombre + " " + dataUser.Apellido,
                            Role = role[0],
                            Dia = Convert.ToInt16(dia),
                            Mes = mes,
                            Year = year,
                            Fecha = _model.Fecha,
                            Codigo = _model.Codigo,
                            Credito = _model.Credito
                        };
                        //int data = Convert.ToInt16("fg");
                        _objeto._context.Add(compra);
                        await _objeto._context.SaveChangesAsync();
                        if (_model.Credito)
                        {
                           

                            _objeto._context.Update(tProveedores);
                            await _objeto._context.SaveChangesAsync();

                           
                            var reportes = new TReportes_proveedores
                            {
                                ReportesID = proveedore_Report.ReportesID,
                                Deuda = Deuda,
                                FechaDeuda = DateTime.Today,
                                Pago = proveedore_Report.Pago,
                                FechaPago = proveedore_Report.FechaPago,
                                Ticket = Ticket,
                                TProveedores = tProveedores
                            };
                            _objeto._context.Update(reportes);
                            await _objeto._context.SaveChangesAsync();
                            var ticketsReport = new TTickets
                            {
                                Propietario = "Proveedor",
                                Deuda = Deuda,
                                FechaDeuda = DateTime.Today,
                                Pago = proveedore_Report.Pago,
                                FechaPago = proveedore_Report.FechaPago,
                                Ticket = Ticket,
                                Email = _model.Email
                            };
                            _objeto._context.Add(ticketsReport);
                            await _objeto._context.SaveChangesAsync();
                        }
                        // Confirmar transacción si todos los comandos tienen éxito, la transacción se revertirá automáticamente
                        // cuando se desecha si alguno de los comandos falla
                        transaction.Commit();
                        valor = true;
                    }
                    catch (Exception ex)
                    {

                        Input = new InputModel
                        {
                            TComprasTemp = _model,
                            ErrorMessage = ex.Message
                        };
                        transaction.Rollback();
                        valor = false;
                    }
                }
            });
            if (valor)
            {
                return RedirectToPage("Compras");
            }
            else
            {
                return Page();
            }
        }
    }
}