namespace GestiondeUsuario
{
    partial class Form1
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
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtNombreUsuario = new System.Windows.Forms.TextBox();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblContraseña = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbIdioma = new System.Windows.Forms.ComboBox();
            this.lblIdioma = new System.Windows.Forms.Label();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.pnlMarca = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.pnlMarca.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            //
            // btnLogin
            //
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(50, 262);
            this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(320, 42);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            //
            // txtNombreUsuario
            //
            this.txtNombreUsuario.Location = new System.Drawing.Point(50, 142);
            this.txtNombreUsuario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.Size = new System.Drawing.Size(320, 25);
            this.txtNombreUsuario.TabIndex = 0;
            //
            // txtContraseña
            //
            this.txtContraseña.Location = new System.Drawing.Point(50, 209);
            this.txtContraseña.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.PasswordChar = '•';
            this.txtContraseña.Size = new System.Drawing.Size(320, 25);
            this.txtContraseña.TabIndex = 1;
            this.txtContraseña.TextChanged += new System.EventHandler(this.txtContraseña_TextChanged);
            //
            // lblUsuario
            //
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(47, 118);
            this.lblUsuario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(125, 19);
            this.lblUsuario.TabIndex = 5;
            this.lblUsuario.Text = "Nombre de Usuario:";
            //
            // lblContraseña
            //
            this.lblContraseña.AutoSize = true;
            this.lblContraseña.Location = new System.Drawing.Point(47, 185);
            this.lblContraseña.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblContraseña.Name = "lblContraseña";
            this.lblContraseña.Size = new System.Drawing.Size(80, 19);
            this.lblContraseña.TabIndex = 6;
            this.lblContraseña.Text = "Contraseña:";
            this.lblContraseña.Click += new System.EventHandler(this.label2_Click);
            //
            // panel1
            //
            this.panel1.Controls.Add(this.cmbIdioma);
            this.panel1.Controls.Add(this.lblIdioma);
            this.panel1.Controls.Add(this.lblBienvenida);
            this.panel1.Controls.Add(this.txtNombreUsuario);
            this.panel1.Controls.Add(this.lblContraseña);
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Controls.Add(this.txtContraseña);
            this.panel1.Controls.Add(this.lblUsuario);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(300, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(420, 400);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            //
            // cmbIdioma
            //
            this.cmbIdioma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIdioma.FormattingEnabled = true;
            this.cmbIdioma.Location = new System.Drawing.Point(250, 342);
            this.cmbIdioma.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbIdioma.Name = "cmbIdioma";
            this.cmbIdioma.Size = new System.Drawing.Size(120, 25);
            this.cmbIdioma.TabIndex = 3;
            this.cmbIdioma.SelectedIndexChanged += new System.EventHandler(this.cmbIdioma_SelectedIndexChanged_1);
            //
            // lblIdioma
            //
            this.lblIdioma.AutoSize = true;
            this.lblIdioma.Location = new System.Drawing.Point(47, 345);
            this.lblIdioma.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblIdioma.Name = "lblIdioma";
            this.lblIdioma.Size = new System.Drawing.Size(51, 19);
            this.lblIdioma.TabIndex = 8;
            this.lblIdioma.Text = "Idioma";
            //
            // lblBienvenida
            //
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBienvenida.Location = new System.Drawing.Point(45, 55);
            this.lblBienvenida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(145, 32);
            this.lblBienvenida.TabIndex = 7;
            this.lblBienvenida.Text = "Bienvenido!";
            //
            // pnlMarca
            //
            this.pnlMarca.Controls.Add(this.picLogo);
            this.pnlMarca.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMarca.Location = new System.Drawing.Point(0, 0);
            this.pnlMarca.Name = "pnlMarca";
            this.pnlMarca.Size = new System.Drawing.Size(300, 400);
            this.pnlMarca.TabIndex = 10;
            //
            // picLogo
            //
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.picLogo.Location = new System.Drawing.Point(0, 100);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(300, 300);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            //
            // Form1
            //
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 400);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlMarca);
            this.Name = "Form1";
            this.Text = "Plumy";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlMarca.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblContraseña;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.ComboBox cmbIdioma;
        private System.Windows.Forms.Label lblIdioma;
        private System.Windows.Forms.Panel pnlMarca;
        private System.Windows.Forms.PictureBox picLogo;
    }
}

