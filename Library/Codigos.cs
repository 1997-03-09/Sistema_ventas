using Sistem_Ventas.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sistem_Ventas.Library
{
    public class Codigos
    {
        public static Random rnd = new Random();
        private ApplicationDbContext _context;
        private string year = DateTime.Now.ToString("yyy");
        public Codigos()
        {

        }
        public Codigos(ApplicationDbContext context)
        {
            _context = context;
        }
        public String codigoTicket(String tabla)
        {
            int codigo = 0, count = 0;
            String codigos = null;
            do
            {
                for (int i = 0; i <= 20; i++)
                {
                    codigo = rnd.Next(1000000, 10000001);
                }
                codigos = Convert.ToString(codigo);
                switch (tabla)
                {
                    case "TCompras":
                        count = _context.TCompras.Where(c => c.Codigo.Equals(codigos)).ToList().Count();
                        break;
                    case "TProductos":
                        count = _context.TProductos.Where(r => r.Codigo.Equals(codigos)).ToList().Count();

                        break;
                    case "Tickets":
                        count = _context.TTickets.Where(r => r.Ticket.Equals(codigos)).ToList().Count();
                       
                        break;
                }
               

            } while (count > 0);
            return codigos;
        }
        public String codigosTickets(String propietario, String email, String tabla)
        {
            String ticket = null;
            String codigo = null;
            switch (tabla)
            {

                case "TCompras":
                    var compra = _context.TCompras.Where(c => c.Email.Equals(email) && c.Year.Equals(year)).ToList();
                    if (compra.Count.Equals(0))
                    {
                        ticket = "0000000001";
                    }
                    else
                    {
                        var data = compra.Last();
                        if (data.Codigo.Equals("9999999999"))
                        {
                            ticket = "0000000001";
                        }
                        else
                        {
                            var cod = Convert.ToInt64(data.Codigo);
                            cod++;
                            var num = cod.ToString("D10");
                            ticket = num;
                        }
                    }
                    break;

                case "Tickets":
                    var report = _context.TTickets.Where(r => r.Propietario.Equals(propietario)
           && r.Email.Equals(email)).ToList();
                    if (report.Count.Equals(0))
                    {
                        ticket = "0000000001";
                    }
                    else
                    {
                        var data = report.Last();
                        if (data.Ticket.Equals("9999999999"))
                        {
                            ticket = "0000000001";
                        }
                        else
                        {
                            var cod = Convert.ToInt64(data.Ticket);
                            cod++;
                            var num = cod.ToString("D10");
                           
                            ticket = num;
                        }
                    }
                    break;
            }
           
            return ticket;
        }
        public String GetCodeBarra(string barcode)
        {
            String BarcodeImage;
            
            using (MemoryStream ms = new MemoryStream())
            {
                // La imagen se dibuja basada en el tamaño del texto
                using (Bitmap bitMap = new Bitmap(barcode.Length * 20, 80))
                {
                    // El objeto Graphics se genera para la imagen
                    using (Graphics graphics = Graphics.FromImage(bitMap))
                    {
                        // Usamos la fuente Barcode
                        Font oFont = new Font("IDAutomationHC39M", 16);
                        PointF point = new PointF(2f, 2f);
                        // Un objeto White Brush se utiliza para rellenar la imagen con el color blanco
                        SolidBrush whiteBrush = new SolidBrush(Color.White);
                        graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                        // Un objeto Black Brush se utiliza para dibujar el código de barras
                        SolidBrush blackBrush = new SolidBrush(Color.Black);
                        graphics.DrawString(barcode, oFont, blackBrush, point);

                    }
                    // El mapa de bits se guarda en la memoria de memoria.
                    bitMap.Save(ms, ImageFormat.Png);
                    // La imagen se convierte a una cadena Base64
                    BarcodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
            return BarcodeImage;
        }
        public String codigoBarra(String descripcion, String precio)
        {
            int codigo = 0, count = 0;
            String codigos = null;
            var producto = _context.TProductos.Where(r => r.Descripcion.Equals(descripcion)
            && r.Precio.Equals("$" + precio)).ToList();
            if (producto.Count > 0)
            {
                codigos = producto[0].Codigo;
            }
            else
            {
                do
                {
                    for (int i = 0; i <= 20; i++)
                    {
                        codigo = rnd.Next(1000000, 10000001);
                    }
                    codigos = Convert.ToString(codigo);
                    count = _context.TProductos.Where(r => r.Codigo.Equals(codigos)).ToList().Count();


                } while (count > 0);
            }
            return codigos;
        }
    }
    
}
