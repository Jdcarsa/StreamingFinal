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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PlataformaStreaming.Vista
{
    public partial class DeshabilitarCliente_Admin : Form
    {
        Cliente cliente = new Cliente();
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
