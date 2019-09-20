using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sistem_Ventas.Controllers;
using Sistem_Ventas.Library;

namespace Sistem_Ventas.Areas.Principal.Controllers
{
    [Authorize]
    [Area("Principal")]
    public class PrincipalController : Controller
    {
        private LUsuarios _usuarios;
        private SignInManager<IdentityUser> _signInManager;
        public PrincipalController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _usuarios = new LUsuarios();
            
        }
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                
                return View();
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            
        }
    }
}