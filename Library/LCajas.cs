using Microsoft.AspNetCore.Identity;
using Sistem_Ventas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sistem_Ventas.Library
{
    public class LCajas : ListObject
    {
        public LCajas(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public int[] GetCajas_registros(ClaimsPrincipal user)
        {
            var email = _userManager.GetUserName(user);
            int dia = DateTime.Now.Day;
            var cajas = _context.TCajas_registros.Where(u => u.Usuario.Equals(email) && u.Dia.Equals(dia)).ToList();
            if (0 < cajas.Count)
            {
                var caja = cajas.Last();
                return new int[] { caja.IdCaja, caja.Caja };
            }
            return new int[] { };
        }
        internal String UpdateEstado(int id)
        {
            String mensajeError;
            try
            {
                var cajas = _context.TCajas.Where(c => c.ID.Equals(id) && c.Asignada.Equals(false)).ToList();
                if (cajas.Count.Equals(0))
                {
                    mensajeError = "La caja no se puede desactivar";
                }
                else
                {
                    var caja = cajas.Last();
                    caja.Estado = caja.Estado ? false : true;
                    _context.Update(caja);
                    _context.SaveChanges();
                    mensajeError = "Done";
                }
            }
            catch (Exception e)
            {
                mensajeError = e.Message;
            }
            return mensajeError;
        }
    }
}
