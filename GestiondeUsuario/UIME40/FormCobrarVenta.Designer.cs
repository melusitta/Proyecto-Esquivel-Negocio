namespace GestiondeUsuario
{
    partial class FormCobrarVenta
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
            this.lblFactura = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.grpPago = new System.Windows.Forms.GroupBox();
            this.lblFormaPago = new System.Windows.Forms.Label();
            this.cmbFormaPago = new System.Windows.Forms.ComboBox();
            this.lblMonto = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.chkAcreditado = new System.Windows.Forms.CheckBox();
            this.pnlTarjeta = new System.Windows.Forms.Panel();
            this.lblNumeroTarjeta = new System.Windows.Forms.Label();
            this.txtNumeroTarjeta = new System.Windows.Forms.TextBox();
            this.lblTitular = new System.Windows.Forms.Label();
            this.txtTitular = new System.Windows.Forms.TextBox();
            this.lblVencimiento = new System.Windows.Forms.Label();
            this.txtVencimiento = new System.Windows.Forms.TextBox();
            this.lblCVV = new System.Windows.Forms.Label();
            this.txtCVV = new System.Windows.Forms.TextBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnCobrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.grpPago.SuspendLayout();
            this.pnlTarjeta.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Text = "Cobrar venta";
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblFactura
            // 
            this.lblFactura.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFactura.Location = new System.Drawing.Point(20, 62);
            this.lblFactura.Text = "";
            this.lblFactura.AutoSize = true;
            this.lblFactura.Name = "lblFactura";
            this.lblFactura.TabIndex = 1;
            // 
            // lblFecha
            // 
            this.lblFecha.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(20, 90);
            this.lblFecha.Text = "";
            this.lblFecha.AutoSize = true;
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.TabIndex = 2;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeRows = false;
            this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvItems.Location = new System.Drawing.Point(20, 120);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.ReadOnly = true;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(600, 200);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.TabIndex = 3;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(20, 332);
            this.lblTotal.Text = "Total: $ 0,00";
            this.lblTotal.AutoSize = true;
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.TabIndex = 4;
            // 
            // grpPago
            // 
            this.grpPago.Controls.Add(this.lblFormaPago);
            this.grpPago.Controls.Add(this.cmbFormaPago);
            this.grpPago.Controls.Add(this.lblMonto);
            this.grpPago.Controls.Add(this.txtMonto);
            this.grpPago.Controls.Add(this.chkAcreditado);
            this.grpPago.Controls.Add(this.pnlTarjeta);
            this.grpPago.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.grpPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPago.Location = new System.Drawing.Point(20, 370);
            this.grpPago.Size = new System.Drawing.Size(600, 200);
            this.grpPago.TabStop = false;
            this.grpPago.Text = "Pago";
            this.grpPago.Name = "grpPago";
            this.grpPago.TabIndex = 5;
            // 
            // lblFormaPago
            // 
            this.lblFormaPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormaPago.Location = new System.Drawing.Point(10, 32);
            this.lblFormaPago.Text = "Forma de pago:";
            this.lblFormaPago.AutoSize = false;
            this.lblFormaPago.Size = new System.Drawing.Size(110, 25);
            this.lblFormaPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFormaPago.Name = "lblFormaPago";
            this.lblFormaPago.TabIndex = 6;
            // 
            // cmbFormaPago
            // 
            this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormaPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFormaPago.Location = new System.Drawing.Point(125, 32);
            this.cmbFormaPago.Size = new System.Drawing.Size(180, 25);
            this.cmbFormaPago.Name = "cmbFormaPago";
            this.cmbFormaPago.TabIndex = 7;
            this.cmbFormaPago.SelectedIndexChanged += new System.EventHandler(this.cmbFormaPago_SelectedIndexChanged);
            // 
            // lblMonto
            // 
            this.lblMonto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonto.Location = new System.Drawing.Point(10, 72);
            this.lblMonto.Text = "Monto:";
            this.lblMonto.AutoSize = false;
            this.lblMonto.Size = new System.Drawing.Size(110, 25);
            this.lblMonto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.TabIndex = 8;
            // 
            // txtMonto
            // 
            this.txtMonto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMonto.Location = new System.Drawing.Point(125, 72);
            this.txtMonto.Size = new System.Drawing.Size(180, 25);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.TabIndex = 9;
            // 
            // chkAcreditado
            // 
            this.chkAcreditado.AutoSize = true;
            this.chkAcreditado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAcreditado.Location = new System.Drawing.Point(330, 34);
            this.chkAcreditado.Text = "El banco confirmó el pago";
            this.chkAcreditado.UseVisualStyleBackColor = true;
            this.chkAcreditado.Name = "chkAcreditado";
            this.chkAcreditado.TabIndex = 10;
            // 
            // pnlTarjeta
            // 
            this.pnlTarjeta.Controls.Add(this.lblNumeroTarjeta);
            this.pnlTarjeta.Controls.Add(this.txtNumeroTarjeta);
            this.pnlTarjeta.Controls.Add(this.lblTitular);
            this.pnlTarjeta.Controls.Add(this.txtTitular);
            this.pnlTarjeta.Controls.Add(this.lblVencimiento);
            this.pnlTarjeta.Controls.Add(this.txtVencimiento);
            this.pnlTarjeta.Controls.Add(this.lblCVV);
            this.pnlTarjeta.Controls.Add(this.txtCVV);
            this.pnlTarjeta.Location = new System.Drawing.Point(10, 110);
            this.pnlTarjeta.Size = new System.Drawing.Size(580, 78);
            this.pnlTarjeta.Visible = false;
            this.pnlTarjeta.Name = "pnlTarjeta";
            this.pnlTarjeta.TabIndex = 11;
            // 
            // lblNumeroTarjeta
            // 
            this.lblNumeroTarjeta.AutoSize = false;
            this.lblNumeroTarjeta.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroTarjeta.Location = new System.Drawing.Point(0, 4);
            this.lblNumeroTarjeta.Size = new System.Drawing.Size(110, 25);
            this.lblNumeroTarjeta.Text = "N° de tarjeta:";
            this.lblNumeroTarjeta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblNumeroTarjeta.Name = "lblNumeroTarjeta";
            this.lblNumeroTarjeta.TabIndex = 12;
            // 
            // txtNumeroTarjeta
            // 
            this.txtNumeroTarjeta.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroTarjeta.Location = new System.Drawing.Point(115, 4);
            this.txtNumeroTarjeta.Size = new System.Drawing.Size(180, 25);
            this.txtNumeroTarjeta.MaxLength = 16;
            this.txtNumeroTarjeta.Name = "txtNumeroTarjeta";
            this.txtNumeroTarjeta.TabIndex = 13;
            this.txtNumeroTarjeta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSoloNumeros_KeyPress);
            // 
            // lblTitular
            // 
            this.lblTitular.AutoSize = false;
            this.lblTitular.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitular.Location = new System.Drawing.Point(300, 4);
            this.lblTitular.Size = new System.Drawing.Size(75, 25);
            this.lblTitular.Text = "Titular:";
            this.lblTitular.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTitular.Name = "lblTitular";
            this.lblTitular.TabIndex = 14;
            // 
            // txtTitular
            // 
            this.txtTitular.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitular.Location = new System.Drawing.Point(380, 4);
            this.txtTitular.Size = new System.Drawing.Size(190, 25);
            this.txtTitular.MaxLength = 60;
            this.txtTitular.Name = "txtTitular";
            this.txtTitular.TabIndex = 15;
            // 
            // lblVencimiento
            // 
            this.lblVencimiento.AutoSize = false;
            this.lblVencimiento.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVencimiento.Location = new System.Drawing.Point(0, 42);
            this.lblVencimiento.Size = new System.Drawing.Size(110, 25);
            this.lblVencimiento.Text = "Vencimiento:";
            this.lblVencimiento.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblVencimiento.Name = "lblVencimiento";
            this.lblVencimiento.TabIndex = 16;
            // 
            // txtVencimiento
            // 
            this.txtVencimiento.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVencimiento.Location = new System.Drawing.Point(115, 42);
            this.txtVencimiento.Size = new System.Drawing.Size(70, 25);
            this.txtVencimiento.MaxLength = 5;
            this.txtVencimiento.Name = "txtVencimiento";
            this.txtVencimiento.TabIndex = 17;
            this.txtVencimiento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVencimiento_KeyPress);
            // 
            // lblCVV
            // 
            this.lblCVV.AutoSize = false;
            this.lblCVV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCVV.Location = new System.Drawing.Point(190, 42);
            this.lblCVV.Size = new System.Drawing.Size(55, 25);
            this.lblCVV.Text = "CVV:";
            this.lblCVV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCVV.Name = "lblCVV";
            this.lblCVV.TabIndex = 18;
            // 
            // txtCVV
            // 
            this.txtCVV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCVV.Location = new System.Drawing.Point(250, 42);
            this.txtCVV.Size = new System.Drawing.Size(50, 25);
            this.txtCVV.MaxLength = 3;
            this.txtCVV.UseSystemPasswordChar = true;
            this.txtCVV.Name = "txtCVV";
            this.txtCVV.TabIndex = 19;
            this.txtCVV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSoloNumeros_KeyPress);
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 586);
            this.btnVolver.Size = new System.Drawing.Size(110, 36);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 20;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnCobrar
            // 
            this.btnCobrar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnCobrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCobrar.Location = new System.Drawing.Point(470, 582);
            this.btnCobrar.Size = new System.Drawing.Size(150, 40);
            this.btnCobrar.Text = "Cobrar";
            this.btnCobrar.UseVisualStyleBackColor = true;
            this.btnCobrar.Name = "btnCobrar";
            this.btnCobrar.TabIndex = 21;
            this.btnCobrar.Click += new System.EventHandler(this.btnCobrar_Click);
            // 
            // FormCobrarVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 635);
            this.Controls.Add(this.btnCobrar);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.grpPago);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblFactura);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormCobrarVenta";
            this.Text = "Cobrar venta";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCobrarVenta_FormClosed);
            this.Load += new System.EventHandler(this.FormCobrarVenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.grpPago.ResumeLayout(false);
            this.grpPago.PerformLayout();
            this.pnlTarjeta.ResumeLayout(false);
            this.pnlTarjeta.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblFactura;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.GroupBox grpPago;
        private System.Windows.Forms.Label lblFormaPago;
        private System.Windows.Forms.ComboBox cmbFormaPago;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.CheckBox chkAcreditado;
        private System.Windows.Forms.Panel pnlTarjeta;
        private System.Windows.Forms.Label lblNumeroTarjeta;
        private System.Windows.Forms.TextBox txtNumeroTarjeta;
        private System.Windows.Forms.Label lblTitular;
        private System.Windows.Forms.TextBox txtTitular;
        private System.Windows.Forms.Label lblVencimiento;
        private System.Windows.Forms.TextBox txtVencimiento;
        private System.Windows.Forms.Label lblCVV;
        private System.Windows.Forms.TextBox txtCVV;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnCobrar;
    }
}
