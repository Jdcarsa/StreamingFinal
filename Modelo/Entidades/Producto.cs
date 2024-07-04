using System;
using System.Collections.Generic;

namespace PlataformaStreaming.Modelo.Entidades
{
    public class Producto
    {
        public int Codigo { get; set; }

        public int CodigoAdmin { get; set; }
        public string Portada { get; set; } 
        public string Video { get; set; } 

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaEstreno { get; set; }

        public string Duracion { get; set; }

        public string Genero { get; set; }

        public string TipoProducto { get; set; }
        public int EstadoProducto { get; set; } = 1;


    }
}
