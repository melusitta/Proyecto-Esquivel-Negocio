namespace GestiondeUsuario
{
    partial class FormRecuperar
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
            this.lblNuevaPass = new System.Windows.Forms.Label();
            this.lblDNI = new System.Windows.Forms.Label();
            this.lblConfirmarPass = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.txtNuevaPass = new System.Windows.Forms.TextBox();
            this.txtConfirmarPass = new System.Windows.Forms.TextBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnRecuperar = new System.Windows.Forms.Button();
            this.lblContraseñaActual = new System.Windows.Forms.Label();
            this.txtContraseñaActual = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNuevaPass
            // 
            this.lblNuevaPass.AutoSize = true;
            this.lblNuevaPass.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNuevaPass.Location = new System.Drawing.Point(7, 75);
            this.lblNuevaPass.Name = "lblNuevaPass";
            this.lblNuevaPass.Size = new System.Drawing.Size(145, 20);
            this.lblNuevaPass.TabIndex = 3;
            this.lblNuevaPass.Text = "Nueva Contraseña:";
            // 
            // lblDNI
            // 
            this.lblDNI.AutoSize = true;
            this.lblDNI.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDNI.Location = new System.Drawing.Point(7, 10);
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.Size = new System.Drawing.Size(41, 20);
            this.lblDNI.TabIndex = 2;
            this.lblDNI.Text = "DNI:";
            // 
            // lblConfirmarPass
            // 
            this.lblConfirmarPass.AutoSize = true;
            this.lblConfirmarPass.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmarPass.Location = new System.Drawing.Point(267, 75);
            this.lblConfirmarPass.Name = "lblConfirmarPass";
            this.lblConfirmarPass.Size = new System.Drawing.Size(165, 20);
            this.lblConfirmarPass.TabIndex = 4;
            this.lblConfirmarPass.Text = "Confirmar Contraseña";
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(10, 33);
            this.txtDNI.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(230, 25);
            this.txtDNI.TabIndex = 5;
            // 
            // txtNuevaPass
            // 
            this.txtNuevaPass.Location = new System.Drawing.Point(10, 98);
            this.txtNuevaPass.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNuevaPass.Name = "txtNuevaPass";
            this.txtNuevaPass.PasswordChar = '•';
            this.txtNuevaPass.Size = new System.Drawing.Size(230, 25);
            this.txtNuevaPass.TabIndex = 6;
            this.txtNuevaPass.UseSystemPasswordChar = true;
            // 
            // txtConfirmarPass
            // 
            this.txtConfirmarPass.Location = new System.Drawing.Point(270, 98);
            this.txtConfirmarPass.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmarPass.Name = "txtConfirmarPass";
            this.txtConfirmarPass.PasswordChar = '•';
            this.txtConfirmarPass.Size = new System.Drawing.Size(230, 25);
            this.txtConfirmarPass.TabIndex = 7;
            this.txtConfirmarPass.UseSystemPasswordChar = true;
            // 
            // btnVolver
            // 
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(230, 150);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(130, 38);
            this.btnVolver.TabIndex = 8;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnRecuperar
            // 
            this.btnRecuperar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecuperar.Location = new System.Drawing.Point(370, 150);
            this.btnRecuperar.Name = "btnRecuperar";
            this.btnRecuperar.Size = new System.Drawing.Size(130, 38);
            this.btnRecuperar.TabIndex = 9;
            this.btnRecuperar.Text = "Cambiar";
            this.btnRecuperar.UseVisualStyleBackColor = true;
            this.btnRecuperar.Click += new System.EventHandler(this.btnRecuperar_Click);
            // 
            // lblContraseñaActual
            // 
            this.lblContraseñaActual.AutoSize = true;
            this.lblContraseñaActual.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContraseñaActual.Location = new System.Drawing.Point(267, 10);
            this.lblContraseñaActual.Name = "lblContraseñaActual";
            this.lblContraseñaActual.Size = new System.Drawing.Size(145, 20);
            this.lblContraseñaActual.TabIndex = 10;
            this.lblContraseñaActual.Text = "Contraseña Actual:";
            // 
            // txtContraseñaActual
            // 
            this.txtContraseñaActual.Location = new System.Drawing.Point(270, 33);
            this.txtContraseñaActual.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContraseñaActual.Name = "txtContraseñaActual";
            this.txtContraseñaActual.PasswordChar = '•';
            this.txtContraseñaActual.Size = new System.Drawing.Size(230, 25);
            this.txtContraseñaActual.TabIndex = 11;
            this.txtContraseñaActual.UseSystemPasswordChar = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRecuperar);
            this.panel1.Controls.Add(this.lblContraseñaActual);
            this.panel1.Controls.Add(this.btnVolver);
            this.panel1.Controls.Add(this.txtContraseñaActual);
            this.panel1.Controls.Add(this.lblDNI);
            this.panel1.Controls.Add(this.txtNuevaPass);
            this.panel1.Controls.Add(this.txtDNI);
            this.panel1.Controls.Add(this.txtConfirmarPass);
            this.panel1.Controls.Add(this.lblConfirmarPass);
            this.panel1.Controls.Add(this.lblNuevaPass);
            this.panel1.Location = new System.Drawing.Point(20, 15);
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(510, 200);
            this.panel1.TabIndex = 12;
            // 
            // FormRecuperar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 230);
            this.Controls.Add(this.panel1);
            this.AcceptButton = this.btnRecuperar;
            this.Name = "FormRecuperar";
            this.Text = "FormRecuperar";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormRecuperar_FormClosed);
            this.Load += new System.EventHandler(this.FormRecuperar_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblNuevaPass;
        private System.Windows.Forms.Label lblDNI;
        private System.Windows.Forms.Label lblConfirmarPass;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.TextBox txtNuevaPass;
        private System.Windows.Forms.TextBox txtConfirmarPass;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnRecuperar;
        private System.Windows.Forms.Label lblContraseñaActual;
        private System.Windows.Forms.TextBox txtContraseñaActual;
        private System.Windows.Forms.Panel panel1;
    }
}