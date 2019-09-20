using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Models
{
    public class DataPaginador<T>
    {
        public List<T> List { get; set; }
        public string Pagi_info { get; set; }
        public string Pagi_navegacion { get; set; }
        public T Input { get; set; }
        public DataCliente dataCliente { get; set; }
        public string Search { get; set; }
    }
}
