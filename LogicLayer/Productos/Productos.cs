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

namespace LogicLayer.Productos
{
    public class Productos
    {





		private int idp;
        private string idUM;
        private string razon;
        private int idP;
        private int IdSL;

        public Productos()
        {
        }

        public Productos(int idp, string idUM, string razon)
        {
            this.idp = idp;
            this.idUM = idUM;
            this.razon = razon;
        }

        public Productos(int idp)
        {
            idP = idp;
        }

     

        public bool SetProductos(ref ProductosEntity productosEntity)
        {

            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_PRODUCTO", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GP");
                objStoreProc.Add_Par_VarChar_Input("@I_TIPO_MOV", "");
                objStoreProc.Add_Par_Int_Input("@I_ID_PRODUCTO", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA",0);
                objStoreProc.Add_Par_Int_Input("@I_ID_MARCA", productosEntity.C_Id_Marca);
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", productosEntity.C_Id_Sucursal);
                objStoreProc.Add_Par_Int_Input("@I_ID_CATEGORIA", productosEntity.C_Id_Categoria);
                objStoreProc.Add_Par_VarChar_Input("@I_NOMBRE_PRODUCTO", productosEntity.C_Nombre_Producto);
                objStoreProc.Add_Par_Int_Input("@I_STOCK_DISPONIBLE", productosEntity.C_Stock_Disponible);   
                objStoreProc.Add_Par_Int_Input("@I_CANTIDAD", 0);
                objStoreProc.Add_Par_Decimal_Input("@I_PRECIO_COMPRA", productosEntity.C_Precio_Compra);
                objStoreProc.Add_Par_Decimal_Input("@I_PRECIO_VENTA", productosEntity.C_Precio_Venta);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", productosEntity.C_Url_IMG);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", productosEntity.C_Descripcion);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", productosEntity.C_Usuario_Creacion);
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
                        productosEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        productosEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }
                    else
                    {
                        productosEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        productosEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = false;
                    }
                }

                else
                {
                    productosEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    productosEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                productosEntity.C_Transaccion_Estado = 26;
                productosEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        
    }

        public bool CambiarEstadoProducto(ref ProductosEntity productosEntity)
        {


            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_PRODUCTO", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "CP");
                objStoreProc.Add_Par_VarChar_Input("@I_TIPO_MOV", "");
                objStoreProc.Add_Par_Int_Input("@I_ID_PRODUCTO", idp);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_MARCA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_CATEGORIA", 0);
                objStoreProc.Add_Par_VarChar_Input("@I_NOMBRE_PRODUCTO", "");
                objStoreProc.Add_Par_Int_Input("@I_STOCK_DISPONIBLE", 0);
                objStoreProc.Add_Par_Int_Input("@I_CANTIDAD", 0);
                objStoreProc.Add_Par_Decimal_Input("@I_PRECIO_COMPRA", (decimal)0.00);
                objStoreProc.Add_Par_Decimal_Input("@I_PRECIO_VENTA", (decimal)0.00);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", "");
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
                        productosEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        productosEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }
                    else
                    {
                        productosEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        productosEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = false;
                    }
                }

                else
                {
                    productosEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    productosEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                productosEntity.C_Transaccion_Estado = 26;
                productosEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool PutProductos(ref ProductosEntity productosEntity)
        {


            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_PRODUCTO", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "AP");
                objStoreProc.Add_Par_VarChar_Input("@I_TIPO_MOV", productosEntity.C_Tipo_Mov);
                objStoreProc.Add_Par_Int_Input("@I_ID_PRODUCTO", productosEntity.C_Id_Producto);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_MARCA", productosEntity.C_Id_Marca);
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", productosEntity.C_Id_Sucursal);
                objStoreProc.Add_Par_Int_Input("@I_ID_CATEGORIA", productosEntity.C_Id_Categoria);
                objStoreProc.Add_Par_VarChar_Input("@I_NOMBRE_PRODUCTO", productosEntity.C_Nombre_Producto);
                objStoreProc.Add_Par_Int_Input("@I_STOCK_DISPONIBLE", productosEntity.C_Stock_Disponible);
                objStoreProc.Add_Par_Int_Input("@I_CANTIDAD", productosEntity.C_Cantidad);
                objStoreProc.Add_Par_Decimal_Input("@I_PRECIO_COMPRA", productosEntity.C_Precio_Compra);
                objStoreProc.Add_Par_Decimal_Input("@I_PRECIO_VENTA", productosEntity.C_Precio_Venta);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", productosEntity.C_Url_IMG);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", productosEntity.C_Descripcion);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_MODIFICACION", productosEntity.C_Usuario_Modificacion);
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
                        productosEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        productosEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }
                    else
                    {
                        productosEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        productosEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = false;
                    }
                }

                else
                {
                    productosEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    productosEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                productosEntity.C_Transaccion_Estado = 26;
                productosEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool GetProductos(ref List<ProductosEntity> productosEntity)
        {
            DataLayer.EntityModel.ProductosEntity ProductosEntity = new DataLayer.EntityModel.ProductosEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_PRUDUCTOS", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GA");
                objStoreProc.Add_Par_Int_Input("@I_ID_PRODUCTO", 0);

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (data.Rows.Count > 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            ProductosEntity = new DataLayer.EntityModel.ProductosEntity();
                            ProductosEntity.C_Id_Producto = Convert.ToInt32(row["ID_PRODUCTO"]);
                            ProductosEntity.C_Id_Sucursal = Convert.ToInt32(row["ID_SL"]);
                            ProductosEntity.C_Id_Marca = Convert.ToInt32(row["ID_MARCA"]);
                            ProductosEntity.C_Nombre_Marca = row["NOMBRE_MARCA"].ToString();
                            ProductosEntity.C_Id_Categoria = Convert.ToInt32(row["ID_CAT"]);
                            ProductosEntity.C_Nombre_Producto = row["NOMBRE_PRODUCTO"].ToString();
                            ProductosEntity.C_Descripcion = row["DESCRIPCION"].ToString();
                            ProductosEntity.C_Stock_Disponible = Convert.ToInt32(row["STOCK_DISPONIBLE"]);
                            ProductosEntity.C_Precio_Compra = Convert.ToDecimal(row["PRECIO_COMPRA"]);
                            ProductosEntity.C_Precio_Venta = Convert.ToDecimal(row["PRECIO_VENTA"]);
                            ProductosEntity.C_Url_IMG = row["URL_IMG"].ToString();
                            ProductosEntity.C_Estado = (bool)row["ESTADO"] ? 1 : 2;
                            ProductosEntity.C_Usuario_Creacion = row["USUARIO_CREACION"].ToString();
                            ProductosEntity.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"] != DBNull.Value ? row["USUARIO_MODIFICACION"].ToString() : "";
                            ProductosEntity.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                            ProductosEntity.C_Fecha_Modificacion = row["FECHA_MODIFICACION"] != DBNull.Value ? row["FECHA_MODIFICACION"].ToString() : "";
                           




                            productosEntity.Add(ProductosEntity);

                        }

                        res = true;

                    }
                    else
                    {
                        ProductosEntity.C_Transaccion_Estado = 33;
                        ProductosEntity.C_Transaccion_Mensaje = "No hay registros";
                        productosEntity.Add(ProductosEntity);
                        res = false;
                    }

                }
                else
                {

                    ProductosEntity.C_Transaccion_Estado = 32;
                    ProductosEntity.C_Transaccion_Mensaje = msgResEjecucion;
                    productosEntity.Add(ProductosEntity);
                    res = false;
                }
            }
            catch (Exception e)
            {
                ProductosEntity.C_Transaccion_Estado = 35;
                ProductosEntity.C_Transaccion_Mensaje = e.Message;
                productosEntity.Add(ProductosEntity);
                res = false;
            }
            return res;
        }

        public bool GetProductosById(ref ProductosEntity productosEntity)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_PRUDUCTOS", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GI");
                objStoreProc.Add_Par_Int_Input("@I_ID_PRODUCTO", idP);

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {


                    if (data.Rows.Count > 0)
                    {

                        DataRow row = data.Rows[0];

                       
                        productosEntity.C_Id_Producto = Convert.ToInt32(row["ID_PRODUCTO"]);
                        productosEntity.C_Id_Sucursal = Convert.ToInt32(row["ID_SL"]);
                        productosEntity.C_Id_Marca = Convert.ToInt32(row["ID_MARCA"]);
                        productosEntity.C_Id_Categoria = Convert.ToInt32(row["ID_CAT"]);
                        productosEntity.C_Nombre_Producto = row["NOMBRE_PRODUCTO"].ToString();
                        productosEntity.C_Descripcion = row["DESCRIPCION"].ToString();
                        productosEntity.C_Stock_Disponible = Convert.ToInt32(row["STOCK_DISPONIBLE"]);
                        productosEntity.C_Precio_Compra = Convert.ToDecimal(row["PRECIO_COMPRA"]);
                        productosEntity.C_Precio_Venta = Convert.ToDecimal(row["PRECIO_VENTA"]);
                        productosEntity.C_Url_IMG = row["URL_IMG"].ToString();
                        productosEntity.C_Estado = (bool)row["ESTADO"] ? 1 : 2;
                        productosEntity.C_Usuario_Creacion = row["USUARIO_CREACION"].ToString();
                        productosEntity.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"] != DBNull.Value ? row["USUARIO_MODIFICACION"].ToString() : "";
                        productosEntity.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                        productosEntity.C_Fecha_Modificacion = row["FECHA_MODIFICACION"] != DBNull.Value ? row["FECHA_MODIFICACION"].ToString() : "";






                        res = true;

                    }
                    else
                    {
                        productosEntity.C_Transaccion_Estado = 33;
                        productosEntity.C_Transaccion_Mensaje = "No hay registros";

                        res = false;
                    }

                }
                else
                {

                    productosEntity.C_Transaccion_Estado = 32;
                    productosEntity.C_Transaccion_Mensaje = msgResEjecucion;

                    res = false;
                }
            }
            catch (Exception e)
            {
                productosEntity.C_Transaccion_Estado = 35;
                productosEntity.C_Transaccion_Mensaje = e.Message;
                res = false;
            }
            return res;
        }

        public bool GetProductosFactura(ref List<ProductosEntity> productosEntity)
        {
            DataLayer.EntityModel.ProductosEntity ProductosEntity = new DataLayer.EntityModel.ProductosEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_PRUDUCTOS", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GP");
                objStoreProc.Add_Par_Int_Input("@I_ID_PRODUCTO", idP);

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (data.Rows.Count > 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            ProductosEntity = new DataLayer.EntityModel.ProductosEntity();
                            ProductosEntity.C_Id_Producto = Convert.ToInt32(row["ID_PRODUCTO"]);
                            ProductosEntity.C_Nombre_Producto = row["NOMBRE_PRODUCTO"].ToString();
                            ProductosEntity.C_Url_IMG = row["URL_IMG"].ToString();


                           
                                productosEntity.Add(ProductosEntity);
                             
                         


                      
                              
                          
                        }

                        res = true;

                    }
                    else
                    {
                        ProductosEntity.C_Transaccion_Estado = 33;
                        ProductosEntity.C_Transaccion_Mensaje = "No hay registros";
                        productosEntity.Add(ProductosEntity);
                        res = false;
                    }

                }
                else
                {

                    ProductosEntity.C_Transaccion_Estado = 32;
                    ProductosEntity.C_Transaccion_Mensaje = msgResEjecucion;
                    productosEntity.Add(ProductosEntity);
                    res = false;
                }
            }
            catch (Exception e)
            {
                ProductosEntity.C_Transaccion_Estado = 35;
                ProductosEntity.C_Transaccion_Mensaje = e.Message;
                productosEntity.Add(ProductosEntity);
                res = false;
            }
            return res;
        }

        public bool GetProductosFacturaById(ref ProductosEntity productosEntity)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_PRUDUCTOS", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "PI");
                objStoreProc.Add_Par_Int_Input("@I_ID_PRODUCTO", idP);

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {


                    if (data.Rows.Count > 0)
                    {

                        DataRow row = data.Rows[0];


                        productosEntity.C_Id_Producto = Convert.ToInt32(row["ID_PRODUCTO"]);
                        productosEntity.C_Nombre_Producto = row["NOMBRE_PRODUCTO"].ToString();
                        productosEntity.C_Stock_Disponible = Convert.ToInt32(row["STOCK_DISPONIBLE"]);
                        productosEntity.C_Precio_Venta = Convert.ToDecimal(row["PRECIO_VENTA"]);
                        productosEntity.C_Url_IMG = row["URL_IMG"].ToString();
                       


                        res = true;

                    }
                    else
                    {
                        productosEntity.C_Transaccion_Estado = 33;
                        productosEntity.C_Transaccion_Mensaje = "No hay registros";

                        res = false;
                    }

                }
                else
                {

                    productosEntity.C_Transaccion_Estado = 32;
                    productosEntity.C_Transaccion_Mensaje = msgResEjecucion;

                    res = false;
                }
            }
            catch (Exception e)
            {
                productosEntity.C_Transaccion_Estado = 35;
                productosEntity.C_Transaccion_Mensaje = e.Message;
                res = false;
            }
            return res;
        }
    }
}
