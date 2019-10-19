using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Models
{
    public class TCajas_registros
    {
        public int ID { set; get; }
        public string IdUsuario { set; get; }
        public string Nombre { set; get; }
        public string Apellido { set; get; }
        public string Usuario { set; get; }
        public string Role { set; get; }
        public int IdCaja { set; get; }
        public int Caja { set; get; }
        public bool Estado { set; get; }
        public string Hora { set; get; }
        public int Dia { set; get; }
        public string Mes { set; get; }
        public string Year { set; get; }
        public string Fecha { set; get; }
    }
}
