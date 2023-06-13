using System;
using System.Data.OracleClient;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;
using System.Windows.Forms;
using System.Drawing;
using Label = System.Windows.Forms.Label;
using System.Security.Permissions;
using System.Collections.Generic;
using PlataformaStreaming.Control;
using PlataformaStreaming.Vista;
using System.Collections;

namespace BasesDatosFormulario
{
    class Conexion
    {


        public Conexion() { }


        public  OracleConnection Conectar()
        {
            OracleConnection con = new OracleConnection("DATA SOURCE = localhost ; PASSWORD = clave123; USER ID = proyecto_bases;");
            return con;
        }


        public void ejecutarDMLProcedure(OracleConnection conexion , string nombreProcedimiento ,
            OracleParameter[] parameters)
        {
            conexion.Open();
                OracleCommand command = new OracleCommand(nombreProcedimiento, conexion);
                command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (OracleParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);

                }
            }
            command.ExecuteNonQuery();
            conexion.Close();
        }

        public Object ejecutarDMLProcedureOut(OracleConnection conexion, string nombreProcedimiento,
            OracleParameter[] parameters , string parametroSalida)
        {
            conexion.Open();
            OracleCommand command = new OracleCommand(nombreProcedimiento, conexion);
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (OracleParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);

                }
            }
            command.ExecuteNonQuery();
            return command.Parameters[parametroSalida].Value;
        }




        public DataGridView ejecutarDMLProcedure(OracleConnection conexion, string nombreProcedimiento,
        OracleParameter[] parameters, DataGridView gridView)
        {
            conexion.Open();
            OracleCommand command = new OracleCommand(nombreProcedimiento, conexion);
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (OracleParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);

                }
                OracleDataAdapter adaptador = new OracleDataAdapter();
                adaptador.SelectCommand = command;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                gridView.DataSource = tabla;
            }
            command.ExecuteNonQuery();
            conexion.Close();
            return gridView;
        }



        public DataSet ejecutarDML(OracleConnection conexion, string setencia)
        {
            OracleCommand command = new OracleCommand(setencia, conexion);
            command.Connection.Open();
            command.ExecuteNonQuery();
            OracleDataAdapter adapter = new OracleDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            command.Connection.Close();
            return dataSet;
        }

        public void  ejecutarTransaccion(OracleConnection conexion, string setencia , string setencia2
            , OracleParameter[] parameters , OracleParameter[] parameters2)
        {
            conexion.Open();
            OracleCommand command = new OracleCommand(setencia, conexion);
            OracleCommand command2 = new OracleCommand(setencia2, conexion);
            command.CommandType = CommandType.StoredProcedure;
            command2.CommandType = CommandType.StoredProcedure;
            OracleTransaction transaction = conexion.BeginTransaction();

            try
            {
                command.Transaction = transaction;
                command2.Transaction = transaction;

                if (parameters != null && parameters2 != null)
                {
                    foreach (OracleParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                    foreach (OracleParameter param in parameters2)
                    {
                        command2.Parameters.Add(param);
                    }
                }
                command.ExecuteNonQuery();
                command2.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
                transaction.Rollback();
            }
            conexion.Close();
        }

    }

    
}