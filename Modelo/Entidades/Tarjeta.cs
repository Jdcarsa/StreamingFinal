using System;

namespace PlataformaStreaming.Modelo.Entidades
{
    public class Tarjeta
    {

        public int ClienteId { get; set; }

        public string NombreUsuarioCliente { get; set; }
        public int Codigo { get; set; }

        public string NumeroTarjeta { get; set; }

        public DateTime FechaExp { get; set; }

        public string NombreTarjeta { get; set; }

        public string CVV { get; set; }

        public string TipoTarjeta { get; set; }



    }
}
