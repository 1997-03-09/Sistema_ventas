using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Areas.Productos.Models
{
    public class InputModelRegistrar
    {
        public int ID { get; set; }
        public String Codigo { get; set; }
        [Required(ErrorMessage = "<font color='red'>El campo descripcion es obligatorio.</font>")]
        public String Descripcion { get; set; }
        [Required(ErrorMessage = "<font color='red'>El campo precio es obligatorio.</font>")]
        public String Precio { get; set; }
        [Required(ErrorMessage = "<font color='red'>El campo departamento es obligatorio.</font>")]
        public String Departamento { get; set; }
        [Required(ErrorMessage = "<font color='red'>El campo categoria es obligatorio.</font>")]
        public String Categoria { get; set; }
    }
}
