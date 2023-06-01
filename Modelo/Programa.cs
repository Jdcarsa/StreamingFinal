using PlataformaStreaming.Vista;
using System;
using System.Windows.Forms;

namespace PlataformaStreaming
{
    internal static class Programa
    {
        [STAThread]
        
        static void Main()
        { 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PanelPrincipal());
            //Application.Run(new VerCatalogo("JULI"));
        }
    }
}
