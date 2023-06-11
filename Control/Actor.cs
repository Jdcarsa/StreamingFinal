using BasesDatosFormulario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing;


namespace PlataformaStreaming.Control
{
     internal class Actor
    {
        public static Conexion conexion = new Conexion();

        public Actor() { }

        public void llenarcbCodigoActor(System.Windows.Forms.ComboBox cbTitulos)
        {

            DataSet dataSet = conexion.ejecutarDML(conexion.Conectar(), "SELECT CODIGO FROM ACTOR");
            cbTitulos.DataSource = dataSet.Tables[0];
            cbTitulos.DisplayMember = "CODIGO";

        }

        /* public void llenarcbCodigoActor(System.Windows.Forms.ComboBox cbTitulos)
         {
             OracleConnection con = conexion.Conectar();
             OracleCommand comando = new OracleCommand("SELECT CODIGO FROM ACTOR", con);
             con.Open();
             OracleDataReader registro = comando.ExecuteReader();
             while (registro.Read())
             {
                 cbTitulos.Items.Add(registro["CODIGO"].ToString());

             }
             con.Close();
         }*/

        //Registra un producto a la base de datos
        public void registrarActuacion(int codProducto, int codActor, String actuacion)
        {
            try
            {
                OracleParameter[] parameters =
                    new OracleParameter[] {
                    new OracleParameter("P_PRODUCTO",codProducto),
                    new OracleParameter("P_ACTOR",codActor),
                    new OracleParameter("P_PAPEL",actuacion),
                    };
                conexion.ejecutarDMLProcedure(conexion.Conectar(), "REGISTRAR_ACTUACION", parameters);
                MessageBox.Show("Se ha registrado correctamente la actuaccion",
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
                OracleCommand comando = new OracleCommand("REGISTRAR_ACTUACION", con);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("P_PRODUCTO", OracleType.Number).Value = codProducto;
                comando.Parameters.Add("P_ACTOR", OracleType.Number).Value = codActor;
                comando.Parameters.Add("P_PAPEL", OracleType.VarChar).Value = actuacion;

                comando.ExecuteNonQuery();
                MessageBox.Show("Se ha registrado correctamente el la actuaccion",
                "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
            }
            con.Close();
            */
        }

        //Proyecta los datos de los actores
        public DataGridView proyectarActores(DataGridView dgvActores)
        {

            try
            {
                OracleParameter[] parameters =
                   new OracleParameter[] {
                   new OracleParameter("REG_ACTORES", OracleType.Cursor)};
                parameters[0].Direction = ParameterDirection.Output;
                return conexion.ejecutarDMLProcedure(conexion.Conectar(), 
                    "PROYECTAR_ACTORES", parameters, dgvActores);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
                return dgvActores;
            }
            /*
            OracleConnection con = conexion.Conectar();
            con.Open();
            try
            {
                OracleCommand comando = new OracleCommand("PROYECTAR_ACTORES", con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("REG_ACTORES", OracleType.Cursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adaptador = new OracleDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                dgvActores.DataSource = tabla;

                con.Close();

                return dgvActores;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
                con.Close();
                return dgvActores;
            }*/

        }

        public bool registrarActor(string pNombre, string sNombre, string pApellido, string sApellido, string fechaNacimiento)
        {
            try
            {
                OracleParameter[] parameters =
                    new OracleParameter[] {
                    new OracleParameter("P_NOMBRE1",pNombre),
                    new OracleParameter("P_NOMBRE2",sNombre),
                    new OracleParameter("P_APELLIDO1",pApellido),
                    new OracleParameter("P_APELLIDO2",sApellido),
                    new OracleParameter("P_FECHANACIMIENTO", fechaNacimiento) };
                conexion.ejecutarDMLProcedure(conexion.Conectar(), "CREAR_ACTOR", parameters);
                MessageBox.Show("Se ha registrado correctamente el actor",
                "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error" + ex.Message);
                return false;
            }
            /*
            OracleConnection con = conexion.Conectar();
            con.Open();
            try
            {
                OracleCommand comando = new OracleCommand("CREAR_ACTOR", con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("P_NOMBRE1", OracleType.VarChar).Value = pNombre;
                comando.Parameters.Add("P_NOMBRE2", OracleType.VarChar).Value = sNombre;
                comando.Parameters.Add("P_APELLIDO1", OracleType.VarChar).Value = pApellido;
                comando.Parameters.Add("P_APELLIDO2", OracleType.VarChar).Value = sApellido;
                comando.Parameters.Add("P_FECHANACIMIENTO", OracleType.DateTime).Value = fechaNacimiento;
                comando.ExecuteNonQuery();
                MessageBox.Show("Actor creado correctamente :D");
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error" + ex.Message);
                con.Open();
                return false;
            }
            */
        }
    }
}
