

using System;
using System.Collections.Generic;

namespace PlataformaStreaming.Modelo.Entidades
{
    public class Cliente
    {
        public int Codigo { get; set; }

        public string NombreUsuarioCliente { get; set; }

        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Contrasenia { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public int TipoAcceso { get; set; } = 2;
    }
}
