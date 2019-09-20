using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sistem_Ventas.Controllers
{
    //[Route("/Error")]
    public class ErrorController : Controller
    {
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode.Value == 404 || statusCode.Value == 500)
                {
                    ViewData["Error"] = statusCode.ToString();
                }
            }
            return View();
        }
    }
}