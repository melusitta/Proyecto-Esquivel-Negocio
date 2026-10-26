using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;

namespace GestiondeUsuario
{
    // Manual de usuario en PDF con los mismos temas que la ayuda en pantalla (los que el usuario puede usar).
    // Se guarda en Descargas y el aviso ofrece abrirlo.
    public class ImpresionAyuda
    {
        // Cada renglón del manual ya armado: texto, fuente, color, sangría y espacio antes
        private class Bloque
        {
            public string Texto;
            public Font Fuente;
            public Brush Color;
            public float Sangria, EspacioAntes;
            public bool NuevaPagina;
        }

        private readonly List<Bloque> _bloques = new List<Bloque>();
        private int _siguiente, _pagina;
        private readonly string _usuario;

        private ImpresionAyuda(string usuario)
        {
            _usuario = usuario;
        }

        private static string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormAyuda", clave);
        }

        public static void GenerarManual()
        {
            string ruta = Path.Combine(Impresion.CarpetaDescargas(), T("archivoManual") + ".pdf");
            try
            {
                var manual = new ImpresionAyuda(SessionManager.Instancia.ObtenerUsuarioActivo()?.Nombre);
                manual.Armar();
                using (PrintDocument documento = manual.CrearDocumento())
                    Impresion.GuardarPdf(documento, ruta);
                Aviso.Exito(string.Format(T("msgManualListo"), Path.GetFileName(ruta)), T("abrirManual"),
                    () => System.Diagnostics.Process.Start(ruta));
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(T("msgManualError") + " " + GestorIdioma.Instancia.TraducirError(ex),
                    T("tituloForm"), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void Armar()
        {
            var lila = new SolidBrush(Tema.PrincipalOscuro);
            var rosa = new SolidBrush(Tema.Acento);
            var negro = Brushes.Black;
            var fGrupo = new Font(Tema.Fuente, 18, FontStyle.Bold);
            var fTitulo = new Font(Tema.Fuente, 14, FontStyle.Bold);
            var fSubtitulo = new Font(Tema.Fuente, 11.5f, FontStyle.Bold);
            var fNormal = new Font(Tema.Fuente, 10.5f);
            var fConsejo = new Font(Tema.Fuente, 10.5f, FontStyle.Italic);

            List<TemaAyuda> temas = TemasAyuda.Disponibles();
            bool primerGrupo = true;
            foreach (string grupo in TemasAyuda.Grupos)
            {
                var delGrupo = temas.Where(t => t.Grupo == grupo).ToList();
                if (delGrupo.Count == 0) continue;
                _bloques.Add(new Bloque { Texto = TemasAyuda.NombreGrupo(grupo), Fuente = fGrupo, Color = lila, EspacioAntes = primerGrupo ? 0 : 10, NuevaPagina = !primerGrupo });
                primerGrupo = false;
                foreach (TemaAyuda tema in delGrupo)
                {
                    _bloques.Add(new Bloque { Texto = TemasAyuda.Titulo(tema), Fuente = fTitulo, Color = lila, EspacioAntes = 18 });
                    foreach (string linea in TemasAyuda.Lineas(tema))
                    {
                        string l = SinEmojis(linea);
                        if (linea.StartsWith("## "))
                            _bloques.Add(new Bloque { Texto = l.Substring(3), Fuente = fSubtitulo, Color = lila, EspacioAntes = 10 });
                        else if (linea.StartsWith("- "))
                            _bloques.Add(new Bloque { Texto = "•  " + l.Substring(2), Fuente = fNormal, Color = negro, Sangria = 18, EspacioAntes = 3 });
                        else if (linea.StartsWith("💡"))
                            _bloques.Add(new Bloque { Texto = l, Fuente = fConsejo, Color = rosa, EspacioAntes = 8 });
                        else if (linea.Length > 2 && char.IsDigit(linea[0]) && linea.IndexOf(". ") > 0 && linea.IndexOf(". ") < 3)
                            _bloques.Add(new Bloque { Texto = l, Fuente = fNormal, Color = negro, Sangria = 18, EspacioAntes = 4 });
                        else
                            _bloques.Add(new Bloque { Texto = l, Fuente = fNormal, Color = negro, EspacioAntes = 6 });
                    }
                }
            }
        }

        // GDI+ no dibuja emojis (salen cuadraditos): en el PDF se sacan
        private static string SinEmojis(string texto)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < texto.Length; i++)
            {
                char c = texto[i];
                if (char.IsSurrogate(c) || c == '️' || (c >= '☀' && c <= '➿') || (c >= '⬀' && c <= '⯿'))
                    continue;
                sb.Append(c);
            }
            return sb.ToString().Trim();
        }

        private PrintDocument CrearDocumento()
        {
            var documento = new PrintDocument { DocumentName = T("archivoManual") };
            documento.BeginPrint += (s, e) => { _siguiente = 0; _pagina = 0; };
            documento.PrintPage += ImprimirPagina;
            return documento;
        }

        private void ImprimirPagina(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle area = e.MarginBounds;
            float izq = area.Left, der = area.Right, ancho = der - izq, y = area.Top;
            _pagina++;

            using (var lila = new SolidBrush(Tema.PrincipalOscuro))
            using (var lapiz = new Pen(Tema.Principal, 1.5f))
            using (var fMarca = new Font(Tema.Fuente, 20, FontStyle.Bold))
            using (var fChica = new Font(Tema.Fuente, 9))
            using (var derecha = new StringFormat { Alignment = StringAlignment.Far })
            using (var centro = new StringFormat { Alignment = StringAlignment.Center })
            {
                // Encabezado: logo, Plumy y "Manual de usuario"
                if (Tema.Logo != null)
                    g.DrawImage(Tema.Logo, izq, y, 50, 50);
                g.DrawString("Plumy", fMarca, lila, izq + 60, y + 4);
                g.DrawString(T("tituloManual"), fChica, Brushes.Black, der, y + 8, derecha);
                g.DrawString(string.Format(T("paginaManual"), _pagina), fChica, Brushes.Gray, der, y + 26, derecha);
                y += 62;
                g.DrawLine(lapiz, izq, y, der, y);
                y += 14;

                // Portada: para quién es y la fecha
                if (_pagina == 1)
                {
                    using (var fPortada = new Font(Tema.Fuente, 24, FontStyle.Bold))
                        g.DrawString(T("tituloManual"), fPortada, lila, izq, y);
                    y += 46;
                    g.DrawString(string.Format(T("portadaManual"), _usuario, DateTime.Now.ToString("dd/MM/yyyy")), fChica, Brushes.Black, izq, y);
                    y += 30;
                }

                float limite = area.Bottom - 30;
                while (_siguiente < _bloques.Count)
                {
                    Bloque b = _bloques[_siguiente];
                    bool inicioDePagina = y < area.Top + 120;
                    if (b.NuevaPagina && !inicioDePagina)
                    {
                        e.HasMorePages = true;
                        break;
                    }
                    float espacio = inicioDePagina ? 0 : b.EspacioAntes;
                    SizeF tam = g.MeasureString(b.Texto, b.Fuente, (int)(ancho - b.Sangria));
                    // Un título no queda solo al pie de la página
                    bool esTitulo = b.Fuente.Size >= 14;
                    if (y + espacio + tam.Height + (esTitulo ? 60 : 0) > limite)
                    {
                        e.HasMorePages = true;
                        break;
                    }
                    y += espacio;
                    g.DrawString(b.Texto, b.Fuente, b.Color, new RectangleF(izq + b.Sangria, y, ancho - b.Sangria, tam.Height + 2));
                    y += tam.Height;
                    _siguiente++;
                }
                g.DrawString(GestorIdioma.Instancia.Obtener("ImpresionCompras", "pie"), fChica, lila, (izq + der) / 2, area.Bottom - 12, centro);
            }
        }
    }
}
