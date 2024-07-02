using BasesDatosFormulario;
using PlataformaStreaming.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Windows.Forms;

namespace PlataformaStreaming.Control
{
    internal class AdminControlador
    {
        public static Conexion conexion = new Conexion();
        public static ClienteControlador cliente = new ClienteControlador();
        public AdminControlador() { }
        /* Metodo para insetar clientes a la base de datos 
         * obtiene como parametros los datos ingresados en cada textBox
         *  retorna un boolean , true si se completo , false si no */
        public bool insertarAdmin(Administrador nuevoAdmin)
        {
            try
            {
                DateTime fechaActual = DateTime.Today;
                OracleParameter[] parameters =
                    new OracleParameter[] {
                new OracleParameter("P_ID", nuevoAdmin.AdmId),
                new OracleParameter("P_USUARIO_ADMIN", nuevoAdmin.NombreUsuarioAdmin),
                new OracleParameter("P_NOMBRE1", nuevoAdmin.PrimerNombre),
                new OracleParameter("P_NOMBRE2", nuevoAdmin.SegundoNombre),
                new OracleParameter("P_APELLIDO1", nuevoAdmin.PrimerApellido),
                new OracleParameter("P_APELLIDO2", nuevoAdmin.SegundoApellido),
                new OracleParameter("P_FECHANACIMIENTO", nuevoAdmin.FechaNacimiento),
                new OracleParameter("P_CONTRASENIA", nuevoAdmin.Contrasenia),
                new OracleParameter("P_FECHACONTRATO", fechaActual),
                new OracleParameter("P_TELEFONO",  nuevoAdmin.Telefono),
                new OracleParameter("P_CORREO",  nuevoAdmin.Correo)
                    };
                conexion.ejecutarDMLProcedure(conexion.Conectar(), "CREAR_ADMINISTRADOR", parameters);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
                return false;
            }
        }


        //Para evitar problemas , primero averigua si existe el usuario
        //Si existe no lo deja crear y duelve un true en caso de que si exista
        // O false en caso de que no
        public Boolean existeAdmin(string usuario)
        {
            try
            {
                OracleParameter[] parameters =
                    new OracleParameter[] {
                    new OracleParameter("P_USUARIO_ADMIN",  usuario),
                    new OracleParameter("V_EXISTE", OracleType.Number, 2)};
                parameters[1].Direction = ParameterDirection.Output;
                string alm = conexion.ejecutarDMLProcedureOut(
                    conexion.Conectar(), "EXISTE_ADMIN", parameters, "V_EXISTE").ToString();
                return alm.Equals("1") ? true : false;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error" + ex.Message);
                return false;
            }
          

        }

