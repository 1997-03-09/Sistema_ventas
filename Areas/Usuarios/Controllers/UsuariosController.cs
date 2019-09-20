using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sistem_Ventas.Areas.Cajas.Models;
using Sistem_Ventas.Areas.Usuarios.Models;
using Sistem_Ventas.Controllers;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;
using Sistem_Ventas.Models;
using Sistem_Ventas.Servicios.Interface;

namespace Sistem_Ventas.Areas.Usuarios.Controllers
{
    [Authorize]
    [Area("Usuarios")]
    public class UsuariosController : Controller
    {
        private IListObject _objeto;
        public UsuariosController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, IListObject objeto)
        {
            _objeto = objeto;
            _objeto._context = context;
            objeto._signInManager = signInManager;
            objeto._cajas = new LCajas(userManager, context);
            objeto._usuarios = new LUsuarios(userManager, signInManager, roleManager, context);
        }
        public async Task<IActionResult> Index(int id, String Search)
        {
            if (_objeto._signInManager.IsSignedIn(User))
            {
                var url = Request.Scheme + "://" + Request.Host.Value;
                var objects = new Paginador<InputModelRegistrar>().paginador(await _objeto._usuarios.getTUsuariosAsync(Search)
                    ,id, "Usuarios", "Usuarios", "Index",url);
                var models = new DataPaginador<InputModelRegistrar>
                {
                    List = (List<InputModelRegistrar>)objects[2],
                    Pagi_info = (String)objects[0],
                    Pagi_navegacion = (String)objects[1],
                    Input = new InputModelRegistrar()
                };

                return View(models);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        public async Task<IActionResult> SessionClose()
        {
            var data = _objeto._cajas.GetCajas_registros(User);
            var cajas = new TCajas
            {
                ID = data[0],
                Caja = data[1],
                Estado = true,
                Asignada = false,
                Usuario = "Sin usuario"
            };
            _objeto._context.Update(cajas);
            await _objeto._context.SaveChangesAsync();
            //HttpContext.Session.Remove("User");
            await _objeto._signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}