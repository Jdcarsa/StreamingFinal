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

namespace PlataformaStreaming.Vista
{
    public partial class ActualizarCliente : Form
    {
        Cliente cliente = new Cliente();
        public ActualizarCliente()
        {
            InitializeComponent();
            dtpNacimiento.Format = DateTimePickerFormat.Custom;
            dtpNacimiento.CustomFormat = "dd/MM/yyyy";

            List<string> valores = new List<string>();

            if (cliente.cargarClienteUI(1, ref valores))
            {
                txtPrimerNombre.Text = valores[0];
                txtSegundoNombre.Text = valores[1];
                txtPrimerApellido.Text = valores[2];
                txtSegundoApellido.Text = valores[3];
                txtTelefono.Text = valores[4];
                dtpNacimiento.Text = valores[5];
                txtCorreo.Text = valores[6];
                txtNombreUsuario.Text = valores[7];
            }

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string contrasenia = "";
            if (cliente.recuperarContrasenia(1, "CLIENTE", ref contrasenia))
            {
                if (contrasenia.CompareTo(txtContrasenia.Text) == 0)
                {
                    if (txtContraseniaNueva.Text.Length != 0)
                    {
                        contrasenia = txtContraseniaNueva.Text;
                    }
                    if (cliente.actualizarCliente(1, txtNombreUsuario.Text, txtPrimerNombre.Text, txtSegundoNombre.Text, txtPrimerApellido.Text, txtSegundoApellido.Text,
                        dtpNacimiento.Text, txtContraseniaNueva.Text, txtTelefono.Text, txtCorreo.Text))
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("La contraseña ingresada no es correcta");
                }

            }
        }
    }
}