        public Boolean existeAdminID(string usuario)
        {
            try
            {
                OracleParameter[] parameters =
                    new OracleParameter[] {
                    new OracleParameter("P_USUARIO_ADMIN",  usuario),
                    new OracleParameter("V_EXISTE", OracleType.Number, 2)};
                parameters[1].Direction = ParameterDirection.Output;
                string alm = conexion.ejecutarDMLProcedureOut(
                    conexion.Conectar(), "EXISTE_ADMIN_ID", parameters, "V_EXISTE").ToString();
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


        public bool registrarAdmin(Administrador nuevoAdmin)
        {
            int edad = DateTime.Today.Year - nuevoAdmin.FechaNacimiento.Year;

            if (esMayorEdad(edad))
            {
                if ((!existeAdmin(nuevoAdmin.NombreUsuarioAdmin)
                     || !existeAdminID(nuevoAdmin.AdmId.ToString())) && !cliente.existeUsuario(nuevoAdmin.NombreUsuarioAdmin))
                {
                    return insertarAdmin(nuevoAdmin);
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

            try
            {
                OracleParameter[] parameters =
                    new OracleParameter[] {
                    new OracleParameter("P_CONTRASENIA", contrasenia),
                    new OracleParameter("P_CONTRASENIA_NUEVA", contrasenia_nueva), };
                conexion.ejecutarDMLProcedure(conexion.Conectar(), "ACTUALIZAR_SU", parameters);
                MessageBox.Show("Se ha actualizado la contraseña.",
                "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error" + ex.Message);
                return false;
            }
          
        }

        public bool cargarAdmin(int prmCodigo, ref List<String> valores)
        {

            try
            {
                DataSet dataSet = conexion.ejecutarDML(conexion.Conectar(),
                "SELECT PRIMERNOMBRE, SEGUNDONOMBRE, PRIMERAPELLIDO,SEGUNDOAPELLIDO,  TELEFONO," +
                "CORREO, CONTRASENIA, NOMBRE_USUARIO_ADMIN FROM ADMINISTRADOR WHERE CODIGO = "
                + prmCodigo + "");
                valores.Add(dataSet.Tables[0].Rows[0]["PRIMERNOMBRE"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["SEGUNDONOMBRE"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["PRIMERAPELLIDO"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["SEGUNDOAPELLIDO"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["TELEFONO"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["CONTRASENIA"].ToString());
                valores.Add(dataSet.Tables[0].Rows[0]["NOMBRE_USUARIO_ADMIN"].ToString());
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }



        public bool actualizarAdmin(int codigo, string NombreUsuario, string pNombre, string sNombre,
             string pApellido, string sApellido, string contrasenia, string telf)
        {
            try
            {
                OracleParameter[] parameters =
                   new OracleParameter[] {
                    new OracleParameter("P_CODIGO", codigo),
                    new OracleParameter("P_USUARIO_ADMIN", NombreUsuario),
                    new OracleParameter("P_NOMBRE1",pNombre),
                    new OracleParameter("P_NOMBRE2",sNombre),
                    new OracleParameter("P_APELLIDO1",pApellido),
                    new OracleParameter("P_APELLIDO2",sApellido),
                    new OracleParameter("P_CONTRASENIA", contrasenia),
                    new OracleParameter("P_TELEFONO",  telf)
                     };
                conexion.ejecutarDMLProcedure(conexion.Conectar(), "ACTUALIZAR_ADMINISTRADOR", parameters);
                MessageBox.Show("Cuenta actualizada correctamente",
                "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error" + ex.Message);
                return false;
            }
          
        }

        public void llenarcbCodigoAdministrador(System.Windows.Forms.ComboBox cbTitulos)
        {
            DataSet dataSet =
                conexion.ejecutarDML(conexion.Conectar(), "SELECT CODIGO FROM ADMINISTRADOR WHERE CODIGO != 1");
            cbTitulos.DataSource = dataSet.Tables[0];
            cbTitulos.DisplayMember = "CODIGO";
          
        }

        //Proyecta los datos de los administradores
        public DataGridView proyectarAdministradores(DataGridView dgvAdministradores)
        {
            try
            {
                OracleParameter[] parameters =
                   new OracleParameter[] {
                   new OracleParameter("REG_ADMINISTRADORES", OracleType.Cursor)};
                parameters[0].Direction = ParameterDirection.Output;
                return conexion.ejecutarDMLProcedure(conexion.Conectar(),
                    "PROYECTAR_ADMINISTRADORES", parameters, dgvAdministradores);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
                return dgvAdministradores;
            }
           

        }

        public void deshabilitarAdministrador(int codAdmin)
        {
            try
            {
                OracleParameter[] parameters =
                    new OracleParameter[] {
                    new OracleParameter("P_CODIGO", codAdmin),
                    };
                conexion.ejecutarDMLProcedure(conexion.Conectar(), "DESHABILITAR_ADMINISTRADOR", parameters);
                MessageBox.Show("Se ha deshabilitado del administrador correctamente",
                "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
            }
           
        }

        public int recuperaCodigo(string usuarioAdmin)
        {
            int codigo = 0;
            DataSet dataSet = conexion.ejecutarDML(conexion.Conectar(),
                "SELECT CODIGO FROM ADMINISTRADOR WHERE NOMBRE_USUARIO_ADMIN = '" + usuarioAdmin + "'");
            codigo = Convert.ToInt32(dataSet.Tables[0].Rows[0]["CODIGO"]);
            return codigo;

         
        }

    }
}

