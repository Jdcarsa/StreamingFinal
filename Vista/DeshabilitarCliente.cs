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
    public partial class DeshabilitarCliente : Form
    {
        Cliente cliente = new Cliente();
        public DeshabilitarCliente()
        {
            InitializeComponent();
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            cliente.deshabilitarCliente(38);

            //Aquí se debe insertar el código del cliente como parámetro
        }
    }
}
