using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Areas.Productos.Models
{
    public class TProductos : InputModelRegistrar
    {
       
        public String Descuento { get; set; }
        public int Dia { get; set; }
        public String Mes { get; set; }
        public String Year { get; set; }
        public String Fecha { get; set; }
        public String Compra { get; set; }
        public Byte[] Image { get; set; }
    }
}
