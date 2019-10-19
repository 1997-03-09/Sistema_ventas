using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Areas.Clientes.Models
{
    public class TReportes_clientes
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
        public TClientes TClientes { get; set; }

    }
}
