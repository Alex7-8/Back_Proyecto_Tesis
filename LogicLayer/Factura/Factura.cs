using DataLayer.Conexion;
using DataLayer.EntityModel;
using System.Text.Json.Serialization;

using Microsoft.Extensions.Configuration;

using System.Data;
namespace LogicLayer.Factura
{
    public class Factura
    {


		private int idSerie;
        private string idUM;
        private string razon;
        private int IdSerie;
        private int idSL;
        private int idPersona;

        public Factura()
        {
        }

        public Factura(int idC, string idUM, string razon)
        {
            idSerie = idC;
            this.idUM = idUM;
            this.razon = razon;
        }

        public Factura(int IdC)
        {
            IdSerie = IdC;
        }

        public Factura(int IdC, int idSL, int idPersona)
        {
            IdSerie = IdC;
            this.idSL = idSL;
            this.idPersona = idPersona;
        }

        public Factura(int IdC, int idSL) 
        {
            this.idSL = idSL;
            idSerie = IdC;
        }


        public bool SetFacturaVenta(ref FacturaEntity facturaEntity)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_FACTURA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "G");
                objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", facturaEntity.C_Id_Persona);
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", facturaEntity.C_Id_Sucursal);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_SERVICIO", string.Join(";", facturaEntity.C_Id_Servicio));
                objStoreProc.Add_Par_Int_Input("@I_ID_SERIE", facturaEntity.C_Id_Serie);
                objStoreProc.Add_Par_Int_Input("@I_ID_ESTADO", facturaEntity.C_Id_Estado);
                objStoreProc.Add_Par_Int_Input("@I_ID_DTE_FACTURA", facturaEntity.C_Id_DTE_Factura);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_PRODUCTO", string.Join(";", facturaEntity.C_Id_Producto));
                objStoreProc.Add_Par_VarChar_Input("@I_CANTIDAD", string.Join(";", facturaEntity.C_Cantidad));
                objStoreProc.Add_Par_VarChar_Input("@I_PRECIO", string.Join(";", facturaEntity.C_Precio));
                objStoreProc.Add_Par_VarChar_Input("@I_SUBTOTAL", string.Join(";", facturaEntity.C_SubTotal));
                objStoreProc.Add_Par_VarChar_Input("@I_IVA", string.Join(";", facturaEntity.C_IVA));
                objStoreProc.Add_Par_VarChar_Input("@I_TOTAL", string.Join(";", facturaEntity.C_Total));
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_NUMERO_AUTORIZACION", facturaEntity.C_NumeroAutorizacionFactura_Compra);
                objStoreProc.Add_Par_VarChar_Input("@I_NUMERO_SERIE", facturaEntity.C_NumeroSerieFactura_Compra);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG ", facturaEntity.C_IMG_Factura_Compra);
                objStoreProc.Add_Par_VarChar_Input("@I_VALIDAR", facturaEntity.C_Validar);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", facturaEntity.C_Usuario_Creacion);
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


                        facturaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        facturaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }
                    else
                    {
                        facturaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        facturaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = false;
                    }

                }

                else
                {
                    facturaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    facturaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                facturaEntity.C_Transaccion_Estado = 26;
                facturaEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool GetFacturaVentaCliente(ref FacturaEntity facturaEntity)
        {

            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_FACTURA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GC");
                objStoreProc.Add_Par_Int_Input("@I_ID_Serie", IdSerie);
                objStoreProc.Add_Par_Int_Input("@I_ID_SL", idSL);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", idPersona);
                objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", 0);

                DataTable datas = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (datas.Rows.Count > 0)
                    {

                        foreach (DataRow row in datas.Rows)
                        {

                            facturaEntity.C_Nombre_Cliente = row["NOMBRE"].ToString();
                            facturaEntity.C_Departamento = row["NOMBRE_DEPARTAMENTO"].ToString();
                            facturaEntity.C_Municipio = row["NOMBRE_MUNICIPIO"].ToString();
                            facturaEntity.C_Direccion = row["DIRECCION"].ToString();
                            facturaEntity.C_Telefono = row["PTELEFONO"].ToString();
                            facturaEntity.C_NIT = row["NIT"].ToString();
                            facturaEntity.C_Numero_Serie = row["NUMERO_SERIE"].ToString();
                            facturaEntity.C_Numero_Factura = row["NUMERO_FACTURA"].ToString();
                            facturaEntity.C_Nombre_Sucursal = row["NOMBRE_SUCURSAL"].ToString();
                            facturaEntity.C_Departamento_Sucursal = row["DEPARTAMENTO_SUCURSAL"].ToString();
                            facturaEntity.C_Municipio_Sucursal = row["MUNICIPIO_SUCURSAL"].ToString();
                            facturaEntity.C_Direccion_Sucursal = row["DIRECCION_SUCURSAL"].ToString();
                            facturaEntity.C_Correo_Sucursal = row["CORREO_SUCURSAL"].ToString();
                            facturaEntity.C_Numero_Telefono = row["TELEFONO_SUCURSAL"].ToString();
                            facturaEntity.C_NIT_Sucursal = row["NIT_SUCURSAL"].ToString();
                            facturaEntity.C_IMG_Sucursal = row["IMG_SUCURSAL"].ToString();
                        }

                        res = true;

                    }
                    else
                    {
                        facturaEntity.C_Transaccion_Estado = 33;
                        facturaEntity.C_Transaccion_Mensaje = "No hay registros";

                        res = false;
                    }

                }
                else
                {

                    facturaEntity.C_Transaccion_Estado = 32;
                    facturaEntity.C_Transaccion_Mensaje = msgResEjecucion;

                    res = false;
                }
            }
            catch (Exception e)
            {
                facturaEntity.C_Transaccion_Estado = 35;
                facturaEntity.C_Transaccion_Mensaje = e.Message;

                res = false;
            }
            return res;
        }

        public bool GetFacturaVentaCF(ref FacturaEntity facturaEntity)
        {

            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_FACTURA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "CF");
                objStoreProc.Add_Par_Int_Input("@I_ID_Serie", idSerie);
                objStoreProc.Add_Par_Int_Input("@I_ID_SL", idSL);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", 0);

                DataTable datas = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (datas.Rows.Count > 0)
                    {

                        foreach (DataRow row in datas.Rows)
                        {

                            facturaEntity.C_Nombre_Sucursal = row["NOMBRE_SUCURSAL"].ToString();
                            facturaEntity.C_Departamento_Sucursal = row["DEPARTAMENTO_SUCURSAL"].ToString();
                            facturaEntity.C_Municipio_Sucursal = row["MUNICIPIO_SUCURSAL"].ToString();
                            facturaEntity.C_Direccion_Sucursal = row["DIRECCION_SUCURSAL"].ToString();
                            facturaEntity.C_Correo_Sucursal = row["CORREO_SUCURSAL"].ToString();
                            facturaEntity.C_Numero_Telefono = row["TELEFONO_SUCURSAL"].ToString();
                            facturaEntity.C_NIT_Sucursal = row["NIT_SUCURSAL"].ToString();
                            facturaEntity.C_Numero_Serie = row["NUMERO_SERIE"].ToString();
                            facturaEntity.C_Numero_Factura = row["NUMERO_FACTURA"].ToString();
                            facturaEntity.C_IMG_Sucursal = row["IMG_SUCURSAL"].ToString();
                        }

                        res = true;

                    }
                    else
                    {
                        facturaEntity.C_Transaccion_Estado = 33;
                        facturaEntity.C_Transaccion_Mensaje = "No hay registros";

                        res = false;
                    }

                }
                else
                {

                    facturaEntity.C_Transaccion_Estado = 32;
                    facturaEntity.C_Transaccion_Mensaje = msgResEjecucion;

                    res = false;
                }
            }
            catch (Exception e)
            {
                facturaEntity.C_Transaccion_Estado = 35;
                facturaEntity.C_Transaccion_Mensaje = e.Message;

                res = false;
            }
            return res;
        }

        public bool SetFacturaServicios(ref FacturaEntity facturaEntity)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_FACTURA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "S");
                objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", facturaEntity.C_Id_Persona);
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", facturaEntity.C_Id_Sucursal);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_SERVICIO", string.Join(";", facturaEntity.C_Id_Servicio));
                objStoreProc.Add_Par_Int_Input("@I_ID_SERIE", facturaEntity.C_Id_Serie);
                objStoreProc.Add_Par_Int_Input("@I_ID_ESTADO", facturaEntity.C_Id_Estado);
                objStoreProc.Add_Par_Int_Input("@I_ID_DTE_FACTURA", facturaEntity.C_Id_DTE_Factura);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_PRODUCTO", string.Join(";", facturaEntity.C_Id_Producto));
                objStoreProc.Add_Par_VarChar_Input("@I_CANTIDAD", string.Join(";", facturaEntity.C_Cantidad));
                objStoreProc.Add_Par_VarChar_Input("@I_PRECIO", string.Join(";", facturaEntity.C_Precio));
                objStoreProc.Add_Par_VarChar_Input("@I_SUBTOTAL", string.Join(";", facturaEntity.C_SubTotal));
                objStoreProc.Add_Par_VarChar_Input("@I_IVA", string.Join(";", facturaEntity.C_IVA));
                objStoreProc.Add_Par_VarChar_Input("@I_TOTAL", string.Join(";", facturaEntity.C_Total));
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_NUMERO_AUTORIZACION", facturaEntity.C_NumeroAutorizacionFactura_Compra);
                objStoreProc.Add_Par_VarChar_Input("@I_NUMERO_SERIE", facturaEntity.C_NumeroSerieFactura_Compra);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG ", facturaEntity.C_IMG_Factura_Compra);
                objStoreProc.Add_Par_VarChar_Input("@I_VALIDAR", facturaEntity.C_Validar);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", facturaEntity.C_Usuario_Creacion);
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


                        facturaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        facturaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }
                    else
                    {
                        facturaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        facturaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = false;
                    }

                }

                else
                {
                    facturaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    facturaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                facturaEntity.C_Transaccion_Estado = 26;
                facturaEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool SetFacturaServiciosCProductos(ref FacturaEntity facturaEntity)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_FACTURA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "SP");
                objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", facturaEntity.C_Id_Persona);
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", facturaEntity.C_Id_Sucursal);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_SERVICIO", string.Join(";", facturaEntity.C_Id_Servicio));
                objStoreProc.Add_Par_Int_Input("@I_ID_SERIE", facturaEntity.C_Id_Serie);
                objStoreProc.Add_Par_Int_Input("@I_ID_ESTADO", facturaEntity.C_Id_Estado);
                objStoreProc.Add_Par_Int_Input("@I_ID_DTE_FACTURA", facturaEntity.C_Id_DTE_Factura);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_PRODUCTO", string.Join(";", facturaEntity.C_Id_Producto));
                objStoreProc.Add_Par_VarChar_Input("@I_CANTIDAD", string.Join(";", facturaEntity.C_Cantidad));
                objStoreProc.Add_Par_VarChar_Input("@I_PRECIO", string.Join(";", facturaEntity.C_Precio));
                objStoreProc.Add_Par_VarChar_Input("@I_SUBTOTAL", string.Join(";", facturaEntity.C_SubTotal));
                objStoreProc.Add_Par_VarChar_Input("@I_IVA", string.Join(";", facturaEntity.C_IVA));
                objStoreProc.Add_Par_VarChar_Input("@I_TOTAL", string.Join(";", facturaEntity.C_Total));
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_NUMERO_AUTORIZACION", facturaEntity.C_NumeroAutorizacionFactura_Compra);
                objStoreProc.Add_Par_VarChar_Input("@I_NUMERO_SERIE", facturaEntity.C_NumeroSerieFactura_Compra);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG ", facturaEntity.C_IMG_Factura_Compra);
                objStoreProc.Add_Par_VarChar_Input("@I_VALIDAR", facturaEntity.C_Validar);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", facturaEntity.C_Usuario_Creacion);
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


                        facturaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        facturaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }
                    else
                    {
                        facturaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        facturaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = false;
                    }

                }

                else
                {
                    facturaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    facturaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                facturaEntity.C_Transaccion_Estado = 26;
                facturaEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        //public bool SetFacturaCompra(ref FacturaEntity facturaEntity)
        //{
        //    bool res = false;


        //    try
        //    {


        //        IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        //        /*Validar usuario en BD*/
        //        EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_FACTURA", "", "");

        //        objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "FP");
        //        objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", 0);
        //        objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", facturaEntity.C_Id_Persona);
        //        objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", facturaEntity.C_Id_Sucursal);
        //        objStoreProc.Add_Par_VarChar_Input("@I_ID_SERVICIO", string.Join(";", facturaEntity.C_Id_Servicio));
        //        objStoreProc.Add_Par_Int_Input("@I_ID_SERIE", facturaEntity.C_Id_Serie);
        //        objStoreProc.Add_Par_Int_Input("@I_ID_ESTADO", facturaEntity.C_Id_Estado);
        //        objStoreProc.Add_Par_Int_Input("@I_ID_DTE_FACTURA", facturaEntity.C_Id_DTE_Factura);
        //        objStoreProc.Add_Par_VarChar_Input("@I_ID_PRODUCTO", string.Join(";", facturaEntity.C_Id_Producto));
        //        objStoreProc.Add_Par_VarChar_Input("@I_CANTIDAD", string.Join(";", facturaEntity.C_Cantidad));
        //        objStoreProc.Add_Par_VarChar_Input("@I_PRECIO", string.Join(";", facturaEntity.C_Precio));
        //        objStoreProc.Add_Par_VarChar_Input("@I_SUBTOTAL", string.Join(";", facturaEntity.C_SubTotal));
        //        objStoreProc.Add_Par_VarChar_Input("@I_IVA", string.Join(";", facturaEntity.C_IVA));
        //        objStoreProc.Add_Par_VarChar_Input("@I_TOTAL", string.Join(";", facturaEntity.C_Total));
        //        objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", null);
        //        objStoreProc.Add_Par_VarChar_Input("@I_NUMERO_AUTORIZACION", facturaEntity.C_NumeroAutorizacionFactura_Compra);
        //        objStoreProc.Add_Par_VarChar_Input("@I_NUMERO_SERIE", facturaEntity.C_NumeroSerieFactura_Compra);
        //        objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG ", facturaEntity.C_IMG_Factura_Compra);
        //        objStoreProc.Add_Par_VarChar_Input("@I_VALIDAR", facturaEntity.C_Validar);
        //        objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", facturaEntity.C_Usuario_Creacion);
        //        objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_MODIFICACION", null);
        //        objStoreProc.Add_Par_Int_Output("@O_TRANSACCION_ESTADO");
        //        objStoreProc.Add_Par_VarChar_Output("@O_TRANSACCION_MENSAJE", 200);



        //        DataTable data = new DataTable();
        //        string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


        //        int O_TRANSACCION_ESTADO = 0;
        //        string O_TRANSACCION_MENSAJE = "";




        //        if (string.IsNullOrEmpty(msgResEjecucion))
        //        {

        //            O_TRANSACCION_MENSAJE = (string)objStoreProc.obtenerValorParametroOutput("@O_TRANSACCION_MENSAJE");
        //            O_TRANSACCION_ESTADO = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@O_TRANSACCION_ESTADO"));



        //            if (O_TRANSACCION_ESTADO == 0)
        //            {


        //                facturaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
        //                facturaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

        //                res = true;
        //            }
        //            else
        //            {
        //                facturaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
        //                facturaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

        //                res = false;
        //            }

        //        }

        //        else
        //        {
        //            facturaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
        //            facturaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
        //            res = false;
        //        }

        //    }

        //    catch (Exception e)
        //    {

        //        facturaEntity.C_Transaccion_Estado = 26;
        //        facturaEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
        //        return false;

        //    }

        //    return res;
        //}

        public bool GetFacturaVentaByDia(ref List<FacturaEntity> facturaEntity)
        {
            DataLayer.EntityModel.FacturaEntity FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_FACTURA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GF");
                objStoreProc.Add_Par_Int_Input("@I_ID_Serie", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_SL", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", idPersona);
                objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", 0);

                DataTable datas = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (datas.Rows.Count > 0)
                    {

                        foreach (DataRow row in datas.Rows)
                        {
							FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
							FacturaEntity.C_Id_Factura = Convert.ToInt32(row["ID_FACTURA_VENTA"].ToString());
                            FacturaEntity.C_Total = row["TOTAL"].ToString();

                            if (row["CLIENTE"] != "")
                            {
                                FacturaEntity.C_Nombre_Cliente = row["CLIENTE"].ToString();
                            }
                            else
                            {
                                FacturaEntity.C_Nombre_Cliente = "C/F";
                            }

                            FacturaEntity.C_Nombre_Sucursal = row["SUCURSAL"].ToString();
                            FacturaEntity.C_Numero_Serie = row["SERIE"].ToString();
                            FacturaEntity.C_Nombre_Estado = row["ESTADO"].ToString();
                            FacturaEntity.C_Id_Estado_Factura = Convert.ToInt32(row["IDESTADO"].ToString());
                            FacturaEntity.C_Nombre_Sucursal = row["SUCURSAL"].ToString();
                            FacturaEntity.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                            FacturaEntity.C_Fecha_Modificacion = row["FECHA_MODIFICACION"] != DBNull.Value ? row["FECHA_MODIFICACION"].ToString() : "";
                            FacturaEntity.C_Usuario_Creacion = row["USUARIO_CREACION"] != DBNull.Value ? row["USUARIO_CREACION"].ToString() : "";
                            FacturaEntity.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"] != DBNull.Value ? row["USUARIO_MODIFICACION"].ToString() : "";


                            facturaEntity.Add(FacturaEntity);
                        }

                        res = true;

                    }
                    else
                    {
                        FacturaEntity.C_Transaccion_Estado = 33;
                        FacturaEntity.C_Transaccion_Mensaje = "No hay registros";
                        facturaEntity.Add(FacturaEntity);

                        res = false;
                    }

                }
                else
                {

                    FacturaEntity.C_Transaccion_Estado = 32;
                    FacturaEntity.C_Transaccion_Mensaje = msgResEjecucion;
                    facturaEntity.Add(FacturaEntity);

                    res = false;
                }
            }
            catch (Exception e)
            {
                FacturaEntity.C_Transaccion_Estado = 35;
                FacturaEntity.C_Transaccion_Mensaje = e.Message;
                facturaEntity.Add(FacturaEntity);

                res = false;
            }
            return res;

        }

        public bool GetFacturaServiciosByDia(ref List<FacturaEntity> facturaEntity)
        {
            DataLayer.EntityModel.FacturaEntity FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_FACTURA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GS");
                objStoreProc.Add_Par_Int_Input("@I_ID_Serie", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_SL", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", idPersona);
                objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", 0);

                DataTable datas = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (datas.Rows.Count > 0)
                    {

                        foreach (DataRow row in datas.Rows)
                        {
							FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
							FacturaEntity.C_Id_Factura = Convert.ToInt32(row["ID_FACTURA_VENTA"].ToString());
                            FacturaEntity.C_Total = row["TOTAL"].ToString();

                            if (row["CLIENTE"] != "")
                            {
                                FacturaEntity.C_Nombre_Cliente = row["CLIENTE"].ToString();
                            }
                            else
                            {
                                FacturaEntity.C_Nombre_Cliente = "C/F";
                            }

                            FacturaEntity.C_Nombre_Sucursal = row["SUCURSAL"].ToString();
                            FacturaEntity.C_Numero_Serie = row["SERIE"].ToString();
                            FacturaEntity.C_Nombre_Estado = row["ESTADO"].ToString();
                            FacturaEntity.C_Id_Estado_Factura = Convert.ToInt32(row["IDESTADO"].ToString());
                            FacturaEntity.C_Nombre_Sucursal = row["SUCURSAL"].ToString();
                            FacturaEntity.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                            FacturaEntity.C_Fecha_Modificacion = row["FECHA_MODIFICACION"] != DBNull.Value ? row["FECHA_MODIFICACION"].ToString() : "";
                            FacturaEntity.C_Usuario_Creacion = row["USUARIO_CREACION"] != DBNull.Value ? row["USUARIO_CREACION"].ToString() : "";
                            FacturaEntity.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"] != DBNull.Value ? row["USUARIO_MODIFICACION"].ToString() : "";


                            facturaEntity.Add(FacturaEntity);
                        }

                        res = true;

                    }
                    else
                    {
                        FacturaEntity.C_Transaccion_Estado = 33;
                        FacturaEntity.C_Transaccion_Mensaje = "No hay registros";
                        facturaEntity.Add(FacturaEntity);

                        res = false;
                    }

                }
                else
                {

                    FacturaEntity.C_Transaccion_Estado = 32;
                    FacturaEntity.C_Transaccion_Mensaje = msgResEjecucion;
                    facturaEntity.Add(FacturaEntity);

                    res = false;
                }
            }
            catch (Exception e)
            {
                FacturaEntity.C_Transaccion_Estado = 35;
                FacturaEntity.C_Transaccion_Mensaje = e.Message;
                facturaEntity.Add(FacturaEntity);

                res = false;
            }
            return res;

        }

        public bool GetGananciasByDia(ref FacturaEntity facturaEntity)
        {
        
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_FACTURA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GD");
                objStoreProc.Add_Par_Int_Input("@I_ID_Serie", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_SL", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", 0);

                DataTable datas = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                 

                    if (datas.Rows.Count > 0)
                    {

                        foreach (DataRow row in datas.Rows)
                        {

							facturaEntity.C_Ventas =row["PVENTA"] != DBNull.Value ? row["PVENTA"].ToString() : "N/A";
							facturaEntity.C_Compras = row["PCOMPRA"] != DBNull.Value ? row["PCOMPRA"].ToString() : "N/A";
							facturaEntity.C_Ganancias = row["GANANCIA"] != DBNull.Value ? row["GANANCIA"].ToString() : "N/A";



                        }

                        res = true;

                    }
                    else
                    {
						facturaEntity.C_Transaccion_Estado = 33;
						facturaEntity.C_Transaccion_Mensaje = "No hay registros";
                      

                        res = false;
                    }

                }
                else
                {

					facturaEntity.C_Transaccion_Estado = 32;
					facturaEntity.C_Transaccion_Mensaje = msgResEjecucion;
                  

                    res = false;
                }
            }
            catch (Exception e)
            {
				facturaEntity.C_Transaccion_Estado = 35;
				facturaEntity.C_Transaccion_Mensaje = e.Message;
              

                res = false;
            }
            return res;
        }

        public bool GetGananciasBySemana(ref FacturaEntity facturaEntity)
        {
           
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_FACTURA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "SG");
                objStoreProc.Add_Par_Int_Input("@I_ID_Serie", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_SL", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", 0);

                DataTable datas = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (datas.Rows.Count > 0)
                    {

                        foreach (DataRow row in datas.Rows)
                        {

							facturaEntity.C_Ventas = row["PVENTA"] != DBNull.Value ? row["PVENTA"].ToString() : "N/A";
							facturaEntity.C_Compras = row["PCOMPRA"] != DBNull.Value ? row["PCOMPRA"].ToString() : "N/A";
							facturaEntity.C_Ganancias = row["GANANCIA"] != DBNull.Value ? row["GANANCIA"].ToString() : "N/A";



                          
                        }

                        res = true;

                    }
                    else
                    {
                        facturaEntity.C_Transaccion_Estado = 33;
                        facturaEntity.C_Transaccion_Mensaje = "No hay registros";
                     

                        res = false;
                    }

                }
                else
                {

					facturaEntity.C_Transaccion_Estado = 32;
					facturaEntity.C_Transaccion_Mensaje = msgResEjecucion;
                 

                    res = false;
                }
            }
            catch (Exception e)
            {
				facturaEntity.C_Transaccion_Estado = 35;
				facturaEntity.C_Transaccion_Mensaje = e.Message;
               

                res = false;
            }
            return res;
        }

        public bool GetGananciasByMes(ref FacturaEntity facturaEntity)
        {
           
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_FACTURA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GM");
                objStoreProc.Add_Par_Int_Input("@I_ID_Serie", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_SL", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", 0);

                DataTable datas = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                   

                    if (datas.Rows.Count > 0)
                    {

                        foreach (DataRow row in datas.Rows)
                        {

							facturaEntity.C_Ventas = row["PVENTA"] != DBNull.Value ? row["PVENTA"].ToString() : "N/A";
							facturaEntity.C_Compras = row["PCOMPRA"] != DBNull.Value ? row["PCOMPRA"].ToString() : "N/A";
							facturaEntity.C_Ganancias = row["GANANCIA"] != DBNull.Value ? row["GANANCIA"].ToString() : "N/A";



                          
                        }

                        res = true;

                    }
                    else
                    {
						facturaEntity.C_Transaccion_Estado = 33;
						facturaEntity.C_Transaccion_Mensaje = "No hay registros";
                      

                        res = false;
                    }

                }
                else
                {

					facturaEntity.C_Transaccion_Estado = 32;
					facturaEntity.C_Transaccion_Mensaje = msgResEjecucion;
                   

                    res = false;
                }
            }
            catch (Exception e)
            {
				facturaEntity.C_Transaccion_Estado = 35;
				facturaEntity.C_Transaccion_Mensaje = e.Message;
        

                res = false;
            }
            return res;
        }

        public bool GetGananciasByAnio(ref FacturaEntity facturaEntity)
        {
            
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_FACTURA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GN");
                objStoreProc.Add_Par_Int_Input("@I_ID_Serie", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_SL", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", 0);

                DataTable datas = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (datas.Rows.Count > 0)
                    {

                        foreach (DataRow row in datas.Rows)
                        {

							facturaEntity.C_Ventas = row["PVENTA"] != DBNull.Value ? row["PVENTA"].ToString() : "N/A";
							facturaEntity.C_Compras = row["PCOMPRA"] != DBNull.Value ? row["PCOMPRA"].ToString() : "N/A";
							facturaEntity.C_Ganancias = row["GANANCIA"] != DBNull.Value ? row["GANANCIA"].ToString() : "N/A";



                          
                        }

                        res = true;

                    }
                    else
                    {
						facturaEntity.C_Transaccion_Estado = 33;
						facturaEntity.C_Transaccion_Mensaje = "No hay registros";
                       

                        res = false;
                    }

                }
                else
                {

					facturaEntity.C_Transaccion_Estado = 32;
					facturaEntity.C_Transaccion_Mensaje = msgResEjecucion;
                 

                    res = false;
                }
            }
            catch (Exception e)
            {
				facturaEntity.C_Transaccion_Estado = 35;
				facturaEntity.C_Transaccion_Mensaje = e.Message;
              

                res = false;
            }
            return res;
        }


        

        public bool GetDetalleFacturaById(ref List<FacturaEntity> facturaEntity)
        {
            DataLayer.EntityModel.FacturaEntity FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_FACTURA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "DV");
                objStoreProc.Add_Par_Int_Input("@I_ID_Serie", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_SL", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", IdSerie);


                DataTable datas = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (datas.Rows.Count > 0)
                    {

                        foreach (DataRow row in datas.Rows)
                        {

                            FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
                            FacturaEntity.C_Id_Producto = row["ID_PRODUCTO"].ToString();
                            FacturaEntity.C_Nombre_Producto = row["NOMBRE"].ToString();
                            FacturaEntity.C_Id_Factura = Convert.ToInt32(row["ID_FACTURA_VENTA"].ToString());
                            FacturaEntity.C_Id_Servicio = row["ID_SERVICIO"].ToString();
                            FacturaEntity.C_Cantidad = row["CANTIDAD"].ToString();
                            FacturaEntity.C_Precio = row["PRECIO"].ToString();
                            FacturaEntity.C_SubTotal = row["SUBTOTAL"].ToString();
                            FacturaEntity.C_IVA = row["IVA"].ToString();
                            FacturaEntity.C_Total = row["TOTAL"].ToString();

                         

                            facturaEntity.Add(FacturaEntity);
                        }

                        res = true;

                    }
                    else
                    {
                        FacturaEntity.C_Transaccion_Estado = 33;
                        FacturaEntity.C_Transaccion_Mensaje = "No hay registros";
                        facturaEntity.Add(FacturaEntity);

                        res = false;
                    }

                }
                else
                {

                    FacturaEntity.C_Transaccion_Estado = 32;
                    FacturaEntity.C_Transaccion_Mensaje = msgResEjecucion;
                    facturaEntity.Add(FacturaEntity);

                    res = false;
                }
            }
            catch (Exception e)
            {
                FacturaEntity.C_Transaccion_Estado = 35;
                FacturaEntity.C_Transaccion_Mensaje = e.Message;
                facturaEntity.Add(FacturaEntity);

                res = false;
            }
            return res;
        }

		public bool GetDetalleFacturaServicioById(ref List<FacturaEntity> facturaEntity)
		{
			DataLayer.EntityModel.FacturaEntity FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
			bool res = false;
			try
			{
				IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				/*Validar usuario en BD*/
				EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_FACTURA", "", "");
				objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "DS");
				objStoreProc.Add_Par_Int_Input("@I_ID_Serie", 0);
				objStoreProc.Add_Par_Int_Input("@I_ID_SL", 0);
				objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
				objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", IdSerie);


				DataTable datas = new DataTable();
				string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


				if (string.IsNullOrEmpty(msgResEjecucion))
				{
					List<string> listempty = new List<string>();

					if (datas.Rows.Count > 0)
					{

						foreach (DataRow row in datas.Rows)
						{

							FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
							FacturaEntity.C_Nombre_Servicio = row["NOMBRE"].ToString();
							FacturaEntity.C_Id_Factura = Convert.ToInt32(row["ID_FACTURA_VENTA"].ToString());
							FacturaEntity.C_Id_Servicio = row["ID_SERVICIO"].ToString();
							FacturaEntity.C_Cantidad = row["CANTIDAD"].ToString();
							FacturaEntity.C_Precio = row["PRECIO"].ToString();
							FacturaEntity.C_SubTotal = row["SUBTOTAL"].ToString();
							FacturaEntity.C_IVA = row["IVA"].ToString();
							FacturaEntity.C_Total = row["TOTAL"].ToString();



							facturaEntity.Add(FacturaEntity);
						}

						res = true;

					}
					else
					{
						FacturaEntity.C_Transaccion_Estado = 33;
						FacturaEntity.C_Transaccion_Mensaje = "No hay registros";
						facturaEntity.Add(FacturaEntity);

						res = false;
					}

				}
				else
				{

					FacturaEntity.C_Transaccion_Estado = 32;
					FacturaEntity.C_Transaccion_Mensaje = msgResEjecucion;
					facturaEntity.Add(FacturaEntity);

					res = false;
				}
			}
			catch (Exception e)
			{
				FacturaEntity.C_Transaccion_Estado = 35;
				FacturaEntity.C_Transaccion_Mensaje = e.Message;
				facturaEntity.Add(FacturaEntity);

				res = false;
			}
			return res;
		}

		

		public bool GetGananciasSBySemana(ref FacturaEntity facturaEntity)
		{
			
			bool res = false;
			try
			{
				IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				/*Validar usuario en BD*/
				EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_FACTURA", "", "");
				objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "CG");
				objStoreProc.Add_Par_Int_Input("@I_ID_Serie", 0);
				objStoreProc.Add_Par_Int_Input("@I_ID_SL", 0);
				objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
				objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", 0);

				DataTable datas = new DataTable();
				string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


				if (string.IsNullOrEmpty(msgResEjecucion))
				{
					List<string> listempty = new List<string>();

					if (datas.Rows.Count > 0)
					{

						foreach (DataRow row in datas.Rows)
						{


							facturaEntity.C_Ganancias = row["GANANCIA"] != DBNull.Value ? row["GANANCIA"].ToString() : "N/A";



						}

						res = true;

					}
					else
					{
						facturaEntity.C_Transaccion_Estado = 33;
						facturaEntity.C_Transaccion_Mensaje = "No hay registros";
					

						res = false;
					}

				}
				else
				{

					facturaEntity.C_Transaccion_Estado = 32;
					facturaEntity.C_Transaccion_Mensaje = msgResEjecucion;
					

					res = false;
				}
			}
			catch (Exception e)
			{
				facturaEntity.C_Transaccion_Estado = 35;
				facturaEntity.C_Transaccion_Mensaje = e.Message;
				

				res = false;
			}
			return res;
		}

		public bool GetGananciasSByMes(ref FacturaEntity facturaEntity)
		{
			
			bool res = false;
			try
			{
				IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				/*Validar usuario en BD*/
				EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_FACTURA", "", "");
				objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "CM");
				objStoreProc.Add_Par_Int_Input("@I_ID_Serie", 0);
				objStoreProc.Add_Par_Int_Input("@I_ID_SL", 0);
				objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
				objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", 0);

				DataTable datas = new DataTable();
				string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


				if (string.IsNullOrEmpty(msgResEjecucion))
				{
					List<string> listempty = new List<string>();

					if (datas.Rows.Count > 0)
					{

						foreach (DataRow row in datas.Rows)
						{


							facturaEntity.C_Ganancias = row["GANANCIA"] != DBNull.Value ? row["GANANCIA"].ToString() : "N/A";



					
						}

						res = true;

					}
					else
					{
						facturaEntity.C_Transaccion_Estado = 33;
						facturaEntity.C_Transaccion_Mensaje = "No hay registros";
						

						res = false;
					}

				}
				else
				{

					facturaEntity.C_Transaccion_Estado = 32;
					facturaEntity.C_Transaccion_Mensaje = msgResEjecucion;
				

					res = false;
				}
			}
			catch (Exception e)
			{
				facturaEntity.C_Transaccion_Estado = 35;
				facturaEntity.C_Transaccion_Mensaje = e.Message;
				

				res = false;
			}
			return res;
		}

		public bool GetGananciasSByAnio(ref FacturaEntity facturaEntity)
		{
			DataLayer.EntityModel.FacturaEntity FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
			bool res = false;
			try
			{
				IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				/*Validar usuario en BD*/
				EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_FACTURA", "", "");
				objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "CN");
				objStoreProc.Add_Par_Int_Input("@I_ID_Serie", 0);
				objStoreProc.Add_Par_Int_Input("@I_ID_SL", 0);
				objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
				objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", 0);

				DataTable datas = new DataTable();
				string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


				if (string.IsNullOrEmpty(msgResEjecucion))
				{
					List<string> listempty = new List<string>();

					if (datas.Rows.Count > 0)
					{

						foreach (DataRow row in datas.Rows)
						{


							facturaEntity.C_Ganancias = row["GANANCIA"] != DBNull.Value ? row["GANANCIA"].ToString() : "N/A";



							
						}

						res = true;

					}
					else
					{
						facturaEntity.C_Transaccion_Estado = 33;
						facturaEntity.C_Transaccion_Mensaje = "No hay registros";
						

						res = false;
					}

				}
				else
				{

					facturaEntity.C_Transaccion_Estado = 32;
					facturaEntity.C_Transaccion_Mensaje = msgResEjecucion;
				

					res = false;
				}
			}
			catch (Exception e)
			{
				facturaEntity.C_Transaccion_Estado = 35;
				facturaEntity.C_Transaccion_Mensaje = e.Message;
		

				res = false;
			}
			return res;
		}

		public bool GetGananciasSByDia(ref FacturaEntity facturaEntity)
		{
			
			bool res = false;
			try
			{
				IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				/*Validar usuario en BD*/
				EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_FACTURA", "", "");
				objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "CD");
				objStoreProc.Add_Par_Int_Input("@I_ID_Serie", 0);
				objStoreProc.Add_Par_Int_Input("@I_ID_SL", 0);
				objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
				objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", 0);

				DataTable datas = new DataTable();
				string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref datas);


				if (string.IsNullOrEmpty(msgResEjecucion))
				{
					List<string> listempty = new List<string>();

					if (datas.Rows.Count > 0)
					{

						foreach (DataRow row in datas.Rows)
						{

							facturaEntity.C_Ganancias = row["GANANCIA"] != DBNull.Value ? row["GANANCIA"].ToString() : "N/A";



					
						}

						res = true;

					}
					else
					{
						facturaEntity.C_Transaccion_Estado = 33;
						facturaEntity.C_Transaccion_Mensaje = "No hay registros";
					

						res = false;
					}

				}
				else
				{

					facturaEntity.C_Transaccion_Estado = 32;
					facturaEntity.C_Transaccion_Mensaje = msgResEjecucion;
				

					res = false;
				}
			}
			catch (Exception e)
			{
				facturaEntity.C_Transaccion_Estado = 35;
				facturaEntity.C_Transaccion_Mensaje = e.Message;

				res = false;
			}
			return res;
		}

		public bool CambiarEstadoFactura(ref FacturaEntity facturaEntity)
		{
			bool res = false;


			try
			{


				IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				/*Validar usuario en BD*/
				EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_FACTURA", "", "");

				objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "C");
				objStoreProc.Add_Par_Int_Input("@I_ID_FACTURA", idSerie);
				objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", facturaEntity.C_Id_Persona);
				objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", facturaEntity.C_Id_Sucursal);
				objStoreProc.Add_Par_VarChar_Input("@I_ID_SERVICIO", null);
				objStoreProc.Add_Par_Int_Input("@I_ID_SERIE", 0);
				objStoreProc.Add_Par_Int_Input("@I_ID_ESTADO", 0);
				objStoreProc.Add_Par_Int_Input("@I_ID_DTE_FACTURA", 0);
				objStoreProc.Add_Par_VarChar_Input("@I_ID_PRODUCTO", null);
				objStoreProc.Add_Par_VarChar_Input("@I_CANTIDAD", null);
				objStoreProc.Add_Par_VarChar_Input("@I_PRECIO", null);
				objStoreProc.Add_Par_VarChar_Input("@I_SUBTOTAL", null);
				objStoreProc.Add_Par_VarChar_Input("@I_IVA", null);
				objStoreProc.Add_Par_VarChar_Input("@I_TOTAL", null);
				objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", razon);
				objStoreProc.Add_Par_VarChar_Input("@I_NUMERO_AUTORIZACION", null);
				objStoreProc.Add_Par_VarChar_Input("@I_NUMERO_SERIE", null);
				objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG ", null);
				objStoreProc.Add_Par_VarChar_Input("@I_VALIDAR", null);
				objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", facturaEntity.C_Usuario_Creacion);
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


						facturaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
						facturaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

						res = true;
					}
					else
					{
						facturaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
						facturaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

						res = false;
					}

				}

				else
				{
					facturaEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
					facturaEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
					res = false;
				}

			}

			catch (Exception e)
			{

				facturaEntity.C_Transaccion_Estado = 26;
				facturaEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
				return false;

			}

			return res;
		}
	}
    
}
