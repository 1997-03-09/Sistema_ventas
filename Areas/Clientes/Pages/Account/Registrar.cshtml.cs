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
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistem_Ventas.Areas.Clientes.Controllers;
using Sistem_Ventas.Areas.Clientes.Models;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;
using Sistem_Ventas.Servicios.Interface;

namespace Sistem_Ventas.Areas.Clientes.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class RegistrarModel : PageModel
    {
        private static String idGet = null;
        private IListObject _objeto;
        private static List<TClientes> clientesList;
        public RegistrarModel(ApplicationDbContext context, IHostingEnvironment environment, IListObject objeto)
        {
            _objeto = objeto;
            objeto._context = context;
            objeto._environment = environment;
            objeto._image = new Uploadimage();
            objeto._clientes = new LClientes(context);
            objeto._selectList = new List<SelectListItem>();
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
                    return RedirectToAction(nameof(ClientesController.Index), "Clientes");
                }
                
            }
            else
            {
                Input = new InputModel
                {
                    CreditoList = _objeto._clientes.getCreditos()
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
            public List<SelectListItem> CreditoList { get; set; }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (idGet == null)
            {
                if (await guardarAsync())
                {
                    return RedirectToAction(nameof(ClientesController.Index), "Clientes");
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
                    return RedirectToAction(nameof(ClientesController.Index), "Clientes");
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
            _objeto._selectList.Add(new SelectListItem
            {
                Text = Input.Creditos
            });
            try
            {
                if (ModelState.IsValid)
                {
                    var clienteList = _objeto._context.TClientes.Where(u => u.Email.Equals(Input.Email)).ToList();
                    if (clienteList.Count.Equals(0))
                    {
                        var imageName = Input.Email + ".png";
                        var cliete = new TClientes
                        {
                            Nombre = Input.Nombre,
                            Apellido = Input.Apellido,
                            NID = Input.NID,
                            Telefono = Input.Telefono,
                            Email = Input.Email,
                            Direccion = Input.Direccion,
                            Creditos = Input.Creditos
                        };
                        await _objeto._context.AddAsync(cliete);
                        _objeto._context.SaveChanges();
                        var reportes = new TReportes_clientes
                        {
                            Deuda = "$0.00",
                            FechaDeuda = DateTime.Today,
                            Pago = "$0.00",
                            FechaPago = DateTime.Today,
                            Ticket = "00000000",
                            TClientes = cliete
                        };
                        await _objeto._context.AddAsync(reportes);
                        _objeto._context.SaveChanges();
                        await _objeto._image.copiarImagenAsync(Input.AvatarImage, imageName, _objeto._environment, "Clientes", null);
                        valor = true;
                    }
                    else
                    {
                        Input = new InputModel
                        {
                            ErrorMessage = "El " + Input.Email + " ya esta registrado",
                            CreditoList = _objeto._selectList
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
                    CreditoList = _objeto._selectList
                };
                valor = false;
            }
            return valor;
        }
        private bool setEditar(string Email)
        {
            try
            {
                if (new EmailAddressAttribute().IsValid(Email))
                {
                    clientesList = _objeto._context.TClientes.Where(u => u.Email.Equals(Email)).ToList();
                    Input = new InputModel
                    {
                        Nombre = clientesList[0].Nombre,
                        Apellido = clientesList[0].Apellido,
                        NID = clientesList[0].NID,
                        Telefono = clientesList[0].Telefono,
                        Email = clientesList[0].Email,
                        Direccion = Email = clientesList[0].Direccion,
                        CreditoList = getCreditos(clientesList[0].Creditos)
                    };
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
                    ErrorMessage = ex.Message,
                    CreditoList = new List<SelectListItem>()
                };
                return false;
            }
           
        }
        private List<SelectListItem> getCreditos(String credito)
        {
            _objeto._selectList.Add(new SelectListItem
            {
                Text = credito
            });
            var creditos = _objeto._context.TCreditos.ToList();
            creditos.ForEach(item => {
                if (item.Creditos != credito)
                {
                    _objeto._selectList.Add(new SelectListItem
                    {
                        Text = item.Creditos
                    });
                }
            });
            return _objeto._selectList;
        }
        private async Task<bool> actualizarAsync()
        {
            var valor = false;
            try
            {
                if (ModelState.IsValid)
                {
                    var cliete = new TClientes
                    {
                        ID = clientesList[0].ID,
                        Nombre = Input.Nombre,
                        Apellido = Input.Apellido,
                        NID = Input.NID,
                        Telefono = Input.Telefono,
                        Email = Input.Email,
                        Direccion = Input.Direccion,
                        Creditos = Input.Creditos
                    };
                    _objeto._context.Update(cliete);
                    _objeto._context.SaveChanges();
                    var imageName = Input.Email + ".png";
                    await _objeto._image.copiarImagenAsync(Input.AvatarImage, imageName, _objeto._environment, "Clientes", idGet);
                    valor = true;
                }
            }
            catch (Exception ex)
            {
                Input = new InputModel
                {
                    ErrorMessage = ex.Message,
                    CreditoList = getCreditos(clientesList[0].Creditos)
                };
                valor = false;
            }
            return valor;
        }
    }
}