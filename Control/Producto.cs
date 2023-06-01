using BasesDatosFormulario;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlataformaStreaming.Control
{
    internal class Producto
    {
        public static Conexion conexion = new Conexion();

        public Producto() { }

        public void mostrarDatos(string nombrePelicula, Label lbCategoria,
                    Label lbGenero, Label lbDescripcion, Label lbNumeroVisitas, Label lbElenco)
        {
            OracleConnection conc = conexion.Conectar();
            conc.Open();

            try
            {
                OracleCommand comando = new OracleCommand("DATOS_PRODUCTOS", conc);

                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("P_TITULO", OracleType.VarChar).Value = nombrePelicula;
                comando.Parameters.Add("REG_ACTORES", OracleType.Cursor).Direction = ParameterDirection.Output;
                comando.ExecuteNonQuery();
                OracleCommand comand2 = new OracleCommand("DATOS_PRODUCTOS2", conc);
                comand2.CommandType = CommandType.StoredProcedure;
                comand2.Parameters.Add("P_TITULO", OracleType.VarChar).Value = nombrePelicula;
                comand2.Parameters.Add("REG_DETALLES", OracleType.Cursor).Direction = ParameterDirection.Output;
                comand2.ExecuteNonQuery();
                OracleDataReader reader = comand2.ExecuteReader();
                OracleDataReader reader2 = comando.ExecuteReader();
                //Lee las columnas ingresadas en el cursor y la asgina a los label
                while (reader.Read())
                {
                    lbCategoria.Text = reader["TIPO_PRODUCTO"].ToString();
                    lbGenero.Text = reader["GENERO"].ToString().ToLower();
                    lbDescripcion.Text = reader["DESCRIPCION"].ToString();
                    lbNumeroVisitas.Text = reader["VISITAS"].ToString();

                }
                // se debe cerrar cada reader cuando salga del while
                reader.Close();
                string elenco = "";
                //En el caso de ser de 1 fila , puede usar una variable aux donde se concatene
                //cada fila y al final asginarla todo al label 
                while (reader2.Read())
                {
                    elenco = elenco + reader2["NOMBRECOMPLETO"].ToString() + " , ";
                }
                reader2.Close();
                conc.Close();
                lbElenco.Text = elenco;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
                conc.Close();
            }
        }
        /*
         * Recibe el combobox a llenar como parametro
         * ejecuta la setencia select y rellena el combobox
         */
        public void llenarcbTitulos(System.Windows.Forms.ComboBox cbTitulos)
        {
            OracleConnection con = conexion.Conectar();
            OracleCommand comando = new OracleCommand("SELECT NOMBRE FROM PRODUCTO", con);
            con.Open();
            OracleDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                cbTitulos.Items.Add(registro["NOMBRE"].ToString());

            }
            con.Close();
        }


        public void registrarPoducto(int codAdmin, String portada, String video, String nombre, String descripcion, String fecha, String duracion, String genero,
           String tipo)
        {
            OracleConnection conc = conexion.Conectar();
            conc.Open();
            try
            {
                OracleCommand comando = new OracleCommand("REGISTRAR_PRODUCTOS", conc);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("P_CODIGO_ADMIN", OracleType.Number).Value = codAdmin;
                comando.Parameters.Add("P_PORTADA", OracleType.VarChar).Value = portada;
                comando.Parameters.Add("P_VIDEO", OracleType.VarChar).Value = video;
                comando.Parameters.Add("P_NOMBRE", OracleType.VarChar).Value = nombre;
                comando.Parameters.Add("P_DESCRIPCION", OracleType.VarChar).Value = descripcion;
                comando.Parameters.Add("P_FECHAESTRENO", OracleType.DateTime).Value = fecha;
                comando.Parameters.Add("P_DURACION", OracleType.VarChar).Value = duracion;
                comando.Parameters.Add("P_GENERO", OracleType.VarChar).Value = genero;
                comando.Parameters.Add("P_TIPO_PRODUCTO", OracleType.VarChar).Value = tipo;

                comando.ExecuteNonQuery();
                MessageBox.Show("Inserción Correcta :D");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
            }
            conc.Close();
        }

        //Proyecta los datos de los productos
        public DataGridView proyectarProductos(DataGridView dgvProductos)
        {
            OracleConnection conc = conexion.Conectar();
            conc.Open();
            try
            {


                OracleCommand comando = new OracleCommand("PROYECTAR_PRODUCTOS", conc);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("REG_PRODUCTOS", OracleType.Cursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adaptador = new OracleDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                dgvProductos.DataSource = tabla;

                conc.Close();
                return dgvProductos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
                conc.Close();
                return dgvProductos;
            }
        }

        public void llenarcbCodigos(System.Windows.Forms.ComboBox cbTitulos)
        {
            OracleConnection conc = conexion.Conectar();
            OracleCommand comando = new OracleCommand("SELECT CODIGO FROM PRODUCTO", conc);
            conc.Open();
            OracleDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                cbTitulos.Items.Add(registro["CODIGO"].ToString());

            }
            conc.Close();
        }


        public void ActualizarProducto(int codPelicula, int codAdmin, String portada, String descripcion, String fecha, String duracion, String genero)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            try
            {
                OracleCommand comando = new OracleCommand("ACTUALIZAR_PRODUCTOS", con);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("P_COD_PELICULA", OracleType.Number).Value = codPelicula;
                comando.Parameters.Add("P_CODIGO_ADMIN", OracleType.Number).Value = codAdmin;
                comando.Parameters.Add("P_PORTADA", OracleType.VarChar).Value = portada;
                comando.Parameters.Add("P_DESCRIPCION", OracleType.VarChar).Value = descripcion;
                comando.Parameters.Add("P_FECHAESTRENO", OracleType.DateTime).Value = fecha;
                comando.Parameters.Add("P_DURACION", OracleType.VarChar).Value = duracion;
                comando.Parameters.Add("P_GENERO", OracleType.VarChar).Value = genero;

                comando.ExecuteNonQuery();
                MessageBox.Show("Actualización Correcta :D");
                
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
            }
            con.Close();
        }

        public void deshabilitarProducto(int codProducto)
        {
            try
            {
                OracleConnection con = conexion.Conectar();
                con.Open();

                OracleCommand comando = new OracleCommand("DESHABILITAR_PRODUCTO", con);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("P_CODIGO", OracleType.Number).Value = codProducto;

                comando.ExecuteNonQuery();
                MessageBox.Show("Se ha deshabilitado de manera exitosa :D");
                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
            }
        }

        public void llenarcbCodigosHabilitados(System.Windows.Forms.ComboBox cbTitulos)
        {
            OracleConnection con = conexion.Conectar();
            OracleCommand comando = new OracleCommand("SELECT CODIGO FROM PRODUCTO WHERE ESTADO_PRODUCTO = 1", con);
            con.Open();
            OracleDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                cbTitulos.Items.Add(registro["CODIGO"].ToString());

            }
            con.Close();
        }

    }
}
