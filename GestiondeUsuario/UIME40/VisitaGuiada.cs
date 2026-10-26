using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // Visita guiada de la pantalla principal: oscurece la ventana, ilumina la parte que se explica con un
    // recuadro y muestra una tarjetita con el texto y los botones Saltar / Anterior / Siguiente.
    // Se maneja también con las flechas y Esc. Arranca sola la primera vez que cada usuario entra
    // (se recuerda en un archivo de la compu, no en la base) y se repite desde Ayuda → Visita guiada.
    public class VisitaGuiada : Form
    {
        public class Paso
        {
            public string Clave;                 // textos en la sección "VisitaGuiada": paso<Clave>Titulo / paso<Clave>Texto
            public Func<Rectangle> Zona;          // en coordenadas de la ventana; vacía = sin resaltar
        }

        private const int Relleno = 6, Separacion = 16, AnchoTarjeta = 340;

        private readonly Form _ventana;
        private readonly List<Paso> _pasos;
        private readonly Bitmap _foto, _fotoOscura;
        private int _actual;
        private Rectangle _zona;

        private readonly Panel _tarjeta = new Panel();
        private readonly Label _lblTitulo = new Label(), _lblContador = new Label(), _lblTexto = new Label();
        private readonly Button _btnSaltar = new Button(), _btnAnterior = new Button(), _btnSiguiente = new Button();

        // ---------- Recordar quién ya la vio ----------

        private static string ArchivoVistos()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Plumy", "visita_guiada.txt");
        }

        public static bool YaLaVio(string usuario)
        {
            try
            {
                string ruta = ArchivoVistos();
                return File.Exists(ruta) && File.ReadAllLines(ruta).Contains(usuario);
            }
            catch
            {
                return true;   // si no se puede leer, no se insiste con la visita
            }
        }

        private static void MarcarVista(string usuario)
        {
            try
            {
                if (string.IsNullOrEmpty(usuario) || YaLaVio(usuario)) return;
                string ruta = ArchivoVistos();
                Directory.CreateDirectory(Path.GetDirectoryName(ruta));
                File.AppendAllLines(ruta, new[] { usuario });
            }
            catch { /* sin permiso de escritura: la próxima vez se vuelve a ofrecer */ }
        }

        // ---------- Inicio ----------

        public static void Iniciar(Form ventana, List<Paso> pasos)
        {
            if (pasos.Count == 0 || !ventana.Visible || ventana.WindowState == FormWindowState.Minimized) return;
            using (var visita = new VisitaGuiada(ventana, pasos))
                visita.ShowDialog(ventana);
            MarcarVista(SessionManager.Instancia.ObtenerUsuarioActivo()?.NombreUsuario);
        }

        private VisitaGuiada(Form ventana, List<Paso> pasos)
        {
            _ventana = ventana;
            _pasos = pasos;

            // Una foto de la ventana: oscurecida de fondo y la original para la parte iluminada
            Size cliente = ventana.ClientSize;
            _foto = new Bitmap(cliente.Width, cliente.Height);
            using (var completa = new Bitmap(ventana.Width, ventana.Height))
            {
                ventana.DrawToBitmap(completa, new Rectangle(Point.Empty, ventana.Size));
                Point origen = ventana.PointToScreen(Point.Empty);
                int bordeX = origen.X - ventana.Left, bordeY = origen.Y - ventana.Top;
                using (Graphics g = Graphics.FromImage(_foto))
                    g.DrawImage(completa, new Rectangle(Point.Empty, cliente), new Rectangle(bordeX, bordeY, cliente.Width, cliente.Height), GraphicsUnit.Pixel);
            }
            _fotoOscura = new Bitmap(_foto);
            using (Graphics g = Graphics.FromImage(_fotoOscura))
            using (var velo = new SolidBrush(Color.FromArgb(150, 46, 42, 59)))
                g.FillRectangle(velo, 0, 0, cliente.Width, cliente.Height);

            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Bounds = ventana.RectangleToScreen(ventana.ClientRectangle);
            DoubleBuffered = true;
            KeyPreview = true;
            BackColor = Tema.Texto;
            Font = new Font(Tema.Fuente, 10F);

            ArmarTarjeta();
            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape) Close();
                else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter) Avanzar(1);
                else if (e.KeyCode == Keys.Left) Avanzar(-1);
            };
            Shown += (s, e) => MostrarPaso();
        }

        private void ArmarTarjeta()
        {
            _tarjeta.BackColor = Color.White;
            _tarjeta.Width = AnchoTarjeta;
            _tarjeta.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var lapiz = new Pen(Tema.Principal, 2))
                    e.Graphics.DrawRectangle(lapiz, 1, 1, _tarjeta.Width - 3, _tarjeta.Height - 3);
            };

            _lblTitulo.Font = new Font(Tema.Fuente, 12F, FontStyle.Bold);
            _lblTitulo.ForeColor = Tema.PrincipalOscuro;
            _lblTitulo.Location = new Point(16, 14);
            _lblTitulo.MaximumSize = new Size(AnchoTarjeta - 100, 0);
            _lblTitulo.AutoSize = true;

            _lblContador.Font = new Font(Tema.Fuente, 9F);
            _lblContador.ForeColor = Tema.Acento;
            _lblContador.TextAlign = ContentAlignment.MiddleRight;
            _lblContador.Location = new Point(AnchoTarjeta - 86, 16);
            _lblContador.Size = new Size(70, 20);

            _lblTexto.ForeColor = Tema.Texto;
            _lblTexto.Location = new Point(16, 46);
            _lblTexto.MaximumSize = new Size(AnchoTarjeta - 32, 0);
            _lblTexto.AutoSize = true;

            foreach (var b in new[] { _btnSaltar, _btnAnterior, _btnSiguiente })
            {
                b.Font = new Font(Tema.Fuente, 9.5F);
                b.Size = new Size(100, 32);
                b.TabStop = false;
            }
            _btnSaltar.FlatStyle = FlatStyle.Flat;
            _btnSaltar.FlatAppearance.BorderSize = 0;
            _btnSaltar.FlatAppearance.MouseOverBackColor = Tema.PrincipalSuave;
            _btnSaltar.ForeColor = Tema.PrincipalOscuro;
            _btnSaltar.Cursor = Cursors.Hand;
            Tema.EstiloSecundario(_btnAnterior);
            Tema.EstiloPrincipal(_btnSiguiente);

            _btnSaltar.Click += (s, e) => Close();
            _btnAnterior.Click += (s, e) => Avanzar(-1);
            _btnSiguiente.Click += (s, e) => Avanzar(1);

            _tarjeta.Controls.AddRange(new Control[] { _lblTitulo, _lblContador, _lblTexto, _btnSaltar, _btnAnterior, _btnSiguiente });
            Controls.Add(_tarjeta);
        }

        private static string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("VisitaGuiada", clave);
        }

        private void Avanzar(int cuanto)
        {
            int nuevo = _actual + cuanto;
            if (nuevo >= _pasos.Count)
            {
                Close();
                return;
            }
            if (nuevo < 0) return;
            _actual = nuevo;
            MostrarPaso();
        }

        private void MostrarPaso()
        {
            Paso paso = _pasos[_actual];
            Rectangle zona = paso.Zona?.Invoke() ?? Rectangle.Empty;
            _zona = zona.IsEmpty ? Rectangle.Empty : Rectangle.Intersect(Rectangle.Inflate(zona, Relleno, Relleno), ClientRectangle);

            _lblTitulo.Text = T("paso" + paso.Clave + "Titulo");
            _lblContador.Text = string.Format(T("contador"), _actual + 1, _pasos.Count);
            _lblTexto.Text = T("paso" + paso.Clave + "Texto");
            _btnSaltar.Text = T("saltar");
            _btnAnterior.Text = T("anterior");
            _btnSiguiente.Text = _actual == _pasos.Count - 1 ? T("finalizar") : T("siguiente");
            _btnAnterior.Visible = _actual > 0;
            _btnSaltar.Visible = _actual < _pasos.Count - 1;

            _lblTexto.Top = Math.Max(46, _lblTitulo.Bottom + 8);
            int yBotones = _lblTexto.Bottom + 16;
            _btnSiguiente.Location = new Point(AnchoTarjeta - 16 - _btnSiguiente.Width, yBotones);
            _btnAnterior.Location = new Point(_btnSiguiente.Left - 8 - _btnAnterior.Width, yBotones);
            _btnSaltar.Location = new Point(16, yBotones);
            _tarjeta.Height = yBotones + _btnSiguiente.Height + 14;
            _tarjeta.Location = UbicarTarjeta(_zona, _tarjeta.Size);
            Invalidate();
            _tarjeta.Invalidate(true);
            _btnSiguiente.Focus();
        }

        // Al lado de la zona resaltada: a la derecha, si no entra abajo, si no a la izquierda; sin zona, al centro
        private Point UbicarTarjeta(Rectangle zona, Size tam)
        {
            Rectangle area = ClientRectangle;
            if (zona.IsEmpty)
                return new Point((area.Width - tam.Width) / 2, (area.Height - tam.Height) / 2);
            Func<int, int, Point> dentro = (x, y) => new Point(
                Math.Max(Separacion, Math.Min(x, area.Width - tam.Width - Separacion)),
                Math.Max(Separacion, Math.Min(y, area.Height - tam.Height - Separacion)));
            if (zona.Right + Separacion + tam.Width <= area.Width)
                return dentro(zona.Right + Separacion, zona.Top);
            int abajo = Math.Min(zona.Bottom + Separacion, area.Height - tam.Height - Separacion);
            if (abajo >= zona.Top + zona.Height * 2 / 3)
                return dentro(zona.Left + Separacion, abajo);
            if (zona.Top - Separacion - tam.Height >= 0)
                return dentro(zona.Left, zona.Top - Separacion - tam.Height);
            if (zona.Left - Separacion - tam.Width >= 0)
                return dentro(zona.Left - Separacion - tam.Width, zona.Top);
            return dentro(zona.Left + Separacion, zona.Bottom - tam.Height - Separacion);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(_fotoOscura, 0, 0);
            if (_zona.IsEmpty) return;
            g.DrawImage(_foto, _zona, _zona, GraphicsUnit.Pixel);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var lapiz = new Pen(Tema.Acento, 3))
            using (GraphicsPath forma = Tema.RectanguloRedondeado(new RectangleF(_zona.X + 1.5f, _zona.Y + 1.5f, _zona.Width - 3, _zona.Height - 3), 8))
                g.DrawPath(lapiz, forma);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _foto.Dispose();
                _fotoOscura.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
