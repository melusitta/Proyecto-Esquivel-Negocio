namespace GestiondeUsuario
{
    partial class FormCompletarRegistroProveedor
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
            this.lblProveedores = new System.Windows.Forms.Label();
            this.dgvProveedores = new System.Windows.Forms.DataGridView();
            this.lblDatos = new System.Windows.Forms.Label();
            this.lblCUIT = new System.Windows.Forms.Label();
            this.txtCUIT = new System.Windows.Forms.TextBox();
            this.lblRazonSocial = new System.Windows.Forms.Label();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.lblContacto = new System.Windows.Forms.Label();
            this.txtContacto = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblCondiciones = new System.Windows.Forms.Label();
            this.txtCondiciones = new System.Windows.Forms.TextBox();
            this.lblRubro = new System.Windows.Forms.Label();
            this.txtRubro = new System.Windows.Forms.TextBox();
            this.lblEntrega = new System.Windows.Forms.Label();
            this.txtEntrega = new System.Windows.Forms.TextBox();
            this.lblPlazo = new System.Windows.Forms.Label();
            this.nudPlazo = new System.Windows.Forms.NumericUpDown();
            this.lblFormaPago = new System.Windows.Forms.Label();
            this.cmbFormaPago = new System.Windows.Forms.ComboBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnCompletar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlazo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Text = "Completar registro de proveedor";
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblProveedores
            // 
            this.lblProveedores.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProveedores.Location = new System.Drawing.Point(20, 60);
            this.lblProveedores.Text = "Proveedores (primero los pre-registrados)";
            this.lblProveedores.AutoSize = true;
            this.lblProveedores.Name = "lblProveedores";
            this.lblProveedores.TabIndex = 1;
            // 
            // dgvProveedores
            // 
            this.dgvProveedores.AllowUserToAddRows = false;
            this.dgvProveedores.AllowUserToDeleteRows = false;
            this.dgvProveedores.AllowUserToResizeRows = false;
            this.dgvProveedores.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.dgvProveedores.Location = new System.Drawing.Point(20, 85);
            this.dgvProveedores.MultiSelect = false;
            this.dgvProveedores.ReadOnly = true;
            this.dgvProveedores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProveedores.Size = new System.Drawing.Size(410, 450);
            this.dgvProveedores.Name = "dgvProveedores";
            this.dgvProveedores.TabIndex = 2;
            this.dgvProveedores.SelectionChanged += new System.EventHandler(this.dgvProveedores_SelectionChanged);
            // 
            // lblDatos
            // 
            this.lblDatos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatos.Location = new System.Drawing.Point(460, 60);
            this.lblDatos.Text = "Datos del proveedor";
            this.lblDatos.AutoSize = true;
            this.lblDatos.Name = "lblDatos";
            this.lblDatos.TabIndex = 3;
            // 
            // lblCUIT
            // 
            this.lblCUIT.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCUIT.Location = new System.Drawing.Point(450, 85);
            this.lblCUIT.Text = "CUIT:";
            this.lblCUIT.AutoSize = false;
            this.lblCUIT.Size = new System.Drawing.Size(180, 25);
            this.lblCUIT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCUIT.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblCUIT.Name = "lblCUIT";
            this.lblCUIT.TabIndex = 4;
            // 
            // txtCUIT
            // 
            this.txtCUIT.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCUIT.Location = new System.Drawing.Point(640, 85);
            this.txtCUIT.Size = new System.Drawing.Size(340, 25);
            this.txtCUIT.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtCUIT.MaxLength = 13;
            this.txtCUIT.Name = "txtCUIT";
            this.txtCUIT.TabIndex = 5;
            // 
            // lblRazonSocial
            // 
            this.lblRazonSocial.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRazonSocial.Location = new System.Drawing.Point(450, 125);
            this.lblRazonSocial.Text = "Razón social:";
            this.lblRazonSocial.AutoSize = false;
            this.lblRazonSocial.Size = new System.Drawing.Size(180, 25);
            this.lblRazonSocial.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRazonSocial.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblRazonSocial.Name = "lblRazonSocial";
            this.lblRazonSocial.TabIndex = 6;
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRazonSocial.Location = new System.Drawing.Point(640, 125);
            this.txtRazonSocial.Size = new System.Drawing.Size(340, 25);
            this.txtRazonSocial.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtRazonSocial.MaxLength = 150;
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.TabIndex = 7;
            // 
            // lblContacto
            // 
            this.lblContacto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContacto.Location = new System.Drawing.Point(450, 165);
            this.lblContacto.Text = "Contacto:";
            this.lblContacto.AutoSize = false;
            this.lblContacto.Size = new System.Drawing.Size(180, 25);
            this.lblContacto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblContacto.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblContacto.Name = "lblContacto";
            this.lblContacto.TabIndex = 8;
            // 
            // txtContacto
            // 
            this.txtContacto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContacto.Location = new System.Drawing.Point(640, 165);
            this.txtContacto.Size = new System.Drawing.Size(340, 25);
            this.txtContacto.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtContacto.MaxLength = 100;
            this.txtContacto.Name = "txtContacto";
            this.txtContacto.TabIndex = 9;
            // 
            // lblTelefono
            // 
            this.lblTelefono.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelefono.Location = new System.Drawing.Point(450, 205);
            this.lblTelefono.Text = "Teléfono:";
            this.lblTelefono.AutoSize = false;
            this.lblTelefono.Size = new System.Drawing.Size(180, 25);
            this.lblTelefono.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTelefono.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.TabIndex = 10;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefono.Location = new System.Drawing.Point(640, 205);
            this.txtTelefono.Size = new System.Drawing.Size(340, 25);
            this.txtTelefono.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtTelefono.MaxLength = 30;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.TabIndex = 11;
            // 
            // lblEmail
            // 
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(450, 245);
            this.lblEmail.Text = "Email:";
            this.lblEmail.AutoSize = false;
            this.lblEmail.Size = new System.Drawing.Size(180, 25);
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.TabIndex = 12;
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(640, 245);
            this.txtEmail.Size = new System.Drawing.Size(340, 25);
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.TabIndex = 13;
            // 
            // lblCondiciones
            // 
            this.lblCondiciones.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCondiciones.Location = new System.Drawing.Point(450, 285);
            this.lblCondiciones.Text = "Condiciones comerciales:";
            this.lblCondiciones.AutoSize = false;
            this.lblCondiciones.Size = new System.Drawing.Size(180, 25);
            this.lblCondiciones.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCondiciones.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblCondiciones.Name = "lblCondiciones";
            this.lblCondiciones.TabIndex = 14;
            // 
            // txtCondiciones
            // 
            this.txtCondiciones.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCondiciones.Location = new System.Drawing.Point(640, 285);
            this.txtCondiciones.Size = new System.Drawing.Size(340, 25);
            this.txtCondiciones.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtCondiciones.MaxLength = 250;
            this.txtCondiciones.Name = "txtCondiciones";
            this.txtCondiciones.TabIndex = 15;
            // 
            // lblRubro
            // 
            this.lblRubro.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRubro.Location = new System.Drawing.Point(450, 325);
            this.lblRubro.Text = "Rubro:";
            this.lblRubro.AutoSize = false;
            this.lblRubro.Size = new System.Drawing.Size(180, 25);
            this.lblRubro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRubro.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblRubro.Name = "lblRubro";
            this.lblRubro.TabIndex = 16;
            // 
            // txtRubro
            // 
            this.txtRubro.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRubro.Location = new System.Drawing.Point(640, 325);
            this.txtRubro.Size = new System.Drawing.Size(340, 25);
            this.txtRubro.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtRubro.MaxLength = 100;
            this.txtRubro.Name = "txtRubro";
            this.txtRubro.TabIndex = 17;
            // 
            // lblEntrega
            // 
            this.lblEntrega.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntrega.Location = new System.Drawing.Point(450, 365);
            this.lblEntrega.Text = "Condiciones de entrega:";
            this.lblEntrega.AutoSize = false;
            this.lblEntrega.Size = new System.Drawing.Size(180, 25);
            this.lblEntrega.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEntrega.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblEntrega.Name = "lblEntrega";
            this.lblEntrega.TabIndex = 18;
            // 
            // txtEntrega
            // 
            this.txtEntrega.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntrega.Location = new System.Drawing.Point(640, 365);
            this.txtEntrega.Size = new System.Drawing.Size(340, 25);
            this.txtEntrega.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtEntrega.MaxLength = 200;
            this.txtEntrega.Name = "txtEntrega";
            this.txtEntrega.TabIndex = 19;
            // 
            // lblPlazo
            // 
            this.lblPlazo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlazo.Location = new System.Drawing.Point(450, 405);
            this.lblPlazo.Text = "Plazo de entrega (días):";
            this.lblPlazo.AutoSize = false;
            this.lblPlazo.Size = new System.Drawing.Size(180, 25);
            this.lblPlazo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPlazo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblPlazo.Name = "lblPlazo";
            this.lblPlazo.TabIndex = 20;
            // 
            // nudPlazo
            // 
            this.nudPlazo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.nudPlazo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPlazo.Location = new System.Drawing.Point(640, 405);
            this.nudPlazo.Size = new System.Drawing.Size(100, 25);
            this.nudPlazo.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudPlazo.Maximum = new decimal(new int[] { 365, 0, 0, 0 });
            this.nudPlazo.Name = "nudPlazo";
            this.nudPlazo.TabIndex = 21;
            // 
            // lblFormaPago
            // 
            this.lblFormaPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormaPago.Location = new System.Drawing.Point(450, 445);
            this.lblFormaPago.Text = "Forma de pago:";
            this.lblFormaPago.AutoSize = false;
            this.lblFormaPago.Size = new System.Drawing.Size(180, 25);
            this.lblFormaPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFormaPago.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblFormaPago.Name = "lblFormaPago";
            this.lblFormaPago.TabIndex = 22;
            // 
            // cmbFormaPago
            // 
            this.cmbFormaPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFormaPago.Location = new System.Drawing.Point(640, 445);
            this.cmbFormaPago.Size = new System.Drawing.Size(220, 25);
            this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormaPago.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.cmbFormaPago.Name = "cmbFormaPago";
            this.cmbFormaPago.TabIndex = 23;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 550);
            this.btnVolver.Size = new System.Drawing.Size(110, 34);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 24;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnCompletar
            // 
            this.btnCompletar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnCompletar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompletar.Location = new System.Drawing.Point(740, 546);
            this.btnCompletar.Size = new System.Drawing.Size(240, 38);
            this.btnCompletar.Text = "Completar registro";
            this.btnCompletar.UseVisualStyleBackColor = true;
            this.btnCompletar.Name = "btnCompletar";
            this.btnCompletar.TabIndex = 25;
            this.btnCompletar.Click += new System.EventHandler(this.btnCompletar_Click);
            // 
            // FormCompletarRegistroProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.btnCompletar);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.cmbFormaPago);
            this.Controls.Add(this.lblFormaPago);
            this.Controls.Add(this.nudPlazo);
            this.Controls.Add(this.lblPlazo);
            this.Controls.Add(this.txtEntrega);
            this.Controls.Add(this.lblEntrega);
            this.Controls.Add(this.txtRubro);
            this.Controls.Add(this.lblRubro);
            this.Controls.Add(this.txtCondiciones);
            this.Controls.Add(this.lblCondiciones);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.lblTelefono);
            this.Controls.Add(this.txtContacto);
            this.Controls.Add(this.lblContacto);
            this.Controls.Add(this.txtRazonSocial);
            this.Controls.Add(this.lblRazonSocial);
            this.Controls.Add(this.txtCUIT);
            this.Controls.Add(this.lblCUIT);
            this.Controls.Add(this.lblDatos);
            this.Controls.Add(this.dgvProveedores);
            this.Controls.Add(this.lblProveedores);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormCompletarRegistroProveedor";
            this.Text = "Completar registro de proveedor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCompletarRegistroProveedor_FormClosed);
            this.Load += new System.EventHandler(this.FormCompletarRegistroProveedor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlazo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblProveedores;
        private System.Windows.Forms.DataGridView dgvProveedores;
        private System.Windows.Forms.Label lblDatos;
        private System.Windows.Forms.Label lblCUIT;
        private System.Windows.Forms.TextBox txtCUIT;
        private System.Windows.Forms.Label lblRazonSocial;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.Label lblContacto;
        private System.Windows.Forms.TextBox txtContacto;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblCondiciones;
        private System.Windows.Forms.TextBox txtCondiciones;
        private System.Windows.Forms.Label lblRubro;
        private System.Windows.Forms.TextBox txtRubro;
        private System.Windows.Forms.Label lblEntrega;
        private System.Windows.Forms.TextBox txtEntrega;
        private System.Windows.Forms.Label lblPlazo;
        private System.Windows.Forms.NumericUpDown nudPlazo;
        private System.Windows.Forms.Label lblFormaPago;
        private System.Windows.Forms.ComboBox cmbFormaPago;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnCompletar;
    }
}
