using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Areas.Cajas.Models
{
    public class TCajas
    {
        public int ID { get; set; }
        public int Caja { get; set; }
        public bool Estado { get; set; }
        public bool Asignada { get; set; }
        public string Usuario { get; set; }
    }
}
