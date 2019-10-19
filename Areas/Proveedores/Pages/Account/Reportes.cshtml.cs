using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sistem_Ventas.Areas.Proveedores.Controllers;
using Sistem_Ventas.Areas.Proveedores.Models;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;
using Sistem_Ventas.Models;
using Sistem_Ventas.Servicios.Interface;

namespace Sistem_Ventas.Areas.Proveedores.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class ReportesModel : PageModel
    {
        private IListObject _objeto;
        private static String idGet = null,proveedor;
        private static InputModelRegistrar inputModel;
        private static TProveedores tProveedores;
        private static TReportes_proveedores proveedore_Report;
        public static IList<TReportes_proveedores> proveedoresReport;
        public ReportesModel(IListObject objeto, ApplicationDbContext context)
        {
            _objeto = objeto;
            objeto._context = context;
            objeto._proveedores = new LProveedores(context);
        }
        public IActionResult OnGet(String id)
        {
            if (id != null)
            {
                idGet = id;
                if (setProveedores(id))
                {
                    return Page();
                }
                else
                {
                    return RedirectToAction(nameof(ProveedoresController.Index), "Proveedores");
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
            public IList<TReportes_proveedores> ProveedoresReport { get; set; }
        }
        private bool setProveedores(String email)
        {
            try
            {
                if (new EmailAddressAttribute().IsValid(email))
                {
                    var proveedorList = _objeto._proveedores.getTProveedores(email).ToList();
                    if (0 < proveedorList.Count)
                    {
                        inputModel = proveedorList.ElementAt(0);
                        tProveedores = new TProveedores
                        {
                            ID = inputModel.ID,
                            Proveedor = inputModel.Proveedor,
                            Telefono = inputModel.Telefono,
                            Email = inputModel.Email,
                            Direccion = inputModel.Direccion,

                        };
                        proveedoresReport = _objeto._context.TReportes_proveedores.Where(r => r.TProveedores.Equals(tProveedores)).ToList();
                        if (0 < proveedoresReport.Count)
                        {
                            proveedore_Report = proveedoresReport.ElementAt(0);
                            Input = new InputModel
                            {
                                Proveedor = inputModel.Proveedor,
                                Email = email,
                                ProveedoresReport = proveedoresReport,

                            };

                        }
                        else
                        {
                            Input = new InputModel
                            {
                                Proveedor = inputModel.Proveedor,
                                Email = idGet,
                                ErrorMessage = "El proveedor "+ inputModel.Proveedor+" no contiene repoprtes",
                                ProveedoresReport = new List<TReportes_proveedores>()
                            };
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
                    Proveedor = inputModel.Proveedor,
                    Email = idGet,
                    ErrorMessage = ex.Message,
                    ProveedoresReport = new List<TReportes_proveedores>()
                };
                return true;
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
        public async Task<IActionResult> OnPostAsync()
        {
            if (Input1.Pago != null)
            {
                if (await savePagoAsync())
                {
                    if (setProveedores(idGet))
                    {
                        return Redirect("/Proveedores/Account/Reportes?id=" + idGet);
                    }
                    else
                    {
                        return RedirectToAction(nameof(ProveedoresController.Index), "Proveedores");
                    }
                }
                else
                {
                    return Page();
                }
            }
            else
            {
                if (setProveedores(idGet))
                {
                    return Page();
                }
                else
                {
                    return RedirectToAction(nameof(ProveedoresController.Index), "Proveedores");
                }
            }
        }
        private async Task<bool> savePagoAsync()
        {
            try
            {
                var pago = Convert.ToDecimal(Input1.Pago);
                var deuda = Convert.ToDecimal(proveedore_Report.Deuda.Replace("$", "")) - pago;
                var dataInput = new InputModel
                {
                    Proveedor = proveedor,
                    Email = idGet,
                    ProveedoresReport = proveedoresReport
                };
                if (proveedore_Report.Deuda.Equals("$0.00"))
                {
                    Input = dataInput;
                    Input1 = new InputModel1
                    {
                        ErrorMessage = "El proveedor no contiene deuda",
                    };
                    return false;
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
                        return false;
                    }
                    else
                    {
                        var ticket = new Codigos(_objeto._context).codigosTickets("Proveedor", idGet, "Tickets");
                        _objeto._context.Update(tProveedores);
                        await _objeto._context.SaveChangesAsync();
                        var reportes = new TReportes_proveedores
                        {
                            ReportesID = proveedore_Report.ReportesID,
                            Deuda = String.Format("${0:#,###,###,##0.00####}", deuda),
                            FechaDeuda = DateTime.Today,
                            Pago = String.Format("${0:#,###,###,##0.00####}", pago),
                            FechaPago = DateTime.Today,
                            Ticket = ticket,
                            TProveedores = tProveedores
                        };
                        _objeto._context.Update(reportes);
                        await _objeto._context.SaveChangesAsync();
                        var ticketsReport = new TTickets
                        {
                            Propietario = "Proveedor",
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
                    Proveedor = proveedor,
                    Email = idGet,
                    ErrorMessage = ex.Message,
                    ProveedoresReport = new List<TReportes_proveedores>()
                };
                return false;
            }
            
        }
    }
}