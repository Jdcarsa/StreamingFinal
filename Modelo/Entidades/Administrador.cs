using System;
using System.Collections.Generic;

namespace PlataformaStreaming.Modelo.Entidades
{
    public class Administrador
    {
        public int Codigo { get; set; }

        public double AdmId { get; set; }

        public string NombreUsuarioAdmin { get; set; }

        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Contrasenia { get; set; }
        public DateTime FechaContrato { get; set; }

        public string Telefono { get; set; }

        public string Correo { get; set; }
        public int TipoAcceso { get; set; } = 1;
    }
}
