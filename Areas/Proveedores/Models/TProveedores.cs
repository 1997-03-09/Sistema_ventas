using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Areas.Proveedores.Models
{
    public class TProveedores : InputModelRegistrar
    {
        public ICollection<TReportes_proveedores> TReportes_proveedores { get; set; }
    }
}
