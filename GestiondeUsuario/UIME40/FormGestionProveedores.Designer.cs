namespace GestiondeUsuario
{
    partial class FormGestionProveedores
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
            this.dgvProveedores = new System.Windows.Forms.DataGridView();
            this.lblDatos = new System.Windows.Forms.Label();
            this.lblCUIT = new System.Windows.Forms.Label();
            this.txtCUIT = new System.Windows.Forms.TextBox();
            this.lblCBU = new System.Windows.Forms.Label();
            this.txtCBU = new System.Windows.Forms.TextBox();
            this.lblRazonSocial = new System.Windows.Forms.Label();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.lblBanco = new System.Windows.Forms.Label();
            this.txtBanco = new System.Windows.Forms.TextBox();
            this.lblContacto = new System.Windows.Forms.Label();
            this.txtContacto = new System.Windows.Forms.TextBox();
            this.lblCondiciones = new System.Windows.Forms.Label();
            this.txtCondiciones = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblEntrega = new System.Windows.Forms.Label();
            this.txtEntrega = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPlazo = new System.Windows.Forms.Label();
            this.txtPlazo = new System.Windows.Forms.TextBox();
            this.lblRubro = new System.Windows.Forms.Label();
            this.txtRubro = new System.Windows.Forms.TextBox();
            this.lblFormaPago = new System.Windows.Forms.Label();
            this.flpFormasPago = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Text = "Gestión de proveedores";
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblBuscar
            // 
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.Location = new System.Drawing.Point(20, 62);
            this.lblBuscar.Text = "Buscar:";
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.TabIndex = 1;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(90, 58);
            this.txtBuscar.Size = new System.Drawing.Size(320, 25);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.TabIndex = 2;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // dgvProveedores
            // 
            this.dgvProveedores.AllowUserToAddRows = false;
            this.dgvProveedores.AllowUserToDeleteRows = false;
            this.dgvProveedores.AllowUserToResizeRows = false;
            this.dgvProveedores.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvProveedores.MultiSelect = false;
            this.dgvProveedores.ReadOnly = true;
            this.dgvProveedores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProveedores.Location = new System.Drawing.Point(20, 92);
            this.dgvProveedores.Size = new System.Drawing.Size(960, 220);
            this.dgvProveedores.Name = "dgvProveedores";
            this.dgvProveedores.TabIndex = 3;
            this.dgvProveedores.SelectionChanged += new System.EventHandler(this.dgvProveedores_SelectionChanged);
            // 
            // lblDatos
            // 
            this.lblDatos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatos.Location = new System.Drawing.Point(20, 325);
            this.lblDatos.Text = "";
            this.lblDatos.AutoSize = false;
            this.lblDatos.Size = new System.Drawing.Size(960, 22);
            this.lblDatos.Name = "lblDatos";
            this.lblDatos.TabIndex = 4;
            // 
            // lblCUIT
            // 
            this.lblCUIT.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCUIT.Location = new System.Drawing.Point(20, 357);
            this.lblCUIT.Text = "CUIT:";
            this.lblCUIT.AutoSize = false;
            this.lblCUIT.Size = new System.Drawing.Size(160, 25);
            this.lblCUIT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCUIT.Name = "lblCUIT";
            this.lblCUIT.TabIndex = 5;
            // 
            // txtCUIT
            // 
            this.txtCUIT.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCUIT.Location = new System.Drawing.Point(185, 357);
            this.txtCUIT.Size = new System.Drawing.Size(290, 25);
            this.txtCUIT.MaxLength = 13;
            this.txtCUIT.ReadOnly = true;
            this.txtCUIT.Name = "txtCUIT";
            this.txtCUIT.TabIndex = 6;
            // 
            // lblCBU
            // 
            this.lblCBU.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCBU.Location = new System.Drawing.Point(490, 357);
            this.lblCBU.Text = "CBU:";
            this.lblCBU.AutoSize = false;
            this.lblCBU.Size = new System.Drawing.Size(195, 25);
            this.lblCBU.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCBU.Name = "lblCBU";
            this.lblCBU.TabIndex = 7;
            // 
            // txtCBU
            // 
            this.txtCBU.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCBU.Location = new System.Drawing.Point(690, 357);
            this.txtCBU.Size = new System.Drawing.Size(290, 25);
            this.txtCBU.MaxLength = 26;
            this.txtCBU.ReadOnly = true;
            this.txtCBU.Name = "txtCBU";
            this.txtCBU.TabIndex = 8;
            // 
            // lblRazonSocial
            // 
            this.lblRazonSocial.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRazonSocial.Location = new System.Drawing.Point(20, 393);
            this.lblRazonSocial.Text = "Razón social:";
            this.lblRazonSocial.AutoSize = false;
            this.lblRazonSocial.Size = new System.Drawing.Size(160, 25);
            this.lblRazonSocial.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRazonSocial.Name = "lblRazonSocial";
            this.lblRazonSocial.TabIndex = 9;
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRazonSocial.Location = new System.Drawing.Point(185, 393);
            this.txtRazonSocial.Size = new System.Drawing.Size(290, 25);
            this.txtRazonSocial.MaxLength = 150;
            this.txtRazonSocial.ReadOnly = true;
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.TabIndex = 10;
            // 
            // lblBanco
            // 
            this.lblBanco.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBanco.Location = new System.Drawing.Point(490, 393);
            this.lblBanco.Text = "Banco:";
            this.lblBanco.AutoSize = false;
            this.lblBanco.Size = new System.Drawing.Size(195, 25);
            this.lblBanco.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBanco.Name = "lblBanco";
            this.lblBanco.TabIndex = 11;
            // 
            // txtBanco
            // 
            this.txtBanco.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBanco.Location = new System.Drawing.Point(690, 393);
            this.txtBanco.Size = new System.Drawing.Size(290, 25);
            this.txtBanco.MaxLength = 100;
            this.txtBanco.ReadOnly = true;
            this.txtBanco.Name = "txtBanco";
            this.txtBanco.TabIndex = 12;
            // 
            // lblContacto
            // 
            this.lblContacto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContacto.Location = new System.Drawing.Point(20, 429);
            this.lblContacto.Text = "Contacto:";
            this.lblContacto.AutoSize = false;
            this.lblContacto.Size = new System.Drawing.Size(160, 25);
            this.lblContacto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblContacto.Name = "lblContacto";
            this.lblContacto.TabIndex = 13;
            // 
            // txtContacto
            // 
            this.txtContacto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContacto.Location = new System.Drawing.Point(185, 429);
            this.txtContacto.Size = new System.Drawing.Size(290, 25);
            this.txtContacto.MaxLength = 100;
            this.txtContacto.ReadOnly = true;
            this.txtContacto.Name = "txtContacto";
            this.txtContacto.TabIndex = 14;
            // 
            // lblCondiciones
            // 
            this.lblCondiciones.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCondiciones.Location = new System.Drawing.Point(490, 429);
            this.lblCondiciones.Text = "Condiciones comerciales:";
            this.lblCondiciones.AutoSize = false;
            this.lblCondiciones.Size = new System.Drawing.Size(195, 25);
            this.lblCondiciones.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCondiciones.Name = "lblCondiciones";
            this.lblCondiciones.TabIndex = 15;
            // 
            // txtCondiciones
            // 
            this.txtCondiciones.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCondiciones.Location = new System.Drawing.Point(690, 429);
            this.txtCondiciones.Size = new System.Drawing.Size(290, 25);
            this.txtCondiciones.MaxLength = 200;
            this.txtCondiciones.Name = "txtCondiciones";
            this.txtCondiciones.TabIndex = 16;
            // 
            // lblTelefono
            // 
            this.lblTelefono.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelefono.Location = new System.Drawing.Point(20, 465);
            this.lblTelefono.Text = "Teléfono:";
            this.lblTelefono.AutoSize = false;
            this.lblTelefono.Size = new System.Drawing.Size(160, 25);
            this.lblTelefono.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.TabIndex = 17;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefono.Location = new System.Drawing.Point(185, 465);
            this.txtTelefono.Size = new System.Drawing.Size(290, 25);
            this.txtTelefono.MaxLength = 20;
            this.txtTelefono.ReadOnly = true;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.TabIndex = 18;
            // 
            // lblEntrega
            // 
            this.lblEntrega.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntrega.Location = new System.Drawing.Point(490, 465);
            this.lblEntrega.Text = "Condiciones de entrega:";
            this.lblEntrega.AutoSize = false;
            this.lblEntrega.Size = new System.Drawing.Size(195, 25);
            this.lblEntrega.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEntrega.Name = "lblEntrega";
            this.lblEntrega.TabIndex = 19;
            // 
            // txtEntrega
            // 
            this.txtEntrega.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntrega.Location = new System.Drawing.Point(690, 465);
            this.txtEntrega.Size = new System.Drawing.Size(290, 25);
            this.txtEntrega.MaxLength = 200;
            this.txtEntrega.Name = "txtEntrega";
            this.txtEntrega.TabIndex = 20;
            // 
            // lblEmail
            // 
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(20, 501);
            this.lblEmail.Text = "Email:";
            this.lblEmail.AutoSize = false;
            this.lblEmail.Size = new System.Drawing.Size(160, 25);
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.TabIndex = 21;
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(185, 501);
            this.txtEmail.Size = new System.Drawing.Size(290, 25);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.TabIndex = 22;
            // 
            // lblPlazo
            // 
            this.lblPlazo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlazo.Location = new System.Drawing.Point(490, 501);
            this.lblPlazo.Text = "Plazo de entrega (días):";
            this.lblPlazo.AutoSize = false;
            this.lblPlazo.Size = new System.Drawing.Size(195, 25);
            this.lblPlazo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPlazo.Name = "lblPlazo";
            this.lblPlazo.TabIndex = 23;
            // 
            // txtPlazo
            // 
            this.txtPlazo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlazo.Location = new System.Drawing.Point(690, 501);
            this.txtPlazo.Size = new System.Drawing.Size(290, 25);
            this.txtPlazo.MaxLength = 4;
            this.txtPlazo.Name = "txtPlazo";
            this.txtPlazo.TabIndex = 24;
            // 
            // lblRubro
            // 
            this.lblRubro.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRubro.Location = new System.Drawing.Point(20, 537);
            this.lblRubro.Text = "Rubro:";
            this.lblRubro.AutoSize = false;
            this.lblRubro.Size = new System.Drawing.Size(160, 25);
            this.lblRubro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRubro.Name = "lblRubro";
            this.lblRubro.TabIndex = 25;
            // 
            // txtRubro
            // 
            this.txtRubro.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRubro.Location = new System.Drawing.Point(185, 537);
            this.txtRubro.Size = new System.Drawing.Size(290, 25);
            this.txtRubro.MaxLength = 100;
            this.txtRubro.ReadOnly = true;
            this.txtRubro.Name = "txtRubro";
            this.txtRubro.TabIndex = 26;
            // 
            // lblFormaPago
            // 
            this.lblFormaPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormaPago.Location = new System.Drawing.Point(0, 563);
            this.lblFormaPago.Text = "Formas de pago que acepta:";
            this.lblFormaPago.AutoSize = false;
            this.lblFormaPago.Size = new System.Drawing.Size(180, 46);
            this.lblFormaPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFormaPago.Name = "lblFormaPago";
            this.lblFormaPago.TabIndex = 27;
            // 
            // flpFormasPago
            // 
            this.flpFormasPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flpFormasPago.Location = new System.Drawing.Point(185, 571);
            this.flpFormasPago.Size = new System.Drawing.Size(795, 30);
            this.flpFormasPago.Name = "flpFormasPago";
            this.flpFormasPago.TabIndex = 28;
            // 
            // lblAyuda
            // 
            this.lblAyuda.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAyuda.Location = new System.Drawing.Point(185, 609);
            this.lblAyuda.Text = "";
            this.lblAyuda.AutoSize = true;
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.TabIndex = 29;
            // 
            // btnVolver
            // 
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 644);
            this.btnVolver.Text = "Volver";
            this.btnVolver.Size = new System.Drawing.Size(110, 34);
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 30;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(780, 640);
            this.btnGuardar.Text = "Guardar cambios";
            this.btnGuardar.Size = new System.Drawing.Size(200, 38);
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.TabIndex = 31;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // FormGestionProveedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 697);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.lblAyuda);
            this.Controls.Add(this.flpFormasPago);
            this.Controls.Add(this.lblFormaPago);
            this.Controls.Add(this.txtRubro);
            this.Controls.Add(this.lblRubro);
            this.Controls.Add(this.txtPlazo);
            this.Controls.Add(this.lblPlazo);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEntrega);
            this.Controls.Add(this.lblEntrega);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.lblTelefono);
            this.Controls.Add(this.txtCondiciones);
            this.Controls.Add(this.lblCondiciones);
            this.Controls.Add(this.txtContacto);
            this.Controls.Add(this.lblContacto);
            this.Controls.Add(this.txtBanco);
            this.Controls.Add(this.lblBanco);
            this.Controls.Add(this.txtRazonSocial);
            this.Controls.Add(this.lblRazonSocial);
            this.Controls.Add(this.txtCBU);
            this.Controls.Add(this.lblCBU);
            this.Controls.Add(this.txtCUIT);
            this.Controls.Add(this.lblCUIT);
            this.Controls.Add(this.lblDatos);
            this.Controls.Add(this.dgvProveedores);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormGestionProveedores";
            this.Text = "Gestión de proveedores";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormGestionProveedores_FormClosed);
            this.Load += new System.EventHandler(this.FormGestionProveedores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvProveedores;
        private System.Windows.Forms.Label lblDatos;
        private System.Windows.Forms.Label lblCUIT;
        private System.Windows.Forms.TextBox txtCUIT;
        private System.Windows.Forms.Label lblCBU;
        private System.Windows.Forms.TextBox txtCBU;
        private System.Windows.Forms.Label lblRazonSocial;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.Label lblBanco;
        private System.Windows.Forms.TextBox txtBanco;
        private System.Windows.Forms.Label lblContacto;
        private System.Windows.Forms.TextBox txtContacto;
        private System.Windows.Forms.Label lblCondiciones;
        private System.Windows.Forms.TextBox txtCondiciones;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblEntrega;
        private System.Windows.Forms.TextBox txtEntrega;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPlazo;
        private System.Windows.Forms.TextBox txtPlazo;
        private System.Windows.Forms.Label lblRubro;
        private System.Windows.Forms.TextBox txtRubro;
        private System.Windows.Forms.Label lblFormaPago;
        private System.Windows.Forms.FlowLayoutPanel flpFormasPago;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnGuardar;
    }
}
