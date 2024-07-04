using PlataformaStreaming.Control;
using PlataformaStreaming.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PlataformaStreaming.Vista
{
    public partial class ActualizarCliente_Admin : Form
    {
        ClienteControlador cliente = new ClienteControlador();
        public ActualizarCliente_Admin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(400, 110);
            dtpNacimiento.Format = DateTimePickerFormat.Custom;
            dtpNacimiento.CustomFormat = "dd/MM/yyyy";

            dgvClientes = cliente.proyectarClientes(dgvClientes);
            cliente.llenarcbCodigoCliente(cboxCodigo);
        }

        private void cboxCodigo_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> valores = new List<string>();
            if (cliente.cargarClienteConCodigo(cboxCodigo.Text, ref valores))
            {
                txtPrimerNombre.Text = valores[0];
                txtSegundoNombre.Text = valores[1];
                txtPrimerApellido.Text = valores[2];
                txtSegundoApellido.Text = valores[3];
                txtTelefono.Text = valores[4];
                dtpNacimiento.Text = valores[5];
                txtCorreo.Text = valores[6];
                txtNombreUsuario.Text = valores[7];
                txtContraseniaNueva.Text = valores[8];
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            Cliente clienteActualizado = new Cliente
            {
                Codigo = int.Parse(cboxCodigo.Text),
                NombreUsuarioCliente = txtNombreUsuario.Text,
                PrimerNombre = txtPrimerNombre.Text,
                SegundoNombre = txtSegundoNombre.Text,
                PrimerApellido = txtPrimerApellido.Text,
                SegundoApellido = txtSegundoApellido.Text,
                FechaNacimiento = DateTime.Parse(dtpNacimiento.Text),
                Contrasenia = txtContraseniaNueva.Text,
                Telefono = txtTelefono.Text,
                Correo = txtCorreo.Text
            };

            if (cliente.actualizarCliente(clienteActualizado))
            {
                this.Close();
            }
        }

    }
}
