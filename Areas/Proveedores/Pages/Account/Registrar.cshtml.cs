using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sistem_Ventas.Areas.Proveedores.Controllers;
using Sistem_Ventas.Areas.Proveedores.Models;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;
using Sistem_Ventas.Servicios.Interface;

namespace Sistem_Ventas.Areas.Proveedores.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class RegistrarModel : PageModel
    {
        private static String idGet = null;
        private IListObject _objeto;
        private static TProveedores tProveedores;
        public RegistrarModel(ApplicationDbContext context, IHostingEnvironment environment, IListObject objeto)
        {
            _objeto = objeto;
            objeto._context = context;
            objeto._environment = environment;
            objeto._image = new Uploadimage();
        }
        public IActionResult OnGet(String id)
        {
            if (id != null)
            {
                idGet = id;
                if (setEditar(id))
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
                idGet = null;
                tProveedores = new TProveedores();
                Input = new InputModel
                {
                    Email = "default"
                };
                return Page();
            }
            
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelRegistrar
        {
            [TempData]
            public string ErrorMessage { get; set; }
            public IFormFile AvatarImage { get; set; }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (idGet == null)
            {
                if (await guardarAsync())
                {
                    return RedirectToAction(nameof(ProveedoresController.Index), "Proveedores");
                }
                else
                {
                    return Page();
                }
            }
            else
            {
                if (await actualizarAsync())
                {
                    return RedirectToAction(nameof(ProveedoresController.Index), "Proveedores");
                }
                else
                {
                    return Page();
                }
            }
            
        }
        private async Task<bool> guardarAsync()
        {
            var valor = false;
            try
            {
                if (ModelState.IsValid)
                {
                    var proveedorList = _objeto._context.TProveedores.Where(u => u.Email.Equals(Input.Email)).ToList();
                    if (0 == proveedorList.Count)
                    {
                        var imageName = Input.Email + ".png";
                        var proveedor = new TProveedores
                        {
                            Proveedor = Input.Proveedor,
                            Telefono = Input.Telefono,
                            Email = Input.Email,
                            Direccion = Input.Direccion,
                        };
                        await _objeto._context.AddAsync(proveedor);
                        _objeto._context.SaveChanges();
                        var reportes = new TReportes_proveedores
                        {
                            Deuda = "$0.00",
                            FechaDeuda = DateTime.Today,
                            Pago = "$0.00",
                            FechaPago = DateTime.Today,
                            Ticket = "0000000000",
                            TProveedores = proveedor
                        };
                        await _objeto._context.AddAsync(reportes);
                        _objeto._context.SaveChanges();
                        await _objeto._image.copiarImagenAsync(Input.AvatarImage, imageName, _objeto._environment, "Proveedores", null);
                        valor = true;
                    }
                    else
                    {
                        Input = new InputModel
                        {
                            Email = "default",
                            ErrorMessage = "El " + Input.Email + " ya esta registrado",
                        };
                        valor = false;
                    }
                }
            }
            catch (Exception ex)
            {

                Input = new InputModel
                {
                    ErrorMessage = ex.Message,
                };
                valor = false;
            }
            return valor;
        }
        private bool setEditar(String Email)
        {
            try
            {
                if (new EmailAddressAttribute().IsValid(Email))
                {

                    var list = _objeto._context.TProveedores.Where(u => u.Email.Equals(Email)).ToList();
                    if (0 < list.Count)
                    {
                        tProveedores = list.ElementAt(0);
                        Input = new InputModel
                        {
                            Proveedor = tProveedores.Proveedor,
                            Telefono = tProveedores.Telefono,
                            Email = tProveedores.Email,
                            Direccion = tProveedores.Direccion,

                        };
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
                    ErrorMessage = ex.Message
                };
                return true;
            }
        }
        private async Task<bool> actualizarAsync()
        {
            var valor = false;
            try
            {
                if (ModelState.IsValid)
                {
                    var proveedor = new TProveedores
                    {
                        ID = tProveedores.ID,
                        Proveedor = Input.Proveedor,
                        Telefono = Input.Telefono,
                        Email = Input.Email,
                        Direccion = Input.Direccion,
                    };
                    _objeto._context.Update(proveedor);
                    _objeto._context.SaveChanges();
                    var imageName = Input.Email + ".png";
                    await _objeto._image.copiarImagenAsync(Input.AvatarImage, imageName, _objeto._environment, "Proveedores", idGet);
                    valor = true;
                }
            }
            catch (Exception ex)
            {

                Input = new InputModel
                {
                    ErrorMessage = ex.Message,
                    Email = Input.Email

                };
                valor = false;
            }
            return valor;
        }
    }
}