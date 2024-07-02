using PlataformaStreaming.Vista;
using System;
using System.Windows.Forms;

namespace PlataformaStreaming
{
    public partial class PanelPrincipal : Form
    {

        public PanelPrincipal()
        {
            InitializeComponent();

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            this.Hide();//Oculta la ventana actual
            IniciarSesion inicioSesion = new IniciarSesion(this);
            inicioSesion.Show();//Muestra la siguiente ventana , iniciar sesion
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registro registro = new Registro(this);
            registro.Show();
        }
    }
}
