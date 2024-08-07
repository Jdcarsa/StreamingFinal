﻿using BasesDatosFormulario;
using PlataformaStreaming.Control;
using PlataformaStreaming.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Windows.Forms;

namespace PlataformaStreaming.Modelo
{
    internal class Servicio
    {
        //EN ESTA CLASE DEBE IR TODO LO RELACIONADO CON LA PLATAFORMA
        // INGRESAR TARJETA , LOGIN , 
        public static Conexion conexion = new Conexion();
        public static ClienteControlador cliente = new ClienteControlador();
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
                System.Data.OracleClient.OracleCommand comando =
                    new System.Data.OracleClient.OracleCommand("PRECIO_SUSCRIPCION", conc);
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
            try
            {
                OracleParameter[] parameters =
                    new OracleParameter[] {
                    new OracleParameter("P_USUARIO_CLIENTE", usuario),
                    new OracleParameter("P_CODIGO_PLAN", codPlan)
                    };
                conexion.ejecutarDMLProcedure(conexion.Conectar(), "REGISTRAR_SUSCRIPCION", parameters);
            }
            catch (Exception ex)

            {
                MessageBox.Show("Ha ocurrido un error! " + ex.Message);
            }

        }

        //Devulve el tipo de acceso
        public int accesoPlataforma(string usuario, string contrasenia)
        {
            int existe = 0;
            int pExiste = 0;
            ClienteControlador c = new ClienteControlador();
            AdminControlador a = new AdminControlador();
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

        public void transaccion(Tarjeta tarjeta, int codPlan)
        {
            try
            {
                OracleParameter[] parameters =
                    new OracleParameter[] {
                new OracleParameter("P_USUARIO_CLIENTE", tarjeta.NombreUsuarioCliente),
                new OracleParameter("P_NUMEROTARJETA", tarjeta.NumeroTarjeta),
                new OracleParameter("P_FECHAEXP", tarjeta.FechaExp),
                new OracleParameter("P_NOMBRETARJETA", tarjeta.NombreTarjeta),
                new OracleParameter("P_CVV", tarjeta.CVV),
                new OracleParameter("P_TIPOTARJETA", tarjeta.TipoTarjeta)
                    };
                OracleParameter[] parameters2 =
                    new OracleParameter[] {
                new OracleParameter("P_USUARIO_CLIENTE", tarjeta.NombreUsuarioCliente),
                new OracleParameter("P_CODIGO_PLAN", codPlan)
                    };
                conexion.ejecutarTransaccion(conexion.Conectar(), "REGISTRAR_SUSCRIPCION",
                    "REGISTRAR_TARJETA", parameters2, parameters);
                MessageBox.Show("Pago Correcto",
                    "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error: " + ex.Message);
            }
        }


        public bool datosUsuario(string usuario, ref List<string> datos)
        {
            OracleConnection con = conexion.Conectar();
            con.Open();
            try
            {
                string sql = "SELECT C.CODIGO, C.PRIMERNOMBRE, C.SEGUNDONOMBRE, C.PRIMERAPELLIDO, C.SEGUNDOAPELLIDO, C.CORREO, P.NOMBRE," +
                    " P.PRECIO, T.NUMEROTARJETA, T.TIPOTARJETA FROM CLIENTE C INNER JOIN CLIENTE_PLAN CP ON CP.CODIGO_CLIENTE = C.CODIGO " +
                    "INNER JOIN PLANSUSCRIPCION P ON P.CODIGO = CP.CODIGO_PLAN INNER JOIN TARJETA T ON T.CLIENTE = C.CODIGO " +
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

        public void registrarReproduccion(string usuario, string producto)
        {
            string codigo = cliente.buscarCodigoCliente(usuario);
            try
            {
                OracleParameter[] parameters =
                    new OracleParameter[] {
                    new OracleParameter("P_COD_CLIENTE", codigo),
                    new OracleParameter("P_COD_PRODUCTO", producto)
                    };
                conexion.ejecutarDMLProcedure(conexion.Conectar(), "REGISTRAR_REPRODUCCION", parameters);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error" + ex.Message);
            }
        }
    }

}
