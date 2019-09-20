using Sistem_Ventas.Areas.Compras.Models;
using Sistem_Ventas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Library
{
    public class LCompras : ListObject
    {
        public LCompras(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<TCompras> getTCompras(String valor)
        {
            List<TCompras> listProductos;
            if (valor == null)
            {
                listProductos = _context.TCompras.ToList();
            }
            else
            {
                listProductos = _context.TCompras.Where(u => u.Descripcion.StartsWith(valor)).ToList();
            }
            return listProductos;
        }
    }
}
