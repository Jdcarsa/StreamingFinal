using PlataformaStreaming.Control;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlataformaStreaming.Vista
{
    public partial class ActualizarSuperUsuario : Form
    {
        public ActualizarSuperUsuario()
        {
            InitializeComponent();
            //Posiciona la ventana en el "centro " de la ventana padre
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(570, 280);
        }

        private void txtRegistrar_Click(object sender, EventArgs e)
        {
            AdminControlador super = new AdminControlador();

            super.actualizarSU(txtContraseniaActual.Text, txtContraseniaNueva.Text);

        }
    }
}
