using Microsoft.AspNetCore.Mvc.Rendering;
using Sistem_Ventas.Areas.Clientes.Models;
using Sistem_Ventas.Data;
using Sistem_Ventas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Library
{
    public class LClientes : ListObject
    {
        public LClientes(){}
        public LClientes(ApplicationDbContext context)
        {
            _context = context;
            _selectList = new List<SelectListItem>();
        }
        public List<SelectListItem> getCreditos()
        {
            foreach (var item in _context.TCreditos.ToList())
            {
                _selectList.Add(new SelectListItem {
                    Value = item.ID.ToString(),
                    Text = item.Creditos
                });
            }
            return _selectList;
        }
        public List<InputModelRegistrar> getTClientes(String valor)
        {
            List<TClientes> listClientes;
            if (valor == null)
            {
                listClientes = _context.TClientes.ToList();
            }
            else
            {
                listClientes = _context.TClientes.Where(u => u.NID.StartsWith(valor) || u.Nombre.StartsWith(valor) || u.Apellido.StartsWith(valor) || u.Email.StartsWith(valor)).ToList();
            }
            List<InputModelRegistrar> clientesList = new List<InputModelRegistrar>();
            listClientes.ForEach(item => {
                clientesList.Add(new TClientes {
                    ID = item.ID,
                    NID = item.NID,
                    Nombre = item.Nombre,
                    Apellido = item.Apellido,
                    Email = item.Email,
                    Telefono = item.Telefono,
                    Direccion = item.Direccion,
                    Creditos = item.Creditos,
                });
            });
            return clientesList;
        }
        public List<TTickets> getTickets(String valor)
        {
            List<TTickets> listTickets;
            if (valor == null)
            {
                listTickets = _context.TTickets.Where(t =>t.Propietario.Equals("Cliente")).ToList();
            }
            else
            {
                listTickets = _context.TTickets.Where(t => t.Email.StartsWith(valor) && t.Propietario.Equals("Cliente")).ToList();
            }
            List<TTickets> lists = new List<TTickets>();
            listTickets.ForEach(item => {
                lists.Add(new TTickets
                {
                    ID = item.ID,
                    Deuda = item.Deuda,
                    FechaDeuda = item.FechaDeuda,
                    Pago = item.Pago,
                    FechaPago = item.FechaPago,
                    Ticket = item.Ticket,
                    Email = item.Email
                });
            });
            return lists;
        }
    }
}
