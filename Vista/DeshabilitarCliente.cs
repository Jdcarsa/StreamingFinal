using PlataformaStreaming.Control;
using System;
using System.Windows.Forms;

namespace PlataformaStreaming.Vista
{
    public partial class DeshabilitarCliente : Form
    {
        ClienteControlador cliente = new ClienteControlador();
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
