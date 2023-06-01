namespace PlataformaStreaming.Vista
{
    partial class Registro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Registro));
            this.btnBack = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCrear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tboxPNombre = new System.Windows.Forms.TextBox();
            this.timePicker = new System.Windows.Forms.DateTimePicker();
            this.tboxSNombre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbTelf = new System.Windows.Forms.TextBox();
            this.tbPApellido = new System.Windows.Forms.TextBox();
            this.tbSApellido = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.tbUsuario = new System.Windows.Forms.TextBox();
            this.tbContrasenia = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnOcultarContrasenia = new System.Windows.Forms.Button();
            this.btnMostrarContrasenia = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(47)))), ((int)(((byte)(135)))));
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(12, 416);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(80, 25);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Volver";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 36);
            this.label1.TabIndex = 2;
            this.label1.Text = "TV PLUS +";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.UseMnemonic = false;
            // 
            // btnCrear
            // 
            this.btnCrear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(47)))), ((int)(((byte)(135)))));
            this.btnCrear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrear.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrear.Location = new System.Drawing.Point(89, 346);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(140, 36);
            this.btnCrear.TabIndex = 3;
            this.btnCrear.Text = "Confirmar";
            this.btnCrear.UseVisualStyleBackColor = false;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(83, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 36);
            this.label2.TabIndex = 4;
            this.label2.Text = "Registrarse";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.UseMnemonic = false;
            // 
            // tboxPNombre
            // 
            this.tboxPNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxPNombre.Location = new System.Drawing.Point(6, 18);
            this.tboxPNombre.MaxLength = 20;
            this.tboxPNombre.Name = "tboxPNombre";
            this.tboxPNombre.Size = new System.Drawing.Size(144, 26);
            this.tboxPNombre.TabIndex = 28;
            this.tboxPNombre.TextChanged += new System.EventHandler(this.tboxPNombre_TextChanged);
            this.tboxPNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tboxNombre_KeyPress);
            // 
            // timePicker
            // 
            this.timePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.timePicker.Location = new System.Drawing.Point(173, 132);
            this.timePicker.MinDate = new System.DateTime(1930, 1, 1, 0, 0, 0, 0);
            this.timePicker.Name = "timePicker";
            this.timePicker.Size = new System.Drawing.Size(144, 27);
            this.timePicker.TabIndex = 1;
            // 
            // tboxSNombre
            // 
            this.tboxSNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxSNombre.Location = new System.Drawing.Point(172, 18);
            this.tboxSNombre.MaxLength = 20;
            this.tboxSNombre.Name = "tboxSNombre";
            this.tboxSNombre.Size = new System.Drawing.Size(144, 26);
            this.tboxSNombre.TabIndex = 28;
            this.tboxSNombre.TextChanged += new System.EventHandler(this.tboxSNombre_TextChanged);
            this.tboxSNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tboxSNombre_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 17);
            this.label3.TabIndex = 28;
            this.label3.Text = "Primer Nombre";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(169, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 17);
            this.label4.TabIndex = 28;
            this.label4.Text = "Segundo Nombre";
            // 
            // tbTelf
            // 
            this.tbTelf.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTelf.Location = new System.Drawing.Point(6, 131);
            this.tbTelf.MaxLength = 20;
            this.tbTelf.Name = "tbTelf";
            this.tbTelf.Size = new System.Drawing.Size(144, 26);
            this.tbTelf.TabIndex = 15;
            this.tbTelf.TextChanged += new System.EventHandler(this.tbTelf_TextChanged);
            this.tbTelf.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTelf_KeyPress);
            // 
            // tbPApellido
            // 
            this.tbPApellido.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPApellido.Location = new System.Drawing.Point(6, 77);
            this.tbPApellido.MaxLength = 20;
            this.tbPApellido.Name = "tbPApellido";
            this.tbPApellido.Size = new System.Drawing.Size(144, 26);
            this.tbPApellido.TabIndex = 16;
            this.tbPApellido.TextChanged += new System.EventHandler(this.tbPApellido_TextChanged);
            this.tbPApellido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPApellido_KeyPress);
            // 
            // tbSApellido
            // 
            this.tbSApellido.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSApellido.Location = new System.Drawing.Point(173, 77);
            this.tbSApellido.MaxLength = 20;
            this.tbSApellido.Name = "tbSApellido";
            this.tbSApellido.Size = new System.Drawing.Size(144, 26);
            this.tbSApellido.TabIndex = 17;
            this.tbSApellido.TextChanged += new System.EventHandler(this.tbSApellido_TextChanged);
            this.tbSApellido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSApellido_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(3, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Primer Apellido";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(170, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 17);
            this.label6.TabIndex = 19;
            this.label6.Text = "Segundo Apellido";
            // 
            // tbEmail
            // 
            this.tbEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbEmail.Location = new System.Drawing.Point(6, 182);
            this.tbEmail.MaxLength = 200;
            this.tbEmail.Multiline = true;
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Size = new System.Drawing.Size(311, 28);
            this.tbEmail.TabIndex = 20;
            this.tbEmail.TextChanged += new System.EventHandler(this.tbEmail_TextChanged);
            this.tbEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbEmail_KeyPress);
            // 
            // tbUsuario
            // 
            this.tbUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbUsuario.Location = new System.Drawing.Point(6, 233);
            this.tbUsuario.MaxLength = 20;
            this.tbUsuario.Multiline = true;
            this.tbUsuario.Name = "tbUsuario";
            this.tbUsuario.Size = new System.Drawing.Size(310, 28);
            this.tbUsuario.TabIndex = 21;
            this.tbUsuario.TextChanged += new System.EventHandler(this.tbUsuario_TextChanged);
            this.tbUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbUsuario_KeyPress);
            // 
            // tbContrasenia
            // 
            this.tbContrasenia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbContrasenia.Location = new System.Drawing.Point(6, 284);
            this.tbContrasenia.MaxLength = 20;
            this.tbContrasenia.Multiline = true;
            this.tbContrasenia.Name = "tbContrasenia";
            this.tbContrasenia.PasswordChar = '*';
            this.tbContrasenia.Size = new System.Drawing.Size(311, 28);
            this.tbContrasenia.TabIndex = 22;
            this.tbContrasenia.TextChanged += new System.EventHandler(this.tbContrasenia_TextChanged);
            this.tbContrasenia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbContrasenia_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(170, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 17);
            this.label7.TabIndex = 23;
            this.label7.Text = "Fecha de nacimiento";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(3, 111);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 17);
            this.label8.TabIndex = 24;
            this.label8.Text = "Telefono";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(3, 162);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 17);
            this.label9.TabIndex = 25;
            this.label9.Text = "Correo Electronico";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(3, 213);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 17);
            this.label10.TabIndex = 26;
            this.label10.Text = "Usuario";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(3, 264);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 17);
            this.label11.TabIndex = 27;
            this.label11.Text = "Contraseña";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(58)))), ((int)(((byte)(93)))));
            this.panel5.Controls.Add(this.btnOcultarContrasenia);
            this.panel5.Controls.Add(this.btnMostrarContrasenia);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.tbContrasenia);
            this.panel5.Controls.Add(this.btnCrear);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.tboxPNombre);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.tbUsuario);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.tboxSNombre);
            this.panel5.Controls.Add(this.tbEmail);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.timePicker);
            this.panel5.Controls.Add(this.tbPApellido);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.tbTelf);
            this.panel5.Controls.Add(this.tbSApellido);
            this.panel5.Location = new System.Drawing.Point(230, 56);
            this.panel5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(332, 399);
            this.panel5.TabIndex = 1;
            // 
            // btnOcultarContrasenia
            // 
            this.btnOcultarContrasenia.BackColor = System.Drawing.Color.White;
            this.btnOcultarContrasenia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOcultarContrasenia.FlatAppearance.BorderSize = 0;
            this.btnOcultarContrasenia.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOcultarContrasenia.ForeColor = System.Drawing.Color.Transparent;
            this.btnOcultarContrasenia.Image = ((System.Drawing.Image)(resources.GetObject("btnOcultarContrasenia.Image")));
            this.btnOcultarContrasenia.Location = new System.Drawing.Point(291, 286);
            this.btnOcultarContrasenia.Name = "btnOcultarContrasenia";
            this.btnOcultarContrasenia.Size = new System.Drawing.Size(25, 24);
            this.btnOcultarContrasenia.TabIndex = 30;
            this.btnOcultarContrasenia.UseVisualStyleBackColor = false;
            this.btnOcultarContrasenia.Click += new System.EventHandler(this.btnOcultarContrasenia_Click);
            // 
            // btnMostrarContrasenia
            // 
            this.btnMostrarContrasenia.BackColor = System.Drawing.Color.White;
            this.btnMostrarContrasenia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMostrarContrasenia.FlatAppearance.BorderSize = 0;
            this.btnMostrarContrasenia.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMostrarContrasenia.ForeColor = System.Drawing.Color.Transparent;
            this.btnMostrarContrasenia.Image = ((System.Drawing.Image)(resources.GetObject("btnMostrarContrasenia.Image")));
            this.btnMostrarContrasenia.Location = new System.Drawing.Point(291, 286);
            this.btnMostrarContrasenia.Margin = new System.Windows.Forms.Padding(0);
            this.btnMostrarContrasenia.Name = "btnMostrarContrasenia";
            this.btnMostrarContrasenia.Size = new System.Drawing.Size(25, 24);
            this.btnMostrarContrasenia.TabIndex = 29;
            this.btnMostrarContrasenia.UseVisualStyleBackColor = false;
            this.btnMostrarContrasenia.Click += new System.EventHandler(this.btnMostrarContrasenia_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(53)))), ((int)(((byte)(101)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(230, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 57);
            this.panel1.TabIndex = 5;
            // 
            // Registro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(41)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBack);
            this.Name = "Registro";
            this.Text = "Registro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Registro_FormClosing);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tboxPNombre;
        private System.Windows.Forms.DateTimePicker timePicker;
        private System.Windows.Forms.TextBox tboxSNombre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbTelf;
        private System.Windows.Forms.TextBox tbPApellido;
        private System.Windows.Forms.TextBox tbSApellido;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbEmail;
        private System.Windows.Forms.TextBox tbUsuario;
        private System.Windows.Forms.TextBox tbContrasenia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOcultarContrasenia;
        private System.Windows.Forms.Button btnMostrarContrasenia;
    }
}