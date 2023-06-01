using BasesDatosFormulario;
using PlataformaStreaming.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace PlataformaStreaming.Vista
{
    public partial class CrearAdministrador : Form
    {
        Cliente cliente = new Cliente();
        public CrearAdministrador()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(570, 160);
            btnCrear.Enabled = false;
            btnMostrarContrasenia.FlatStyle = FlatStyle.Flat;
            btnMostrarContrasenia.FlatAppearance.BorderSize = 0;
            btnOcultarContrasenia.FlatStyle = FlatStyle.Flat;
            btnOcultarContrasenia.FlatAppearance.BorderSize = 0;
            btnMostrarContrasenia.BringToFront();
        }

       




        private void tbId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Ingrese solo números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void tboxPNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarEspacios(e);
            validarEntradas(e);
        }

        private void tboxSNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarEspacios(e);
            validarEntradas(e);
        }

        private void tbPApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarEspacios(e);
            validarEntradas(e);
        }

        private void tbSApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarEspacios(e);
            validarEntradas(e);
        }

        private void tbEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarEspacios(e);
        }

        private void tbUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarEspacios(e);
        }

        private void tbContrasenia_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarEspacios(e);
            
        }

        private void validarEspacios(KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void validarEntradas(KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Ingrese solo letras", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void validadorLongitud(System.Windows.Forms.TextBox text)
        {
            if (text.Text.Length >= 10)
            {
                MessageBox.Show("Solo puede ingresar 9 caracteres");
                text.Text = text.Text.Substring(0, 10);
                text.SelectionStart = text.Text.Length;
            }
        }

        private void tboxPNombre_TextChanged(object sender, EventArgs e)
        {
            validadorLongitud(tboxPNombre);
            habilitarBtn();
        }

        private void tboxSNombre_TextChanged(object sender, EventArgs e)
        {
            validadorLongitud(tboxSNombre);
            habilitarBtn();
        }

        private void tbPApellido_TextChanged(object sender, EventArgs e)
        {
            validadorLongitud(tbPApellido);
            habilitarBtn();
        }

        private void tbSApellido_TextChanged(object sender, EventArgs e)
        {
            validadorLongitud(tbSApellido);
            habilitarBtn();
        }

        private void tbEmail_TextChanged(object sender, EventArgs e)
        {
            habilitarBtn();
            if (tbEmail.Text.Length > 35)
            {
                MessageBox.Show("Solo puede ingresar 35 caracteres");
                tbEmail.Text = tbEmail.Text.Substring(0, 35);
                tbEmail.SelectionStart = tbEmail.Text.Length;
            }
            
        }

        private void tbUsuario_TextChanged(object sender, EventArgs e)
        {
            habilitarBtn();
            if (tbEmail.Text.Length > 20)
            {
                MessageBox.Show("Solo puede ingresar 20 caracteres");
                tbEmail.Text = tbEmail.Text.Substring(0, 20);
                tbEmail.SelectionStart = tbEmail.Text.Length;
            }
            
        }

        private void habilitarBtn()
        {
            if (tboxPNombre.Text != string.Empty
                && tbPApellido.Text != string.Empty
                && tbSApellido.Text != string.Empty
                && tbEmail.Text != string.Empty
                && tbUsuario.Text != string.Empty
                && tbContrasenia.Text != string.Empty
                && tbId.Text != string.Empty)
            {
                btnCrear.Enabled = true;
            }
            else
            {
                btnCrear.Enabled = false;
            }
        }



        private void btnCrear_Click(object sender, EventArgs e)
        {
            Conexion conexion = new Conexion();
            int edad = DateTime.Today.Year - timePicker.Value.Year;
            Admin admin = new Admin();
            if (admin.registrarAdmin(tbUsuario.Text, tboxPNombre.Text,
                tboxSNombre.Text, tbPApellido.Text, tbSApellido.Text, tbEmail.Text
               , tbTelf.Text, tbContrasenia.Text, timePicker.Text, int.Parse(tbId.Text) , edad))
            {
                MessageBox.Show("Se ha creado correctamente el administrador",
                    "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Vuelva a intentarlo");
            }
        }

        private void btnOcultarContrasenia_Click_1(object sender, EventArgs e)
        {
            if (tbContrasenia.PasswordChar == '\0')
            {
                btnMostrarContrasenia.BringToFront();
                tbContrasenia.PasswordChar = '*';
            }
        }

        private void btnMostrarContrasenia_Click(object sender, EventArgs e)
        {
            if (tbContrasenia.PasswordChar == '*')
            {
                btnOcultarContrasenia.BringToFront();
                tbContrasenia.PasswordChar = '\0';
            }
        }

        private void tbId_TextChanged(object sender, EventArgs e)
        {
            habilitarBtn();
        }

        private void tbTelf_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Ingrese solo números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void tbTelf_TextChanged(object sender, EventArgs e)
        {
            habilitarBtn();
        }

        private void tbContrasenia_TextChanged(object sender, EventArgs e)
        {
            habilitarBtn();
        }
    }
}
