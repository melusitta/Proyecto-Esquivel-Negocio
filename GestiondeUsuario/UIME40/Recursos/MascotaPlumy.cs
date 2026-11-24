using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // Plumy, la mascota de la pantalla principal: un muñequito del pony con su barrilete que flota y se mece
    // suavecito, con un globito de diálogo que saluda y da consejos. Con un clic pasa al consejo siguiente
    // y da un saltito.
    // Los textos los arma FormPrincipal; este control solo los dibuja.
    public class MascotaPlumy : Control
    {
        private const int Avatar = 160;          // ancho del muñequito
        private const int Radio = 18;

        private string _saludo = "";
        private List<string> _consejos = new List<string>();
        private string _pista = "";
        private int _actual;

        private readonly Timer _timer = new Timer { Interval = 40 };
        private double _tiempo;          // para flotar
        private float _salto;            // 0..1 mientras dura el saltito

        public MascotaPlumy()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;
            _timer.Tick += (s, e) =>
            {
                _tiempo += 0.08;
                if (_salto > 0) _salto = Math.Max(0, _salto - 0.08f);
                Invalidate(new Rectangle(0, 0, Avatar + 30, Height));
            };
        }

        // Saludo (arriba, en negrita), consejos (de a uno, se cambian con un clic) y la pista chiquita de abajo
        public void Mostrar(string saludo, List<string> consejos, string pista)
        {
            _saludo = saludo ?? "";
            _consejos = consejos ?? new List<string>();
            _pista = pista ?? "";
            if (_actual >= _consejos.Count) _actual = 0;
            Invalidate();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!DesignMode) _timer.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _timer.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (_consejos.Count > 1)
                _actual = (_actual + 1) % _consejos.Count;
            _salto = 1;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Muñequito flotando y meciéndose (y saltando si se lo tocó), con su sombrita
            Image muneco = Tema.Mascota;
            float alto = muneco == null ? Avatar : Avatar * muneco.Height / (float)muneco.Width;
            float flota = (float)Math.Sin(_tiempo) * 4;
            float salta = (float)Math.Sin(_salto * Math.PI) * 18;
            float piso = 22 + alto + 4;
            using (var sombra = new SolidBrush(Color.FromArgb(35, Tema.PrincipalOscuro)))
            {
                float achica = (flota + salta) / 2;
                g.FillEllipse(sombra, 30 + achica, piso, Avatar - 40 - 2 * achica, 12);
            }
            if (muneco != null)
            {
                var estado = g.Save();
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.TranslateTransform(10 + Avatar / 2f, 22 + alto / 2f - flota - salta);
                g.RotateTransform((float)Math.Sin(_tiempo * 0.7) * 3);
                g.DrawImage(muneco, -Avatar / 2f, -alto / 2f, Avatar, alto);
                g.Restore(estado);
            }

            // Globito de diálogo con la colita hacia Plumy
            float x = Avatar + 34, ancho = Width - x - 4;
            if (ancho < 60) return;
            string consejo = _consejos.Count > 0 ? _consejos[_actual] : "";
            int anchoTexto = (int)ancho - 36;
            using (var negrita = new Font(Tema.Fuente, 13F, FontStyle.Bold))
            using (var normal = new Font(Tema.Fuente, 11F))
            using (var chica = new Font(Tema.Fuente, 8.5F, FontStyle.Italic))
            {
                Size s = TextRenderer.MeasureText(_saludo, negrita, new Size(anchoTexto, 0), TextFormatFlags.WordBreak);
                Size c = TextRenderer.MeasureText(consejo, normal, new Size(anchoTexto, 0), TextFormatFlags.WordBreak);
                Size p = TextRenderer.MeasureText(_pista, chica, new Size(anchoTexto, 0), TextFormatFlags.WordBreak);
                float altoGlobo = Math.Min(Height - 4, 18 + s.Height + 8 + c.Height + 10 + p.Height + 16);
                var globo = new RectangleF(x, 10, ancho, altoGlobo);

                using (GraphicsPath forma = Tema.RectanguloRedondeado(globo, Radio))
                using (var colita = new GraphicsPath())
                using (var blanco = new SolidBrush(Color.White))
                using (var borde = new Pen(Tema.Principal, 2))
                {
                    float yc = Math.Min(globo.Bottom - 30, 60);
                    colita.AddPolygon(new[] { new PointF(x + 2, yc - 12), new PointF(x - 22, yc + 4), new PointF(x + 2, yc + 10) });
                    using (var region = new Region(forma))
                    {
                        region.Union(colita);
                        g.FillRegion(blanco, region);
                    }
                    g.DrawPath(borde, forma);
                    g.DrawLine(borde, x + 1, yc - 12, x - 22, yc + 4);
                    g.DrawLine(borde, x - 22, yc + 4, x + 1, yc + 10);
                    using (var tapa = new Pen(Color.White, 3))
                        g.DrawLine(tapa, x + 1, yc - 10, x + 1, yc + 8);
                }

                int tx = (int)x + 18, ty = 10 + 18;
                TextRenderer.DrawText(g, _saludo, negrita, new Rectangle(tx, ty, anchoTexto, s.Height), Tema.PrincipalOscuro, TextFormatFlags.WordBreak);
                ty += s.Height + 8;
                TextRenderer.DrawText(g, consejo, normal, new Rectangle(tx, ty, anchoTexto, c.Height), Tema.Texto, TextFormatFlags.WordBreak);
                ty += c.Height + 10;
                TextRenderer.DrawText(g, _pista, chica, new Rectangle(tx, ty, anchoTexto, p.Height), Tema.Acento, TextFormatFlags.WordBreak);
            }
        }
    }
}
