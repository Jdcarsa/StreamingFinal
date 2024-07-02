using BasesDatosFormulario;
using PlataformaStreaming.Vista;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Windows.Forms;

namespace PlataformaStreaming.Control
{
    internal class CatalogoProductoControlador
    {
        public static Conexion conexion = new Conexion();

        public CatalogoProductoControlador() { }

        //CATALOGO
        public List<Pelicula> cargarCatalogo() //Cargar catalogo
        {
            OracleConnection conc = conexion.Conectar();
            conc.Open();
            List<Pelicula> catalogo = new List<Pelicula>();
            try
            {
                string consulta = "SELECT CODIGO, PORTADA, VIDEO, NOMBRE, DESCRIPCION, FECHAESTRENO, DURACION, GENERO, TIPO_PRODUCTO FROM PRODUCTO WHERE ESTADO_PRODUCTO = 1";

                Console.WriteLine(conc.State.ToString());

                OracleCommand comando = new OracleCommand(consulta, conc);
                comando.CommandType = CommandType.Text;

                OracleDataReader dr = comando.ExecuteReader();

                while (dr.Read())
                {
                    Pelicula pelicula = new Pelicula();
                    pelicula.Codigo = dr["CODIGO"].ToString();
                    byte[] portadaBytes = (byte[])dr["PORTADA"];
                    string portadaBase64 = Convert.ToBase64String(portadaBytes);
                    pelicula.Portada = portadaBase64;
                    byte[] videoBytes = (byte[])dr["VIDEO"];
                    string videoBase64 = Convert.ToBase64String(videoBytes);
                    pelicula.Video = videoBase64;
                    pelicula.Nombre = dr["NOMBRE"].ToString();
                    pelicula.Descripcion = dr["DESCRIPCION"].ToString();
                    pelicula.FechaEstreno = dr["FECHAESTRENO"].ToString();
                    pelicula.Duracion = dr["DURACION"].ToString();
                    pelicula.Genero = dr["GENERO"].ToString();
                    pelicula.Tipo = dr["TIPO_PRODUCTO"].ToString();
                    pelicula.Consulta = consulta;

                    catalogo.Add(pelicula);
                }
                dr.Close();
                conc.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error " + ex.Message);
                conc.Close();
            }
            return catalogo;
        }

        //Busca por catalogo
        public List<Pelicula> buscarCatalogo(string busqueda)
        {
            OracleConnection conc = conexion.Conectar();
            conc.Open();
            List<Pelicula> catalogo = new List<Pelicula>();
            try
            {
                string consulta = "SELECT CODIGO, PORTADA, VIDEO, NOMBRE, DESCRIPCION, FECHAESTRENO, DURACION, GENERO, TIPO_PRODUCTO FROM PRODUCTO WHERE UPPER(NOMBRE) LIKE '%'|| UPPER(:busqueda) ||'%' AND ESTADO_PRODUCTO = 1";

                Console.WriteLine(conc.State.ToString());

                OracleCommand comando = new OracleCommand(consulta, conc);
                comando.CommandType = CommandType.Text;
                comando.Parameters.Add(":busqueda", busqueda);

                OracleDataReader dr = comando.ExecuteReader();

                while (dr.Read())
                {
                    Pelicula pelicula = new Pelicula();
                    pelicula.Codigo = dr["CODIGO"].ToString();
                    byte[] portadaBytes = (byte[])dr["PORTADA"];
                    string portadaBase64 = Convert.ToBase64String(portadaBytes);
                    pelicula.Portada = portadaBase64;
                    byte[] videoBytes = (byte[])dr["VIDEO"];
                    string videoBase64 = Convert.ToBase64String(videoBytes);
                    pelicula.Video = videoBase64;
                    pelicula.Nombre = dr["NOMBRE"].ToString();
                    pelicula.Descripcion = dr["DESCRIPCION"].ToString();
                    pelicula.FechaEstreno = dr["FECHAESTRENO"].ToString();
                    pelicula.Duracion = dr["DURACION"].ToString();
                    pelicula.Genero = dr["GENERO"].ToString();
                    pelicula.Tipo = dr["TIPO_PRODUCTO"].ToString();
                    pelicula.Consulta = consulta;

                    catalogo.Add(pelicula);


                }
                dr.Close();
                conc.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error " + ex.Message);
                conc.Close();
            }

            return catalogo;
        }

