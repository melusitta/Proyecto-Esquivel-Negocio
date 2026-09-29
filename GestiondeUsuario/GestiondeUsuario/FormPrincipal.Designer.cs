namespace GestiondeUsuario
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.pnlLateral = new System.Windows.Forms.Panel();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.usuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarContraseñaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarIdiomaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionDeUsuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionDePerfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitacoraEventosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionDeRolesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionRespaldoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMaestro = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionProductosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionClientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVenta = new System.Windows.Forms.ToolStripMenuItem();
            this.cargarCarritoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generarFacturaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCompras = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReporte = new System.Windows.Forms.ToolStripMenuItem();
            this.productosMasVendidosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAyuda = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBienvenida
            // 
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBienvenida.Location = new System.Drawing.Point(250, 50);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(57, 20);
            this.lblBienvenida.TabIndex = 2;
            this.lblBienvenida.Text = "label2";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usuarioToolStripMenuItem,
            this.menuAdmin,
            this.menuMaestro,
            this.menuVenta,
            this.menuCompras,
            this.menuReporte,
            this.menuAyuda});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(210, 270);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // usuarioToolStripMenuItem
            // 
            this.usuarioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iniciarSesionToolStripMenuItem,
            this.cambiarContraseñaToolStripMenuItem,
            this.cambiarIdiomaToolStripMenuItem,
            this.cerrarSesionToolStripMenuItem});
            this.usuarioToolStripMenuItem.Name = "usuarioToolStripMenuItem";
            this.usuarioToolStripMenuItem.Size = new System.Drawing.Size(60, 19);
            this.usuarioToolStripMenuItem.Text = "Usuario";
            this.usuarioToolStripMenuItem.Click += new System.EventHandler(this.usuarioToolStripMenuItem_Click);
            // 
            // iniciarSesionToolStripMenuItem
            // 
            this.iniciarSesionToolStripMenuItem.Name = "iniciarSesionToolStripMenuItem";
            this.iniciarSesionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.iniciarSesionToolStripMenuItem.Text = "Iniciar sesion";
            this.iniciarSesionToolStripMenuItem.Click += new System.EventHandler(this.iniciarSesionToolStripMenuItem_Click);
            // 
            // cambiarContraseñaToolStripMenuItem
            // 
            this.cambiarContraseñaToolStripMenuItem.Name = "cambiarContraseñaToolStripMenuItem";
            this.cambiarContraseñaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cambiarContraseñaToolStripMenuItem.Text = "Cambiar contraseña";
            this.cambiarContraseñaToolStripMenuItem.Click += new System.EventHandler(this.cambiarContraseñaToolStripMenuItem_Click);
            // 
            // cambiarIdiomaToolStripMenuItem
            // 
            this.cambiarIdiomaToolStripMenuItem.Name = "cambiarIdiomaToolStripMenuItem";
            this.cambiarIdiomaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cambiarIdiomaToolStripMenuItem.Text = "Cambiar idioma";
            this.cambiarIdiomaToolStripMenuItem.Click += new System.EventHandler(this.cambiarIdiomaToolStripMenuItem_Click);
            // 
            // cerrarSesionToolStripMenuItem
            // 
            this.cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            this.cerrarSesionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cerrarSesionToolStripMenuItem.Text = "Cerrar sesion";
            this.cerrarSesionToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesionToolStripMenuItem_Click);
            // 
            // menuAdmin
            // 
            this.menuAdmin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestionDeUsuariosToolStripMenuItem,
            this.gestionDePerfilesToolStripMenuItem,
            this.bitacoraEventosToolStripMenuItem,
            this.gestionDeRolesToolStripMenuItem,
            this.gestionRespaldoToolStripMenuItem1});
            this.menuAdmin.Name = "menuAdmin";
            this.menuAdmin.Size = new System.Drawing.Size(60, 19);
            this.menuAdmin.Text = "Admin";
            // 
            // gestionDeUsuariosToolStripMenuItem
            // 
            this.gestionDeUsuariosToolStripMenuItem.Name = "gestionDeUsuariosToolStripMenuItem";
            this.gestionDeUsuariosToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gestionDeUsuariosToolStripMenuItem.Text = "Gestion de usuarios";
            this.gestionDeUsuariosToolStripMenuItem.Click += new System.EventHandler(this.gestionDeUsuariosToolStripMenuItem_Click);
            // 
            // gestionDePerfilesToolStripMenuItem
            // 
            this.gestionDePerfilesToolStripMenuItem.Name = "gestionDePerfilesToolStripMenuItem";
            this.gestionDePerfilesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gestionDePerfilesToolStripMenuItem.Text = "Gestion de Familias";
            this.gestionDePerfilesToolStripMenuItem.Click += new System.EventHandler(this.gestionDePerfilesToolStripMenuItem_Click);
            // 
            // bitacoraEventosToolStripMenuItem
            // 
            this.bitacoraEventosToolStripMenuItem.Name = "bitacoraEventosToolStripMenuItem";
            this.bitacoraEventosToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bitacoraEventosToolStripMenuItem.Text = "Bitacora eventos";
            this.bitacoraEventosToolStripMenuItem.Click += new System.EventHandler(this.bitacoraEventosToolStripMenuItem_Click);
            // 
            // gestionDeRolesToolStripMenuItem
            // 
            this.gestionDeRolesToolStripMenuItem.Name = "gestionDeRolesToolStripMenuItem";
            this.gestionDeRolesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gestionDeRolesToolStripMenuItem.Text = "Gestion de Roles";
            this.gestionDeRolesToolStripMenuItem.Click += new System.EventHandler(this.gestionDeRolesToolStripMenuItem_Click);
            // 
            // gestionRespaldoToolStripMenuItem1
            // 
            this.gestionRespaldoToolStripMenuItem1.Name = "gestionRespaldoToolStripMenuItem1";
            this.gestionRespaldoToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.gestionRespaldoToolStripMenuItem1.Text = "Gestion Respaldo";
            this.gestionRespaldoToolStripMenuItem1.Click += new System.EventHandler(this.gestionRespaldoToolStripMenuItem1_Click);
            // 
            // menuMaestro
            // 
            this.menuMaestro.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestionProductosToolStripMenuItem,
            this.gestionClientesToolStripMenuItem});
            this.menuMaestro.Name = "menuMaestro";
            this.menuMaestro.Size = new System.Drawing.Size(60, 19);
            this.menuMaestro.Text = "Maestro";
            // 
            // gestionProductosToolStripMenuItem
            // 
            this.gestionProductosToolStripMenuItem.Name = "gestionProductosToolStripMenuItem";
            this.gestionProductosToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gestionProductosToolStripMenuItem.Text = "Gestión de productos";
            this.gestionProductosToolStripMenuItem.Click += new System.EventHandler(this.gestionProductosToolStripMenuItem_Click);
            // 
            // gestionClientesToolStripMenuItem
            // 
            this.gestionClientesToolStripMenuItem.Name = "gestionClientesToolStripMenuItem";
            this.gestionClientesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gestionClientesToolStripMenuItem.Text = "Gestión de clientes";
            this.gestionClientesToolStripMenuItem.Click += new System.EventHandler(this.gestionClientesToolStripMenuItem_Click);
            // 
            // menuVenta
            // 
            this.menuVenta.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cargarCarritoToolStripMenuItem,
            this.generarFacturaToolStripMenuItem});
            this.menuVenta.Name = "menuVenta";
            this.menuVenta.Size = new System.Drawing.Size(60, 19);
            this.menuVenta.Text = "Venta";
            // 
            // cargarCarritoToolStripMenuItem
            // 
            this.cargarCarritoToolStripMenuItem.Name = "cargarCarritoToolStripMenuItem";
            this.cargarCarritoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cargarCarritoToolStripMenuItem.Text = "Cargar carrito";
            this.cargarCarritoToolStripMenuItem.Click += new System.EventHandler(this.cargarCarritoToolStripMenuItem_Click);
            // 
            // generarFacturaToolStripMenuItem
            // 
            this.generarFacturaToolStripMenuItem.Name = "generarFacturaToolStripMenuItem";
            this.generarFacturaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.generarFacturaToolStripMenuItem.Text = "Generar factura";
            this.generarFacturaToolStripMenuItem.Click += new System.EventHandler(this.generarFacturaToolStripMenuItem_Click);
            // 
            // menuCompras
            // 
            this.menuCompras.Name = "menuCompras";
            this.menuCompras.Size = new System.Drawing.Size(60, 19);
            this.menuCompras.Text = "Compras";
            // 
            // productosMasVendidosToolStripMenuItem
            // 
            this.productosMasVendidosToolStripMenuItem.Name = "productosMasVendidosToolStripMenuItem";
            this.productosMasVendidosToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.productosMasVendidosToolStripMenuItem.Text = "Productos más vendidos";
            this.productosMasVendidosToolStripMenuItem.Click += new System.EventHandler(this.productosMasVendidosToolStripMenuItem_Click);
            // 
            // menuReporte
            // 
            this.menuReporte.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productosMasVendidosToolStripMenuItem});
            this.menuReporte.Name = "menuReporte";
            this.menuReporte.Size = new System.Drawing.Size(60, 19);
            this.menuReporte.Text = "Reporte";
            // 
            // menuAyuda
            // 
            this.menuAyuda.Name = "menuAyuda";
            this.menuAyuda.Size = new System.Drawing.Size(60, 19);
            this.menuAyuda.Text = "Ayuda";
            // 
            // pnlLateral
            // 
            this.pnlLateral.Controls.Add(this.menuStrip1);
            this.pnlLateral.Controls.Add(this.picLogo);
            this.pnlLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLateral.Location = new System.Drawing.Point(0, 0);
            this.pnlLateral.Size = new System.Drawing.Size(210, 480);
            this.pnlLateral.TabIndex = 5;
            this.pnlLateral.Name = "pnlLateral";
            // 
            // picLogo
            // 
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.picLogo.Location = new System.Drawing.Point(0, 270);
            this.picLogo.Size = new System.Drawing.Size(210, 210);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabStop = false;
            this.picLogo.Name = "picLogo";
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 480);
            this.Controls.Add(this.lblBienvenida);
            this.MainMenuStrip = this.menuStrip1;
            this.Controls.Add(this.pnlLateral);
            this.Name = "FormPrincipal";
            this.Text = "Plumy";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormPrincipal_FormClosed);
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem usuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iniciarSesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambiarContraseñaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambiarIdiomaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAdmin;
        private System.Windows.Forms.ToolStripMenuItem gestionDeUsuariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionDePerfilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitacoraEventosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuMaestro;
        private System.Windows.Forms.ToolStripMenuItem gestionProductosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionClientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuVenta;
        private System.Windows.Forms.ToolStripMenuItem cargarCarritoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generarFacturaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuCompras;
        private System.Windows.Forms.ToolStripMenuItem menuReporte;
        private System.Windows.Forms.ToolStripMenuItem productosMasVendidosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAyuda;
        private System.Windows.Forms.ToolStripMenuItem gestionDeRolesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionRespaldoToolStripMenuItem1;
        private System.Windows.Forms.Panel pnlLateral;
        private System.Windows.Forms.PictureBox picLogo;
    }
}