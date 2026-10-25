namespace GestiondeUsuario
{
    partial class FormRealizarPreRegistro
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
            this.txtPlazo = new System.Windows.Forms.TextBox();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.lblOpcionales = new System.Windows.Forms.Label();
            this.lblFormaPago = new System.Windows.Forms.Label();
            this.cmbFormaPago = new System.Windows.Forms.ComboBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Text = "Realizar pre-registro";
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblCUIT
            // 
            this.lblCUIT.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCUIT.Location = new System.Drawing.Point(20, 70);
            this.lblCUIT.Text = "CUIT:";
            this.lblCUIT.AutoSize = false;
            this.lblCUIT.Size = new System.Drawing.Size(180, 25);
            this.lblCUIT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCUIT.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblCUIT.Name = "lblCUIT";
            this.lblCUIT.TabIndex = 1;
            // 
            // txtCUIT
            // 
            this.txtCUIT.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCUIT.Location = new System.Drawing.Point(210, 70);
            this.txtCUIT.Size = new System.Drawing.Size(150, 25);
            this.txtCUIT.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.txtCUIT.MaxLength = 13;
            this.txtCUIT.Name = "txtCUIT";
            this.txtCUIT.TabIndex = 2;
            // 
            // lblRazonSocial
            // 
            this.lblRazonSocial.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRazonSocial.Location = new System.Drawing.Point(20, 110);
            this.lblRazonSocial.Text = "Razón social:";
            this.lblRazonSocial.AutoSize = false;
            this.lblRazonSocial.Size = new System.Drawing.Size(180, 25);
            this.lblRazonSocial.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRazonSocial.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblRazonSocial.Name = "lblRazonSocial";
            this.lblRazonSocial.TabIndex = 3;
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRazonSocial.Location = new System.Drawing.Point(210, 110);
            this.txtRazonSocial.Size = new System.Drawing.Size(360, 25);
            this.txtRazonSocial.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtRazonSocial.MaxLength = 150;
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.TabIndex = 4;
            // 
            // lblContacto
            // 
            this.lblContacto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContacto.Location = new System.Drawing.Point(20, 150);
            this.lblContacto.Text = "Contacto:";
            this.lblContacto.AutoSize = false;
            this.lblContacto.Size = new System.Drawing.Size(180, 25);
            this.lblContacto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblContacto.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblContacto.Name = "lblContacto";
            this.lblContacto.TabIndex = 5;
            // 
            // txtContacto
            // 
            this.txtContacto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContacto.Location = new System.Drawing.Point(210, 150);
            this.txtContacto.Size = new System.Drawing.Size(360, 25);
            this.txtContacto.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtContacto.MaxLength = 100;
            this.txtContacto.Name = "txtContacto";
            this.txtContacto.TabIndex = 6;
            // 
            // lblTelefono
            // 
            this.lblTelefono.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelefono.Location = new System.Drawing.Point(20, 190);
            this.lblTelefono.Text = "Teléfono:";
            this.lblTelefono.AutoSize = false;
            this.lblTelefono.Size = new System.Drawing.Size(180, 25);
            this.lblTelefono.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTelefono.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.TabIndex = 7;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefono.Location = new System.Drawing.Point(210, 190);
            this.txtTelefono.Size = new System.Drawing.Size(360, 25);
            this.txtTelefono.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtTelefono.MaxLength = 30;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.TabIndex = 8;
            // 
            // lblEmail
            // 
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(20, 230);
            this.lblEmail.Text = "Email:";
            this.lblEmail.AutoSize = false;
            this.lblEmail.Size = new System.Drawing.Size(180, 25);
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.TabIndex = 9;
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(210, 230);
            this.txtEmail.Size = new System.Drawing.Size(360, 25);
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.TabIndex = 10;
            // 
            // lblCondiciones
            // 
            this.lblCondiciones.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCondiciones.Location = new System.Drawing.Point(20, 270);
            this.lblCondiciones.Text = "Condiciones comerciales:";
            this.lblCondiciones.AutoSize = false;
            this.lblCondiciones.Size = new System.Drawing.Size(180, 25);
            this.lblCondiciones.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCondiciones.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblCondiciones.Name = "lblCondiciones";
            this.lblCondiciones.TabIndex = 11;
            // 
            // txtCondiciones
            // 
            this.txtCondiciones.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCondiciones.Location = new System.Drawing.Point(210, 270);
            this.txtCondiciones.Size = new System.Drawing.Size(360, 25);
            this.txtCondiciones.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtCondiciones.MaxLength = 250;
            this.txtCondiciones.Name = "txtCondiciones";
            this.txtCondiciones.TabIndex = 12;
            // 
            // lblRubro
            // 
            this.lblRubro.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRubro.Location = new System.Drawing.Point(20, 345);
            this.lblRubro.Text = "Rubro:";
            this.lblRubro.AutoSize = false;
            this.lblRubro.Size = new System.Drawing.Size(180, 25);
            this.lblRubro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRubro.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblRubro.Name = "lblRubro";
            this.lblRubro.TabIndex = 13;
            // 
            // txtRubro
            // 
            this.txtRubro.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRubro.Location = new System.Drawing.Point(210, 345);
            this.txtRubro.Size = new System.Drawing.Size(360, 25);
            this.txtRubro.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtRubro.MaxLength = 100;
            this.txtRubro.Name = "txtRubro";
            this.txtRubro.TabIndex = 14;
            // 
            // lblEntrega
            // 
            this.lblEntrega.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntrega.Location = new System.Drawing.Point(20, 385);
            this.lblEntrega.Text = "Condiciones de entrega:";
            this.lblEntrega.AutoSize = false;
            this.lblEntrega.Size = new System.Drawing.Size(180, 25);
            this.lblEntrega.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEntrega.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblEntrega.Name = "lblEntrega";
            this.lblEntrega.TabIndex = 15;
            // 
            // txtEntrega
            // 
            this.txtEntrega.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntrega.Location = new System.Drawing.Point(210, 385);
            this.txtEntrega.Size = new System.Drawing.Size(360, 25);
            this.txtEntrega.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtEntrega.MaxLength = 200;
            this.txtEntrega.Name = "txtEntrega";
            this.txtEntrega.TabIndex = 16;
            // 
            // lblPlazo
            // 
            this.lblPlazo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlazo.Location = new System.Drawing.Point(20, 425);
            this.lblPlazo.Text = "Plazo de entrega (días):";
            this.lblPlazo.AutoSize = false;
            this.lblPlazo.Size = new System.Drawing.Size(180, 25);
            this.lblPlazo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPlazo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblPlazo.Name = "lblPlazo";
            this.lblPlazo.TabIndex = 17;
            // 
            // txtPlazo
            // 
            this.txtPlazo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlazo.Location = new System.Drawing.Point(210, 425);
            this.txtPlazo.Size = new System.Drawing.Size(80, 25);
            this.txtPlazo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.txtPlazo.MaxLength = 3;
            this.txtPlazo.Name = "txtPlazo";
            this.txtPlazo.TabIndex = 18;
            // 
            // lblAyuda
            // 
            this.lblAyuda.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAyuda.Location = new System.Drawing.Point(370, 74);
            this.lblAyuda.Text = "CUIT con o sin guiones (11 dígitos)";
            this.lblAyuda.AutoSize = true;
            this.lblAyuda.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.TabIndex = 19;
            // 
            // lblOpcionales
            // 
            this.lblOpcionales.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpcionales.Location = new System.Drawing.Point(20, 312);
            this.lblOpcionales.Text = "Datos comerciales (opcionales)";
            this.lblOpcionales.AutoSize = true;
            this.lblOpcionales.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblOpcionales.Name = "lblOpcionales";
            this.lblOpcionales.TabIndex = 20;
            // 
            // lblFormaPago
            // 
            this.lblFormaPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormaPago.Location = new System.Drawing.Point(20, 465);
            this.lblFormaPago.Text = "Forma de pago:";
            this.lblFormaPago.AutoSize = false;
            this.lblFormaPago.Size = new System.Drawing.Size(180, 25);
            this.lblFormaPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFormaPago.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblFormaPago.Name = "lblFormaPago";
            this.lblFormaPago.TabIndex = 21;
            // 
            // cmbFormaPago
            // 
            this.cmbFormaPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFormaPago.Location = new System.Drawing.Point(210, 465);
            this.cmbFormaPago.Size = new System.Drawing.Size(220, 25);
            this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormaPago.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.cmbFormaPago.Name = "cmbFormaPago";
            this.cmbFormaPago.TabIndex = 22;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 565);
            this.btnVolver.Size = new System.Drawing.Size(110, 34);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 23;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(390, 561);
            this.btnGuardar.Size = new System.Drawing.Size(180, 38);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.TabIndex = 24;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // FormRealizarPreRegistro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 620);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.cmbFormaPago);
            this.Controls.Add(this.lblFormaPago);
            this.Controls.Add(this.lblOpcionales);
            this.Controls.Add(this.lblAyuda);
            this.Controls.Add(this.txtPlazo);
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
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormRealizarPreRegistro";
            this.Text = "Realizar pre-registro";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormRealizarPreRegistro_FormClosed);
            this.Load += new System.EventHandler(this.FormRealizarPreRegistro_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
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
        private System.Windows.Forms.TextBox txtPlazo;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.Label lblOpcionales;
        private System.Windows.Forms.Label lblFormaPago;
        private System.Windows.Forms.ComboBox cmbFormaPago;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnGuardar;
    }
}
