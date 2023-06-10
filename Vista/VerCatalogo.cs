using BasesDatosFormulario;
using iTextSharp.text;
using iTextSharp.text.pdf.codec.wmf;
using Org.BouncyCastle.Utilities;
using PlataformaStreaming.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PlataformaStreaming.Vista
{
    public partial class VerCatalogo : Form
    {

        //public List<Peliculas> Producto;
        PanelPrincipal panel;
        string usuario;
        public VerCatalogo(string usuario , PanelPrincipal panel)
        {
            InitializeComponent();
            cargarBotones();
            mostrarCatalogo();
            this.usuario = usuario;
            this.panel = panel;
            //Producto = new List<Peliculas>();

            // Suscribe el evento Paint del FlowLayoutPanel
            //panelCatalogo.Paint += panelCatalogo_Paint;

        }

        Catalogo con = new Catalogo();
        public void escogerCatalogo(List<Pelicula> catalogo)
        {
            foreach (Pelicula producto in catalogo)
            {
                byte[] portada = Convert.FromBase64String(producto.Portada);
                MemoryStream ms = new MemoryStream(portada);
                ms.Seek(0, SeekOrigin.Begin);
                System.Drawing.Image portadaImage = System.Drawing.Image.FromStream(ms);

                FlowLayoutPanel contenedor = new FlowLayoutPanel();
                contenedor.FlowDirection = FlowDirection.TopDown;
                contenedor.WrapContents = false;
                contenedor.AutoSize = true;

                Panel panelPortada = new Panel();

                panelPortada.BackgroundImage = portadaImage;
                panelPortada.BackgroundImageLayout = ImageLayout.Stretch;
                panelPortada.Cursor = Cursors.Hand;

                Label labelDescripcion = new Label();
                //Pasar cursor encima de la portada
                /*panelPortada.MouseMove += (sender, e) =>
                    {
                        //Reemplazar imagen por descripcion
                        panelPortada.BackgroundImage = null; 
                        panelPortada.BackColor = Color.Black; 
                        panelPortada.BackgroundImageLayout = ImageLayout.Stretch;
                        panelPortada.Text = producto.Descripcion;

                        labelDescripcion.Text = producto.Descripcion;
                        labelDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                        labelDescripcion.Dock = DockStyle.Fill;
                        labelDescripcion.ForeColor = Color.White;

                        // Agregar el Label al Panel
                        panelPortada.Controls.Add(labelDescripcion);
                    };*/

                panelPortada.MouseLeave += (leaveSender, leaveEvent) =>
                {
                    panelPortada.Controls.Remove(labelDescripcion);
                    panelPortada.BackgroundImage = portadaImage;
                    panelPortada.BackgroundImageLayout = ImageLayout.Stretch;

                };

                panelPortada.MouseClick += (leaveSender, leaveEvent) =>
                {

                    string video = producto.Video;

                    Reproducir reproduccionForm = new Reproducir(video);
                    reproduccionForm.Show();
                    reproduccionForm.WindowState = FormWindowState.Maximized;
                };

                panelPortada.Size = new Size(200, 282);

                Label labelTitulo = new Label();
                labelTitulo.Text = producto.Nombre;
                //labelTitulo.Text = pelicula.Titulo;
                labelTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                labelTitulo.Dock = DockStyle.Bottom;
                labelTitulo.AutoSize = false;
                labelTitulo.Size = new Size(190, 60);
                labelTitulo.MaximumSize = new Size(190, 60);
                labelTitulo.AutoEllipsis = true;
                labelTitulo.BackColor = Color.Black;
                labelTitulo.ForeColor = Color.White;
                labelTitulo.Font = new System.Drawing.Font("Franklin Gothic Book", 15, FontStyle.Regular);

                contenedor.Controls.Add(panelPortada);
                contenedor.Controls.Add(labelTitulo);

                panelCatalogo.Controls.Add(contenedor);
            }
        }

        public void mostrarCatalogo() //MUESTRA EL CATALOGO PRINCIPAL
        {
            List<Pelicula> catalogo = con.cargarCatalogo();
            escogerCatalogo(catalogo);
        }

        
        public void filtrarCategoria(string tipo, string genero, string anioInicial, string anioFinal) //MUESTRA EL CATALOGO FILTRADO
        {
            panelCatalogo.Controls.Clear();

            List<Pelicula> catalogo = con.filtrarCatalogo(genero, tipo, anioInicial, anioFinal);
            escogerCatalogo(catalogo);
            
        }

        private void btnBuscar_Click(object sender, EventArgs e) //MUESTRA EL CATALOGO ESCRITO CON EL BOTON DE LUPA
        {
            string texto = tbBusqueda.Text;

            if (!string.IsNullOrWhiteSpace(texto))
            {
                panelCatalogo.Controls.Clear();

                List<Pelicula> catalogo = con.buscarCatalogo(tbBusqueda.Text);
                escogerCatalogo(catalogo);
            }
           
        }

        private void tbBusqueda_TextChanged(object sender, EventArgs e) //MUESTRA EL CATALOGO QUE SE ESCRIBE EN LA BARRA EN TIEMPO REAL
        {
            string filtro = tbBusqueda.Text.ToLower();
            panelCatalogo.Controls.Clear();

            List<Pelicula> catalogo = con.buscarCatalogo(filtro);
            escogerCatalogo(catalogo);
        }
        private void btnInicio_Click(object sender, EventArgs e)
        {
            panelCatalogo.Controls.Clear();
            button1.Text = "Todos";
            button2.Text = "Todos";
            button3.Text = "Todos";
            mostrarCatalogo();
        }

        private void tbBusqueda_Click(object sender, EventArgs e)
        {
            if (tbBusqueda.Text == "Buscar...")
            {
                tbBusqueda.Text = "";
            }
        }

        

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ForeColor = Color.White; 
            button1.Image = Properties.Resources.menu;
            
        }

        public void cargarBotones() //CARGAR OPCIONES DE LOS BOTONES
        {
            //BOTON TIPO DE PRODUCTO
            button1.ContextMenuStrip = cmsTipo;
            cmsTipo.ShowCheckMargin = false;
            cmsTipo.BackColor = ColorTranslator.FromHtml("#8B8B95");
            cmsTipo.ForeColor = Color.Black;
            cmsTipo.Font = new System.Drawing.Font("Franklin Gothic Book", 10, FontStyle.Regular);

            cmsTipo.Items.Clear();
            cmsTipo.Items.Add("Todos");
            cmsTipo.Items.Add("Documental");
            cmsTipo.Items.Add("Película");

            //BOTON GENERO O CATEGORIA
            button2.ContextMenuStrip = cmsCategoria;
            cmsCategoria.BackColor = ColorTranslator.FromHtml("#8B8B95");
            cmsCategoria.ForeColor = Color.Black;
            cmsCategoria.Font = new System.Drawing.Font("Franklin Gothic Book", 10, FontStyle.Regular);

            cmsCategoria.Items.Clear();
            cmsCategoria.Items.Add("Todos");
            cmsCategoria.Items.Add("Acción");
            cmsCategoria.Items.Add("Aventura");
            cmsCategoria.Items.Add("Ciencia Ficción");
            cmsCategoria.Items.Add("Comedia");
            cmsCategoria.Items.Add("Cotidiana");
            cmsCategoria.Items.Add("Drama");
            cmsCategoria.Items.Add("Educativa");
            cmsCategoria.Items.Add("Fantasía");
            cmsCategoria.Items.Add("Musical");
            cmsCategoria.Items.Add("Suspenso");
            cmsCategoria.Items.Add("Terror");
            cmsCategoria.Items.Add("Vida Salvaje");

            //BOTON AÑO DE PRODUCCION
            button3.ContextMenuStrip = cmsAnio;
            cmsAnio.BackColor = ColorTranslator.FromHtml("#8B8B95");
            cmsAnio.ForeColor = Color.Black;
            cmsAnio.Font = new System.Drawing.Font("Franklin Gothic Book", 10, FontStyle.Regular);

            cmsAnio.Items.Clear();
            cmsAnio.Items.Add("Todos");
            cmsAnio.Items.Add("2020 - 2023");
            cmsAnio.Items.Add("2010 - 2019");
            cmsAnio.Items.Add("2000 - 2009");
            cmsAnio.Items.Add("1990 - 1999");
            cmsAnio.Items.Add("1980 - 1989");
            cmsAnio.Items.Add("1970 - 1979");
            cmsAnio.Items.Add("1960 - 1969");
            cmsAnio.Items.Add("1950 - 1959");

            //BOTON CUENTA
            button4.ContextMenuStrip = cmsCuenta;
            cmsCuenta.BackColor = ColorTranslator.FromHtml("#8B8B95");
            cmsCuenta.ForeColor = Color.Black;
            cmsCuenta.Font = new System.Drawing.Font("Franklin Gothic Book", 10, FontStyle.Regular);

            cmsCuenta.Items.Clear();
            cmsCuenta.Items.Add("Actualizar Cuenta");
            cmsCuenta.Items.Add("Deshabilitar Cuenta");

            //BOTON ORDENAR POR
            btnOrden.ContextMenuStrip = cmsOrden;
            cmsOrden.BackColor = ColorTranslator.FromHtml("#8B8B95");
            cmsOrden.ForeColor = Color.Black;
            cmsOrden.Font = new System.Drawing.Font("Franklin Gothic Book", 10, FontStyle.Regular);

            cmsOrden.Items.Clear();
            cmsOrden.Items.Add("Sin orden");
            cmsOrden.Items.Add("De A a Z");
            cmsOrden.Items.Add("De Z a A");
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = SystemColors.ButtonShadow; 
            button1.Image = Properties.Resources.menuGris;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.ForeColor = Color.White;
            button2.Image = Properties.Resources.menu;
            
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ForeColor = SystemColors.ButtonShadow;
            button2.Image = Properties.Resources.menuGris;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.ForeColor = Color.White;
            button3.Image = Properties.Resources.menu;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.ForeColor = SystemColors.ButtonShadow;
            button3.Image = Properties.Resources.menuGris;
        }
            
        //EVENTOS PARA MOSTRAR LOS DATOS DE LOS BOTONES

        private void button1_Click(object sender, EventArgs e)
        {
            cmsTipo.Show(button1, new System.Drawing.Point(0, button1.Height));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmsCategoria.Show(button2, new System.Drawing.Point(0, button2.Height));
        }
        private void button3_Click(object sender, EventArgs e)
        {
            cmsAnio.Show(button3, new System.Drawing.Point(0, button3.Height));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cmsCuenta.Show(btnCuenta, new System.Drawing.Point(0, btnCuenta.Height));
        }

        private void cmsCategoria_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem clickedItem = e.ClickedItem;
            string selectedOption = clickedItem.Text;

            button2.Text = selectedOption;
        }

        private void cmsTipo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem clickedItem = e.ClickedItem;
            string selectedOption = clickedItem.Text;

            button1.Text = selectedOption;
        }

        private void cmsAnio_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem clickedItem = e.ClickedItem;
            string selectedOption = clickedItem.Text;

            button3.Text = selectedOption;
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            string textoAnios = button3.Text;
            if (textoAnios == "Todos")
            {
                textoAnios = "1950 - 2023";
            }
            string[] anios = textoAnios.Split('-');

            string anioInicial = anios[0]; 
            string anioFinal = anios[1]; 
            filtrarCategoria(button1.Text, button2.Text, anioInicial, anioFinal);
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            panelCatalogo.VerticalScroll.Value = e.NewValue;
        }

        private void cmsCuenta_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem clickedItem = e.ClickedItem;
            string selectedOption = clickedItem.Text;

            if (selectedOption == "Actualizar Cuenta")
            {
                ActualizarCliente actualizarCliente = new ActualizarCliente(usuario);
                actualizarCliente.Show();

            }
            else if (selectedOption == "Deshabilitar Cuenta")
            {
                DeshabilitarCliente deshabilitarCliente = new DeshabilitarCliente();
                deshabilitarCliente.Show();
            }
            else
            {

            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)//BOTON CERRAR
        {
            this.Close();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)//BOTON MINIMIZAR
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /*private void btnMinimizar_Click(object sender, EventArgs e)//BOTON RESTAURAR Y MAXIMIZAR
        {
            if (WindowState == FormWindowState.Normal)
            {
                // La ventana está en modo normal, cambiar a maximizado
                WindowState = FormWindowState.Maximized;
                btnMaximizar.Image = Properties.Resources.Icono_Restaurar;
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                btnMaximizar.Image = Properties.Resources.Icono_Maximizar;
            }
        }*/

        private void VerCatalogo_FormClosing(object sender, FormClosingEventArgs e)
        {
            panel.Close();
        }

        private void btnOrdenar_Click(object sender, EventArgs e)
        {
            cmsOrden.Show(btnOrden, new System.Drawing.Point(0, btnOrden.Height));
        }

        private void btnOrdenar_MouseEnter(object sender, EventArgs e)
        {
            btnOrden.ForeColor = Color.White;

            if (btnOrden.Text == "Sin orden")
            {
                btnOrden.Image = Properties.Resources.orden;
            }
        }

        private void btnOrdenar_MouseLeave(object sender, EventArgs e)
        {
            btnOrden.ForeColor = SystemColors.ButtonShadow;

            if (btnOrden.Text == "Sin orden")
            {
                btnOrden.Image = Properties.Resources.ordenGris;
            }
        }

        private void cmsOrden_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem clickedItem = e.ClickedItem;
            string selectedOption = clickedItem.Text;

            btnOrden.Text = selectedOption;

            if (btnOrden.Text == "De A a Z")
            {
                btnOrden.Image = Properties.Resources.AZ;
            }
            else if (btnOrden.Text == "De Z a A")
            {
                btnOrden.Image = Properties.Resources.ZA;
            }
            else if (btnOrden.Text == "Sin orden")
            {
                btnOrden.Image = Properties.Resources.ordenGris;
            }
        }
        public List<Pelicula> obtenerCatalogoActual()
        {
            /*foreach ()
            {

            }*/
            return new List<Pelicula>();
        }
        private void btnOrdenar_Click_1(object sender, EventArgs e)
        {
            List<Pelicula> catalogo = con.cargarCatalogo();
            String consulta = catalogo[0].Consulta;
            //catalogo = panelCatalogo.Controls;
            
            if (btnOrden.Text == "De A a Z")
            {
                panelCatalogo.Controls.Clear();
                catalogo = con.ordenarCatalogoAlfabeto(consulta, "");

                escogerCatalogo(catalogo);
            }
            else if (btnOrden.Text == "De Z a A")
            {
                panelCatalogo.Controls.Clear();
                catalogo = con.ordenarCatalogoAlfabeto(consulta, " DESC");

                escogerCatalogo(catalogo);
            }
        }
    }
}
