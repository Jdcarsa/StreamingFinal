using PlataformaStreaming.Control;
using System;
using System.Windows.Forms;

namespace PlataformaStreaming.Vista
{
    public partial class DiccionarioDatos : Form
    {
        Diccionario diccionario = new Diccionario();
        static int caso;

        public DiccionarioDatos()
        {
            InitializeComponent();
        }

        private void btnRegistrosTablas_Click(object sender, EventArgs e)
        {
            lblRegistros.Text = "Mostrando las tablas.";
            diccionario.proyectarTablas(dgvRegistros);
            cbOpcion.Hide();
        }

        private void btnMostrarProcedimientos_Click(object sender, EventArgs e)
        {
            lblRegistros.Text = "Mostrando los procedimientos.";
            diccionario.proyectarProcedimientos(dgvRegistros);
            cbOpcion.Hide();
        }

        private void btnMostrarColumnas_Click(object sender, EventArgs e)
        {
            caso = 0;
            cambiarValores(caso);
            diccionario.llenarcbNombreTablas(cbOpcion);
            lblRegistros.Text = "Mostrando las columnas.";
            cbOpcion.Show();
        }
        private void btnMostrarRestricciones_Click(object sender, EventArgs e)
        {
            caso = 1;
            cambiarValores(caso);
            diccionario.llenarcbNombreTablas(cbOpcion);
            lblRegistros.Text = "Mostrando las restricciones.";
            cbOpcion.Show();
        }

        private void btnMostrarVista_Click(object sender, EventArgs e)
        {
            diccionario.llenarcbNombreVistas(cbOpcion);
            caso = 2;
            cambiarValores(caso);
            lblRegistros.Text = "Mostrando la vista.";
            cbOpcion.Show();
        }

        private void cbOpcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cambiarValores(caso);
        }

        private void cambiarValores(int casoActual) 
        {
            switch (casoActual)
            {
                case 0:
                    
                    diccionario.proyectarColumnas(cbOpcion.Text,dgvRegistros);
                    break;
                case 1:
                    
                    diccionario.proyectarRestricciones(cbOpcion.Text, dgvRegistros);
                    break;
                case 2:                   
                    diccionario.proyectarVista(cbOpcion.Text, dgvRegistros);
                    break;
            }
        }
    }
}
