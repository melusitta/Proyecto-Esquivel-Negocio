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
            this.lblCBU = new System.Windows.Forms.Label();
            this.txtCBU = new System.Windows.Forms.TextBox();
            this.lblBanco = new System.Windows.Forms.Label();
            this.txtBanco = new System.Windows.Forms.TextBox();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnAplicar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Text = "Completar registro proveedor";
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblProveedores
            // 
            this.lblProveedores.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProveedores.Location = new System.Drawing.Point(20, 60);
            this.lblProveedores.Text = "Proveedores";
            this.lblProveedores.AutoSize = true;
            this.lblProveedores.Name = "lblProveedores";
            this.lblProveedores.TabIndex = 1;
            // 
            // dgvProveedores
            // 
            this.dgvProveedores.AllowUserToAddRows = false;
            this.dgvProveedores.AllowUserToDeleteRows = false;
            this.dgvProveedores.AllowUserToResizeRows = false;
            this.dgvProveedores.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvProveedores.Location = new System.Drawing.Point(20, 85);
            this.dgvProveedores.MultiSelect = false;
            this.dgvProveedores.ReadOnly = true;
            this.dgvProveedores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProveedores.Size = new System.Drawing.Size(960, 300);
            this.dgvProveedores.Name = "dgvProveedores";
            this.dgvProveedores.TabIndex = 2;
            this.dgvProveedores.SelectionChanged += new System.EventHandler(this.dgvProveedores_SelectionChanged);
            // 
            // lblDatos
            // 
            this.lblDatos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatos.Location = new System.Drawing.Point(20, 400);
            this.lblDatos.Text = "";
            this.lblDatos.AutoSize = false;
            this.lblDatos.Size = new System.Drawing.Size(960, 22);
            this.lblDatos.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblDatos.Name = "lblDatos";
            this.lblDatos.TabIndex = 3;
            // 
            // lblCBU
            // 
            this.lblCBU.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCBU.Location = new System.Drawing.Point(20, 435);
            this.lblCBU.Text = "CBU:";
            this.lblCBU.AutoSize = false;
            this.lblCBU.Size = new System.Drawing.Size(100, 25);
            this.lblCBU.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCBU.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblCBU.Name = "lblCBU";
            this.lblCBU.TabIndex = 4;
            // 
            // txtCBU
            // 
            this.txtCBU.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCBU.Location = new System.Drawing.Point(130, 435);
            this.txtCBU.Size = new System.Drawing.Size(300, 25);
            this.txtCBU.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.txtCBU.MaxLength = 26;
            this.txtCBU.Name = "txtCBU";
            this.txtCBU.TabIndex = 5;
            // 
            // lblBanco
            // 
            this.lblBanco.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBanco.Location = new System.Drawing.Point(450, 435);
            this.lblBanco.Text = "Banco:";
            this.lblBanco.AutoSize = false;
            this.lblBanco.Size = new System.Drawing.Size(80, 25);
            this.lblBanco.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBanco.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblBanco.Name = "lblBanco";
            this.lblBanco.TabIndex = 6;
            // 
            // txtBanco
            // 
            this.txtBanco.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBanco.Location = new System.Drawing.Point(540, 435);
            this.txtBanco.Size = new System.Drawing.Size(300, 25);
            this.txtBanco.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.txtBanco.MaxLength = 100;
            this.txtBanco.Name = "txtBanco";
            this.txtBanco.TabIndex = 7;
            // 
            // lblAyuda
            // 
            this.lblAyuda.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAyuda.Location = new System.Drawing.Point(130, 466);
            this.lblAyuda.Text = "CBU de 22 dígitos";
            this.lblAyuda.AutoSize = true;
            this.lblAyuda.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.TabIndex = 8;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 506);
            this.btnVolver.Size = new System.Drawing.Size(110, 34);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 9;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnAplicar
            // 
            this.btnAplicar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnAplicar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicar.Location = new System.Drawing.Point(780, 502);
            this.btnAplicar.Size = new System.Drawing.Size(200, 38);
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = true;
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.TabIndex = 10;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // FormCompletarRegistroProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 560);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.lblAyuda);
            this.Controls.Add(this.txtBanco);
            this.Controls.Add(this.lblBanco);
            this.Controls.Add(this.txtCBU);
            this.Controls.Add(this.lblCBU);
            this.Controls.Add(this.lblDatos);
            this.Controls.Add(this.dgvProveedores);
            this.Controls.Add(this.lblProveedores);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormCompletarRegistroProveedor";
            this.Text = "Completar registro proveedor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCompletarRegistroProveedor_FormClosed);
            this.Load += new System.EventHandler(this.FormCompletarRegistroProveedor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblProveedores;
        private System.Windows.Forms.DataGridView dgvProveedores;
        private System.Windows.Forms.Label lblDatos;
        private System.Windows.Forms.Label lblCBU;
        private System.Windows.Forms.TextBox txtCBU;
        private System.Windows.Forms.Label lblBanco;
        private System.Windows.Forms.TextBox txtBanco;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnAplicar;
    }
}
