using BasesDatosFormulario;
using PlataformaStreaming.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace PlataformaStreaming.Vista
{
    public partial class Registro : Form
    {
        PanelPrincipal panel;
        Cliente cliente = new Cliente();
        public Registro(PanelPrincipal panel)
        {
            InitializeComponent();
            this.panel = panel;
            this.StartPosition = FormStartPosition.CenterScreen;
            btnCrear.Enabled = false;
            btnMostrarContrasenia.FlatStyle = FlatStyle.Flat;
            btnMostrarContrasenia.FlatAppearance.BorderSize = 0;
            btnOcultarContrasenia.FlatStyle = FlatStyle.Flat;
            btnOcultarContrasenia.FlatAppearance.BorderSize = 0;
            btnMostrarContrasenia.BringToFront();

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();//cierra esta ventana
            panel.Show();//vuelve a mostrar el panel principal
        }

        private void tboxNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarEntradas(e);
        }

        private void Registro_FormClosing(object sender, FormClosingEventArgs e)
        {
            
           panel.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void tboxSNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarEntradas(e);
        }

        private void tbPApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarEntradas(e);
            
        }

        private void tbSApellido_TextChanged(object sender, EventArgs e)
        {
            validadorLongitud(tbSApellido);
            habilitarBtn();
        }

        private void tbSApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarEntradas(e);
        }

        private void tbUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarEspacios(e);
        }

        private void tbContrasenia_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarEspacios(e);
        }

        private void tbEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarEspacios(e);
        }

        private void tbTelf_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Ingrese solo números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }

        }
        //registra al cliente en la base de datos
        private void btnCrear_Click(object sender, EventArgs e)
        {
            Conexion conexion = new Conexion();
            int edad = DateTime.Today.Year - timePicker.Value.Year;
            if(cliente.registrarCliente(tbUsuario.Text, tboxPNombre.Text,
                tboxSNombre.Text, tbPApellido.Text, tbSApellido.Text, tbEmail.Text
               , tbTelf.Text, tbContrasenia.Text, timePicker.Text, edad))
            {
                this.Hide(); // cierra esta ventana
                PlanesSuscripcion planes = new PlanesSuscripcion(panel, this, tbUsuario.Text);
                planes.Show();// abre la siguiente ventana, planes de suscripcion
            }
            else
            {
                MessageBox.Show("Vuelva a intentarlo");
            }
        }

        private void tbTelf_TextChanged(object sender, EventArgs e)
        {
            validadorLongitud(tbTelf);
            habilitarBtn();
        }

        private void tboxPNombre_TextChanged(object sender, EventArgs e)
        {
            validadorLongitud(tboxPNombre);
            habilitarBtn();
        }

        private void tboxSNombre_TextChanged(object sender, EventArgs e)
        {
            validadorLongitud(tboxSNombre);
        }

        private void tbPApellido_TextChanged(object sender, EventArgs e)
        {
            validadorLongitud(tbPApellido);
            habilitarBtn();
        }

        private void tbUsuario_TextChanged(object sender, EventArgs e)
        {
           
            habilitarBtn();
        }
        


        private void tbContrasenia_TextChanged(object sender, EventArgs e)
        {
            
            habilitarBtn();
        }

        private void tbEmail_TextChanged(object sender, EventArgs e)
        {
            if (tbEmail.Text.Length > 35)
            {
                MessageBox.Show("Solo puede ingresar 35 caracteres");
                tbEmail.Text = tbEmail.Text.Substring(0, 35);
                tbEmail.SelectionStart = tbEmail.Text.Length;
            }
            habilitarBtn();
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

        //Verifica que solo se ingresen letras
        private void validarEntradas(KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Ingrese solo letras", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //Verifica que no se ingrese un espacio en blanco
        private void validarEspacios(KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Habilita el boton cuando los campos obligatorios esten llenos
        private void habilitarBtn()
        {
            if (tboxPNombre.Text != string.Empty 
                && tbPApellido.Text !=string.Empty
                && tbSApellido.Text !=string.Empty
                && tbEmail.Text != string.Empty
                && tbUsuario.Text != string.Empty
                && tbContrasenia.Text != string.Empty
                && tbTelf.Text != string.Empty)
            {
                btnCrear.Enabled = true;
            }
            else
            {
                btnCrear.Enabled = false;
            }
        }


        //Muestra el txt del tbox contrasenia
        private void btnMostrarContrasenia_Click(object sender, EventArgs e)
        {
            if (tbContrasenia.PasswordChar == '*')
            {
                btnOcultarContrasenia.BringToFront();
                tbContrasenia.PasswordChar = '\0';
            }
        }
        //Oculta el txt del tbox contrasenia
        private void btnOcultarContrasenia_Click(object sender, EventArgs e)
        {
            if (tbContrasenia.PasswordChar == '\0')
            {
                btnMostrarContrasenia.BringToFront();
                tbContrasenia.PasswordChar = '*';
            }
        }
    }
}
