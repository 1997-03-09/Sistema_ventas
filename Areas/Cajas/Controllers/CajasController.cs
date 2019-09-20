using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sistem_Ventas.Areas.Cajas.Models;
using Sistem_Ventas.Controllers;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;
using Sistem_Ventas.Models;
using Sistem_Ventas.Servicios.Interface;

namespace Sistem_Ventas.Areas.Cajas.Controllers
{
    [Authorize]
    [Area("Cajas")]
    public class CajasController : Controller
    {
        private IListObject _objeto;
        private LCajas _lcajas;
        private static String mensajeError = null;
        private static DataPaginador<TCajas> models;

        public CajasController(SignInManager<IdentityUser> signInManager, 
            ApplicationDbContext context, IListObject objeto)
        {
            _objeto = objeto;
            objeto._context = context;
            objeto._signInManager = signInManager;
            _lcajas = new LCajas(null, context);
        }
        public IActionResult Index(int id)
        {
            if (_objeto._signInManager.IsSignedIn(User))
            {
                var url = Request.Scheme + "://" + Request.Host.Value;
                Object[] objects = new Object[3];
                var data = _objeto._context.TCajas.ToList();
                if (0 < data.Count)
                {
                    objects = new Paginador<TCajas>().paginador(data, id, "Cajas", "Cajas", "Index", url);
                }
                else
                {
                    objects[0] = "No hay datos que mostrar";
                    objects[1] = "No hay datos que mostrar";
                    objects[2] = new List<TCajas>();
                }
                models = new DataPaginador<TCajas>
                {
                    List = (List<TCajas>)objects[2],
                    Pagi_info = (String)objects[0],
                    Pagi_navegacion = (String)objects[1],
                    Input = new TCajas()
                };

                return View(models);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
               
        }
        [HttpPost]
        public async Task<IActionResult> SetCaja(int caja)
        {
            try
            {
                var cajas = new TCajas
                {
                    Caja = caja,
                    Estado = true,
                    Asignada = false,
                    Usuario = "Sin usuario"
                };
                var data = _objeto._context.TCajas.Where(c => c.Caja.Equals(caja)).ToList();
                if (0 == data.Count)
                {
                    _objeto._context.Add(cajas);
                    await _objeto._context.SaveChangesAsync();
                }
                else
                {
                    mensajeError = $"El numero de {caja} ya esta registrada";
                }
            }
            catch (Exception ex)
            {

                mensajeError = ex.Message;
            }
            return Redirect("/Cajas/Index");
        }
        [HttpPost]
        public IActionResult UpdateEstado(int id)
        {
            mensajeError = _lcajas.UpdateEstado(id);
            return Redirect("/Cajas/Index");
        }
    }
}