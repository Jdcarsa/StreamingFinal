using BasesDatosFormulario;
using PlataformaStreaming.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlataformaStreaming.Vista
{
    public partial class RegistrarActuacion : Form
    {
        Producto pro = new Producto();
        Actor actor = new Actor();
        public RegistrarActuacion()
        {

            InitializeComponent();
            //Posiciona la ventana en el "centro " de la ventana padre
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(400, 150);
            dgvProductos = pro.proyectarProductos(dgvProductos);
            dgvActores = actor.proyectarActores(dgvActores);

            pro.llenarcbCodigos(cboCodProducto);
            actor.llenarcbCodigoActor(cboCodActor);
        }

        private void btnRegistrarActuacion_Click(object sender, EventArgs e)
        {
            actor.registrarActuacion(int.Parse(cboCodProducto.Text), int.Parse(cboCodActor.Text), cboxPapel.Text);
        }
    }
}
