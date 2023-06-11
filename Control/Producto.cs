using BasesDatosFormulario;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Siticone.Desktop.UI.WinForms.Suite;

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
            DataSet dataSet = conexion.ejecutarDML(conexion.Conectar(), "SELECT NOMBRE FROM PRODUCTO");
            cbTitulos.DataSource = dataSet.Tables[0];
            cbTitulos.DisplayMember = "NOMBRE";
        }


        public void registrarPoducto(int codAdmin, String portada, String video, String nombre, String descripcion, String fecha, String duracion, String genero,
           String tipo)
        {
            try
            {
                OracleParameter[] parameters =
                    new OracleParameter[] {
                    new OracleParameter("P_CODIGO_ADMIN",codAdmin),
                    new OracleParameter("P_PORTADA", portada),
                    new OracleParameter("P_VIDEO",  video),
                    new OracleParameter("P_NOMBRE", nombre),
                    new OracleParameter("P_DESCRIPCION",  descripcion),
                    new OracleParameter("P_FECHAESTRENO",  fecha),
                    new OracleParameter("P_DURACION", duracion),
                    new OracleParameter("P_GENERO", genero),
                    new OracleParameter("P_TIPO_PRODUCTO", tipo)
                    };
                conexion.ejecutarDMLProcedure(conexion.Conectar(), "REGISTRAR_PRODUCTOS", parameters);
                MessageBox.Show("Se ha registrado correctamente el producto",
                "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
            }
            /*
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
            */
        }

        //Proyecta los datos de los productos
        public DataGridView proyectarProductos(DataGridView dgvProductos)
        {
            try
            {
                OracleParameter[] parameters =
                   new OracleParameter[] {
                   new OracleParameter("REG_PRODUCTOS", OracleType.Cursor)};
                parameters[0].Direction = ParameterDirection.Output;
                return conexion.ejecutarDMLProcedure(conexion.Conectar(),
                    "PROYECTAR_PRODUCTOS", parameters, dgvProductos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
                return dgvProductos;
            }
            /*
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
            */
        }

        public void llenarcbCodigos(System.Windows.Forms.ComboBox cbTitulos)
        {
            DataSet dataSet = 
                conexion.ejecutarDML(conexion.Conectar(), "SELECT CODIGO FROM PRODUCTO");
            cbTitulos.DataSource = dataSet.Tables[0];
            cbTitulos.DisplayMember = "CODIGO";
            /*
            OracleConnection conc = conexion.Conectar();
            OracleCommand comando = new OracleCommand("SELECT CODIGO FROM PRODUCTO", conc);
            conc.Open();
            OracleDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                cbTitulos.Items.Add(registro["CODIGO"].ToString());

            }
            conc.Close();*/
        }


        public void ActualizarProducto(int codPelicula, int codAdmin, String portada, String descripcion, String fecha, String duracion, String genero)
        {
            try
            {
                OracleParameter[] parameters =
                    new OracleParameter[] {
                    new OracleParameter("P_CODIGO_ADMIN",codAdmin),
                    new OracleParameter("P_PORTADA", portada),
                    new OracleParameter("P_DESCRIPCION",  descripcion),
                    new OracleParameter("P_FECHAESTRENO",  fecha),
                    new OracleParameter("P_DURACION", duracion),
                    new OracleParameter("P_GENERO", genero),
                    };
                conexion.ejecutarDMLProcedure(conexion.Conectar(), "ACTUALIZAR_PRODUCTOS", parameters);
                MessageBox.Show("Se ha actualizado correctamente el producto",
                "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
            }
            /*
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
            */
        }

        public void deshabilitarProducto(int codProducto)
        {
            try
            {
                OracleParameter[] parameters =
                    new OracleParameter[] {
                    new OracleParameter("P_CODIGO", codProducto)};
                conexion.ejecutarDMLProcedure(conexion.Conectar(), "DESHABILITAR_PRODUCTO", parameters);
                MessageBox.Show("Se ha deshabilitadoo el producto correctamente ",
                "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
            }
            /*
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
            */
        }

        public void llenarcbCodigosHabilitados(System.Windows.Forms.ComboBox cbTitulos)
        {
            DataSet dataSet =
            conexion.ejecutarDML(conexion.Conectar(),
            "SELECT CODIGO FROM PRODUCTO WHERE ESTADO_PRODUCTO = 1");
            cbTitulos.DataSource = dataSet.Tables[0];
            cbTitulos.DisplayMember = "CODIGO";
            /*
            OracleConnection con = conexion.Conectar();
            OracleCommand comando = new OracleCommand("SELECT CODIGO FROM PRODUCTO WHERE ESTADO_PRODUCTO = 1", con);
            con.Open();
            OracleDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                cbTitulos.Items.Add(registro["CODIGO"].ToString());

            }
            con.Close();
            */
        }

    }
}
