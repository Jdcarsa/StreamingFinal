using System;

namespace PlataformaStreaming.Modelo.Entidades
{
    public class ClientePlan
    {
        public int Codigo { get; set; }

        public int CodigoCliente { get; set; }

        public int CodigoPlan { get; set; }
        public DateTime FechaCompra { get; set; } = DateTime.Now;

        public DateTime FechaVencimiento { get; set; }

    }
}