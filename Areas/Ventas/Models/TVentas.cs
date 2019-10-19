using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Areas.Ventas.Models
{
    public class TVentas
    {
        public int ID { set; get; }
        public string Codigo { set; get; }
        public string Descripcion { set; get; }
        public Decimal Precio { set; get; }
        public int Cantidad { set; get; }
        public Decimal Importe { set; get; }
        public int Dia { set; get; }
        public string Mes { set; get; }
        public string Year { set; get; }
        public string Fecha { set; get; }
        public int Caja { set; get; }
        public string IdUsuario { set; get; }
    }
}
