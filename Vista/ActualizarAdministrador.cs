using PlataformaStreaming.Control;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PlataformaStreaming.Vista
{
    public partial class ActualizarAdministrador : Form
    {
        AdminControlador admin = new AdminControlador();
        string contraseniaAux;
        public int idAdmin;
        public ActualizarAdministrador(int idAdmin)
        {
            InitializeComponent();
            //Posiciona la ventana en el "centro " de la ventana padre
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(520, 140);


            List<string> valores = new List<string>();

            if (admin.cargarAdmin(idAdmin, ref valores))
            {
                txtPrimerNombre.Text = valores[0];
                txtSegundoNombre.Text = valores[1];
                txtPrimerApellido.Text = valores[2];
                txtSegundoApellido.Text = valores[3];
                txtTelefono.Text = valores[4];
                contraseniaAux = valores[5];
                txtNombreUsuario.Text = valores[6];
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (txtContrasenia.Text == contraseniaAux)
            {
                admin.actualizarAdmin(idAdmin, txtNombreUsuario.Text, txtPrimerNombre.Text, txtSegundoNombre.Text,
                    txtPrimerApellido.Text, txtSegundoApellido.Text, txtContraseniaNueva.Text, txtTelefono.Text);
            }
        }
    }
}
