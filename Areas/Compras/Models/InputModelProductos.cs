using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Areas.Compras.Models
{
    public class InputModelProductos
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "<font color='red'>El campo descripcion es obligatorio.</font>")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "<font color='red'>El campo cantidad es obligatorio.</font>")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "<font color='red'>La cantidad no es correcta.</font>")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "<font color='red'>El campo precio es obligatorio.</font>")]
        [RegularExpression(@"^[0-9]+([.][0-9]+)?$", ErrorMessage = "<font color='red'>El precio no es correcto.</font>")]
        public String Precio { get; set; }
        public String Importe { get; set; }
        public int IdProveedor { get; set; }
        public String Proveedor { get; set; }
        public String Email { get; set; }
        public String IdUsuario { get; set; }
        public String Usuario { get; set; }
        public String Role { get; set; }
        public int Dia { get; set; }
        public String Mes { get; set; }
        public String Year { get; set; }
        public String Fecha { get; set; }
        public String Codigo { get; set; }
        public bool Credito { get; set; }
    }
}
