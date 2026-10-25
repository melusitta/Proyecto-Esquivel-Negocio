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
            this.lblCotizacion = new System.Windows.Forms.Label();
            this.cmbCotizacion = new System.Windows.Forms.ComboBox();
            this.btnRegistrarCotizacion = new System.Windows.Forms.Button();
            this.lblInfoCotizacion = new System.Windows.Forms.Label();
            this.lblEstadoProveedor = new System.Windows.Forms.Label();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.lblItems = new System.Windows.Forms.Label();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.lblTotalOrden = new System.Windows.Forms.Label();
            this.lblFechaEmision = new System.Windows.Forms.Label();
            this.dtpFechaEmision = new System.Windows.Forms.DateTimePicker();
            this.lblEntrega = new System.Windows.Forms.Label();
            this.txtEntrega = new System.Windows.Forms.TextBox();
            this.lblPago = new System.Windows.Forms.Label();
            this.txtPago = new System.Windows.Forms.TextBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnOrdenesSinPagar = new System.Windows.Forms.Button();
            this.btnGenerar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
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
            // lblCotizacion
            // 
            this.lblCotizacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCotizacion.Location = new System.Drawing.Point(20, 65);
            this.lblCotizacion.Text = "Cotización:";
            this.lblCotizacion.AutoSize = false;
            this.lblCotizacion.Size = new System.Drawing.Size(110, 25);
            this.lblCotizacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCotizacion.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblCotizacion.Name = "lblCotizacion";
            this.lblCotizacion.TabIndex = 1;
            // 
            // cmbCotizacion
            // 
            this.cmbCotizacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCotizacion.Location = new System.Drawing.Point(135, 65);
            this.cmbCotizacion.Size = new System.Drawing.Size(600, 25);
            this.cmbCotizacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCotizacion.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.cmbCotizacion.Name = "cmbCotizacion";
            this.cmbCotizacion.TabIndex = 2;
            this.cmbCotizacion.SelectedIndexChanged += new System.EventHandler(this.cmbCotizacion_SelectedIndexChanged);
            // 
            // btnRegistrarCotizacion
            // 
            this.btnRegistrarCotizacion.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnRegistrarCotizacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarCotizacion.Location = new System.Drawing.Point(860, 61);
            this.btnRegistrarCotizacion.Size = new System.Drawing.Size(220, 33);
            this.btnRegistrarCotizacion.Text = "Registrar cotización";
            this.btnRegistrarCotizacion.UseVisualStyleBackColor = true;
            this.btnRegistrarCotizacion.Name = "btnRegistrarCotizacion";
            this.btnRegistrarCotizacion.TabIndex = 3;
            this.btnRegistrarCotizacion.Click += new System.EventHandler(this.btnRegistrarCotizacion_Click);
            // 
            // lblInfoCotizacion
            // 
            this.lblInfoCotizacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoCotizacion.Location = new System.Drawing.Point(135, 97);
            this.lblInfoCotizacion.Text = "";
            this.lblInfoCotizacion.AutoSize = false;
            this.lblInfoCotizacion.Size = new System.Drawing.Size(945, 22);
            this.lblInfoCotizacion.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblInfoCotizacion.Name = "lblInfoCotizacion";
            this.lblInfoCotizacion.TabIndex = 4;
            // 
            // lblEstadoProveedor
            // 
            this.lblEstadoProveedor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoProveedor.Location = new System.Drawing.Point(20, 130);
            this.lblEstadoProveedor.Text = "";
            this.lblEstadoProveedor.AutoSize = false;
            this.lblEstadoProveedor.Size = new System.Drawing.Size(820, 22);
            this.lblEstadoProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblEstadoProveedor.Name = "lblEstadoProveedor";
            this.lblEstadoProveedor.TabIndex = 5;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.btnRegistrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrar.Location = new System.Drawing.Point(960, 124);
            this.btnRegistrar.Size = new System.Drawing.Size(120, 33);
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.TabIndex = 6;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // lblItems
            // 
            this.lblItems.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItems.Location = new System.Drawing.Point(20, 165);
            this.lblItems.Text = "Productos de la cotización, con las cantidades y precios acordados";
            this.lblItems.AutoSize = true;
            this.lblItems.Name = "lblItems";
            this.lblItems.TabIndex = 7;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeRows = false;
            this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvItems.Location = new System.Drawing.Point(20, 190);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.ReadOnly = true;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(1060, 270);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.TabIndex = 8;
            // 
            // lblTotalOrden
            // 
            this.lblTotalOrden.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOrden.Location = new System.Drawing.Point(20, 470);
            this.lblTotalOrden.Text = "";
            this.lblTotalOrden.AutoSize = true;
            this.lblTotalOrden.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblTotalOrden.Name = "lblTotalOrden";
            this.lblTotalOrden.TabIndex = 9;
            // 
            // lblFechaEmision
            // 
            this.lblFechaEmision.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaEmision.Location = new System.Drawing.Point(20, 508);
            this.lblFechaEmision.Text = "Fecha de emisión:";
            this.lblFechaEmision.AutoSize = false;
            this.lblFechaEmision.Size = new System.Drawing.Size(170, 25);
            this.lblFechaEmision.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFechaEmision.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblFechaEmision.Name = "lblFechaEmision";
            this.lblFechaEmision.TabIndex = 10;
            // 
            // dtpFechaEmision
            // 
            this.dtpFechaEmision.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaEmision.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaEmision.Location = new System.Drawing.Point(200, 508);
            this.dtpFechaEmision.Size = new System.Drawing.Size(140, 25);
            this.dtpFechaEmision.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.dtpFechaEmision.Name = "dtpFechaEmision";
            this.dtpFechaEmision.TabIndex = 11;
            // 
            // lblEntrega
            // 
            this.lblEntrega.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntrega.Location = new System.Drawing.Point(20, 545);
            this.lblEntrega.Text = "Condiciones de entrega:";
            this.lblEntrega.AutoSize = false;
            this.lblEntrega.Size = new System.Drawing.Size(170, 25);
            this.lblEntrega.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEntrega.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblEntrega.Name = "lblEntrega";
            this.lblEntrega.TabIndex = 12;
            // 
            // txtEntrega
            // 
            this.txtEntrega.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntrega.Location = new System.Drawing.Point(200, 545);
            this.txtEntrega.Size = new System.Drawing.Size(880, 25);
            this.txtEntrega.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtEntrega.MaxLength = 200;
            this.txtEntrega.Name = "txtEntrega";
            this.txtEntrega.TabIndex = 13;
            // 
            // lblPago
            // 
            this.lblPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPago.Location = new System.Drawing.Point(20, 582);
            this.lblPago.Text = "Condiciones de pago:";
            this.lblPago.AutoSize = false;
            this.lblPago.Size = new System.Drawing.Size(170, 25);
            this.lblPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPago.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblPago.Name = "lblPago";
            this.lblPago.TabIndex = 14;
            // 
            // txtPago
            // 
            this.txtPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPago.Location = new System.Drawing.Point(200, 582);
            this.txtPago.Size = new System.Drawing.Size(880, 25);
            this.txtPago.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtPago.MaxLength = 200;
            this.txtPago.Name = "txtPago";
            this.txtPago.TabIndex = 15;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 646);
            this.btnVolver.Size = new System.Drawing.Size(110, 34);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 16;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnOrdenesSinPagar
            // 
            this.btnOrdenesSinPagar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnOrdenesSinPagar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrdenesSinPagar.Location = new System.Drawing.Point(150, 646);
            this.btnOrdenesSinPagar.Size = new System.Drawing.Size(240, 34);
            this.btnOrdenesSinPagar.Text = "Órdenes sin pagar";
            this.btnOrdenesSinPagar.UseVisualStyleBackColor = true;
            this.btnOrdenesSinPagar.Name = "btnOrdenesSinPagar";
            this.btnOrdenesSinPagar.TabIndex = 17;
            this.btnOrdenesSinPagar.Click += new System.EventHandler(this.btnOrdenesSinPagar_Click);
            // 
            // btnGenerar
            // 
            this.btnGenerar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnGenerar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerar.Location = new System.Drawing.Point(870, 640);
            this.btnGenerar.Size = new System.Drawing.Size(210, 40);
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.TabIndex = 18;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // FormGenerarOrdenCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.btnOrdenesSinPagar);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.txtPago);
            this.Controls.Add(this.lblPago);
            this.Controls.Add(this.txtEntrega);
            this.Controls.Add(this.lblEntrega);
            this.Controls.Add(this.dtpFechaEmision);
            this.Controls.Add(this.lblFechaEmision);
            this.Controls.Add(this.lblTotalOrden);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.lblItems);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.lblEstadoProveedor);
            this.Controls.Add(this.lblInfoCotizacion);
            this.Controls.Add(this.btnRegistrarCotizacion);
            this.Controls.Add(this.cmbCotizacion);
            this.Controls.Add(this.lblCotizacion);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormGenerarOrdenCompra";
            this.Text = "Generar orden de compra";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormGenerarOrdenCompra_FormClosed);
            this.Load += new System.EventHandler(this.FormGenerarOrdenCompra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblCotizacion;
        private System.Windows.Forms.ComboBox cmbCotizacion;
        private System.Windows.Forms.Button btnRegistrarCotizacion;
        private System.Windows.Forms.Label lblInfoCotizacion;
        private System.Windows.Forms.Label lblEstadoProveedor;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Label lblTotalOrden;
        private System.Windows.Forms.Label lblFechaEmision;
        private System.Windows.Forms.DateTimePicker dtpFechaEmision;
        private System.Windows.Forms.Label lblEntrega;
        private System.Windows.Forms.TextBox txtEntrega;
        private System.Windows.Forms.Label lblPago;
        private System.Windows.Forms.TextBox txtPago;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnOrdenesSinPagar;
        private System.Windows.Forms.Button btnGenerar;
    }
}
