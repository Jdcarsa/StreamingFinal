using BasesDatosFormulario;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasesDatosFormulario;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

namespace PlataformaStreaming.Control
{


    internal class Cliente
    {
        public static Conexion conexion = new Conexion();
        public static Admin admin = new Admin();

        public Cliente()
        {
        }
        /* Metodo para insetar clientes a la base de datos 
         * obtiene como parametros los datos ingresados en cada textBox
         *  retorna un boolean , true si se completo , false si no */
        public Boolean insertarClientes(string usuario, string pNombre, string sNombre
           , string pApellido, string sApellido, string email, string telf
           , string contrasenia, string fechaNac)
        {

            OracleConnection con = conexion.Conectar();
            try
            {
                con.Open();
                OracleCommand comando = new OracleCommand("CREAR_CLIENTE", con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("P_USUARIO_CLIENTE", OracleType.VarChar).Value = usuario;
                comando.Parameters.Add("P_NOMBRE1", OracleType.VarChar).Value = pNombre;
                comando.Parameters.Add("P_NOMBRE2", OracleType.VarChar).Value = sNombre;
                comando.Parameters.Add("P_APELLIDO1", OracleType.VarChar).Value = pApellido;
                comando.Parameters.Add("P_APELLIDO2", OracleType.VarChar).Value = sApellido;
                comando.Parameters.Add("P_FECHANACIMIENTO", OracleType.VarChar).Value = fechaNac;
                comando.Parameters.Add("P_CONTRASENIA", OracleType.VarChar).Value = contrasenia;
                comando.Parameters.Add("P_TELEFONO", OracleType.VarChar).Value = telf;
                comando.Parameters.Add("P_CORREO", OracleType.VarChar).Value = email;
                comando.ExecuteNonQuery();
                con.Close();
                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error" + ex.Message);
                con.Close();
                return false;
            }

        }
        //Para evitar problemas , primero averiguia si existe el usuario
        //Si existe no lo deja crear y duelve un true en caso de que si exista
        // O false en caso de que no
        public Boolean existeUsuario(string usuario)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            OracleCommand comando = new OracleCommand("EXISTE_CLIENTE", con);
            try
            {

                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("P_USUARIO_CLIENTE", OracleType.VarChar).Value = usuario;
                comando.Parameters.Add("V_EXISTE", OracleType.Number, 10).Direction = ParameterDirection.Output;
                comando.ExecuteNonQuery();
                con.Close();
                string alm = comando.Parameters["V_EXISTE"].Value.ToString();
                return alm.Equals("1") ? true : false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error   " + ex.Message.ToString(), ex.Message);
                Console.WriteLine(ex.Message.ToString());
                return false;
            }

        }

        public Boolean esMayorEdad(int edad)
        {
            return edad >= 18;
        }

