using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using PlataformaStreaming.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PlataformaStreaming.Vista
{
    public partial class Factura : Form
    {
        PanelPrincipal panel;
        private String usuario;
        public Factura(String usuario, PanelPrincipal panel)
        {
            InitializeComponent();
            this.panel = panel;
            this.usuario = usuario;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Servicio servicio = new Servicio();
            List<String> datos = new List<String>();

            if (servicio.datosUsuario(usuario, ref datos))
            {
                //string elementoComoString = (string)datos[1];



                SaveFileDialog guardar = new SaveFileDialog();
                guardar.FileName = usuario + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";

                string factura = Properties.Resources.Recibo.ToString();
                factura = factura.Replace("@CLIENTE", datos[0] + " " + datos[1] + " " + datos[2] + " " + datos[3]);
                factura = factura.Replace("@FECHA", DateTime.Now.ToString("MMMM dd, yyyy"));
                factura = factura.Replace("@DOCUMENTO", datos[4]);
                factura = factura.Replace("@CORREO", datos[5]);
                factura = factura.Replace("@tipo", "Tarjeta de " + datos[6]);
                factura = factura.Replace("@numeroTarjeta", datos[7]);
                factura = factura.Replace("@plan", datos[8]);
                factura = factura.Replace("@TOTAL", datos[9]);


                if (guardar.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                    {
                        Document document = new Document(PageSize.A4, 25, 25, 25, 25);
                        PdfWriter writer = PdfWriter.GetInstance(document, stream);

                        document.Open();
                        //document.Add(new Paragraph(usuario));

                        using (StringReader sr = new StringReader(factura))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, sr);
                        }

                        document.Close();
                        stream.Close();
                    }
                }

                VerCatalogo catalogo = new VerCatalogo(usuario, panel);
                this.Hide();
                catalogo.ShowDialog();

            };

            //List<String> lista = new ArrayList<>();






        }

        private void Factura_FormClosing(object sender, FormClosingEventArgs e)
        {
            panel.Show();
        }
    }
}
