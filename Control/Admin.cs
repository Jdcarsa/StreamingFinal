using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasesDatosFormulario;
using System.Drawing;

namespace PlataformaStreaming.Control
{
     internal class Admin
    {
        public static Conexion conexion = new Conexion();
        public static Cliente cliente = new Cliente();
        public Admin() { }
        /* Metodo para insetar clientes a la base de datos 
         * obtiene como parametros los datos ingresados en cada textBox
         *  retorna un boolean , true si se completo , false si no */
        public Boolean insertarAdmin(string usuario, string pNombre, string sNombre
            , string pApellido, string sApellido, string email, string contrasenia
            , string fechaNac, int id, string telf)
        {
            OracleConnection con = conexion.Conectar();
            try
            {
                con.Open();

                DateTime fechaActual = DateTime.Today;
                OracleCommand comando = new OracleCommand("CREAR_ADMINISTRADOR", con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("P_ID", OracleType.Number).Value = id;
                comando.Parameters.Add("P_USUARIO_ADMIN", OracleType.VarChar).Value = usuario;
                comando.Parameters.Add("P_NOMBRE1", OracleType.VarChar).Value = pNombre;
                comando.Parameters.Add("P_NOMBRE2", OracleType.VarChar).Value = sNombre;
                comando.Parameters.Add("P_APELLIDO1", OracleType.VarChar).Value = pApellido;
                comando.Parameters.Add("P_APELLIDO2", OracleType.VarChar).Value = sApellido;
                comando.Parameters.Add("P_FECHANACIMIENTO", OracleType.VarChar).Value = fechaNac;
                comando.Parameters.Add("P_CONTRASENIA", OracleType.VarChar).Value = contrasenia;
                comando.Parameters.Add("P_FECHACONTRATO", OracleType.DateTime).Value = fechaActual;
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

        //Para evitar problemas , primero averigua si existe el usuario
        //Si existe no lo deja crear y duelve un true en caso de que si exista
        // O false en caso de que no
        public Boolean existeAdmin(string usuario)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            OracleCommand comando = new OracleCommand("EXISTE_ADMIN", con);
            try
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("P_USUARIO_ADMIN", OracleType.VarChar).Value = usuario;
                comando.Parameters.Add("V_EXISTE", OracleType.Number, 10).Direction = ParameterDirection.Output;
                comando.ExecuteNonQuery();
                con.Close();
                string alm = comando.Parameters["V_EXISTE"].Value.ToString();
                return alm.Equals("1") ? true : false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error   " + ex.Message);
                con.Close();
                return false;
            }

        }

        public Boolean existeAdminID(string usuario)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            OracleCommand comando = new OracleCommand("EXISTE_ADMIN_ID", con);
            try
            {

                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("P_USUARIO_ADMIN", OracleType.VarChar).Value = usuario;
                comando.Parameters.Add("V_EXISTE", OracleType.Number, 10).Direction = ParameterDirection.Output;
                comando.ExecuteNonQuery();
                con.Close();
                string alm = comando.Parameters["V_EXISTE"].Value.ToString();
                return alm.Equals("1") ? true : false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error   " + ex.Message);
                con.Close();
                return false;
            }

        }

        public Boolean esMayorEdad(int edad)
        {
            return edad >= 18;
        }


