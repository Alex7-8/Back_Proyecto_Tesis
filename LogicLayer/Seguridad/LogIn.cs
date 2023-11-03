
using DataLayer.Conexion;
using DataLayer.EntityModel;
using LogicLayer.Helper;

using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Net.Sockets;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Extensions.Primitives;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;

namespace LogicLayer.Seguridad
{
    public class LogIn
    {






		private string _ID_USUARIO;
        private string _CONTRASENIA;

        public LogIn()
        {
        }

        public LogIn(string I_id_USUARIO, string I_CONTRASENIA)
        {
            _ID_USUARIO = I_id_USUARIO;
            _CONTRASENIA = I_CONTRASENIA;
        }

  

        public bool Estatus(ref LogInEntity logInEntity)
        {
            bool res = false;

            try
            {



                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


                EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_ESTADO_BD", "", "");


                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                int O_TRANSACCION_ESTADO = 0;
                string O_TRANSACCION_MENSAJE = "";
                string Id_Dispositivo = "";



                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    DataRow row = data.Rows[0];

                    logInEntity.C_Transaccion_Estado = 0;
                    logInEntity.C_Transaccion_Mensaje = row["Status"].ToString();
                    res = true;


                }

                else
                {
                    logInEntity.C_Transaccion_Estado = 11;
                    logInEntity.C_Transaccion_Mensaje = msgResEjecucion;
                    res = false;
                }


            }
            catch (Exception e)
            {

                logInEntity.C_Transaccion_Estado = 15;
                logInEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool IniciarSesion(ref LogInEntity logInEntity)
        {


            bool res = false;

            try
            {

                //validaciones
                if (logInEntity.C_Contrasenia.Trim() == "")
                {
                    logInEntity.C_Transaccion_Estado = 10;
                    logInEntity.C_Transaccion_Mensaje = "La contraseña no puede estar en blanco";
                    return false;
                }

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                var root = builder.Build();
                string token = "";
                var secret = root.GetValue<string>("AppConfig:MySecret");
                //if (secret != null)
                //{
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_LOGIN", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "I");
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", _ID_USUARIO);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA", _CONTRASENIA);
                objStoreProc.Add_Par_VarChar_Input("@I_DIRECCION_IP", logInEntity.C_Direccion_IP);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_DISPOSITIVO", logInEntity.C_Id_Dispositivo);
                objStoreProc.Add_Par_VarChar_Input("@I_TIPO_DISPOSITIVO", logInEntity.C_Tipo_Dispositivo);

                objStoreProc.Add_Par_Int_Output("@O_TRANSACCION_ESTADO");
                objStoreProc.Add_Par_VarChar_Output("@O_TRANSACCION_MENSAJE", 200);



                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                int O_TRANSACCION_ESTADO = 0;
                string O_TRANSACCION_MENSAJE = "";
                string Id_Dispositivo = "";



                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    O_TRANSACCION_MENSAJE = (string)objStoreProc.obtenerValorParametroOutput("@O_TRANSACCION_MENSAJE");
                    O_TRANSACCION_ESTADO = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@O_TRANSACCION_ESTADO"));

                    if (O_TRANSACCION_ESTADO == 0)
                    {
                        var jwtHelper = new JWTHelper();
                        DataRow row = data.Rows[0];

                        logInEntity.C_Id_Empleado = row["ID_EMPLEADO"].ToString();
                        logInEntity.C_Nombre_Sucursal = row["NOMBRE_SUCURSAL"].ToString();
                        logInEntity.C_Id_Sucursal = row["ID_SUCURSAL"].ToString();
                        logInEntity.C_Logo_Sucursal = row["LOGO_SUCURSAL"].ToString();
                        logInEntity.C_Nombre_Empleado = row["NOMBRE_EMPLEADO"].ToString();
                        logInEntity.C_Foto_Empleado = row["FOTO_EMPLEADO"].ToString();
                        logInEntity.C_Correo = row["CORREO"].ToString();
                        logInEntity.C_Descripcion_Roles = row["ROL"].ToString();
                        logInEntity.C_Id_Usuario = row["ID_USUARIO"].ToString();
                        logInEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        logInEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;




                        token = jwtHelper.CreateToken(logInEntity.C_Id_Usuario, logInEntity.C_Descripcion_Roles, logInEntity.C_Id_Sucursal, logInEntity.C_Nombre_Sucursal, logInEntity.C_Logo_Sucursal, logInEntity.C_Nombre_Empleado, logInEntity.C_Foto_Empleado, logInEntity.C_Correo, secret);

                        logInEntity.C_Token = token;

                        res = true;
                    }
                    else
                    {
                        var jwtHelper = new JWTHelper();
                        int puntoComaIndex = O_TRANSACCION_MENSAJE.IndexOf(';');

                        if (puntoComaIndex != -1)
                        {
                            // Obtener la subcadena después del punto y coma (;)
                            string subcadena = O_TRANSACCION_MENSAJE.Substring(puntoComaIndex + 1);

                            // Eliminar espacios en blanco al inicio y al final de la subcadena
                            Id_Dispositivo = subcadena.Trim();


                            // Obtener la subcadena antes del punto y coma (;)
                            string subcadenas = O_TRANSACCION_MENSAJE.Substring(0, puntoComaIndex);

                            // Eliminar espacios en blanco al inicio y al final de la subcadena
                            string texto = subcadenas.Trim();




                            DataRow row = data.Rows[0];

                            logInEntity.C_Transaccion_Mensaje = texto;
                            token = jwtHelper.TokenDispositivo(Id_Dispositivo, secret);
                            logInEntity.C_Token = token;

                            string validartoken = jwtHelper.TokenDispositivoValidacion(Id_Dispositivo, _ID_USUARIO, secret);


                            string nomnbre = logInEntity.C_Nombre_Empleado = row["NOMBRE_EMPLEADO"].ToString();
                            string correo = logInEntity.C_Correo = row["CORREO"].ToString();

                            string textoMensaje = "https://cafenetcontrol.com/Validar" + "?token=" + validartoken;
                            string mensajefinal = @"
                                                    <html>
                                                    <head>
                                                        <style>

                                                            body {
                                                                font-family: Arial, sans-serif;
                                                                line-height: 1.6;
                                                                color: #333;
                                                            }

                                                            h1 {
                                                                color: #007BFF;
                                                            }

                                                            p {
                                                                font-size: 16px;
                                                            }
                                                        </style>
                                                    </head>
                                                    <body>
                                                        <h1>¡Bienvenido a nuestro sitio!</h1>
                                                        <p>Para completar el proceso de registro de un nuevo dispositivo, le indicamos que debe ingresar al siguiente enlace:</p>
                                                        <p><a href=""" + textoMensaje + @""">Confirmar registro</a></p>
                                                        <p>Si usted no ha intentado iniciar sesión en dispositivos nuevos, ¡le indicamos por seguridad que no abra el enlace!</p>
                                                        <p>Por motivos de seguridad el link de activacion solo es valido por 10 minutos</p>
                                                    </body>
                                                    </html>
                                                ";

                            string asunto = "ACTIVAR NUEVO DISPOSITIVO";



                            Logica.Metodos C = new Logica.Metodos();

                            C.Correos(asunto, mensajefinal, correo, nomnbre);


                        }
                        else
                        {
                            logInEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;
                            logInEntity.C_Token = "0";
                        }

                        logInEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;

                        res = false;
                    }
                }
                else
                {
                    logInEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    logInEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }


            }
            catch (Exception e)
            {

                logInEntity.C_Transaccion_Estado = 15;
                logInEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }


