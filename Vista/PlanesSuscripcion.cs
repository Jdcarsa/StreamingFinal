using PlataformaStreaming.Modelo;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlataformaStreaming.Vista
{
    public partial class PlanesSuscripcion : Form
    {
        PanelPrincipal panel;
        Registro registro;
        private Color originalColor;
        private Boolean semanalActive = false;
        private Boolean mensualActive = false;
        private Boolean anualActive = false;

        private string usuarioAsignar;

        public PlanesSuscripcion(PanelPrincipal panel, Registro registro, string usuarioAsignar)
        {
            /*Para registrar el id junto a la suscripcion 
             * debo pasar el id del cliente como parametro
             * al constructor de esta clase , desde la interfaz anterior
             * , desde registro */
            InitializeComponent();
            this.panel = panel;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.registro = registro;
            this.usuarioAsignar = usuarioAsignar;
        }

        private void PlanesSuscripcion_FormClosing(object sender, FormClosingEventArgs e)
        {

            panel.Show();
            registro.Close();
        }
        //Controla para que solo cambie un plan de color
        private void mensual_Click(object sender, EventArgs e)
        {
            seleccion(2);
        }

        private void PlanesSuscripcion_Load(object sender, EventArgs e)
        {
            //Se alamacena al color original del panel en una variable
            originalColor = mensual.BackColor;
            btnSiguiente.Enabled = false;
            Servicio servicio = new Servicio();
            //incializar los precios de los planes
            //precioSemanal.Text = "$ " + servicio.preciosPlanes(1)+" COP";
            //precioMensual.Text = "$ " + servicio.preciosPlanes(2)+ " COP";
            //precioAual.Text = "$ " + servicio.preciosPlanes(3) + " COP";

        }

        /*
         * La funcion cambia el color del panel ingresado 
         * como parametro , al hacer click por primera vez
         * cambiar y al segundo click vuelve al color original
         */

        private void cambiarColor(Panel panelContenedor)
        {
            if (panelContenedor.BackColor.Equals(originalColor))
            {
                panelContenedor.BackColor = Color.FromArgb(100, 100, 100);
            }
            else
            {
                panelContenedor.BackColor = originalColor;
            }
        }
        //Controla para que solo cambie un plan de color
        private void anual_Click(object sender, EventArgs e)
        {
            seleccion(3);
        }
        //Controla para que solo cambie un plan de color
        private void semanal_Click(object sender, EventArgs e)
        {
            seleccion(1);
        }


        /*Controla cuando se debe activar el control para seguir 
         * a la siguiente ventana 
         */
        private void activaBtn(Boolean activar)
        {
            if (activar)
            {
                btnSiguiente.Enabled = true;
            }
            else
            {
                btnSiguiente.Enabled = false;
            }
        }



        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            Servicio servicio = new Servicio();


            if (semanalActive)
            {
                //servicio.asignarPlan(usuarioAsignar,1);
                IngresarTarjeta tarjeta = new IngresarTarjeta(panel, registro, usuarioAsignar, semanal.BackgroundImage, 1);
                tarjeta.Show();
            }
            else if (mensualActive)
            {
                IngresarTarjeta tarjeta = new IngresarTarjeta(panel, registro, usuarioAsignar, mensual.BackgroundImage, 2);
                tarjeta.Show();
            }
            else if (anualActive)
            {

                IngresarTarjeta tarjeta = new IngresarTarjeta(panel, registro, usuarioAsignar, anual.BackgroundImage, 3);
                tarjeta.Show();
            }

            this.Hide();
        }



        //Controla que no se active los tres paneles al mismo tiempo
        private void seleccion(int n)
        {
            switch (n)
            {
                case 1:
                    if (!mensualActive && !anualActive)
                    {
                        cambiarColor(semanal);
                        semanalActive = !semanalActive;
                        activaBtn(semanalActive);
                    }
                    //Si da errores es borrar de aqui para abajo
                    if (mensualActive && !semanalActive)
                    {
                        cambiarColor(semanal);
                        cambiarColor(mensual);
                        semanalActive = !semanalActive;
                        mensualActive = !mensualActive;
                        activaBtn(semanalActive);
                    }

                    if (anualActive && !semanalActive)
                    {
                        cambiarColor(semanal);
                        cambiarColor(anual);
                        semanalActive = !semanalActive;
                        anualActive = !anualActive;
                        activaBtn(semanalActive);
                    }

                    //Hasta aqui

                    break;

                case 2:
                    if (!semanalActive && !anualActive)
                    {
                        cambiarColor(mensual);
                        mensualActive = !mensualActive;
                        activaBtn(mensualActive);
                    }
                    //Si da errrors borrar 

                    if (!mensualActive && semanalActive)
                    {
                        cambiarColor(semanal);
                        cambiarColor(mensual);
                        semanalActive = !semanalActive;
                        mensualActive = !mensualActive;
                        activaBtn(mensualActive);
                    }

                    if (!mensualActive && anualActive)
                    {
                        cambiarColor(anual);
                        cambiarColor(mensual);
                        anualActive = !anualActive;
                        mensualActive = !mensualActive;
                        activaBtn(mensualActive);
                    }
                    break;

                case 3:
                    if (!mensualActive && !semanalActive)
                    {
                        cambiarColor(anual);
                        anualActive = !anualActive;
                        activaBtn(anualActive);
                    }
                    /*
                    if (anualActive)
                    {
                        semanal.Cursor = Cursors.Default;
                        mensual.Cursor = Cursors.Default;
                    }
                    else if (!anualActive)
                    {
                        semanal.Cursor = Cursors.Hand;
                        mensual.Cursor = Cursors.Hand;
                    }*/

                    //Si da error borrar
                    if (!anualActive && semanalActive)
                    {
                        cambiarColor(semanal);
                        cambiarColor(anual);
                        semanalActive = !semanalActive;
                        anualActive = !anualActive;
                        activaBtn(anualActive);
                    }

                    if (!anualActive && mensualActive)
                    {
                        cambiarColor(mensual);
                        cambiarColor(anual);
                        mensualActive = !mensualActive;
                        anualActive = !anualActive;
                        activaBtn(anualActive);
                    }
                    break;
            }
        }

        public String getUsuario()
        {
            return usuarioAsignar;
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
