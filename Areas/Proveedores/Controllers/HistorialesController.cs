using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sistem_Ventas.Controllers;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;
using Sistem_Ventas.Models;
using Sistem_Ventas.Servicios.Interface;

namespace Sistem_Ventas.Areas.Proveedores.Controllers
{
    [Authorize]
    [Area("Proveedores")]
    public class HistorialesController : Controller
    {
        private IListObject _objeto;
        private static List<TTickets> listTicket;
        public HistorialesController(SignInManager<IdentityUser> signInManager, ApplicationDbContext context, IListObject objeto, IHostingEnvironment hostingEnvironment)
        {
            _objeto = objeto;
            objeto._signInManager = signInManager;
            objeto._environment = hostingEnvironment;
            objeto._proveedores = new LProveedores(context);
        }
        public IActionResult HistoryTickets(int id, String Search)
        {
            if (_objeto._signInManager.IsSignedIn(User))
            {
                Object[] objects = new Object[3];
                var url = Request.Scheme + "://" + Request.Host.Value;
                var lists = _objeto._proveedores.getTickets(Search);
                if (0 < lists.Count)
                {
                    objects = new Paginador<TTickets>().paginador(lists, id, "Proveedores", "Historiales", "HistoryTickets", url);
                }
                else
                {
                    objects[0] = "No data";
                    objects[1] = "No data";
                    objects[2] = new List<TTickets>();
                }

                var models = new DataPaginador<TTickets>
                {
                    List = (List<TTickets>)objects[2],
                    Pagi_info = (String)objects[0],
                    Pagi_navegacion = (String)objects[1],
                    Input = new TTickets()
                };
                listTicket = models.List;
                return View(models);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
                
        }
        [HttpPost]
        public async Task<IActionResult> ExportHistory()
        {
            var list = new List<String[]>();
            foreach (var item in listTicket)
            {
                String[] listData ={
                    Convert.ToString(item.ID),
                    item.Deuda,
                    Convert.ToString(item.FechaDeuda),
                    item.Pago,
                    Convert.ToString(item.FechaPago),
                    item.Ticket,
                    item.Email
                };
                list.Add(listData);
            }
            String[] titles = { "ID", "Deuda", "Fecha Deuda", "Pago", "Fecha Pago", "Ticket", "Email" };
            var export = new ExportData(_objeto._environment, Request, list, titles, "TicketProveedores.xlsx", "Tickets");
            return await export.ExportExcelAsync();
        }
    }
}