        public Boolean registrarAdmin(string tbUsuario, string tboxPNombre,
       string tboxSNombre, string tbPApellido, string tbSApellido, string tbEmail
       , string tbTelf, string tbContrasenia, string timePicker, int id ,int edad)
        {
            if (esMayorEdad(edad))
            {
                if ((!existeAdmin(tbUsuario)
                    || !existeAdminID(tbUsuario)) && !cliente.existeUsuario(tbUsuario))
                {
                    return insertarAdmin(tbUsuario, tboxPNombre, tboxSNombre, tbPApellido, tbSApellido, tbEmail
                    , tbContrasenia, timePicker, id, tbTelf);
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
                MessageBox.Show("El Administrador ingresado es menor de edad",
                "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

        }

        public Boolean actualizarSU(string contrasenia, string contrasenia_nueva)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            try
            {
                DateTime fechaActual = DateTime.Today;
                OracleCommand comando = new OracleCommand("ACTUALIZAR_SU", con);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("P_CONTRASENIA", OracleType.VarChar).Value = contrasenia;
                comando.Parameters.Add("P_CONTRASENIA_NUEVA", OracleType.VarChar).Value = contrasenia_nueva;
                comando.ExecuteNonQuery();
                MessageBox.Show("Se ha actualizado la contraseña. :D");
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

        public bool cargarAdmin(int prmCodigo, ref List<String> valores)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            try
            {
                string c_consulta = "SELECT PRIMERNOMBRE, SEGUNDONOMBRE, PRIMERAPELLIDO,SEGUNDOAPELLIDO,  TELEFONO," +
                    "CORREO, CONTRASENIA, NOMBRE_USUARIO_ADMIN FROM ADMINISTRADOR WHERE CODIGO = " + prmCodigo + "";
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
                    valores.Add(dr["CONTRASENIA"].ToString());
                    valores.Add(dr["NOMBRE_USUARIO_ADMIN"].ToString());
                    return true;
                }
                return false;
            con.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            con.Close();
        }

        public bool actualizarAdmin(int codigo, string NombreUsuario, string pNombre, string sNombre,
             string pApellido, string sApellido, string contrasenia, string telf)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            try { 


                    OracleCommand comando = new OracleCommand("ACTUALIZAR_ADMINISTRADOR", con);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("P_CODIGO", OracleType.Number).Value = codigo;
            comando.Parameters.Add("P_USUARIO_ADMIN", OracleType.VarChar).Value = NombreUsuario;
            comando.Parameters.Add("P_NOMBRE1", OracleType.VarChar).Value = pNombre;
            comando.Parameters.Add("P_NOMBRE2", OracleType.VarChar).Value = sNombre;
            comando.Parameters.Add("P_APELLIDO1", OracleType.VarChar).Value = pApellido;
            comando.Parameters.Add("P_APELLIDO2", OracleType.VarChar).Value = sApellido;
            comando.Parameters.Add("P_CONTRASENIA", OracleType.VarChar).Value = contrasenia;
            comando.Parameters.Add("P_TELEFONO", OracleType.VarChar).Value = telf;
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

        public void llenarcbCodigoAdministrador(System.Windows.Forms.ComboBox cbTitulos)
        {
            OracleConnection con = conexion.Conectar();
            OracleCommand comando = new OracleCommand("SELECT CODIGO FROM ADMINISTRADOR WHERE CODIGO != 1", con);
            con.Open();
            OracleDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                cbTitulos.Items.Add(registro["CODIGO"].ToString());
            }
            con.Close();
        }

        //Proyecta los datos de los administradores
        public DataGridView proyectarAdministradores(DataGridView dgvAdministradores)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            try
            {
                OracleCommand comando = new OracleCommand("PROYECTAR_ADMINISTRADORES", con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("REG_ADMINISTRADORES", OracleType.Cursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adaptador = new OracleDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                dgvAdministradores.DataSource = tabla;

                con.Close();

                return dgvAdministradores;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
                con.Close();
                return dgvAdministradores;
            }

        }

        public void deshabilitarAdministrador(int codAdmin)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            try
            {
                OracleCommand comando = new OracleCommand("DESHABILITAR_ADMINISTRADOR", con);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("P_CODIGO", OracleType.Number).Value = codAdmin;

                comando.ExecuteNonQuery();
                MessageBox.Show("Se ha deshabilitado de manera exitosa :D");
                
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
            }
            con.Close();
        }

        public int recuperaCodigo (string usuarioAdmin)
        {
            int codigo = 0;
            OracleConnection con = conexion.Conectar();
            OracleCommand comando = new OracleCommand("SELECT CODIGO FROM ADMINISTRADOR WHERE NOMBRE_USUARIO_ADMIN = '" + usuarioAdmin + "'", con);
            con.Open();
            OracleDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
               codigo = Convert.ToInt32(registro["CODIGO"].ToString());
            }
            con.Close();
            return codigo;
        }
    }
}
