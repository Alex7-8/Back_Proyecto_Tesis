using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.VisualBasic;

namespace DataLayer.Conexion
{
    public class EjectProcAlm
    {
        private SqlCommand Com;
        private SqlConnection ConSql;
        private SqlParameter Par;
        private string strConexion;



      


            public EjectProcAlm(string nom_proc, string usuario, string password)
        {
            prAsignarConexion(usuario, password, "", "");
            ConSql = new SqlConnection(strConexion);
            Com = new SqlCommand(nom_proc, ConSql);
            Com.CommandType = CommandType.StoredProcedure;
        }

        private void prAsignarConexion(string strUsuario, string strContrasenia, string database = "", string servidor = "")
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();


              builder.DataSource = string.IsNullOrEmpty(servidor) ? "wdb4.my-hosting-panel.com" : servidor;
              builder.UserID = string.IsNullOrEmpty(strUsuario) ? "srvcentr_service" : strUsuario;
              builder.Password = string.IsNullOrEmpty(strContrasenia) ? "I8knq4?79" : strContrasenia;
              builder.InitialCatalog = string.IsNullOrEmpty(database) ? "Control_Servicios" : database;
              builder.PersistSecurityInfo = false;
              builder.MultipleActiveResultSets = false;
              builder.Encrypt = false;
              builder.TrustServerCertificate = false;
              builder.ConnectTimeout = 30;
             strConexion = builder.ConnectionString;
           // strConexion = strSql;
        }
        public void Add_Par_Datetime_Input(string nombre, DateTime valor)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.DateTime);
            Par.Direction = ParameterDirection.Input;
            Par.Value = valor;
            Com.Parameters.Add(Par);
        }
        public void Add_Par_Datetime_Output(string nombre, DateTime valor)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.DateTime);
            Par.Direction = ParameterDirection.Output;
            Com.Parameters.Add(Par);
        }
        public void Add_Par_VarChar_Input(string nombre, string valor)
        {      
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.VarChar);
            Par.Direction = ParameterDirection.Input;
            if (Strings.Trim(valor).Equals(""))
            {
                Par.Value = DBNull.Value;

            }
            else
            {
                Par.Value = Strings.Trim(valor);

            }
            Com.Parameters.Add(Par);
        
        }

        public void Add_Par_VarChar_Output(string nombre, int Tam)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.VarChar);
            Par.Size = Tam;
            Par.Direction = ParameterDirection.Output;
            // Par.Value = Trim(valor)
            Com.Parameters.Add(Par);
        }

        public void Add_Par_Int_Input(string nombre, int valor)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.Int);
            Par.Direction = ParameterDirection.Input;
            Par.Value = valor;
            Com.Parameters.Add(Par);
        }
        public void Add_Par_Int_Output(string nombre)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.Int);
            Par.Direction = ParameterDirection.Output;
            Com.Parameters.Add(Par);
        }
        public void Add_Par_BigInt_Input(string nombre, long valor)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.BigInt);
            Par.Direction = ParameterDirection.Input;
            Par.Value = valor;
            Com.Parameters.Add(Par);
        }
        public void Add_Par_BigInt_Output(string nombre)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.BigInt);
            Par.Direction = ParameterDirection.Output;
            Com.Parameters.Add(Par);
        }
        public void Add_Par_Float_Input(string nombre, double valor)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.Float);
            Par.Direction = ParameterDirection.Input;
            Par.Value = valor;
            Com.Parameters.Add(Par);
        }
        public void Add_Par_Float_Output(string nombre)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.Float);
            Par.Direction = ParameterDirection.Output;
            Com.Parameters.Add(Par);
        }
        public void Add_Par_Decimal_Input(string nombre, decimal valor)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.Decimal);
            Par.Direction = ParameterDirection.Input;
            Par.Value = valor;
            Com.Parameters.Add(Par);
        }
        public void Add_Par_Decimal_Output(string nombre)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.Decimal);
            Par.Direction = ParameterDirection.Output;
            Com.Parameters.Add(Par);
        }

        /// <summary>
        /// RECIBE LOS N PARÁMETROS NECESARIOS PARA EJECUTAR UN PROCEDIMIENTO ALMACENADO
        /// UTILIZA DATA SET PARA LLENAR ALGUN OBJETO O DEVOLVER DATOS... 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>UTILIZA DATA SET PARA LLENAR ALGUN OBJETO O DEVOLVER DATOS... </returns>
        public string Ejecutar_proc_alm_datatable_parametros(ref DataTable ds)
        {
            string salida = "";
            SqlDataAdapter DA;
            try
            {
                ConSql.Open();
                DA = new SqlDataAdapter(Com);
                ConSql.Close();
                DA.Fill(ds);
            }
            catch (SqlException ex)
            {
                salida = "-ERROR- " + ex.Message;
            }
            finally
            {
                ConSql.Close();
            }
            return salida;
        }

        /// <summary>
        /// RECIBE LOS N PARÁMETROS NECESARIOS PARA EJECUTAR UN PROCEDIMIENTO ALMACENADO
        /// SOLO EJECUTA EL PROCEDIMIENTO, NO DEVUELVE DATOS.
        /// </summary>
        /// <returns>Devuelve el resultado de la ejecución</returns>
        public string Ejecutar_proc_alm_parametros()
        {
            string salida = "";
            try
            {
                ConSql.Open();
                Com.ExecuteNonQuery();
                ConSql.Close();
            }
            catch (SqlException ex)
            {
                salida = "-ERROR- " + ex.Message;
            }
            finally
            {
                ConSql.Close();
            }
            return salida;
        }
        /// <summary>
        /// Esta función ejecuta un procedimiento almacenado que se configuro con 
        /// parámetros de salida o NO, y devulve el primer resultado de la primera fila
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public string EjecutarProcParSalida(ref object result)
        {
            string Salida = "";
            try
            {
                ConSql.Open();
                result = Com.ExecuteScalar();
            }

            catch (SqlException e)
            {
                Salida = "-ERROR- " + e.Message;
            }
            finally
            {
                ConSql.Close();
            }
            return Salida;
        }

        /// <summary>
        /// Funcion que retorna el valor de un parametro.
        /// </summary>
        /// <param name="strCampo">Nombre del parametro a devolver</param>
        /// <returns></returns>
        public object obtenerValorParametroOutput(string strCampo)
        {
            return Com.Parameters[strCampo].Value;
        }


    }

}