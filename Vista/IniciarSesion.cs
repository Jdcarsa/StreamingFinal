using PlataformaStreaming.Control;
using PlataformaStreaming.Modelo;
using System;
using System.Windows.Forms;

namespace PlataformaStreaming.Vista
{
    public partial class IniciarSesion : Form
    {
        PanelPrincipal panel;
        public IniciarSesion(PanelPrincipal panel)
        {
            InitializeComponent();
            this.panel = panel;
            this.StartPosition = FormStartPosition.CenterScreen;
            btnIngresar.Enabled = false;
            btnMostrarContrasenia.FlatStyle = FlatStyle.Flat;
            btnMostrarContrasenia.FlatAppearance.BorderSize = 0;
            btnOcultarContrasenia.FlatStyle = FlatStyle.Flat;
            btnOcultarContrasenia.FlatAppearance.BorderSize = 0;
            btnMostrarContrasenia.BringToFront();
        }



        private void IniciarSesion_FormClosing(object sender, FormClosingEventArgs e)
        {
            //panel.Show();
        }

        private void btnOcultarContrasenia_Click(object sender, EventArgs e)
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

        // Habilita el boton cuando los campos obligatorios esten llenos
        private void habilitarBtn()
        {
            if (tbUsuario.Text != string.Empty
                && tbContrasenia.Text != string.Empty)
            {
                btnIngresar.Enabled = true;
            }
            else
            {
                btnIngresar.Enabled = false;
            }
        }

        private void tbUsuario_TextChanged(object sender, EventArgs e)
        {
            validadorLongitud(tbUsuario);
            habilitarBtn();
        }

        private void tbContrasenia_TextChanged(object sender, EventArgs e)
        {
            validadorLongitud(tbContrasenia);
            habilitarBtn();
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

        private void validadorLongitud(System.Windows.Forms.TextBox text)
        {
            if (text.Text.Length > 15)
            {
                MessageBox.Show("Solo puede ingresar 15 caracteres");
                text.Text = text.Text.Substring(0, 15);
                text.SelectionStart = text.Text.Length;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            panel.Show();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            Servicio servicio = new Servicio();

            switch ((servicio.accesoPlataforma(tbUsuario.Text, tbContrasenia.Text)))
            {
                case -1:
                    MessageBox.Show("La contraseña ingresada incorrecta",
                        "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;

                case 0:
                    PanelSuperUsuario panelSuper = new PanelSuperUsuario(panel);
                    panelSuper.Show();
                    this.Close();
                    break;

                case 1:
                    AdminControlador a = new AdminControlador();
                    PanelAdministrador panelAdmin = new PanelAdministrador(a.recuperaCodigo(tbUsuario.Text), panel);
                    panelAdmin.Show();
                    this.Close();
                    break;

                case 2:
                    VerCatalogo catalogo = new VerCatalogo(tbUsuario.Text, panel);
                    catalogo.Show();
                    this.Close();
                    break;

                case 3:
                    MessageBox.Show("El usuario ingreso se encuentra deshabilitado",
                        "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;

                case 4:
                    MessageBox.Show("El usuario no existe",
                        "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;

            }
        }
    }
}
