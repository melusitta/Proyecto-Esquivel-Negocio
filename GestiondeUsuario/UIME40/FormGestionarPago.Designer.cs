namespace GestiondeUsuario
{
    partial class FormGestionarPago
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
            this.lblOrdenes = new System.Windows.Forms.Label();
            this.dgvOrdenes = new System.Windows.Forms.DataGridView();
            this.lblItems = new System.Windows.Forms.Label();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.grpFactura = new System.Windows.Forms.GroupBox();
            this.lblNroFactura = new System.Windows.Forms.Label();
            this.txtNroFactura = new System.Windows.Forms.TextBox();
            this.lblFechaFactura = new System.Windows.Forms.Label();
            this.dtpFechaFactura = new System.Windows.Forms.DateTimePicker();
            this.lblImporte = new System.Windows.Forms.Label();
            this.nudImporte = new System.Windows.Forms.NumericUpDown();
            this.lblImpuestos = new System.Windows.Forms.Label();
            this.nudImpuestos = new System.Windows.Forms.NumericUpDown();
            this.grpPago = new System.Windows.Forms.GroupBox();
            this.lblTotalPagar = new System.Windows.Forms.Label();
            this.lblMonto = new System.Windows.Forms.Label();
            this.nudMonto = new System.Windows.Forms.NumericUpDown();
            this.lblFechaOperacion = new System.Windows.Forms.Label();
            this.dtpFechaOperacion = new System.Windows.Forms.DateTimePicker();
            this.lblComprobante = new System.Windows.Forms.Label();
            this.txtComprobante = new System.Windows.Forms.TextBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnRegistrarPago = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.grpFactura.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudImporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImpuestos)).BeginInit();
            this.grpPago.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonto)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Text = "Gestionar pago";
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblOrdenes
            // 
            this.lblOrdenes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrdenes.Location = new System.Drawing.Point(20, 60);
            this.lblOrdenes.Text = "Órdenes de compra enviadas, pendientes de pago";
            this.lblOrdenes.AutoSize = true;
            this.lblOrdenes.Name = "lblOrdenes";
            this.lblOrdenes.TabIndex = 1;
            // 
            // dgvOrdenes
            // 
            this.dgvOrdenes.AllowUserToAddRows = false;
            this.dgvOrdenes.AllowUserToDeleteRows = false;
            this.dgvOrdenes.AllowUserToResizeRows = false;
            this.dgvOrdenes.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvOrdenes.Location = new System.Drawing.Point(20, 85);
            this.dgvOrdenes.MultiSelect = false;
            this.dgvOrdenes.ReadOnly = true;
            this.dgvOrdenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdenes.Size = new System.Drawing.Size(1020, 170);
            this.dgvOrdenes.Name = "dgvOrdenes";
            this.dgvOrdenes.TabIndex = 2;
            this.dgvOrdenes.SelectionChanged += new System.EventHandler(this.dgvOrdenes_SelectionChanged);
            // 
            // lblItems
            // 
            this.lblItems.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItems.Location = new System.Drawing.Point(20, 270);
            this.lblItems.Text = "Detalle de la orden";
            this.lblItems.AutoSize = true;
            this.lblItems.Name = "lblItems";
            this.lblItems.TabIndex = 3;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeRows = false;
            this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvItems.Location = new System.Drawing.Point(20, 295);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.ReadOnly = true;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(500, 290);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.TabIndex = 4;
            // 
            // grpFactura
            // 
            this.grpFactura.Controls.Add(this.lblNroFactura);
            this.grpFactura.Controls.Add(this.txtNroFactura);
            this.grpFactura.Controls.Add(this.lblFechaFactura);
            this.grpFactura.Controls.Add(this.dtpFechaFactura);
            this.grpFactura.Controls.Add(this.lblImporte);
            this.grpFactura.Controls.Add(this.nudImporte);
            this.grpFactura.Controls.Add(this.lblImpuestos);
            this.grpFactura.Controls.Add(this.nudImpuestos);
            this.grpFactura.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.grpFactura.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFactura.Location = new System.Drawing.Point(540, 270);
            this.grpFactura.Size = new System.Drawing.Size(500, 160);
            this.grpFactura.TabStop = false;
            this.grpFactura.Text = "Factura del proveedor";
            this.grpFactura.Name = "grpFactura";
            this.grpFactura.TabIndex = 5;
            // 
            // lblNroFactura
            // 
            this.lblNroFactura.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNroFactura.Location = new System.Drawing.Point(10, 30);
            this.lblNroFactura.Text = "N° de factura:";
            this.lblNroFactura.AutoSize = false;
            this.lblNroFactura.Size = new System.Drawing.Size(140, 25);
            this.lblNroFactura.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblNroFactura.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblNroFactura.Name = "lblNroFactura";
            this.lblNroFactura.TabIndex = 6;
            // 
            // txtNroFactura
            // 
            this.txtNroFactura.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNroFactura.Location = new System.Drawing.Point(160, 30);
            this.txtNroFactura.Size = new System.Drawing.Size(200, 25);
            this.txtNroFactura.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.txtNroFactura.MaxLength = 30;
            this.txtNroFactura.Name = "txtNroFactura";
            this.txtNroFactura.TabIndex = 7;
            // 
            // lblFechaFactura
            // 
            this.lblFechaFactura.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaFactura.Location = new System.Drawing.Point(10, 62);
            this.lblFechaFactura.Text = "Fecha:";
            this.lblFechaFactura.AutoSize = false;
            this.lblFechaFactura.Size = new System.Drawing.Size(140, 25);
            this.lblFechaFactura.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFechaFactura.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblFechaFactura.Name = "lblFechaFactura";
            this.lblFechaFactura.TabIndex = 8;
            // 
            // dtpFechaFactura
            // 
            this.dtpFechaFactura.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFactura.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFactura.Location = new System.Drawing.Point(160, 62);
            this.dtpFechaFactura.Size = new System.Drawing.Size(140, 25);
            this.dtpFechaFactura.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.dtpFechaFactura.Name = "dtpFechaFactura";
            this.dtpFechaFactura.TabIndex = 9;
            // 
            // lblImporte
            // 
            this.lblImporte.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImporte.Location = new System.Drawing.Point(10, 94);
            this.lblImporte.Text = "Importe (sin imp.):";
            this.lblImporte.AutoSize = false;
            this.lblImporte.Size = new System.Drawing.Size(140, 25);
            this.lblImporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblImporte.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblImporte.Name = "lblImporte";
            this.lblImporte.TabIndex = 10;
            // 
            // nudImporte
            // 
            this.nudImporte.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.nudImporte.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudImporte.Location = new System.Drawing.Point(160, 94);
            this.nudImporte.Size = new System.Drawing.Size(150, 25);
            this.nudImporte.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudImporte.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            this.nudImporte.ThousandsSeparator = true;
            this.nudImporte.DecimalPlaces = 2;
            this.nudImporte.Name = "nudImporte";
            this.nudImporte.TabIndex = 11;
            this.nudImporte.ValueChanged += new System.EventHandler(this.Montos_ValueChanged);
            // 
            // lblImpuestos
            // 
            this.lblImpuestos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImpuestos.Location = new System.Drawing.Point(10, 126);
            this.lblImpuestos.Text = "Impuestos:";
            this.lblImpuestos.AutoSize = false;
            this.lblImpuestos.Size = new System.Drawing.Size(140, 25);
            this.lblImpuestos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblImpuestos.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblImpuestos.Name = "lblImpuestos";
            this.lblImpuestos.TabIndex = 12;
            // 
            // nudImpuestos
            // 
            this.nudImpuestos.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.nudImpuestos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudImpuestos.Location = new System.Drawing.Point(160, 126);
            this.nudImpuestos.Size = new System.Drawing.Size(150, 25);
            this.nudImpuestos.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudImpuestos.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            this.nudImpuestos.ThousandsSeparator = true;
            this.nudImpuestos.DecimalPlaces = 2;
            this.nudImpuestos.Name = "nudImpuestos";
            this.nudImpuestos.TabIndex = 13;
            this.nudImpuestos.ValueChanged += new System.EventHandler(this.Montos_ValueChanged);
            // 
            // grpPago
            // 
            this.grpPago.Controls.Add(this.lblTotalPagar);
            this.grpPago.Controls.Add(this.lblMonto);
            this.grpPago.Controls.Add(this.nudMonto);
            this.grpPago.Controls.Add(this.lblFechaOperacion);
            this.grpPago.Controls.Add(this.dtpFechaOperacion);
            this.grpPago.Controls.Add(this.lblComprobante);
            this.grpPago.Controls.Add(this.txtComprobante);
            this.grpPago.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.grpPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPago.Location = new System.Drawing.Point(540, 440);
            this.grpPago.Size = new System.Drawing.Size(500, 145);
            this.grpPago.TabStop = false;
            this.grpPago.Text = "Pago";
            this.grpPago.Name = "grpPago";
            this.grpPago.TabIndex = 14;
            // 
            // lblTotalPagar
            // 
            this.lblTotalPagar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPagar.Location = new System.Drawing.Point(10, 28);
            this.lblTotalPagar.Text = "";
            this.lblTotalPagar.AutoSize = false;
            this.lblTotalPagar.Size = new System.Drawing.Size(480, 22);
            this.lblTotalPagar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblTotalPagar.Name = "lblTotalPagar";
            this.lblTotalPagar.TabIndex = 15;
            // 
            // lblMonto
            // 
            this.lblMonto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonto.Location = new System.Drawing.Point(10, 55);
            this.lblMonto.Text = "Monto abonado:";
            this.lblMonto.AutoSize = false;
            this.lblMonto.Size = new System.Drawing.Size(140, 25);
            this.lblMonto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMonto.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.TabIndex = 16;
            // 
            // nudMonto
            // 
            this.nudMonto.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.nudMonto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMonto.Location = new System.Drawing.Point(160, 55);
            this.nudMonto.Size = new System.Drawing.Size(150, 25);
            this.nudMonto.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudMonto.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            this.nudMonto.ThousandsSeparator = true;
            this.nudMonto.DecimalPlaces = 2;
            this.nudMonto.Name = "nudMonto";
            this.nudMonto.TabIndex = 17;
            // 
            // lblFechaOperacion
            // 
            this.lblFechaOperacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaOperacion.Location = new System.Drawing.Point(10, 85);
            this.lblFechaOperacion.Text = "Fecha de operación:";
            this.lblFechaOperacion.AutoSize = false;
            this.lblFechaOperacion.Size = new System.Drawing.Size(140, 25);
            this.lblFechaOperacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFechaOperacion.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblFechaOperacion.Name = "lblFechaOperacion";
            this.lblFechaOperacion.TabIndex = 18;
            // 
            // dtpFechaOperacion
            // 
            this.dtpFechaOperacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaOperacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaOperacion.Location = new System.Drawing.Point(160, 85);
            this.dtpFechaOperacion.Size = new System.Drawing.Size(140, 25);
            this.dtpFechaOperacion.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.dtpFechaOperacion.Name = "dtpFechaOperacion";
            this.dtpFechaOperacion.TabIndex = 19;
            // 
            // lblComprobante
            // 
            this.lblComprobante.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComprobante.Location = new System.Drawing.Point(10, 115);
            this.lblComprobante.Text = "N° de comprobante:";
            this.lblComprobante.AutoSize = false;
            this.lblComprobante.Size = new System.Drawing.Size(140, 25);
            this.lblComprobante.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblComprobante.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblComprobante.Name = "lblComprobante";
            this.lblComprobante.TabIndex = 20;
            // 
            // txtComprobante
            // 
            this.txtComprobante.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComprobante.Location = new System.Drawing.Point(160, 115);
            this.txtComprobante.Size = new System.Drawing.Size(200, 25);
            this.txtComprobante.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.txtComprobante.MaxLength = 30;
            this.txtComprobante.Name = "txtComprobante";
            this.txtComprobante.TabIndex = 21;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 630);
            this.btnVolver.Size = new System.Drawing.Size(110, 34);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 22;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnRegistrarPago
            // 
            this.btnRegistrarPago.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnRegistrarPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarPago.Location = new System.Drawing.Point(800, 626);
            this.btnRegistrarPago.Size = new System.Drawing.Size(240, 38);
            this.btnRegistrarPago.Text = "Registrar pago";
            this.btnRegistrarPago.UseVisualStyleBackColor = true;
            this.btnRegistrarPago.Name = "btnRegistrarPago";
            this.btnRegistrarPago.TabIndex = 23;
            this.btnRegistrarPago.Click += new System.EventHandler(this.btnRegistrarPago_Click);
            // 
            // FormGestionarPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 680);
            this.Controls.Add(this.btnRegistrarPago);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.grpPago);
            this.Controls.Add(this.grpFactura);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.lblItems);
            this.Controls.Add(this.dgvOrdenes);
            this.Controls.Add(this.lblOrdenes);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormGestionarPago";
            this.Text = "Gestionar pago";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormGestionarPago_FormClosed);
            this.Load += new System.EventHandler(this.FormGestionarPago_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.grpFactura.ResumeLayout(false);
            this.grpFactura.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudImporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImpuestos)).EndInit();
            this.grpPago.ResumeLayout(false);
            this.grpPago.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblOrdenes;
        private System.Windows.Forms.DataGridView dgvOrdenes;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.GroupBox grpFactura;
        private System.Windows.Forms.Label lblNroFactura;
        private System.Windows.Forms.TextBox txtNroFactura;
        private System.Windows.Forms.Label lblFechaFactura;
        private System.Windows.Forms.DateTimePicker dtpFechaFactura;
        private System.Windows.Forms.Label lblImporte;
        private System.Windows.Forms.NumericUpDown nudImporte;
        private System.Windows.Forms.Label lblImpuestos;
        private System.Windows.Forms.NumericUpDown nudImpuestos;
        private System.Windows.Forms.GroupBox grpPago;
        private System.Windows.Forms.Label lblTotalPagar;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.NumericUpDown nudMonto;
        private System.Windows.Forms.Label lblFechaOperacion;
        private System.Windows.Forms.DateTimePicker dtpFechaOperacion;
        private System.Windows.Forms.Label lblComprobante;
        private System.Windows.Forms.TextBox txtComprobante;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnRegistrarPago;
    }
}
