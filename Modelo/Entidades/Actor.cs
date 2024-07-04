

using System;
using System.Collections.Generic;

namespace PlataformaStreaming.Modelo.Entidades
{
    public class Actor
    {
        public int Codigo { get; set; }

        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public DateTime FechaNacimiento { get; set; }
    }
}
