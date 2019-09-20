using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Models
{
    public class LoginViewModels
    {
        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "<font color='red'>El campo correo electrónico es obligatorio.</font>")]
            [EmailAddress(ErrorMessage = "<font color='red'>El correo electrónico no es una dirección de correo electrónico válida.</font>")]
            public string Email { get; set; }

            [Required(ErrorMessage = "<font color='red'>El campo contraseña es obligatorio.</font>")]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "<font color='red'>El número de caracteres del {0} debe ser al menos {2}.</font>", MinimumLength = 6)]
            public string Password { get; set; }
        }
    }
}
