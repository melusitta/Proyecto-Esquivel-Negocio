namespace GestiondeUsuario
{
    partial class FormAyuda
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
            this.tvTemas = new System.Windows.Forms.TreeView();
            this.rtbContenido = new System.Windows.Forms.RichTextBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnManualPdf = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Text = "Ayuda";
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblBuscar
            // 
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.Location = new System.Drawing.Point(20, 66);
            this.lblBuscar.Text = "Buscar:";
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.TabIndex = 1;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(85, 63);
            this.txtBuscar.Size = new System.Drawing.Size(245, 25);
            this.txtBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.TabIndex = 2;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // tvTemas
            // 
            this.tvTemas.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.tvTemas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvTemas.Location = new System.Drawing.Point(20, 100);
            this.tvTemas.Size = new System.Drawing.Size(310, 460);
            this.tvTemas.HideSelection = false;
            this.tvTemas.FullRowSelect = true;
            this.tvTemas.ShowLines = false;
            this.tvTemas.ItemHeight = 28;
            this.tvTemas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvTemas.Name = "tvTemas";
            this.tvTemas.TabIndex = 3;
            this.tvTemas.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTemas_AfterSelect);
            // 
            // rtbContenido
            // 
            this.rtbContenido.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.rtbContenido.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbContenido.Location = new System.Drawing.Point(350, 63);
            this.rtbContenido.Size = new System.Drawing.Size(550, 497);
            this.rtbContenido.ReadOnly = true;
            this.rtbContenido.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbContenido.BackColor = System.Drawing.Color.White;
            this.rtbContenido.TabStop = false;
            this.rtbContenido.Text = "";
            this.rtbContenido.Name = "rtbContenido";
            this.rtbContenido.TabIndex = 4;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 586);
            this.btnVolver.Size = new System.Drawing.Size(110, 34);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 5;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnManualPdf
            // 
            this.btnManualPdf.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnManualPdf.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManualPdf.Location = new System.Drawing.Point(700, 582);
            this.btnManualPdf.Size = new System.Drawing.Size(200, 38);
            this.btnManualPdf.Text = "Manual en PDF";
            this.btnManualPdf.UseVisualStyleBackColor = true;
            this.btnManualPdf.Name = "btnManualPdf";
            this.btnManualPdf.TabIndex = 6;
            this.btnManualPdf.Click += new System.EventHandler(this.btnManualPdf_Click);
            // 
            // FormAyuda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 640);
            this.Controls.Add(this.btnManualPdf);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.rtbContenido);
            this.Controls.Add(this.tvTemas);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormAyuda";
            this.Text = "Ayuda";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormAyuda_FormClosed);
            this.Load += new System.EventHandler(this.FormAyuda_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.TreeView tvTemas;
        private System.Windows.Forms.RichTextBox rtbContenido;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnManualPdf;
    }
}
