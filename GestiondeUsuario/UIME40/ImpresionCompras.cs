using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;

namespace GestiondeUsuario
{
    // PN2: documentos que se envían al proveedor (pasos 3, 7 y 9): solicitud de compra, orden de compra
    // y comprobante de pago. Se guardan como PDF en Descargas\Plumy - Compras para mandarlos.
    // El remito de la mercadería recibida (paso 11) está en ImpresionRemito.
    public class ImpresionCompras
    {
        // ---------- Contenido del documento ----------
        private string _titulo, _numero, _fecha, _sello, _nota;
        private readonly List<Tuple<string, string>> _datos = new List<Tuple<string, string>>();
        private string[] _columnas;
        private float[] _anchos;          // proporción del ancho de cada columna (suman 1)
        private bool[] _numericas;        // alineadas a la derecha
        private readonly List<string[]> _filas = new List<string[]>();
        private readonly List<Tuple<string, string>> _totales = new List<Tuple<string, string>>();
        private readonly List<Tuple<string, string>> _pie = new List<Tuple<string, string>>();
        private int _siguienteFila;

        private ImpresionCompras() { }

        private static string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("ImpresionCompras", clave);
        }

        private void DatosProveedor(Proveedor p)
        {
            _datos.Add(Tuple.Create(T("proveedor"), p.RazonSocial));
            _datos.Add(Tuple.Create(T("cuit"), p.CUIT));
            _datos.Add(Tuple.Create(T("contacto"), p.Contacto + "  ·  " + p.Telefono + "  ·  " + p.Email));
        }

        private void ColumnasProductos(bool conPrecios)
        {
            if (conPrecios)
            {
                _columnas = new[] { T("colCodigo"), T("colProducto"), T("colCantidad"), T("colPrecio"), T("colSubtotal") };
                _anchos = new[] { 0.12f, 0.35f, 0.14f, 0.17f, 0.22f };
                _numericas = new[] { false, false, true, true, true };
            }
            else
            {
                _columnas = new[] { T("colCodigo"), T("colProducto"), T("colMarca"), T("colCantidad") };
                _anchos = new[] { 0.14f, 0.50f, 0.20f, 0.16f };
                _numericas = new[] { false, false, false, true };
            }
        }

        // Paso 3: solicitud que recibe un proveedor con los productos que se le asignaron
        public static ImpresionCompras Solicitud(SolicitudCompra s, Proveedor proveedor)
        {
            var d = new ImpresionCompras
            {
                _titulo = T("tituloSolicitud"),
                _numero = s.NroSolicitud.ToString("D6"),
                _fecha = (s.FechaEnvio ?? DateTime.Now).ToString("dd/MM/yyyy"),
                _nota = T("notaSolicitud")
            };
            d.DatosProveedor(proveedor);
            d._datos.Add(Tuple.Create(T("prioridad"), TextosCompras.Prioridad(s.Prioridad)));
            d.ColumnasProductos(false);
            foreach (ItemSolicitud i in s.Items.Where(i => i.Proveedor.Id == proveedor.Id))
                d._filas.Add(new[] { i.Producto.CodigoProducto.ToString(), i.Producto.Nombre, i.Producto.Marca, i.Cantidad.ToString() });
            d._pie.Add(Tuple.Create(T("solicitadoPor"), s.CreadaPor));
            return d;
        }

        // Paso 7: orden de compra
        public static ImpresionCompras Orden(OrdenCompra o)
        {
            var d = new ImpresionCompras
            {
                _titulo = T("tituloOrden"),
                _numero = o.NroOrden.ToString("D6"),
                _fecha = o.FechaEmision.ToString("dd/MM/yyyy")
            };
            d.DatosProveedor(o.Proveedor);
            d._datos.Add(Tuple.Create(T("condEntrega"), o.CondicionesEntrega));
            d._datos.Add(Tuple.Create(T("condPago"), o.CondicionesPago));
            d.ColumnasProductos(true);
            foreach (ItemOrdenCompra i in o.Items)
                d._filas.Add(new[] { i.Producto.CodigoProducto.ToString(), i.Producto.Nombre, i.Cantidad.ToString(),
                                     "$ " + i.PrecioUnitario.ToString("N2"), "$ " + i.Subtotal.ToString("N2") });
            d._totales.Add(Tuple.Create(T("total"), "$ " + o.Total.ToString("N2")));
            d._pie.Add(Tuple.Create(T("emitidaPor"), o.Supervisor));
            return d;
        }

