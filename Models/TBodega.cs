using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Models
{
    public class TBodega
    {
        public int ID { set; get; }
        public string Codigo { set; get; }
        public int Existencia { set; get; }
        public int Dia { set; get; }
        public string Mes { set; get; }
        public string Year { set; get; }
        public string Fecha { set; get; }
    }
}
