using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // Aviso que aparece abajo a la derecha, se desliza hacia arriba y se va solo: reemplaza la ventanita de
    // "Éxito" de Windows. Los errores y las preguntas (Sí/No) siguen siendo MessageBox para que no se pierdan.
    // Si hay varios a la vez se apilan. Con el mouse encima no se cierra; con un clic se cierra.
    public class Aviso : Form
    {
        private const int Ancho = 380, Margen = 16, Radio = 14;
        private const int Visible_ms = 4500, VisibleConAccion_ms = 8000;

        private static readonly List<Aviso> _abiertos = new List<Aviso>();

        private readonly string _titulo, _mensaje, _textoAccion;
        private readonly Action _accion;
        private readonly bool _exito;
        private readonly Timer _timer = new Timer { Interval = 15 };
        private int _restante_ms;
        private float _aparicion;            // 0 -> 1 al entrar, 1 -> 0 al salir
        private bool _saliendo;
        private Rectangle _zonaAccion, _zonaCerrar;
        private int _yFinal;

        public static void Exito(string mensaje)
        {
            Mostrar(true, mensaje, null, null);
        }

        // Con un vínculo de acción, por ejemplo "📄 Abrir PDF"
        public static void Exito(string mensaje, string textoAccion, Action accion)
        {
            Mostrar(true, mensaje, textoAccion, accion);
        }

        public static void Info(string mensaje)
        {
            Mostrar(false, mensaje, null, null);
        }

        private static void Mostrar(bool exito, string mensaje, string textoAccion, Action accion)
        {
            var g = GestorIdioma.Instancia;
            var aviso = new Aviso(exito, g.Obtener("Aviso", exito ? "tituloExito" : "tituloInfo"), mensaje ?? "", textoAccion, accion);
            aviso.Ubicar();
            aviso.Show();
        }

        private Aviso(bool exito, string titulo, string mensaje, string textoAccion, Action accion)
        {
            _exito = exito;
            _titulo = titulo;
            _mensaje = mensaje;
            _textoAccion = textoAccion;
            _accion = accion;
            _restante_ms = accion != null ? VisibleConAccion_ms : Visible_ms;

            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            TopMost = true;
            StartPosition = FormStartPosition.Manual;
            BackColor = Color.White;
            DoubleBuffered = true;
            Opacity = 0;
            Cursor = Cursors.Hand;
            Font = new Font(Tema.Fuente, 10F);

            // Alto según el texto
            using (var negrita = new Font(Tema.Fuente, 11F, FontStyle.Bold))
            {
                int anchoTexto = Ancho - 80 - 30;
                Size titulo_ = TextRenderer.MeasureText(_titulo, negrita, new Size(anchoTexto, 0), TextFormatFlags.WordBreak);
                Size cuerpo = TextRenderer.MeasureText(_mensaje, Font, new Size(anchoTexto, 0), TextFormatFlags.WordBreak);
                int alto = 16 + titulo_.Height + 4 + cuerpo.Height + (accion != null ? 30 : 0) + 16;
                Size = new Size(Ancho, Math.Max(84, alto));
            }
            using (GraphicsPath forma = Tema.RectanguloRedondeado(new RectangleF(0, 0, Width, Height), Radio))
                Region = new Region(forma);

            _timer.Tick += Avanzar;
            MouseClick += AlHacerClic;
            MouseMove += (s, e) => Cursor = _zonaAccion.Contains(e.Location) || _zonaCerrar.Contains(e.Location) ? Cursors.Hand : Cursors.Default;
        }

        // No le saca el foco a la pantalla en la que se está trabajando
        protected override bool ShowWithoutActivation => true;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x08000000 | 0x00000080;   // WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW
                return cp;
            }
        }

        // Ventana de Plumy en la que se está trabajando. Si es un diálogo (que suele cerrarse justo después
        // del aviso, como Pagar o Realizar pre-registro), la de atrás.
        private static Form VentanaDeReferencia()
        {
            Form ventana = Form.ActiveForm;
            if (ventana == null || ventana is Aviso)
                foreach (Form f in Application.OpenForms)
                    if (f.Visible && !(f is Aviso) && f.WindowState != FormWindowState.Minimized)
                        ventana = f;
            while (ventana != null && ventana.Modal && ventana.Owner != null)
                ventana = ventana.Owner;
            return ventana != null && ventana.Visible && ventana.WindowState != FormWindowState.Minimized ? ventana : null;
        }

        // Abajo a la derecha dentro de la ventana; si no hay ninguna visible, en la pantalla
        private static Rectangle AreaDeTrabajo()
        {
            Form ventana = VentanaDeReferencia();
            Rectangle pantalla = (ventana != null ? Screen.FromControl(ventana) : Screen.FromPoint(Cursor.Position)).WorkingArea;
            if (ventana == null)
                return pantalla;
            Rectangle area = Rectangle.Intersect(ventana.RectangleToScreen(ventana.ClientRectangle), pantalla);
            return area.Width >= Ancho + 2 * Margen && area.Height > 100 ? area : pantalla;
        }

        // Se apila arriba de los avisos que ya están abiertos
        private void Ubicar()
        {
            Rectangle area = AreaDeTrabajo();
            int y = area.Bottom - Margen;
            foreach (Aviso otro in _abiertos)
                y = Math.Min(y, otro._yFinal - 10);
            _yFinal = y - Height;
            Location = new Point(area.Right - Width - Margen, _yFinal + 20);
            _abiertos.Add(this);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _timer.Start();
        }

        private void Avanzar(object sender, EventArgs e)
        {
            bool encima = ClientRectangle.Contains(PointToClient(Cursor.Position));
            if (_saliendo)
            {
                _aparicion -= 0.08f;
                if (_aparicion <= 0)
                {
                    _timer.Stop();
                    Close();
                    return;
                }
            }
            else if (_aparicion < 1)
                _aparicion = Math.Min(1, _aparicion + 0.1f);
            else if (!encima)
            {
                _restante_ms -= _timer.Interval;
                if (_restante_ms <= 0)
                    _saliendo = true;
            }

            // Entra deslizándose hacia arriba y saliendo se desvanece
            float suave = 1 - (1 - _aparicion) * (1 - _aparicion);
            Opacity = suave;
            if (!_saliendo)
                Top = _yFinal + (int)(20 * (1 - suave));
        }

        private void AlHacerClic(object sender, MouseEventArgs e)
        {
            if (_accion != null && _zonaAccion.Contains(e.Location))
            {
                try { _accion(); } catch { /* sin visor o carpeta: el aviso igual se cierra */ }
            }
            _saliendo = true;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _abiertos.Remove(this);
            _timer.Dispose();
            base.OnFormClosed(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Color color = _exito ? Tema.Acento : Tema.PrincipalOscuro;

            // Borde lila y una franja de color a la izquierda
            using (GraphicsPath forma = Tema.RectanguloRedondeado(new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f), Radio))
            using (var lapiz = new Pen(Tema.Principal, 1.5f))
            using (var franja = new SolidBrush(_exito ? Tema.AcentoSuave : Tema.PrincipalSuave))
            {
                g.SetClip(new Rectangle(0, 0, 64, Height));
                g.FillPath(franja, forma);
                g.ResetClip();
                g.DrawPath(lapiz, forma);
            }

            // Circulito con un tilde (éxito) o una "i" (información)
            var circulo = new RectangleF(16, Height / 2f - 16, 32, 32);
            using (var relleno = new SolidBrush(color))
                g.FillEllipse(relleno, circulo);
            using (var blanco = new Pen(Color.White, 3f) { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round })
            {
                if (_exito)
                    g.DrawLines(blanco, new[] { new PointF(circulo.X + 9, circulo.Y + 17), new PointF(circulo.X + 14, circulo.Y + 22), new PointF(circulo.X + 23, circulo.Y + 11) });
                else
                {
                    g.DrawLine(blanco, circulo.X + 16, circulo.Y + 14, circulo.X + 16, circulo.Y + 23);
                    g.FillEllipse(Brushes.White, circulo.X + 14.3f, circulo.Y + 7, 3.4f, 3.4f);
                }
            }

            // Título, mensaje y la acción (si hay)
            int x = 80, anchoTexto = Width - x - 30, y = 16;
            using (var negrita = new Font(Tema.Fuente, 11F, FontStyle.Bold))
            {
                Size t = TextRenderer.MeasureText(_titulo, negrita, new Size(anchoTexto, 0), TextFormatFlags.WordBreak);
                TextRenderer.DrawText(g, _titulo, negrita, new Rectangle(x, y, anchoTexto, t.Height), Tema.PrincipalOscuro, TextFormatFlags.WordBreak);
                y += t.Height + 4;
            }
            Size m = TextRenderer.MeasureText(_mensaje, Font, new Size(anchoTexto, 0), TextFormatFlags.WordBreak);
            TextRenderer.DrawText(g, _mensaje, Font, new Rectangle(x, y, anchoTexto, m.Height), Tema.Texto, TextFormatFlags.WordBreak);
            y += m.Height + 6;
            if (_accion != null)
            {
                using (var vinculo = new Font(Tema.Fuente, 10F, FontStyle.Bold | FontStyle.Underline))
                {
                    Size a = TextRenderer.MeasureText(_textoAccion, vinculo);
                    _zonaAccion = new Rectangle(x, y, a.Width, a.Height);
                    TextRenderer.DrawText(g, _textoAccion, vinculo, _zonaAccion, Tema.Acento);
                }
            }

            // Cruz para cerrar
            _zonaCerrar = new Rectangle(Width - 28, 8, 18, 18);
            TextRenderer.DrawText(g, "✕", Font, _zonaCerrar, Tema.PrincipalOscuro,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}
