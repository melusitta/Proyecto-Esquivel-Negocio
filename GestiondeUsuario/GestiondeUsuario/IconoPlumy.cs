using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // Íconos chiquitos dibujados con los colores de Plumy (sin imágenes externas).
    // Uso: IconoPlumy.Agregar(lblUsuario, IconoPlumy.Tipo.Usuario);
    // Pone el ícono a la izquierda de la etiqueta y corre la etiqueta para hacerle lugar.
    public class IconoPlumy : Control
    {
        public enum Tipo { Usuario, Candado }

        private const int Tam = 18;
        private const int Separacion = 5;
        private readonly Tipo _tipo;

        private IconoPlumy(Tipo tipo)
        {
            _tipo = tipo;
            Size = new Size(Tam, Tam);
            TabStop = false;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        public static void Agregar(Label etiqueta, Tipo tipo)
        {
            var icono = new IconoPlumy(tipo);
            icono.Anchor = etiqueta.Anchor;   // se acomoda igual que la etiqueta al agrandar la ventana
            icono.Location = new Point(etiqueta.Left, etiqueta.Top + (etiqueta.Height - Tam) / 2);
            etiqueta.Left += Tam + Separacion;
            etiqueta.Parent.Controls.Add(icono);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Parent?.BackColor ?? Tema.Fondo);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var relleno = new SolidBrush(Tema.PrincipalOscuro))
            {
                if (_tipo == Tipo.Usuario)
                    DibujarUsuario(g, relleno);
                else
                    DibujarCandado(g, relleno);
            }
        }

        // Cabeza redonda y hombros
        private static void DibujarUsuario(Graphics g, Brush relleno)
        {
            g.FillEllipse(relleno, 5.5f, 1.5f, 7f, 7f);
            using (var hombros = new GraphicsPath())
            {
                hombros.AddArc(2f, 10f, 14f, 12f, 180, 180);
                hombros.CloseFigure();
                g.FillPath(relleno, hombros);
            }
        }

        // Arco del candado, cuerpo con esquinas redondeadas y cerradura
        private static void DibujarCandado(Graphics g, Brush relleno)
        {
            using (var arco = new Pen(Tema.PrincipalOscuro, 2f))
            {
                g.DrawArc(arco, 5f, 1.5f, 8f, 10f, 180, 180);
                g.DrawLine(arco, 5f, 6.5f, 5f, 8.5f);
                g.DrawLine(arco, 13f, 6.5f, 13f, 8.5f);
            }
            using (var cuerpo = new GraphicsPath())
            {
                var r = new RectangleF(2.5f, 8f, 13f, 9f);
                float d = 3f;
                cuerpo.AddArc(r.X, r.Y, d, d, 180, 90);
                cuerpo.AddArc(r.Right - d, r.Y, d, d, 270, 90);
                cuerpo.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
                cuerpo.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
                cuerpo.CloseFigure();
                g.FillPath(relleno, cuerpo);
            }
            using (var hueco = new SolidBrush(Color.White))
            {
                g.FillEllipse(hueco, 7.6f, 10.5f, 2.8f, 2.8f);
                g.FillRectangle(hueco, 8.4f, 12.5f, 1.2f, 2.6f);
            }
        }
    }
}
