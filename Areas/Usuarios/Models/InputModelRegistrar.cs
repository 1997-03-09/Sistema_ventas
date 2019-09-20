using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Areas.Usuarios.Models
{
    public class InputModelRegistrar
    {
        [Required(ErrorMessage = "<font color='red'>El campo nombre es obligatorio.</font>")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "<font color='red'>El campo apellido es obligatorio.</font>")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "<font color='red'>El campo nid es obligatorio.</font>")]
        public string NID { get; set; }

        [Required(ErrorMessage = "<font color='red'>El campo telefono es obligatorio.</font>")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{5})$", ErrorMessage = "<font color='red'>El formato telefono ingresado no es válido.</font>")]
        public string Telefono { get; set; }


        [Required(ErrorMessage = "<font color='red'>El campo correo electrónico es obligatorio.</font>")]
        [EmailAddress(ErrorMessage = "<font color='red'>El campo correo electrónico no es una dirección de correo electrónico válida.</font>")]
        public string Email { get; set; }

        [Display(Name ="Contraseña")]
        [Required(ErrorMessage = "<font color='red'>El campo contraseña es obligatorio.</font>")]
        //[DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "<font color='red'>El número de caracteres de {0} debe ser al menos {2}.</font>", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
        public string ID { get; set; }
        public int Id { get; set; }

    }
}
