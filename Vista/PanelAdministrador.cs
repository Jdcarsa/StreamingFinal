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
    public partial class PanelAdministrador : Form

    {
        private int codigoAdmin = new int();
        PanelPrincipal panel;
        public PanelAdministrador(int codigoAdmin, PanelPrincipal panel)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.codigoAdmin = codigoAdmin;
            this.panel = panel;
        }
        /**
         * Los MouseMove hacen que cambie el color del boton cuando el mouse
         * pasa por encima de ellos y los MouseLeave hacen que vuelvan al color
         * orignal cuando el mouse deja el boton
         */
        private void btnRegistroProducto_MouseMove(object sender, MouseEventArgs e)
        {
            btnRegistroProducto.BackColor = Color.FromArgb(63, 64, 150);
        }

        private void btnRegistroProducto_MouseLeave(object sender, EventArgs e)
        {
            btnRegistroProducto.BackColor = Color.FromArgb(63, 58, 93);
        }

        private void btnActualizarProducto_MouseLeave(object sender, EventArgs e)
        {
            btnActualizarProducto.BackColor = Color.FromArgb(63, 58, 93);
        }

        private void btnActualizarProducto_MouseMove(object sender, MouseEventArgs e)
        {
            btnActualizarProducto.BackColor = Color.FromArgb(63, 64, 150);
        }

        private void btnDeshabilitarProducto_MouseMove(object sender, MouseEventArgs e)
        {
            btnDeshabilitarProducto.BackColor = Color.FromArgb(63, 64, 150);
        }

        private void btnDeshabilitarProducto_MouseLeave(object sender, EventArgs e)
        {
            btnDeshabilitarProducto.BackColor = Color.FromArgb(63, 58, 93);
        }

        private void btnMostrarDatos_MouseMove(object sender, MouseEventArgs e)
        {
            btnMostrarDatos.BackColor = Color.FromArgb(63, 64, 150);
        }

        private void btnMostrarDatos_MouseLeave(object sender, EventArgs e)
        {
            btnMostrarDatos.BackColor = Color.FromArgb(63, 58, 93);
        }

        private void btnActualizarDatos_MouseMove(object sender, MouseEventArgs e)
        {
            btnActualizarDatos.BackColor = Color.FromArgb(63, 64, 150);
        }

        private void btnActualizarDatos_MouseLeave(object sender, EventArgs e)
        {
            btnActualizarDatos.BackColor = Color.FromArgb(63, 58, 93);
        }
        //Llama al form donde se muestran los datos de la pelicula
        private void btnMostrarDatos_Click(object sender, EventArgs e)
        {
            MostrarDatos mostrarDatos = new MostrarDatos();
            mostrarDatos.Show();
        }

        private void btnRegistrarActor_Click(object sender, EventArgs e)
        {
            RegistrarActor registrarActor = new RegistrarActor();
            registrarActor.Show();
        }

        private void btnRegistrarActor_MouseLeave(object sender, EventArgs e)
        {
            btnRegistrarActor.BackColor = Color.FromArgb(63, 58, 93);
        }

        private void btnRegistrarActor_MouseMove(object sender, MouseEventArgs e)
        {
            btnRegistrarActor.BackColor = Color.FromArgb(63, 64, 150);
        }

        private void btnRegistroActuacion_Click(object sender, EventArgs e)
        {
            RegistrarActuacion registrarActuacion = new RegistrarActuacion();
            registrarActuacion.Show();
        }

        private void btnRegistroActuacion_MouseLeave(object sender, EventArgs e)
        {
            btnRegistroActuacion.BackColor = Color.FromArgb(63, 58, 93);
        }

        private void btnRegistroActuacion_MouseMove(object sender, MouseEventArgs e)
        {
            btnRegistroActuacion.BackColor = Color.FromArgb(63, 64, 150);
        }

        private void btnActualizarCliente_Click(object sender, EventArgs e)
        {
            ActualizarCliente_Admin actualizarCliente = new ActualizarCliente_Admin();  
            actualizarCliente.Show();
        }

        private void btnActualizarCliente_MouseLeave(object sender, EventArgs e)
        {
            btnActualizarCliente.BackColor = Color.FromArgb(63, 58, 93);
        }

        private void btnActualizarCliente_MouseMove(object sender, MouseEventArgs e)
        {
            btnActualizarCliente.BackColor = Color.FromArgb(63, 64, 150);
        }

        private void btnRegistroProducto_Click(object sender, EventArgs e)
        {
            RegistrarProducto registrarProducto = new RegistrarProducto(codigoAdmin);
            registrarProducto.Show();
        }

        private void btnActualizarProducto_Click(object sender, EventArgs e)
        {
            ActualizarProducto actualizarProducto = new ActualizarProducto(codigoAdmin);
            actualizarProducto.Show();
        }

        private void btnDeshabilitarProducto_Click(object sender, EventArgs e)
        {
            DeshabilitarProducto deshabilitarProducto = new DeshabilitarProducto(); 
            deshabilitarProducto.Show();
        }

        private void btnActualizarDatos_Click(object sender, EventArgs e)
        {
            ActualizarAdministrador actualizarAdministrador = new ActualizarAdministrador(codigoAdmin);    
            actualizarAdministrador.Show();
        }

        private void PanelAdministrador_FormClosing(object sender, FormClosingEventArgs e)
        {
            panel.Close();
        }
    }
}
