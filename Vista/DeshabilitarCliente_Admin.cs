using PlataformaStreaming.Control;
using System;
using System.Windows.Forms;

namespace PlataformaStreaming.Vista
{
    public partial class DeshabilitarCliente_Admin : Form
    {
        ClienteControlador cliente = new ClienteControlador();
        public DeshabilitarCliente_Admin()
        {
            InitializeComponent();
            dgvClientes = cliente.proyectarClientes(dgvClientes);

            cliente.llenarcbCodigoCliente(cboxCodigo);
        }

        private void txtCodigoCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCodigoCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cboxCodigo.Text))
            {
                cliente.deshabilitarCliente(int.Parse(cboxCodigo.Text));
            }
            else
            {
                Console.WriteLine("Digite un código para deshabilitar");
            }
        }
    }
}
