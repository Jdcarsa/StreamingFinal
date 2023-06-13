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
    internal class Diccionario
    {
        public static Conexion conexion = new Conexion();
        public void llenarcbNombreTablas(System.Windows.Forms.ComboBox cbTitulos)
        {
            DataSet dataSet =
            conexion.ejecutarDML(conexion.Conectar(), "SELECT OBJECT_NAME FROM USER_OBJECTS WHERE OBJECT_TYPE = 'TABLE'");
            cbTitulos.DataSource = dataSet.Tables[0];
            cbTitulos.DisplayMember = "OBJECT_NAME";
        }

        public DataGridView proyectarTablas(DataGridView dgvDatos)
        {
            try
            {
                OracleParameter[] parameters = new OracleParameter[] {new OracleParameter("REG_TABLAS", OracleType.Cursor)};
                parameters[0].Direction = ParameterDirection.Output;
                return conexion.ejecutarDMLProcedure(conexion.Conectar(), "PROYECTAR_TABLAS", parameters, dgvDatos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
                return dgvDatos;
            }
        }

        public DataGridView proyectarProcedimientos(DataGridView dgvDatos)
        {
            try
            {
                OracleParameter[] parameters = new OracleParameter[] { new OracleParameter("REG_PROCEDIMIENTOS", OracleType.Cursor) };
                parameters[0].Direction = ParameterDirection.Output;
                return conexion.ejecutarDMLProcedure(conexion.Conectar(), "PROYECTAR_PROCEDIMIENTOS", parameters, dgvDatos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
                return dgvDatos;
            }
        }

        public DataGridView proyectarColumnas(string nombreTabla,DataGridView dgvDatos)
        {
            try
            {
                OracleParameter[] parameters = new OracleParameter[] 
                {
                    new OracleParameter("REG_PROCEDIMIENTOS", OracleType.Cursor),
                    new OracleParameter("V_NOMBRE_TABLA", nombreTabla)
                };
                parameters[0].Direction = ParameterDirection.Output;
                return conexion.ejecutarDMLProcedure(conexion.Conectar(), "PROYECTAR_COLUMNAS", parameters, dgvDatos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
                return dgvDatos;
            }
        }

        public DataGridView proyectarRestricciones(string nombreTabla, DataGridView dgvDatos)
        {
            try
            {
                OracleParameter[] parameters = new OracleParameter[]
                {
                    new OracleParameter("REG_RESTRICCIONES", OracleType.Cursor),
                    new OracleParameter("V_NOMBRE_TABLA", nombreTabla)
                };
                parameters[0].Direction = ParameterDirection.Output;
                return conexion.ejecutarDMLProcedure(conexion.Conectar(), "PROYECTAR_RESTRICCIONES", parameters, dgvDatos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
                return dgvDatos;
            }
        }

        public void proyectarVista(string nombreVista, DataGridView vista)
        {

            DataSet dataSet =
            conexion.ejecutarDML(conexion.Conectar(), "SELECT * FROM "+nombreVista+"");
            vista.DataSource = dataSet.Tables[0];
        }

        public void llenarcbNombreVistas(System.Windows.Forms.ComboBox cbTitulos)
        {
            DataSet dataSet =
            conexion.ejecutarDML(conexion.Conectar(), "SELECT * FROM USER_OBJECTS WHERE OBJECT_TYPE = 'VIEW'");
            cbTitulos.DataSource = dataSet.Tables[0];
            cbTitulos.DisplayMember = "OBJECT_NAME";
        }
    }
}
