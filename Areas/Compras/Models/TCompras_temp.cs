using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Areas.Compras.Models
{
    public class TCompras_temp
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public String Precio { get; set; }
        public String Importe { get; set; }
        public int IdProveedor { get; set; }
        public String Proveedor { get; set; }
        public String Email { get; set; }
        public String Fecha { get; set; }
        public bool Credito { get; set; }
        public String Codigo { get; set; }
    }
}
