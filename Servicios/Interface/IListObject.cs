using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistem_Ventas.Data;
using Sistem_Ventas.Library;
using Sistem_Ventas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Servicios.Interface
{
    public interface IListObject
    {
        UsersRoles _usersRole { get; set; }
        UserData _userData { get; set; }
        LUsuarios _usuarios { get; set; }
        LClientes _clientes { get; set; }
        LProveedores _proveedores { get; set; }
        LCompras _compras { get; set; }
        LProductos _productos { get; set; }
        LVentas _ventas { get; set; }
        LCajas _cajas { get; set; }
        Uploadimage _image { get; set; }
        IdentityError _identityError { get; set; }
        ApplicationDbContext _context { get; set; }
        IHostingEnvironment _environment { get; set; }

        List<SelectListItem> _selectList { get; set; }

        RoleManager<IdentityRole> _roleManager { get; set; }
        UserManager<IdentityUser> _userManager { get; set; }
        SignInManager<IdentityUser> _signInManager { get; set; }
    }
}
