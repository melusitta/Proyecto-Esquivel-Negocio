namespace GestiondeUsuario
{
    partial class FormGestionFamilias
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
            this.lblListaContenido = new System.Windows.Forms.Label();
            this.lblListaFamilias = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lstFamilias = new System.Windows.Forms.ListBox();
            this.lstContenido = new System.Windows.Forms.ListBox();
            this.lblPatentesDisponibles = new System.Windows.Forms.Label();
            this.lstDisponibles = new System.Windows.Forms.ListBox();
            this.rbPatente = new System.Windows.Forms.RadioButton();
            this.rbFamilia = new System.Windows.Forms.RadioButton();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.lblModo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(18, 14);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(168, 20);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Gestion de Familias";
            // 
            // lstFamilias
            // 
            this.lstFamilias.FormattingEnabled = true;
            this.lstFamilias.Location = new System.Drawing.Point(20, 85);
            this.lstFamilias.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstFamilias.Name = "lstFamilias";
            this.lstFamilias.Size = new System.Drawing.Size(230, 250);
            this.lstFamilias.TabIndex = 2;
            this.lstFamilias.SelectedIndexChanged += new System.EventHandler(this.lstFamilias_SelectedIndexChanged);
            // 
            // lstContenido
            // 
            this.lstContenido.FormattingEnabled = true;
            this.lstContenido.Location = new System.Drawing.Point(285, 85);
            this.lstContenido.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstContenido.Name = "lstContenido";
            this.lstContenido.Size = new System.Drawing.Size(230, 250);
            this.lstContenido.TabIndex = 3;
            // 
            // lblPatentesDisponibles
            // 
            this.lblPatentesDisponibles.AutoSize = true;
            this.lblPatentesDisponibles.Location = new System.Drawing.Point(547, 60);
            this.lblPatentesDisponibles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatentesDisponibles.Name = "lblPatentesDisponibles";
            this.lblPatentesDisponibles.Size = new System.Drawing.Size(152, 13);
            this.lblPatentesDisponibles.TabIndex = 4;
            this.lblPatentesDisponibles.Text = "Patentes y Familias disponibles";
            // 
            // lstDisponibles
            // 
            this.lstDisponibles.FormattingEnabled = true;
            this.lstDisponibles.Location = new System.Drawing.Point(550, 85);
            this.lstDisponibles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDisponibles.Name = "lstDisponibles";
            this.lstDisponibles.Size = new System.Drawing.Size(230, 250);
            this.lstDisponibles.TabIndex = 5;
            // 
            // rbPatente
            // 
            this.rbPatente.AutoSize = true;
            this.rbPatente.Location = new System.Drawing.Point(550, 352);
            this.rbPatente.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPatente.Name = "rbPatente";
            this.rbPatente.Size = new System.Drawing.Size(62, 17);
            this.rbPatente.TabIndex = 6;
            this.rbPatente.TabStop = true;
            this.rbPatente.Text = "Patente";
            this.rbPatente.UseVisualStyleBackColor = true;
            this.rbPatente.CheckedChanged += new System.EventHandler(this.rbPatente_CheckedChanged);
            // 
            // rbFamilia
            // 
            this.rbFamilia.AutoSize = true;
            this.rbFamilia.Location = new System.Drawing.Point(670, 352);
            this.rbFamilia.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFamilia.Name = "rbFamilia";
            this.rbFamilia.Size = new System.Drawing.Size(57, 17);
            this.rbFamilia.TabIndex = 7;
            this.rbFamilia.TabStop = true;
            this.rbFamilia.Text = "Familia";
            this.rbFamilia.UseVisualStyleBackColor = true;
            this.rbFamilia.CheckedChanged += new System.EventHandler(this.rbFamilia_CheckedChanged);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(550, 390);
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(110, 34);
            this.btnAgregar.TabIndex = 8;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnQuitar
            // 
            this.btnQuitar.Location = new System.Drawing.Point(670, 390);
            this.btnQuitar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(110, 34);
            this.btnQuitar.TabIndex = 9;
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.UseVisualStyleBackColor = true;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(0, 5);
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(47, 13);
            this.lblNombre.TabIndex = 10;
            this.lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(100, 2);
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(395, 25);
            this.txtNombre.TabIndex = 11;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(0, 40);
            this.lblDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(66, 13);
            this.lblDescripcion.TabIndex = 12;
            this.lblDescripcion.Text = "Descripcion:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(100, 37);
            this.txtDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(395, 25);
            this.txtDescripcion.TabIndex = 13;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(20, 350);
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(91, 34);
            this.btnNuevo.TabIndex = 14;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(121, 350);
            this.btnModificar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(91, 34);
            this.btnModificar.TabIndex = 15;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(222, 350);
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(91, 34);
            this.btnEliminar.TabIndex = 16;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(323, 350);
            this.btnAplicar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(91, 34);
            this.btnAplicar.TabIndex = 17;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = true;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(424, 350);
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(91, 34);
            this.btnCancelar.TabIndex = 18;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(20, 480);
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(110, 32);
            this.btnVolver.TabIndex = 19;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // lblModo
            // 
            this.lblModo.AutoSize = true;
            this.lblModo.Location = new System.Drawing.Point(300, 22);
            this.lblModo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModo.Name = "lblModo";
            this.lblModo.Size = new System.Drawing.Size(35, 13);
            this.lblModo.TabIndex = 20;
            this.lblModo.Text = "label5";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtDescripcion);
            this.panel1.Controls.Add(this.lblNombre);
            this.panel1.Controls.Add(this.txtNombre);
            this.panel1.Controls.Add(this.lblDescripcion);
            this.panel1.Location = new System.Drawing.Point(20, 398);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(495, 70);
            this.panel1.TabIndex = 21;
            // 
            // lblListaFamilias
            // 
            this.lblListaFamilias.AutoSize = true;
            this.lblListaFamilias.Location = new System.Drawing.Point(17, 60);
            this.lblListaFamilias.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListaFamilias.Text = "Familias";
            this.lblListaFamilias.Name = "lblListaFamilias";
            // 
            // lblListaContenido
            // 
            this.lblListaContenido.AutoSize = true;
            this.lblListaContenido.Location = new System.Drawing.Point(282, 60);
            this.lblListaContenido.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListaContenido.Text = "Contenido";
            this.lblListaContenido.Name = "lblListaContenido";
            // 
            // FormGestionFamilias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 525);
            this.Controls.Add(this.panel1);
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
            this.Controls.Add(this.lstDisponibles);
            this.Controls.Add(this.lblPatentesDisponibles);
            this.Controls.Add(this.lstContenido);
            this.Controls.Add(this.lstFamilias);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblListaFamilias);
            this.Controls.Add(this.lblListaContenido);
            this.Name = "FormGestionFamilias";
            this.Text = "FormGestionFamilias";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormGestionFamilias_FormClosed);
            this.Load += new System.EventHandler(this.FormGestionFamilias_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.ListBox lstFamilias;
        private System.Windows.Forms.ListBox lstContenido;
        private System.Windows.Forms.Label lblPatentesDisponibles;
        private System.Windows.Forms.ListBox lstDisponibles;
        private System.Windows.Forms.RadioButton rbPatente;
        private System.Windows.Forms.RadioButton rbFamilia;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label lblModo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblListaFamilias;
        private System.Windows.Forms.Label lblListaContenido;
    }
}