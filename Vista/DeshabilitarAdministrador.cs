using PlataformaStreaming.Control;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlataformaStreaming.Vista
{
    public partial class DeshabilitarAdministrador : Form
    {
        AdminControlador admin = new AdminControlador();
        public DeshabilitarAdministrador()
        {
            InitializeComponent();
            //Posiciona la ventana en el "centro " de la ventana padre
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(380, 160);

            dgvAdministradores = admin.proyectarAdministradores(dgvAdministradores);

            admin.llenarcbCodigoAdministrador(cboxCodigo);
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            admin.deshabilitarAdministrador(int.Parse(cboxCodigo.Text));
        }
    }
}
