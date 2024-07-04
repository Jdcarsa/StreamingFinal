using PlataformaStreaming.Control;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlataformaStreaming.Vista
{
    public partial class MostrarDatos : Form
    {
        ProductoControlador producto = new ProductoControlador();
        public MostrarDatos()
        {
            InitializeComponent();
            //Posiciona la ventana en el "centro " de la ventana padre
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(400, 160);
        }

        private void MostrarDatos_Load(object sender, EventArgs e)
        {
            //Ingresa el primer valor del comobox y es puesto por defecto
            cbTitulos.Items.Insert(0, "Seleccionar");
            cbTitulos.SelectedIndex = 0;
            //Rellena el combobox
            producto.llenarcbTitulos(cbTitulos);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {


            if (cbTitulos.SelectedIndex == 0)
            {
                MessageBox.Show("Opcion no valida", "Alerta",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string titulo = cbTitulos.GetItemText(cbTitulos.SelectedItem);
                producto.mostrarDatos(titulo, lbCategoria, lbGenero
                     , lbDescripcion, lbNumeroVisitas, lbElenco);

            }
        }
    }
}
