using PlataformaStreaming.Modelo;
using PlataformaStreaming.Modelo.Entidades;
using PlataformaStreaming.Vista;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PlataformaStreaming
{
    public partial class IngresarTarjeta : Form
    {
        PanelPrincipal panel;
        Registro registro;
        private String usuario;
        private int codPlan;
        Servicio conexion = new Servicio();

        public IngresarTarjeta(PanelPrincipal panel, Registro registro, string usuario, System.Drawing.Image plan, int codPlan)
        {
            InitializeComponent();
            panelPlan.BackgroundImage = plan;
            panelPlan.BackgroundImageLayout = ImageLayout.Stretch;

            this.panel = panel;
            this.registro = registro;
            lbUser.Text = usuario;
            this.StartPosition = FormStartPosition.CenterScreen;
            cbTipo.DropDownStyle = ComboBoxStyle.DropDownList; //EVITAR EDICION EL COMBO BOX
            cbTipo.Items.Add("Crédito");
            cbTipo.Items.Add("Débito");
            this.usuario = usuario;
            this.codPlan = codPlan;
            cargarDatos();
            //lbNombre.Text = usuario;
        }

        public void cargarDatos()
        {
            List<String> datos = new List<String>();
            Label nombrePlan = new Label();
            Label percioPlan = new Label();

            if (conexion.cargarDatosPlan(usuario, ref datos))
            {
                nombrePlan.Text = datos[1];
                nombrePlan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                nombrePlan.Dock = DockStyle.Top;
                nombrePlan.AutoSize = false;
                nombrePlan.Size = new Size(220, 60);
                nombrePlan.MaximumSize = new Size(220, 60);
                nombrePlan.AutoEllipsis = true;
                nombrePlan.BackColor = Color.Transparent;
                nombrePlan.ForeColor = Color.White;
                nombrePlan.Font = new System.Drawing.Font("Franklin Gothic Book", 15, FontStyle.Regular);

                percioPlan.Text = datos[0];
                percioPlan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                percioPlan.Dock = DockStyle.Bottom;
                percioPlan.AutoSize = false;
                percioPlan.Size = new Size(220, 60);
                percioPlan.MaximumSize = new Size(220, 60);
                percioPlan.AutoEllipsis = true;
                percioPlan.BackColor = Color.Transparent;
                percioPlan.ForeColor = Color.White;
                percioPlan.Font = new System.Drawing.Font("Franklin Gothic Book", 15, FontStyle.Regular);
            }

            //panelContenedor.Controls.Add(nombrePlan);
        }
        private void IngresarTarjeta_Load(object sender, EventArgs e)
        {
            btnPagar.Enabled = false;

        }
        //VALIDACION DE LOS CAMPOS VACIOS
        private void habilitarPagar()
        {
            var validar = !String.IsNullOrEmpty(tbNumero.Text) && !String.IsNullOrEmpty(tbNombre.Text) &&
                !String.IsNullOrEmpty(tbMes.Text) && !String.IsNullOrEmpty(tbCCV.Text) && !String.IsNullOrEmpty(tbAnio.Text)
                && !String.IsNullOrEmpty(cbTipo.Text);

            btnPagar.Enabled = validar;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Servicio servicio = new Servicio();

            Tarjeta nuevaTarjeta = new Tarjeta
            {
                NombreUsuarioCliente = usuario,
                NumeroTarjeta = tbNumero.Text,
                FechaExp = DateTime.Parse("05/05/2023"), // Asegúrate de ajustar esta fecha según corresponda
                NombreTarjeta = tbNombre.Text,
                CVV = tbCCV.Text,
                TipoTarjeta = cbTipo.Text
            };

            try
            {
                servicio.transaccion(nuevaTarjeta, codPlan);
                this.Hide();
                Factura factura = new Factura(usuario, panel);
                factura.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vuelva a intentarlo");
            }
        }


        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            habilitarPagar();
        }

        private void tbNumero_TextChanged(object sender, EventArgs e)
        {
            habilitarPagar();
            string numeroTarjeta = tbNumero.Text.Replace(" ", "");

            if (tbNumero.Text.Length >= 20)
            {
                tbNumero.Text = tbNumero.Text.Substring(0, 20);
                tbNumero.SelectionStart = tbNumero.Text.Length;
            }
            // Agregar un espacio después de cada cuarto número ingresado
            if (numeroTarjeta.Length > 0 && (numeroTarjeta.Length % 4) == 0)
            {
                int ultimoEspacio = tbNumero.Text.LastIndexOf(" ");
                if (ultimoEspacio != tbNumero.Text.Length - 1)
                {
                    tbNumero.Text += " ";
                    tbNumero.SelectionStart = tbNumero.Text.Length;
                }
            }
        }

        private void tbMes_TextChanged(object sender, EventArgs e)
        {
            habilitarPagar();
        }

        private void tbAnio_TextChanged(object sender, EventArgs e)
        {
            habilitarPagar();
        }

        private void tbCCV_TextChanged(object sender, EventArgs e)
        {
            habilitarPagar();
        }

        private void tbNombre_TextChanged(object sender, EventArgs e)
        {
            habilitarPagar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            PlanesSuscripcion planes = new PlanesSuscripcion(panel, registro, usuario);
            planes.Show();
        }

        private void tbNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 31 && e.KeyChar < 48) || e.KeyChar > 57)
            {
                MessageBox.Show("Ingrese solo números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void tbNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 33 && e.KeyChar < 64) || e.KeyChar > 237)
            {
                MessageBox.Show("Ingrese solo letras", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void tbCCV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 31 && e.KeyChar < 48) || e.KeyChar > 57)
            {
                e.Handled = true;
                return;
            }
        }



        public void setUsuario(String usuario)
        {
            this.usuario = usuario;
        }

        private void panelPrin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbNombre_Click(object sender, EventArgs e)
        {

        }

        private void tbMes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                int number;
                if (int.TryParse(tbMes.Text + e.KeyChar.ToString(), out number))
                {
                    if (number < 1 || number > 12)
                    {
                        e.Handled = true;
                    }
                }
                else if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void tbMes_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(tbMes.Text, out int number) && number >= 1 && number <= 9)
            {
                tbMes.Text = number.ToString("D2");
            }
        }

        private void tbAnio_Leave(object sender, EventArgs e)
        {
            if (tbAnio.Text.Length < 2)
            {
                tbAnio.Text = tbAnio.Text + "0";
            }
        }

        private void datos()
        {
            Label labelUsuario = new Label();
            labelUsuario.Text = usuario;
            //labelTitulo.Text = pelicula.Titulo;
            lbUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lbUser.Dock = DockStyle.Top;
            lbUser.AutoSize = false;
            lbUser.Size = new Size(190, 60);
            lbUser.MaximumSize = new Size(190, 60);
            lbUser.AutoEllipsis = true;
            lbUser.BackColor = Color.Black;
            lbUser.ForeColor = Color.White;
            lbUser.Font = new System.Drawing.Font("Franklin Gothic Book", 15, FontStyle.Regular);
            //panelUsuario.Controls.Add(labelUsuario);

        }
    }
}
