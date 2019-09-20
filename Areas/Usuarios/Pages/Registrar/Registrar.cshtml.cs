using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistem_Ventas.Areas.Usuarios.Controllers;
using Sistem_Ventas.Areas.Usuarios.Models;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;

namespace Sistem_Ventas.Areas.Usuarios.Pages.Registrar
{
    [Authorize(Roles = "Admin")]
    public class RegistrarModel : PageModel
    {
        private ListObject objeto = new ListObject();
        private static String idGet = null;
        private static List<IdentityUser> userList1;
        private static List<TUsuarios> userList2;
        public RegistrarModel(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ApplicationDbContext context,IHostingEnvironment environment)
        {
            objeto._roleManager = roleManager;
            objeto._userManager = userManager;
            objeto._environment = environment;
            objeto._context = context;
            objeto._usuarios = new LUsuarios();
            objeto._usersRole = new UsersRoles();
            objeto._image = new Uploadimage();
            objeto._selectList = new List<SelectListItem>();
        }
        public async Task OnGetAsync(String id)
        {
            if (id != null)
            {
                idGet = id;
                await setEditarAsync(id);
            }
            else
            {
                Input = new InputModel
                {
                   
                    rolesLista = objeto._usersRole.getRoles(objeto._roleManager)
                };
            }
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelRegistrar
        {
           
            [TempData]
            public string ErrorMessage { get; set; }
            public IFormFile AvatarImage { get; set; }
            public List<SelectListItem> rolesLista { get; set; }
        }
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> OnPostAsync()
        {
            if (idGet == null)
            {
                var valor = await guardarAsync();
                if (valor)
                {
                    return RedirectToAction(nameof(UsuariosController.Index), "Usuarios");
                }
                else
                {
                    return Page();
                }
            }
            else
            {
                var valor = await actualizarAsync();
                if (valor)
                {
                    return RedirectToAction(nameof(UsuariosController.Index), "Usuarios");
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
                    objeto._selectList.Add(new SelectListItem
                    {
                        Text = Input.Role
                    });
                    var userList = objeto._userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList();
                    if (userList.Count.Equals(0))
                    {
                        var imageName = Input.Email + ".png";
                        var user = new IdentityUser
                        {
                            UserName = Input.Email,
                            Email = Input.Email,
                            PhoneNumber = Input.Telefono
                        };
                        var result = await objeto._userManager.CreateAsync(user, Input.Password);
                        if (result.Succeeded)
                        {
                            await objeto._userManager.AddToRoleAsync(user, Input.Role);
                            var listUser = objeto._userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList();

                            var usuarios = new TUsuarios
                            {
                                Nombre = Input.Nombre,
                                Apellido = Input.Apellido,
                                NID = Input.NID,
                                Imagen = Input.Email,
                                IdUser = listUser[0].Id,
                            };
                            await objeto._context.AddAsync(usuarios);
                            objeto._context.SaveChanges();
                            await objeto._image.copiarImagenAsync(Input.AvatarImage, imageName, objeto._environment, "Usuarios",null);
                            valor = true;
                        }
                        else
                        {
                            foreach (var item in result.Errors)
                            {
                                Input = new InputModel
                                {
                                    ErrorMessage = item.Description,
                                    rolesLista = objeto._selectList
                                };
                            }
                            valor = false;
                        }

                    }
                    else
                    {
                        Input = new InputModel
                        {
                            ErrorMessage = "El " + Input.Email + " ya esta registrado",
                            rolesLista = objeto._selectList
                        };
                        valor = false;
                    }
                }
                else
                {
                    Input = new InputModel
                    {
                        ErrorMessage = "Seleccione un role",
                        rolesLista = objeto._usersRole.getRoles(objeto._roleManager)
                    };
                }
               
            }
            catch (Exception ex)
            {
                Input = new InputModel
                {
                    ErrorMessage = ex.Message,
                    rolesLista = objeto._selectList
                };
                valor = false;
            }
            return valor;
        }
        private async Task setEditarAsync(string Email)
        {
            userList1 = objeto._userManager.Users.Where(u => u.Email.Equals(Email)).ToList();
            userList2 = objeto._context.TUsuarios.Where(u => u.IdUser.Equals(userList1[0].Id)).ToList();
            var userRoles = await objeto._usersRole.getRole(objeto._userManager, objeto._roleManager, userList1[0].Id);

            Input = new InputModel
            {
                Nombre = userList2[0].Nombre,
                Apellido = userList2[0].Apellido,
                NID = userList2[0].NID,
                Telefono = userList1[0].PhoneNumber,
                Email = userList1[0].Email,
                Password = "*********",
                rolesLista = getRoles(userRoles[0].Text)
            };
        }
        private async Task<bool> actualizarAsync()
        {
            var valor = false;
            try
            {
                if (ModelState.IsValid)
                {
                    var identityUser = new IdentityUser
                    {
                        Id = userList1[0].Id,
                        UserName = Input.Email,
                        Email = Input.Email,
                        PhoneNumber = Input.Telefono,
                        EmailConfirmed = userList1[0].EmailConfirmed,
                        LockoutEnabled = userList1[0].LockoutEnabled,
                        LockoutEnd = userList1[0].LockoutEnd,
                        NormalizedEmail = userList1[0].NormalizedEmail,
                        NormalizedUserName = userList1[0].NormalizedUserName,
                        PasswordHash = userList1[0].PasswordHash,
                        PhoneNumberConfirmed = userList1[0].PhoneNumberConfirmed,
                        SecurityStamp = userList1[0].SecurityStamp,
                        TwoFactorEnabled = userList1[0].TwoFactorEnabled,
                        AccessFailedCount = userList1[0].AccessFailedCount,
                        ConcurrencyStamp = userList1[0].ConcurrencyStamp
                    };
                    objeto._context.Update(identityUser);
                    await objeto._context.SaveChangesAsync();
                    var usuarios = new TUsuarios
                    {
                        ID = userList2[0].ID,
                        Nombre = Input.Nombre,
                        Apellido = Input.Apellido,
                        NID = Input.NID,
                        Imagen = Input.Email,
                        IdUser = userList1[0].Id,
                    };
                    objeto._context.Update(usuarios);
                    await objeto._context.SaveChangesAsync();
                    var imageName = Input.Email + ".png";
                    await objeto._image.copiarImagenAsync(Input.AvatarImage, imageName, objeto._environment, "Usuarios", idGet);
          
                    valor = true;
                }
                else
                {
                    Input = new InputModel
                    {
                        ErrorMessage = "Seleccione un role",
                        rolesLista = objeto._usersRole.getRoles(objeto._roleManager)
                    };
                    valor = false;
                }
            }
            catch (Exception ex)
            {

                Input = new InputModel
                {
                    ErrorMessage = ex.Message,
                    rolesLista = getRoles(Input.Role)
                };
                valor = false;
            }

            return valor;
        }
        private List<SelectListItem> getRoles(String role)
        {
            objeto._selectList.Add(new SelectListItem
            {
                Text = role
            });
            var roles = objeto._usersRole.getRoles(objeto._roleManager);
            roles.ForEach(item => {
                if (item.Text != role)
                {
                    objeto._selectList.Add(new SelectListItem
                    {
                        Text = item.Text
                    });
                }
            });
            return objeto._selectList;
        }
    }
}