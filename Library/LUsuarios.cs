using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sistem_Ventas.Areas.Cajas.Models;
using Sistem_Ventas.Areas.Usuarios.Models;
using Sistem_Ventas.Data;
using Sistem_Ventas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Library
{
    public class LUsuarios : ListObject
    {
        public LUsuarios()
        {

        }
        public LUsuarios(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _usersRole = new UsersRoles();
        }
        public LUsuarios(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
            _usersRole = new UsersRoles();
        }
        internal async Task<List<object[]>> userLogin(string email, string password)
        {
            try
            {
                TCajas _caja = null;
                var appUser1 = _context.Users.Where(u => u.Email.Equals(email)).ToList();
                if (0 < appUser1.Count)
                {
                    var user = appUser1.ElementAt(0);
                    _selectList = await _usersRole.getRole(_userManager, _roleManager, user.Id);
                    var cajas = _context.TCajas.Where(c => c.Estado.Equals(true) && c.Asignada.Equals(false)).ToList();
                    var appUser = _context.TUsuarios.Where(u => u.IdUser.Equals(user.Id)).ToList().ElementAt(0);
                    if (_selectList[0].Text.Equals("Admin"))
                    {
                        var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            var registro = new TCajas_registros
                            {
                                IdUsuario = user.Id,
                                Nombre = appUser.Nombre,
                                Apellido = appUser.Apellido,
                                Usuario = user.Email,
                                Role = _selectList[0].Text,
                                IdCaja = 0,
                                Caja = 0,
                                Estado = true,
                                Hora = DateTime.Now.ToString("HH:mm:ss"),
                                Dia = DateTime.Now.Day,
                                Mes = DateTime.Now.ToString("MMM"),
                                Year = DateTime.Now.ToString("yyy"),
                                Fecha = DateTime.Now.ToString("dd/MMM/yyy")
                            };
                            _context.Add(registro);
                            await _context.SaveChangesAsync();
                            code = "0";
                            description = result.Succeeded.ToString();
                        }
                        else
                        {
                            code = "1";
                            description = "Correo o contraseña inválidos";
                        }
                    }
                    else
                    {
                        if (0 < cajas.Count)
                        {
                            var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
                            if (result.Succeeded)
                            {
                                var strategy = _context.Database.CreateExecutionStrategy();
                                await strategy.ExecuteAsync(async () => {
                                    using (var transaction = _context.Database.BeginTransaction())
                                    {
                                        _caja = cajas.ElementAt(0);
                                        _caja.Asignada = true;
                                        _caja.Usuario = user.Email;
                                        _context.Update(_caja);
                                        await _context.SaveChangesAsync();

                                        var registro = new TCajas_registros
                                        {
                                            IdUsuario = user.Id,
                                            Nombre = appUser.Nombre,
                                            Apellido = appUser.Apellido,
                                            Usuario = user.Email,
                                            Role = _selectList[0].Text,
                                            IdCaja = _caja.ID,
                                            Caja = _caja.Caja,
                                            Estado = true,
                                            Hora = DateTime.Now.ToString("HH:mm:ss"),
                                            Dia = DateTime.Now.Day,
                                            Mes = DateTime.Now.ToString("MMM"),
                                            Year = DateTime.Now.ToString("yyy"),
                                            Fecha = DateTime.Now.ToString("dd/MMM/yyy")
                                        };
                                        _context.Add(registro);
                                        await _context.SaveChangesAsync();
                                        code = "0";
                                        description = result.Succeeded.ToString();
                                        transaction.Commit();
                                    }
                                });
                            }
                            else
                            {
                                code = "1";
                                description = "Correo o contraseña inválidos";
                            }
                        }
                        else
                        {
                            description = "No hay cajas disponibles";
                        }
                    }
                }
                else
                {
                    description = "Usuario no registrado";
                }


               
            }
            catch (Exception ex)
            {

                code = "2";
                description = ex.Message;
            }
            _identityError = new IdentityError
            {
                Code = code,
                Description = description
            };
            object[] data = { _identityError, _userData };
            dataList.Add(data);
            return dataList;
        }
        public async Task<List<InputModelRegistrar>> getTUsuariosAsync(String valor)
        {
            List<TUsuarios> listUser;
            if (valor == null)
            {
                listUser = _context.TUsuarios.ToList();
            }
            else
            {
                listUser = _context.TUsuarios.Where(u => u.NID.StartsWith(valor) || u.Nombre.StartsWith(valor) || u.Apellido.StartsWith(valor) || u.Imagen.StartsWith(valor)).ToList();
            }
            List<InputModelRegistrar> userList = new List<InputModelRegistrar>();
            
            foreach (var item in listUser)
            {
                _selectList = await _usersRole.getRole(_userManager, _roleManager, item.IdUser);
                userList.Add(new InputModelRegistrar
                {
                    Id = item.ID,
                    ID = item.IdUser,
                    NID = item.NID,
                    Nombre = item.Nombre,
                    Apellido = item.Apellido,
                    Role = _selectList[0].Text,
                    Email = item.Imagen
                });
                _selectList.Clear();
            }
            return userList;
        }
       /* public String userData(HttpContext HttpContext)
        {
            String role = null;
            var user = HttpContext.Session.GetString("User");
            if (user != null)
            {
                UserData dataItem = JsonConvert.DeserializeObject<UserData>(user.ToString());
                role = dataItem.Role;
            }
            else
            {
                role = "No data";
            }
            return role;
        }*/
    }
}
