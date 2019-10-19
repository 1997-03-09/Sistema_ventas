using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sistem_Ventas.Areas.Productos.Models;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;
using Sistem_Ventas.Servicios.Interface;

namespace Sistem_Ventas.Areas.Productos.Pages.Inventario
{
    public class DetallesModel : PageModel
    {
        private IListObject _objeto;
        private static TProductos _producto;
        private static int _idGet;
        public DetallesModel(IListObject objeto, ApplicationDbContext context)
        {
            _objeto = objeto;
            objeto._context = context;
            objeto._image = new Uploadimage();
            objeto._productos = new LProductos(context);
        }
        public void OnGet(int id)
        {
            _idGet = id;
            _producto = _objeto._productos.getTProducto(id);
            inputModel(null);
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InventarioProductos
        {
            [TempData]
            public string ErrorMessage { get; set; }
            public string CodeImage { get; set; }
            public IFormFile AvatarImage { get; set; }
        }
        private void inputModel(Exception ex)
        {
            Input = new InputModel
            {
                Image = _producto.Image,
                Descripcion = _producto.Descripcion,
                Precio = _producto.Precio.Replace("$", "").Replace(",", ""),
                Descuento = _producto.Descuento.Replace("%", ""),
                Categoria = _producto.Categoria,
                Departamento = _producto.Departamento,
                Fecha = _producto.Fecha,
                CodeImage = new Codigos().GetCodeBarra(_producto.Codigo),
                ErrorMessage = ex != null ? ex.Message : ""
            };
        }
        public IActionResult OnPost()
        {
            var valor = false;
            if (ModelState.IsValid)
            {
                try
                {
                    byte[] image = null;
                    if (Input.AvatarImage != null)
                    {
                        var images = _objeto._image.ByteAvatarImageAsync(Input.AvatarImage);
                        image = images.Result;
                    }
                    else
                    {
                        image = _producto.Image;
                    }
                    var precio = string.Format("${0:#,###,###,##0.00####}", Convert.ToDecimal(Input.Precio));
                    var descuento = string.Format("%{0:#,###,###,##0.00####}", Convert.ToDecimal(Input.Descuento));
                    var producto = new TProductos
                    {
                        ID = _idGet,
                        Codigo = _producto.Codigo,
                        Descripcion = Input.Descripcion,
                        Precio = precio,
                        Descuento = descuento,
                        Categoria = Input.Categoria,
                        Departamento = Input.Departamento,
                        Dia = _producto.Dia,
                        Mes = _producto.Mes,
                        Year = _producto.Year,
                        Fecha = _producto.Fecha,
                        Compra = _producto.Compra,
                        Image = image,
                    };
                    _objeto._context.Update(producto);
                    _objeto._context.SaveChanges();
                    valor = true;
                }
                catch (Exception ex)
                {

                    inputModel(ex);
                    valor = false;
                }
            }
            else
            {
                inputModel(null);
                valor = false;
            }
            if (valor)
            {
                return RedirectToPage("/Inventario/Inventario");
            }
            else
            {
                return Page();
            }
        }
    }
}