        public bool ValidarDispositivo(ref LogInEntity logInEntity)
        {





            bool res = false;

            try
            {



                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                var root = builder.Build();
                string token = "";
                var secret = root.GetValue<string>("AppConfig:MySecret");
                //if (secret != null)
                //{
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_ACTIVAR_DISPOSITIVO", "", "");

                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", logInEntity.C_Id_Usuario);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_DISPOSITIVO", logInEntity.C_Id_Dispositivo);

                objStoreProc.Add_Par_Int_Input("@I_VALIDACION", 2);


                objStoreProc.Add_Par_Int_Output("@O_TRANSACCION_ESTADO");
                objStoreProc.Add_Par_VarChar_Output("@O_TRANSACCION_MENSAJE", 200);



                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                int O_TRANSACCION_ESTADO = 0;
                string O_TRANSACCION_MENSAJE = "";
                string Id_Dispositivo = "";



                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    O_TRANSACCION_MENSAJE = (string)objStoreProc.obtenerValorParametroOutput("@O_TRANSACCION_MENSAJE");
                    O_TRANSACCION_ESTADO = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@O_TRANSACCION_ESTADO"));

                    if (O_TRANSACCION_ESTADO == 0)
                    {



                        logInEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        logInEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;


                        res = true;
                    }



                    else
                    {
                        logInEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        logInEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;
                        res = false;

                    }




                }

                else
                {
                    logInEntity.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    logInEntity.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }


            }
            catch (Exception e)
            {

                logInEntity.C_Transaccion_Estado = 15;
                logInEntity.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }
    }



















}
















