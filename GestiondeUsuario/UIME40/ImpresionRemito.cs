using BE;
using BLL;
using Servicios;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // Remito de la mercadería recibida en el depósito (PN2 paso 11), con el formato de un remito en papel:
    // encabezado con el comercio, la letra R y el número; datos del proveedor y de la orden; la tabla de
    // cantidad, descripción, estado y ubicación; las observaciones (discrepancias) y la firma de "Recibí conforme".
    // Lo imprimen Registrar productos recibidos y el reporte de recibos de mercadería.
    public class ImpresionRemito
    {
        private const float AltoFila = 26;

        private readonly OrdenCompra _orden;
        private readonly Recepcion _recepcion;
        private readonly FacturaProveedor _factura;
        private int _siguiente, _pagina;

        private ImpresionRemito(OrdenCompra orden, Recepcion recepcion)
        {
            _orden = orden;
            _recepcion = recepcion;
            _factura = PagoProveedorBLL.Instancia.ObtenerFacturaDeOrden(orden.Id);
        }

        private static string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("ImpresionRemito", clave);
        }

        // Número del remito: punto de venta 0001 y el número de la recepción
        public static string Numero(Recepcion r)
        {
            return "0001-" + r.Id.ToString("D8");
        }

        // Vista previa para imprimir; sin impresora se guarda como PDF en Descargas
        public static void MostrarVistaPrevia(IWin32Window dueño, OrdenCompra orden, Recepcion recepcion)
        {
            var remito = new ImpresionRemito(orden, recepcion);
            string titulo = T("titulo") + " " + Numero(recepcion);
            using (PrintDocument documento = remito.CrearDocumento())
                Impresion.MostrarOGuardarPdf(documento, dueño, titulo,
                    ImpresionCompras.NombreArchivo("archivoRemito", Numero(recepcion), orden.Proveedor.RazonSocial));
        }

        public PrintDocument CrearDocumento()
        {
            var documento = new PrintDocument { DocumentName = T("titulo") + " " + Numero(_recepcion) };
            documento.BeginPrint += (s, e) => { _siguiente = 0; _pagina = 0; };
            documento.PrintPage += ImprimirPagina;
            return documento;
        }

        private void ImprimirPagina(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle area = e.MarginBounds;
            float izq = area.Left, der = area.Right, ancho = der - izq, arriba = area.Top;
            _pagina++;

            using (var lila = new SolidBrush(Tema.PrincipalOscuro))
            using (var lapiz = new Pen(Tema.PrincipalOscuro, 1.5f))
            using (var lapizFino = new Pen(Tema.Borde, 1f))
            using (var punteado = new Pen(Color.Gray, 0.8f) { DashStyle = DashStyle.Dot })
            using (var fMarca = new Font(Tema.Fuente, 18, FontStyle.Bold))
            using (var fTitulo = new Font(Tema.Fuente, 16, FontStyle.Bold))
            using (var fLetra = new Font(Tema.Fuente, 22, FontStyle.Bold))
            using (var fNegrita = new Font(Tema.Fuente, 9.5f, FontStyle.Bold))
            using (var fNormal = new Font(Tema.Fuente, 9.5f))
            using (var fChica = new Font(Tema.Fuente, 7.5f))
            using (var centro = new StringFormat { Alignment = StringAlignment.Center })
            using (var derecha = new StringFormat { Alignment = StringAlignment.Far })
            {
                // ---- Marco redondeado de todo el remito
                using (GraphicsPath marco = Tema.RectanguloRedondeado(new RectangleF(izq, arriba, ancho, area.Height), 14))
                    g.DrawPath(lapiz, marco);

                // ---- Encabezado: comercio | letra R | remito, número y fecha
                float altoEncabezado = 118, medio = izq + ancho / 2;
                if (Tema.Logo != null)
                    g.DrawImage(Tema.Logo, izq + 14, arriba + 14, 62, 62);
                g.DrawString("Plumy", fMarca, lila, izq + 84, arriba + 18);
                g.DrawString(GestorIdioma.Instancia.Obtener("ImpresionFactura", "comercio"), fChica, Brushes.Black, izq + 86, arriba + 52);
                g.DrawString(T("rubro"), fChica, Brushes.Black, izq + 14, arriba + 86);

                var caja = new RectangleF(medio - 24, arriba, 48, 54);
                g.FillRectangle(Brushes.White, caja);
                g.DrawRectangle(lapiz, caja.X, caja.Y, caja.Width, caja.Height);
                g.DrawString("R", fLetra, lila, new RectangleF(caja.X, caja.Y + 2, caja.Width, 34), centro);
                g.DrawString(T("codigo"), fChica, Brushes.Black, new RectangleF(caja.X, caja.Y + 38, caja.Width, 14), centro);
                g.DrawLine(lapiz, medio, caja.Bottom, medio, arriba + altoEncabezado);

                float xd = medio + 40;
                g.DrawString(T("titulo").ToUpper(), fTitulo, lila, xd, arriba + 12);
                g.DrawString(T("noValido"), fChica, Brushes.Black, xd, arriba + 40);
                g.DrawString(T("nro") + " " + Numero(_recepcion), fNegrita, Brushes.Black, xd, arriba + 60);
                g.DrawString(T("fecha") + " " + _recepcion.Fecha.ToString("dd/MM/yyyy"), fNormal, Brushes.Black, xd, arriba + 82);
                if (_pagina > 1)
                    g.DrawString(string.Format(T("pagina"), _pagina), fChica, Brushes.Gray, der - 12, arriba + 100, derecha);
                float y = arriba + altoEncabezado;
                g.DrawLine(lapiz, izq, y, der, y);

                // ---- Proveedor y orden (renglones con línea punteada, como un remito en papel)
                y += 10;
                Renglon(g, T("senores"), _orden.Proveedor.RazonSocial, izq + 14, y, medio + 120 - izq - 14, fNegrita, fNormal, punteado);
                Renglon(g, T("cuit"), _orden.Proveedor.CUIT, medio + 130, y, der - medio - 144, fNegrita, fNormal, punteado);
                y += 26;
                Renglon(g, T("contacto"), _orden.Proveedor.Contacto + "  ·  " + _orden.Proveedor.Telefono + "  ·  " + _orden.Proveedor.Email,
                    izq + 14, y, ancho - 28, fNegrita, fNormal, punteado);
                y += 26;
                Renglon(g, T("orden"), _orden.NroOrden.ToString("D6"), izq + 14, y, ancho / 3, fNegrita, fNormal, punteado);
                Renglon(g, T("factura"), _factura?.NroFactura ?? "-", izq + 14 + ancho / 3 + 10, y, ancho / 3 - 10, fNegrita, fNormal, punteado);
                Renglon(g, T("recibido"), _recepcion.Fecha.ToString("dd/MM/yyyy HH:mm"), izq + 14 + 2 * ancho / 3 + 10, y, ancho / 3 - 34, fNegrita, fNormal, punteado);
                y += 32;
                g.DrawLine(lapiz, izq, y, der, y);

                // ---- Tabla: cantidad | descripción | estado | ubicación
                float[] bordes = { izq, izq + 90, der - 230, der - 120, der };
                string[] titulos = { T("colCantidad"), T("colDescripcion"), T("colEstado"), T("colUbicacion") };
                using (var fondo = new SolidBrush(Tema.PrincipalSuave))
                    g.FillRectangle(fondo, izq + 1, y + 1, ancho - 2, AltoFila - 2);
                for (int i = 0; i < titulos.Length; i++)
                    g.DrawString(titulos[i], fNegrita, lila, new RectangleF(bordes[i], y + 6, bordes[i + 1] - bordes[i], AltoFila), centro);
                float inicioTabla = y;
                y += AltoFila;
                g.DrawLine(lapiz, izq, y, der, y);

                float finTabla = area.Bottom - 170;
                int filas = (int)((finTabla - y) / AltoFila);
                for (int f = 0; f < filas; f++)
                {
                    float yf = y + f * AltoFila;
                    if (_siguiente < _recepcion.Items.Count)
                    {
                        ItemRecepcion item = _recepcion.Items[_siguiente++];
                        int pedido = _orden.Items.Find(i => i.Producto.Id == item.Producto.Id)?.Cantidad ?? item.CantidadRecibida;
                        string descripcion = item.Producto.CodigoProducto + " - " + item.Producto.Nombre + " (" + item.Producto.Marca + ")"
                            + (pedido != item.CantidadRecibida ? "  " + string.Format(T("dePedido"), pedido) : "");
                        Celda(g, item.CantidadRecibida.ToString(), fNormal, bordes[0], bordes[1], yf, centro);
                        Celda(g, descripcion, fNormal, bordes[1] + 6, bordes[2] - 6, yf, null);
                        Celda(g, TextosCompras.Estado(item.Estado), fNormal, bordes[2], bordes[3], yf, centro);
                        Celda(g, item.Ubicacion, fNormal, bordes[3] + 6, bordes[4] - 6, yf, null);
                    }
                    g.DrawLine(lapizFino, izq, yf + AltoFila, der, yf + AltoFila);
                }
                float finFilas = y + filas * AltoFila;
                for (int i = 1; i < bordes.Length - 1; i++)
                    g.DrawLine(lapiz, bordes[i], inicioTabla, bordes[i], finFilas);
                g.DrawLine(lapiz, izq, finFilas, der, finFilas);

                bool hayMas = _siguiente < _recepcion.Items.Count;
                if (hayMas)
                {
                    g.DrawString(T("continua"), fChica, Brushes.Gray, der - 12, finFilas + 6, derecha);
                    e.HasMorePages = true;
                    return;
                }

                // ---- Pie: observaciones | recibí conforme
                float yPie = finFilas;
                g.DrawLine(lapiz, medio + 40, yPie, medio + 40, area.Bottom);
                g.DrawString(T("observaciones"), fNegrita, lila, izq + 14, yPie + 10);
                string obs = string.IsNullOrEmpty(_recepcion.Comentario) ? T("sinObservaciones") : _recepcion.Comentario;
                g.DrawString(obs, fNormal, Brushes.Black, new RectangleF(izq + 14, yPie + 30, medio + 40 - izq - 28, area.Bottom - yPie - 60));
                g.DrawString(string.Format(T("totalUnidades"), _recepcion.Items.Sum(i => i.CantidadRecibida)), fNegrita, Brushes.Black, izq + 14, area.Bottom - 26);

                float xFirma = medio + 60, anchoFirma = der - xFirma - 20, yFirma = area.Bottom - 60;
                g.DrawLine(lapiz, xFirma, yFirma, xFirma + anchoFirma, yFirma);
                g.DrawString(T("recibiConforme"), fNegrita, lila, new RectangleF(xFirma, yFirma + 6, anchoFirma, 18), centro);
                g.DrawString(_recepcion.Responsable, fChica, Brushes.Black, new RectangleF(xFirma, yFirma + 24, anchoFirma, 16), centro);
                e.HasMorePages = false;
            }
        }

        // Etiqueta en negrita, el valor y la línea punteada debajo
        private static void Renglon(Graphics g, string etiqueta, string valor, float x, float y, float ancho, Font negrita, Font normal, Pen punteado)
        {
            float anchoEtiqueta = g.MeasureString(etiqueta, negrita).Width;
            g.DrawString(etiqueta, negrita, Brushes.Black, x, y);
            using (var formato = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap })
                g.DrawString(valor ?? "", normal, Brushes.Black, new RectangleF(x + anchoEtiqueta, y, ancho - anchoEtiqueta, 18), formato);
            g.DrawLine(punteado, x + anchoEtiqueta, y + 17, x + ancho, y + 17);
        }

        private static void Celda(Graphics g, string texto, Font fuente, float desde, float hasta, float y, StringFormat alineacion)
        {
            using (var formato = new StringFormat(alineacion ?? StringFormat.GenericDefault) { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap })
                g.DrawString(texto ?? "", fuente, Brushes.Black, new RectangleF(desde, y + 6, hasta - desde, AltoFila - 6), formato);
        }
    }
}
