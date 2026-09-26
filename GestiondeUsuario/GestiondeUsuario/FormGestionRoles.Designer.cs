namespace GestiondeUsuario
{
    partial class FormGestionRoles
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
            this.tlpListas = new System.Windows.Forms.TableLayoutPanel();
            this.lblListaContenido = new System.Windows.Forms.Label();
            this.lblListaRoles = new System.Windows.Forms.Label();
            this.lblModo = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.rbFamilia = new System.Windows.Forms.RadioButton();
            this.rbPatente = new System.Windows.Forms.RadioButton();
            this.lstDisponibles = new System.Windows.Forms.ListBox();
            this.lblPatentesDisponibles = new System.Windows.Forms.Label();
            this.lstContenido = new System.Windows.Forms.ListBox();
            this.lstRoles = new System.Windows.Forms.ListBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblModo
            // 
            this.lblModo.AutoSize = true;
            this.lblModo.Location = new System.Drawing.Point(300, 22);
            this.lblModo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModo.Name = "lblModo";
            this.lblModo.Size = new System.Drawing.Size(35, 13);
            this.lblModo.TabIndex = 40;
            this.lblModo.Text = "label5";
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(20, 480);
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(110, 32);
            this.btnVolver.TabIndex = 39;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(424, 350);
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(91, 34);
            this.btnCancelar.TabIndex = 38;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(323, 350);
            this.btnAplicar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(91, 34);
            this.btnAplicar.TabIndex = 37;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = true;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(222, 350);
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(91, 34);
            this.btnEliminar.TabIndex = 36;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(121, 350);
            this.btnModificar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(91, 34);
            this.btnModificar.TabIndex = 35;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(20, 350);
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(91, 34);
            this.btnNuevo.TabIndex = 34;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(100, 37);
            this.txtDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(395, 25);
            this.txtDescripcion.TabIndex = 33;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(100, 2);
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(395, 25);
            this.txtNombre.TabIndex = 31;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(0, 5);
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(47, 13);
            this.lblNombre.TabIndex = 30;
            this.lblNombre.Text = "Nombre:";
            // 
            // btnQuitar
            // 
            this.btnQuitar.Location = new System.Drawing.Point(670, 390);
            this.btnQuitar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(110, 34);
            this.btnQuitar.TabIndex = 29;
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.UseVisualStyleBackColor = true;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(550, 390);
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(110, 34);
            this.btnAgregar.TabIndex = 28;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // rbFamilia
            // 
            this.rbFamilia.AutoSize = true;
            this.rbFamilia.Location = new System.Drawing.Point(670, 352);
            this.rbFamilia.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFamilia.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.rbFamilia.Name = "rbFamilia";
            this.rbFamilia.Size = new System.Drawing.Size(57, 17);
            this.rbFamilia.TabIndex = 27;
            this.rbFamilia.TabStop = true;
            this.rbFamilia.Text = "Familia";
            this.rbFamilia.UseVisualStyleBackColor = true;
            this.rbFamilia.CheckedChanged += new System.EventHandler(this.rbFamilia_CheckedChanged);
            // 
            // rbPatente
            // 
            this.rbPatente.AutoSize = true;
            this.rbPatente.Location = new System.Drawing.Point(550, 352);
            this.rbPatente.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPatente.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.rbPatente.Name = "rbPatente";
            this.rbPatente.Size = new System.Drawing.Size(62, 17);
            this.rbPatente.TabIndex = 26;
            this.rbPatente.TabStop = true;
            this.rbPatente.Text = "Patente";
            this.rbPatente.UseVisualStyleBackColor = true;
            this.rbPatente.CheckedChanged += new System.EventHandler(this.rbPatente_CheckedChanged);
            // 
            // lstDisponibles
            // 
            this.lstDisponibles.FormattingEnabled = true;
            this.lstDisponibles.Location = new System.Drawing.Point(550, 85);
            this.lstDisponibles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDisponibles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDisponibles.IntegralHeight = false;
            this.lstDisponibles.Margin = new System.Windows.Forms.Padding(18, 4, 0, 0);
            this.lstDisponibles.Name = "lstDisponibles";
            this.lstDisponibles.Size = new System.Drawing.Size(230, 250);
            this.lstDisponibles.TabIndex = 25;
            this.lstDisponibles.SelectedIndexChanged += new System.EventHandler(this.lstDisponibles_SelectedIndexChanged);
            // 
            // lblPatentesDisponibles
            // 
            this.lblPatentesDisponibles.AutoSize = true;
            this.lblPatentesDisponibles.Location = new System.Drawing.Point(547, 60);
            this.lblPatentesDisponibles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatentesDisponibles.Margin = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.lblPatentesDisponibles.Name = "lblPatentesDisponibles";
            this.lblPatentesDisponibles.Size = new System.Drawing.Size(152, 13);
            this.lblPatentesDisponibles.TabIndex = 24;
            this.lblPatentesDisponibles.Text = "Patentes y Familias disponibles";
            // 
            // lstContenido
            // 
            this.lstContenido.FormattingEnabled = true;
            this.lstContenido.Location = new System.Drawing.Point(285, 85);
            this.lstContenido.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstContenido.IntegralHeight = false;
            this.lstContenido.Margin = new System.Windows.Forms.Padding(9, 4, 9, 0);
            this.lstContenido.Name = "lstContenido";
            this.lstContenido.Size = new System.Drawing.Size(230, 250);
            this.lstContenido.TabIndex = 23;
            // 
            // lstRoles
            // 
            this.lstRoles.FormattingEnabled = true;
            this.lstRoles.Location = new System.Drawing.Point(20, 85);
            this.lstRoles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRoles.IntegralHeight = false;
            this.lstRoles.Margin = new System.Windows.Forms.Padding(0, 4, 18, 0);
            this.lstRoles.Name = "lstRoles";
            this.lstRoles.Size = new System.Drawing.Size(230, 250);
            this.lstRoles.TabIndex = 22;
            this.lstRoles.SelectedIndexChanged += new System.EventHandler(this.lstRoles_SelectedIndexChanged);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(148, 20);
            this.lblTitulo.TabIndex = 21;
            this.lblTitulo.Text = "Gestion de Roles";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDescripcion);
            this.panel1.Controls.Add(this.lblNombre);
            this.panel1.Controls.Add(this.txtNombre);
            this.panel1.Controls.Add(this.txtDescripcion);
            this.panel1.Location = new System.Drawing.Point(20, 398);
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(495, 70);
            this.panel1.TabIndex = 41;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(0, 40);
            this.lblDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(66, 13);
            this.lblDescripcion.TabIndex = 32;
            this.lblDescripcion.Text = "Descripcion:";
            // 
            // lblListaRoles
            // 
            this.lblListaRoles.AutoSize = true;
            this.lblListaRoles.Location = new System.Drawing.Point(17, 60);
            this.lblListaRoles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListaRoles.Text = "Roles";
            this.lblListaRoles.Margin = new System.Windows.Forms.Padding(0, 0, 18, 0);
            this.lblListaRoles.Name = "lblListaRoles";
            // 
            // lblListaContenido
            // 
            this.lblListaContenido.AutoSize = true;
            this.lblListaContenido.Location = new System.Drawing.Point(282, 60);
            this.lblListaContenido.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListaContenido.Text = "Contenido";
            this.lblListaContenido.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.lblListaContenido.Name = "lblListaContenido";
            // 
            // tlpListas
            // 
            this.tlpListas.Controls.Add(this.lblListaRoles, 0, 0);
            this.tlpListas.Controls.Add(this.lblListaContenido, 1, 0);
            this.tlpListas.Controls.Add(this.lblPatentesDisponibles, 2, 0);
            this.tlpListas.Controls.Add(this.lstRoles, 0, 1);
            this.tlpListas.Controls.Add(this.lstContenido, 1, 1);
            this.tlpListas.Controls.Add(this.lstDisponibles, 2, 1);
            this.tlpListas.ColumnCount = 3;
            this.tlpListas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpListas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpListas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpListas.RowCount = 2;
            this.tlpListas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpListas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpListas.Location = new System.Drawing.Point(20, 58);
            this.tlpListas.Size = new System.Drawing.Size(760, 280);
            this.tlpListas.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.tlpListas.TabIndex = 30;
            this.tlpListas.Name = "tlpListas";
            // 
            // FormGestionRoles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 525);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.lblModo);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.rbFamilia);
            this.Controls.Add(this.rbPatente);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tlpListas);
            this.Name = "FormGestionRoles";
            this.Text = "FormGestionRoles";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormGestionRoles_FormClosed);
            this.Load += new System.EventHandler(this.FormGestionRoles_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblModo;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.RadioButton rbFamilia;
        private System.Windows.Forms.RadioButton rbPatente;
        private System.Windows.Forms.ListBox lstDisponibles;
        private System.Windows.Forms.Label lblPatentesDisponibles;
        private System.Windows.Forms.ListBox lstContenido;
        private System.Windows.Forms.ListBox lstRoles;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblListaRoles;
        private System.Windows.Forms.Label lblListaContenido;
        private System.Windows.Forms.TableLayoutPanel tlpListas;
    }
}