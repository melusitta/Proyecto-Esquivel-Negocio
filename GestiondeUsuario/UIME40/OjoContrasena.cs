using Servicios;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // Botón con un ojo dentro de una caja de contraseña: al tocarlo se ve lo que se escribió,
    // al tocarlo de nuevo se vuelve a ocultar. Uso: OjoContrasena.Agregar(txtContraseña);
    // (llamarlo después de Tema.Aplicar)
    public class OjoContrasena : Button
    {
        public const string Marca = "ojo";   // Tema no le aplica el estilo de botón común
        private const int Ancho = 26;

        private readonly TextBox _caja;
        private readonly char _caracterOriginal;
        private readonly bool _sistemaOriginal;
        private readonly ToolTip _ayuda = new ToolTip();
        private bool _visible;

        private OjoContrasena(TextBox caja)
        {
            _caja = caja;
            _caracterOriginal = caja.PasswordChar == '\0' ? '•' : caja.PasswordChar;
            _sistemaOriginal = caja.UseSystemPasswordChar;

            Tag = Marca;
            Dock = DockStyle.Right;
            Width = Ancho;
            TabStop = false;           // no cambia el orden de tabulación del formulario
            Cursor = Cursors.Hand;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Color.White;
            Paint += Dibujar;
            Click += (s, e) => Alternar();
        }

        public static void Agregar(TextBox caja)
        {
            var ojo = new OjoContrasena(caja);
            caja.Controls.Add(ojo);
            // Que el texto no quede escrito debajo del ojo
            ojo.ReservarMargen();
            caja.HandleCreated += (s, e) => ojo.ReservarMargen();
            caja.FontChanged += (s, e) => ojo.ReservarMargen();
            ojo.ActualizarAyuda();
        }

        private void Alternar()
        {
            _visible = !_visible;
            _caja.UseSystemPasswordChar = !_visible && _sistemaOriginal;
            _caja.PasswordChar = _visible ? '\0' : _caracterOriginal;
            ActualizarAyuda();
            Invalidate();
            _caja.Focus();
            _caja.SelectionStart = _caja.TextLength;   // el cursor queda al final para seguir escribiendo
        }

        private void ActualizarAyuda()
        {
            _ayuda.SetToolTip(this, GestorIdioma.Instancia.Obtener("OjoContrasena", _visible ? "ocultar" : "mostrar"));
        }

        // ---------- Dibujo del ícono ----------

        private void Dibujar(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            bool encima = ClientRectangle.Contains(PointToClient(Cursor.Position));
            g.Clear(encima ? Tema.PrincipalSuave : _caja.BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            float cx = Width / 2f, cy = Height / 2f;
            float w = 16, h = 10;
            using (var lapiz = new Pen(Tema.PrincipalOscuro, 1.6f))
            using (var relleno = new SolidBrush(Tema.PrincipalOscuro))
            using (var ojo = new GraphicsPath())
            {
                // Contorno del ojo: dos curvas que se juntan en las puntas
                ojo.AddBezier(cx - w / 2, cy, cx - w / 4, cy - h / 1.4f, cx + w / 4, cy - h / 1.4f, cx + w / 2, cy);
                ojo.AddBezier(cx + w / 2, cy, cx + w / 4, cy + h / 1.4f, cx - w / 4, cy + h / 1.4f, cx - w / 2, cy);
                g.DrawPath(lapiz, ojo);
                g.FillEllipse(relleno, cx - 2.6f, cy - 2.6f, 5.2f, 5.2f);

                // Contraseña visible: el ojo aparece tachado (tocar para ocultar)
                if (_visible)
                {
                    using (var borde = new Pen(_caja.BackColor, 3.5f))
                        g.DrawLine(borde, cx - w / 2 + 1, cy + h / 2 + 1, cx + w / 2 - 1, cy - h / 2 - 1);
                    g.DrawLine(lapiz, cx - w / 2 + 1, cy + h / 2 + 1, cx + w / 2 - 1, cy - h / 2 - 1);
                }
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            ActualizarAyuda();   // en el idioma que esté activo ahora
            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); Invalidate(); }

        // ---------- Margen derecho del TextBox (mensaje de Windows EM_SETMARGINS) ----------

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private const int EM_SETMARGINS = 0xD3;
        private const int EC_RIGHTMARGIN = 0x2;

        private void ReservarMargen()
        {
            if (_caja.IsHandleCreated)
                SendMessage(_caja.Handle, EM_SETMARGINS, (IntPtr)EC_RIGHTMARGIN, (IntPtr)((Ancho + 2) << 16));
        }
    }
}