        public Boolean registrarCliente(string tbUsuario, string tboxPNombre,
               string tboxSNombre, string tbPApellido, string tbSApellido, string tbEmail
               , string tbTelf, string tbContrasenia, string timePicker, int edad)
        {
            if (esMayorEdad(edad))
            {
                if (!existeUsuario(tbUsuario)
                    && !admin.existeAdmin(tbUsuario))
                {
                    return insertarClientes(tbUsuario, tboxPNombre, tboxSNombre, tbPApellido, tbSApellido, tbEmail
                    , tbTelf, tbContrasenia, timePicker);
                }
                else
                {
                    MessageBox.Show("El usuario ingresado se encuentra en uso",
                    "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

            }
            else
            {
                MessageBox.Show("El cliente ingresado es menor de edad",
                "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

        }

        public void llenarcbCodigoCliente(System.Windows.Forms.ComboBox cbTitulos)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            OracleCommand comando = new OracleCommand("SELECT CODIGO FROM CLIENTE WHERE TIPOACCESO != 3", con);

            OracleDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                cbTitulos.Items.Add(registro["CODIGO"].ToString());

            }
            con.Close();
        }

        public bool actualizarCliente(int codigo, string NombreUsuario, string pNombre, string sNombre,
             string pApellido, string sApellido, string fechaNac, string contrasenia, string telf, string email)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            try
            {
                OracleCommand comando = new OracleCommand("ACTUALIZAR_CLIENTE", con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("P_CODIGO", OracleType.Number).Value = codigo;
                comando.Parameters.Add("P_USUARIO_CLIENTE", OracleType.VarChar).Value = NombreUsuario;
                comando.Parameters.Add("P_NOMBRE1", OracleType.VarChar).Value = pNombre;
                comando.Parameters.Add("P_NOMBRE2", OracleType.VarChar).Value = sNombre;
                comando.Parameters.Add("P_APELLIDO1", OracleType.VarChar).Value = pApellido;
                comando.Parameters.Add("P_APELLIDO2", OracleType.VarChar).Value = sApellido;
                comando.Parameters.Add("P_FECHANACIMIENTO", OracleType.DateTime).Value = fechaNac;
                comando.Parameters.Add("P_CONTRASENIA", OracleType.VarChar).Value = contrasenia;
                comando.Parameters.Add("P_TELEFONO", OracleType.VarChar).Value = telf;
                comando.Parameters.Add("P_CORREO", OracleType.VarChar).Value = email;
                comando.ExecuteNonQuery();
                MessageBox.Show("Cuenta actualizada correctamente");
                con.Close();
                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error" + ex.Message);
                con.Close();
                return false;
            }
        }

        public DataGridView proyectarClientes(DataGridView dgvClientes)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            try
            {
                OracleCommand comando = new OracleCommand("PROYECTAR_CLIENTES", con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("REG_CLIENTES", OracleType.Cursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adaptador = new OracleDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                dgvClientes.DataSource = tabla;

                con.Close();

                return dgvClientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
                con.Close();
                return dgvClientes;

            }

        }

        public bool cargarClienteUI(int prmCodigo, ref List<String> valores)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            try
            {
                string c_consulta = "SELECT PRIMERNOMBRE, SEGUNDONOMBRE, PRIMERAPELLIDO,SEGUNDOAPELLIDO, TELEFONO," +
                    " FECHANACIMIENTO, CORREO, NOMBRE_USUARIO_CLIENTE, CONTRASENIA FROM CLIENTE WHERE CODIGO = " + prmCodigo + "";
                OracleCommand comando = new OracleCommand(c_consulta, con);
                OracleDataReader dr;
                dr = comando.ExecuteReader();
                if (dr.Read())
                {
                    valores.Add(dr["PRIMERNOMBRE"].ToString());
                    valores.Add(dr["SEGUNDONOMBRE"].ToString());
                    valores.Add(dr["PRIMERAPELLIDO"].ToString());
                    valores.Add(dr["SEGUNDOAPELLIDO"].ToString());
                    valores.Add(dr["TELEFONO"].ToString());
                    valores.Add(dr["FECHANACIMIENTO"].ToString());
                    valores.Add(dr["CORREO"].ToString());
                    valores.Add(dr["NOMBRE_USUARIO_CLIENTE"].ToString());
                    valores.Add(dr["CONTRASENIA"].ToString());

                    return true;
                }
                con.Close();
                return false;

            }
            catch (Exception e)
            {
                con.Close();
                return false;
            }

        }

        public bool recuperarContrasenia(int prmCodigo, string tabla, ref string contrasenia)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            try
            {
                string c_consulta = "SELECT CONTRASENIA FROM " + tabla + " WHERE CODIGO = " + prmCodigo + "";
                OracleCommand comando = new OracleCommand(c_consulta, con);
                OracleDataReader dr;
                dr = comando.ExecuteReader();
                if (dr.Read())
                {
                    contrasenia = dr["CONTRASENIA"].ToString();
                    return true;
                }
                con.Close();
                return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public void deshabilitarCliente(int codCliente)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            try
            {
                OracleCommand comando = new OracleCommand("DESHABILITAR_CLIENTE", con);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("P_CODIGO", OracleType.Number).Value = codCliente;

                comando.ExecuteNonQuery();
                MessageBox.Show("Se ha deshabilitado de manera exitosa :D");
                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
            }
        }
    }

}
