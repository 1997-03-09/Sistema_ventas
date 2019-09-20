using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sistem_Ventas.Areas.Ventas.Models;
using Sistem_Ventas.Controllers;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;
using Sistem_Ventas.Models;
using Sistem_Ventas.Servicios.Interface;

namespace Sistem_Ventas.Areas.Ventas.Controllers
{
    [Authorize]
    [Area("Ventas")]
    public class VentasController : Controller
    {
        private IListObject _objeto;
        private static String mensajeError = null;
        private static DataPaginador<TTempo_ventas> models;
        private ApplicationDbContext _context;

        public VentasController(SignInManager<IdentityUser> signInManager,
           UserManager<IdentityUser> userManager, ApplicationDbContext context,
           IListObject objeto)
        {
            _objeto = objeto;
            _context = context;
            objeto._signInManager = signInManager;
            objeto._ventas = new LVentas(context, userManager);
        }
        public IActionResult Ventas(int id)
        {
            if (_objeto._signInManager.IsSignedIn(User))
            {
                var url = Request.Scheme + "://" + Request.Host.Value;
                Object[] objects = new Object[3];
                var data = _context.TTempo_ventas.ToList();
                if (0 < data.Count)
                {
                    objects = new Paginador<TTempo_ventas>().paginador(data, id, "", "Ventas", "Ventas", url);
                }
                else
                {
                    objects[0] = "No hay datos que mostrar";
                    objects[1] = "No hay datos que mostrar";
                    objects[2] = new List<TTempo_ventas>();
                }
                models = new DataPaginador<TTempo_ventas>
                {
                    List = (List<TTempo_ventas>)objects[2],
                    Pagi_info = (String)objects[0],
                    Pagi_navegacion = (String)objects[1],
                    Input = new TTempo_ventas(),
                    dataCliente = new DataCliente()
                };
                return View(models);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        
        }
        [HttpPost]
        public IActionResult GetProducto(String Search)
        {
            _ = _objeto._ventas.GetProductoAsync(Search, User);
            return Redirect("/Ventas/Ventas?area=Ventas");
        }
    }
}