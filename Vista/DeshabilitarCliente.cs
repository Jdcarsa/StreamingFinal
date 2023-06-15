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
        PanelPrincipal panelPrincipal;
        PanelPrincipal nuevo = new PanelPrincipal();
        string usuario;
        public DeshabilitarCliente(string usuario, PanelPrincipal panelPrincipal)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.panelPrincipal = panelPrincipal;
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            int codigo = int.Parse(cliente.buscarCodigoCliente(usuario));
            cliente.deshabilitarCliente(codigo);

            this.Close();
            this.panelPrincipal.Hide();
            nuevo.Show();
            //Aquí se debe insertar el código del cliente como parámetro
        }
    }
}
