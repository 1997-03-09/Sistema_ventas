using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sistem_Ventas.Areas.Clientes.Models;
using Sistem_Ventas.Controllers;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;
using Sistem_Ventas.Models;
using Sistem_Ventas.Servicios.Interface;

namespace Sistem_Ventas.Areas.Clientes.Controllers
{
    [Authorize]
    [Area("Clientes")]
    public class ClientesController : Controller
    {
        private IListObject _objeto;
        private static List<InputModelRegistrar> listClientes;
        public ClientesController(SignInManager<IdentityUser> signInManager, ApplicationDbContext context, IListObject objeto, IHostingEnvironment hostingEnvironment)
        {
            _objeto = objeto;
            objeto._signInManager = signInManager;
            objeto._environment = hostingEnvironment;
            objeto._clientes = new LClientes(context);
           
        }
        public IActionResult Index(int id, String Search)
        {
            if (_objeto._signInManager.IsSignedIn(User))
            {
                var url = Request.Scheme + "://" + Request.Host.Value;
                var objects = new Paginador<InputModelRegistrar>().paginador(_objeto._clientes.getTClientes(Search)
                   , id, "Clientes", "Clientes", "Index", url);
                var models = new DataPaginador<InputModelRegistrar>
                {
                    List = (List<InputModelRegistrar>)objects[2],
                    Pagi_info = (String)objects[0],
                    Pagi_navegacion = (String)objects[1],
                    Input = new InputModelRegistrar()
                };
                listClientes = models.List;
                return View(models);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
           
        }
        //[HttpPost]
        public async Task<IActionResult> Export()
        {
            var list = new List<String[]>();
            foreach (var item in listClientes)
            {
                String[] listData ={
                    Convert.ToString(item.ID),
                    item.NID,
                    item.Nombre,
                    item.Apellido,
                    item.Email,
                    item.Telefono,
                    item.Direccion,
                    item.Creditos
                };
                list.Add(listData);
            }
            String[] titles = { "ID", "NID", "Nombre", "Apellido", "Email", "Telefono", "Direccion","Creditos" };
            var export = new ExportData(_objeto._environment, Request, list, titles, "Clientes.xlsx", "Clientes");
            return await export.ExportExcelAsync();
        }
    }
}