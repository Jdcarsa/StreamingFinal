using BasesDatosFormulario;
using PlataformaStreaming.Modelo.Entidades;
using System;
using System.Data;
using System.Data.OracleClient;
using System.Windows.Forms;


namespace PlataformaStreaming.Control
{
    internal class ActorControlador
    {
        public static Conexion conexion = new Conexion();

        public ActorControlador() { }

        public void llenarcbCodigoActor(System.Windows.Forms.ComboBox cbTitulos)
        {

            DataSet dataSet = conexion.ejecutarDML(conexion.Conectar(), "SELECT CODIGO FROM ACTOR");
            cbTitulos.DataSource = dataSet.Tables[0];
            cbTitulos.DisplayMember = "CODIGO";

        }


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


        }

        public bool registrarActor(Actor nuevoActor)
        {
            try
            {
                OracleParameter[] parameters =
                    new OracleParameter[] {
                new OracleParameter("P_NOMBRE1", nuevoActor.PrimerNombre),
                new OracleParameter("P_NOMBRE2", nuevoActor.SegundoNombre),
                new OracleParameter("P_APELLIDO1", nuevoActor.PrimerApellido),
                new OracleParameter("P_APELLIDO2", nuevoActor.SegundoApellido),
                new OracleParameter("P_FECHANACIMIENTO", nuevoActor.FechaNacimiento)
                    };
                conexion.ejecutarDMLProcedure(conexion.Conectar(), "CREAR_ACTOR", parameters);
                MessageBox.Show("Se ha registrado correctamente el actor",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
                return false;
            }
        }

    }
}
