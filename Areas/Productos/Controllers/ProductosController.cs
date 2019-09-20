using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sistem_Ventas.Areas.Compras.Models;
using Sistem_Ventas.Controllers;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;
using Sistem_Ventas.Models;
using Sistem_Ventas.Servicios.Interface;

namespace Sistem_Ventas.Areas.Productos.Controllers
{
    [Authorize]
    [Area("Productos")]
    public class ProductosController : Controller
    {
        private IListObject _objeto;
        private static List<TCompras_temp> listCompras;
        public ProductosController(SignInManager<IdentityUser> signInManager, ApplicationDbContext context, IListObject objeto, IHostingEnvironment hostingEnvironment)
        {
            _objeto = objeto;
            objeto._signInManager = signInManager;
            objeto._environment = hostingEnvironment;
            objeto._productos = new LProductos(context);
        }
        public IActionResult Index(int id, String Search)
        {
            if (_objeto._signInManager.IsSignedIn(User))
            {
                Object[] objects = new Object[3];
                var url = Request.Scheme + "://" + Request.Host.Value;
                var data = _objeto._productos.getTCompras_temp(Search);
                if (0 < data.Count)
                {
                    objects = new Paginador<TCompras_temp>().paginador(data, id, "Productos", "Productos", "Index", url);
                }
                else
                {
                    objects[0] = "No data";
                    objects[1] = "No data";
                    objects[2] = new List<TCompras_temp>();
                }
                var models = new DataPaginador<TCompras_temp>
                {
                    List = (List<TCompras_temp>)objects[2],
                    Pagi_info = (String)objects[0],
                    Pagi_navegacion = (String)objects[1],
                    Input = new TCompras_temp()
                };
                listCompras = models.List;
                return View(models);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
               
        }
        public async Task<IActionResult> ExportCompras()
        {
            var list = new List<String[]>();
            foreach (var item in listCompras)
            {
                String[] listData ={
                    item.ID.ToString(),
                    item.Descripcion,
                    item.Cantidad.ToString(),
                    item.Precio,
                    item.Importe,
                    item.IdProveedor.ToString(),
                    item.Proveedor,
                    item.Email,
                    item.Fecha,
                    item.Codigo,
                    item.Credito.ToString()
                };
                list.Add(listData);
            }
            String[] titles = { "ID", "Descripcion", "Cantidad", "Precio", "Importe", "IdProveedor",
                "Proveedor", "Email" ,"Fecha", "Codigo", "Credito"};
            var export = new ExportData(_objeto._environment, Request, list, titles, "Compras_temp.xlsx", "Compras_temp");
            return await export.ExportExcelAsync();
        }
    }
}