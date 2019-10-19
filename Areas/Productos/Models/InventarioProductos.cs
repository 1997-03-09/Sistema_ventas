using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Areas.Productos.Models
{
    public class InventarioProductos : TProductos
    {
        public int Existencia { set; get; }
    }
}
