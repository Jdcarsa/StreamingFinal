using PlataformaStreaming.Control;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlataformaStreaming.Vista
{
    public partial class DeshabilitarProducto : Form
    {
        ProductoControlador pro = new ProductoControlador();
        public DeshabilitarProducto()
        {
            InitializeComponent();
            //Posiciona la ventana en el "centro " de la ventana padre
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(480, 160);
            dgvProductos = pro.proyectarProductos(dgvProductos);

            pro.llenarcbCodigosHabilitados(cboCodProducto);
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            pro.deshabilitarProducto(int.Parse(cboCodProducto.Text));
        }
    }
}
