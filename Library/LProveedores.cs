using Sistem_Ventas.Areas.Proveedores.Models;
using Sistem_Ventas.Data;
using Sistem_Ventas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Library
{
    public class LProveedores : ListObject
    {
        public LProveedores() { }

        public LProveedores(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<InputModelRegistrar> getTProveedores(String valor)
        {
            List<TProveedores> listProveedores;
            if (valor == null)
            {
                listProveedores = _context.TProveedores.ToList();
            }
            else
            {
                listProveedores = _context.TProveedores.Where(u => u.Proveedor.StartsWith(valor) || u.Email.StartsWith(valor)).ToList();
            }
            List<InputModelRegistrar> proveedoresList = new List<InputModelRegistrar>();
            listProveedores.ForEach(item =>{
                proveedoresList.Add(new TProveedores
                {
                    ID = item.ID,
                    Proveedor = item.Proveedor,
                    Email = item.Email,
                    Telefono = item.Telefono,
                    Direccion = item.Direccion,
                });
            });
            return proveedoresList;
        }
        public List<TTickets> getTickets(String valor)
        {
            List<TTickets> listTickets;
            if (valor == null)
            {
                listTickets = _context.TTickets.Where(t => t.Propietario.Equals("Proveedor")).ToList();
            }
            else
            {
                listTickets = _context.TTickets.Where(t => t.Email.StartsWith(valor) && t.Propietario.Equals("Proveedor")).ToList();
            }
            List<TTickets> lists = new List<TTickets>();
            listTickets.ForEach(item => {
                lists.Add(item);
            });
            return lists;
        }
    }
}
