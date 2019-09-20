using Sistem_Ventas.Areas.Compras.Models;
using Sistem_Ventas.Areas.Productos.Models;
using Sistem_Ventas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Library
{
    public class LProductos : ListObject
    {
        public LProductos(ApplicationDbContext context)
        {
            _context = context;
        }
        internal List<TCompras_temp> getTCompras_temp(string search)
        {
            List<TCompras_temp> listProductos;
            if (search == null)
            {
                listProductos = _context.TCompras_temp.ToList();
            }
            else
            {
                listProductos = _context.TCompras_temp.Where(u => u.Descripcion.StartsWith(search)).ToList();
            }
            return listProductos;
        }
        internal List<TProductos> getTProductos(string search)
        {
            List<TProductos> listProductos;
            if (search == null)
            {
                listProductos = _context.TProductos.ToList();
            }
            else
            {
                listProductos = _context.TProductos.Where(u => u.Descripcion.StartsWith(search)).ToList();
            }
            return listProductos;
        }
        internal TProductos getTProducto(int id)
        {
            return _context.TProductos.Where(p => p.ID.Equals(id)).ToList().ElementAt(0);
        }
    }
}
