using DataLayer.Conexion;
using DataLayer.EntityModel;
using LogicLayer.Helper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicLayer.Sucursal
{
    public class Sucursal
    {




		private int idS;
        private string idUM;
        private string razon;
        private int IdSL;



        public Sucursal()
        {
        }

        public Sucursal(int idC, string idUM, string razon)
        {
           idS = idC;
            this.idUM = idUM;
            this.razon = razon;
        }

        public Sucursal(int IdC)
        {
           IdSL = IdC;
        }


      

     



        public bool SetSucursal(ref SucursalEntity sucursalEntity)
        {

            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_SUCURSAL", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GC");
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_MUNICIPIO", sucursalEntity.C_Id_Municipio);
                objStoreProc.Add_Par_VarChar_Input("@I_DIRECCION", sucursalEntity.C_Direccion);
                objStoreProc.Add_Par_VarChar_Input("@I_NOMBRE", sucursalEntity.C_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", sucursalEntity.C_Url_Img);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", sucursalEntity.C_Usuario_Creacion);
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
                        sucursalEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        sucursalEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }
                    else
                    {
                        sucursalEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        sucursalEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = false;
                    }

                }

                else
                {
                    sucursalEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    sucursalEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                sucursalEntity.C_Transaccion_Estado = 26;
                sucursalEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool CambiarEstadoSucursal(ref SucursalEntity sucursalEntity)
        {

            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_SUCURSAL", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "CC");
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", idS);
                objStoreProc.Add_Par_Int_Input("@I_ID_MUNICIPIO", 0);
                objStoreProc.Add_Par_VarChar_Input("@I_DIRECCION",null);
                objStoreProc.Add_Par_VarChar_Input("@I_NOMBRE", null);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", razon);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", null);
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
                        sucursalEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        sucursalEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }
                    else
                    {
                        sucursalEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        sucursalEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = false;
                    }
                }

                else
                {
                    sucursalEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    sucursalEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                sucursalEntity.C_Transaccion_Estado = 26;
                sucursalEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool PutSucursal(ref SucursalEntity sucursalEntity)
        {

            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_SUCURSAL", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GC");
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", sucursalEntity.C_Id_Sucursal);
                objStoreProc.Add_Par_Int_Input("@I_ID_MUNICIPIO", sucursalEntity.C_Id_Municipio);
                objStoreProc.Add_Par_VarChar_Input("@I_DIRECCION", sucursalEntity.C_Direccion);
                objStoreProc.Add_Par_VarChar_Input("@I_NOMBRE", sucursalEntity.C_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", sucursalEntity.C_Descripcion);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", sucursalEntity.C_Url_Img);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", sucursalEntity.C_Usuario_Creacion);
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
                        sucursalEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        sucursalEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }
                    else
                    {
                        sucursalEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        sucursalEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = false;
                    }
                }

                else
                {
                    sucursalEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    sucursalEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                sucursalEntity.C_Transaccion_Estado = 26;
                sucursalEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool GetSucursal(ref List<SucursalEntity> sucursalEntity)
        {
            DataLayer.EntityModel.SucursalEntity SucursalEntity = new DataLayer.EntityModel.SucursalEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_SUCURSAL", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GA");
                objStoreProc.Add_Par_Int_Input("@I_ID_SL", 0);

                DataTable datas = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (datas.Rows.Count > 0)
                    {

                        foreach (DataRow row in datas.Rows)
                        {
                            SucursalEntity = new DataLayer.EntityModel.SucursalEntity();
                            SucursalEntity.C_Id_Sucursal = Convert.ToInt32(row["ID_SL"].ToString());
                            SucursalEntity.C_Id_Municipio = Convert.ToInt32(row["ID_MUN"].ToString());
                            SucursalEntity.C_Direccion = row["DIRECCION"].ToString(); 
                            SucursalEntity.C_Nombre = row["NOMBRE"].ToString();
                            SucursalEntity.C_Url_Img = row["URL_IMG"].ToString();
                            SucursalEntity.C_Estado = (bool)row["ESTADO"] ? 1 : 2;
                            SucursalEntity.C_Descripcion = row["DESCRIPCION"].ToString();
                            SucursalEntity.C_Usuario_Creacion = row["USUARIO_CREACION"].ToString();
                            SucursalEntity.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"] != DBNull.Value ? row["USUARIO_MODIFICACION"].ToString() : "";
                            SucursalEntity.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                            SucursalEntity.C_Fecha_Modificacion = row["FECHA_MODIFICACION"] != DBNull.Value ? row["FECHA_MODIFICACION"].ToString() : "";
                            




                            sucursalEntity.Add(SucursalEntity);

                        }

                        res = true;

                    }
                    else
                    {
                        SucursalEntity.C_Transaccion_Estado = 33;
                        SucursalEntity.C_Transaccion_Mensaje = "No hay registros";
                        sucursalEntity.Add(SucursalEntity);
                        res = false;
                    }

                }
                else
                {

                    SucursalEntity.C_Transaccion_Estado = 32;
                    SucursalEntity.C_Transaccion_Mensaje = msgResEjecucion;
                    sucursalEntity.Add(SucursalEntity);
                    res = false;
                }
            }
            catch (Exception e)
            {
                SucursalEntity.C_Transaccion_Estado = 35;
                SucursalEntity.C_Transaccion_Mensaje = e.Message;
                sucursalEntity.Add(SucursalEntity);
                res = false;
            }
            return res;
        }

        public bool GetSucursalById(ref SucursalEntity sucursalEntity)
        {
         
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_SUCURSAL", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GI");
                objStoreProc.Add_Par_Int_Input("@I_ID_SL", IdSL);

                DataTable datas = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (datas.Rows.Count > 0)
                    {

                        foreach (DataRow row in datas.Rows)
                        {
                           
                            sucursalEntity.C_Id_Sucursal = Convert.ToInt32(row["ID_SL"].ToString());
                            sucursalEntity.C_Id_Municipio = Convert.ToInt32(row["ID_MUN"].ToString());
                            sucursalEntity.C_Direccion = row["DIRECCION"].ToString();
                            sucursalEntity.C_Nombre = row["NOMBRE"].ToString();
                            sucursalEntity.C_Url_Img = row["URL_IMG"].ToString();
                            sucursalEntity.C_Estado = (bool)row["ESTADO"] ? 1 : 2;
                            sucursalEntity.C_Descripcion = row["DESCRIPCION"].ToString();
                            sucursalEntity.C_Usuario_Creacion = row["USUARIO_CREACION"].ToString();
                            sucursalEntity.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"] != DBNull.Value ? row["USUARIO_MODIFICACION"].ToString() : "";
                            sucursalEntity.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                            sucursalEntity.C_Fecha_Modificacion = row["FECHA_MODIFICACION"] != DBNull.Value ? row["FECHA_MODIFICACION"].ToString() : "";





                           

                        }

                        res = true;

                    }
                    else
                    {
                        sucursalEntity.C_Transaccion_Estado = 33;
                        sucursalEntity.C_Transaccion_Mensaje = "No hay registros";
                      
                        res = false;
                    }

                }
                else
                {

                    sucursalEntity.C_Transaccion_Estado = 32;
                    sucursalEntity.C_Transaccion_Mensaje = msgResEjecucion;
                    
                    res = false;
                }
            }
            catch (Exception e)
            {
                sucursalEntity.C_Transaccion_Estado = 35;
                sucursalEntity.C_Transaccion_Mensaje = e.Message;
               
                res = false;
            }
            return res;
        }
    }
    
}
