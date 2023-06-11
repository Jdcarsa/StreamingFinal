using BasesDatosFormulario;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using PlataformaStreaming.Control;

namespace PlataformaStreaming.Vista
{
    public partial class ActualizarCliente_Admin : Form
    {
        Cliente cliente = new Cliente();
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

            if (cliente.actualizarCliente(int.Parse(cboxCodigo.Text), txtNombreUsuario.Text, txtPrimerNombre.Text, txtSegundoNombre.Text,
                txtPrimerApellido.Text, txtSegundoApellido.Text, dtpNacimiento.Text, txtContraseniaNueva.Text, txtTelefono.Text, txtCorreo.Text))
            {
                this.Close();
            }
        }
    }
}
