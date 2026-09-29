namespace GestiondeUsuario
{
    partial class FormGestionProductos
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
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.grpFiltro = new System.Windows.Forms.GroupBox();
            this.rbActivos = new System.Windows.Forms.RadioButton();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.nudCodigo = new System.Windows.Forms.NumericUpDown();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblMarca = new System.Windows.Forms.Label();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.lblModelo = new System.Windows.Forms.Label();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.nudPrecio = new System.Windows.Forms.NumericUpDown();
            this.lblExistencia = new System.Windows.Forms.Label();
            this.nudExistencia = new System.Windows.Forms.NumericUpDown();
            this.lblModo = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnBaja = new System.Windows.Forms.Button();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.grpFiltro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCodigo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudExistencia)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Text = "Gestión de Productos";
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblBuscar
            // 
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.Location = new System.Drawing.Point(20, 64);
            this.lblBuscar.Text = "Buscar:";
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.TabIndex = 1;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(90, 61);
            this.txtBuscar.Size = new System.Drawing.Size(260, 25);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.TabIndex = 2;
            this.txtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscar_KeyPress);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(360, 58);
            this.btnBuscar.Size = new System.Drawing.Size(110, 32);
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // grpFiltro
            // 
            this.grpFiltro.Controls.Add(this.rbActivos);
            this.grpFiltro.Controls.Add(this.rbTodos);
            this.grpFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.grpFiltro.Location = new System.Drawing.Point(600, 50);
            this.grpFiltro.Size = new System.Drawing.Size(280, 46);
            this.grpFiltro.TabStop = false;
            this.grpFiltro.Text = "";
            this.grpFiltro.Name = "grpFiltro";
            this.grpFiltro.TabIndex = 4;
            // 
            // rbActivos
            // 
            this.rbActivos.AutoSize = true;
            this.rbActivos.Checked = true;
            this.rbActivos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbActivos.Location = new System.Drawing.Point(15, 15);
            this.rbActivos.Text = "Activos";
            this.rbActivos.UseVisualStyleBackColor = true;
            this.rbActivos.Name = "rbActivos";
            this.rbActivos.TabIndex = 5;
            this.rbActivos.CheckedChanged += new System.EventHandler(this.rbFiltro_CheckedChanged);
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTodos.Location = new System.Drawing.Point(140, 15);
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.TabIndex = 6;
            this.rbTodos.CheckedChanged += new System.EventHandler(this.rbFiltro_CheckedChanged);
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.AllowUserToResizeRows = false;
            this.dgvProductos.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvProductos.Location = new System.Drawing.Point(20, 105);
            this.dgvProductos.MultiSelect = false;
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(860, 235);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.TabIndex = 7;
            this.dgvProductos.SelectionChanged += new System.EventHandler(this.dgvProductos_SelectionChanged);
            // 
            // lblCodigo
            // 
            this.lblCodigo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigo.Location = new System.Drawing.Point(15, 360);
            this.lblCodigo.Text = "Código:";
            this.lblCodigo.AutoSize = false;
            this.lblCodigo.Size = new System.Drawing.Size(110, 25);
            this.lblCodigo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.TabIndex = 8;
            // 
            // nudCodigo
            // 
            this.nudCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.nudCodigo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudCodigo.Location = new System.Drawing.Point(130, 360);
            this.nudCodigo.Size = new System.Drawing.Size(120, 25);
            this.nudCodigo.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudCodigo.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            this.nudCodigo.ThousandsSeparator = false;
            this.nudCodigo.Name = "nudCodigo";
            this.nudCodigo.TabIndex = 9;
            // 
            // lblNombre
            // 
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(15, 395);
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.AutoSize = false;
            this.lblNombre.Size = new System.Drawing.Size(110, 25);
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.TabIndex = 10;
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(130, 395);
            this.txtNombre.Size = new System.Drawing.Size(240, 25);
            this.txtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.TabIndex = 11;
            // 
            // lblMarca
            // 
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarca.Location = new System.Drawing.Point(15, 430);
            this.lblMarca.Text = "Marca:";
            this.lblMarca.AutoSize = false;
            this.lblMarca.Size = new System.Drawing.Size(110, 25);
            this.lblMarca.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMarca.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.TabIndex = 12;
            // 
            // txtMarca
            // 
            this.txtMarca.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMarca.Location = new System.Drawing.Point(130, 430);
            this.txtMarca.Size = new System.Drawing.Size(240, 25);
            this.txtMarca.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.txtMarca.MaxLength = 50;
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.TabIndex = 13;
            // 
            // lblColor
            // 
            this.lblColor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColor.Location = new System.Drawing.Point(15, 465);
            this.lblColor.Text = "Color:";
            this.lblColor.AutoSize = false;
            this.lblColor.Size = new System.Drawing.Size(110, 25);
            this.lblColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblColor.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblColor.Name = "lblColor";
            this.lblColor.TabIndex = 14;
            // 
            // txtColor
            // 
            this.txtColor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColor.Location = new System.Drawing.Point(130, 465);
            this.txtColor.Size = new System.Drawing.Size(240, 25);
            this.txtColor.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.txtColor.MaxLength = 30;
            this.txtColor.Name = "txtColor";
            this.txtColor.TabIndex = 15;
            // 
            // lblModelo
            // 
            this.lblModelo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModelo.Location = new System.Drawing.Point(385, 360);
            this.lblModelo.Text = "Modelo:";
            this.lblModelo.AutoSize = false;
            this.lblModelo.Size = new System.Drawing.Size(110, 25);
            this.lblModelo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblModelo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblModelo.Name = "lblModelo";
            this.lblModelo.TabIndex = 16;
            // 
            // txtModelo
            // 
            this.txtModelo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModelo.Location = new System.Drawing.Point(500, 360);
            this.txtModelo.Size = new System.Drawing.Size(190, 25);
            this.txtModelo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.txtModelo.MaxLength = 50;
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.TabIndex = 17;
            // 
            // lblPrecio
            // 
            this.lblPrecio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecio.Location = new System.Drawing.Point(385, 395);
            this.lblPrecio.Text = "Precio:";
            this.lblPrecio.AutoSize = false;
            this.lblPrecio.Size = new System.Drawing.Size(110, 25);
            this.lblPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPrecio.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.TabIndex = 18;
            // 
            // nudPrecio
            // 
            this.nudPrecio.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.nudPrecio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPrecio.Location = new System.Drawing.Point(500, 395);
            this.nudPrecio.Size = new System.Drawing.Size(190, 25);
            this.nudPrecio.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudPrecio.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            this.nudPrecio.ThousandsSeparator = true;
            this.nudPrecio.DecimalPlaces = 2;
            this.nudPrecio.Name = "nudPrecio";
            this.nudPrecio.TabIndex = 19;
            // 
            // lblExistencia
            // 
            this.lblExistencia.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExistencia.Location = new System.Drawing.Point(385, 430);
            this.lblExistencia.Text = "Existencia:";
            this.lblExistencia.AutoSize = false;
            this.lblExistencia.Size = new System.Drawing.Size(110, 25);
            this.lblExistencia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblExistencia.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblExistencia.Name = "lblExistencia";
            this.lblExistencia.TabIndex = 20;
            // 
            // nudExistencia
            // 
            this.nudExistencia.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.nudExistencia.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudExistencia.Location = new System.Drawing.Point(500, 430);
            this.nudExistencia.Size = new System.Drawing.Size(190, 25);
            this.nudExistencia.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudExistencia.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            this.nudExistencia.ThousandsSeparator = true;
            this.nudExistencia.Name = "nudExistencia";
            this.nudExistencia.TabIndex = 21;
            // 
            // lblModo
            // 
            this.lblModo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModo.Location = new System.Drawing.Point(500, 470);
            this.lblModo.Text = "Seleccioná una opción";
            this.lblModo.AutoSize = true;
            this.lblModo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblModo.Name = "lblModo";
            this.lblModo.TabIndex = 22;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(720, 356);
            this.btnAgregar.Size = new System.Drawing.Size(160, 38);
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.TabIndex = 23;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnModificar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar.Location = new System.Drawing.Point(720, 400);
            this.btnModificar.Size = new System.Drawing.Size(160, 38);
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.TabIndex = 24;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnBaja
            // 
            this.btnBaja.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnBaja.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBaja.Location = new System.Drawing.Point(720, 444);
            this.btnBaja.Size = new System.Drawing.Size(160, 38);
            this.btnBaja.Text = "Dar de baja";
            this.btnBaja.UseVisualStyleBackColor = true;
            this.btnBaja.Name = "btnBaja";
            this.btnBaja.TabIndex = 25;
            this.btnBaja.Click += new System.EventHandler(this.btnBaja_Click);
            // 
            // btnAplicar
            // 
            this.btnAplicar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnAplicar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicar.Location = new System.Drawing.Point(720, 502);
            this.btnAplicar.Size = new System.Drawing.Size(160, 38);
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = true;
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.TabIndex = 26;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(720, 546);
            this.btnCancelar.Size = new System.Drawing.Size(160, 38);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.TabIndex = 27;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 564);
            this.btnVolver.Size = new System.Drawing.Size(110, 32);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 28;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // FormGestionProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 610);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.btnBaja);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.lblModo);
            this.Controls.Add(this.nudExistencia);
            this.Controls.Add(this.lblExistencia);
            this.Controls.Add(this.nudPrecio);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.txtModelo);
            this.Controls.Add(this.lblModelo);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.txtMarca);
            this.Controls.Add(this.lblMarca);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.nudCodigo);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.grpFiltro);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormGestionProductos";
            this.Text = "Gestión de Productos";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormGestionProductos_FormClosed);
            this.Load += new System.EventHandler(this.FormGestionProductos_Load);
            this.grpFiltro.ResumeLayout(false);
            this.grpFiltro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCodigo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudExistencia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox grpFiltro;
        private System.Windows.Forms.RadioButton rbActivos;
        private System.Windows.Forms.RadioButton rbTodos;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.NumericUpDown nudCodigo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.NumericUpDown nudPrecio;
        private System.Windows.Forms.Label lblExistencia;
        private System.Windows.Forms.NumericUpDown nudExistencia;
        private System.Windows.Forms.Label lblModo;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnBaja;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnVolver;
    }
}
