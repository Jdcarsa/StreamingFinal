using BasesDatosFormulario;
using PlataformaStreaming.Modelo.Entidades;
using System;
using System.Data;
using System.Data.OracleClient;
using System.Windows.Forms;

namespace PlataformaStreaming.Control
{
    internal class ProductoControlador
    {
        public static Conexion conexion = new Conexion();

        public ProductoControlador() { }

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


        public bool registrarProducto(Producto nuevoProducto)
        {
            try
            {
                OracleParameter[] parameters =
                    new OracleParameter[] {
                new OracleParameter("P_CODIGO_ADMIN", nuevoProducto.CodigoAdmin),
                new OracleParameter("P_PORTADA", nuevoProducto.Portada),
                new OracleParameter("P_VIDEO", nuevoProducto.Video),
                new OracleParameter("P_NOMBRE", nuevoProducto.Nombre),
                new OracleParameter("P_DESCRIPCION", nuevoProducto.Descripcion),
                new OracleParameter("P_FECHAESTRENO", nuevoProducto.FechaEstreno),
                new OracleParameter("P_DURACION", nuevoProducto.Duracion),
                new OracleParameter("P_GENERO", nuevoProducto.Genero),
                new OracleParameter("P_TIPO_PRODUCTO", nuevoProducto.TipoProducto)
                    };
                conexion.ejecutarDMLProcedure(conexion.Conectar(), "REGISTRAR_PRODUCTOS", parameters);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
                return false;
            }
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

        }

        public void llenarcbCodigos(System.Windows.Forms.ComboBox cbTitulos)
        {
            DataSet dataSet =
                conexion.ejecutarDML(conexion.Conectar(), "SELECT CODIGO FROM PRODUCTO");
            cbTitulos.DataSource = dataSet.Tables[0];
            cbTitulos.DisplayMember = "CODIGO";

        }


        public bool ActualizarProducto(int codPelicula, Producto productoActualizado)
        {
            try
            {
                OracleParameter[] parameters =
                    new OracleParameter[] {
                new OracleParameter("P_CODIGO_ADMIN", productoActualizado.CodigoAdmin),
                new OracleParameter("P_PORTADA", productoActualizado.Portada),
                new OracleParameter("P_DESCRIPCION", productoActualizado.Descripcion),
                new OracleParameter("P_FECHAESTRENO", productoActualizado.FechaEstreno),
                new OracleParameter("P_DURACION", productoActualizado.Duracion),
                new OracleParameter("P_GENERO", productoActualizado.Genero)
                    };
                conexion.ejecutarDMLProcedure(conexion.Conectar(), "ACTUALIZAR_PRODUCTOS", parameters);
                return true;
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
                return false;
            }

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

        }

        public void llenarcbCodigosHabilitados(System.Windows.Forms.ComboBox cbTitulos)
        {
            DataSet dataSet =
            conexion.ejecutarDML(conexion.Conectar(),
            "SELECT CODIGO FROM PRODUCTO WHERE ESTADO_PRODUCTO = 1");
            cbTitulos.DataSource = dataSet.Tables[0];
            cbTitulos.DisplayMember = "CODIGO";

        }

    }
}
