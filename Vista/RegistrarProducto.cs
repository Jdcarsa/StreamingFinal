using PlataformaStreaming.Control;
using PlataformaStreaming.Modelo.Entidades;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlataformaStreaming.Vista
{
    public partial class RegistrarProducto : Form
    {
        //Catalogo catalogo = new Catalogo();
        public int idAdmin;
        public RegistrarProducto(int idAdmin)
        {
            this.idAdmin = idAdmin;
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(550, 50);
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "dd/MM/yyyy";
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Crear el objeto Producto
            Producto nuevoProducto = new Producto
            {
                CodigoAdmin = idAdmin,
                Portada = txtPortada.Text,
                Video = txtVideo.Text,
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                FechaEstreno = DateTime.Parse(dateTimePicker.Text),
                Duracion = txtDuración.Text,
                Genero = cboxGenero.Text,
                TipoProducto = cboxTipo.Text
            };

            ProductoControlador pro = new ProductoControlador();
            if (pro.registrarProducto(nuevoProducto))
            {
                MessageBox.Show("Se ha registrado correctamente el producto",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Vuelva a intentarlo");
            }
        }



    }
}
