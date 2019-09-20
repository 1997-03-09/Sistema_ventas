using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sistem_Ventas.Areas.Ventas.Models;
using Sistem_Ventas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sistem_Ventas.Library
{
    public class LVentas : ListObject
    {
        public LVentas(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task GetProductoAsync(String codigo, ClaimsPrincipal user)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async ()=> {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var bodega = _context.TBodega.Where(b => b.Codigo.Equals(codigo)).ToList();
                    if (!bodega.Count.Equals(0))
                    {
                        try
                        {
                            int dia = DateTime.Now.Day;
                            string mes = DateTime.Now.ToString("MMM");
                            var email = _userManager.GetUserName(user);
                            var producto = _context.TProductos.Where(b => b.Codigo.Equals(codigo)).ToList();
                            var cajas = _context.TCajas_registros.Where(u => u.Usuario.Equals(email) 
                            && u.Dia.Equals(dia) && u.Mes.Equals(mes)).ToList().ElementAt(0);

                            var d = bodega.ElementAt(0);
                            var p = producto.ElementAt(0);

                            var productTempo = _context.TTempo.Where(b => b.Codigo.Equals(codigo)).ToList();
                            if (productTempo.Count.Equals(0))
                            {
                                var tempoVenta = new TTempo_ventas
                                {
                                    Descripcion = p.Descripcion,
                                    Cantidad = 1,
                                    Precio = Convert.ToDecimal(p.Precio.Replace("$", "").Replace(",", "")),
                                    Importe = Convert.ToDecimal(p.Precio.Replace("$", "").Replace(",", "")),
                                    Codigo = codigo,
                                    Email = email,
                                    Caja = cajas.Caja
                                };
                                _context.Add(tempoVenta);
                                _context.SaveChanges();
                            }
                            else
                            {
                                int cant = 0;
                                productTempo.ForEach(t =>
                                {
                                    cant += t.Cantidad;
                                });
                                cant += 1;
                                var data = productTempo.ElementAt(0);
                                var precio = Convert.ToDecimal(p.Precio.Replace("$", "").Replace(",", ""));
                                var importe = cant * precio;
                                var tempoVenta = new TTempo_ventas
                                {
                                    ID = data.IdTempo,
                                    Descripcion = p.Descripcion,
                                    Cantidad = cant,
                                    Precio = precio,
                                    Importe = importe,
                                    Codigo = codigo,
                                    Email = email,
                                    Caja = cajas.Caja
                                };
                                _context.Update(tempoVenta);
                                _context.SaveChanges();
                            }
                            var ventaTempo = _context.TTempo_ventas.Where(b => b.Codigo.Equals(codigo)).ToList();
                            var tempo = new TTempo
                            {
                                Cantidad = 1,
                                Codigo = codigo,
                                IdTempo = ventaTempo[0].ID
                            };
                            _context.Add(tempo);
                            _context.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception)
                        {

                            transaction.Rollback();
                        }
                    }
                }
            });
        }
    }
}
