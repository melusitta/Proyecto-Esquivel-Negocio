using Servicios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // Impresión de factura, reporte y bitácora.
    // Con impresora se abre la vista previa (desde ahí se imprime). Sin ninguna impresora instalada
    // la vista previa no funciona, así que las mismas páginas se guardan como PDF en Descargas y se abre el archivo.
    public static class Impresion
    {
        // Hoja A4 en centésimas de pulgada (la unidad con la que dibujan los PrintDocument) y márgenes de 1"
        private const int AnchoHoja = 827, AltoHoja = 1169, Margen = 100;
        private const int Dpi = 200;

        public static void MostrarOGuardarPdf(PrintDocument documento, IWin32Window dueño, string titulo, string nombreArchivo)
        {
            if (HayImpresora())
            {
                using (var vista = new PrintPreviewDialog())
                {
                    vista.Document = documento;
                    vista.Width = 900;
                    vista.Height = 800;
                    vista.Text = titulo;
                    if (Tema.Icono != null)
                        vista.Icon = Tema.Icono;
                    vista.ShowDialog(dueño);
                }
                return;
            }

            var g = GestorIdioma.Instancia;
            try
            {
                string ruta = RutaLibre(CarpetaDescargas(), nombreArchivo);
                GuardarPdf(documento, ruta);
                MessageBox.Show(dueño, string.Format(g.Obtener("Impresion", "msgPdfGuardado"), ruta),
                    g.Obtener("Impresion", "msgPdfTitulo"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                try { Process.Start(ruta); } catch { /* sin visor de PDF: el archivo igual quedó en Descargas */ }
            }
            catch (Exception ex)
            {
                MessageBox.Show(dueño, g.Obtener("Impresion", "msgPdfError") + "\n" + ex.Message,
                    g.Obtener("Impresion", "msgPdfTitulo"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool HayImpresora()
        {
            try
            {
                return PrinterSettings.InstalledPrinters.Count > 0 && new PrinterSettings().IsValid;
            }
            catch
            {
                return false;
            }
        }

        // ---------- PDF ----------

        // Dibuja cada página del documento en una imagen (con el mismo código que usa la impresora) y arma el PDF
        public static void GuardarPdf(PrintDocument documento, string ruta)
        {
            var paginas = new List<Bitmap>();
            try
            {
                Invocar(documento, "OnBeginPrint", new PrintEventArgs());
                Rectangle hoja = new Rectangle(0, 0, AnchoHoja, AltoHoja);
                Rectangle margenes = new Rectangle(Margen, Margen, AnchoHoja - 2 * Margen, AltoHoja - 2 * Margen);
                bool hayMas = true;
                while (hayMas && paginas.Count < 500)
                {
                    var bmp = new Bitmap(AnchoHoja * Dpi / 100, AltoHoja * Dpi / 100, PixelFormat.Format24bppRgb);
                    bmp.SetResolution(Dpi, Dpi);
                    using (Graphics gr = Graphics.FromImage(bmp))
                    {
                        gr.Clear(Color.White);
                        // Misma unidad que la impresora: centésimas de pulgada
                        gr.PageUnit = GraphicsUnit.Inch;
                        gr.PageScale = 0.01f;
                        gr.SmoothingMode = SmoothingMode.AntiAlias;
                        gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                        var e = new PrintPageEventArgs(gr, margenes, hoja, null);
                        Invocar(documento, "OnPrintPage", e);
                        hayMas = e.HasMorePages;
                    }
                    paginas.Add(bmp);
                }
                Invocar(documento, "OnEndPrint", new PrintEventArgs());
                EscribirPdf(paginas, ruta);
            }
            finally
            {
                foreach (Bitmap b in paginas)
                    b.Dispose();
            }
        }

        // Dispara los eventos del documento (BeginPrint, PrintPage, EndPrint) sin pasar por una impresora
        private static void Invocar(PrintDocument documento, string metodo, EventArgs e)
        {
            MethodInfo m = typeof(PrintDocument).GetMethod(metodo, BindingFlags.Instance | BindingFlags.NonPublic);
            try
            {
                m.Invoke(documento, new object[] { e });
            }
            catch (TargetInvocationException ex) when (ex.InnerException != null)
            {
                throw ex.InnerException;
            }
        }

        // PDF mínimo: una imagen por página (RGB comprimido con Flate, sin pérdida)
        private static void EscribirPdf(List<Bitmap> paginas, string ruta)
        {
            var inv = CultureInfo.InvariantCulture;
            float anchoPt = AnchoHoja * 0.72f, altoPt = AltoHoja * 0.72f;   // 1/100" -> puntos (1/72")
            var offsets = new List<long>();
            using (var fs = new FileStream(ruta, FileMode.Create, FileAccess.Write))
            {
                void Escribir(string s) { byte[] b = Encoding.ASCII.GetBytes(s); fs.Write(b, 0, b.Length); }
                void Objeto(string contenido) { offsets.Add(fs.Position); Escribir(offsets.Count + " 0 obj\n" + contenido + "\nendobj\n"); }

                Escribir("%PDF-1.4\n%âãÏÓ\n");
                int n = paginas.Count;
                // 1: catálogo, 2: páginas; por cada página: página, contenido e imagen (3 objetos)
                var kids = new StringBuilder();
                for (int i = 0; i < n; i++)
                    kids.Append(3 + i * 3).Append(" 0 R ");
                Objeto("<< /Type /Catalog /Pages 2 0 R >>");
                Objeto("<< /Type /Pages /Kids [" + kids + "] /Count " + n + " >>");

                for (int i = 0; i < n; i++)
                {
                    int pag = 3 + i * 3, cont = pag + 1, img = pag + 2;
                    Objeto(string.Format(inv, "<< /Type /Page /Parent 2 0 R /MediaBox [0 0 {0:0.##} {1:0.##}] " +
                        "/Resources << /XObject << /Im{2} {3} 0 R >> >> /Contents {4} 0 R >>", anchoPt, altoPt, i, img, cont));

                    string dibujo = string.Format(inv, "q {0:0.##} 0 0 {1:0.##} 0 0 cm /Im{2} Do Q", anchoPt, altoPt, i);
                    Objeto("<< /Length " + dibujo.Length + " >>\nstream\n" + dibujo + "\nendstream");

                    Bitmap bmp = paginas[i];
                    byte[] datos = Comprimir(PixelesRgb(bmp));
                    offsets.Add(fs.Position);
                    Escribir(img + " 0 obj\n<< /Type /XObject /Subtype /Image /Width " + bmp.Width + " /Height " + bmp.Height +
                             " /ColorSpace /DeviceRGB /BitsPerComponent 8 /Filter /FlateDecode /Length " + datos.Length + " >>\nstream\n");
                    fs.Write(datos, 0, datos.Length);
                    Escribir("\nendstream\nendobj\n");
                }

                long xref = fs.Position;
                var tabla = new StringBuilder("xref\n0 " + (offsets.Count + 1) + "\n0000000000 65535 f \n");
                foreach (long o in offsets)
                    tabla.Append(o.ToString("D10")).Append(" 00000 n \n");
                Escribir(tabla.ToString());
                Escribir("trailer\n<< /Size " + (offsets.Count + 1) + " /Root 1 0 R >>\nstartxref\n" + xref + "\n%%EOF\n");
            }
        }

        // Filas de píxeles en orden R, G, B (el Bitmap las guarda como B, G, R y con relleno al final de cada fila)
        private static byte[] PixelesRgb(Bitmap bmp)
        {
            var datos = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            try
            {
                int filaBytes = bmp.Width * 3;
                byte[] fila = new byte[Math.Abs(datos.Stride)];
                byte[] rgb = new byte[filaBytes * bmp.Height];
                for (int y = 0; y < bmp.Height; y++)
                {
                    Marshal.Copy(IntPtr.Add(datos.Scan0, y * datos.Stride), fila, 0, fila.Length);
                    for (int x = 0, o = y * filaBytes; x < filaBytes; x += 3, o += 3)
                    {
                        rgb[o] = fila[x + 2];
                        rgb[o + 1] = fila[x + 1];
                        rgb[o + 2] = fila[x];
                    }
                }
                return rgb;
            }
            finally
            {
                bmp.UnlockBits(datos);
            }
        }

        // FlateDecode espera formato zlib: encabezado + deflate + suma Adler-32
        private static byte[] Comprimir(byte[] datos)
        {
            using (var ms = new MemoryStream())
            {
                ms.WriteByte(0x78);
                ms.WriteByte(0x9C);
                using (var deflate = new DeflateStream(ms, CompressionLevel.Optimal, true))
                    deflate.Write(datos, 0, datos.Length);
                uint a = 1, b = 0;
                foreach (byte d in datos)
                {
                    a = (a + d) % 65521;
                    b = (b + a) % 65521;
                }
                uint adler = (b << 16) | a;
                ms.WriteByte((byte)(adler >> 24));
                ms.WriteByte((byte)(adler >> 16));
                ms.WriteByte((byte)(adler >> 8));
                ms.WriteByte((byte)adler);
                return ms.ToArray();
            }
        }

        // ---------- Carpeta y nombre ----------

        [DllImport("shell32.dll")]
        private static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint flags, IntPtr token, out IntPtr ruta);

        // Carpeta Descargas del usuario (aunque la haya movido); si no se puede obtener, la de su perfil
        public static string CarpetaDescargas()
        {
            try
            {
                IntPtr p;
                if (SHGetKnownFolderPath(new Guid("374DE290-123F-4565-9164-39C4925E467B"), 0, IntPtr.Zero, out p) == 0)
                {
                    string ruta = Marshal.PtrToStringUni(p);
                    Marshal.FreeCoTaskMem(p);
                    if (Directory.Exists(ruta))
                        return ruta;
                }
            }
            catch { }
            string perfil = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            return Directory.Exists(perfil) ? perfil : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        // Si ya existe un archivo con ese nombre, agrega (2), (3)...
        private static string RutaLibre(string carpeta, string nombreArchivo)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                nombreArchivo = nombreArchivo.Replace(c, '_');
            string ruta = Path.Combine(carpeta, nombreArchivo + ".pdf");
            for (int i = 2; File.Exists(ruta); i++)
                ruta = Path.Combine(carpeta, nombreArchivo + " (" + i + ").pdf");
            return ruta;
        }
    }
}
