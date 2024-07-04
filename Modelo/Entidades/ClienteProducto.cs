using System;

namespace PlataformaStreaming.Modelo.Entidades
{
    public class ClienteProducto
    {

        public int CodigoCliente { get; private set; }

        public int CodigoProducto { get; private set; }
        public int CodigoReproduccion { get; private set; }
        public DateTime FechaReproduccion { get; private set; } = DateTime.Now;

    }
}