using BasesDatosFormulario;
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
using WMPLib;


namespace PlataformaStreaming.Vista
{
    public partial class Reproducir : Form
    {
        private string rutaVideo;

        public Reproducir(string video)
        {
            InitializeComponent();
            rutaVideo = video;
            Reproduccion();
        }

        private void Reproduccion()
        {
            byte[] videoBytes = Convert.FromBase64String(rutaVideo);

            // Crear un archivo temporal para el video
            string tempVideoPath = Path.GetTempFileName();

            File.WriteAllBytes(tempVideoPath, videoBytes);

            reproductorWMP.URL = tempVideoPath;

            reproductorWMP.PlayStateChange += reproduccionAutomatica;
        }

        private void reproduccionAutomatica(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == (int)WMPLib.WMPPlayState.wmppsReady)
            {
                // Iniciar la reproducción
                reproductorWMP.Ctlcontrols.play();
            }
        }

    }
}
