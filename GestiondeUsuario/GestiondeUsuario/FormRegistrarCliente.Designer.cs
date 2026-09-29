namespace GestiondeUsuario
{
    partial class FormRegistrarCliente
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
            this.lblDatos = new System.Windows.Forms.Label();
            this.lblDNI = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblLocalidad = new System.Windows.Forms.Label();
            this.txtLocalidad = new System.Windows.Forms.TextBox();
            this.lblCodigoPostal = new System.Windows.Forms.Label();
            this.txtCodigoPostal = new System.Windows.Forms.TextBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.lblClientes = new System.Windows.Forms.Label();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.btnSerializar = new System.Windows.Forms.Button();
            this.btnDeserializar = new System.Windows.Forms.Button();
            this.lblDeserializado = new System.Windows.Forms.Label();
            this.lstDeserializado = new System.Windows.Forms.ListBox();
            this.btnVolver = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Text = "Registrar cliente";
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblDatos
            // 
            this.lblDatos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatos.Location = new System.Drawing.Point(20, 60);
            this.lblDatos.Text = "Datos del cliente";
            this.lblDatos.AutoSize = true;
            this.lblDatos.Name = "lblDatos";
            this.lblDatos.TabIndex = 1;
            // 
            // lblDNI
            // 
            this.lblDNI.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDNI.Location = new System.Drawing.Point(10, 90);
            this.lblDNI.Text = "DNI:";
            this.lblDNI.AutoSize = false;
            this.lblDNI.Size = new System.Drawing.Size(110, 25);
            this.lblDNI.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.TabIndex = 2;
            // 
            // txtDNI
            // 
            this.txtDNI.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDNI.Location = new System.Drawing.Point(130, 90);
            this.txtDNI.Size = new System.Drawing.Size(300, 25);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.TabIndex = 3;
            // 
            // lblNombre
            // 
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(10, 125);
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.AutoSize = false;
            this.lblNombre.Size = new System.Drawing.Size(110, 25);
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.TabIndex = 4;
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(130, 125);
            this.txtNombre.Size = new System.Drawing.Size(300, 25);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.TabIndex = 5;
            // 
            // lblApellido
            // 
            this.lblApellido.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApellido.Location = new System.Drawing.Point(10, 160);
            this.lblApellido.Text = "Apellido:";
            this.lblApellido.AutoSize = false;
            this.lblApellido.Size = new System.Drawing.Size(110, 25);
            this.lblApellido.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.TabIndex = 6;
            // 
            // txtApellido
            // 
            this.txtApellido.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApellido.Location = new System.Drawing.Point(130, 160);
            this.txtApellido.Size = new System.Drawing.Size(300, 25);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.TabIndex = 7;
            // 
            // lblTelefono
            // 
            this.lblTelefono.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelefono.Location = new System.Drawing.Point(10, 195);
            this.lblTelefono.Text = "Teléfono:";
            this.lblTelefono.AutoSize = false;
            this.lblTelefono.Size = new System.Drawing.Size(110, 25);
            this.lblTelefono.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.TabIndex = 8;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefono.Location = new System.Drawing.Point(130, 195);
            this.txtTelefono.Size = new System.Drawing.Size(300, 25);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.TabIndex = 9;
            // 
            // lblEmail
            // 
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(10, 230);
            this.lblEmail.Text = "Email:";
            this.lblEmail.AutoSize = false;
            this.lblEmail.Size = new System.Drawing.Size(110, 25);
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.TabIndex = 10;
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(130, 230);
            this.txtEmail.Size = new System.Drawing.Size(300, 25);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.TabIndex = 11;
            // 
            // lblDireccion
            // 
            this.lblDireccion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDireccion.Location = new System.Drawing.Point(10, 265);
            this.lblDireccion.Text = "Dirección:";
            this.lblDireccion.AutoSize = false;
            this.lblDireccion.Size = new System.Drawing.Size(110, 25);
            this.lblDireccion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.TabIndex = 12;
            // 
            // txtDireccion
            // 
            this.txtDireccion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDireccion.Location = new System.Drawing.Point(130, 265);
            this.txtDireccion.Size = new System.Drawing.Size(300, 25);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.TabIndex = 13;
            // 
            // lblLocalidad
            // 
            this.lblLocalidad.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalidad.Location = new System.Drawing.Point(10, 300);
            this.lblLocalidad.Text = "Localidad:";
            this.lblLocalidad.AutoSize = false;
            this.lblLocalidad.Size = new System.Drawing.Size(110, 25);
            this.lblLocalidad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLocalidad.Name = "lblLocalidad";
            this.lblLocalidad.TabIndex = 14;
            // 
            // txtLocalidad
            // 
            this.txtLocalidad.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocalidad.Location = new System.Drawing.Point(130, 300);
            this.txtLocalidad.Size = new System.Drawing.Size(300, 25);
            this.txtLocalidad.Name = "txtLocalidad";
            this.txtLocalidad.TabIndex = 15;
            // 
            // lblCodigoPostal
            // 
            this.lblCodigoPostal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoPostal.Location = new System.Drawing.Point(10, 335);
            this.lblCodigoPostal.Text = "Código postal:";
            this.lblCodigoPostal.AutoSize = false;
            this.lblCodigoPostal.Size = new System.Drawing.Size(110, 25);
            this.lblCodigoPostal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCodigoPostal.Name = "lblCodigoPostal";
            this.lblCodigoPostal.TabIndex = 16;
            // 
            // txtCodigoPostal
            // 
            this.txtCodigoPostal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoPostal.Location = new System.Drawing.Point(130, 335);
            this.txtCodigoPostal.Size = new System.Drawing.Size(300, 25);
            this.txtCodigoPostal.Name = "txtCodigoPostal";
            this.txtCodigoPostal.TabIndex = 17;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.btnRegistrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrar.Location = new System.Drawing.Point(250, 380);
            this.btnRegistrar.Size = new System.Drawing.Size(180, 40);
            this.btnRegistrar.Text = "Registrar cliente";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.TabIndex = 18;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // lblClientes
            // 
            this.lblClientes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientes.Location = new System.Drawing.Point(480, 60);
            this.lblClientes.Text = "Clientes registrados (seleccioná los que querés serializar)";
            this.lblClientes.AutoSize = false;
            this.lblClientes.Size = new System.Drawing.Size(600, 22);
            this.lblClientes.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblClientes.Name = "lblClientes";
            this.lblClientes.TabIndex = 19;
            // 
            // dgvClientes
            // 
            this.dgvClientes.AllowUserToAddRows = false;
            this.dgvClientes.AllowUserToDeleteRows = false;
            this.dgvClientes.AllowUserToResizeRows = false;
            this.dgvClientes.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvClientes.Location = new System.Drawing.Point(480, 90);
            this.dgvClientes.MultiSelect = true;
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientes.Size = new System.Drawing.Size(600, 290);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.TabIndex = 20;
            // 
            // btnSerializar
            // 
            this.btnSerializar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnSerializar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSerializar.Location = new System.Drawing.Point(480, 390);
            this.btnSerializar.Size = new System.Drawing.Size(150, 38);
            this.btnSerializar.Text = "Serializar";
            this.btnSerializar.UseVisualStyleBackColor = true;
            this.btnSerializar.Name = "btnSerializar";
            this.btnSerializar.TabIndex = 21;
            this.btnSerializar.Click += new System.EventHandler(this.btnSerializar_Click);
            // 
            // btnDeserializar
            // 
            this.btnDeserializar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnDeserializar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeserializar.Location = new System.Drawing.Point(640, 390);
            this.btnDeserializar.Size = new System.Drawing.Size(150, 38);
            this.btnDeserializar.Text = "Deserializar";
            this.btnDeserializar.UseVisualStyleBackColor = true;
            this.btnDeserializar.Name = "btnDeserializar";
            this.btnDeserializar.TabIndex = 22;
            this.btnDeserializar.Click += new System.EventHandler(this.btnDeserializar_Click);
            // 
            // lblDeserializado
            // 
            this.lblDeserializado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeserializado.Location = new System.Drawing.Point(480, 440);
            this.lblDeserializado.Text = "Archivo deserializado";
            this.lblDeserializado.AutoSize = true;
            this.lblDeserializado.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblDeserializado.Name = "lblDeserializado";
            this.lblDeserializado.TabIndex = 23;
            // 
            // lstDeserializado
            // 
            this.lstDeserializado.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lstDeserializado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDeserializado.FormattingEnabled = true;
            this.lstDeserializado.HorizontalScrollbar = true;
            this.lstDeserializado.IntegralHeight = false;
            this.lstDeserializado.ItemHeight = 15;
            this.lstDeserializado.Location = new System.Drawing.Point(480, 465);
            this.lstDeserializado.Size = new System.Drawing.Size(600, 130);
            this.lstDeserializado.Name = "lstDeserializado";
            this.lstDeserializado.TabIndex = 24;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 563);
            this.btnVolver.Size = new System.Drawing.Size(110, 36);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 25;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // FormRegistrarCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 620);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.lstDeserializado);
            this.Controls.Add(this.lblDeserializado);
            this.Controls.Add(this.btnDeserializar);
            this.Controls.Add(this.btnSerializar);
            this.Controls.Add(this.dgvClientes);
            this.Controls.Add(this.lblClientes);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.txtCodigoPostal);
            this.Controls.Add(this.lblCodigoPostal);
            this.Controls.Add(this.txtLocalidad);
            this.Controls.Add(this.lblLocalidad);
            this.Controls.Add(this.txtDireccion);
            this.Controls.Add(this.lblDireccion);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.lblTelefono);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.lblApellido);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtDNI);
            this.Controls.Add(this.lblDNI);
            this.Controls.Add(this.lblDatos);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormRegistrarCliente";
            this.Text = "Registrar cliente";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormRegistrarCliente_FormClosed);
            this.Load += new System.EventHandler(this.FormRegistrarCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDatos;
        private System.Windows.Forms.Label lblDNI;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label lblLocalidad;
        private System.Windows.Forms.TextBox txtLocalidad;
        private System.Windows.Forms.Label lblCodigoPostal;
        private System.Windows.Forms.TextBox txtCodigoPostal;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Label lblClientes;
        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.Button btnSerializar;
        private System.Windows.Forms.Button btnDeserializar;
        private System.Windows.Forms.Label lblDeserializado;
        private System.Windows.Forms.ListBox lstDeserializado;
        private System.Windows.Forms.Button btnVolver;
    }
}
