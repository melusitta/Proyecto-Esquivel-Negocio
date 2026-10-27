namespace GestiondeUsuario
{
    partial class FormControlCambios
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
            this.lblProductos = new System.Windows.Forms.Label();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.lblVersiones = new System.Windows.Forms.Label();
            this.dgvVersiones = new System.Windows.Forms.DataGridView();
            this.lblAyudaRestaurar = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnRestaurar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVersiones)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Text = "Control de cambios";
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblBuscar
            // 
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.Location = new System.Drawing.Point(20, 66);
            this.lblBuscar.Text = "Buscar producto:";
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.TabIndex = 1;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(140, 63);
            this.txtBuscar.Size = new System.Drawing.Size(300, 25);
            this.txtBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.TabIndex = 2;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // lblProductos
            // 
            this.lblProductos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductos.Location = new System.Drawing.Point(20, 100);
            this.lblProductos.Text = "Productos";
            this.lblProductos.AutoSize = true;
            this.lblProductos.Name = "lblProductos";
            this.lblProductos.TabIndex = 3;
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.AllowUserToResizeRows = false;
            this.dgvProductos.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvProductos.Location = new System.Drawing.Point(20, 125);
            this.dgvProductos.MultiSelect = false;
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(1280, 190);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.TabIndex = 4;
            this.dgvProductos.SelectionChanged += new System.EventHandler(this.dgvProductos_SelectionChanged);
            // 
            // lblVersiones
            // 
            this.lblVersiones.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersiones.Location = new System.Drawing.Point(20, 330);
            this.lblVersiones.Text = "Versiones del producto (en rosa, lo que cambió respecto de la versión anterior)";
            this.lblVersiones.AutoSize = true;
            this.lblVersiones.Name = "lblVersiones";
            this.lblVersiones.TabIndex = 5;
            // 
            // dgvVersiones
            // 
            this.dgvVersiones.AllowUserToAddRows = false;
            this.dgvVersiones.AllowUserToDeleteRows = false;
            this.dgvVersiones.AllowUserToResizeRows = false;
            this.dgvVersiones.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvVersiones.Location = new System.Drawing.Point(20, 355);
            this.dgvVersiones.MultiSelect = false;
            this.dgvVersiones.ReadOnly = true;
            this.dgvVersiones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVersiones.Size = new System.Drawing.Size(1280, 255);
            this.dgvVersiones.Name = "dgvVersiones";
            this.dgvVersiones.TabIndex = 6;
            this.dgvVersiones.SelectionChanged += new System.EventHandler(this.dgvVersiones_SelectionChanged);
            // 
            // lblAyudaRestaurar
            // 
            this.lblAyudaRestaurar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAyudaRestaurar.Location = new System.Drawing.Point(20, 620);
            this.lblAyudaRestaurar.Text = "";
            this.lblAyudaRestaurar.AutoSize = true;
            this.lblAyudaRestaurar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblAyudaRestaurar.Name = "lblAyudaRestaurar";
            this.lblAyudaRestaurar.TabIndex = 7;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 646);
            this.btnVolver.Size = new System.Drawing.Size(110, 34);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 8;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnRestaurar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestaurar.Location = new System.Drawing.Point(1060, 642);
            this.btnRestaurar.Size = new System.Drawing.Size(240, 38);
            this.btnRestaurar.Text = "Restaurar esta versión";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.TabIndex = 9;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // FormControlCambios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1320, 700);
            this.Controls.Add(this.btnRestaurar);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.lblAyudaRestaurar);
            this.Controls.Add(this.dgvVersiones);
            this.Controls.Add(this.lblVersiones);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.lblProductos);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormControlCambios";
            this.Text = "Control de cambios";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormControlCambios_FormClosed);
            this.Load += new System.EventHandler(this.FormControlCambios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVersiones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblProductos;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Label lblVersiones;
        private System.Windows.Forms.DataGridView dgvVersiones;
        private System.Windows.Forms.Label lblAyudaRestaurar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnRestaurar;
    }
}
