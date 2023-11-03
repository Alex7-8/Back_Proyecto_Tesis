﻿using DataLayer.Conexion;
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

namespace LogicLayer.Categoria
{





    public class Categoria
    {



		private int idC;
        private string idUM;
        private string razon;
        private int IdC;



        public Categoria()
        {
        }

        public Categoria(int idC, string idUM, string razon)
        {
            this.idC = idC;
            this.idUM = idUM;
            this.razon = razon;
        }

        public Categoria(int IdC)
        {
            this.IdC = IdC;
        }

        public bool SetCategoria(ref CategoriaEntity categoriaEntity)
        {

            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_CATEGORIA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GC");
                objStoreProc.Add_Par_Int_Input("@I_ID_CATEGORIA", 0);
                objStoreProc.Add_Par_VarChar_Input("@I_NOMBRE", categoriaEntity.C_Nombre_Categoria);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", categoriaEntity.C_Usuario_Creacion);
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
                        categoriaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        categoriaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }
                    else
                    {
                        categoriaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        categoriaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;
                        res = false;
                    }
                }

                else
                {
                    categoriaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    categoriaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                categoriaEntity.C_Transaccion_Estado = 26;
                categoriaEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool CambiarEstadoCategoria(ref CategoriaEntity categoriaEntity)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_CATEGORIA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "CC");
                objStoreProc.Add_Par_Int_Input("@I_ID_CATEGORIA", idC);
         
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
                        categoriaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        categoriaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }
                    else
                    {
                        categoriaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        categoriaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;
                        res = false;
                    }
                }

                else
                {
                    categoriaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    categoriaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                categoriaEntity.C_Transaccion_Estado = 26;
                categoriaEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool PutCategoria(ref CategoriaEntity categoriaEntity)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_CATEGORIA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "AC");
                objStoreProc.Add_Par_Int_Input("@I_ID_CATEGORIA", categoriaEntity.C_Id_Categoria);
                objStoreProc.Add_Par_VarChar_Input("@I_NOMBRE", categoriaEntity.C_Nombre_Categoria);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", categoriaEntity.C_Descripcion);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_MODIFICACION", categoriaEntity.C_Usuario_Modificacion);
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
                        categoriaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        categoriaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }
                    else
                    {
                        categoriaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        categoriaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;
                        res = false;
                    }
                }

                else
                {
                    categoriaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    categoriaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                categoriaEntity.C_Transaccion_Estado = 26;
                categoriaEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool GetCategorias(ref List<CategoriaEntity> categoriaEntity)
        {
            DataLayer.EntityModel.CategoriaEntity CategoriaEntity = new DataLayer.EntityModel.CategoriaEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_CATEGORIA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "G");
                objStoreProc.Add_Par_Int_Input("@I_ID_CATEGORIA", 0);
              

                DataTable datas = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (datas.Rows.Count > 0)
                    {

                        foreach (DataRow row in datas.Rows)
                        {
                            CategoriaEntity = new DataLayer.EntityModel.CategoriaEntity();
                            CategoriaEntity.C_Id_Categoria = Convert.ToInt32(row["ID_CAT"].ToString());
                            CategoriaEntity.C_Nombre_Categoria = row["NOMBRE"].ToString();
                            CategoriaEntity.C_Descripcion = row["DESCRIPCION"].ToString();
                            CategoriaEntity.C_Usuario_Creacion = row["USUARIO_CREACION"].ToString();
                            CategoriaEntity.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"] != DBNull.Value ? row["USUARIO_MODIFICACION"].ToString() : "";
                            CategoriaEntity.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                            CategoriaEntity.C_Fecha_Modificacion = row["FECHA_MODIFICACION"] != DBNull.Value ? row["FECHA_MODIFICACION"].ToString() : "";
                            CategoriaEntity.C_Estado = (bool)row["ESTADO"] ? 1 : 2;




                            categoriaEntity.Add(CategoriaEntity);

                        }

                        res = true;

                    }
                    else
                    {
                        CategoriaEntity.C_Transaccion_Estado = 33;
                        CategoriaEntity.C_Transaccion_Mensaje = "No hay registros";
                        categoriaEntity.Add(CategoriaEntity);
                        res = false;
                    }

                }
                else
                {

                    CategoriaEntity.C_Transaccion_Estado = 32;
                    CategoriaEntity.C_Transaccion_Mensaje = msgResEjecucion;
                    categoriaEntity.Add(CategoriaEntity);
                    res = false;
                }
            }
            catch (Exception e)
            {
                CategoriaEntity.C_Transaccion_Estado = 35;
                CategoriaEntity.C_Transaccion_Mensaje = e.Message;
                categoriaEntity.Add(CategoriaEntity);
                res = false;
            }
            return res;
        }

        public bool GetCategoriaById(ref CategoriaEntity categoriaEntity)
        {

            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_CATEGORIA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "ID");
                objStoreProc.Add_Par_Int_Input("@I_ID_CATEGORIA", IdC);
       

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (data.Rows.Count > 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            categoriaEntity = new DataLayer.EntityModel.CategoriaEntity();
                            categoriaEntity.C_Id_Categoria = Convert.ToInt32(row["ID_CAT"].ToString());
                            categoriaEntity.C_Nombre_Categoria = row["NOMBRE"].ToString();
                            categoriaEntity.C_Descripcion = row["DESCRIPCION"].ToString();
                            categoriaEntity.C_Usuario_Creacion = row["USUARIO_CREACION"].ToString();
                            categoriaEntity.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"] != DBNull.Value ? row["USUARIO_MODIFICACION"].ToString() : "";
                            categoriaEntity.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                            categoriaEntity.C_Fecha_Modificacion = row["FECHA_MODIFICACION"] != DBNull.Value ? row["FECHA_MODIFICACION"].ToString() : "";
                            categoriaEntity.C_Estado = (bool)row["ESTADO"] ? 1 : 2;




                            

                        }

                        res = true;

                    }
                    else
                    {
                        categoriaEntity.C_Transaccion_Estado = 33;
                        categoriaEntity.C_Transaccion_Mensaje = "No hay registros";
                        
                        res = false;
                    }

                }
                else
                {

                    categoriaEntity.C_Transaccion_Estado = 32;
                    categoriaEntity.C_Transaccion_Mensaje = msgResEjecucion;
                  
                    res = false;
                }
            }
            catch (Exception e)
            {
                categoriaEntity.C_Transaccion_Estado = 35;
                categoriaEntity.C_Transaccion_Mensaje = e.Message;
                
                res = false;
            }
            return res;
        }



    }
    
}
