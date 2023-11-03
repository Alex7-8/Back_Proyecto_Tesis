using DataLayer.Conexion;
using DataLayer.EntityModel;

using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

using System.Data;


namespace LogicLayer.SerieFactura
{
    public class SerieFactura
    {

	


		private int idSerie;
        private string idUM;
        private string razon;
        private int IdSerie;



        public SerieFactura()
        {
        }

        public SerieFactura(int idC, string idUM, string razon)
        {
            idSerie = idC;
            this.idUM = idUM;
            this.razon = razon;
        }

        public SerieFactura(int IdC)
        {
            IdSerie = IdC;
        }

 


   

        public bool SetSerie(ref SerieFacturaEntity serieEntity)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_SERIE_FACTURA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "G");
                objStoreProc.Add_Par_Int_Input("@I_ID_SERIE", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_SL", serieEntity.C_Id_Sl);
                objStoreProc.Add_Par_VarChar_Input("@I_NOMBRE", serieEntity.C_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", serieEntity.C_Usuario_Creacion);
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

                    if (O_TRANSACCION_ESTADO == 0)
                    {
                        serieEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        serieEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }
                    else
                    {
                        serieEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        serieEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = false;
                    }
                   
                }

                else
                {
                    serieEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    serieEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                serieEntity.C_Transaccion_Estado = 26;
                serieEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool PutSerie(ref SerieFacturaEntity serieEntity)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_SERIE_FACTURA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "A");
                objStoreProc.Add_Par_Int_Input("@I_ID_SERIE", serieEntity.C_Id_Serie);
                objStoreProc.Add_Par_Int_Input("@I_ID_SL", serieEntity.C_Id_Sl);
                objStoreProc.Add_Par_VarChar_Input("@I_NOMBRE", serieEntity.C_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", serieEntity.C_Descripcion);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_MODIFICACION", serieEntity.C_Usuario_Modificacion);
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
                        serieEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        serieEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }
                    else
                    {
                        serieEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        serieEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = false;
                    }

                }

                else
                {
                    serieEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    serieEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                serieEntity.C_Transaccion_Estado = 26;
                serieEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool CambiarEstadoSerie(ref SerieFacturaEntity serieEntity)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_SERIE_FACTURA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "C");
                objStoreProc.Add_Par_Int_Input("@I_ID_SERIE", idSerie);
                objStoreProc.Add_Par_Int_Input("@I_ID_SL", serieEntity.C_Id_Sl);
                objStoreProc.Add_Par_VarChar_Input("@I_NOMBRE", null);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", razon);
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
                        serieEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        serieEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }
                    else
                    {
                        serieEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        serieEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = false;
                    }

                }

                else
                {
                    serieEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    serieEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                serieEntity.C_Transaccion_Estado = 26;
                serieEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool GetSerie(ref List<SerieFacturaEntity> serieEntity)
        {
            DataLayer.EntityModel.SerieFacturaEntity SerieEntity = new DataLayer.EntityModel.SerieFacturaEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_SERIE_FACTURA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GA");
                objStoreProc.Add_Par_Int_Input("@I_ID_SERIE", 0);

                DataTable datas = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (datas.Rows.Count > 0)
                    {

                        foreach (DataRow row in datas.Rows)
                        {
                            SerieEntity = new DataLayer.EntityModel.SerieFacturaEntity();
                            SerieEntity.C_Id_Serie = Convert.ToInt32(row["ID_SERIE"].ToString());
                            SerieEntity.C_Nombre = row["NOMBRE"].ToString();
                            SerieEntity.C_Descripcion = row["DESCRIPCION"].ToString();
                            SerieEntity.C_Estado = (bool)row["ESTADO"] ? 1 : 2;
                            SerieEntity.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                            SerieEntity.C_Fecha_Modificacion = row["FECHA_MODIFICACION"].ToString();
                            SerieEntity.C_Usuario_Creacion = row["USUARIO_CREACION"].ToString();
                            SerieEntity.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"].ToString();

                            serieEntity.Add(SerieEntity);

                        }

                        res = true;

                    }
                    else
                    {
                        SerieEntity.C_Transaccion_Estado = 33;
                        SerieEntity.C_Transaccion_Mensaje = "No hay registros";
                        serieEntity.Add(SerieEntity);
                        res = false;
                    }

                }
                else
                {

                    SerieEntity.C_Transaccion_Estado = 32;
                    SerieEntity.C_Transaccion_Mensaje = msgResEjecucion;
                    serieEntity.Add(SerieEntity);
                    res = false;
                }
            }
            catch (Exception e)
            {
                SerieEntity.C_Transaccion_Estado = 35;
                SerieEntity.C_Transaccion_Mensaje = e.Message;
                serieEntity.Add(SerieEntity);
                res = false;
            }
            return res;
        }

        public bool GetSerieById(ref SerieFacturaEntity serieEntity)
        {

            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_SERIE_FACTURA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GI");
                objStoreProc.Add_Par_Int_Input("@I_ID_Serie", IdSerie);

                DataTable datas = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (datas.Rows.Count > 0)
                    {

                        foreach (DataRow row in datas.Rows)
                        {

                            serieEntity.C_Id_Serie = Convert.ToInt32(row["ID_SERIE"].ToString());
                            serieEntity.C_Id_Sl = Convert.ToInt32(row["ID_SL"].ToString());
                            serieEntity.C_Nombre = row["NOMBRE"].ToString();
                            serieEntity.C_Descripcion = row["DESCRIPCION"].ToString();
                            serieEntity.C_Estado = (bool)row["ESTADO"] ? 1 : 2;
                            serieEntity.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                            serieEntity.C_Fecha_Modificacion = row["FECHA_MODIFICACION"].ToString();
                            serieEntity.C_Usuario_Creacion = row["USUARIO_CREACION"].ToString();
                            serieEntity.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"].ToString();


                        }

                        res = true;

                    }
                    else
                    {
                        serieEntity.C_Transaccion_Estado = 33;
                        serieEntity.C_Transaccion_Mensaje = "No hay registros";

                        res = false;
                    }

                }
                else
                {

                    serieEntity.C_Transaccion_Estado = 32;
                    serieEntity.C_Transaccion_Mensaje = msgResEjecucion;

                    res = false;
                }
            }
            catch (Exception e)
            {
                serieEntity.C_Transaccion_Estado = 35;
                serieEntity.C_Transaccion_Mensaje = e.Message;

                res = false;
            }
            return res;
        }

        public bool GetSerieBySL(ref List<SerieFacturaEntity> serieEntity)
        {

            DataLayer.EntityModel.SerieFacturaEntity SerieEntity = new DataLayer.EntityModel.SerieFacturaEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_SERIE_FACTURA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GC");
                objStoreProc.Add_Par_Int_Input("@I_ID_SERIE", IdSerie);

                DataTable datas = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (datas.Rows.Count > 0)
                    {

                        foreach (DataRow row in datas.Rows)
                        {
                            SerieEntity = new DataLayer.EntityModel.SerieFacturaEntity();
                            SerieEntity.C_Id_Serie = Convert.ToInt32(row["ID_SERIE"].ToString());
                            SerieEntity.C_Nombre = row["NOMBRE"].ToString();

                            serieEntity.Add(SerieEntity);

                        }

                        res = true;

                    }
                    else
                    {
                        SerieEntity.C_Transaccion_Estado = 33;
                        SerieEntity.C_Transaccion_Mensaje = "No hay registros";
                        serieEntity.Add(SerieEntity);
                        res = false;
                    }

                }
                else
                {

                    SerieEntity.C_Transaccion_Estado = 32;
                    SerieEntity.C_Transaccion_Mensaje = msgResEjecucion;
                    serieEntity.Add(SerieEntity);
                    res = false;
                }
            }
            catch (Exception e)
            {
                SerieEntity.C_Transaccion_Estado = 35;
                SerieEntity.C_Transaccion_Mensaje = e.Message;
                serieEntity.Add(SerieEntity);
                res = false;
            }
            return res;
        }
    }
    
}
