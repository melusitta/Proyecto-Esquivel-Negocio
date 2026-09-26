using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // Paleta y estilos visuales de Plumy. Cada formulario llama a Tema.Aplicar(this) en su Load.
    // Convenciones por nombre de control:
    //   lblTitulo                         -> estilo de título
    //   btnAplicar / btnLogin / btnAceptar -> botón principal (relleno lila)
    public static class Tema
    {
        public static readonly Color Fondo = ColorTranslator.FromHtml("#FFF8F0");
        // Lila de la marca (mismo tono que el fondo del logo): paneles y encabezados, siempre con texto oscuro
        public static readonly Color Principal = ColorTranslator.FromHtml("#989ACB");
        // Versión oscura del lila para texto y botones: el blanco sobre #989ACB casi no se lee
        public static readonly Color PrincipalOscuro = ColorTranslator.FromHtml("#6E70A8");
        public static readonly Color PrincipalHover = ColorTranslator.FromHtml("#5D5F96");
        public static readonly Color PrincipalSuave = ColorTranslator.FromHtml("#ECECF6");
        public static readonly Color Acento = ColorTranslator.FromHtml("#E87BB1");
        public static readonly Color AcentoSuave = ColorTranslator.FromHtml("#FCE4EE");
        public static readonly Color Texto = ColorTranslator.FromHtml("#2E2A3B");
        public static readonly Color Borde = ColorTranslator.FromHtml("#D6D7EA");
        public static readonly Color FilaAlterna = ColorTranslator.FromHtml("#F7F7FC");

        public const string Fuente = "Segoe UI";

        private static readonly string[] BotonesPrincipales = { "btnAplicar", "btnLogin", "btnAceptar" };

        private static Image _logo;

        // Logo desde Recursos\logo_plumy.png (se copia a la carpeta de salida). Devuelve null si no está.
        public static Image Logo
        {
            get
            {
                if (_logo == null)
                {
                    string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Recursos", "logo_plumy.png");
                    if (File.Exists(ruta))
                        _logo = Image.FromFile(ruta);
                }
                return _logo;
            }
        }

        private static Icon _icono;

        // Ícono de las ventanas: cara del pony, desde Recursos\plumy.ico. Devuelve null si no está.
        public static Icon Icono
        {
            get
            {
                if (_icono == null)
                {
                    string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Recursos", "plumy.ico");
                    if (File.Exists(ruta))
                        _icono = new Icon(ruta);
                }
                return _icono;
            }
        }

        public static void Aplicar(Form form)
        {
            if (Icono != null)
                form.Icon = Icono;
            form.BackColor = Fondo;
            form.ForeColor = Texto;
            form.StartPosition = FormStartPosition.CenterScreen;
            // Se puede agrandar y maximizar, pero no achicar por debajo del tamaño de diseño.
            // Cómo se acomoda cada control al agrandar lo define su Anchor en el diseñador.
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.MaximizeBox = true;
            form.MinimumSize = form.Size;
            AplicarControles(form.Controls);
        }

        private static void AplicarControles(Control.ControlCollection controles)
        {
            foreach (Control c in controles)
            {
                // El ojo de las contraseñas se dibuja solo (ver OjoContrasena)
                if (Equals(c.Tag, OjoContrasena.Marca))
                    continue;

                // Unificamos la familia de fuente respetando el tamaño y estilo del diseñador
                c.Font = new Font(Fuente, c.Font.Size, c.Font.Style);

                if (c is Button btn)
                {
                    if (System.Array.IndexOf(BotonesPrincipales, btn.Name) >= 0)
                        EstiloPrincipal(btn);
                    else
                        EstiloSecundario(btn);
                }
                else if (c is DataGridView dgv)
                    EstiloGrilla(dgv);
                else if (c is TextBox txt)
                {
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.BackColor = Color.White;
                    txt.ForeColor = Texto;
                }
                else if (c is ListBox lst)
                {
                    lst.BorderStyle = BorderStyle.FixedSingle;
                    lst.BackColor = Color.White;
                    lst.ForeColor = Texto;
                }
                else if (c is GroupBox gb)
                    gb.ForeColor = PrincipalOscuro;
                else if (c is Label lbl)
                {
                    lbl.BackColor = Color.Transparent;
                    if (lbl.Name == "lblTitulo")
                        EstiloTitulo(lbl);
                    else
                        lbl.ForeColor = Texto;
                }
                else if (c is RadioButton || c is CheckBox)
                    c.ForeColor = Texto;

                if (c.HasChildren)
                    AplicarControles(c.Controls);
            }
        }

        public static void EstiloTitulo(Label lbl)
        {
            lbl.Font = new Font(Fuente, 16F, FontStyle.Bold);
            lbl.ForeColor = PrincipalOscuro;
        }

        public static void EstiloPrincipal(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.UseVisualStyleBackColor = false;
            btn.BackColor = PrincipalOscuro;
            btn.ForeColor = Color.White;
            btn.FlatAppearance.BorderColor = PrincipalOscuro;
            btn.FlatAppearance.MouseOverBackColor = PrincipalHover;
            btn.FlatAppearance.MouseDownBackColor = PrincipalHover;
            btn.Font = new Font(Fuente, btn.Font.Size, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            Redondear(btn);
        }

        public static void EstiloSecundario(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.UseVisualStyleBackColor = false;
            btn.BackColor = Color.White;
            btn.ForeColor = PrincipalOscuro;
            btn.FlatAppearance.BorderColor = PrincipalOscuro;
            btn.FlatAppearance.MouseOverBackColor = PrincipalSuave;
            btn.FlatAppearance.MouseDownBackColor = Borde;
            btn.Cursor = Cursors.Hand;
            Redondear(btn);
        }

        // ---------- Botones redondeados ----------
        // WinForms no tiene botones redondeados: los dibujamos nosotros en el Paint,
        // tomando los colores que dejaron EstiloPrincipal/EstiloSecundario.
        public const int RadioBoton = 10;
        private static readonly ConditionalWeakTable<Button, object> _redondeados = new ConditionalWeakTable<Button, object>();

        private static void Redondear(Button btn)
        {
            object marca;
            if (_redondeados.TryGetValue(btn, out marca)) return; // ya tiene el Paint enganchado
            _redondeados.Add(btn, null);
            btn.Paint += PintarBotonRedondeado;
        }

        private static void PintarBotonRedondeado(object sender, PaintEventArgs e)
        {
            var btn = (Button)sender;
            Graphics g = e.Graphics;
            Color fondo = btn.Parent?.BackColor ?? Fondo;

            bool encima = btn.Enabled && btn.ClientRectangle.Contains(btn.PointToClient(Cursor.Position));
            bool presionado = encima && (Control.MouseButtons & MouseButtons.Left) != 0;

            Color relleno = presionado ? btn.FlatAppearance.MouseDownBackColor
                          : encima ? btn.FlatAppearance.MouseOverBackColor
                          : btn.BackColor;
            Color borde = btn.FlatAppearance.BorderColor;
            Color texto = btn.ForeColor;
            if (!btn.Enabled)
            {
                relleno = Mezclar(relleno, fondo, 0.45);
                borde = Mezclar(borde, fondo, 0.55);
                texto = Mezclar(texto, fondo, 0.5);
            }

            g.Clear(fondo);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new RectangleF(0.5f, 0.5f, btn.Width - 1.5f, btn.Height - 1.5f);
            using (GraphicsPath forma = RectanguloRedondeado(rect, RadioBoton))
            using (var pincel = new SolidBrush(relleno))
            using (var lapiz = new Pen(borde, btn.Focused ? 2f : 1f))
            {
                g.FillPath(pincel, forma);
                g.DrawPath(lapiz, forma);
            }

            TextRenderer.DrawText(g, btn.Text, btn.Font, btn.ClientRectangle, texto,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
        }

        private static GraphicsPath RectanguloRedondeado(RectangleF r, float radio)
        {
            float d = radio * 2;
            var forma = new GraphicsPath();
            forma.AddArc(r.X, r.Y, d, d, 180, 90);
            forma.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            forma.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            forma.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            forma.CloseFigure();
            return forma;
        }

        // Mezcla el color a con b (proporcion = cuánto de b)
        private static Color Mezclar(Color a, Color b, double proporcion)
        {
            return Color.FromArgb(
                (int)(a.R + (b.R - a.R) * proporcion),
                (int)(a.G + (b.G - a.G) * proporcion),
                (int)(a.B + (b.B - a.B) * proporcion));
        }

        // Menú lateral del FormPrincipal: fondo lila, ítems a todo el ancho y submenús blancos
        public static void EstiloMenu(MenuStrip menu)
        {
            menu.BackColor = Principal;
            menu.Renderer = new RendererPlumy();
            menu.Padding = new Padding(10, 20, 10, 0);
            int ancho = menu.Width - menu.Padding.Horizontal;

            foreach (ToolStripItem item in menu.Items)
            {
                item.Font = new Font(Fuente, 11F, FontStyle.Bold);
                item.AutoSize = false;
                item.Size = new Size(ancho, 34);
                item.TextAlign = ContentAlignment.MiddleLeft;
                item.Padding = new Padding(8, 0, 0, 0);

                if (item is ToolStripMenuItem menuItem)
                    menuItem.DropDown.Font = new Font(Fuente, 10F);
            }
        }

        private class RendererPlumy : ToolStripProfessionalRenderer
        {
            public RendererPlumy() : base(new ColoresPlumy())
            {
                RoundedEdges = false;
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                bool enBarra = e.Item.Owner is MenuStrip;
                if (e.Item.Enabled)
                    e.TextColor = Texto;
                else
                    e.TextColor = enBarra ? ColorTranslator.FromHtml("#E4E4F1") : ColorTranslator.FromHtml("#A9AAC8");
                base.OnRenderItemText(e);
            }
        }

        private class ColoresPlumy : ProfessionalColorTable
        {
            public override Color MenuStripGradientBegin => Principal;
            public override Color MenuStripGradientEnd => Principal;
            public override Color MenuItemSelected => PrincipalSuave;
            public override Color MenuItemSelectedGradientBegin => PrincipalSuave;
            public override Color MenuItemSelectedGradientEnd => PrincipalSuave;
            public override Color MenuItemPressedGradientBegin => PrincipalSuave;
            public override Color MenuItemPressedGradientMiddle => PrincipalSuave;
            public override Color MenuItemPressedGradientEnd => PrincipalSuave;
            public override Color MenuItemBorder => PrincipalOscuro;
            public override Color MenuBorder => Borde;
            public override Color ToolStripDropDownBackground => Color.White;
            public override Color ImageMarginGradientBegin => Color.White;
            public override Color ImageMarginGradientMiddle => Color.White;
            public override Color ImageMarginGradientEnd => Color.White;
            public override Color SeparatorDark => Borde;
        }

        // Reparte el ancho de la grilla según estos pesos (más peso = columna más ancha)
        public static void AnchoColumnas(DataGridView dgv, params (string columna, float peso)[] pesos)
        {
            foreach (var p in pesos)
                if (dgv.Columns.Contains(p.columna))
                    dgv.Columns[p.columna].FillWeight = p.peso;
        }

        public static void EstiloGrilla(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.GridColor = Borde;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.RowHeadersVisible = false;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 32;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Principal;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Texto;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Principal;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(Fuente, 9.75F, FontStyle.Bold);

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Texto;
            dgv.DefaultCellStyle.SelectionBackColor = AcentoSuave;
            dgv.DefaultCellStyle.SelectionForeColor = Texto;
            dgv.DefaultCellStyle.Font = new Font(Fuente, 9.75F);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = FilaAlterna;
            dgv.RowTemplate.Height = 28;
        }
    }
}
