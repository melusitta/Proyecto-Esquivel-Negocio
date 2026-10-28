namespace GestiondeUsuario
{
    partial class FormGenerarSolicitudCompra
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
            this.lblProductos = new System.Windows.Forms.Label();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.lblItems = new System.Windows.Forms.Label();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.lblResumen = new System.Windows.Forms.Label();
            this.lblProveedores = new System.Windows.Forms.Label();
            this.btnPreRegistrar = new System.Windows.Forms.Button();
            this.dgvProveedores = new System.Windows.Forms.DataGridView();
            this.btnVolver = new System.Windows.Forms.Button();
            this.lblPrioridad = new System.Windows.Forms.Label();
            this.cmbPrioridad = new System.Windows.Forms.ComboBox();
            this.btnFinalizar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Text = "Generar solicitud de compra";
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblProductos
            // 
            this.lblProductos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductos.Location = new System.Drawing.Point(20, 60);
            this.lblProductos.Text = "Productos con stock mínimo";
            this.lblProductos.AutoSize = true;
            this.lblProductos.Name = "lblProductos";
            this.lblProductos.TabIndex = 1;
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.AllowUserToResizeRows = false;
            this.dgvProductos.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.dgvProductos.Location = new System.Drawing.Point(20, 85);
            this.dgvProductos.MultiSelect = true;
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(560, 340);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.TabIndex = 2;
            this.dgvProductos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductos_CellDoubleClick);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(20, 435);
            this.btnAgregar.Size = new System.Drawing.Size(250, 36);
            this.btnAgregar.Text = "Agregar a la solicitud  >";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // lblItems
            // 
            this.lblItems.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItems.Location = new System.Drawing.Point(600, 60);
            this.lblItems.Text = "Solicitud de compra: asigná el proveedor y la cantidad de cada producto";
            this.lblItems.AutoSize = true;
            this.lblItems.Name = "lblItems";
            this.lblItems.TabIndex = 4;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeRows = false;
            this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvItems.Location = new System.Drawing.Point(600, 85);
            this.dgvItems.MultiSelect = true;
            this.dgvItems.ReadOnly = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(620, 340);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.TabIndex = 5;
            this.dgvItems.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvItems_CurrentCellDirtyStateChanged);
            this.dgvItems.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellValueChanged);
            this.dgvItems.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvItems_DataError);
            // 
            // btnQuitar
            // 
            this.btnQuitar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnQuitar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitar.Location = new System.Drawing.Point(600, 435);
            this.btnQuitar.Size = new System.Drawing.Size(170, 36);
            this.btnQuitar.Text = "Quitar producto";
            this.btnQuitar.UseVisualStyleBackColor = true;
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.TabIndex = 6;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // lblResumen
            // 
            this.lblResumen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResumen.Location = new System.Drawing.Point(790, 443);
            this.lblResumen.Text = "";
            this.lblResumen.AutoSize = true;
            this.lblResumen.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblResumen.Name = "lblResumen";
            this.lblResumen.TabIndex = 7;
            // 
            // lblProveedores
            // 
            this.lblProveedores.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProveedores.Location = new System.Drawing.Point(20, 488);
            this.lblProveedores.Text = "Proveedores disponibles";
            this.lblProveedores.AutoSize = true;
            this.lblProveedores.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblProveedores.Name = "lblProveedores";
            this.lblProveedores.TabIndex = 8;
            // 
            // btnPreRegistrar
            // 
            this.btnPreRegistrar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnPreRegistrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreRegistrar.Location = new System.Drawing.Point(1050, 480);
            this.btnPreRegistrar.Size = new System.Drawing.Size(170, 32);
            this.btnPreRegistrar.Text = "Pre-registrar";
            this.btnPreRegistrar.UseVisualStyleBackColor = true;
            this.btnPreRegistrar.Name = "btnPreRegistrar";
            this.btnPreRegistrar.TabIndex = 9;
            this.btnPreRegistrar.Click += new System.EventHandler(this.btnPreRegistrar_Click);
            // 
            // dgvProveedores
            // 
            this.dgvProveedores.AllowUserToAddRows = false;
            this.dgvProveedores.AllowUserToDeleteRows = false;
            this.dgvProveedores.AllowUserToResizeRows = false;
            this.dgvProveedores.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvProveedores.Location = new System.Drawing.Point(20, 516);
            this.dgvProveedores.MultiSelect = false;
            this.dgvProveedores.ReadOnly = true;
            this.dgvProveedores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProveedores.Size = new System.Drawing.Size(1200, 150);
            this.dgvProveedores.Name = "dgvProveedores";
            this.dgvProveedores.TabIndex = 10;
            this.dgvProveedores.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProveedores_CellClick);
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 686);
            this.btnVolver.Size = new System.Drawing.Size(110, 34);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 11;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // lblPrioridad
            // 
            this.lblPrioridad.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrioridad.Location = new System.Drawing.Point(150, 690);
            this.lblPrioridad.Text = "Prioridad:";
            this.lblPrioridad.AutoSize = false;
            this.lblPrioridad.Size = new System.Drawing.Size(90, 25);
            this.lblPrioridad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPrioridad.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblPrioridad.Name = "lblPrioridad";
            this.lblPrioridad.TabIndex = 12;
            // 
            // cmbPrioridad
            // 
            this.cmbPrioridad.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPrioridad.Location = new System.Drawing.Point(245, 690);
            this.cmbPrioridad.Size = new System.Drawing.Size(150, 25);
            this.cmbPrioridad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrioridad.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.cmbPrioridad.Name = "cmbPrioridad";
            this.cmbPrioridad.TabIndex = 13;
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnFinalizar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.Location = new System.Drawing.Point(1010, 680);
            this.btnFinalizar.Size = new System.Drawing.Size(210, 40);
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.UseVisualStyleBackColor = true;
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.TabIndex = 14;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // FormGenerarSolicitudCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 740);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.cmbPrioridad);
            this.Controls.Add(this.lblPrioridad);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.dgvProveedores);
            this.Controls.Add(this.btnPreRegistrar);
            this.Controls.Add(this.lblProveedores);
            this.Controls.Add(this.lblResumen);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.lblItems);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.lblProductos);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormGenerarSolicitudCompra";
            this.Text = "Generar solicitud de compra";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormGenerarSolicitudCompra_FormClosed);
            this.Load += new System.EventHandler(this.FormGenerarSolicitudCompra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblProductos;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.Label lblProveedores;
        private System.Windows.Forms.Button btnPreRegistrar;
        private System.Windows.Forms.DataGridView dgvProveedores;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label lblPrioridad;
        private System.Windows.Forms.ComboBox cmbPrioridad;
        private System.Windows.Forms.Button btnFinalizar;
    }
}
