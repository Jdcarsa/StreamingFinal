﻿using System;
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
            PanelPrincipal panel = new PanelPrincipal();
            Application.Run(panel);
        }
    }
}
