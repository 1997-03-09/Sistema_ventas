using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sistem_Ventas.Areas.Compras.Models;
using Sistem_Ventas.Areas.Productos.Controllers;
using Sistem_Ventas.Areas.Productos.Models;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;
using Sistem_Ventas.Models;
using Sistem_Ventas.Servicios.Interface;

namespace Sistem_Ventas.Areas.Productos.Pages.Registrar
{
    public class RegistrarModel : PageModel
    {
        private static int idGet = 0;
        public static String BarcodeImage,codigoBarra;
        private IListObject _objeto;
        private static TCompras_temp dataCompras;
        private string dia = DateTime.Now.ToString("dd");
        private string mes = DateTime.Now.ToString("MMM");
        private string year = DateTime.Now.ToString("yyy");
        private string fecha = DateTime.Now.ToString("dd/MMM/yyy");
        public RegistrarModel(ApplicationDbContext context, IHostingEnvironment environment, IListObject objeto)
        {
            _objeto = objeto;
            objeto._context = context;
            objeto._environment = environment;
            objeto._image = new Uploadimage();
        }
        public void OnGet(int id)
        {
            idGet = id;
            dataCompras = _objeto._context.TCompras_temp.Where(t => t.ID.Equals(id)).ToList().ElementAt(0);
            //codigoBarra = new Codigos(_objeto._context).codigoTicket("TProductos");
            //BarcodeImage = new Codigos().GetCodeBarra(codigoBarra);
            Input = new InputModel
            {
                Descripcion = dataCompras.Descripcion,
                Compras = dataCompras,
                Image = new Codigos().GetCodeBarra(
                    new Codigos(_objeto._context).codigoTicket("TProductos")
                    )
            };
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelRegistrar
        {
            [TempData]
            public String ErrorMessage { get; set; }
            public String Precios { get; set; }
            public String Image { get; set; }
            public TCompras_temp Compras { get; set; }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (await insertAsync())
            {
                return RedirectToAction(nameof(ProductosController.Index), "Productos");
            }
            else
            {
                return Page();
            }
        }
        private async Task<bool> insertAsync()
        {
            bool valor = false;
            codigoBarra = new Codigos(_objeto._context).codigoTicket("TProductos");
            Input = new InputModel
            {
                Descripcion = Input.Descripcion,
                Precio = Input.Precio,
                Compras = dataCompras,
                Image = new Codigos().GetCodeBarra(codigoBarra),
                Departamento = Input.Departamento,
                Categoria = Input.Categoria
            };
            try
            {
                var precio1 = Convert.ToDecimal(dataCompras.Precio.Replace("$", ""));
                var precio2 = Convert.ToDecimal(Input.Precio);
                if (precio2 > precio1)
                {
                    var strategy = _objeto._context.Database.CreateExecutionStrategy();
                    await strategy.ExecuteAsync(async () => {
                        using (var transaction = _objeto._context.Database.BeginTransaction())
                        {
                            try
                            {
                                var imageByte = _objeto._image.Imagebyte(dataCompras.Codigo, _objeto._environment);
                                var precio = string.Format("${0:#,###,###,##0.00####}", Convert.ToDecimal(Input.Precio));
                                var producto = new TProductos
                                {
                                    Codigo = codigoBarra,
                                    Descripcion = Input.Descripcion,
                                    Precio = precio,
                                    Departamento = Input.Departamento,
                                    Categoria = Input.Categoria,
                                    Descuento = "%0.0",
                                    Dia = Convert.ToInt16(dia),
                                    Mes = mes,
                                    Year = year,
                                    Fecha = fecha,
                                    Compra = dataCompras.Codigo,
                                    Image = imageByte
                                };
                                _objeto._context.Add(producto);
                                await _objeto._context.SaveChangesAsync();
                                var bodega = new TBodega
                                {
                                    Codigo = codigoBarra,
                                    Existencia = dataCompras.Cantidad,
                                    Dia = Convert.ToInt16(dia),
                                    Mes = mes,
                                    Year = year,
                                    Fecha = fecha
                                };
                                _objeto._context.Add(bodega);
                                await _objeto._context.SaveChangesAsync();
                                transaction.Commit();
                                valor = true;
                               //var compra=  _objeto._context.TCompras_temp.SingleOrDefault(t => t.ID.Equals(idGet));
                               // _objeto._context.Remove(compra);
                            }
                            catch (Exception ex)
                            {

                                valor = false;
                                Input.ErrorMessage = ex.Message;
                                transaction.Rollback();
                            }
                        }
                    });
                }
                else
                {
                    Input.ErrorMessage = "El precio debe ser mayor al precio de compra";

                    valor = false;
                }
            }
            catch (Exception ex)
            {

                Input.ErrorMessage = ex.Message;
                valor = false;
            }
            return valor;
        }
    }
}