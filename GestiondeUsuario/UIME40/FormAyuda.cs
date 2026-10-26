using Newtonsoft.Json.Linq;
using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestiondeUsuario
{
    // Ayuda: manual en pantalla con los temas que el usuario puede usar (los mismos permisos que el menú),
    // agrupados como el menú, con buscador. Se abre desde el menú Ayuda, o con F1 / el botón "?" de cualquier
    // pantalla, directamente en el tema de esa pantalla. Es una sola ventana: si ya está abierta se reutiliza.
    // Formato de los textos (sección "Ayuda" del idioma): "## " subtítulo, "- " viñeta, "1. " paso, "💡 " consejo.
    public partial class FormAyuda : Form, IObservadorIdioma
    {
        private static FormAyuda _abierta;
        private List<TemaAyuda> _temas = new List<TemaAyuda>();
        private string _temaPedido;

        public FormAyuda()
        {
            InitializeComponent();
        }

        // Abre la ayuda (o la trae al frente) en el tema de esa pantalla, si tiene uno
        public static void Abrir(string formulario = null)
        {
            if (_abierta == null || _abierta.IsDisposed)
            {
                _abierta = new FormAyuda { _temaPedido = TemasAyuda.DeFormulario(formulario ?? "")?.Id };
                _abierta.Show();
                return;
            }
            if (_abierta.WindowState == FormWindowState.Minimized)
                _abierta.WindowState = FormWindowState.Normal;
            _abierta.Activate();
            _abierta.Seleccionar(TemasAyuda.DeFormulario(formulario ?? "")?.Id);
        }

        private void FormAyuda_Load(object sender, EventArgs e)
        {
            Tema.Aplicar(this);
            rtbContenido.BackColor = Color.White;
            tvTemas.BackColor = Tema.PrincipalSuave;
            tvTemas.ForeColor = Tema.Texto;
            GestorIdioma.Instancia.Suscribir(this);
            GestorIdioma.Instancia.CambiarIdioma(SessionManager.Instancia.ObtenerIdioma());
            txtBuscar.Focus();
        }

        private void FormAyuda_FormClosed(object sender, FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
            _abierta = null;
        }

        private string T(string clave)
        {
            return GestorIdioma.Instancia.Obtener("FormAyuda", clave);
        }

        // ---------- Índice ----------

        // El tema de la pantalla desde la que se pidió ayuda se muestra aunque sea de otro permiso
        private void CargarTemas()
        {
            _temas = TemasAyuda.Disponibles();
            TemaAyuda pedido = TemasAyuda.Todos.FirstOrDefault(t => t.Id == _temaPedido);
            if (pedido != null && !_temas.Contains(pedido))
                _temas.Add(pedido);
            MostrarIndice();
        }

        private void MostrarIndice()
        {
            string buscado = Normalizar(txtBuscar.Text);
            string actual = (tvTemas.SelectedNode?.Tag as TemaAyuda)?.Id ?? _temaPedido;
            tvTemas.BeginUpdate();
            tvTemas.Nodes.Clear();
            foreach (string grupo in TemasAyuda.Grupos)
            {
                var temas = _temas.Where(t => t.Grupo == grupo && Coincide(t, buscado)).ToList();
                if (temas.Count == 0) continue;
                TreeNode nodoGrupo = tvTemas.Nodes.Add(TemasAyuda.NombreGrupo(grupo));
                nodoGrupo.NodeFont = new Font(Tema.Fuente, 10.5F, FontStyle.Bold);
                nodoGrupo.ForeColor = Tema.PrincipalOscuro;
                foreach (TemaAyuda t in temas)
                    nodoGrupo.Nodes.Add(new TreeNode("   " + TemasAyuda.Titulo(t)) { Tag = t });
            }
            tvTemas.ExpandAll();
            tvTemas.EndUpdate();

            if (tvTemas.Nodes.Count == 0)
            {
                MostrarSinResultados();
                return;
            }
            if (!Seleccionar(actual))
                tvTemas.SelectedNode = tvTemas.Nodes[0].Nodes[0];
        }

        private bool Seleccionar(string idTema)
        {
            if (idTema == null) return false;
            foreach (TreeNode grupo in tvTemas.Nodes)
                foreach (TreeNode nodo in grupo.Nodes)
                    if ((nodo.Tag as TemaAyuda)?.Id == idTema)
                    {
                        tvTemas.SelectedNode = nodo;
                        nodo.EnsureVisible();
                        return true;
                    }
            return false;
        }

        private void tvTemas_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var tema = e.Node.Tag as TemaAyuda;
            if (tema == null)
            {
                // Clic en un grupo: se abre su primer tema
                if (e.Node.Nodes.Count > 0)
                    tvTemas.SelectedNode = e.Node.Nodes[0];
                return;
            }
            MostrarTema(tema);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            MostrarIndice();
        }

        // Busca en el título y en el texto, sin importar mayúsculas ni tildes
        private static bool Coincide(TemaAyuda tema, string buscado)
        {
            if (buscado.Length == 0) return true;
            return Normalizar(TemasAyuda.Titulo(tema)).Contains(buscado)
                || TemasAyuda.Lineas(tema).Any(l => Normalizar(l).Contains(buscado));
        }

        private static string Normalizar(string texto)
        {
            var sb = new StringBuilder();
            foreach (char c in (texto ?? "").Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD))
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            return sb.ToString();
        }

        // ---------- Contenido ----------

        private void MostrarTema(TemaAyuda tema)
        {
            rtbContenido.Clear();
            Escribir(TemasAyuda.Titulo(tema) + "\n", new Font(Tema.Fuente, 16F, FontStyle.Bold), Tema.PrincipalOscuro, 0, 10);
            foreach (string linea in TemasAyuda.Lineas(tema))
            {
                if (linea.StartsWith("## "))
                    Escribir(linea.Substring(3) + "\n", new Font(Tema.Fuente, 12F, FontStyle.Bold), Tema.PrincipalOscuro, 0, 12);
                else if (linea.StartsWith("- "))
                    Escribir("•  " + linea.Substring(2) + "\n", new Font(Tema.Fuente, 10.5F), Tema.Texto, 18, 3, 12);
                else if (linea.StartsWith("💡"))
                    // El RichTextBox no dibuja emojis de color: el consejo va con una estrellita
                    Escribir("★ " + linea.Substring("💡".Length).Trim() + "\n", new Font(Tema.Fuente, 10.5F, FontStyle.Italic), Tema.Acento, 0, 8);
                else if (linea.Length > 2 && char.IsDigit(linea[0]) && linea.IndexOf(". ") > 0 && linea.IndexOf(". ") < 3)
                {
                    int punto = linea.IndexOf(". ");
                    Escribir(linea.Substring(0, punto + 1) + "  ", new Font(Tema.Fuente, 10.5F, FontStyle.Bold), Tema.Acento, 18, 4, 20);
                    Escribir(linea.Substring(punto + 2) + "\n", new Font(Tema.Fuente, 10.5F), Tema.Texto, -1, 0);
                }
                else
                    Escribir(linea + "\n", new Font(Tema.Fuente, 10.5F), Tema.Texto, 0, 6);
            }
            rtbContenido.SelectionStart = 0;
            rtbContenido.ScrollToCaret();
        }

        private void MostrarSinResultados()
        {
            rtbContenido.Clear();
            string mensaje = new string(T("sinResultados").Where(c => !char.IsSurrogate(c)).ToArray()).Trim();
            Escribir("\n" + mensaje, new Font(Tema.Fuente, 11F, FontStyle.Italic), Tema.PrincipalOscuro, 0, 0);
        }

        // Agrega texto con su formato. sangria -1 = seguir en el mismo párrafo; colgante = sangría de las líneas siguientes
        private void Escribir(string texto, Font fuente, Color color, int sangria, int espacioAntes, int colgante = 0)
        {
            rtbContenido.SelectionStart = rtbContenido.TextLength;
            rtbContenido.SelectionLength = 0;
            if (sangria >= 0)
            {
                rtbContenido.SelectionIndent = 14 + sangria;
                rtbContenido.SelectionHangingIndent = colgante;
                rtbContenido.SelectionRightIndent = 14;
                if (espacioAntes > 0 && rtbContenido.TextLength > 0)
                {
                    rtbContenido.SelectionFont = new Font(Tema.Fuente, Math.Max(1, espacioAntes * 0.6f));
                    rtbContenido.AppendText("\n");
                    rtbContenido.SelectionStart = rtbContenido.TextLength;
                }
            }
            rtbContenido.SelectionFont = fuente;
            rtbContenido.SelectionColor = color;
            rtbContenido.AppendText(texto);
        }

        // ---------- Botones ----------

        private void btnManualPdf_Click(object sender, EventArgs e)
        {
            ImpresionAyuda.GenerarManual();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ---------- Idioma ----------

        public void ActualizarIdioma(JObject traducciones)
        {
            var t = traducciones["FormAyuda"];
            if (t == null) return;

            this.Text = t["tituloForm"]?.ToString();
            lblTitulo.Text = t["tituloForm"]?.ToString();
            lblBuscar.Text = t["lblBuscar"]?.ToString();
            btnManualPdf.Text = t["btnManualPdf"]?.ToString();
            btnVolver.Text = t["btnVolver"]?.ToString();
            CargarTemas();
        }
    }
}