        public List<Pelicula> filtrarCatalogo(string genero, string tipo, string fechaInicial, string fechaFinal)
        {
            OracleConnection conc = conexion.Conectar();
            conc.Open();
            if (genero == "Todos")
            {
                genero = "";
            }
            if (tipo == "Todos")
            {
                tipo = "";
            }

            fechaInicial = "01/01/" + fechaInicial;
            fechaFinal = "31/12/" + fechaFinal;


            List<Pelicula> catalogo = new List<Pelicula>();
            try
            {
                string consulta = "SELECT CODIGO, PORTADA, VIDEO, NOMBRE, DESCRIPCION, FECHAESTRENO, DURACION, GENERO, TIPO_PRODUCTO FROM PRODUCTO WHERE UPPER(GENERO) LIKE '%'|| UPPER(:genero) ||'%' AND UPPER(TIPO_PRODUCTO) LIKE '%'|| UPPER(:tipo) ||'%' AND FECHAESTRENO BETWEEN :fechaInicial AND :fechaFinal  AND ESTADO_PRODUCTO = 1";

                Console.WriteLine(conc.State.ToString());

                OracleCommand comando = new OracleCommand(consulta, conc);
                comando.CommandType = CommandType.Text;
                comando.Parameters.Add(":genero", genero);
                comando.Parameters.Add(":tipo", tipo);
                comando.Parameters.Add(":fechaInicial", fechaInicial);
                comando.Parameters.Add(":fechaFinal", fechaFinal);
                OracleDataReader dr = comando.ExecuteReader();

                while (dr.Read())
                {
                    Pelicula pelicula = new Pelicula();
                    pelicula.Codigo = dr["CODIGO"].ToString();
                    byte[] portadaBytes = (byte[])dr["PORTADA"];
                    string portadaBase64 = Convert.ToBase64String(portadaBytes);
                    pelicula.Portada = portadaBase64;
                    byte[] videoBytes = (byte[])dr["VIDEO"];
                    string videoBase64 = Convert.ToBase64String(videoBytes);
                    pelicula.Video = videoBase64;
                    pelicula.Nombre = dr["NOMBRE"].ToString();
                    pelicula.Descripcion = dr["DESCRIPCION"].ToString();
                    pelicula.FechaEstreno = dr["FECHAESTRENO"].ToString();
                    pelicula.Duracion = dr["DURACION"].ToString();
                    pelicula.Genero = dr["GENERO"].ToString();
                    pelicula.Tipo = dr["TIPO_PRODUCTO"].ToString();
                    pelicula.Consulta = consulta;

                    catalogo.Add(pelicula);
                }
                dr.Close();
                conc.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error " + ex.Message);
                conc.Close();
            }

            return catalogo;
        }

        public List<Pelicula> ordenarCatalogoAlfabeto(string consulta, string parametro)
        {
            OracleConnection conc = conexion.Conectar();
            conc.Open();

            List<Pelicula> catalogo = new List<Pelicula>();

            consulta = consulta + " ORDER BY NOMBRE" + parametro;

            Console.WriteLine(conc.State.ToString());

            OracleCommand comando = new OracleCommand(consulta, conc);
            comando.CommandType = CommandType.Text;
            OracleDataReader dr = comando.ExecuteReader();

            try
            {

                while (dr.Read())
                {
                    Pelicula pelicula = new Pelicula();
                    pelicula.Codigo = dr["CODIGO"].ToString();
                    byte[] portadaBytes = (byte[])dr["PORTADA"];
                    string portadaBase64 = Convert.ToBase64String(portadaBytes);
                    pelicula.Portada = portadaBase64;
                    byte[] videoBytes = (byte[])dr["VIDEO"];
                    string videoBase64 = Convert.ToBase64String(videoBytes);
                    pelicula.Video = videoBase64;
                    pelicula.Nombre = dr["NOMBRE"].ToString();
                    pelicula.Descripcion = dr["DESCRIPCION"].ToString();
                    pelicula.FechaEstreno = dr["FECHAESTRENO"].ToString();
                    pelicula.Duracion = dr["DURACION"].ToString();
                    pelicula.Genero = dr["GENERO"].ToString();
                    pelicula.Tipo = dr["TIPO_PRODUCTO"].ToString();
                    pelicula.Consulta = consulta;

                    catalogo.Add(pelicula);
                }
                dr.Close();
                conc.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error " + ex.Message);
                conc.Close();
            }

            return catalogo;
        }


