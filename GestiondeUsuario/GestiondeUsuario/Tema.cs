using System;
using System.Drawing;
using System.IO;
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

        public static void Aplicar(Form form)
        {
            form.BackColor = Fondo;
            form.ForeColor = Texto;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.MaximizeBox = false;
            AplicarControles(form.Controls);
        }

        private static void AplicarControles(Control.ControlCollection controles)
        {
            foreach (Control c in controles)
            {
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
