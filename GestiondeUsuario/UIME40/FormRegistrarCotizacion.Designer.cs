namespace GestiondeUsuario
{
    partial class FormRegistrarCotizacion
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
            this.lblProveedor = new System.Windows.Forms.Label();
            this.cmbProveedor = new System.Windows.Forms.ComboBox();
            this.lblItems = new System.Windows.Forms.Label();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.lblPlazo = new System.Windows.Forms.Label();
            this.nudPlazo = new System.Windows.Forms.NumericUpDown();
            this.lblFormaPago = new System.Windows.Forms.Label();
            this.cmbFormaPago = new System.Windows.Forms.ComboBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnRegistrarCotizacion = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlazo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Text = "Registrar cotización";
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblSolicitud
            // 
            this.lblSolicitud.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSolicitud.Location = new System.Drawing.Point(20, 65);
            this.lblSolicitud.Text = "Solicitud:";
            this.lblSolicitud.AutoSize = false;
            this.lblSolicitud.Size = new System.Drawing.Size(100, 25);
            this.lblSolicitud.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSolicitud.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblSolicitud.Name = "lblSolicitud";
            this.lblSolicitud.TabIndex = 1;
            // 
            // cmbSolicitud
            // 
            this.cmbSolicitud.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSolicitud.Location = new System.Drawing.Point(125, 65);
            this.cmbSolicitud.Size = new System.Drawing.Size(300, 25);
            this.cmbSolicitud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSolicitud.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.cmbSolicitud.Name = "cmbSolicitud";
            this.cmbSolicitud.TabIndex = 2;
            this.cmbSolicitud.SelectedIndexChanged += new System.EventHandler(this.cmbSolicitud_SelectedIndexChanged);
            // 
            // lblProveedor
            // 
            this.lblProveedor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProveedor.Location = new System.Drawing.Point(440, 65);
            this.lblProveedor.Text = "Proveedor:";
            this.lblProveedor.AutoSize = false;
            this.lblProveedor.Size = new System.Drawing.Size(100, 25);
            this.lblProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.TabIndex = 3;
            // 
            // cmbProveedor
            // 
            this.cmbProveedor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProveedor.Location = new System.Drawing.Point(545, 65);
            this.cmbProveedor.Size = new System.Drawing.Size(355, 25);
            this.cmbProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.cmbProveedor.Name = "cmbProveedor";
            this.cmbProveedor.TabIndex = 4;
            this.cmbProveedor.SelectedIndexChanged += new System.EventHandler(this.cmbProveedor_SelectedIndexChanged);
            // 
            // lblItems
            // 
            this.lblItems.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItems.Location = new System.Drawing.Point(20, 105);
            this.lblItems.Text = "Productos que se le pidieron: tildá los que cotizó y cargá lo que informó";
            this.lblItems.AutoSize = true;
            this.lblItems.Name = "lblItems";
            this.lblItems.TabIndex = 5;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeRows = false;
            this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvItems.Location = new System.Drawing.Point(20, 130);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.ReadOnly = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(880, 300);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.TabIndex = 6;
            this.dgvItems.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvItems_DataError);
            // 
            // lblPlazo
            // 
            this.lblPlazo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlazo.Location = new System.Drawing.Point(20, 447);
            this.lblPlazo.Text = "Plazo de entrega (días):";
            this.lblPlazo.AutoSize = false;
            this.lblPlazo.Size = new System.Drawing.Size(180, 25);
            this.lblPlazo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPlazo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblPlazo.Name = "lblPlazo";
            this.lblPlazo.TabIndex = 7;
            // 
            // nudPlazo
            // 
            this.nudPlazo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.nudPlazo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPlazo.Location = new System.Drawing.Point(205, 447);
            this.nudPlazo.Size = new System.Drawing.Size(90, 25);
            this.nudPlazo.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudPlazo.Maximum = new decimal(new int[] { 365, 0, 0, 0 });
            this.nudPlazo.Name = "nudPlazo";
            this.nudPlazo.TabIndex = 8;
            // 
            // lblFormaPago
            // 
            this.lblFormaPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormaPago.Location = new System.Drawing.Point(320, 447);
            this.lblFormaPago.Text = "Forma de pago:";
            this.lblFormaPago.AutoSize = false;
            this.lblFormaPago.Size = new System.Drawing.Size(130, 25);
            this.lblFormaPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFormaPago.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblFormaPago.Name = "lblFormaPago";
            this.lblFormaPago.TabIndex = 9;
            // 
            // cmbFormaPago
            // 
            this.cmbFormaPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFormaPago.Location = new System.Drawing.Point(455, 447);
            this.cmbFormaPago.Size = new System.Drawing.Size(220, 25);
            this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormaPago.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.cmbFormaPago.Name = "cmbFormaPago";
            this.cmbFormaPago.TabIndex = 10;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 506);
            this.btnVolver.Size = new System.Drawing.Size(110, 34);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 11;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnRegistrarCotizacion
            // 
            this.btnRegistrarCotizacion.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnRegistrarCotizacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarCotizacion.Location = new System.Drawing.Point(680, 502);
            this.btnRegistrarCotizacion.Size = new System.Drawing.Size(220, 38);
            this.btnRegistrarCotizacion.Text = "Registrar cotización";
            this.btnRegistrarCotizacion.UseVisualStyleBackColor = true;
            this.btnRegistrarCotizacion.Name = "btnRegistrarCotizacion";
            this.btnRegistrarCotizacion.TabIndex = 12;
            this.btnRegistrarCotizacion.Click += new System.EventHandler(this.btnRegistrarCotizacion_Click);
            // 
            // FormRegistrarCotizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 560);
            this.Controls.Add(this.btnRegistrarCotizacion);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.cmbFormaPago);
            this.Controls.Add(this.lblFormaPago);
            this.Controls.Add(this.nudPlazo);
            this.Controls.Add(this.lblPlazo);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.lblItems);
            this.Controls.Add(this.cmbProveedor);
            this.Controls.Add(this.lblProveedor);
            this.Controls.Add(this.cmbSolicitud);
            this.Controls.Add(this.lblSolicitud);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormRegistrarCotizacion";
            this.Text = "Registrar cotización";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormRegistrarCotizacion_FormClosed);
            this.Load += new System.EventHandler(this.FormRegistrarCotizacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlazo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSolicitud;
        private System.Windows.Forms.ComboBox cmbSolicitud;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.ComboBox cmbProveedor;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Label lblPlazo;
        private System.Windows.Forms.NumericUpDown nudPlazo;
        private System.Windows.Forms.Label lblFormaPago;
        private System.Windows.Forms.ComboBox cmbFormaPago;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnRegistrarCotizacion;
    }
}