        // Paso 9: comprobante del pago de la orden
        public static ImpresionCompras Comprobante(OrdenCompra o, FacturaProveedor f)
        {
            var d = new ImpresionCompras
            {
                _titulo = T("tituloComprobante"),
                _numero = f.NroComprobante,
                _fecha = f.FechaOperacion.ToString("dd/MM/yyyy"),
                _sello = T("pagado")
            };
            d.DatosProveedor(o.Proveedor);
            d._datos.Add(Tuple.Create(T("ordenRef"), o.NroOrden.ToString("D6")));
            d._datos.Add(Tuple.Create(T("facturaRef"), f.NroFactura + "  (" + f.Fecha.ToString("dd/MM/yyyy") + ")"));
            d.ColumnasProductos(true);
            foreach (ItemOrdenCompra i in o.Items)
                d._filas.Add(new[] { i.Producto.CodigoProducto.ToString(), i.Producto.Nombre, i.Cantidad.ToString(),
                                     "$ " + i.PrecioUnitario.ToString("N2"), "$ " + i.Subtotal.ToString("N2") });
            d._totales.Add(Tuple.Create(T("importe"), "$ " + f.Importe.ToString("N2")));
            d._totales.Add(Tuple.Create(T("impuestos"), "$ " + f.Impuestos.ToString("N2")));
            d._totales.Add(Tuple.Create(T("montoAbonado"), "$ " + f.MontoAbonado.ToString("N2")));
            d._pie.Add(Tuple.Create(T("pagadoPor"), f.Supervisor));
            return d;
        }

        // ---------- PDF ----------

        public static string Carpeta()
        {
            string carpeta = Path.Combine(Impresion.CarpetaDescargas(), "Plumy - Compras");
            Directory.CreateDirectory(carpeta);
            return carpeta;
        }

