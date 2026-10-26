using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.CompilerServices;
using Servicios;
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
        // Fila de la grilla que está bajo el mouse
        public static readonly Color FilaEncima = ColorTranslator.FromHtml("#EFEAF8");

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

        private static Image _mascota;

        // Muñequito de Plumy (con fondo transparente) desde Recursos\mascota_plumy.png. Devuelve null si no está.
        public static Image Mascota
        {
            get
            {
                if (_mascota == null)
                {
                    string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Recursos", "mascota_plumy.png");
                    if (File.Exists(ruta))
                        _mascota = Image.FromFile(ruta);
                }
                return _mascota;
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
            RecordarMaximizado(form);
            AplicarControles(form.Controls);
        }

        // ---------- Ventanas maximizadas ----------
        // Si la usuaria maximiza una pantalla, las siguientes que se abran también arrancan maximizadas,
        // hasta que la restaure. Minimizar no cambia nada. Los diálogos (ShowDialog) no se tocan.
        private static bool _maximizado;

        private static void RecordarMaximizado(Form form)
        {
            if (form.Modal) return;
            if (_maximizado)
                form.WindowState = FormWindowState.Maximized;
            form.Resize += (s, e) =>
            {
                if (form.WindowState == FormWindowState.Maximized)
                    _maximizado = true;
                else if (form.WindowState == FormWindowState.Normal)
                    _maximizado = false;
            };
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

        // ---------- Botones redondeados y vivos ----------
        // WinForms no tiene botones redondeados: los dibujamos nosotros en el Paint,
        // tomando los colores que dejaron EstiloPrincipal/EstiloSecundario.
        // Al pasar el mouse el color cambia de a poco (un timer avanza la animación) y al hacer clic
        // el botón se "hunde" un poquito.
        public const int RadioBoton = 10;
        private static readonly ConditionalWeakTable<Button, EstadoBoton> _redondeados = new ConditionalWeakTable<Button, EstadoBoton>();

        private class EstadoBoton
        {
            public float Encima;        // 0 = normal, 1 = color de hover del todo
            public bool MouseEncima;
            public bool Presionado;
        }

        private static readonly System.Collections.Generic.List<Button> _animando = new System.Collections.Generic.List<Button>();
        private static Timer _timerBotones;
        private const float PasoAnimacion = 0.18f;   // ~6 cuadros de 15 ms

        private static void Redondear(Button btn)
        {
            EstadoBoton estado;
            if (_redondeados.TryGetValue(btn, out estado)) return; // ya tiene el Paint enganchado
            estado = new EstadoBoton();
            _redondeados.Add(btn, estado);
            btn.Paint += PintarBotonRedondeado;
            btn.MouseEnter += (s, e) => { estado.MouseEncima = true; Animar(btn); };
            btn.MouseLeave += (s, e) => { estado.MouseEncima = false; estado.Presionado = false; Animar(btn); };
            btn.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) { estado.Presionado = true; btn.Invalidate(); } };
            btn.MouseUp += (s, e) => { estado.Presionado = false; btn.Invalidate(); };
            btn.EnabledChanged += (s, e) => { estado.Encima = 0; estado.Presionado = false; btn.Invalidate(); };
        }

        private static void Animar(Button btn)
        {
            if (!_animando.Contains(btn))
                _animando.Add(btn);
            if (_timerBotones == null)
            {
                _timerBotones = new Timer { Interval = 15 };
                _timerBotones.Tick += AvanzarAnimacion;
            }
            _timerBotones.Start();
        }

        private static void AvanzarAnimacion(object sender, EventArgs e)
        {
            for (int i = _animando.Count - 1; i >= 0; i--)
            {
                Button btn = _animando[i];
                EstadoBoton estado;
                if (btn.IsDisposed || !_redondeados.TryGetValue(btn, out estado))
                {
                    _animando.RemoveAt(i);
                    continue;
                }
                float objetivo = estado.MouseEncima && btn.Enabled ? 1f : 0f;
                estado.Encima = objetivo > estado.Encima ? Math.Min(objetivo, estado.Encima + PasoAnimacion)
                                                         : Math.Max(objetivo, estado.Encima - PasoAnimacion);
                btn.Invalidate();
                if (estado.Encima == objetivo)
                    _animando.RemoveAt(i);
            }
            if (_animando.Count == 0)
                _timerBotones.Stop();
        }

        private static void PintarBotonRedondeado(object sender, PaintEventArgs e)
        {
            var btn = (Button)sender;
            Graphics g = e.Graphics;
            Color fondo = btn.Parent?.BackColor ?? Fondo;

            EstadoBoton estado;
            _redondeados.TryGetValue(btn, out estado);
            float encima = btn.Enabled ? estado?.Encima ?? 0f : 0f;
            bool presionado = btn.Enabled && estado != null && estado.Presionado;

            Color relleno = presionado ? btn.FlatAppearance.MouseDownBackColor
                          : Mezclar(btn.BackColor, btn.FlatAppearance.MouseOverBackColor, encima);
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
            // Presionado: un poquito más chico y el texto un pixel más abajo
            float hundido = presionado ? 1.5f : 0f;
            var rect = new RectangleF(0.5f + hundido, 0.5f + hundido, btn.Width - 1.5f - 2 * hundido, btn.Height - 1.5f - 2 * hundido);
            using (GraphicsPath forma = RectanguloRedondeado(rect, RadioBoton))
            using (var pincel = new SolidBrush(relleno))
            using (var lapiz = new Pen(borde, btn.Focused ? 2f : 1f))
            {
                g.FillPath(pincel, forma);
                g.DrawPath(lapiz, forma);
            }

            Rectangle areaTexto = btn.ClientRectangle;
            if (presionado)
                areaTexto.Offset(0, 1);
            TextRenderer.DrawText(g, btn.Text, btn.Font, areaTexto, texto,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
        }

        public static GraphicsPath RectanguloRedondeado(RectangleF r, float radio)
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
        public static Color Mezclar(Color a, Color b, double proporcion)
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
            AnimarGrilla(dgv);
        }

        // ---------- Grillas vivas ----------
        // La fila bajo el mouse se pinta lila suave, y una grilla vacía muestra un mensajito en el medio
        // (el de MensajeVacio, o uno general) en vez de quedar en blanco.
        private static readonly ConditionalWeakTable<DataGridView, Func<string>> _mensajesVacio = new ConditionalWeakTable<DataGridView, Func<string>>();

        public static void MensajeVacio(DataGridView dgv, Func<string> texto)
        {
            _mensajesVacio.Remove(dgv);
            _mensajesVacio.Add(dgv, texto);
            dgv.Invalidate();
        }

        private static void AnimarGrilla(DataGridView dgv)
        {
            Func<string> yaEnganchada;
            if (_mensajesVacio.TryGetValue(dgv, out yaEnganchada)) return;
            _mensajesVacio.Add(dgv, () => GestorIdioma.Instancia.Obtener("Tema", "grillaVacia"));

            dgv.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgv.Rows.Count)
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = FilaEncima;
            };
            dgv.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgv.Rows.Count)
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Empty;
            };
            dgv.RowsAdded += (s, e) => dgv.Invalidate();
            dgv.RowsRemoved += (s, e) => dgv.Invalidate();
            dgv.DataBindingComplete += (s, e) => dgv.Invalidate();
            dgv.Paint += PintarGrillaVacia;
        }

        private static void PintarGrillaVacia(object sender, PaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            Func<string> texto;
            if (dgv.Rows.Count > 0 || dgv.Columns.Count == 0 || !_mensajesVacio.TryGetValue(dgv, out texto))
                return;
            string mensaje = texto();
            if (string.IsNullOrEmpty(mensaje))
                return;
            int arriba = dgv.ColumnHeadersVisible ? dgv.ColumnHeadersHeight : 0;
            var area = new Rectangle(10, arriba, dgv.ClientSize.Width - 20, dgv.ClientSize.Height - arriba);
            using (var fuente = new Font(Fuente, 11F))
                TextRenderer.DrawText(e.Graphics, mensaje, fuente, area, PrincipalOscuro,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
        }
    }
}
