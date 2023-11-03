using DataLayer.Conexion;
using DataLayer.EntityModel;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Crypto;
using System.Text.Json.Serialization;
using System.Data;
namespace LogicLayer.Marca
{
    public class Marca
    {




		private int idMarca;
        private string idUM;
        private string razon;
        private int IdMarca;



        public Marca()
        {
        }

        public Marca(int idC, string idUM, string razon)
        {
            idMarca = idC;
            this.idUM = idUM;
            this.razon = razon;
        }

        public Marca(int IdC)
        {
            IdMarca = IdC;
        }

 

        public bool SetMarca(ref MarcaEntity marcaEntity)
        {

            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_MARCA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "G");
                objStoreProc.Add_Par_Int_Input("@I_ID_MARCA", 0);
                objStoreProc.Add_Par_VarChar_Input("@I_NOMBRE", marcaEntity.C_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", marcaEntity.C_Url_IMG);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", marcaEntity.C_Usuario_Creacion);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_MODIFICACION", null);
                objStoreProc.Add_Par_Int_Output("@O_TRANSACCION_ESTADO");
                objStoreProc.Add_Par_VarChar_Output("@O_TRANSACCION_MENSAJE", 200);



                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                int O_TRANSACCION_ESTADO = 0;
                string O_TRANSACCION_MENSAJE = "";




                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    O_TRANSACCION_MENSAJE = (string)objStoreProc.obtenerValorParametroOutput("@O_TRANSACCION_MENSAJE");
                    O_TRANSACCION_ESTADO = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@O_TRANSACCION_ESTADO"));

                    marcaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    marcaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                    res = true;
                }

