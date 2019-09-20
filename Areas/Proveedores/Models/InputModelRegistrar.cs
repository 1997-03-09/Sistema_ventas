using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Areas.Proveedores.Models
{
    public class InputModelRegistrar
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "<font color='red'>El campo Proveedor es obligatorio.</font>")]
        public string Proveedor { get; set; }

        [Required(ErrorMessage = "<font color='red'>El campo telefono es obligatorio.</font>")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{5})$", ErrorMessage = "<font color='red'>El formato telefono ingresado no es válido.</font>")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "<font color='red'>El campo correo electrónico es obligatorio.</font>")]
        [EmailAddress(ErrorMessage = "<font color='red'>El campo correo electrónico no es una dirección de correo electrónico válida.</font>")]
        public string Email { get; set; }

        [Required(ErrorMessage = "<font color='red'>El campo direccion es obligatorio.</font>")]
        public string Direccion { get; set; }

        //public String Creditos { get; set; }
    }
}
