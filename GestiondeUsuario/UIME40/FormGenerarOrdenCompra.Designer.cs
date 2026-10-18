namespace GestiondeUsuario
{
    partial class FormGenerarOrdenCompra
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSolicitud = new System.Windows.Forms.Label();
            this.cmbSolicitud = new System.Windows.Forms.ComboBox();
            this.lblInfoSolicitud = new System.Windows.Forms.Label();
            this.tabCompra = new System.Windows.Forms.TabControl();
            this.tabCotizaciones = new System.Windows.Forms.TabPage();
            this.tabComparar = new System.Windows.Forms.TabPage();
            this.tabOrdenes = new System.Windows.Forms.TabPage();
            this.lblCotizaciones = new System.Windows.Forms.Label();
            this.dgvCotizaciones = new System.Windows.Forms.DataGridView();
            this.grpNuevaCotizacion = new System.Windows.Forms.GroupBox();
            this.lblProveedorCot = new System.Windows.Forms.Label();
            this.cmbProveedorCot = new System.Windows.Forms.ComboBox();
            this.lblPlazo = new System.Windows.Forms.Label();
            this.nudPlazo = new System.Windows.Forms.NumericUpDown();
            this.lblFormaPagoCot = new System.Windows.Forms.Label();
            this.cmbFormaPagoCot = new System.Windows.Forms.ComboBox();
            this.dgvItemsCot = new System.Windows.Forms.DataGridView();
            this.btnRegistrarCotizacion = new System.Windows.Forms.Button();
            this.lblComparativa = new System.Windows.Forms.Label();
            this.dgvComparativa = new System.Windows.Forms.DataGridView();
            this.lblCotizacionElegida = new System.Windows.Forms.Label();
            this.cmbCotizacion = new System.Windows.Forms.ComboBox();
            this.lblEstadoProveedor = new System.Windows.Forms.Label();
            this.dgvItemsOrden = new System.Windows.Forms.DataGridView();
            this.lblEntrega = new System.Windows.Forms.Label();
            this.txtEntrega = new System.Windows.Forms.TextBox();
            this.lblPago = new System.Windows.Forms.Label();
            this.txtPago = new System.Windows.Forms.TextBox();
            this.lblTotalOrden = new System.Windows.Forms.Label();
            this.btnGenerarOrden = new System.Windows.Forms.Button();
            this.lblOrdenes = new System.Windows.Forms.Label();
            this.dgvOrdenes = new System.Windows.Forms.DataGridView();
            this.btnEnviarOrden = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.tabCompra.SuspendLayout();
            this.tabCotizaciones.SuspendLayout();
            this.tabComparar.SuspendLayout();
            this.tabOrdenes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotizaciones)).BeginInit();
            this.grpNuevaCotizacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlazo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemsCot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComparativa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemsOrden)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Text = "Generar orden de compra";
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblSolicitud
            // 
            this.lblSolicitud.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSolicitud.Location = new System.Drawing.Point(20, 60);
            this.lblSolicitud.Text = "Solicitud enviada:";
            this.lblSolicitud.AutoSize = false;
            this.lblSolicitud.Size = new System.Drawing.Size(130, 25);
            this.lblSolicitud.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSolicitud.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblSolicitud.Name = "lblSolicitud";
            this.lblSolicitud.TabIndex = 1;
            // 
            // cmbSolicitud
            // 
            this.cmbSolicitud.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSolicitud.Location = new System.Drawing.Point(155, 60);
            this.cmbSolicitud.Size = new System.Drawing.Size(330, 25);
            this.cmbSolicitud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSolicitud.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.cmbSolicitud.Name = "cmbSolicitud";
            this.cmbSolicitud.TabIndex = 2;
            this.cmbSolicitud.SelectedIndexChanged += new System.EventHandler(this.cmbSolicitud_SelectedIndexChanged);
            // 
            // lblInfoSolicitud
            // 
            this.lblInfoSolicitud.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoSolicitud.Location = new System.Drawing.Point(500, 63);
            this.lblInfoSolicitud.Text = "";
            this.lblInfoSolicitud.AutoSize = false;
            this.lblInfoSolicitud.Size = new System.Drawing.Size(630, 22);
            this.lblInfoSolicitud.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblInfoSolicitud.Name = "lblInfoSolicitud";
            this.lblInfoSolicitud.TabIndex = 3;
            // 
            // tabCompra
            // 
            this.tabCompra.Controls.Add(this.tabCotizaciones);
            this.tabCompra.Controls.Add(this.tabComparar);
            this.tabCompra.Controls.Add(this.tabOrdenes);
            this.tabCompra.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.tabCompra.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabCompra.Location = new System.Drawing.Point(20, 98);
            this.tabCompra.Size = new System.Drawing.Size(1110, 565);
            this.tabCompra.Name = "tabCompra";
            this.tabCompra.TabIndex = 4;
            // 
            // tabCotizaciones
            // 
            this.tabCotizaciones.Controls.Add(this.lblCotizaciones);
            this.tabCotizaciones.Controls.Add(this.dgvCotizaciones);
            this.tabCotizaciones.Controls.Add(this.grpNuevaCotizacion);
            this.tabCotizaciones.Location = new System.Drawing.Point(4, 27);
            this.tabCotizaciones.Padding = new System.Windows.Forms.Padding(3);
            this.tabCotizaciones.Size = new System.Drawing.Size(1102, 534);
            this.tabCotizaciones.Text = "1. Cotizaciones";
            this.tabCotizaciones.UseVisualStyleBackColor = true;
            this.tabCotizaciones.Name = "tabCotizaciones";
            this.tabCotizaciones.TabIndex = 5;
            // 
            // tabComparar
            // 
            this.tabComparar.Controls.Add(this.lblComparativa);
            this.tabComparar.Controls.Add(this.dgvComparativa);
            this.tabComparar.Controls.Add(this.lblCotizacionElegida);
            this.tabComparar.Controls.Add(this.cmbCotizacion);
            this.tabComparar.Controls.Add(this.lblEstadoProveedor);
            this.tabComparar.Controls.Add(this.dgvItemsOrden);
            this.tabComparar.Controls.Add(this.lblEntrega);
            this.tabComparar.Controls.Add(this.txtEntrega);
            this.tabComparar.Controls.Add(this.lblPago);
            this.tabComparar.Controls.Add(this.txtPago);
            this.tabComparar.Controls.Add(this.lblTotalOrden);
            this.tabComparar.Controls.Add(this.btnGenerarOrden);
            this.tabComparar.Location = new System.Drawing.Point(4, 27);
            this.tabComparar.Padding = new System.Windows.Forms.Padding(3);
            this.tabComparar.Size = new System.Drawing.Size(1102, 534);
            this.tabComparar.Text = "2. Comparar y generar orden";
            this.tabComparar.UseVisualStyleBackColor = true;
            this.tabComparar.Name = "tabComparar";
            this.tabComparar.TabIndex = 6;
            // 
            // tabOrdenes
            // 
            this.tabOrdenes.Controls.Add(this.lblOrdenes);
            this.tabOrdenes.Controls.Add(this.dgvOrdenes);
            this.tabOrdenes.Controls.Add(this.btnEnviarOrden);
            this.tabOrdenes.Location = new System.Drawing.Point(4, 27);
            this.tabOrdenes.Padding = new System.Windows.Forms.Padding(3);
            this.tabOrdenes.Size = new System.Drawing.Size(1102, 534);
            this.tabOrdenes.Text = "3. Órdenes de compra";
            this.tabOrdenes.UseVisualStyleBackColor = true;
            this.tabOrdenes.Name = "tabOrdenes";
            this.tabOrdenes.TabIndex = 7;
            // 
            // lblCotizaciones
            // 
            this.lblCotizaciones.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCotizaciones.Location = new System.Drawing.Point(10, 10);
            this.lblCotizaciones.Text = "Cotizaciones recibidas";
            this.lblCotizaciones.AutoSize = true;
            this.lblCotizaciones.Name = "lblCotizaciones";
            this.lblCotizaciones.TabIndex = 8;
            // 
            // dgvCotizaciones
            // 
            this.dgvCotizaciones.AllowUserToAddRows = false;
            this.dgvCotizaciones.AllowUserToDeleteRows = false;
            this.dgvCotizaciones.AllowUserToResizeRows = false;
            this.dgvCotizaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvCotizaciones.Location = new System.Drawing.Point(10, 35);
            this.dgvCotizaciones.MultiSelect = false;
            this.dgvCotizaciones.ReadOnly = true;
            this.dgvCotizaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCotizaciones.Size = new System.Drawing.Size(1080, 150);
            this.dgvCotizaciones.Name = "dgvCotizaciones";
            this.dgvCotizaciones.TabIndex = 9;
            // 
            // grpNuevaCotizacion
            // 
            this.grpNuevaCotizacion.Controls.Add(this.lblProveedorCot);
            this.grpNuevaCotizacion.Controls.Add(this.cmbProveedorCot);
            this.grpNuevaCotizacion.Controls.Add(this.lblPlazo);
            this.grpNuevaCotizacion.Controls.Add(this.nudPlazo);
            this.grpNuevaCotizacion.Controls.Add(this.lblFormaPagoCot);
            this.grpNuevaCotizacion.Controls.Add(this.cmbFormaPagoCot);
            this.grpNuevaCotizacion.Controls.Add(this.dgvItemsCot);
            this.grpNuevaCotizacion.Controls.Add(this.btnRegistrarCotizacion);
            this.grpNuevaCotizacion.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.grpNuevaCotizacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpNuevaCotizacion.Location = new System.Drawing.Point(10, 195);
            this.grpNuevaCotizacion.Size = new System.Drawing.Size(1080, 330);
            this.grpNuevaCotizacion.TabStop = false;
            this.grpNuevaCotizacion.Text = "Registrar cotización recibida";
            this.grpNuevaCotizacion.Name = "grpNuevaCotizacion";
            this.grpNuevaCotizacion.TabIndex = 10;
            // 
            // lblProveedorCot
            // 
            this.lblProveedorCot.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProveedorCot.Location = new System.Drawing.Point(10, 30);
            this.lblProveedorCot.Text = "Proveedor:";
            this.lblProveedorCot.AutoSize = false;
            this.lblProveedorCot.Size = new System.Drawing.Size(90, 25);
            this.lblProveedorCot.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblProveedorCot.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblProveedorCot.Name = "lblProveedorCot";
            this.lblProveedorCot.TabIndex = 11;
            // 
            // cmbProveedorCot
            // 
            this.cmbProveedorCot.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProveedorCot.Location = new System.Drawing.Point(105, 30);
            this.cmbProveedorCot.Size = new System.Drawing.Size(290, 25);
            this.cmbProveedorCot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProveedorCot.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.cmbProveedorCot.Name = "cmbProveedorCot";
            this.cmbProveedorCot.TabIndex = 12;
            // 
            // lblPlazo
            // 
            this.lblPlazo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlazo.Location = new System.Drawing.Point(405, 30);
            this.lblPlazo.Text = "Plazo (días):";
            this.lblPlazo.AutoSize = false;
            this.lblPlazo.Size = new System.Drawing.Size(100, 25);
            this.lblPlazo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPlazo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblPlazo.Name = "lblPlazo";
            this.lblPlazo.TabIndex = 13;
            // 
            // nudPlazo
            // 
            this.nudPlazo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.nudPlazo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPlazo.Location = new System.Drawing.Point(510, 30);
            this.nudPlazo.Size = new System.Drawing.Size(70, 25);
            this.nudPlazo.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudPlazo.Maximum = new decimal(new int[] { 365, 0, 0, 0 });
            this.nudPlazo.Name = "nudPlazo";
            this.nudPlazo.TabIndex = 14;
            // 
            // lblFormaPagoCot
            // 
            this.lblFormaPagoCot.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormaPagoCot.Location = new System.Drawing.Point(590, 30);
            this.lblFormaPagoCot.Text = "Forma de pago:";
            this.lblFormaPagoCot.AutoSize = false;
            this.lblFormaPagoCot.Size = new System.Drawing.Size(115, 25);
            this.lblFormaPagoCot.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFormaPagoCot.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblFormaPagoCot.Name = "lblFormaPagoCot";
            this.lblFormaPagoCot.TabIndex = 15;
            // 
            // cmbFormaPagoCot
            // 
            this.cmbFormaPagoCot.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFormaPagoCot.Location = new System.Drawing.Point(710, 30);
            this.cmbFormaPagoCot.Size = new System.Drawing.Size(180, 25);
            this.cmbFormaPagoCot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormaPagoCot.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.cmbFormaPagoCot.Name = "cmbFormaPagoCot";
            this.cmbFormaPagoCot.TabIndex = 16;
            // 
            // dgvItemsCot
            // 
            this.dgvItemsCot.AllowUserToAddRows = false;
            this.dgvItemsCot.AllowUserToDeleteRows = false;
            this.dgvItemsCot.AllowUserToResizeRows = false;
            this.dgvItemsCot.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvItemsCot.Location = new System.Drawing.Point(10, 65);
            this.dgvItemsCot.MultiSelect = false;
            this.dgvItemsCot.ReadOnly = false;
            this.dgvItemsCot.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemsCot.Size = new System.Drawing.Size(1060, 210);
            this.dgvItemsCot.Name = "dgvItemsCot";
            this.dgvItemsCot.TabIndex = 17;
            this.dgvItemsCot.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvItemsCot_DataError);
            // 
            // btnRegistrarCotizacion
            // 
            this.btnRegistrarCotizacion.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnRegistrarCotizacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarCotizacion.Location = new System.Drawing.Point(830, 285);
            this.btnRegistrarCotizacion.Size = new System.Drawing.Size(240, 36);
            this.btnRegistrarCotizacion.Text = "Registrar cotización";
            this.btnRegistrarCotizacion.UseVisualStyleBackColor = true;
            this.btnRegistrarCotizacion.Name = "btnRegistrarCotizacion";
            this.btnRegistrarCotizacion.TabIndex = 18;
            this.btnRegistrarCotizacion.Click += new System.EventHandler(this.btnRegistrarCotizacion_Click);
            // 
            // lblComparativa
            // 
            this.lblComparativa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComparativa.Location = new System.Drawing.Point(10, 10);
            this.lblComparativa.Text = "Precio unitario cotizado por proveedor (en verde, el mejor precio)";
            this.lblComparativa.AutoSize = true;
            this.lblComparativa.Name = "lblComparativa";
            this.lblComparativa.TabIndex = 19;
            // 
            // dgvComparativa
            // 
            this.dgvComparativa.AllowUserToAddRows = false;
            this.dgvComparativa.AllowUserToDeleteRows = false;
            this.dgvComparativa.AllowUserToResizeRows = false;
            this.dgvComparativa.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvComparativa.Location = new System.Drawing.Point(10, 35);
            this.dgvComparativa.MultiSelect = false;
            this.dgvComparativa.ReadOnly = true;
            this.dgvComparativa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvComparativa.Size = new System.Drawing.Size(1080, 180);
            this.dgvComparativa.Name = "dgvComparativa";
            this.dgvComparativa.TabIndex = 20;
            // 
            // lblCotizacionElegida
            // 
            this.lblCotizacionElegida.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCotizacionElegida.Location = new System.Drawing.Point(10, 228);
            this.lblCotizacionElegida.Text = "Proveedor elegido:";
            this.lblCotizacionElegida.AutoSize = false;
            this.lblCotizacionElegida.Size = new System.Drawing.Size(140, 25);
            this.lblCotizacionElegida.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCotizacionElegida.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblCotizacionElegida.Name = "lblCotizacionElegida";
            this.lblCotizacionElegida.TabIndex = 21;
            // 
            // cmbCotizacion
            // 
            this.cmbCotizacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCotizacion.Location = new System.Drawing.Point(155, 228);
            this.cmbCotizacion.Size = new System.Drawing.Size(330, 25);
            this.cmbCotizacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCotizacion.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.cmbCotizacion.Name = "cmbCotizacion";
            this.cmbCotizacion.TabIndex = 22;
            this.cmbCotizacion.SelectedIndexChanged += new System.EventHandler(this.cmbCotizacion_SelectedIndexChanged);
            // 
            // lblEstadoProveedor
            // 
            this.lblEstadoProveedor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoProveedor.Location = new System.Drawing.Point(500, 231);
            this.lblEstadoProveedor.Text = "";
            this.lblEstadoProveedor.AutoSize = false;
            this.lblEstadoProveedor.Size = new System.Drawing.Size(580, 22);
            this.lblEstadoProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblEstadoProveedor.Name = "lblEstadoProveedor";
            this.lblEstadoProveedor.TabIndex = 23;
            // 
            // dgvItemsOrden
            // 
            this.dgvItemsOrden.AllowUserToAddRows = false;
            this.dgvItemsOrden.AllowUserToDeleteRows = false;
            this.dgvItemsOrden.AllowUserToResizeRows = false;
            this.dgvItemsOrden.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvItemsOrden.Location = new System.Drawing.Point(10, 262);
            this.dgvItemsOrden.MultiSelect = false;
            this.dgvItemsOrden.ReadOnly = true;
            this.dgvItemsOrden.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemsOrden.Size = new System.Drawing.Size(1080, 160);
            this.dgvItemsOrden.Name = "dgvItemsOrden";
            this.dgvItemsOrden.TabIndex = 24;
            // 
            // lblEntrega
            // 
            this.lblEntrega.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntrega.Location = new System.Drawing.Point(10, 435);
            this.lblEntrega.Text = "Condiciones de entrega:";
            this.lblEntrega.AutoSize = false;
            this.lblEntrega.Size = new System.Drawing.Size(170, 25);
            this.lblEntrega.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEntrega.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblEntrega.Name = "lblEntrega";
            this.lblEntrega.TabIndex = 25;
            // 
            // txtEntrega
            // 
            this.txtEntrega.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntrega.Location = new System.Drawing.Point(185, 435);
            this.txtEntrega.Size = new System.Drawing.Size(420, 25);
            this.txtEntrega.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.txtEntrega.MaxLength = 200;
            this.txtEntrega.Name = "txtEntrega";
            this.txtEntrega.TabIndex = 26;
            // 
            // lblPago
            // 
            this.lblPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPago.Location = new System.Drawing.Point(10, 470);
            this.lblPago.Text = "Condiciones de pago:";
            this.lblPago.AutoSize = false;
            this.lblPago.Size = new System.Drawing.Size(170, 25);
            this.lblPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPago.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblPago.Name = "lblPago";
            this.lblPago.TabIndex = 27;
            // 
            // txtPago
            // 
            this.txtPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPago.Location = new System.Drawing.Point(185, 470);
            this.txtPago.Size = new System.Drawing.Size(420, 25);
            this.txtPago.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.txtPago.MaxLength = 200;
            this.txtPago.Name = "txtPago";
            this.txtPago.TabIndex = 28;
            // 
            // lblTotalOrden
            // 
            this.lblTotalOrden.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOrden.Location = new System.Drawing.Point(620, 437);
            this.lblTotalOrden.Text = "";
            this.lblTotalOrden.AutoSize = false;
            this.lblTotalOrden.Size = new System.Drawing.Size(460, 25);
            this.lblTotalOrden.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.lblTotalOrden.Name = "lblTotalOrden";
            this.lblTotalOrden.TabIndex = 29;
            // 
            // btnGenerarOrden
            // 
            this.btnGenerarOrden.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnGenerarOrden.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarOrden.Location = new System.Drawing.Point(840, 482);
            this.btnGenerarOrden.Size = new System.Drawing.Size(250, 40);
            this.btnGenerarOrden.Text = "Generar orden de compra";
            this.btnGenerarOrden.UseVisualStyleBackColor = true;
            this.btnGenerarOrden.Name = "btnGenerarOrden";
            this.btnGenerarOrden.TabIndex = 30;
            this.btnGenerarOrden.Click += new System.EventHandler(this.btnGenerarOrden_Click);
            // 
            // lblOrdenes
            // 
            this.lblOrdenes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrdenes.Location = new System.Drawing.Point(10, 10);
            this.lblOrdenes.Text = "Órdenes generadas para esta solicitud";
            this.lblOrdenes.AutoSize = true;
            this.lblOrdenes.Name = "lblOrdenes";
            this.lblOrdenes.TabIndex = 31;
            // 
            // dgvOrdenes
            // 
            this.dgvOrdenes.AllowUserToAddRows = false;
            this.dgvOrdenes.AllowUserToDeleteRows = false;
            this.dgvOrdenes.AllowUserToResizeRows = false;
            this.dgvOrdenes.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvOrdenes.Location = new System.Drawing.Point(10, 35);
            this.dgvOrdenes.MultiSelect = false;
            this.dgvOrdenes.ReadOnly = true;
            this.dgvOrdenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdenes.Size = new System.Drawing.Size(1080, 435);
            this.dgvOrdenes.Name = "dgvOrdenes";
            this.dgvOrdenes.TabIndex = 32;
            // 
            // btnEnviarOrden
            // 
            this.btnEnviarOrden.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnEnviarOrden.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarOrden.Location = new System.Drawing.Point(840, 482);
            this.btnEnviarOrden.Size = new System.Drawing.Size(250, 40);
            this.btnEnviarOrden.Text = "Enviar orden al proveedor";
            this.btnEnviarOrden.UseVisualStyleBackColor = true;
            this.btnEnviarOrden.Name = "btnEnviarOrden";
            this.btnEnviarOrden.TabIndex = 33;
            this.btnEnviarOrden.Click += new System.EventHandler(this.btnEnviarOrden_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 674);
            this.btnVolver.Size = new System.Drawing.Size(110, 34);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 34;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // FormGenerarOrdenCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 720);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.tabCompra);
            this.Controls.Add(this.lblInfoSolicitud);
            this.Controls.Add(this.cmbSolicitud);
            this.Controls.Add(this.lblSolicitud);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormGenerarOrdenCompra";
            this.Text = "Generar orden de compra";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormGenerarOrdenCompra_FormClosed);
            this.Load += new System.EventHandler(this.FormGenerarOrdenCompra_Load);
            this.tabCompra.ResumeLayout(false);
            this.tabCompra.PerformLayout();
            this.tabCotizaciones.ResumeLayout(false);
            this.tabCotizaciones.PerformLayout();
            this.tabComparar.ResumeLayout(false);
            this.tabComparar.PerformLayout();
            this.tabOrdenes.ResumeLayout(false);
            this.tabOrdenes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotizaciones)).EndInit();
            this.grpNuevaCotizacion.ResumeLayout(false);
            this.grpNuevaCotizacion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlazo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemsCot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComparativa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemsOrden)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSolicitud;
        private System.Windows.Forms.ComboBox cmbSolicitud;
        private System.Windows.Forms.Label lblInfoSolicitud;
        private System.Windows.Forms.TabControl tabCompra;
        private System.Windows.Forms.TabPage tabCotizaciones;
        private System.Windows.Forms.TabPage tabComparar;
        private System.Windows.Forms.TabPage tabOrdenes;
        private System.Windows.Forms.Label lblCotizaciones;
        private System.Windows.Forms.DataGridView dgvCotizaciones;
        private System.Windows.Forms.GroupBox grpNuevaCotizacion;
        private System.Windows.Forms.Label lblProveedorCot;
        private System.Windows.Forms.ComboBox cmbProveedorCot;
        private System.Windows.Forms.Label lblPlazo;
        private System.Windows.Forms.NumericUpDown nudPlazo;
        private System.Windows.Forms.Label lblFormaPagoCot;
        private System.Windows.Forms.ComboBox cmbFormaPagoCot;
        private System.Windows.Forms.DataGridView dgvItemsCot;
        private System.Windows.Forms.Button btnRegistrarCotizacion;
        private System.Windows.Forms.Label lblComparativa;
        private System.Windows.Forms.DataGridView dgvComparativa;
        private System.Windows.Forms.Label lblCotizacionElegida;
        private System.Windows.Forms.ComboBox cmbCotizacion;
        private System.Windows.Forms.Label lblEstadoProveedor;
        private System.Windows.Forms.DataGridView dgvItemsOrden;
        private System.Windows.Forms.Label lblEntrega;
        private System.Windows.Forms.TextBox txtEntrega;
        private System.Windows.Forms.Label lblPago;
        private System.Windows.Forms.TextBox txtPago;
        private System.Windows.Forms.Label lblTotalOrden;
        private System.Windows.Forms.Button btnGenerarOrden;
        private System.Windows.Forms.Label lblOrdenes;
        private System.Windows.Forms.DataGridView dgvOrdenes;
        private System.Windows.Forms.Button btnEnviarOrden;
        private System.Windows.Forms.Button btnVolver;
    }
}