        // Guarda el documento como PDF (si ya existe uno con el mismo nombre, lo reemplaza: es el mismo documento)
        public string GuardarPdf(string nombreArchivo)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                nombreArchivo = nombreArchivo.Replace(c, '_');
            string ruta = Path.Combine(Carpeta(), nombreArchivo + ".pdf");
            using (PrintDocument documento = CrearDocumento())
                Impresion.GuardarPdf(documento, ruta);
            return ruta;
        }

        // Después de un envío: genera los PDF, muestra el mensaje de éxito con dónde quedaron y ofrece abrirlos.
        // Si el PDF falla, el envío igual quedó registrado y se avisa aparte.
        public static void InformarEnvio(System.Windows.Forms.IWin32Window dueño, string mensajeExito, string tituloExito,
                                         Func<List<string>> generarPdfs)
        {
            List<string> rutas;
            try
            {
                rutas = generarPdfs();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(dueño, mensajeExito + "\n\n" + T("msgPdfError") + " " + GestorIdioma.Instancia.TraducirError(ex),
                    tituloExito, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }
            // Aviso con el vínculo para abrir el PDF (o la carpeta, si son varios)
            string detalle = rutas.Count == 1 ? T("msgPdfListo") : string.Format(T("msgPdfsListos"), rutas.Count);
            Aviso.Exito(mensajeExito + "\n" + detalle, T(rutas.Count == 1 ? "abrirPdf" : "abrirCarpeta"), () => Abrir(rutas));
        }

        public static string NombreArchivo(string clave, string numero, string proveedor)
        {
            // Sin punto final: "Peluches del Sur S.A." no debe quedar "S.A..pdf"
            return string.Format(T(clave), numero, (proveedor ?? "").Trim().TrimEnd('.'));
        }

        public static void Abrir(IEnumerable<string> rutas)
        {
            List<string> lista = rutas.ToList();
            try
            {
                if (lista.Count == 1)
                    Process.Start(lista[0]);
                else if (lista.Count > 1)
                    Process.Start("explorer.exe", "/select,\"" + lista[0] + "\"");
            }
            catch { /* sin visor de PDF: los archivos igual quedaron guardados */ }
        }

        public PrintDocument CrearDocumento()
        {
            var documento = new PrintDocument { DocumentName = _titulo + " " + _numero };
            documento.BeginPrint += (s, e) => _siguienteFila = 0;
            documento.PrintPage += ImprimirPagina;
            return documento;
        }

        private void ImprimirPagina(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle area = e.MarginBounds;
            float izq = area.Left, der = area.Right, ancho = der - izq;
            float y = area.Top;

            using (var fMarca = new Font(Tema.Fuente, 20, FontStyle.Bold))
            using (var fTitulo = new Font(Tema.Fuente, 16, FontStyle.Bold))
            using (var fNormal = new Font(Tema.Fuente, 10))
            using (var fNegrita = new Font(Tema.Fuente, 10, FontStyle.Bold))
            using (var fChica = new Font(Tema.Fuente, 9))
            using (var fTotal = new Font(Tema.Fuente, 12, FontStyle.Bold))
            using (var lila = new SolidBrush(Tema.PrincipalOscuro))
            using (var lapizLila = new Pen(Tema.Principal, 1.5f))
            using (var derecha = new StringFormat { Alignment = StringAlignment.Far })
            using (var centro = new StringFormat { Alignment = StringAlignment.Center })
            {
                // ---- Encabezado: logo y comercio a la izquierda; tipo, número y fecha a la derecha
                if (Tema.Logo != null)
                    g.DrawImage(Tema.Logo, izq, y, 75, 75);
                g.DrawString("Plumy", fMarca, lila, izq + 85, y + 8);
                g.DrawString(GestorIdioma.Instancia.Obtener("ImpresionFactura", "comercio"), fChica, Brushes.Black, izq + 87, y + 45);
                g.DrawString(_titulo, fTitulo, lila, der, y, derecha);
                g.DrawString(T("nro") + " " + _numero, fNegrita, Brushes.Black, der, y + 32, derecha);
                g.DrawString(T("fecha") + " " + _fecha, fNormal, Brushes.Black, der, y + 52, derecha);
                y += 90;
                g.DrawLine(lapizLila, izq, y, der, y);
                y += 10;

                // ---- Datos (proveedor, condiciones...) solo en la primera página
                if (_siguienteFila == 0)
                {
                    foreach (var dato in _datos)
                    {
                        g.DrawString(dato.Item1, fNegrita, Brushes.Black, izq, y);
                        g.DrawString(dato.Item2 ?? "", fNormal, Brushes.Black, new RectangleF(izq + 170, y, ancho - 170, 40));
                        y += 22;
                    }
                    y += 8;
                    if (_nota != null)
                    {
                        g.DrawString(_nota, fChica, Brushes.Black, new RectangleF(izq, y, ancho, 40));
                        y += 40;
                    }
                }

                // ---- Tabla
                float[] bordes = new float[_columnas.Length + 1];
                bordes[0] = izq;
                for (int i = 0; i < _columnas.Length; i++)
                    bordes[i + 1] = bordes[i] + ancho * _anchos[i];
                using (var fondo = new SolidBrush(Tema.Principal))
                    g.FillRectangle(fondo, izq, y, ancho, 24);
                for (int i = 0; i < _columnas.Length; i++)
                    Celda(g, _columnas[i], fNegrita, bordes[i], bordes[i + 1], y + 3, _numericas[i], derecha);
                y += 30;

                float limite = area.Bottom - 120 - 22 * _totales.Count;
                while (_siguienteFila < _filas.Count)
                {
                    if (y > limite)
                    {
                        g.DrawString(T("continua"), fChica, Brushes.Gray, der, y, derecha);
                        e.HasMorePages = true;
                        return;
                    }
                    string[] fila = _filas[_siguienteFila];
                    for (int i = 0; i < fila.Length; i++)
                        Celda(g, fila[i], fNormal, bordes[i], bordes[i + 1], y, _numericas[i], derecha);
                    y += 22;
                    _siguienteFila++;
                }
                y += 6;
                g.DrawLine(lapizLila, izq, y, der, y);
                y += 10;

                // ---- Totales y sello
                foreach (var total in _totales)
                {
                    g.DrawString(total.Item1 + "   " + total.Item2, fTotal, lila, der - 6, y, derecha);
                    y += 24;
                }
                if (_sello != null)
                {
                    SizeF tam = g.MeasureString(_sello, fTitulo);
                    var caja = new RectangleF(izq, y - 24 * _totales.Count, tam.Width + 14, tam.Height + 4);
                    using (var lapizSello = new Pen(Tema.Acento, 2.5f))
                    using (var rosa = new SolidBrush(Tema.Acento))
                    {
                        g.DrawRectangle(lapizSello, caja.X, caja.Y, caja.Width, caja.Height);
                        g.DrawString(_sello, fTitulo, rosa, caja.X + 7, caja.Y + 2);
                    }
                }
                y += 10;
                foreach (var p in _pie)
                {
                    g.DrawString(p.Item1 + " " + p.Item2, fChica, Brushes.Black, izq, y);
                    y += 18;
                }
                g.DrawString(T("pie"), fNegrita, lila, (izq + der) / 2, area.Bottom - 30, centro);
                e.HasMorePages = false;
            }
        }

        private static void Celda(Graphics g, string texto, Font fuente, float desde, float hasta, float y, bool numerica, StringFormat derecha)
        {
            var caja = new RectangleF(desde + 6, y, hasta - desde - 12, 22);
            using (var formato = new StringFormat(numerica ? derecha : StringFormat.GenericDefault) { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap })
                g.DrawString(texto ?? "", fuente, Brushes.Black, caja, formato);
        }
    }
}
