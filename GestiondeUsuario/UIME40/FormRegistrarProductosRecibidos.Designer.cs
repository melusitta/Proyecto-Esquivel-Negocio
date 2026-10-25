namespace GestiondeUsuario
{
    partial class FormRegistrarProductosRecibidos
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
            this.lblOrdenes = new System.Windows.Forms.Label();
            this.dgvOrdenes = new System.Windows.Forms.DataGridView();
            this.lblComparacion = new System.Windows.Forms.Label();
            this.lblItems = new System.Windows.Forms.Label();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.lblComentario = new System.Windows.Forms.Label();
            this.txtComentario = new System.Windows.Forms.TextBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnImprimirRecibo = new System.Windows.Forms.Button();
            this.btnConfirmar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Text = "Registrar productos recibidos";
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblOrdenes
            // 
            this.lblOrdenes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrdenes.Location = new System.Drawing.Point(20, 60);
            this.lblOrdenes.Text = "Órdenes de compra pagadas, pendientes de recibir";
            this.lblOrdenes.AutoSize = true;
            this.lblOrdenes.Name = "lblOrdenes";
            this.lblOrdenes.TabIndex = 1;
            // 
            // dgvOrdenes
            // 
            this.dgvOrdenes.AllowUserToAddRows = false;
            this.dgvOrdenes.AllowUserToDeleteRows = false;
            this.dgvOrdenes.AllowUserToResizeRows = false;
            this.dgvOrdenes.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dgvOrdenes.Location = new System.Drawing.Point(20, 85);
            this.dgvOrdenes.MultiSelect = false;
            this.dgvOrdenes.ReadOnly = true;
            this.dgvOrdenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdenes.Size = new System.Drawing.Size(1020, 160);
            this.dgvOrdenes.Name = "dgvOrdenes";
            this.dgvOrdenes.TabIndex = 2;
            this.dgvOrdenes.SelectionChanged += new System.EventHandler(this.dgvOrdenes_SelectionChanged);
            // 
            // lblComparacion
            // 
            this.lblComparacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComparacion.Location = new System.Drawing.Point(20, 258);
            this.lblComparacion.Text = "";
            this.lblComparacion.AutoSize = false;
            this.lblComparacion.Size = new System.Drawing.Size(1020, 22);
            this.lblComparacion.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.lblComparacion.Name = "lblComparacion";
            this.lblComparacion.TabIndex = 3;
            // 
            // lblItems
            // 
            this.lblItems.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItems.Location = new System.Drawing.Point(20, 285);
            this.lblItems.Text = "Mercadería recibida: completá cantidad, estado y ubicación de cada producto";
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
            this.dgvItems.Location = new System.Drawing.Point(20, 310);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.ReadOnly = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(1020, 230);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.TabIndex = 5;
            this.dgvItems.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvItems_DataError);
            // 
            // lblComentario
            // 
            this.lblComentario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComentario.Location = new System.Drawing.Point(20, 552);
            this.lblComentario.Text = "Comentario de la recepción (discrepancias con la orden o la factura):";
            this.lblComentario.AutoSize = true;
            this.lblComentario.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblComentario.Name = "lblComentario";
            this.lblComentario.TabIndex = 6;
            // 
            // txtComentario
            // 
            this.txtComentario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComentario.Location = new System.Drawing.Point(20, 577);
            this.txtComentario.Size = new System.Drawing.Size(1020, 52);
            this.txtComentario.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtComentario.MaxLength = 500;
            this.txtComentario.Multiline = true;
            this.txtComentario.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.TabIndex = 7;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(20, 656);
            this.btnVolver.Size = new System.Drawing.Size(110, 34);
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 8;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnImprimirRecibo
            // 
            this.btnImprimirRecibo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnImprimirRecibo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirRecibo.Location = new System.Drawing.Point(560, 652);
            this.btnImprimirRecibo.Size = new System.Drawing.Size(210, 38);
            this.btnImprimirRecibo.Text = "Imprimir recibo";
            this.btnImprimirRecibo.UseVisualStyleBackColor = true;
            this.btnImprimirRecibo.Name = "btnImprimirRecibo";
            this.btnImprimirRecibo.TabIndex = 9;
            this.btnImprimirRecibo.Click += new System.EventHandler(this.btnImprimirRecibo_Click);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnConfirmar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.Location = new System.Drawing.Point(780, 652);
            this.btnConfirmar.Size = new System.Drawing.Size(260, 38);
            this.btnConfirmar.Text = "Confirmar recepción";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.TabIndex = 10;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // FormRegistrarProductosRecibidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 710);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.btnImprimirRecibo);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.txtComentario);
            this.Controls.Add(this.lblComentario);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.lblItems);
            this.Controls.Add(this.lblComparacion);
            this.Controls.Add(this.dgvOrdenes);
            this.Controls.Add(this.lblOrdenes);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormRegistrarProductosRecibidos";
            this.Text = "Registrar productos recibidos";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormRegistrarProductosRecibidos_FormClosed);
            this.Load += new System.EventHandler(this.FormRegistrarProductosRecibidos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblOrdenes;
        private System.Windows.Forms.DataGridView dgvOrdenes;
        private System.Windows.Forms.Label lblComparacion;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Label lblComentario;
        private System.Windows.Forms.TextBox txtComentario;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnImprimirRecibo;
        private System.Windows.Forms.Button btnConfirmar;
    }
}
