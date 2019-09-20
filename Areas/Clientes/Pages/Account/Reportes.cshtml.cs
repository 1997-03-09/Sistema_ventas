using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sistem_Ventas.Areas.Clientes.Controllers;
using Sistem_Ventas.Areas.Clientes.Models;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;
using Sistem_Ventas.Models;
using Sistem_Ventas.Servicios.Interface;

namespace Sistem_Ventas.Areas.Clientes.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class ReportesModel : PageModel
    {
        private IListObject _objeto;
        private static String idGet = null,nombre,apellido;
        private static TClientes cliente;
        private static List<InputModelRegistrar> clientesList;
        private static IList<TReportes_clientes> clienteReport;
        public ReportesModel(IListObject objeto, ApplicationDbContext context)
        {
            _objeto = objeto;
            objeto._context = context;
            objeto._clientes = new LClientes(context);
        }
        public IActionResult OnGet(String id)
        {
            if (id != null)
            {
                idGet = id;
                if (setCliente(id, null))
                {
                    return Page();
                }
                else
                {
                    return RedirectToAction(nameof(ClientesController.Index), "Clientes");
                }
            }
            else
            {
                return Page();
            }
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelRegistrar
        {
            [TempData]
            public string ErrorMessage { get; set; }
            public string Ticket { get; set; }
            public string Credito { get; set; }
            public IList<TReportes_clientes> ClienteReport { get; set; }
        }
        private bool setCliente(String email, String ticket)
        {
            try
            {
                if (new EmailAddressAttribute().IsValid(email))
                {
                    clienteReport = new List<TReportes_clientes>();
                    clientesList = _objeto._clientes.getTClientes(email);
                   
                    if (0 < clientesList.Count)
                    {
                        //nombre = clientesList[0].Nombre;
                        //apellido = clientesList[0].Apellido;
                        cliente = new TClientes
                        {
                            ID = clientesList[0].ID,
                            Nombre = clientesList[0].Nombre,
                            Apellido = clientesList[0].Apellido,
                            NID = clientesList[0].NID,
                            Telefono = clientesList[0].Telefono,
                            Email = clientesList[0].Email,
                            Direccion = clientesList[0].Direccion,
                            Creditos = clientesList[0].Creditos
                        };
                        clienteReport = _objeto._context.TReportes_clientes.Where(r => r.TClientes.Equals(cliente)).ToList();
                        //if (ticket != null)
                        //{

                        //    clienteReport = _objeto._context.TReportes_clientes.Where(r => r.Ticket.Equals(ticket) && r.TClientes.Equals(cliente)).ToList();
                        //}
                        //else
                        //{
                        //    var list = _objeto._context.TReportes_clientes.Where(r => r.TClientes.Equals(cliente)).Last();
                        //    clienteReport.Add(list);
                        //}
                        Input = new InputModel
                        {
                            Nombre = clientesList[0].Nombre,
                            Apellido = clientesList[0].Apellido,
                            Email = email,
                            ClienteReport = clienteReport,
                            Credito = clientesList[0].Creditos
                        };
                    }
                    else
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                Input = new InputModel
                {
                    Nombre = clientesList[0].Nombre,
                    Apellido = clientesList[0].Apellido,
                    Email = idGet,
                    ErrorMessage = ex.Message,
                    ClienteReport = new List<TReportes_clientes>()
                };
                return true;
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (Input1.Pago != null)
            {
                if (await savePagoAsync())
                {
                    if (setCliente(idGet, Input.Ticket))
                    {
                        return Redirect("/Clientes/Account/Reportes?id="+idGet);
                    }
                    else
                    {
                        return RedirectToAction(nameof(ClientesController.Index), "Clientes");
                    }
                }
                else
                {
                    return Page();
                }
            }
            else
            {
                if (setCliente(idGet, Input.Ticket))
                {
                    return Page();
                }
                else
                {
                    return RedirectToAction(nameof(ClientesController.Index), "Clientes");
                }
            }
            
        }
        [BindProperty]
        public InputModel1 Input1 { get; set; }
        public class InputModel1
        {
            [Required(ErrorMessage = "<font color='red'>Ingrese el pago.</font>")]
            [RegularExpression(@"^[0-9]+([.][0-9]+)?$",
                ErrorMessage = "<font color='red'>El formato del pago no es válido.</font>")]
            public string Pago { get; set; }
            [TempData]
            public string ErrorMessage { get; set; }
        }
        private async Task<bool> savePagoAsync()
        {
            try
            {
                var pago = Convert.ToDecimal(Input1.Pago);
                var deuda = Convert.ToDecimal(clienteReport[0].Deuda.Replace("$", "")) - pago;
                var dataInput  = new InputModel
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Email = idGet,
                    ClienteReport = clienteReport
                };
                if (deuda.Equals(0) || deuda.Equals(0.0))
                {
                    Input = dataInput;
                    Input1 = new InputModel1
                    {
                        ErrorMessage = "El cliente no contiene deuda",
                    };
                    return true;
                }
                else
                {
                    if (deuda < pago)
                    {
                        Input = dataInput;
                        Input1 = new InputModel1
                        {
                            ErrorMessage = "Pago incorrecto",
                        };
                        return true;
                    }
                    else
                    {
                        _objeto._context.Update(cliente);
                        await _objeto._context.SaveChangesAsync();
                        var ticket = new Codigos(_objeto._context).codigoTicket("Tickets");
                        var reportes = new TReportes_clientes
                        {
                            ReportesID = clienteReport[0].ReportesID,
                            Deuda = String.Format("${0:#,###,###,##0.00####}", deuda),
                            FechaDeuda = DateTime.Today,
                            Pago = String.Format("${0:#,###,###,##0.00####}", pago),
                            FechaPago = DateTime.Today,
                            Ticket = ticket,
                            TClientes = cliente
                        };
                        _objeto._context.Update(reportes);
                        await _objeto._context.SaveChangesAsync();
                        var ticketsReport = new TTickets
                        {
                            Propietario = "Cliente",
                            Deuda = String.Format("${0:#,###,###,##0.00####}", deuda),
                            FechaDeuda = DateTime.Today,
                            Pago = String.Format("${0:#,###,###,##0.00####}", pago),
                            FechaPago = DateTime.Today,
                            Ticket = ticket,
                            Email = idGet
                        };
                        _objeto._context.Add(ticketsReport);
                        await _objeto._context.SaveChangesAsync();
                        return true;
                    }
                }
                
            }
            catch (Exception ex)
            {

                Input = new InputModel
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Email = idGet,
                    ErrorMessage = ex.Message,
                    ClienteReport = new List<TReportes_clientes>()
                };
                return false;
            }
        }
    }
}