namespace GestiondeUsuario
{
    partial class FormCargarCarrito
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
            this.btnSeleccionarProductos = new System.Windows.Forms.Button();
            this.lblCarrito = new System.Windows.Forms.Label();
            this.dgvCarrito = new System.Windows.Forms.DataGridView();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblDNI = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.btnAsociar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Text = "Cargar carrito";
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // btnSeleccionarProductos
            // 
            this.btnSeleccionarProductos.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.btnSeleccionarProductos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionarProductos.Location = new System.Drawing.Point(20, 60);
            this.btnSeleccionarProductos.Size = new System.Drawing.Size(240, 40);
            this.btnSeleccionarProductos.Text = "Seleccionar productos";
            this.btnSeleccionarProductos.UseVisualStyleBackColor = true;
            this.btnSeleccionarProductos.Name = "btnSeleccionarProductos";
            this.btnSeleccionarProductos.TabIndex = 1;
            this.btnSeleccionarProductos.Click += new System.EventHandler(this.btnSeleccionarProductos_Click);
            // 
            // lblCarrito
            // 
            this.lblCarrito.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCarrito.Location = new System.Drawing.Point(20, 115);
            this.lblCarrito.Text = "Carrito";
            this.lblCarrito.AutoSize = true;
            this.lblCarrito.Name = "lblCarrito";
            this.lblCarrito.TabIndex = 2;
            // 
            // dgvCarrito
            // 
            this.dgvCarrito.AllowUserToAddRows = false;
            this.dgvCarrito.AllowUserToDeleteRows = false;
            this.dgvCarrito.AllowUserToResizeRows = false;
            this.dgvCarrito.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvCarrito.Location = new System.Drawing.Point(20, 140);
            this.dgvCarrito.MultiSelect = false;
            this.dgvCarrito.ReadOnly = true;
            this.dgvCarrito.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCarrito.Size = new System.Drawing.Size(720, 225);
            this.dgvCarrito.Name = "dgvCarrito";
            this.dgvCarrito.TabIndex = 3;
            // 
            // btnQuitar
            // 
            this.btnQuitar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnQuitar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitar.Location = new System.Drawing.Point(20, 377);
            this.btnQuitar.Size = new System.Drawing.Size(170, 34);
            this.btnQuitar.Text = "Quitar producto";
            this.btnQuitar.UseVisualStyleBackColor = true;
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.TabIndex = 4;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(440, 380);
            this.lblTotal.Text = "Total: $ 0,00";
            this.lblTotal.AutoSize = false;
            this.lblTotal.Size = new System.Drawing.Size(300, 30);
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.TabIndex = 5;
            // 
            // lblDNI
            // 
            this.lblDNI.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDNI.Location = new System.Drawing.Point(20, 433);
            this.lblDNI.Text = "DNI del cliente:";
            this.lblDNI.AutoSize = true;
            this.lblDNI.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.TabIndex = 6;
            // 
            // txtDNI
            // 
            this.txtDNI.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDNI.Location = new System.Drawing.Point(145, 430);
            this.txtDNI.Size = new System.Drawing.Size(150, 25);
            this.txtDNI.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.txtDNI.MaxLength = 8;
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.TabIndex = 7;
            this.txtDNI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDNI_KeyPress);
            // 
            // btnAsociar
            // 
            this.btnAsociar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnAsociar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsociar.Location = new System.Drawing.Point(420, 425);
            this.btnAsociar.Size = new System.Drawing.Size(320, 40);
            this.btnAsociar.Text = "Asociar carrito y enviar a caja";
            this.btnAsociar.UseVisualStyleBackColor = true;
            this.btnAsociar.Name = "btnAsociar";
            this.btnAsociar.TabIndex = 8;
            this.btnAsociar.Click += new System.EventHandler(this.btnAsociar_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 481);
            this.btnVolver.Size = new System.Drawing.Size(110, 32);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 9;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // FormCargarCarrito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 525);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnAsociar);
            this.Controls.Add(this.txtDNI);
            this.Controls.Add(this.lblDNI);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.dgvCarrito);
            this.Controls.Add(this.lblCarrito);
            this.Controls.Add(this.btnSeleccionarProductos);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormCargarCarrito";
            this.Text = "Cargar carrito";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCargarCarrito_FormClosed);
            this.Load += new System.EventHandler(this.FormCargarCarrito_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnSeleccionarProductos;
        private System.Windows.Forms.Label lblCarrito;
        private System.Windows.Forms.DataGridView dgvCarrito;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblDNI;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Button btnAsociar;
        private System.Windows.Forms.Button btnVolver;
    }
}