        public List<Pelicula> mostrarHistorial(string usuario)
        {
            OracleConnection conc = conexion.Conectar();
            conc.Open();
            
            List<Pelicula> catalogo = new List<Pelicula>();

           
            string consulta = "SELECT PR.CODIGO, PORTADA, VIDEO, NOMBRE, DESCRIPCION, FECHAESTRENO, DURACION, GENERO, TIPO_PRODUCTO, FECHA_REPRODUCCION " +
                "FROM PRODUCTO PR INNER JOIN CLIENTE_PRODUCTO CP ON PR.CODIGO = CP.CODIGO_PRODUCTO INNER JOIN CLIENTE CL ON CL.CODIGO = CP.CODIGO_CLIENTE " +
                "INNER JOIN(SELECT CP.CODIGO_PRODUCTO, MAX(FECHA_REPRODUCCION) AS ULTIMA_REPRODUCCION FROM CLIENTE_PRODUCTO CP " +
                "INNER JOIN CLIENTE CL ON CL.CODIGO = CP.CODIGO_CLIENTE WHERE CL.NOMBRE_USUARIO_CLIENTE = :usuario  GROUP BY CP.CODIGO_PRODUCTO) " +
                "ULTIMA_REPRODUCCION_PR ON PR.CODIGO = ULTIMA_REPRODUCCION_PR.CODIGO_PRODUCTO WHERE ESTADO_PRODUCTO = 1 " +
                "AND NOMBRE_USUARIO_CLIENTE = :usuario AND FECHA_REPRODUCCION = ULTIMA_REPRODUCCION_PR.ULTIMA_REPRODUCCION ORDER BY FECHA_REPRODUCCION DESC";


            OracleCommand comando = new OracleCommand(consulta, conc);

            comando.Parameters.Add(":usuario", usuario);
            comando.CommandType = CommandType.Text;
            OracleDataReader dr = comando.ExecuteReader();

            try
            {

                while (dr.Read())
                {
                    Pelicula pelicula = new Pelicula();
                    pelicula.Codigo = dr["CODIGO"].ToString();
                    byte[] portadaBytes = (byte[])dr["PORTADA"];
                    string portadaBase64 = Convert.ToBase64String(portadaBytes);
                    pelicula.Portada = portadaBase64;
                    byte[] videoBytes = (byte[])dr["VIDEO"];
                    string videoBase64 = Convert.ToBase64String(videoBytes);
                    pelicula.Video = videoBase64;
                    pelicula.Nombre = dr["NOMBRE"].ToString();
                    pelicula.Descripcion = dr["DESCRIPCION"].ToString();
                    pelicula.FechaEstreno = dr["FECHAESTRENO"].ToString();
                    pelicula.Duracion = dr["DURACION"].ToString();
                    pelicula.Genero = dr["GENERO"].ToString();
                    pelicula.Tipo = dr["TIPO_PRODUCTO"].ToString();
                    pelicula.Consulta = consulta;

                    catalogo.Add(pelicula);
                }
                dr.Close();
                conc.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error " + ex.Message);
                conc.Close();
            }

            return catalogo;
        }

    }
}
