using PlataformaStreaming.Control;
using PlataformaStreaming.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PlataformaStreaming.Vista
{
    public partial class ActualizarCliente : Form
    {
        ClienteControlador cliente = new ClienteControlador();
        int codigo = 0;
        public ActualizarCliente(string prmUsuario)
        {
            string usuario = prmUsuario;

            InitializeComponent();
            dtpNacimiento.Format = DateTimePickerFormat.Custom;
            dtpNacimiento.CustomFormat = "dd/MM/yyyy";

            List<string> valores = new List<string>();

            if (cliente.cargarClienteConUsuario(usuario, ref valores))
            {
                txtPrimerNombre.Text = valores[0];
                txtSegundoNombre.Text = valores[1];
                txtPrimerApellido.Text = valores[2];
                txtSegundoApellido.Text = valores[3];
                txtTelefono.Text = valores[4];
                dtpNacimiento.Text = valores[5];
                txtCorreo.Text = valores[6];
                txtNombreUsuario.Text = valores[7];
                codigo = int.Parse(valores[9]);
            }

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string contrasenia = "";
            if (cliente.recuperarContrasenia(codigo, "CLIENTE", ref contrasenia))
            {
                if (contrasenia.CompareTo(txtContrasenia.Text) == 0)
                {
                    if (txtContraseniaNueva.Text.Length != 0)
                    {
                        contrasenia = txtContraseniaNueva.Text;
                    }

                    Cliente clienteActualizado = new Cliente
                    {
                        Codigo = codigo,
                        NombreUsuarioCliente = txtNombreUsuario.Text,
                        PrimerNombre = txtPrimerNombre.Text,
                        SegundoNombre = txtSegundoNombre.Text,
                        PrimerApellido = txtPrimerApellido.Text,
                        SegundoApellido = txtSegundoApellido.Text,
                        FechaNacimiento = DateTime.Parse(dtpNacimiento.Text),
                        Contrasenia = contrasenia,
                        Telefono = txtTelefono.Text,
                        Correo = txtCorreo.Text
                    };

                    if (cliente.actualizarCliente(clienteActualizado))
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