                else
                {
                    marcaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    marcaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                marcaEntity.C_Transaccion_Estado = 26;
                marcaEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool CambiarEstadoMarca(ref MarcaEntity marcaEntity)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_MARCA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "C");
                objStoreProc.Add_Par_Int_Input("@I_ID_MARCA", idMarca);
                objStoreProc.Add_Par_VarChar_Input("@I_NOMBRE", null);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", null);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION",razon);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_MODIFICACION", idUM);
                objStoreProc.Add_Par_Int_Output("@O_TRANSACCION_ESTADO");
                objStoreProc.Add_Par_VarChar_Output("@O_TRANSACCION_MENSAJE", 200);



                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                int O_TRANSACCION_ESTADO = 0;
                string O_TRANSACCION_MENSAJE = "";




                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    O_TRANSACCION_MENSAJE = (string)objStoreProc.obtenerValorParametroOutput("@O_TRANSACCION_MENSAJE");
                    O_TRANSACCION_ESTADO = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@O_TRANSACCION_ESTADO"));

                

                    if (O_TRANSACCION_ESTADO == 0)
                    {
                        marcaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        marcaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }
                    else
                    {
                        marcaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        marcaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = false;
                    }
                }
                else
                {
                    marcaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    marcaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                marcaEntity.C_Transaccion_Estado = 26;
                marcaEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool PutMarca(ref MarcaEntity marcaEntity)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_MARCA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "A");
                objStoreProc.Add_Par_Int_Input("@I_ID_MARCA", marcaEntity.C_Id_Marca);
                objStoreProc.Add_Par_VarChar_Input("@I_NOMBRE", marcaEntity.C_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", marcaEntity.C_Url_IMG);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", marcaEntity.C_Descripcion);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_MODIFICACION", marcaEntity.C_Usuario_Modificacion);
                objStoreProc.Add_Par_Int_Output("@O_TRANSACCION_ESTADO");
                objStoreProc.Add_Par_VarChar_Output("@O_TRANSACCION_MENSAJE", 200);



                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                int O_TRANSACCION_ESTADO = 0;
                string O_TRANSACCION_MENSAJE = "";




                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    O_TRANSACCION_MENSAJE = (string)objStoreProc.obtenerValorParametroOutput("@O_TRANSACCION_MENSAJE");
                    O_TRANSACCION_ESTADO = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@O_TRANSACCION_ESTADO"));


                    if (O_TRANSACCION_ESTADO == 0)
                    {
                        marcaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        marcaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }
                    else
                    {
                        marcaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        marcaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = false;
                    }
                }

                else
                {
                    marcaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    marcaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                marcaEntity.C_Transaccion_Estado = 26;
                marcaEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool GetMarca(ref List<MarcaEntity> marcaEntity)
        {
            DataLayer.EntityModel.MarcaEntity MarcaEntity = new DataLayer.EntityModel.MarcaEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_MARCA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GA");
                objStoreProc.Add_Par_Int_Input("@I_ID_MARCA", 0);

                DataTable datas = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (datas.Rows.Count > 0)
                    {

                        foreach (DataRow row in datas.Rows)
                        {
                            MarcaEntity = new DataLayer.EntityModel.MarcaEntity();
                            MarcaEntity.C_Id_Marca = Convert.ToInt32(row["ID_MARCA"].ToString());
                            MarcaEntity.C_Nombre = row["NOMBRE"].ToString();
                            MarcaEntity.C_Url_IMG = row["URL_IMG"].ToString();
                            MarcaEntity.C_Estado = (bool)row["ESTADO"] ? 1 : 2;
                            MarcaEntity.C_Descripcion = row["DESCRIPCION"].ToString();
                            MarcaEntity.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                            MarcaEntity.C_Fecha_Modificacion = row["FECHA_MODIFICACION"].ToString();
                            MarcaEntity.C_Usuario_Creacion = row["USUARIO_CREACION"].ToString();
                            MarcaEntity.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"].ToString();

                            marcaEntity.Add(MarcaEntity);

                        }

                        res = true;

                    }
                    else
                    {
                        MarcaEntity.C_Transaccion_Estado = 33;
                        MarcaEntity.C_Transaccion_Mensaje = "No hay registros";
                        marcaEntity.Add(MarcaEntity);
                        res = false;
                    }

                }
                else
                {

                    MarcaEntity.C_Transaccion_Estado = 32;
                    MarcaEntity.C_Transaccion_Mensaje = msgResEjecucion;
                    marcaEntity.Add(MarcaEntity);
                    res = false;
                }
            }
            catch (Exception e)
            {
                MarcaEntity.C_Transaccion_Estado = 35;
                MarcaEntity.C_Transaccion_Mensaje = e.Message;
                marcaEntity.Add(MarcaEntity);
                res = false;
            }
            return res;
        }

        public bool GetMarcaById(ref MarcaEntity marcaEntity)
        {
        
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_MARCA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GI");
                objStoreProc.Add_Par_Int_Input("@I_ID_MARCA", IdMarca);

                DataTable datas = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (datas.Rows.Count > 0)
                    {

                        foreach (DataRow row in datas.Rows)
                        {
                            marcaEntity = new DataLayer.EntityModel.MarcaEntity();
                            marcaEntity.C_Id_Marca = Convert.ToInt32(row["ID_MARCA"].ToString());
                            marcaEntity.C_Nombre = row["NOMBRE"].ToString();
                            marcaEntity.C_Url_IMG = row["URL_IMG"].ToString();
                            marcaEntity.C_Estado = (bool)row["ESTADO"] ? 1 : 2;
                            marcaEntity.C_Descripcion = row["DESCRIPCION"].ToString();
                            marcaEntity.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                            marcaEntity.C_Fecha_Modificacion = row["FECHA_MODIFICACION"].ToString();
                            marcaEntity.C_Usuario_Creacion = row["USUARIO_CREACION"].ToString();
                            marcaEntity.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"].ToString();


                        }

                        res = true;

                    }
                    else
                    {
                        marcaEntity.C_Transaccion_Estado = 33;
                        marcaEntity.C_Transaccion_Mensaje = "No hay registros";
                       
                        res = false;
                    }

                }
                else
                {

                    marcaEntity.C_Transaccion_Estado = 32;
                    marcaEntity.C_Transaccion_Mensaje = msgResEjecucion;
                   
                    res = false;
                }
            }
            catch (Exception e)
            {
                marcaEntity.C_Transaccion_Estado = 35;
                marcaEntity.C_Transaccion_Mensaje = e.Message;
               
                res = false;
            }
            return res;
        }

        
    }
    
}
