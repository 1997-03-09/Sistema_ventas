using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sistem_Ventas.Areas.Compras.Models;
using Sistem_Ventas.Areas.Proveedores.Models;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;
using Sistem_Ventas.Models;
using Sistem_Ventas.Servicios.Interface;

namespace Sistem_Ventas.Areas.Compras.Pages.Compras
{
    public class ComprasModel : PageModel
    {
        private IListObject _objeto;
        private static InputModelRegistrar inputModel;
        private string fecha = DateTime.Now.ToString("dd/MMM/yyy");
        private static DataPaginador<InputModelRegistrar> dataPaginador;
        public ComprasModel(IListObject objeto, ApplicationDbContext context, IHostingEnvironment environment)
        {
            _objeto = objeto;
            objeto._context = context;
            objeto._environment = environment;
            objeto._image = new Uploadimage();
            objeto._proveedores = new LProveedores(context);
        }
        public void OnGet(int id, String Search)
        {
            var proveedor = "";
            dataPaginador = getProveedores(id, Search);
            if (Search != null && 0 < dataPaginador.List.Count)
            {
                inputModel = dataPaginador.List.Last();
                proveedor = inputModel.Proveedor;
            }
            Input = new InputModel
            {
                model = dataPaginador,
                Proveedor = proveedor
            };
        }
        public string Search { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelProductos
        {
            [TempData]
            public string ErrorMessage { get; set; }
            public bool Credito { get; set; }
            public IFormFile AvatarImage { get; set; }
            public DataPaginador<InputModelRegistrar> model { get; set; }
        }
        private DataPaginador<InputModelRegistrar> getProveedores(int id, String Search)
        {
            Object[] objects = new Object[3];
            var url = Request.Scheme + "://" + Request.Host.Value;
            var data = _objeto._proveedores.getTProveedores(Search);
            if (0 < data.Count)
            {
                objects = new Paginador<InputModelRegistrar>().paginador(data, id, "Compras", "Compras", "Compras/Compras", url);
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
            return models;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (inputModel != null)
            {
                decimal Precio = Convert.ToDecimal(Input.Precio);
                decimal Importe = Precio * Input.Cantidad;
                var codigo = new Codigos(_objeto._context).codigosTickets(null, inputModel.Email, "TCompras");
                var compra = new DataCompras
                {
                    Descripcion = Input.Descripcion,
                    Cantidad = Input.Cantidad,
                    Precio = string.Format("${0:#,###,###,##0.00####}", Precio),
                    Importe = string.Format("${0:#,###,###,##0.00####}", Importe),
                    IdProveedor = inputModel.ID,
                    Proveedor = inputModel.Proveedor,
                    Email = inputModel.Email,
                    Direccion = inputModel.Direccion,
                    Telefono = inputModel.Telefono,
                    Fecha = fecha,
                    Credito = Input.Credito,
                    Codigo = codigo,
                    AvatarImage = Input.AvatarImage
                };
                await _objeto._image.copiarImagenAsync(Input.AvatarImage, codigo + ".png", _objeto._environment, "Compras", null);
                return RedirectToPage("Detalles", compra);
                //_objeto._context.Add(compra);
                //await _objeto._context.SaveChangesAsync();
            }
            else
            {
                Input = new InputModel
                {
                    ErrorMessage = "Seleccione un proveedor",
                    model = dataPaginador,
                };
                return Page();
            }
            
        }
    }
}