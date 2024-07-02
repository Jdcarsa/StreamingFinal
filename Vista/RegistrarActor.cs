using PlataformaStreaming.Control;
using PlataformaStreaming.Modelo.Entidades;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlataformaStreaming.Vista
{
    public partial class RegistrarActor : Form
    {

        ActorControlador actor = new ActorControlador();

        public RegistrarActor()
        {
            InitializeComponent();
            //Posiciona la ventana en el "centro " de la ventana padre
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(590, 230);
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Crear el objeto Actor
            Actor nuevoActor = new Actor
            {
                PrimerNombre = txtPrimerNombre.Text,
                SegundoNombre = txtSegundoNombre.Text,
                PrimerApellido = txtPrimerApellido.Text,
                SegundoApellido = txtSegundoApellido.Text,
                FechaNacimiento = dtpFechaNacimiento.Value
            };

            ActorControlador actorControlador = new ActorControlador();
            if (actorControlador.registrarActor(nuevoActor))
            {
                MessageBox.Show("Se ha registrado correctamente el actor",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Vuelva a intentarlo");
            }
        }

    }
}
