namespace GestiondeUsuario
{
    partial class FormSerializarProductos
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
            this.lblOrigen = new System.Windows.Forms.Label();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.grpXml = new System.Windows.Forms.GroupBox();
            this.txtXml = new System.Windows.Forms.TextBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnDeserializar = new System.Windows.Forms.Button();
            this.btnSerializar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.grpXml.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Text = "Serializar productos";
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblOrigen
            // 
            this.lblOrigen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrigen.Location = new System.Drawing.Point(20, 55);
            this.lblOrigen.Text = "Mostrando: productos de la base de datos";
            this.lblOrigen.AutoSize = false;
            this.lblOrigen.Size = new System.Drawing.Size(1080, 22);
            this.lblOrigen.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblOrigen.Name = "lblOrigen";
            this.lblOrigen.TabIndex = 1;
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.AllowUserToResizeRows = false;
            this.dgvProductos.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvProductos.Location = new System.Drawing.Point(20, 85);
            this.dgvProductos.MultiSelect = false;
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(680, 480);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.TabIndex = 2;
            // 
            // grpXml
            // 
            this.grpXml.Controls.Add(this.txtXml);
            this.grpXml.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.grpXml.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpXml.Location = new System.Drawing.Point(715, 80);
            this.grpXml.Size = new System.Drawing.Size(385, 485);
            this.grpXml.TabStop = false;
            this.grpXml.Text = "Contenido del archivo XML";
            this.grpXml.Name = "grpXml";
            this.grpXml.TabIndex = 3;
            // 
            // txtXml
            // 
            this.txtXml.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtXml.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtXml.Location = new System.Drawing.Point(10, 25);
            this.txtXml.Multiline = true;
            this.txtXml.ReadOnly = true;
            this.txtXml.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtXml.Size = new System.Drawing.Size(365, 450);
            this.txtXml.WordWrap = false;
            this.txtXml.Name = "txtXml";
            this.txtXml.TabIndex = 4;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 590);
            this.btnVolver.Size = new System.Drawing.Size(110, 34);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 5;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Location = new System.Drawing.Point(700, 586);
            this.btnLimpiar.Size = new System.Drawing.Size(120, 38);
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.TabIndex = 6;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnDeserializar
            // 
            this.btnDeserializar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnDeserializar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeserializar.Location = new System.Drawing.Point(830, 586);
            this.btnDeserializar.Size = new System.Drawing.Size(130, 38);
            this.btnDeserializar.Text = "Deserializar";
            this.btnDeserializar.UseVisualStyleBackColor = true;
            this.btnDeserializar.Name = "btnDeserializar";
            this.btnDeserializar.TabIndex = 7;
            this.btnDeserializar.Click += new System.EventHandler(this.btnDeserializar_Click);
            // 
            // btnSerializar
            // 
            this.btnSerializar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnSerializar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSerializar.Location = new System.Drawing.Point(970, 586);
            this.btnSerializar.Size = new System.Drawing.Size(130, 38);
            this.btnSerializar.Text = "Serializar";
            this.btnSerializar.UseVisualStyleBackColor = true;
            this.btnSerializar.Name = "btnSerializar";
            this.btnSerializar.TabIndex = 8;
            this.btnSerializar.Click += new System.EventHandler(this.btnSerializar_Click);
            // 
            // FormSerializarProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 640);
            this.Controls.Add(this.btnSerializar);
            this.Controls.Add(this.btnDeserializar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.grpXml);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.lblOrigen);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormSerializarProductos";
            this.Text = "Serializar productos";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSerializarProductos_FormClosed);
            this.Load += new System.EventHandler(this.FormSerializarProductos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.grpXml.ResumeLayout(false);
            this.grpXml.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblOrigen;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.GroupBox grpXml;
        private System.Windows.Forms.TextBox txtXml;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnDeserializar;
        private System.Windows.Forms.Button btnSerializar;
    }
}
