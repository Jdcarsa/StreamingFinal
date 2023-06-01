using BasesDatosFormulario;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OracleClient;
using PlataformaStreaming.Control;

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
            Producto pro = new Producto();

            pro.registrarPoducto(idAdmin, txtPortada.Text, txtVideo.Text, txtNombre.Text, txtDescripcion.Text, dateTimePicker.Text, txtDuración.Text, cboxGenero.Text, cboxTipo.Text);
            
        }

        
    }
}
