namespace GestiondeUsuario
{
    partial class FormProductosMasVendidos
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
            this.grpFiltros = new System.Windows.Forms.GroupBox();
            this.lblPeriodo = new System.Windows.Forms.Label();
            this.cmbPeriodo = new System.Windows.Forms.ComboBox();
            this.lblAnio = new System.Windows.Forms.Label();
            this.nudAnio = new System.Windows.Forms.NumericUpDown();
            this.lblDesde = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.lblOrden = new System.Windows.Forms.Label();
            this.rbUnidades = new System.Windows.Forms.RadioButton();
            this.rbMonto = new System.Windows.Forms.RadioButton();
            this.lblMostrar = new System.Windows.Forms.Label();
            this.cmbTop = new System.Windows.Forms.ComboBox();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.dgvRanking = new System.Windows.Forms.DataGridView();
            this.grpGrafico = new System.Windows.Forms.GroupBox();
            this.pnlGrafico = new System.Windows.Forms.Panel();
            this.lblResumen = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.grpFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRanking)).BeginInit();
            this.grpGrafico.SuspendLayout();
            this.pnlGrafico.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Text = "Productos más vendidos";
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // grpFiltros
            // 
            this.grpFiltros.Controls.Add(this.lblPeriodo);
            this.grpFiltros.Controls.Add(this.cmbPeriodo);
            this.grpFiltros.Controls.Add(this.lblAnio);
            this.grpFiltros.Controls.Add(this.nudAnio);
            this.grpFiltros.Controls.Add(this.lblDesde);
            this.grpFiltros.Controls.Add(this.dtpDesde);
            this.grpFiltros.Controls.Add(this.lblHasta);
            this.grpFiltros.Controls.Add(this.dtpHasta);
            this.grpFiltros.Controls.Add(this.lblOrden);
            this.grpFiltros.Controls.Add(this.rbUnidades);
            this.grpFiltros.Controls.Add(this.rbMonto);
            this.grpFiltros.Controls.Add(this.lblMostrar);
            this.grpFiltros.Controls.Add(this.cmbTop);
            this.grpFiltros.Controls.Add(this.btnGenerar);
            this.grpFiltros.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.grpFiltros.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFiltros.Location = new System.Drawing.Point(20, 55);
            this.grpFiltros.Size = new System.Drawing.Size(1080, 97);
            this.grpFiltros.TabStop = false;
            this.grpFiltros.Text = "Filtros";
            this.grpFiltros.Name = "grpFiltros";
            this.grpFiltros.TabIndex = 1;
            // 
            // lblPeriodo
            // 
            this.lblPeriodo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeriodo.Location = new System.Drawing.Point(10, 28);
            this.lblPeriodo.Text = "Período:";
            this.lblPeriodo.AutoSize = false;
            this.lblPeriodo.Size = new System.Drawing.Size(110, 25);
            this.lblPeriodo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPeriodo.Name = "lblPeriodo";
            this.lblPeriodo.TabIndex = 2;
            // 
            // cmbPeriodo
            // 
            this.cmbPeriodo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriodo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPeriodo.Location = new System.Drawing.Point(125, 28);
            this.cmbPeriodo.Size = new System.Drawing.Size(180, 25);
            this.cmbPeriodo.Name = "cmbPeriodo";
            this.cmbPeriodo.TabIndex = 3;
            this.cmbPeriodo.SelectedIndexChanged += new System.EventHandler(this.cmbPeriodo_SelectedIndexChanged);
            // 
            // lblAnio
            // 
            this.lblAnio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnio.Location = new System.Drawing.Point(315, 28);
            this.lblAnio.Text = "Año:";
            this.lblAnio.AutoSize = false;
            this.lblAnio.Size = new System.Drawing.Size(110, 25);
            this.lblAnio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAnio.Name = "lblAnio";
            this.lblAnio.TabIndex = 4;
            // 
            // nudAnio
            // 
            this.nudAnio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudAnio.Location = new System.Drawing.Point(430, 28);
            this.nudAnio.Size = new System.Drawing.Size(80, 25);
            this.nudAnio.Minimum = new decimal(new int[] { 2000, 0, 0, 0 });
            this.nudAnio.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            this.nudAnio.Value = new decimal(new int[] { 2026, 0, 0, 0 });
            this.nudAnio.Name = "nudAnio";
            this.nudAnio.TabIndex = 5;
            this.nudAnio.ValueChanged += new System.EventHandler(this.nudAnio_ValueChanged);
            // 
            // lblDesde
            // 
            this.lblDesde.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesde.Location = new System.Drawing.Point(525, 28);
            this.lblDesde.Text = "Desde:";
            this.lblDesde.AutoSize = false;
            this.lblDesde.Size = new System.Drawing.Size(110, 25);
            this.lblDesde.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.TabIndex = 6;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(640, 28);
            this.dtpDesde.Size = new System.Drawing.Size(130, 25);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.TabIndex = 7;
            // 
            // lblHasta
            // 
            this.lblHasta.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHasta.Location = new System.Drawing.Point(775, 28);
            this.lblHasta.Text = "Hasta:";
            this.lblHasta.AutoSize = false;
            this.lblHasta.Size = new System.Drawing.Size(110, 25);
            this.lblHasta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.TabIndex = 8;
            // 
            // dtpHasta
            // 
            this.dtpHasta.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(890, 28);
            this.dtpHasta.Size = new System.Drawing.Size(130, 25);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.TabIndex = 9;
            // 
            // lblOrden
            // 
            this.lblOrden.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrden.Location = new System.Drawing.Point(10, 62);
            this.lblOrden.Text = "Ordenar por:";
            this.lblOrden.AutoSize = false;
            this.lblOrden.Size = new System.Drawing.Size(110, 25);
            this.lblOrden.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblOrden.Name = "lblOrden";
            this.lblOrden.TabIndex = 10;
            // 
            // rbUnidades
            // 
            this.rbUnidades.AutoSize = true;
            this.rbUnidades.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbUnidades.Location = new System.Drawing.Point(125, 63);
            this.rbUnidades.Text = "Unidades";
            this.rbUnidades.UseVisualStyleBackColor = true;
            this.rbUnidades.Checked = true;
            this.rbUnidades.Name = "rbUnidades";
            this.rbUnidades.TabIndex = 11;
            // 
            // rbMonto
            // 
            this.rbMonto.AutoSize = true;
            this.rbMonto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMonto.Location = new System.Drawing.Point(230, 63);
            this.rbMonto.Text = "Monto";
            this.rbMonto.UseVisualStyleBackColor = true;
            this.rbMonto.Name = "rbMonto";
            this.rbMonto.TabIndex = 12;
            // 
            // lblMostrar
            // 
            this.lblMostrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMostrar.Location = new System.Drawing.Point(315, 62);
            this.lblMostrar.Text = "Mostrar:";
            this.lblMostrar.AutoSize = false;
            this.lblMostrar.Size = new System.Drawing.Size(110, 25);
            this.lblMostrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMostrar.Name = "lblMostrar";
            this.lblMostrar.TabIndex = 13;
            // 
            // cmbTop
            // 
            this.cmbTop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTop.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTop.Location = new System.Drawing.Point(430, 62);
            this.cmbTop.Size = new System.Drawing.Size(80, 25);
            this.cmbTop.Name = "cmbTop";
            this.cmbTop.TabIndex = 14;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnGenerar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerar.Location = new System.Drawing.Point(890, 56);
            this.btnGenerar.Size = new System.Drawing.Size(180, 34);
            this.btnGenerar.Text = "Generar reporte";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.TabIndex = 15;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // dgvRanking
            // 
            this.dgvRanking.AllowUserToAddRows = false;
            this.dgvRanking.AllowUserToDeleteRows = false;
            this.dgvRanking.AllowUserToResizeRows = false;
            this.dgvRanking.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.dgvRanking.Location = new System.Drawing.Point(20, 165);
            this.dgvRanking.MultiSelect = false;
            this.dgvRanking.ReadOnly = true;
            this.dgvRanking.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRanking.Size = new System.Drawing.Size(600, 390);
            this.dgvRanking.Name = "dgvRanking";
            this.dgvRanking.TabIndex = 16;
            // 
            // grpGrafico
            // 
            this.grpGrafico.Controls.Add(this.pnlGrafico);
            this.grpGrafico.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.grpGrafico.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpGrafico.Location = new System.Drawing.Point(640, 160);
            this.grpGrafico.Size = new System.Drawing.Size(460, 395);
            this.grpGrafico.TabStop = false;
            this.grpGrafico.Text = "Gráfico";
            this.grpGrafico.Name = "grpGrafico";
            this.grpGrafico.TabIndex = 17;
            // 
            // pnlGrafico
            // 
            this.pnlGrafico.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.pnlGrafico.Location = new System.Drawing.Point(10, 25);
            this.pnlGrafico.Size = new System.Drawing.Size(440, 360);
            this.pnlGrafico.Name = "pnlGrafico";
            this.pnlGrafico.TabIndex = 18;
            // 
            // lblResumen
            // 
            this.lblResumen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResumen.Location = new System.Drawing.Point(20, 570);
            this.lblResumen.Text = "";
            this.lblResumen.AutoSize = true;
            this.lblResumen.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblResumen.Name = "lblResumen";
            this.lblResumen.TabIndex = 19;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 612);
            this.btnVolver.Size = new System.Drawing.Size(110, 34);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 20;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnImprimir.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Location = new System.Drawing.Point(970, 608);
            this.btnImprimir.Size = new System.Drawing.Size(130, 38);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.TabIndex = 21;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // FormProductosMasVendidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 660);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.lblResumen);
            this.Controls.Add(this.grpGrafico);
            this.Controls.Add(this.dgvRanking);
            this.Controls.Add(this.grpFiltros);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormProductosMasVendidos";
            this.Text = "Productos más vendidos";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormProductosMasVendidos_FormClosed);
            this.Load += new System.EventHandler(this.FormProductosMasVendidos_Load);
            this.grpFiltros.ResumeLayout(false);
            this.grpFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRanking)).EndInit();
            this.grpGrafico.ResumeLayout(false);
            this.grpGrafico.PerformLayout();
            this.pnlGrafico.ResumeLayout(false);
            this.pnlGrafico.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grpFiltros;
        private System.Windows.Forms.Label lblPeriodo;
        private System.Windows.Forms.ComboBox cmbPeriodo;
        private System.Windows.Forms.Label lblAnio;
        private System.Windows.Forms.NumericUpDown nudAnio;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label lblOrden;
        private System.Windows.Forms.RadioButton rbUnidades;
        private System.Windows.Forms.RadioButton rbMonto;
        private System.Windows.Forms.Label lblMostrar;
        private System.Windows.Forms.ComboBox cmbTop;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.DataGridView dgvRanking;
        private System.Windows.Forms.GroupBox grpGrafico;
        private System.Windows.Forms.Panel pnlGrafico;
        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnImprimir;
    }
}
