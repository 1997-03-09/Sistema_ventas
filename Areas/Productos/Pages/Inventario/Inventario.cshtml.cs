using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sistem_Ventas.Areas.Productos.Models;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;
using Sistem_Ventas.Models;
using Sistem_Ventas.Servicios.Interface;

namespace Sistem_Ventas.Areas.Productos.Pages.Inventario
{
    public class InventarioModel : PageModel
    {
        private IListObject _objeto;
        private static List<TProductos> listProductos;
        private static string _search;
        private static int _id;
        public InventarioModel(IListObject objeto, ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _objeto = objeto;
            objeto._context = context;
            objeto._environment = hostingEnvironment;
            objeto._productos = new LProductos(context);
        }
        public void OnGet(int id,string Search)
        {
            _id = id;
            _search = Search;
            Input = new InputModel
            {
                model = getProductos(id, Search),

            };
        }
        public string Search { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [TempData]
            public string ErrorMessage { get; set; }
            public DataPaginador<TProductos> model { get; set; }
        }
        private DataPaginador<TProductos> getProductos(int id, String Search)
        {
            Object[] objects = new Object[3];
            var url = Request.Scheme + "://" + Request.Host.Value;
            var data = _objeto._productos.getTProductos(Search);
            if (0 < data.Count)
            {
                objects = new Paginador<TProductos>().paginador(data, id, "Productos", "Productos", "Inventario/Inventario", url);
            }
            else
            {
                objects[0] = "No data";
                objects[1] = "No data";
                objects[2] = new List<TProductos>();
            }
            var models = new DataPaginador<TProductos>
            {
                List = (List<TProductos>)objects[2],
                Pagi_info = (String)objects[0],
                Pagi_navegacion = (String)objects[1],
                Input = new TProductos()
            };
            listProductos = models.List;
            return models;
        }
        public void OnPost()
        {
            Input = new InputModel
            {
                model = getProductos(_id, _search),

            };
            _ = ExportProducto();
        }
        public async Task<IActionResult> ExportProducto()
        {
            var list = new List<String[]>();
            foreach (var item in listProductos)
            {
                String[] listData ={
                    item.ID.ToString(),
                    item.Codigo,
                    item.Descripcion,
                    item.Precio,
                    item.Departamento,
                    item.Categoria,
                    item.Descuento,
                    Convert.ToString(item.Dia),
                    item.Mes,
                    item.Year,
                    item.Fecha,
                    item.Compra
                };
                list.Add(listData);
            }
            String[] titles = { "ID", "Codigo", "Descripcion", "Precio", "Departamento", "Categoria",
                "Descuento", "Dia", "Mes", "Year" ,"Fecha", "Compra"};
            var export = new ExportData(_objeto._environment, Request, list, titles, "Productos.xlsx", "Productos");
            return await export.ExportExcelAsync();
        }
    }
}