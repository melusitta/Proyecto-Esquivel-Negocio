using BE;
using Servicios;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // Paso 15: factura impresa que el cajero le entrega al cliente.
    // Dibuja la factura en un PrintDocument (con varias páginas si hay muchos productos).
    public class ImpresionFactura
    {
        private readonly Factura _factura;
        private int _siguienteItem;   // por dónde sigue el detalle en la próxima página

        public ImpresionFactura(Factura factura)
        {
            _factura = factura;
        }

        public void MostrarVistaPrevia(IWin32Window dueño)
        {
            using (PrintDocument documento = CrearDocumento())
            using (var vista = new PrintPreviewDialog())
            {
                vista.Document = documento;
                vista.Width = 900;
                vista.Height = 800;
                vista.Text = Texto("titulo") + " " + NroFormateado();
                vista.ShowDialog(dueño);
            }
        }

        public PrintDocument CrearDocumento()
        {
            var documento = new PrintDocument();
            documento.DocumentName = Texto("titulo") + " " + NroFormateado();
            // La vista previa y la impresión recorren el documento cada una desde el principio
            documento.BeginPrint += (s, e) => _siguienteItem = 0;
            documento.PrintPage += ImprimirPagina;
            return documento;
        }

        private string NroFormateado()
        {
            return _factura.NroFactura.ToString("D8");
        }

        private static string Texto(string clave)
        {
            return GestorIdioma.Instancia.Obtener("ImpresionFactura", clave);
        }

        private void ImprimirPagina(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle area = e.MarginBounds;
            float izq = area.Left, der = area.Right;
            float y = area.Top;

            using (var fMarca = new Font(Tema.Fuente, 20, FontStyle.Bold))
            using (var fTitulo = new Font(Tema.Fuente, 18, FontStyle.Bold))
            using (var fNormal = new Font(Tema.Fuente, 10))
            using (var fNegrita = new Font(Tema.Fuente, 10, FontStyle.Bold))
            using (var fChica = new Font(Tema.Fuente, 9))
            using (var fTotal = new Font(Tema.Fuente, 14, FontStyle.Bold))
            using (var lila = new SolidBrush(Tema.PrincipalOscuro))
            using (var lapizLila = new Pen(Tema.Principal, 1.5f))
            using (var derecha = new StringFormat { Alignment = StringAlignment.Far })
            using (var centro = new StringFormat { Alignment = StringAlignment.Center })
            {
                // ---- Encabezado: logo y datos del comercio a la izquierda, datos de la factura a la derecha
                if (Tema.Logo != null)
                    g.DrawImage(Tema.Logo, izq, y, 75, 75);
                g.DrawString("Plumy", fMarca, lila, izq + 85, y + 8);
                g.DrawString(Texto("comercio"), fChica, Brushes.Black, izq + 87, y + 45);

                g.DrawString(Texto("titulo"), fTitulo, lila, der, y, derecha);
                g.DrawString(Texto("nro") + " " + NroFormateado(), fNegrita, Brushes.Black, der, y + 34, derecha);
                g.DrawString(Texto("fecha") + " " + _factura.FechaHora.ToString("dd/MM/yyyy") + "   " +
                             Texto("hora") + " " + _factura.FechaHora.ToString("HH:mm"), fNormal, Brushes.Black, der, y + 54, derecha);
                y += 90;
                g.DrawLine(lapizLila, izq, y, der, y);
                y += 10;

                // ---- Cliente
                g.DrawString(Texto("cliente") + " " + _factura.NombreCliente, fNegrita, Brushes.Black, izq, y);
                g.DrawString(Texto("dni") + " " + _factura.DNI, fNormal, Brushes.Black, der, y, derecha);
                y += 30;

                // ---- Detalle: columnas con los números alineados a la derecha
                float colCantidad = izq + (der - izq) * 0.62f;
                float colPrecio = izq + (der - izq) * 0.81f;
                using (var fondo = new SolidBrush(Tema.Principal))
                    g.FillRectangle(fondo, izq, y, der - izq, 24);
                g.DrawString(Texto("colProducto"), fNegrita, Brushes.Black, izq + 6, y + 3);
                g.DrawString(Texto("colCantidad"), fNegrita, Brushes.Black, colCantidad, y + 3, derecha);
                g.DrawString(Texto("colPrecio"), fNegrita, Brushes.Black, colPrecio, y + 3, derecha);
                g.DrawString(Texto("colSubtotal"), fNegrita, Brushes.Black, der - 6, y + 3, derecha);
                y += 30;

                float limite = area.Bottom - 150;   // lugar reservado para el total y el pie
                while (_siguienteItem < _factura.Items.Count)
                {
                    if (y > limite)
                    {
                        g.DrawString(Texto("continua"), fChica, Brushes.Gray, der, y, derecha);
                        e.HasMorePages = true;
                        return;
                    }
                    ItemFactura item = _factura.Items[_siguienteItem];
                    g.DrawString(item.Producto.Nombre, fNormal, Brushes.Black, izq + 6, y);
                    g.DrawString(item.Cantidad.ToString(), fNormal, Brushes.Black, colCantidad, y, derecha);
                    g.DrawString("$ " + item.PrecioUnitario.ToString("N2"), fNormal, Brushes.Black, colPrecio, y, derecha);
                    g.DrawString("$ " + item.Subtotal.ToString("N2"), fNormal, Brushes.Black, der - 6, y, derecha);
                    y += 22;
                    _siguienteItem++;
                }

                // ---- Total y pago
                y += 6;
                g.DrawLine(lapizLila, izq, y, der, y);
                y += 10;
                g.DrawString(Texto("total") + "   $ " + _factura.Total.ToString("N2"), fTotal, lila, der - 6, y, derecha);
                y += 40;

                if (_factura.Estado == Factura.EstadoPagada)
                {
                    string formaPago = GestorIdioma.Instancia.Obtener("FormCobrarVenta", "fp" + _factura.FormaPago);
                    g.DrawString(Texto("formaPago") + " " + formaPago, fNormal, Brushes.Black, izq, y);
                    // Sello "PAGADA"
                    string sello = Texto("pagada");
                    SizeF tam = g.MeasureString(sello, fTitulo);
                    var caja = new RectangleF(der - tam.Width - 20, y - 6, tam.Width + 14, tam.Height + 4);
                    using (var lapizSello = new Pen(Tema.Acento, 2.5f))
                    using (var rosa = new SolidBrush(Tema.Acento))
                    {
                        g.DrawRectangle(lapizSello, caja.X, caja.Y, caja.Width, caja.Height);
                        g.DrawString(sello, fTitulo, rosa, caja.X + 7, caja.Y + 2);
                    }
                    y += 22;
                    if (_factura.FechaPago.HasValue)
                        g.DrawString(Texto("fechaPago") + " " + _factura.FechaPago.Value.ToString("dd/MM/yyyy HH:mm"), fChica, Brushes.Black, izq, y);
                    y += 20;
                }
                g.DrawString(Texto("cajero") + " " + _factura.Cajero, fChica, Brushes.Black, izq, y);

                // ---- Pie
                g.DrawString(Texto("gracias"), fNegrita, lila, (izq + der) / 2, area.Bottom - 30, centro);
                e.HasMorePages = false;
            }
        }
    }
}
