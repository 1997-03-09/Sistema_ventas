using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Areas.Proveedores.Models
{
    public class TReportes_proveedores
    {
        [Key]
        public int ReportesID { get; set; }
        public string Deuda { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaDeuda { get; set; }
        public string Pago { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaPago { get; set; }
        public string Ticket { get; set; }
        public TProveedores TProveedores { get; set; }
    }
}
