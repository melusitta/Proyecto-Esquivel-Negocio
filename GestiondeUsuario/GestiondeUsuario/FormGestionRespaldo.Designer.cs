namespace GestiondeUsuario
{
    partial class FormGestionRespaldo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BackUp = new System.Windows.Forms.Button();
            this.Restore = new System.Windows.Forms.Button();
            this.Volver = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BackUp
            // 
            this.BackUp.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackUp.Location = new System.Drawing.Point(45, 95);
            this.BackUp.Name = "BackUp";
            this.BackUp.Size = new System.Drawing.Size(160, 80);
            this.BackUp.TabIndex = 0;
            this.BackUp.Text = "BackUp";
            this.BackUp.UseVisualStyleBackColor = true;
            this.BackUp.Click += new System.EventHandler(this.BackUp_Click);
            // 
            // Restore
            // 
            this.Restore.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Restore.Location = new System.Drawing.Point(225, 95);
            this.Restore.Name = "Restore";
            this.Restore.Size = new System.Drawing.Size(160, 80);
            this.Restore.TabIndex = 1;
            this.Restore.Text = "Restore";
            this.Restore.UseVisualStyleBackColor = true;
            this.Restore.Click += new System.EventHandler(this.Restore_Click);
            // 
            // Volver
            // 
            this.Volver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Volver.Location = new System.Drawing.Point(20, 215);
            this.Volver.Name = "Volver";
            this.Volver.Size = new System.Drawing.Size(110, 32);
            this.Volver.TabIndex = 2;
            this.Volver.Text = "Volver";
            this.Volver.UseVisualStyleBackColor = true;
            this.Volver.Click += new System.EventHandler(this.Volver_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(0, 30);
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(431, 34);
            this.lblTitulo.TabIndex = 3;
            this.lblTitulo.Text = "Gestion de Respaldo";
            // 
            // FormGestionRespaldo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 265);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.Volver);
            this.Controls.Add(this.Restore);
            this.Controls.Add(this.BackUp);
            this.Name = "FormGestionRespaldo";
            this.Text = "FormGestionRespaldo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormGestionRespaldo_FormClosed);
            this.Load += new System.EventHandler(this.FormGestionRespaldo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BackUp;
        private System.Windows.Forms.Button Restore;
        private System.Windows.Forms.Button Volver;
        private System.Windows.Forms.Label lblTitulo;
    }
}