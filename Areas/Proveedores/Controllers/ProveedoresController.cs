using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sistem_Ventas.Areas.Proveedores.Models;
using Sistem_Ventas.Controllers;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;
using Sistem_Ventas.Models;
using Sistem_Ventas.Servicios.Interface;

namespace Sistem_Ventas.Areas.Proveedores.Controllers
{
    [Authorize]
    [Area("Proveedores")]
    public class ProveedoresController : Controller
    {
        private IListObject _objeto;
        private static List<InputModelRegistrar> listProveedor;
        public ProveedoresController(SignInManager<IdentityUser> signInManager, ApplicationDbContext context, IListObject objeto, IHostingEnvironment hostingEnvironment)
        {
            _objeto = objeto;
            objeto._signInManager = signInManager;
            objeto._environment = hostingEnvironment;
            objeto._proveedores = new LProveedores(context);
        }

        public IActionResult Index(int id, String Search)
        {
            if (_objeto._signInManager.IsSignedIn(User))
            {
                Object[] objects = new Object[3];
                var url = Request.Scheme + "://" + Request.Host.Value;
                var data = _objeto._proveedores.getTProveedores(Search);
                if (0 < data.Count)
                {
                    objects = new Paginador<InputModelRegistrar>().paginador(data, id, "Proveedores", "Proveedores", "Index", url);
                }
                else
                {
                    objects[0] = "No data";
                    objects[1] = "No data";
                    objects[2] = new List<InputModelRegistrar>();
                }
              
                var models = new DataPaginador<InputModelRegistrar>
                {
                    List = (List<InputModelRegistrar>)objects[2],
                    Pagi_info = (String)objects[0],
                    Pagi_navegacion = (String)objects[1],
                    Input = new InputModelRegistrar()
                };
                listProveedor = models.List;
                return View(models);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            
        }
        //[HttpPost]
        public async Task<IActionResult> ExportProveedores()
        {
            var list = new List<String[]>();
            foreach (var item in listProveedor)
            {
                String[] listData ={
                    Convert.ToString(item.ID),
                    item.Proveedor,
                    item.Telefono,
                    item.Email,
                    item.Direccion
                };
                list.Add(listData);
            }
            String[] titles = { "ID", "Proveedor", "Telefono", "Email", "Direccion" };
            var export = new ExportData(_objeto._environment, Request, list, titles, "Proveedores.xlsx", "Proveedores");
            return await export.ExportExcelAsync();
        }
    }
}