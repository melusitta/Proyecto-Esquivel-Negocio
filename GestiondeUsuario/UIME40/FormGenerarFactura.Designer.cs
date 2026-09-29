namespace GestiondeUsuario
{
    partial class FormGenerarFactura
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
            this.lblDNI = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblEstadoCliente = new System.Windows.Forms.Label();
            this.grpPendientes = new System.Windows.Forms.GroupBox();
            this.dgvPendientes = new System.Windows.Forms.DataGridView();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.grpCliente = new System.Windows.Forms.GroupBox();
            this.lblDatosCliente = new System.Windows.Forms.Label();
            this.btnRegistrarCliente = new System.Windows.Forms.Button();
            this.grpCarrito = new System.Windows.Forms.GroupBox();
            this.dgvCarrito = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.lblFactura = new System.Windows.Forms.Label();
            this.btnCobrar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.grpPendientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendientes)).BeginInit();
            this.grpCliente.SuspendLayout();
            this.grpCarrito.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Text = "Generar factura";
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblDNI
            // 
            this.lblDNI.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDNI.Location = new System.Drawing.Point(20, 64);
            this.lblDNI.Text = "DNI del cliente:";
            this.lblDNI.AutoSize = true;
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.TabIndex = 1;
            // 
            // txtDNI
            // 
            this.txtDNI.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDNI.Location = new System.Drawing.Point(150, 61);
            this.txtDNI.Size = new System.Drawing.Size(150, 25);
            this.txtDNI.MaxLength = 8;
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.TabIndex = 2;
            this.txtDNI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDNI_KeyPress);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(310, 58);
            this.btnBuscar.Size = new System.Drawing.Size(110, 32);
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblEstadoCliente
            // 
            this.lblEstadoCliente.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoCliente.Location = new System.Drawing.Point(435, 64);
            this.lblEstadoCliente.Text = "";
            this.lblEstadoCliente.AutoSize = true;
            this.lblEstadoCliente.Name = "lblEstadoCliente";
            this.lblEstadoCliente.TabIndex = 4;
            // 
            // grpPendientes
            // 
            this.grpPendientes.Controls.Add(this.dgvPendientes);
            this.grpPendientes.Controls.Add(this.btnActualizar);
            this.grpPendientes.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.grpPendientes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPendientes.Location = new System.Drawing.Point(20, 100);
            this.grpPendientes.Size = new System.Drawing.Size(420, 386);
            this.grpPendientes.TabStop = false;
            this.grpPendientes.Text = "Pendientes de caja";
            this.grpPendientes.Name = "grpPendientes";
            this.grpPendientes.TabIndex = 5;
            // 
            // dgvPendientes
            // 
            this.dgvPendientes.AllowUserToAddRows = false;
            this.dgvPendientes.AllowUserToDeleteRows = false;
            this.dgvPendientes.AllowUserToResizeRows = false;
            this.dgvPendientes.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvPendientes.Location = new System.Drawing.Point(12, 28);
            this.dgvPendientes.MultiSelect = false;
            this.dgvPendientes.ReadOnly = true;
            this.dgvPendientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPendientes.Size = new System.Drawing.Size(396, 306);
            this.dgvPendientes.Name = "dgvPendientes";
            this.dgvPendientes.TabIndex = 6;
            this.dgvPendientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPendientes_CellClick);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnActualizar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.Location = new System.Drawing.Point(12, 342);
            this.btnActualizar.Size = new System.Drawing.Size(150, 32);
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.TabIndex = 7;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // grpCliente
            // 
            this.grpCliente.Controls.Add(this.lblDatosCliente);
            this.grpCliente.Controls.Add(this.btnRegistrarCliente);
            this.grpCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.grpCliente.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCliente.Location = new System.Drawing.Point(460, 100);
            this.grpCliente.Size = new System.Drawing.Size(330, 330);
            this.grpCliente.TabStop = false;
            this.grpCliente.Text = "Cliente";
            this.grpCliente.Name = "grpCliente";
            this.grpCliente.TabIndex = 8;
            // 
            // lblDatosCliente
            // 
            this.lblDatosCliente.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatosCliente.Location = new System.Drawing.Point(15, 30);
            this.lblDatosCliente.Text = "";
            this.lblDatosCliente.AutoSize = false;
            this.lblDatosCliente.Size = new System.Drawing.Size(300, 200);
            this.lblDatosCliente.Name = "lblDatosCliente";
            this.lblDatosCliente.TabIndex = 9;
            // 
            // btnRegistrarCliente
            // 
            this.btnRegistrarCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnRegistrarCliente.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarCliente.Location = new System.Drawing.Point(15, 275);
            this.btnRegistrarCliente.Size = new System.Drawing.Size(300, 40);
            this.btnRegistrarCliente.Text = "Registrar cliente";
            this.btnRegistrarCliente.UseVisualStyleBackColor = true;
            this.btnRegistrarCliente.Name = "btnRegistrarCliente";
            this.btnRegistrarCliente.TabIndex = 10;
            this.btnRegistrarCliente.Click += new System.EventHandler(this.btnRegistrarCliente_Click);
            // 
            // grpCarrito
            // 
            this.grpCarrito.Controls.Add(this.dgvCarrito);
            this.grpCarrito.Controls.Add(this.lblTotal);
            this.grpCarrito.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.grpCarrito.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCarrito.Location = new System.Drawing.Point(810, 100);
            this.grpCarrito.Size = new System.Drawing.Size(470, 330);
            this.grpCarrito.TabStop = false;
            this.grpCarrito.Text = "Carrito";
            this.grpCarrito.Name = "grpCarrito";
            this.grpCarrito.TabIndex = 11;
            // 
            // dgvCarrito
            // 
            this.dgvCarrito.AllowUserToAddRows = false;
            this.dgvCarrito.AllowUserToDeleteRows = false;
            this.dgvCarrito.AllowUserToResizeRows = false;
            this.dgvCarrito.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvCarrito.Location = new System.Drawing.Point(12, 28);
            this.dgvCarrito.MultiSelect = false;
            this.dgvCarrito.ReadOnly = true;
            this.dgvCarrito.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCarrito.Size = new System.Drawing.Size(446, 250);
            this.dgvCarrito.Name = "dgvCarrito";
            this.dgvCarrito.TabIndex = 12;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(9, 290);
            this.lblTotal.Text = "Total: $ 0,00";
            this.lblTotal.AutoSize = true;
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.TabIndex = 13;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnGenerar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerar.Location = new System.Drawing.Point(460, 446);
            this.btnGenerar.Size = new System.Drawing.Size(220, 40);
            this.btnGenerar.Text = "Generar factura";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.TabIndex = 14;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // lblFactura
            // 
            this.lblFactura.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFactura.Location = new System.Drawing.Point(695, 455);
            this.lblFactura.Text = "";
            this.lblFactura.AutoSize = true;
            this.lblFactura.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblFactura.Name = "lblFactura";
            this.lblFactura.TabIndex = 15;
            // 
            // btnCobrar
            // 
            this.btnCobrar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnCobrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCobrar.Location = new System.Drawing.Point(1060, 446);
            this.btnCobrar.Size = new System.Drawing.Size(220, 40);
            this.btnCobrar.Text = "Cobrar venta";
            this.btnCobrar.UseVisualStyleBackColor = true;
            this.btnCobrar.Name = "btnCobrar";
            this.btnCobrar.TabIndex = 16;
            this.btnCobrar.Click += new System.EventHandler(this.btnCobrar_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 514);
            this.btnVolver.Size = new System.Drawing.Size(110, 34);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 17;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // FormGenerarFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 560);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnCobrar);
            this.Controls.Add(this.lblFactura);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.grpCarrito);
            this.Controls.Add(this.grpCliente);
            this.Controls.Add(this.grpPendientes);
            this.Controls.Add(this.lblEstadoCliente);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtDNI);
            this.Controls.Add(this.lblDNI);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormGenerarFactura";
            this.Text = "Generar factura";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormGenerarFactura_FormClosed);
            this.Load += new System.EventHandler(this.FormGenerarFactura_Load);
            this.grpPendientes.ResumeLayout(false);
            this.grpPendientes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendientes)).EndInit();
            this.grpCliente.ResumeLayout(false);
            this.grpCliente.PerformLayout();
            this.grpCarrito.ResumeLayout(false);
            this.grpCarrito.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDNI;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblEstadoCliente;
        private System.Windows.Forms.GroupBox grpPendientes;
        private System.Windows.Forms.DataGridView dgvPendientes;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.GroupBox grpCliente;
        private System.Windows.Forms.Label lblDatosCliente;
        private System.Windows.Forms.Button btnRegistrarCliente;
        private System.Windows.Forms.GroupBox grpCarrito;
        private System.Windows.Forms.DataGridView dgvCarrito;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Label lblFactura;
        private System.Windows.Forms.Button btnCobrar;
        private System.Windows.Forms.Button btnVolver;
    }
}
