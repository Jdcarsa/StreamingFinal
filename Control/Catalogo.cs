using BasesDatosFormulario;
using PlataformaStreaming.Vista;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlataformaStreaming.Control
{
    internal class Catalogo
    {
        public static Conexion conexion = new Conexion();

        public Catalogo() { }

        //CATALOGO
        public List<Pelicula> cargarCatalogo() //Cargar catalogo
        {
            OracleConnection conc = conexion.Conectar();
            conc.Open();
            List<Pelicula> catalogo = new List<Pelicula>();
            try
            {
                string consulta = "SELECT PORTADA, VIDEO, NOMBRE, DESCRIPCION, FECHAESTRENO, DURACION, GENERO, TIPO_PRODUCTO FROM PRODUCTO WHERE ESTADO_PRODUCTO = 1";

                
                Console.WriteLine(conc.State.ToString());

                OracleCommand comando = new OracleCommand(consulta, conc);
                comando.CommandType = CommandType.Text;

                OracleDataReader dr = comando.ExecuteReader();



                while (dr.Read())
                {
                    Pelicula pelicula = new Pelicula();
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
                string consulta = "SELECT PORTADA, VIDEO, NOMBRE, DESCRIPCION, FECHAESTRENO, DURACION, GENERO, TIPO_PRODUCTO FROM PRODUCTO WHERE UPPER(NOMBRE) LIKE '%'|| UPPER("+busqueda+") ||'%' AND ESTADO_PRODUCTO = 1";

                Console.WriteLine(conc.State.ToString());

                OracleCommand comando = new OracleCommand(consulta, conc);
                comando.CommandType = CommandType.Text;
                //comando.Parameters.Add(":busqueda", busqueda);

                OracleDataReader dr = comando.ExecuteReader();

                while (dr.Read())
                {
                    Pelicula pelicula = new Pelicula();
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
                string consulta = "SELECT PORTADA, VIDEO, NOMBRE, DESCRIPCION, FECHAESTRENO, DURACION, GENERO, TIPO_PRODUCTO FROM PRODUCTO WHERE UPPER(GENERO) LIKE '%'|| UPPER(:genero) ||'%' AND UPPER(TIPO_PRODUCTO) LIKE '%'|| UPPER(:tipo) ||'%' AND FECHAESTRENO BETWEEN :fechaInicial AND :fechaFinal  AND ESTADO_PRODUCTO = 1";

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
