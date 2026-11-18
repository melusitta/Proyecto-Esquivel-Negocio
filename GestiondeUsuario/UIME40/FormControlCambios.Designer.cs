namespace GestiondeUsuario
{
    partial class FormControlCambios
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.cmbUsuario = new System.Windows.Forms.ComboBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.lblDesde = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.lblVersiones = new System.Windows.Forms.Label();
            this.dgvVersiones = new System.Windows.Forms.DataGridView();
            this.lblAyudaRestaurar = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.pnlFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVersiones)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Bitácora de cambios";
            //
            // pnlFiltros: producto, usuario, tipo de cambio, desde, hasta, Buscar y Limpiar en una fila
            //
            this.pnlFiltros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFiltros.Controls.Add(this.lblBuscar);
            this.pnlFiltros.Controls.Add(this.txtBuscar);
            this.pnlFiltros.Controls.Add(this.lblUsuario);
            this.pnlFiltros.Controls.Add(this.cmbUsuario);
            this.pnlFiltros.Controls.Add(this.lblTipo);
            this.pnlFiltros.Controls.Add(this.cmbTipo);
            this.pnlFiltros.Controls.Add(this.lblDesde);
            this.pnlFiltros.Controls.Add(this.dtpDesde);
            this.pnlFiltros.Controls.Add(this.lblHasta);
            this.pnlFiltros.Controls.Add(this.dtpHasta);
            this.pnlFiltros.Controls.Add(this.btnBuscar);
            this.pnlFiltros.Controls.Add(this.btnLimpiar);
            this.pnlFiltros.Location = new System.Drawing.Point(17, 58);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Size = new System.Drawing.Size(1286, 40);
            this.pnlFiltros.TabIndex = 1;
            this.pnlFiltros.WrapContents = false;
            //
            // lblBuscar
            //
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBuscar.Margin = new System.Windows.Forms.Padding(3, 8, 0, 0);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Producto:";
            //
            // txtBuscar
            //
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(3, 5, 12, 3);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(170, 25);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscar_KeyDown);
            //
            // lblUsuario
            //
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUsuario.Margin = new System.Windows.Forms.Padding(3, 8, 0, 0);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.TabIndex = 2;
            this.lblUsuario.Text = "Usuario:";
            //
            // cmbUsuario
            //
            this.cmbUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbUsuario.Margin = new System.Windows.Forms.Padding(3, 5, 12, 3);
            this.cmbUsuario.Name = "cmbUsuario";
            this.cmbUsuario.Size = new System.Drawing.Size(140, 25);
            this.cmbUsuario.TabIndex = 3;
            //
            // lblTipo
            //
            this.lblTipo.AutoSize = true;
            this.lblTipo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTipo.Margin = new System.Windows.Forms.Padding(3, 8, 0, 0);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.TabIndex = 4;
            this.lblTipo.Text = "Tipo de cambio:";
            //
            // cmbTipo
            //
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipo.Margin = new System.Windows.Forms.Padding(3, 5, 12, 3);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(130, 25);
            this.cmbTipo.TabIndex = 5;
            //
            // lblDesde
            //
            this.lblDesde.AutoSize = true;
            this.lblDesde.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDesde.Margin = new System.Windows.Forms.Padding(3, 8, 0, 0);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.TabIndex = 6;
            this.lblDesde.Text = "Desde:";
            //
            // dtpDesde
            //
            this.dtpDesde.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Margin = new System.Windows.Forms.Padding(3, 5, 12, 3);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(110, 25);
            this.dtpDesde.TabIndex = 7;
            //
            // lblHasta
            //
            this.lblHasta.AutoSize = true;
            this.lblHasta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHasta.Margin = new System.Windows.Forms.Padding(3, 8, 0, 0);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.TabIndex = 8;
            this.lblHasta.Text = "Hasta:";
            //
            // dtpHasta
            //
            this.dtpHasta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Margin = new System.Windows.Forms.Padding(3, 5, 16, 3);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(110, 25);
            this.dtpHasta.TabIndex = 9;
            //
            // btnBuscar
            //
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 6, 3);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(100, 32);
            this.btnBuscar.TabIndex = 10;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            //
            // btnLimpiar
            //
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(100, 32);
            this.btnLimpiar.TabIndex = 11;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            //
            // lblVersiones
            //
            this.lblVersiones.AutoSize = true;
            this.lblVersiones.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersiones.Location = new System.Drawing.Point(20, 110);
            this.lblVersiones.Name = "lblVersiones";
            this.lblVersiones.TabIndex = 2;
            this.lblVersiones.Text = "Cambios";
            //
            // dgvVersiones
            //
            this.dgvVersiones.AllowUserToAddRows = false;
            this.dgvVersiones.AllowUserToDeleteRows = false;
            this.dgvVersiones.AllowUserToResizeRows = false;
            this.dgvVersiones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVersiones.Location = new System.Drawing.Point(20, 135);
            this.dgvVersiones.MultiSelect = false;
            this.dgvVersiones.Name = "dgvVersiones";
            this.dgvVersiones.ReadOnly = true;
            this.dgvVersiones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVersiones.Size = new System.Drawing.Size(1280, 475);
            this.dgvVersiones.TabIndex = 3;
            this.dgvVersiones.SelectionChanged += new System.EventHandler(this.dgvVersiones_SelectionChanged);
            //
            // lblAyudaRestaurar
            //
            this.lblAyudaRestaurar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAyudaRestaurar.AutoSize = true;
            this.lblAyudaRestaurar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAyudaRestaurar.Location = new System.Drawing.Point(20, 620);
            this.lblAyudaRestaurar.Name = "lblAyudaRestaurar";
            this.lblAyudaRestaurar.TabIndex = 4;
            this.lblAyudaRestaurar.Text = "";
            //
            // btnVolver
            //
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 646);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(110, 34);
            this.btnVolver.TabIndex = 5;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            //
            // btnRestaurar
            //
            this.btnRestaurar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestaurar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestaurar.Location = new System.Drawing.Point(1060, 642);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(240, 38);
            this.btnRestaurar.TabIndex = 6;
            this.btnRestaurar.Text = "Activar esta versión";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            //
            // FormControlCambios
            //
            this.AcceptButton = this.btnBuscar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1320, 700);
            this.Controls.Add(this.btnRestaurar);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.lblAyudaRestaurar);
            this.Controls.Add(this.dgvVersiones);
            this.Controls.Add(this.lblVersiones);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormControlCambios";
            this.Text = "Bitácora de cambios";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormControlCambios_FormClosed);
            this.Load += new System.EventHandler(this.FormControlCambios_Load);
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVersiones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.FlowLayoutPanel pnlFiltros;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ComboBox cmbUsuario;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblVersiones;
        private System.Windows.Forms.DataGridView dgvVersiones;
        private System.Windows.Forms.Label lblAyudaRestaurar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnRestaurar;
    }
}
