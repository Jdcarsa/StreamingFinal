using PlataformaStreaming.Control;
using PlataformaStreaming.Modelo.Entidades;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlataformaStreaming.Vista
{
    public partial class ActualizarProducto : Form
    {
        ProductoControlador pro = new ProductoControlador();
        private int codigoAdmin = new int();
        public ActualizarProducto(int codigoAdmin)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(400, 110);
            dgvProductos = pro.proyectarProductos(dgvProductos);
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "dd/MM/yyyy";

            pro.llenarcbCodigos(cboxCodigo);
            this.codigoAdmin = codigoAdmin;
        }

        private void txtRegistrar_Click(object sender, EventArgs e)
        {
            Producto productoActualizado = new Producto
            {
                CodigoAdmin = codigoAdmin,
                Portada = txtPortada.Text,
                Descripcion = txtDescripcion.Text,
                FechaEstreno = DateTime.Parse(dateTimePicker.Text),
                Duracion = txtDuración.Text,
                Genero = cboxGenero.Text
            };

            ProductoControlador pro = new ProductoControlador();
            if (pro.ActualizarProducto(int.Parse(cboxCodigo.Text), productoActualizado))
            {
                MessageBox.Show("Se ha actualizado correctamente el producto",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Vuelva a intentarlo");
            }
        }


        private void txtCodPelicula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void txtDuración_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (e.KeyChar != (char)Keys.Back)
                && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != ':'))
            {
                e.Handled = true;
            }
        }
    }
}
