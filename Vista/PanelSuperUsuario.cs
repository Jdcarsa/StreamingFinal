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
    public partial class PanelSuperUsuario : Form
    {
        PanelPrincipal panel;
        public PanelSuperUsuario(PanelPrincipal panel)
        {
            InitializeComponent();
            this.panel = panel;
        }

        private void btnActualizarDatos_MouseMove(object sender, MouseEventArgs e)
        {
            btnActualizarDatos.BackColor = Color.FromArgb(63, 64, 150);
        }

        private void btnActualizarDatos_MouseLeave(object sender, EventArgs e)
        {
            btnActualizarDatos.BackColor = Color.FromArgb(63, 58, 93);
        }

        private void btnDeshabilitarAdmin_MouseLeave(object sender, EventArgs e)
        {
            btnDeshabilitarAdmin.BackColor = Color.FromArgb(63, 58, 93);
        }

        private void btnDeshabilitarAdmin_MouseMove(object sender, MouseEventArgs e)
        {
            btnDeshabilitarAdmin.BackColor = Color.FromArgb(63, 64, 150);
        }

        private void btnCrearAdmin_MouseLeave(object sender, EventArgs e)
        {
            btnCrearAdmin.BackColor = Color.FromArgb(63, 58, 93);

        }

        private void btnCrearAdmin_MouseMove(object sender, MouseEventArgs e)
        {
            btnCrearAdmin.BackColor = Color.FromArgb(63, 64, 150);
        }

        private void btnCrearAdmin_Click(object sender, EventArgs e)
        {
            CrearAdministrador crear = new CrearAdministrador();
            crear.Show();
        }

        private void btnActualizarAdmin_Click(object sender, EventArgs e)
        {
            ActualizarAdministrador_SU actualizarAdministrador = new ActualizarAdministrador_SU();
            actualizarAdministrador.Show();
        }

        private void btnActualizarAdmin_MouseMove(object sender, MouseEventArgs e)
        {
            btnActualizarAdmin.BackColor = Color.FromArgb(63, 64, 150);
        }

        private void btnActualizarAdmin_MouseLeave(object sender, EventArgs e)
        {
            btnActualizarAdmin.BackColor = Color.FromArgb(63, 58, 93);
        }

        private void btnDeshabilitarAdmin_Click(object sender, EventArgs e)
        {
            DeshabilitarAdministrador deshabilitarAdministrador = new DeshabilitarAdministrador();
            deshabilitarAdministrador.Show();
        }

        private void btnActualizarDatos_Click(object sender, EventArgs e)
        {
            ActualizarSuperUsuario actualizarSuper = new ActualizarSuperUsuario();
            actualizarSuper.Show();
        }

        private void PanelSuperUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            panel.Close();
        }
    }
}
