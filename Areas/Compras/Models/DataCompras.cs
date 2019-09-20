using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Areas.Compras.Models
{
    public class DataCompras : TCompras_temp
    {
        private static IFormFile image;
        public IFormFile AvatarImage { get { return image; } set { image = value; } }
        public String Direccion { get; set; }
        public String Telefono { get; set; }
    }
}
