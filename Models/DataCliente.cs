using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Models
{
    public class DataCliente
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NID { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public String Creditos { get; set; }
        public string Deuda { get; set; }
        public string FechaDeuda { get; set; }
        [Required(ErrorMessage = "Ingres el pago.")]
        public string Pago { get; set; }
        public string FechaPago { get; set; }
        public string Ticket { get; set; }
        public string Message { get; set; }
        public bool Credito { get; set; }
        public string Cambio { get; set; }
    }
}
