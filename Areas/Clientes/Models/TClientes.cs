using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Areas.Clientes.Models
{
    public class TClientes : InputModelRegistrar
    {
        
        public ICollection<TReportes_clientes> TReportes_clientes { get; set; }
    }
}
