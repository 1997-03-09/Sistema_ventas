using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Models
{
    public class TTickets
    {
        public int ID { get; set; }
        public string Propietario { get; set; }
        public string Deuda { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaDeuda { get; set; }
        public string Pago { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaPago { get; set; }
        public string Ticket { get; set; }
        public string Email { get; set; }
    }
}
