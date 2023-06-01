using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasesDatosFormulario;
using PlataformaStreaming.Control;

namespace PlataformaStreaming.Modelo
{
    internal class Servicio
    {
        //EN ESTA CLASE DEBE IR TODO LO RELACIONADO CON LA PLATAFORMA
        // INGRESAR TARJETA , LOGIN , 
        public static Conexion conexion = new Conexion();
        public Servicio() { }


        /*Busca en la base de datos el precio del plan 
         * al cual se ha ingresado el codigo por parametro
         * retorna un string que contiene el precio del plan
         */
        public string preciosPlanes(int n)
        {
            OracleConnection conc = conexion.Conectar();
            conc.Open();
            try
            {
                System.Data.OracleClient.OracleCommand comando = new System.Data.OracleClient.OracleCommand("PRECIO_SUSCRIPCION", conc);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.Add("P_CODIGO_PLAN", OracleType.Number).Value = n;
                comando.Parameters.Add("V_PRECIO", OracleType.Number, 11).Direction = ParameterDirection.Output;
                comando.ExecuteNonQuery();
                conc.Close();
                return comando.Parameters["V_PRECIO"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error" + ex.Message);
                conc.Close();
                return "0";
            }


        }
        //Asigna un plan a un usuario , recibe como parametro el nombre de usuario
        public void asignarPlan(string usuario, int codPlan)
        {
            OracleConnection conc = conexion.Conectar();
            conc.Open();
            try
            {
                OracleCommand comando = new OracleCommand("REGISTRAR_SUSCRIPCION", conc);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.Add("P_USUARIO_CLIENTE", OracleType.VarChar).Value = usuario;
                comando.Parameters.Add("P_CODIGO_PLAN", OracleType.Number).Value = codPlan;
                comando.ExecuteNonQuery();
                conc.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error" + ex.Message);
                conc.Close();
            }
        }

        //Devulve el tipo de acceso
        public int accesoPlataforma(string usuario, string contrasenia)
        {
            int existe = 0;
            int pExiste = 0;
            Cliente c = new Cliente();
            Admin a = new Admin();
            if (a.existeAdmin(usuario)) existe = 1;
            else if (c.existeUsuario(usuario)) existe = 2;
            else return 4;

            OracleConnection conc = conexion.Conectar();
            conc.Open();
            try
            {
                OracleCommand comando = new OracleCommand("LOGIN_USUARIO", conc);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.Add("V_EXISTE", OracleType.Number).Value = existe;
                comando.Parameters.Add("P_USUARIO", OracleType.VarChar).Value = usuario;
                comando.Parameters.Add("P_CONTRASENIA", OracleType.VarChar).Value = contrasenia;
                comando.Parameters.Add("V_ACCESO", OracleType.Number, 1).Direction = ParameterDirection.Output;
                comando.ExecuteNonQuery();
                conc.Close();
                pExiste = Convert.ToInt32(comando.Parameters["V_ACCESO"].Value);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error" + ex.Message);
                conc.Close();
            }
            return pExiste;
        }

        //REGISTRA UNA TARJETA DE CREDITO/DEBITO A NOMBRE DE UN USUARIO CLIENTE
        public void registrarTarjeta(string usuario, string tipoTarjeta, string numTarjeta, string nomTajrjeta, string fechaTarjeta, string ccvTarjeta)
        {
            try
            {
                OracleConnection con = conexion.Conectar();
                con.Open();
                System.Data.OracleClient.OracleCommand comando = new System.Data.OracleClient.OracleCommand("REGISTRAR_TARJETA", con);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.Add("P_USUARIO_CLIENTE", OracleType.VarChar).Value = usuario;
                //comando.Parameters.Add("P_CODIGO", OracleType.VarChar).Value = codigo;
                comando.Parameters.Add("P_NUMEROTARJETA", OracleType.VarChar).Value = numTarjeta;
                comando.Parameters.Add("P_FECHAEXP", OracleType.VarChar).Value = fechaTarjeta;
                comando.Parameters.Add("P_NOMBRETARJETA", OracleType.VarChar).Value = nomTajrjeta;
                comando.Parameters.Add("P_CVV", OracleType.VarChar).Value = ccvTarjeta;
                comando.Parameters.Add("P_TIPOTARJETA", OracleType.VarChar).Value = tipoTarjeta;
                comando.ExecuteNonQuery();
                MessageBox.Show("Pago Correcto");
                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
            }

        }


        public bool datosUsuario(string usuario, ref List<string> datos)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            try
            {
                string sql = "SELECT C.CODIGO, C.PRIMERNOMBRE, C.SEGUNDONOMBRE, C.PRIMERAPELLIDO, C.SEGUNDOAPELLIDO, C.CORREO, P.NOMBRE,"+
                    " P.PRECIO, T.NUMEROTARJETA, T.TIPOTARJETA FROM CLIENTE C INNER JOIN CLIENTE_PLAN CP ON CP.CODIGO_CLIENTE = C.CODIGO "+
                    "INNER JOIN PLANSUSCRIPCION P ON P.CODIGO = CP.CODIGO_PLAN INNER JOIN TARJETA T ON T.CLIENTE = C.CODIGO "+
                    "WHERE C.NOMBRE_USUARIO_CLIENTE = :usuario FETCH FIRST ROW ONLY";

                OracleCommand comando = new OracleCommand(sql, con);
                comando.CommandType = CommandType.Text;
                comando.Parameters.Add(":usuario", usuario);

                OracleDataReader dr = comando.ExecuteReader();
                if (dr.Read())
                {
                    datos.Add(dr["PRIMERNOMBRE"].ToString());
                    datos.Add(dr["SEGUNDONOMBRE"].ToString());
                    datos.Add(dr["PRIMERAPELLIDO"].ToString());
                    datos.Add(dr["SEGUNDOAPELLIDO"].ToString());
                    datos.Add(dr["CODIGO"].ToString());
                    datos.Add(dr["CORREO"].ToString());
                    datos.Add(dr["TIPOTARJETA"].ToString());
                    datos.Add(dr["NUMEROTARJETA"].ToString());
                    datos.Add(dr["NOMBRE"].ToString());
                    datos.Add(dr["PRECIO"].ToString());

                    dr.Close();
                    con.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
                con.Close();
                return false;
            }
        }
        public bool cargarDatosPlan(string usuario, ref List<String> datos)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            try
            {
                string c_consulta = "SELECT P.PRECIO, P.NOMBRE FROM CLIENTE C INNER JOIN CLIENTE_PLAN CP ON CP.CODIGO_CLIENTE = C.CODIGO " +
                    "INNER JOIN PLANSUSCRIPCION P ON P.CODIGO = CP.CODIGO_PLAN" +
                    "WHERE C.NOMBRE_USUARIO_CLIENTE = :usuario FETCH FIRST ROW ONLY";
                OracleCommand comando = new OracleCommand(c_consulta, con);
                comando.Parameters.Add(":usuario", usuario);
                OracleDataReader dr;

                dr = comando.ExecuteReader();
                if (dr.Read())
                {
                    datos.Add(dr["PRECIO"].ToString());
                    datos.Add(dr["NOMBRE"].ToString());

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
    }

    
}
