using DataLayer.Conexion;
using DataLayer.EntityModel;
using LogicLayer.Helper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicLayer.Servicios
{
    public class Servicios
    {


	

		private int idS;
        private string idUM;
        private string razon;
        private int idS1;

 

        public Servicios()
        {
        }

        public Servicios(int idS, string idUM, string razon)
        {
            this.idS = idS;
            this.idUM = idUM;
            this.razon = razon;
        }

        public Servicios(int idS1)
        {
            this.idS1 = idS1;
        }


        public bool SetServicios(ref ServiciosEntity serviciosEntity)
        {

            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_SERVICIOS", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GS");
                objStoreProc.Add_Par_Int_Input("@I_ID_SERVICIO", 0);
                objStoreProc.Add_Par_VarChar_Input("@I_NOMBRE_SERVICIO", serviciosEntity.C_Nombre_Servicio);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", serviciosEntity.C_Descripcion);
                objStoreProc.Add_Par_Decimal_Input("@I_PRECIO", serviciosEntity.C_Precio);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", serviciosEntity.C_Url_IMG);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", serviciosEntity.C_Usuario_Creacion);
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
                        serviciosEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        serviciosEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                        res = true;
                    }
                    else
                    {
                        serviciosEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        serviciosEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                        res = false;
                    }
                }

                else
                {
                    serviciosEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    serviciosEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                serviciosEntity.C_Transaccion_Estado = 26;
                serviciosEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool PutServicios(ref ServiciosEntity serviciosEntity)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_SERVICIOS", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "AS");
                objStoreProc.Add_Par_Int_Input("@I_ID_SERVICIO", serviciosEntity.C_Id_Servicio);
                objStoreProc.Add_Par_VarChar_Input("@I_NOMBRE_SERVICIO", serviciosEntity.C_Nombre_Servicio);
                objStoreProc.Add_Par_Decimal_Input("@I_PRECIO", serviciosEntity.C_Precio);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", serviciosEntity.C_Descripcion);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", serviciosEntity.C_Url_IMG);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_MODIFICACION", serviciosEntity.C_Usuario_Modificacion);
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
                        serviciosEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        serviciosEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                        res = true;
                    }
                    else
                    {
                        serviciosEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        serviciosEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                        res = false;
                    }
                }

                else
                {
                    serviciosEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    serviciosEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                serviciosEntity.C_Transaccion_Estado = 26;
                serviciosEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool CambiarEstadoServicio(ref ServiciosEntity serviciosEntity)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_SERVICIOS", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "CS");
                objStoreProc.Add_Par_Int_Input("@I_ID_SERVICIO", idS);
                objStoreProc.Add_Par_VarChar_Input("@I_NOMBRE_SERVICIO", null);
                objStoreProc.Add_Par_Decimal_Input("@I_PRECIO", (decimal)0.00);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", razon);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", "");
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
                        serviciosEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        serviciosEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                        res = true;
                    }
                    else 
                    {
                        serviciosEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        serviciosEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                        res = false;
                    }
                  
                }

                else
                {
                    serviciosEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    serviciosEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                serviciosEntity.C_Transaccion_Estado = 26;
                serviciosEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }


        public bool GetServicios(ref List<ServiciosEntity> serviciosEntity)
        {
            DataLayer.EntityModel.ServiciosEntity ServiciosEntity = new DataLayer.EntityModel.ServiciosEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_SERVICIOS", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GA");
                objStoreProc.Add_Par_Int_Input("@I_ID_SERVICIO", 0);

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (data.Rows.Count > 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            ServiciosEntity = new DataLayer.EntityModel.ServiciosEntity();
                            ServiciosEntity.C_Id_Servicio  = Convert.ToInt32(row["ID_SERVICIO"].ToString());
                            ServiciosEntity.C_Nombre_Servicio = row["NOMBRE"].ToString();
                            ServiciosEntity.C_Descripcion = row["DESCRIPCION"] != DBNull.Value ? row["DESCRIPCION"].ToString() : "";
                            // ServiciosEntity.C_Precio = Convert.ToDecimal(row["PRECIO"]);
                            ServiciosEntity.C_Precio_Final = row["PRECIO"].ToString();
                            ServiciosEntity.C_Url_IMG = row["URL_IMG"].ToString();
                            ServiciosEntity.C_Usuario_Creacion = row["USUARIO_CREACION"].ToString();
                            ServiciosEntity.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"] != DBNull.Value ? row["USUARIO_MODIFICACION"].ToString() : "";
                            ServiciosEntity.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                            ServiciosEntity.C_Fecha_Modificacion = row["FECHA_MODIFICACION"] != DBNull.Value ? row["FECHA_MODIFICACION"].ToString() : "";
                            ServiciosEntity.C_Estado = (bool)row["ESTADO"] ? 1 : 2;




                            serviciosEntity.Add(ServiciosEntity);

                        }

                        res = true;

                    }
                    else
                    {
                        ServiciosEntity.C_Transaccion_Estado = 33;
                        ServiciosEntity.C_Transaccion_Mensaje = "No hay registros";
                        serviciosEntity.Add(ServiciosEntity);
                        res = false;
                    }

                }
                else
                {

                    ServiciosEntity.C_Transaccion_Estado = 32;
                    ServiciosEntity.C_Transaccion_Mensaje = msgResEjecucion;
                    serviciosEntity.Add(ServiciosEntity);
                    res = false;
                }
            }
            catch (Exception e)
            {
                ServiciosEntity.C_Transaccion_Estado = 35;
                ServiciosEntity.C_Transaccion_Mensaje = e.Message;
                serviciosEntity.Add(ServiciosEntity);
                res = false;
            }
            return res;
        }

        public bool GetServiciosById(ref ServiciosEntity serviciosEntity)
        {
           // DataLayer.EntityModel.ServiciosEntity ServiciosEntity = new DataLayer.EntityModel.ServiciosEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_SERVICIOS", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GI");
                objStoreProc.Add_Par_Int_Input("@I_ID_SERVICIO", idS1);

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                   

                    if (data.Rows.Count > 0)
                    {

                        DataRow row = data.Rows[0];

                            serviciosEntity.C_Id_Servicio = Convert.ToInt32(row["ID_SERVICIO"].ToString());
                            serviciosEntity.C_Nombre_Servicio = row["NOMBRE"].ToString();
                            serviciosEntity.C_Estado = (bool)row["ESTADO"] ? 1 : 2;
                            //serviciosEntity.C_Precio = Convert.ToDecimal(row["PRECIO"]); 
                            serviciosEntity.C_Precio_Final = row["PRECIO"].ToString();
                            serviciosEntity.C_Descripcion = row["DESCRIPCION"].ToString();
                            serviciosEntity.C_Url_IMG = row["URL_IMG"].ToString();
                            serviciosEntity.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                            serviciosEntity.C_Fecha_Modificacion = row["FECHA_MODIFICACION"] != DBNull.Value ? row["FECHA_MODIFICACION"].ToString() : "";
                            serviciosEntity.C_Usuario_Creacion = row["USUARIO_CREACION"].ToString();
                            serviciosEntity.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"] != DBNull.Value ? row["USUARIO_MODIFICACION"].ToString() : "";
                            
                            
                            

                        

                        res = true;

                    }
                    else
                    {
                        serviciosEntity.C_Transaccion_Estado = 33;
                        serviciosEntity.C_Transaccion_Mensaje = "No hay registros";
                     
                        res = false;
                    }

                }
                else
                {

                    serviciosEntity.C_Transaccion_Estado = 32;
                    serviciosEntity.C_Transaccion_Mensaje = msgResEjecucion;
                   
                    res = false;
                }
            }
            catch (Exception e)
            {
                serviciosEntity.C_Transaccion_Estado = 35;
                serviciosEntity.C_Transaccion_Mensaje = e.Message;
                res = false;
            }
            return res;
        }
    }
}
