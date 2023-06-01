using BasesDatosFormulario;
using PlataformaStreaming.Control;
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
    public partial class ActualizarProducto : Form
    {
        Producto pro = new Producto();
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
            pro.ActualizarProducto(int.Parse(cboxCodigo.Text), codigoAdmin, txtPortada.Text, txtDescripcion.Text, dateTimePicker.Text, txtDuración.Text, cboxGenero.Text);
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
