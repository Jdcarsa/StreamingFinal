using PlataformaStreaming.Control;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PlataformaStreaming.Vista
{
    public partial class ActualizarAdministrador_SU : Form
    {
        AdminControlador admin = new AdminControlador();
        public ActualizarAdministrador_SU()
        {
            InitializeComponent();
            //Posiciona la ventana en el "centro " de la ventana padre
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(400, 160);
            dgvAdministradores = admin.proyectarAdministradores(dgvAdministradores);

            admin.llenarcbCodigoAdministrador(cboxCodigo);

            List<string> valores = new List<string>();

        }

        private void cboxCodigo_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> valores = new List<string>();

            if (admin.cargarAdmin(int.Parse(cboxCodigo.Text), ref valores))
            {
                txtPrimerNombre.Text = valores[0];
                txtSegundoNombre.Text = valores[1];
                txtPrimerApellido.Text = valores[2];
                txtSegundoApellido.Text = valores[3];
                txtTelefono.Text = valores[4];
                txtContraseniaNueva.Text = valores[5];
                txtNombreUsuario.Text = valores[6];
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            admin.actualizarAdmin(int.Parse(cboxCodigo.Text), txtNombreUsuario.Text, txtPrimerNombre.Text, txtSegundoNombre.Text,
                    txtPrimerApellido.Text, txtSegundoApellido.Text, txtContraseniaNueva.Text, txtTelefono.Text);
        }
    }
}
