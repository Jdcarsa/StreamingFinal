using BasesDatosFormulario;
using PlataformaStreaming.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Windows.Forms;

namespace PlataformaStreaming.Control
{


    internal class ClienteControlador
    {
        public static Conexion conexion = new Conexion();
        public static AdminControlador admin = new AdminControlador();

        public ClienteControlador()
        {
        }
        /* Metodo para insetar clientes a la base de datos 
         * obtiene como parametros los datos ingresados en cada textBox
         *  retorna un boolean , true si se completo , false si no */
        public bool insertarClientes(Cliente nuevoCliente)
        {
            try
            {
                DateTime fechaActual = DateTime.Today;
                OracleParameter[] parameters =
                    new OracleParameter[] {
                new OracleParameter("P_USUARIO_CLIENTE", nuevoCliente.NombreUsuarioCliente),
                new OracleParameter("P_NOMBRE1", nuevoCliente.PrimerNombre),
                new OracleParameter("P_NOMBRE2", nuevoCliente.SegundoNombre),
                new OracleParameter("P_APELLIDO1", nuevoCliente.PrimerApellido),
                new OracleParameter("P_APELLIDO2", nuevoCliente.SegundoApellido),
                new OracleParameter("P_FECHANACIMIENTO", nuevoCliente.FechaNacimiento),
                new OracleParameter("P_CONTRASENIA", nuevoCliente.Contrasenia),
                new OracleParameter("P_FECHAREGISTRO", fechaActual),
                new OracleParameter("P_TELEFONO", nuevoCliente.Telefono),
                new OracleParameter("P_CORREO", nuevoCliente.Correo)
                    };
                conexion.ejecutarDMLProcedure(conexion.Conectar(), "CREAR_CLIENTE", parameters);
                MessageBox.Show("Se ha registrado correctamente el cliente",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error: " + ex.Message);
                return false;
            }
        }

        //Para evitar problemas , primero averiguia si existe el usuario
        //Si existe no lo deja crear y duelve un true en caso de que si exista
        // O false en caso de que no
        public Boolean existeUsuario(string usuario)
        {
            try
            {
                OracleParameter[] parameters =
                    new OracleParameter[] {
                    new OracleParameter("P_USUARIO_CLIENTE",  usuario),
                    new OracleParameter("V_EXISTE", OracleType.Number, 2)};
                parameters[1].Direction = ParameterDirection.Output;
                string alm = conexion.ejecutarDMLProcedureOut(
                    conexion.Conectar(), "EXISTE_CLIENTE", parameters, "V_EXISTE").ToString();
                return alm.Equals("1") ? true : false;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error" + ex.Message);
                return false;
            }
           

        }

        public Boolean esMayorEdad(int edad)
        {
            return edad >= 18;
        }

        public Boolean registrarCliente(Cliente nuevoCliente, int edad)
        {
            if (esMayorEdad(edad))
            {
                if (!existeUsuario(nuevoCliente.NombreUsuarioCliente) && !admin.existeAdmin(nuevoCliente.NombreUsuarioCliente))
                {
                    return insertarClientes(nuevoCliente);
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
            DataSet dataSet = conexion.ejecutarDML(conexion.Conectar(),
                "SELECT CODIGO FROM CLIENTE WHERE TIPOACCESO != 3");
            cbTitulos.DataSource = dataSet.Tables[0];
            cbTitulos.DisplayMember = "CODIGO";
          
        }

        public bool actualizarCliente(Cliente clienteActualizado)
        {
            try
            {
                OracleParameter[] parameters =
                    new OracleParameter[] {
                new OracleParameter("P_CODIGO", clienteActualizado.Codigo),
                new OracleParameter("P_USUARIO_CLIENTE", clienteActualizado.NombreUsuarioCliente),
                new OracleParameter("P_NOMBRE1", clienteActualizado.PrimerNombre),
                new OracleParameter("P_NOMBRE2", clienteActualizado.SegundoNombre),
                new OracleParameter("P_APELLIDO1", clienteActualizado.PrimerApellido),
                new OracleParameter("P_APELLIDO2", clienteActualizado.SegundoApellido),
                new OracleParameter("P_FECHANACIMIENTO", clienteActualizado.FechaNacimiento),
                new OracleParameter("P_CONTRASENIA", clienteActualizado.Contrasenia),
                new OracleParameter("P_TELEFONO", clienteActualizado.Telefono),
                new OracleParameter("P_CORREO", clienteActualizado.Correo)
                    };
                conexion.ejecutarDMLProcedure(conexion.Conectar(), "ACTUALIZAR_CLIENTE", parameters);
                MessageBox.Show("Cuenta actualizada correctamente",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
                return false;
            }
        }


        public DataGridView proyectarClientes(DataGridView dgvClientes)
        {

            try
            {
                OracleParameter[] parameters =
                   new OracleParameter[] {
                   new OracleParameter("REG_CLIENTES", OracleType.Cursor)};
                parameters[0].Direction = ParameterDirection.Output;
                return conexion.ejecutarDMLProcedure(conexion.Conectar(),
                    "PROYECTAR_CLIENTES", parameters, dgvClientes);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
                return dgvClientes;
            }
          
        }

        public bool cargarClienteConCodigo(string prmCodigo, ref List<String> valores)
        {
            try
            {
                DataSet dataSet = conexion.ejecutarDML(conexion.Conectar(),
                "SELECT PRIMERNOMBRE, SEGUNDONOMBRE, PRIMERAPELLIDO,SEGUNDOAPELLIDO, TELEFONO," +
                    " FECHANACIMIENTO, CORREO, NOMBRE_USUARIO_CLIENTE, CONTRASENIA FROM CLIENTE WHERE CODIGO = " + int.Parse(prmCodigo) + "");
                valores.Add(dataSet.Tables[0].Rows[0]["PRIMERNOMBRE"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["SEGUNDONOMBRE"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["PRIMERAPELLIDO"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["SEGUNDOAPELLIDO"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["TELEFONO"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["FECHANACIMIENTO"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["CORREO"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["NOMBRE_USUARIO_CLIENTE"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["CONTRASENIA"].ToString());
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
      

        }

        public bool cargarClienteConUsuario(string prmUsuario, ref List<String> valores)
        {

            try
            {
                DataSet dataSet = conexion.ejecutarDML(conexion.Conectar(),
                "SELECT PRIMERNOMBRE, SEGUNDONOMBRE, PRIMERAPELLIDO,SEGUNDOAPELLIDO, TELEFONO," +
                    " FECHANACIMIENTO, CORREO, NOMBRE_USUARIO_CLIENTE, CONTRASENIA, CODIGO FROM CLIENTE WHERE NOMBRE_USUARIO_CLIENTE = '" + prmUsuario + "'");
                valores.Add(dataSet.Tables[0].Rows[0]["PRIMERNOMBRE"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["SEGUNDONOMBRE"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["PRIMERAPELLIDO"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["SEGUNDOAPELLIDO"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["TELEFONO"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["FECHANACIMIENTO"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["CORREO"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["NOMBRE_USUARIO_CLIENTE"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["CONTRASENIA"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["CODIGO"].ToString());
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool recuperarContrasenia(int prmCodigo, string tabla, ref string contrasenia)
        {
            try
            {
                DataSet dataSet = conexion.ejecutarDML(conexion.Conectar(),
                     "SELECT CONTRASENIA FROM " + tabla + " WHERE CODIGO = " + prmCodigo + "");
                contrasenia = dataSet.Tables[0].Rows[0]["CONTRASENIA"].ToString();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

          
        }

        public void deshabilitarCliente(int codCliente)
        {
            try
            {
                OracleParameter[] parameters =
                    new OracleParameter[] {
                    new OracleParameter("P_CODIGO", codCliente)};
                conexion.ejecutarDMLProcedure(conexion.Conectar(), "DESHABILITAR_CLIENTE", parameters);
                MessageBox.Show("Se ha deshabilitado la cuenta",
                "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error" + ex.Message);
            }
          
        }

        public string buscarCodigoCliente(string usuario)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            string codigo = "";

            try
            {
                string consulta = "SELECT CODIGO FROM CLIENTE WHERE NOMBRE_USUARIO_CLIENTE = :usuario";

                OracleCommand comando = new OracleCommand(consulta, con);
                comando.Parameters.Add(":usuario", usuario);
                OracleDataReader dr;

                dr = comando.ExecuteReader();
                if (dr.Read())
                {
                    codigo = dr["CODIGO"].ToString();

                    return codigo;
                }
                con.Close();

                return codigo;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error" + ex.Message);
                return "";
            }
        }

    }
}
