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
    public partial class DeshabilitarProducto : Form
    {
        Producto pro = new Producto();
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
