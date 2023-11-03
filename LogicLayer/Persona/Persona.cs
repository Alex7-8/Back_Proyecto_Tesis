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

namespace LogicLayer.Persona
{
    public class Persona
    {


		



		private string? Url_Img;
        private int idPersona;
        private int idPersonb;
        private string idUsuario;
        public string IdUsuario;
        private string idUM;
        private string idUMb;
        private string _razon;
		private string valor;

		public Persona()
        {
        }

        public Persona(string? c_Img_Base)
        {
            Url_Img = c_Img_Base;
        }

        public Persona(int idPersona)
        {
            this.idPersona = idPersona;
        }

        public Persona(int idPersona, string idUsuario, string idUM, string razon)
        {
            this.idPersona = idPersona;
            this.idUsuario = idUsuario;

            this.idUM = idUM;
            _razon = razon;

        }

        public Persona(int idPersona, string idUM, string razon)
        {
            idPersonb = idPersona;
            idUMb = idUM;
            _razon = razon;
        }

	

		public bool GetEmpleados(ref List<PersonaEntity> personaList)
        {
            DataLayer.EntityModel.PersonaEntity persona = new DataLayer.EntityModel.PersonaEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("GET_PERSONA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "EA");
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", null);
				objStoreProc.Add_Par_VarChar_Input("@I_PARAMETRO", null);


				DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (data.Rows.Count > 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            persona = new DataLayer.EntityModel.PersonaEntity();
                            persona.C_Id_Persona = Convert.ToInt32(row["ID_PERSONA"].ToString());
                            persona.C_Id_Sucursal = Convert.ToInt32(row["ID_SL"]);
                            persona.C_Id_Usuario = row["ID_USUARIO"].ToString();
                            persona.C_Sucursal = row["SUCURSAL"].ToString();
                            persona.C_Primer_Nombre = row["NOMBRE"].ToString();
                            persona.C_Departamento = row["DEPARTAMENTO"].ToString();
                            persona.C_Municipio = row["MUNICIPIO"].ToString();
                            persona.C_Usuario_Creacion = row["USUARIO_CREACION"].ToString();
                            persona.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"] != DBNull.Value ? row["USUARIO_MODIFICACION"].ToString() : "";
                            persona.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                            persona.C_Fecha_Modificacion = row["FECHA_MODIFICACION"] != DBNull.Value ? row["FECHA_MODIFICACION"].ToString() : "";
                            persona.C_Img_Base = row["URL_IMG"].ToString();
                            persona.C_Estado = (bool)row["ESTADO"] ? 1 : 2;
                            string rolValue = row["ROL"].ToString();
                            persona.C_ID_ROL = string.IsNullOrEmpty(rolValue) ? listempty : rolValue.Split(';').ToList();



                            personaList.Add(persona);

                        }

                        res = true;

                    }
                    else
                    {
                        persona.C_Transaccion_Estado = 33;
                        persona.C_Transaccion_Mensaje = "No hay registros";
                        personaList.Add(persona);
                        res = false;
                    }

                }
                else
                {

                    persona.C_Transaccion_Estado = 32;
                    persona.C_Transaccion_Mensaje = msgResEjecucion;
                    personaList.Add(persona);
                    res = false;
                }
            }
            catch (Exception e)
            {
                persona.C_Transaccion_Estado = 35;
                persona.C_Transaccion_Mensaje = e.Message;
                personaList.Add(persona);
                res = false;
            }
            return res;
        }

        public bool GetEmpleadoByIdUA(ref PersonaEntity persona)
        {
            DataLayer.EntityModel.PersonaEntity Persona = new DataLayer.EntityModel.PersonaEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("GET_PERSONA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "UA");
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", this.idPersona);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", null);
				objStoreProc.Add_Par_VarChar_Input("@I_PARAMETRO", null);

				DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (data.Rows.Count > 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {

                            persona.C_Id_Persona = Convert.ToInt32(row["ID_PERSONA"].ToString());
                            persona.C_Id_Usuario = row["ID_USUARIO"].ToString();
                            persona.C_Id_Cuenta = row["ID_CUENTA"].ToString();
							persona.C_Monto = Convert.ToDecimal(row["MONTO_MAX"].ToString());
							persona.C_Id_Sucursal = Convert.ToInt32(row["ID_SL"].ToString());
                            persona.C_Id_Tipo_Cuenta = Convert.ToInt32(row["ID_TIPO_CUENTA"].ToString());
                            persona.C_Id_Genero = Convert.ToInt32(row["ID_GENERO"].ToString());
                            persona.C_Id_Rol_Persona = Convert.ToInt32(row["ID_ROL_PERSONA"].ToString());
                            persona.C_Id_Departamento = Convert.ToInt32(row["ID_DEPTO"].ToString());
                            persona.C_Id_Municipio = Convert.ToInt32(row["ID_MUN"].ToString());
                            persona.C_Primer_Nombre = row["PRIMER_NOMBRE"].ToString();
                            persona.C_Segundo_Nombre = row["SEGUNDO_NOMBRE"].ToString();
                            persona.C_Tercer_Nombre = row["TERCER_NOMBRE"] != DBNull.Value ? row["TERCER_NOMBRE"].ToString() : "";
                            persona.C_Primer_Apellido = row["PRIMER_APELLIDO"].ToString();
                            persona.C_Segundo_Apellido = row["SEGUNDO_APELLIDO"].ToString();
                            persona.C_Apellido_Casada = row["APELLIDO_CASADA"] != DBNull.Value ? row["APELLIDO_CASADA"].ToString() : "";
                            persona.C_DPI = row["DPI"].ToString();
                            persona.C_NIT = row["NIT"].ToString();
                            persona.C_Direccion = row["DIRECCION"].ToString();
                            persona.C_PNumero_Telefono = row["PTELEFONO"].ToString();
                            persona.C_SNumero_Telefono = row["STELEFONO"] != DBNull.Value ? row["STELEFONO"].ToString() : "";
                            persona.C_Correo = row["CORREO"].ToString();
                            persona.C_Fecha_Nacimiento = Convert.ToDateTime(row["FECHA_NACIMIENTO"]);
                            persona.C_Img_Base = row["URL_IMG"].ToString();
                            string rolValue = row["ROL"].ToString();
                            persona.C_ID_ROL = string.IsNullOrEmpty(rolValue) ? listempty : rolValue.Split(';').ToList();
                            persona.C_Descripcion = row["DESCRIPCION"].ToString();





                        }

                        res = true;

                    }
                    else
                    {
                        persona.C_Transaccion_Estado = 33;
                        persona.C_Transaccion_Mensaje = "No hay registros";

                        res = false;
                    }

                }
                else
                {

                    persona.C_Transaccion_Estado = 32;
                    persona.C_Transaccion_Mensaje = msgResEjecucion;

                    res = false;
                }
            }
            catch (Exception e)
            {
                persona.C_Transaccion_Estado = 35;
                persona.C_Transaccion_Mensaje = e.Message;

                res = false;
            }
            return res;
        }

        public bool setEmpleadosA(ref PersonaEntity persona)
        {

            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_PERSONA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "E");
                objStoreProc.Add_Par_VarChar_Input("@I_VALIDAR", "T");
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", persona.C_Id_Usuario);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_ANTERIOR", null);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_NUEVA", null);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_ROL", string.Join(";", persona.C_ID_ROL));
                objStoreProc.Add_Par_Int_Input("@I_ID_ROL_PERSONA", 1);
                objStoreProc.Add_Par_Int_Input("@I_ID_TIPO_CUENTA", persona.C_Id_Tipo_Cuenta);
                objStoreProc.Add_Par_Int_Input("@I_ID_ESTADO_CUENTA", 1);
                objStoreProc.Add_Par_Decimal_Input("@I_MONTO", persona.C_Monto);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", persona.C_Id_Sucursal);
                objStoreProc.Add_Par_Int_Input("@I_ID_GENERO", persona.C_Id_Genero);
                objStoreProc.Add_Par_Int_Input("@I_ID_MUNICIPIO", persona.C_Id_Municipio);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_NOMBRE", persona.C_Primer_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_NOMBRE", persona.C_Segundo_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_TERCER_NOMBRE", persona.C_Tercer_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_APELLIDO", persona.C_Primer_Apellido);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_APELLIDO", persona.C_Segundo_Apellido);
                objStoreProc.Add_Par_VarChar_Input("@I_APELLIDO_CASADA", persona.C_Apellido_Casada);
                objStoreProc.Add_Par_VarChar_Input("@I_DPI", persona.C_DPI);
                objStoreProc.Add_Par_VarChar_Input("@I_NIT", persona.C_NIT);
                objStoreProc.Add_Par_VarChar_Input("@I_DIRECCION", persona.C_Direccion);
                objStoreProc.Add_Par_VarChar_Input("@I_PTELEFONO", persona.C_PNumero_Telefono);
                objStoreProc.Add_Par_VarChar_Input("@I_STELEFONO", persona.C_SNumero_Telefono);
                objStoreProc.Add_Par_VarChar_Input("@I_CORREO", persona.C_Correo);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", Url_Img);
                objStoreProc.Add_Par_VarChar_Input("@I_EMPRESA", persona.C_Empresa);
                objStoreProc.Add_Par_Int_Input("@I_ESTADO", 1);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", null);
                objStoreProc.Add_Par_Datetime_Input("@I_FECHA_NACIMIENTO", persona.C_Fecha_Nacimiento);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", persona.C_Usuario_Creacion);
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


                        DataRow row = data.Rows[0];

                        string IdUsuario = row["ID_USUARIO"].ToString();
                        string Contrasenia = row["CONTRASEÑA"].ToString();
                        string correo = row["CORREO"].ToString();
                        persona.C_Roles = row["ROL"].ToString();
                        string NumeroCuenta = row["NUMERO_CUENTA"].ToString();
                        string roles = persona.C_Roles.Replace(";", "<br />");


                        string nomnbre = persona.C_Primer_Nombre = row["NOMBRE"].ToString();

                        string textoMensaje = "https://cafeinternet.srvcentral.com/";


                        string mensajefinal = $@"
                                            <html>
                                            <head>
                                                <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
                                                <style>
                                                    body {{
                                                        font-family: Arial, sans-serif;
                                                        line-height: 1.6;
                                                        color: #333;
                                                    }}

                                                    h1 {{
                                                        color: #007BFF;
                                                    }}

                                                    p {{
                                                        font-size: 16px;
                                                    }}
                                                </style>
                                            </head>
                                            <body>
                                                <div class=""container"">
                                                    <h1 class=""mt-4"">¡Bienvenido!</h1>
                                                    <p>Sus credenciales de acceso son las siguientes:</p>
                                                    <p>ID de Usuario: {IdUsuario}</p>
                                                    <p>Contraseña: {Contrasenia}</p>
                                                    <p>Numero de Cuenta: {NumeroCuenta}</p> 
                                                    <p>Rol(es):</p>
                                                    <ul class=""list-group"">
                                                        <li class=""list-group-item"">{roles}</li>
                                                    </ul>
                                                    <p><a class=""btn btn-primary"" href=""{textoMensaje}"">Ingresar al Sistema</a></p>
                                                    <p class=""mt-4"">Por motivos de seguridad, el inicio de sesión será bloqueado. Una vez intente acceder al sistema, recibirá un correo en el cual deberá seguir las instrucciones para habilitar su acceso al sistema.</p>
                                                </div>
                                            </body>
                                            </html>
                                            ";

                        string asunto = "CREDENCIALES DE ACCESO";



                        Logica.Metodos C = new Logica.Metodos();

                        C.Correos(asunto, mensajefinal, correo, nomnbre);

                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }

                    else

                    {
                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;
                        res = false;

                    }




                }

                else
                {
                    persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                persona.C_Transaccion_Estado = 26;
                persona.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

      
        public bool setPersona(ref PersonaEntity persona)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_PERSONA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "P");
                objStoreProc.Add_Par_VarChar_Input("@I_VALIDAR", "T");
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", null);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_ANTERIOR", null);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_NUEVA", null);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_ROL", null);
                objStoreProc.Add_Par_Int_Input("@I_ID_ROL_PERSONA", persona.C_Id_Rol_Persona);
                objStoreProc.Add_Par_Int_Input("@I_ID_TIPO_CUENTA", persona.C_Id_Tipo_Cuenta);
                objStoreProc.Add_Par_Int_Input("@I_ID_ESTADO_CUENTA", 1);
                objStoreProc.Add_Par_Decimal_Input("@I_MONTO", persona.C_Monto);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", persona.C_Id_Sucursal);
                objStoreProc.Add_Par_Int_Input("@I_ID_GENERO", persona.C_Id_Genero);
                objStoreProc.Add_Par_Int_Input("@I_ID_MUNICIPIO", persona.C_Id_Municipio);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_NOMBRE", persona.C_Primer_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_NOMBRE", persona.C_Segundo_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_TERCER_NOMBRE", persona.C_Tercer_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_APELLIDO", persona.C_Primer_Apellido);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_APELLIDO", persona.C_Segundo_Apellido);
                objStoreProc.Add_Par_VarChar_Input("@I_APELLIDO_CASADA", persona.C_Apellido_Casada);
                objStoreProc.Add_Par_VarChar_Input("@I_DPI", persona.C_DPI);
                objStoreProc.Add_Par_VarChar_Input("@I_NIT", persona.C_NIT);
                objStoreProc.Add_Par_VarChar_Input("@I_DIRECCION", persona.C_Direccion);
                objStoreProc.Add_Par_VarChar_Input("@I_PTELEFONO", persona.C_PNumero_Telefono);
                objStoreProc.Add_Par_VarChar_Input("@I_STELEFONO", persona.C_SNumero_Telefono);
                objStoreProc.Add_Par_VarChar_Input("@I_CORREO", persona.C_Correo);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", Url_Img);
                objStoreProc.Add_Par_VarChar_Input("@I_EMPRESA", persona.C_Empresa);
                objStoreProc.Add_Par_Int_Input("@I_ESTADO", 1);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", null);
                objStoreProc.Add_Par_Datetime_Input("@I_FECHA_NACIMIENTO", persona.C_Fecha_Nacimiento);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", persona.C_Usuario_Creacion);
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


                        DataRow row = data.Rows[0];

                        string Nombre = row["NOMBRE"].ToString();
                        string correo = row["CORREO"].ToString();
                        string C_Rol = row["ROL"].ToString();
                        string NumeroCuenta = row["NUMERO_CUENTA"].ToString();
                        string Tipo = row["TIPO"].ToString();


                        string textoMensaje = "";





                        string mensajefinal = $@"
    <html>
    <head>
        <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
        <style>
            body {{
                font-family: Arial, sans-serif;
                line-height: 1.6;
                color: #333;
                background-color: #f4f4f4;
                padding: 20px;
            }}

            .container {{
                background-color: #fff;
                padding: 20px;
                border-radius: 5px;
                box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            }}

            h1 {{
                color: #007BFF;
                margin-bottom: 20px;
            }}

            p {{
                font-size: 16px;
                margin-bottom: 10px;
            }}
        </style>
    </head>
    <body>
        <div class=""container"">
            <h1>Hola:</h1>
            <p>{Nombre}</p>
            <p>Agradecemos ser un: {C_Rol} de nuestra empresa.</p>
            <p>Asimismo, le informamos de su:</p>
            <p>Número de Cuenta: {NumeroCuenta}</p> 
            <p>Tipo: {Tipo}</p>
        </div>
    </body>
    </html>
";


                        string asunto = "Informacion";



                        Logica.Metodos C = new Logica.Metodos();

                        C.Correos(asunto, mensajefinal, correo, Nombre);

                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }

                    else

                    {
                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;
                        res = false;

                    }




                }

                else
                {
                    persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                persona.C_Transaccion_Estado = 26;
                persona.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool putActualizarEmpleadosA(ref PersonaEntity persona)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_PERSONA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "UA");
                objStoreProc.Add_Par_VarChar_Input("@I_VALIDAR", persona.C_Validar);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", persona.C_Id_Usuario);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_ANTERIOR", null);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_NUEVA", null);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_ROL", string.Join(",", persona.C_ID_ROL));
                objStoreProc.Add_Par_Int_Input("@I_ID_ROL_PERSONA", persona.C_Id_Rol_Persona);
                objStoreProc.Add_Par_Int_Input("@I_ID_TIPO_CUENTA", persona.C_Id_Tipo_Cuenta);
                objStoreProc.Add_Par_Int_Input("@I_ID_ESTADO_CUENTA", 1);
                objStoreProc.Add_Par_Decimal_Input("@I_MONTO", persona.C_Monto);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", persona.C_Id_Persona);
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", persona.C_Id_Sucursal);
                objStoreProc.Add_Par_Int_Input("@I_ID_GENERO", persona.C_Id_Genero);
                objStoreProc.Add_Par_Int_Input("@I_ID_MUNICIPIO", persona.C_Id_Municipio);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_NOMBRE", persona.C_Primer_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_NOMBRE", persona.C_Segundo_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_TERCER_NOMBRE", persona.C_Tercer_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_APELLIDO", persona.C_Primer_Apellido);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_APELLIDO", persona.C_Segundo_Apellido);
                objStoreProc.Add_Par_VarChar_Input("@I_APELLIDO_CASADA", persona.C_Apellido_Casada);
                objStoreProc.Add_Par_VarChar_Input("@I_DPI", persona.C_DPI);
                objStoreProc.Add_Par_VarChar_Input("@I_NIT", persona.C_NIT);
                objStoreProc.Add_Par_VarChar_Input("@I_DIRECCION", persona.C_Direccion);
                objStoreProc.Add_Par_VarChar_Input("@I_PTELEFONO", persona.C_PNumero_Telefono);
                objStoreProc.Add_Par_VarChar_Input("@I_STELEFONO", persona.C_SNumero_Telefono);
                objStoreProc.Add_Par_VarChar_Input("@I_CORREO", persona.C_Correo);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", Url_Img);
                objStoreProc.Add_Par_Int_Input("@I_ESTADO", 1);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", persona.C_Descripcion);
                objStoreProc.Add_Par_VarChar_Input("@I_EMPRESA", null);
                objStoreProc.Add_Par_Datetime_Input("@I_FECHA_NACIMIENTO", persona.C_Fecha_Nacimiento);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_MODIFICACION", persona.C_Usuario_Modificacion);

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


                        DataRow row = data.Rows[0];
                        string correo = row["CORREO"].ToString();
                        persona.C_Roles = row["ROL"].ToString();
                        string roles = persona.C_Roles.Replace(";", "<br />");



                        string Nombre = persona.C_Primer_Nombre = row["NOMBRE"].ToString();




                        string mensajefinal = $@"
                                                    <html>
                                                    <head>
                                                        <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
                                                        <style>
                                                            body {{
                                                                font-family: Arial, sans-serif;
                                                                line-height: 1.6;
                                                                color: #333;
                                                                background-color: #f4f4f4;
                                                                padding: 20px;
                                                            }}

                                                            .container {{
                                                                background-color: #fff;
                                                                padding: 20px;
                                                                border-radius: 5px;
                                                                box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                                                            }}

                                                            h1 {{
                                                                color: #007BFF;
                                                                margin-bottom: 20px;
                                                            }}

                                                            p {{
                                                                font-size: 16px;
                                                                margin-bottom: 10px;
                                                            }}
                                                        </style>
                                                    </head>
                                                    <body>
                                                        <div class=""container"">
                                                            <h1>Hola:</h1>
                                                            <p>{Nombre}</p>
                                                            <p>Le informamos que sus datos fueron actualizados, los roles de acceso al sistema que posee son los siguientes</p>
                                                           <p>Rol(es):</p>
                                                    <ul class=""list-group"">
                                                        <li class=""list-group-item"">{roles}</li>
                                                    </ul>
                                                        </div>
                                                    </body>
                                                    </html>
                                                ";



                        string asunto = "Actualizacion de Datos";



                        Logica.Metodos C = new Logica.Metodos();

                        C.Correos(asunto, mensajefinal, correo, Nombre);

                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }

                    else

                    {
                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;
                        res = false;

                    }




                }

                else
                {
                    persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                persona.C_Transaccion_Estado = 26;
                persona.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool CambiarEstadoEmpleado(ref PersonaEntity persona)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_PERSONA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "CE");
                objStoreProc.Add_Par_VarChar_Input("@I_VALIDAR", persona.C_Validar);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", this.idUsuario);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_ANTERIOR", null);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_NUEVA", null);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_ROL", null);
                objStoreProc.Add_Par_Int_Input("@I_ID_ROL_PERSONA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_TIPO_CUENTA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_ESTADO_CUENTA", 0);
                objStoreProc.Add_Par_Decimal_Input("@I_MONTO", (decimal)0.00);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", this.idPersona);
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_GENERO", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_MUNICIPIO", 0);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_NOMBRE", null);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_NOMBRE", null);
                objStoreProc.Add_Par_VarChar_Input("@I_TERCER_NOMBRE", null);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_APELLIDO", null);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_APELLIDO", null);
                objStoreProc.Add_Par_VarChar_Input("@I_APELLIDO_CASADA", null);
                objStoreProc.Add_Par_VarChar_Input("@I_DPI", null);
                objStoreProc.Add_Par_VarChar_Input("@I_NIT", null);
                objStoreProc.Add_Par_VarChar_Input("@I_DIRECCION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_PTELEFONO", null);
                objStoreProc.Add_Par_VarChar_Input("@I_STELEFONO", null);
                objStoreProc.Add_Par_VarChar_Input("@I_CORREO", null);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", null);
                objStoreProc.Add_Par_Int_Input("@I_ESTADO", 0);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", this._razon);
                objStoreProc.Add_Par_VarChar_Input("@I_EMPRESA", null);
                objStoreProc.Add_Par_Datetime_Input("@I_FECHA_NACIMIENTO", persona.C_Fecha_Nacimiento);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_MODIFICACION", this.idUM);

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


                        //DataRow row = data.Rows[0];
                        //string correo = row["CORREO"].ToString();
                        //persona.C_Roles = row["ROL"].ToString();
                        //string roles = persona.C_Roles.Replace(";", "<br />");



                        //string Nombre = persona.C_Primer_Nombre = row["NOMBRE"].ToString();




                        //string mensajefinal = $@"
                        //                            <html>
                        //                            <head>
                        //                                <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
                        //                                <style>
                        //                                    body {{
                        //                                        font-family: Arial, sans-serif;
                        //                                        line-height: 1.6;
                        //                                        color: #333;
                        //                                        background-color: #f4f4f4;
                        //                                        padding: 20px;
                        //                                    }}

                        //                                    .container {{
                        //                                        background-color: #fff;
                        //                                        padding: 20px;
                        //                                        border-radius: 5px;
                        //                                        box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                        //                                    }}

                        //                                    h1 {{
                        //                                        color: #007BFF;
                        //                                        margin-bottom: 20px;
                        //                                    }}

                        //                                    p {{
                        //                                        font-size: 16px;
                        //                                        margin-bottom: 10px;
                        //                                    }}
                        //                                </style>
                        //                            </head>
                        //                            <body>
                        //                                <div class=""container"">
                        //                                    <h1>Hola:</h1>
                        //                                    <p>{Nombre}</p>
                        //                                    <p>Le informamos que sus datos fueron actualizados, los roles de acceso al sistema que posee son los siguientes</p>
                        //                                   <p>Rol(es):</p>
                        //                            <ul class=""list-group"">
                        //                                <li class=""list-group-item"">{roles}</li>
                        //                            </ul>
                        //                                </div>
                        //                            </body>
                        //                            </html>
                        //                        ";



                        //string asunto = "Actualizacion de Datos";



                        //Logica.Metodos C = new Logica.Metodos();

                        //C.Correos(asunto, mensajefinal, correo, Nombre);

                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }

                    else

                    {
                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;
                        res = false;

                    }




                }

                else
                {
                    persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                persona.C_Transaccion_Estado = 26;
                persona.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool GetPersona(ref List<PersonaEntity> personaList)
        {
            DataLayer.EntityModel.PersonaEntity persona = new DataLayer.EntityModel.PersonaEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("GET_PERSONA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "CP");
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", null);
				objStoreProc.Add_Par_VarChar_Input("@I_PARAMETRO", null);

				DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (data.Rows.Count > 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            persona = new DataLayer.EntityModel.PersonaEntity();
                            persona.C_Id_Persona = Convert.ToInt32(row["ID_PERSONA"].ToString());
                            persona.C_Id_Rol_Persona = Convert.ToInt32(row["ID_ROL_PERSONA"].ToString());
                            persona.C_Sucursal = row["SUCURSAL"].ToString();
                            persona.C_Primer_Nombre = row["NOMBRE"].ToString();
                            persona.C_Departamento = row["DEPARTAMENTO"].ToString();
                            persona.C_Municipio = row["MUNICIPIO"].ToString();
                            persona.C_Usuario_Creacion = row["USUARIO_CREACION"].ToString();
                            persona.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"] != DBNull.Value ? row["USUARIO_MODIFICACION"].ToString() : "";
                            persona.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                            persona.C_Fecha_Modificacion = row["FECHA_MODIFICACION"] != DBNull.Value ? row["FECHA_MODIFICACION"].ToString() : "";
                            persona.C_Img_Base = row["URL_IMG"].ToString();
                            persona.C_Estado = (bool)row["ESTADO"] ? 1 : 2;
                            persona.C_Tipo = row["Tipo"].ToString();
                            persona.C_Empresa = row["EMPRESA"] != DBNull.Value ? row["EMPRESA"].ToString() : "";

                            personaList.Add(persona);

                        }

                        res = true;

                    }
                    else
                    {
                        persona.C_Transaccion_Estado = 33;
                        persona.C_Transaccion_Mensaje = "No hay registros";
                        personaList.Add(persona);
                        res = false;
                    }

                }
                else
                {

                    persona.C_Transaccion_Estado = 32;
                    persona.C_Transaccion_Mensaje = msgResEjecucion;
                    personaList.Add(persona);
                    res = false;
                }
            }
            catch (Exception e)
            {
                persona.C_Transaccion_Estado = 35;
                persona.C_Transaccion_Mensaje = e.Message;
                personaList.Add(persona);
                res = false;
            }
            return res;
        }

        public bool GetPersonaById(ref PersonaEntity persona)
        {
            DataLayer.EntityModel.PersonaEntity Persona = new DataLayer.EntityModel.PersonaEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("GET_PERSONA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "TP");
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", this.idPersona);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", null);
				objStoreProc.Add_Par_VarChar_Input("@I_PARAMETRO", null);

				DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (data.Rows.Count > 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {

                            persona.C_Id_Persona = Convert.ToInt32(row["ID_PERSONA"].ToString());
                            persona.C_Id_Cuenta = row["ID_CUENTA"].ToString();
                            persona.C_Monto = Convert.ToDecimal(row["MONTO_MAX"].ToString());
                            persona.C_Id_Sucursal = Convert.ToInt32(row["ID_SL"].ToString());
                            persona.C_Id_Tipo_Cuenta = Convert.ToInt32(row["ID_TIPO_CUENTA"].ToString());
                            persona.C_Id_Genero = Convert.ToInt32(row["ID_GENERO"].ToString());
                            persona.C_Id_Rol_Persona = Convert.ToInt32(row["ID_ROL_PERSONA"].ToString());
                            persona.C_Id_Departamento = Convert.ToInt32(row["ID_DEPTO"].ToString());
                            persona.C_Id_Municipio = Convert.ToInt32(row["ID_MUN"].ToString());
                            persona.C_Primer_Nombre = row["PRIMER_NOMBRE"].ToString();
                            persona.C_Segundo_Nombre = row["SEGUNDO_NOMBRE"].ToString();
                            persona.C_Tercer_Nombre = row["TERCER_NOMBRE"] != DBNull.Value ? row["TERCER_NOMBRE"].ToString() : "";
                            persona.C_Primer_Apellido = row["PRIMER_APELLIDO"].ToString();
                            persona.C_Segundo_Apellido = row["SEGUNDO_APELLIDO"].ToString();
                            persona.C_Apellido_Casada = row["APELLIDO_CASADA"] != DBNull.Value ? row["APELLIDO_CASADA"].ToString() : "";
                            persona.C_DPI = row["DPI"].ToString();
                            persona.C_NIT = row["NIT"].ToString();
                            persona.C_Direccion = row["DIRECCION"].ToString();
                            persona.C_PNumero_Telefono = row["PTELEFONO"].ToString();
                            persona.C_SNumero_Telefono = row["STELEFONO"] != DBNull.Value ? row["STELEFONO"].ToString() : "";
                            persona.C_Correo = row["CORREO"].ToString();
                            persona.C_Fecha_Nacimiento = Convert.ToDateTime(row["FECHA_NACIMIENTO"]);
                            persona.C_Img_Base = row["URL_IMG"].ToString();
                            persona.C_Empresa = row["EMPRESA"] != DBNull.Value ? row["EMPRESA"].ToString() : "";
                            persona.C_Descripcion = row["DESCRIPCION"].ToString();




                        }

                        res = true;

                    }
                    else
                    {
                        persona.C_Transaccion_Estado = 33;
                        persona.C_Transaccion_Mensaje = "No hay registros";

                        res = false;
                    }

                }
                else
                {

                    persona.C_Transaccion_Estado = 32;
                    persona.C_Transaccion_Mensaje = msgResEjecucion;

                    res = false;
                }
            }
            catch (Exception e)
            {
                persona.C_Transaccion_Estado = 35;
                persona.C_Transaccion_Mensaje = e.Message;

                res = false;
            }
            return res;
        }

        public bool CambiarEstadoPersona(ref PersonaEntity persona)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_PERSONA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "AD");
                objStoreProc.Add_Par_VarChar_Input("@I_VALIDAR", persona.C_Validar);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", null);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_ANTERIOR", null);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_NUEVA", null);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_ROL", null);
                objStoreProc.Add_Par_Int_Input("@I_ID_ROL_PERSONA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_TIPO_CUENTA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_ESTADO_CUENTA", 0);
                objStoreProc.Add_Par_Decimal_Input("@I_MONTO", (decimal)0.00);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", this.idPersonb);
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_GENERO", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_MUNICIPIO", 0);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_NOMBRE", null);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_NOMBRE", null);
                objStoreProc.Add_Par_VarChar_Input("@I_TERCER_NOMBRE", null);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_APELLIDO", null);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_APELLIDO", null);
                objStoreProc.Add_Par_VarChar_Input("@I_APELLIDO_CASADA", null);
                objStoreProc.Add_Par_VarChar_Input("@I_DPI", null);
                objStoreProc.Add_Par_VarChar_Input("@I_NIT", null);
                objStoreProc.Add_Par_VarChar_Input("@I_DIRECCION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_PTELEFONO", null);
                objStoreProc.Add_Par_VarChar_Input("@I_STELEFONO", null);
                objStoreProc.Add_Par_VarChar_Input("@I_CORREO", null);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", null);
                objStoreProc.Add_Par_Int_Input("@I_ESTADO", 0);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", this._razon);
                objStoreProc.Add_Par_VarChar_Input("@I_EMPRESA", null);
                objStoreProc.Add_Par_Datetime_Input("@I_FECHA_NACIMIENTO", persona.C_Fecha_Nacimiento);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_MODIFICACION", this.idUMb);

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

                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }

                    else

                    {
                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;
                        res = false;

                    }




                }

                else
                {
                    persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                persona.C_Transaccion_Estado = 26;
                persona.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool putActualizarPersona(ref PersonaEntity persona)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_PERSONA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "UE");
                objStoreProc.Add_Par_VarChar_Input("@I_VALIDAR", persona.C_Validar);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", null);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_ANTERIOR", null);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_NUEVA", null);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_ROL", null);
                objStoreProc.Add_Par_Int_Input("@I_ID_ROL_PERSONA", persona.C_Id_Rol_Persona);
                objStoreProc.Add_Par_Int_Input("@I_ID_TIPO_CUENTA", persona.C_Id_Tipo_Cuenta);
                objStoreProc.Add_Par_Int_Input("@I_ID_ESTADO_CUENTA", 1);
                objStoreProc.Add_Par_Decimal_Input("@I_MONTO", persona.C_Monto);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", persona.C_Id_Persona);
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", persona.C_Id_Sucursal);
                objStoreProc.Add_Par_Int_Input("@I_ID_GENERO", persona.C_Id_Genero);
                objStoreProc.Add_Par_Int_Input("@I_ID_MUNICIPIO", persona.C_Id_Municipio);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_NOMBRE", persona.C_Primer_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_NOMBRE", persona.C_Segundo_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_TERCER_NOMBRE", persona.C_Tercer_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_APELLIDO", persona.C_Primer_Apellido);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_APELLIDO", persona.C_Segundo_Apellido);
                objStoreProc.Add_Par_VarChar_Input("@I_APELLIDO_CASADA", persona.C_Apellido_Casada);
                objStoreProc.Add_Par_VarChar_Input("@I_DPI", persona.C_DPI);
                objStoreProc.Add_Par_VarChar_Input("@I_NIT", persona.C_NIT);
                objStoreProc.Add_Par_VarChar_Input("@I_DIRECCION", persona.C_Direccion);
                objStoreProc.Add_Par_VarChar_Input("@I_PTELEFONO", persona.C_PNumero_Telefono);
                objStoreProc.Add_Par_VarChar_Input("@I_STELEFONO", persona.C_SNumero_Telefono);
                objStoreProc.Add_Par_VarChar_Input("@I_CORREO", persona.C_Correo);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", Url_Img);
                objStoreProc.Add_Par_Int_Input("@I_ESTADO", 1);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", persona.C_Descripcion);
                objStoreProc.Add_Par_VarChar_Input("@I_EMPRESA", persona.C_Empresa);
                objStoreProc.Add_Par_Datetime_Input("@I_FECHA_NACIMIENTO", persona.C_Fecha_Nacimiento);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_MODIFICACION", persona.C_Usuario_Modificacion);

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


                        DataRow row = data.Rows[0];
                        string Nombre = row["NOMBRE"].ToString();
                        string sl = row["SUCURSAL"].ToString();
                        int gn = Convert.ToInt32(row["GENERO"]);
                        string correo = row["CORREO"].ToString();
                        string Descripcion = row["DESCRIPCION"].ToString();
                        string NumeroCuenta = row["NumeroCuenta"].ToString();
                        string Genero = "";
                        string mensajefinal;
                        if (gn == 1)
                        {
                            Genero = "Señorita";
                        }
                        if (gn == 2)
                        {
                            Genero = "Señor";
                        }

                        if (NumeroCuenta == "NA")
                        {
                            mensajefinal = $@"
<html>
<head>
  <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
  <style>
    body {{
      font-family: 'Roboto', sans-serif;
      background-color: #f4f4f4;
      padding: 20px;
    }}
    .container {{
      background-color: #fff;
      padding: 20px;
      border-radius: 5px;
      box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
    }}
    h1 {{
      color: #007BFF;
      margin-bottom: 20px;
    }}
    p.header {{
      font-size: 24px;
      margin-bottom: 10px;
      color: #007BFF;
    }}
    p.content {{
      font-size: 18px;
      margin-bottom: 15px;
    }}
    .description {{
      background-color: #f7f7f7;
      padding: 15px;
      border-radius: 5px;
      margin-top: 20px;
    }}
    .footer {{
      text-align: center;
      margin-top: 20px;
    }}
    .social-icons {{
      font-size: 30px;
      margin-top: 15px;
    }}
    .social-icons a {{
      color: #007BFF;
      margin: 0 10px;
    }}
  </style>
</head>
<body>
  <div class=""container"">
    <h1>Hola, te saludamos de:</h1>
    <p class=""header"">{sl}</p>
    <p class=""content"">{Genero}: {Nombre}</p>
    <p class=""content"">Le informamos que ha habido una actualización de datos, por lo cual te indicamos cuál fue el motivo:</p>
    <div class=""description"">
      <p class=""content"">{Descripcion}</p>
    </div>
  </div>
  <div class=""footer"">
    <div class=""social-icons"">
      <a href=""https://www.facebook.com""><i class=""fab fa-facebook-square""></i></a>
      <a href=""https://www.whatsapp.com""><i class=""fab fa-whatsapp-square""></i></a>
    </div>
  </div>
  <script src=""https://kit.fontawesome.com/a076d05399.js"" crossorigin=""anonymous""></script>
</body>
</html>";
                        }
                        else 
                        {
                            mensajefinal = $@"
<html>
<head>
  <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
  <style>
    body {{
      font-family: 'Roboto', sans-serif;
      background-color: #f4f4f4;
      padding: 20px;
    }}
    .container {{
      background-color: #fff;
      padding: 20px;
      border-radius: 5px;
      box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
    }}
    h1 {{
      color: #007BFF;
      margin-bottom: 20px;
    }}
    p.header {{
      font-size: 24px;
      margin-bottom: 10px;
      color: #007BFF;
    }}
    p.content {{
      font-size: 18px;
      margin-bottom: 15px;
    }}
    .description {{
      background-color: #f7f7f7;
      padding: 15px;
      border-radius: 5px;
      margin-top: 20px;
    }}
    .footer {{
      text-align: center;
      margin-top: 20px;
    }}
    .social-icons {{
      font-size: 30px;
      margin-top: 15px;
    }}
    .social-icons a {{
      color: #007BFF;
      margin: 0 10px;
    }}
  </style>
</head>
<body>
  <div class=""container"">
    <h1>Hola, te saludamos de:</h1>
    <p class=""header"">{sl}</p>
    <p class=""content"">{Genero}: {Nombre}</p>
    <p class=""content"">Le informamos que ha habido una actualización de datos, por lo cual te indicamos cuál fue el motivo:</p>
    <div class=""description"">
      <p class=""content"">{Descripcion}</p>
        <h1>Numero de Cuenta:</h1>
     <p class=""content"">{NumeroCuenta}</p>
    </div>
  </div>
  <div class=""footer"">
    <div class=""social-icons"">
      <a href=""https://www.facebook.com""><i class=""fab fa-facebook-square""></i></a>
      <a href=""https://www.whatsapp.com""><i class=""fab fa-whatsapp-square""></i></a>
    </div>
  </div>
  <script src=""https://kit.fontawesome.com/a076d05399.js"" crossorigin=""anonymous""></script>
</body>
</html>";

                        }

                    




                        string asunto = "Actualizacion de Datos";



                        Logica.Metodos C = new Logica.Metodos();

                        C.Correos(asunto, mensajefinal, correo, Nombre);

                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }

                    else

                    {
                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;
                        res = false;

                    }




                }

                else
                {
                    persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                persona.C_Transaccion_Estado = 26;
                persona.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool GetEmpleadoByIdUsuario(ref PersonaEntity persona)
        {
            DataLayer.EntityModel.PersonaEntity Persona = new DataLayer.EntityModel.PersonaEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("GET_PERSONA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "OE");
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", this.IdUsuario);
				objStoreProc.Add_Par_VarChar_Input("@I_PARAMETRO", null);

				DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (data.Rows.Count > 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {

                            persona.C_Id_Persona = Convert.ToInt32(row["ID_PERSONA"].ToString());
                            persona.C_Id_Usuario = row["ID_USUARIO"].ToString();
                            persona.C_Id_Genero = Convert.ToInt32(row["ID_GENERO"].ToString());
                            persona.C_Id_Departamento = Convert.ToInt32(row["ID_DEPTO"].ToString());
                            persona.C_Id_Municipio = Convert.ToInt32(row["ID_MUN"].ToString());
                            persona.C_Primer_Nombre = row["PRIMER_NOMBRE"].ToString();
                            persona.C_Segundo_Nombre = row["SEGUNDO_NOMBRE"].ToString();
                            persona.C_Tercer_Nombre = row["TERCER_NOMBRE"] != DBNull.Value ? row["TERCER_NOMBRE"].ToString() : "";
                            persona.C_Primer_Apellido = row["PRIMER_APELLIDO"].ToString();
                            persona.C_Segundo_Apellido = row["SEGUNDO_APELLIDO"].ToString();
                            persona.C_Apellido_Casada = row["APELLIDO_CASADA"] != DBNull.Value ? row["APELLIDO_CASADA"].ToString() : "";
                            persona.C_DPI = row["DPI"].ToString();
                            persona.C_NIT = row["NIT"].ToString();
                            persona.C_Direccion = row["DIRECCION"].ToString();
                            persona.C_PNumero_Telefono = row["PTELEFONO"].ToString();
                            persona.C_SNumero_Telefono = row["STELEFONO"] != DBNull.Value ? row["STELEFONO"].ToString() : "";
                            persona.C_Correo = row["CORREO"].ToString();
                            persona.C_Fecha_Nacimiento = Convert.ToDateTime(row["FECHA_NACIMIENTO"]);
                            persona.C_Img_Base = row["URL_IMG"].ToString();
                            persona.C_Descripcion = row["DESCRIPCION"].ToString();




                        }

                        res = true;

                    }
                    else
                    {
                        persona.C_Transaccion_Estado = 33;
                        persona.C_Transaccion_Mensaje = "No hay registros";

                        res = false;
                    }

                }
                else
                {

                    persona.C_Transaccion_Estado = 32;
                    persona.C_Transaccion_Mensaje = msgResEjecucion;

                    res = false;
                }
            }
            catch (Exception e)
            {
                persona.C_Transaccion_Estado = 35;
                persona.C_Transaccion_Mensaje = e.Message;

                res = false;
            }
            return res;
        }

        public bool putEmpleadosByIdUsuario(ref PersonaEntity persona)
        {


            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                var root = builder.Build();
                string token = "";
                var secret = root.GetValue<string>("AppConfig:MySecret");
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_PERSONA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "UR");
                objStoreProc.Add_Par_VarChar_Input("@I_VALIDAR", persona.C_Validar);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", persona.C_Id_Usuario);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_ANTERIOR", persona.C_ContraseniaA);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_NUEVA", persona.C_ContraseniaN);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_ROL", null);
                objStoreProc.Add_Par_Int_Input("@I_ID_ROL_PERSONA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_TIPO_CUENTA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_ESTADO_CUENTA", 1);
                objStoreProc.Add_Par_Decimal_Input("@I_MONTO", (decimal)0.00);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", persona.C_Id_Persona);
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_GENERO", persona.C_Id_Genero);
                objStoreProc.Add_Par_Int_Input("@I_ID_MUNICIPIO", persona.C_Id_Municipio);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_NOMBRE", persona.C_Primer_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_NOMBRE", persona.C_Segundo_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_TERCER_NOMBRE", persona.C_Tercer_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_APELLIDO", persona.C_Primer_Apellido);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_APELLIDO", persona.C_Segundo_Apellido);
                objStoreProc.Add_Par_VarChar_Input("@I_APELLIDO_CASADA", persona.C_Apellido_Casada);
                objStoreProc.Add_Par_VarChar_Input("@I_DPI", persona.C_DPI);
                objStoreProc.Add_Par_VarChar_Input("@I_NIT", persona.C_NIT);
                objStoreProc.Add_Par_VarChar_Input("@I_DIRECCION", persona.C_Direccion);
                objStoreProc.Add_Par_VarChar_Input("@I_PTELEFONO", persona.C_PNumero_Telefono);
                objStoreProc.Add_Par_VarChar_Input("@I_STELEFONO", persona.C_SNumero_Telefono);
                objStoreProc.Add_Par_VarChar_Input("@I_CORREO", persona.C_Correo);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", Url_Img);
                objStoreProc.Add_Par_Int_Input("@I_ESTADO", 1);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", persona.C_Descripcion);
                objStoreProc.Add_Par_VarChar_Input("@I_EMPRESA", persona.C_Empresa);
                objStoreProc.Add_Par_Datetime_Input("@I_FECHA_NACIMIENTO", persona.C_Fecha_Nacimiento);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_MODIFICACION", persona.C_Usuario_Modificacion);

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

                        var jwtHelper = new JWTHelper();
                        DataRow row = data.Rows[0];
                        string Nombre = row["NOMBRE"].ToString();
                        string sl = row["SUCURSAL"].ToString();
                        int gn = Convert.ToInt32(row["GENERO"]);
                        string correo = row["CORREO"].ToString();
                        string Descripcion = row["DESCRIPCION"].ToString();

                        string ID_EMPLEADO = row["ID_EMPLEADO"].ToString();
                        string ID_USUARIO = row["ID_USUARIO"].ToString();
                        string ID_SUCURSAL = row["ID_SUCURSAL"].ToString();
                        string LOGO_SUCURSAL = row["LOGO_SUCURSAL"].ToString();
                        string NOMBRE_SUCURSAL = row["NOMBRE_SUCURSAL"].ToString();
                        string NOMBRE_EMPLEADO = row["NOMBRE_EMPLEADO"].ToString();
                        string FOTO_EMPLEADO = row["FOTO_EMPLEADO"].ToString();
                        string ROL = row["ROL"].ToString();
                        string Genero = "";

                        if (gn == 1)
                        {
                            Genero = "Señorita";
                        }
                        if (gn == 2)
                        {
                            Genero = "Señor";
                        }
                        string mensajefinal = $@"
<html>
<head>
  <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
  <style>
    body {{
      font-family: 'Roboto', sans-serif;
      background-color: #f4f4f4;
      padding: 20px;
    }}
    .container {{
      background-color: #fff;
      padding: 20px;
      border-radius: 5px;
      box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
    }}
    h1 {{
      color: #007BFF;
      margin-bottom: 20px;
    }}
    p.header {{
      font-size: 24px;
      margin-bottom: 10px;
      color: #007BFF;
    }}
    p.content {{
      font-size: 18px;
      margin-bottom: 15px;
    }}
    .description {{
      background-color: #f7f7f7;
      padding: 15px;
      border-radius: 5px;
      margin-top: 20px;
    }}
    .footer {{
      text-align: center;
      margin-top: 20px;
    }}
    .social-icons {{
      font-size: 30px;
      margin-top: 15px;
    }}
    .social-icons a {{
      color: #007BFF;
      margin: 0 10px;
    }}
  </style>
</head>
<body>
  <div class=""container"">
    <h1>Hola, te saludamos de:</h1>
    <p class=""header"">{sl}</p>
    <p class=""content"">{Genero}: {Nombre}</p>
    <p class=""content"">Le informamos que ha habido una actualización de datos, por lo cual te indicamos cuál fue el motivo:</p>
    <div class=""description"">
      <p class=""content"">{Descripcion}</p>
    </div>
  </div>
  <div class=""footer"">
    <div class=""social-icons"">
      <a href=""https://www.facebook.com""><i class=""fab fa-facebook-square""></i></a>
      <a href=""https://www.whatsapp.com""><i class=""fab fa-whatsapp-square""></i></a>
    </div>
  </div>
  <script src=""https://kit.fontawesome.com/a076d05399.js"" crossorigin=""anonymous""></script>
</body>
</html>";




                        string asunto = "Actualizacion de Datos";



                        Logica.Metodos C = new Logica.Metodos();

                        C.Correos(asunto, mensajefinal, correo, Nombre);

                        token = jwtHelper.CreateToken(ID_USUARIO, ROL, ID_SUCURSAL, NOMBRE_SUCURSAL, LOGO_SUCURSAL, NOMBRE_EMPLEADO, FOTO_EMPLEADO, correo, secret);

                        persona.C_Token = token;

                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }

                    else

                    {
                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;
                        res = false;

                    }




                }

                else
                {
                    persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                persona.C_Transaccion_Estado = 26;
                persona.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;

        }





        //zona gerente
        public bool SetEmpleadoG(ref PersonaEntity persona)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_PERSONA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "E");
                objStoreProc.Add_Par_VarChar_Input("@I_VALIDAR", persona.C_Validar);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", persona.C_Id_Usuario);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_ANTERIOR", null);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_NUEVA", null);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_ROL", string.Join(";", 3));
                objStoreProc.Add_Par_Int_Input("@I_ID_ROL_PERSONA", 1);
                objStoreProc.Add_Par_Int_Input("@I_ID_TIPO_CUENTA", persona.C_Id_Tipo_Cuenta);
                objStoreProc.Add_Par_Int_Input("@I_ID_ESTADO_CUENTA", 1);
                objStoreProc.Add_Par_Decimal_Input("@I_MONTO", persona.C_Monto);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", persona.C_Id_Sucursal);
                objStoreProc.Add_Par_Int_Input("@I_ID_GENERO", persona.C_Id_Genero);
                objStoreProc.Add_Par_Int_Input("@I_ID_MUNICIPIO", persona.C_Id_Municipio);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_NOMBRE", persona.C_Primer_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_NOMBRE", persona.C_Segundo_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_TERCER_NOMBRE", persona.C_Tercer_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_APELLIDO", persona.C_Primer_Apellido);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_APELLIDO", persona.C_Segundo_Apellido);
                objStoreProc.Add_Par_VarChar_Input("@I_APELLIDO_CASADA", persona.C_Apellido_Casada);
                objStoreProc.Add_Par_VarChar_Input("@I_DPI", persona.C_DPI);
                objStoreProc.Add_Par_VarChar_Input("@I_NIT", persona.C_NIT);
                objStoreProc.Add_Par_VarChar_Input("@I_DIRECCION", persona.C_Direccion);
                objStoreProc.Add_Par_VarChar_Input("@I_PTELEFONO", persona.C_PNumero_Telefono);
                objStoreProc.Add_Par_VarChar_Input("@I_STELEFONO", persona.C_SNumero_Telefono);
                objStoreProc.Add_Par_VarChar_Input("@I_CORREO", persona.C_Correo);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", Url_Img);
                objStoreProc.Add_Par_VarChar_Input("@I_EMPRESA", null);
                objStoreProc.Add_Par_Int_Input("@I_ESTADO", 1);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", null);
                objStoreProc.Add_Par_Datetime_Input("@I_FECHA_NACIMIENTO", persona.C_Fecha_Nacimiento);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", persona.C_Usuario_Creacion);
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


                        DataRow row = data.Rows[0];

                        string IdUsuario = row["ID_USUARIO"].ToString();
                        string Contrasenia = row["CONTRASEÑA"].ToString();
                        string correo = row["CORREO"].ToString();
                        persona.C_Roles = row["ROL"].ToString();
                        string NumeroCuenta = row["NUMERO_CUENTA"].ToString();
                        string roles = persona.C_Roles.Replace(";", "<br />");


                        string nomnbre = persona.C_Primer_Nombre = row["NOMBRE"].ToString();

                        string textoMensaje = "https://cafeinternet.srvcentral.com/";


                        string mensajefinal = $@"
                                            <html>
                                            <head>
                                                <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
                                                <style>
                                                    body {{
                                                        font-family: Arial, sans-serif;
                                                        line-height: 1.6;
                                                        color: #333;
                                                    }}

                                                    h1 {{
                                                        color: #007BFF;
                                                    }}

                                                    p {{
                                                        font-size: 16px;
                                                    }}
                                                </style>
                                            </head>
                                            <body>
                                                <div class=""container"">
                                                    <h1 class=""mt-4"">¡Bienvenido!</h1>
                                                    <p>Sus credenciales de acceso son las siguientes:</p>
                                                    <p>ID de Usuario: {IdUsuario}</p>
                                                    <p>Contraseña: {Contrasenia}</p>
                                                    <p>Numero de Cuenta: {NumeroCuenta}</p> 
                                                    <p>Rol(es):</p>
                                                    <ul class=""list-group"">
                                                        <li class=""list-group-item"">{roles}</li>
                                                    </ul>
                                                    <p><a class=""btn btn-primary"" href=""{textoMensaje}"">Ingresar al Sistema</a></p>
                                                    <p class=""mt-4"">Por motivos de seguridad, el inicio de sesión será bloqueado. Una vez intente acceder al sistema, recibirá un correo en el cual deberá seguir las instrucciones para habilitar su acceso al sistema.</p>
                                                </div>
                                            </body>
                                            </html>
                                            ";

                        string asunto = "CREDENCIALES DE ACCESO";



                        Logica.Metodos C = new Logica.Metodos();

                        C.Correos(asunto, mensajefinal, correo, nomnbre);

                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }

                    else

                    {
                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;
                        res = false;

                    }




                }

                else
                {
                    persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                persona.C_Transaccion_Estado = 26;
                persona.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool putActualizarEmpleadosG(ref PersonaEntity persona)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_PERSONA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "UA");
                objStoreProc.Add_Par_VarChar_Input("@I_VALIDAR", persona.C_Validar);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", persona.C_Id_Usuario);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_ANTERIOR", null);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_NUEVA", null);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_ROL", string.Join(",", 3));
                objStoreProc.Add_Par_Int_Input("@I_ID_ROL_PERSONA", persona.C_Id_Rol_Persona);
                objStoreProc.Add_Par_Int_Input("@I_ID_TIPO_CUENTA", persona.C_Id_Tipo_Cuenta);
                objStoreProc.Add_Par_Int_Input("@I_ID_ESTADO_CUENTA", 1);
                objStoreProc.Add_Par_Decimal_Input("@I_MONTO", persona.C_Monto);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", persona.C_Id_Persona);
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", persona.C_Id_Sucursal);
                objStoreProc.Add_Par_Int_Input("@I_ID_GENERO", persona.C_Id_Genero);
                objStoreProc.Add_Par_Int_Input("@I_ID_MUNICIPIO", persona.C_Id_Municipio);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_NOMBRE", persona.C_Primer_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_NOMBRE", persona.C_Segundo_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_TERCER_NOMBRE", persona.C_Tercer_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_APELLIDO", persona.C_Primer_Apellido);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_APELLIDO", persona.C_Segundo_Apellido);
                objStoreProc.Add_Par_VarChar_Input("@I_APELLIDO_CASADA", persona.C_Apellido_Casada);
                objStoreProc.Add_Par_VarChar_Input("@I_DPI", persona.C_DPI);
                objStoreProc.Add_Par_VarChar_Input("@I_NIT", persona.C_NIT);
                objStoreProc.Add_Par_VarChar_Input("@I_DIRECCION", persona.C_Direccion);
                objStoreProc.Add_Par_VarChar_Input("@I_PTELEFONO", persona.C_PNumero_Telefono);
                objStoreProc.Add_Par_VarChar_Input("@I_STELEFONO", persona.C_SNumero_Telefono);
                objStoreProc.Add_Par_VarChar_Input("@I_CORREO", persona.C_Correo);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", Url_Img);
                objStoreProc.Add_Par_Int_Input("@I_ESTADO", 1);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", persona.C_Descripcion);
                objStoreProc.Add_Par_VarChar_Input("@I_EMPRESA", null);
                objStoreProc.Add_Par_Datetime_Input("@I_FECHA_NACIMIENTO", persona.C_Fecha_Nacimiento);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", null);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_MODIFICACION", persona.C_Usuario_Modificacion);

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


                        DataRow row = data.Rows[0];
                        string correo = row["CORREO"].ToString();
                        persona.C_Roles = row["ROL"].ToString();
                        string roles = persona.C_Roles.Replace(";", "<br />");



                        string Nombre = persona.C_Primer_Nombre = row["NOMBRE"].ToString();




                        string mensajefinal = $@"
                                                    <html>
                                                    <head>
                                                        <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
                                                        <style>
                                                            body {{
                                                                font-family: Arial, sans-serif;
                                                                line-height: 1.6;
                                                                color: #333;
                                                                background-color: #f4f4f4;
                                                                padding: 20px;
                                                            }}

                                                            .container {{
                                                                background-color: #fff;
                                                                padding: 20px;
                                                                border-radius: 5px;
                                                                box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                                                            }}

                                                            h1 {{
                                                                color: #007BFF;
                                                                margin-bottom: 20px;
                                                            }}

                                                            p {{
                                                                font-size: 16px;
                                                                margin-bottom: 10px;
                                                            }}
                                                        </style>
                                                    </head>
                                                    <body>
                                                        <div class=""container"">
                                                            <h1>Hola:</h1>
                                                            <p>{Nombre}</p>
                                                            <p>Le informamos que sus datos fueron actualizados, los roles de acceso al sistema que posee son los siguientes</p>
                                                           <p>Rol(es):</p>
                                                    <ul class=""list-group"">
                                                        <li class=""list-group-item"">{roles}</li>
                                                    </ul>
                                                        </div>
                                                    </body>
                                                    </html>
                                                ";



                        string asunto = "Actualizacion de Datos";



                        Logica.Metodos C = new Logica.Metodos();

                        C.Correos(asunto, mensajefinal, correo, Nombre);

                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }

                    else

                    {
                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;
                        res = false;

                    }




                }

                else
                {
                    persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                persona.C_Transaccion_Estado = 26;
                persona.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }


        public bool GetEmpleadosG(ref List<PersonaEntity> personaList)
        {
            DataLayer.EntityModel.PersonaEntity persona = new DataLayer.EntityModel.PersonaEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("GET_PERSONA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "EA");
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", null);
				objStoreProc.Add_Par_VarChar_Input("@I_PARAMETRO", null);


				DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (data.Rows.Count > 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            persona = new DataLayer.EntityModel.PersonaEntity();
                            persona.C_Id_Persona = Convert.ToInt32(row["ID_PERSONA"].ToString());
                            persona.C_Id_Sucursal = Convert.ToInt32(row["ID_SL"]);
                            persona.C_Id_Usuario = row["ID_USUARIO"].ToString();
                            persona.C_Sucursal = row["SUCURSAL"].ToString();
                            persona.C_Primer_Nombre = row["NOMBRE"].ToString();
                            persona.C_Departamento = row["DEPARTAMENTO"].ToString();
                            persona.C_Municipio = row["MUNICIPIO"].ToString();
                            persona.C_Usuario_Creacion = row["USUARIO_CREACION"].ToString();
                            persona.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"] != DBNull.Value ? row["USUARIO_MODIFICACION"].ToString() : "";
                            persona.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                            persona.C_Fecha_Modificacion = row["FECHA_MODIFICACION"] != DBNull.Value ? row["FECHA_MODIFICACION"].ToString() : "";
                            persona.C_Img_Base = row["URL_IMG"].ToString();
                            persona.C_Estado = (bool)row["ESTADO"] ? 1 : 2;
                            string rolValue = row["ROL"].ToString();
                            persona.C_ID_ROL = string.IsNullOrEmpty(rolValue) ? listempty : rolValue.Split(';').ToList();


                            if (idPersona == persona.C_Id_Sucursal)
                            {
                                personaList.Add(persona);
                            }


                        }

                        res = true;

                    }
                    else
                    {
                        persona.C_Transaccion_Estado = 33;
                        persona.C_Transaccion_Mensaje = "No hay registros";
                        personaList.Add(persona);
                        res = false;
                    }

                }
                else
                {

                    persona.C_Transaccion_Estado = 32;
                    persona.C_Transaccion_Mensaje = msgResEjecucion;
                    personaList.Add(persona);
                    res = false;
                }
            }
            catch (Exception e)
            {
                persona.C_Transaccion_Estado = 35;
                persona.C_Transaccion_Mensaje = e.Message;
                personaList.Add(persona);
                res = false;
            }
            return res;
        }




        public bool GetProveedor(ref List<PersonaEntity> personaList)
        {
            DataLayer.EntityModel.PersonaEntity persona = new DataLayer.EntityModel.PersonaEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("GET_PERSONA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "CP");
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", null);

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (data.Rows.Count > 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            persona = new DataLayer.EntityModel.PersonaEntity();
                            persona.C_Id_Persona = Convert.ToInt32(row["ID_PERSONA"].ToString());
                            persona.C_Id_Rol_Persona = Convert.ToInt32(row["ID_ROL_PERSONA"].ToString());
                            persona.C_Sucursal = row["SUCURSAL"].ToString();
                            persona.C_Primer_Nombre = row["NOMBRE"].ToString();
                            persona.C_Departamento = row["DEPARTAMENTO"].ToString();
                            persona.C_Municipio = row["MUNICIPIO"].ToString();
                            persona.C_Usuario_Creacion = row["USUARIO_CREACION"].ToString();
                            persona.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"] != DBNull.Value ? row["USUARIO_MODIFICACION"].ToString() : "";
                            persona.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                            persona.C_Fecha_Modificacion = row["FECHA_MODIFICACION"] != DBNull.Value ? row["FECHA_MODIFICACION"].ToString() : "";
                            persona.C_Img_Base = row["URL_IMG"].ToString();
                            persona.C_Estado = (bool)row["ESTADO"] ? 1 : 2;
                            persona.C_Tipo = row["Tipo"].ToString();
                            persona.C_Empresa = row["EMPRESA"] != DBNull.Value ? row["EMPRESA"].ToString() : "";

                            if (persona.C_Id_Rol_Persona == 3) 
                            {
                                personaList.Add(persona);

                            }
                           

                        }

                        res = true;

                    }
                    else
                    {
                        persona.C_Transaccion_Estado = 33;
                        persona.C_Transaccion_Mensaje = "No hay registros";
                        personaList.Add(persona);
                        res = false;
                    }

                }
                else
                {

                    persona.C_Transaccion_Estado = 32;
                    persona.C_Transaccion_Mensaje = msgResEjecucion;
                    personaList.Add(persona);
                    res = false;
                }
            }
            catch (Exception e)
            {
                persona.C_Transaccion_Estado = 35;
                persona.C_Transaccion_Mensaje = e.Message;
                personaList.Add(persona);
                res = false;
            }
            return res;
        }

        public bool GetCliente(ref List<PersonaEntity> personaList)
        {
            DataLayer.EntityModel.PersonaEntity persona = new DataLayer.EntityModel.PersonaEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("GET_PERSONA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "CP");
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", null);
				objStoreProc.Add_Par_VarChar_Input("@I_PARAMETRO", null);

				DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (data.Rows.Count > 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            persona = new DataLayer.EntityModel.PersonaEntity();
                            persona.C_Id_Persona = Convert.ToInt32(row["ID_PERSONA"].ToString());
                            persona.C_Id_Rol_Persona = Convert.ToInt32(row["ID_ROL_PERSONA"].ToString());
                            persona.C_Sucursal = row["SUCURSAL"].ToString();
                            persona.C_Primer_Nombre = row["NOMBRE"].ToString();
                            persona.C_Departamento = row["DEPARTAMENTO"].ToString();
                            persona.C_Municipio = row["MUNICIPIO"].ToString();
                            persona.C_Usuario_Creacion = row["USUARIO_CREACION"].ToString();
                            persona.C_Usuario_Modificacion = row["USUARIO_MODIFICACION"] != DBNull.Value ? row["USUARIO_MODIFICACION"].ToString() : "";
                            persona.C_Fecha_Creacion = row["FECHA_CREACION"].ToString();
                            persona.C_Fecha_Modificacion = row["FECHA_MODIFICACION"] != DBNull.Value ? row["FECHA_MODIFICACION"].ToString() : "";
                            persona.C_Img_Base = row["URL_IMG"].ToString();
                            persona.C_Estado = (bool)row["ESTADO"] ? 1 : 2;
                            persona.C_Tipo = row["Tipo"].ToString();
                            persona.C_Empresa = row["EMPRESA"] != DBNull.Value ? row["EMPRESA"].ToString() : "";

                            if (persona.C_Id_Rol_Persona == 2)
                            {
                                personaList.Add(persona);

                            }


                        }

                        res = true;

                    }
                    else
                    {
                        persona.C_Transaccion_Estado = 33;
                        persona.C_Transaccion_Mensaje = "No hay registros";
                        personaList.Add(persona);
                        res = false;
                    }

                }
                else
                {

                    persona.C_Transaccion_Estado = 32;
                    persona.C_Transaccion_Mensaje = msgResEjecucion;
                    personaList.Add(persona);
                    res = false;
                }
            }
            catch (Exception e)
            {
                persona.C_Transaccion_Estado = 35;
                persona.C_Transaccion_Mensaje = e.Message;
                personaList.Add(persona);
                res = false;
            }
            return res;
        }

        public bool SetProveedor(ref PersonaEntity persona)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_PERSONA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "P");
                objStoreProc.Add_Par_VarChar_Input("@I_VALIDAR", persona.C_Validar);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", null);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_ANTERIOR", null);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_NUEVA", null);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_ROL", null);
                objStoreProc.Add_Par_Int_Input("@I_ID_ROL_PERSONA", 3);
                objStoreProc.Add_Par_Int_Input("@I_ID_TIPO_CUENTA", persona.C_Id_Tipo_Cuenta);
                objStoreProc.Add_Par_Int_Input("@I_ID_ESTADO_CUENTA", 1);
                objStoreProc.Add_Par_Decimal_Input("@I_MONTO", persona.C_Monto);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", persona.C_Id_Sucursal);
                objStoreProc.Add_Par_Int_Input("@I_ID_GENERO", persona.C_Id_Genero);
                objStoreProc.Add_Par_Int_Input("@I_ID_MUNICIPIO", persona.C_Id_Municipio);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_NOMBRE", persona.C_Primer_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_NOMBRE", persona.C_Segundo_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_TERCER_NOMBRE", persona.C_Tercer_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_APELLIDO", persona.C_Primer_Apellido);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_APELLIDO", persona.C_Segundo_Apellido);
                objStoreProc.Add_Par_VarChar_Input("@I_APELLIDO_CASADA", persona.C_Apellido_Casada);
                objStoreProc.Add_Par_VarChar_Input("@I_DPI", persona.C_DPI);
                objStoreProc.Add_Par_VarChar_Input("@I_NIT", persona.C_NIT);
                objStoreProc.Add_Par_VarChar_Input("@I_DIRECCION", persona.C_Direccion);
                objStoreProc.Add_Par_VarChar_Input("@I_PTELEFONO", persona.C_PNumero_Telefono);
                objStoreProc.Add_Par_VarChar_Input("@I_STELEFONO", persona.C_SNumero_Telefono);
                objStoreProc.Add_Par_VarChar_Input("@I_CORREO", persona.C_Correo);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", Url_Img);
                objStoreProc.Add_Par_VarChar_Input("@I_EMPRESA", persona.C_Empresa);
                objStoreProc.Add_Par_Int_Input("@I_ESTADO", 1);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", null);
                objStoreProc.Add_Par_Datetime_Input("@I_FECHA_NACIMIENTO", persona.C_Fecha_Nacimiento);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", persona.C_Usuario_Creacion);
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


                        DataRow row = data.Rows[0];

                        string Nombre = row["NOMBRE"].ToString();
                        string correo = row["CORREO"].ToString();
                        string C_Rol = row["ROL"].ToString();
                        string NumeroCuenta = row["NUMERO_CUENTA"] != DBNull.Value ? row["NUMERO_CUENTA"].ToString() : "";
                        string Tipo = row["TIPO"].ToString();


                        string textoMensaje = "";

                        string mensajefinal;

                        if (NumeroCuenta == "")
                        {
                            mensajefinal
                         = $@"
    <html>
    <head>
        <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
        <style>
            body {{
                font-family: Arial, sans-serif;
                line-height: 1.6;
                color: #333;
                background-color: #f4f4f4;
                padding: 20px;
            }}

            .container {{
                background-color: #fff;
                padding: 20px;
                border-radius: 5px;
                box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            }}

            h1 {{
                color: #007BFF;
                margin-bottom: 20px;
            }}

            p {{
                font-size: 16px;
                margin-bottom: 10px;
            }}
        </style>
    </head>
    <body>
        <div class=""container"">
            <h1>Hola:</h1>
            <p>{Nombre}</p>
            <p>Agradecemos ser un: {C_Rol} de nuestra empresa.</p>
        </div>
    </body>
    </html>
";
                        }
                        else 
                        {
                            mensajefinal
                        = $@"
    <html>
    <head>
        <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
        <style>
            body {{
                font-family: Arial, sans-serif;
                line-height: 1.6;
                color: #333;
                background-color: #f4f4f4;
                padding: 20px;
            }}

            .container {{
                background-color: #fff;
                padding: 20px;
                border-radius: 5px;
                box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            }}

            h1 {{
                color: #007BFF;
                margin-bottom: 20px;
            }}

            p {{
                font-size: 16px;
                margin-bottom: 10px;
            }}
        </style>
    </head>
    <body>
        <div class=""container"">
            <h1>Hola:</h1>
            <p>{Nombre}</p>
            <p>Agradecemos ser un: {C_Rol} de nuestra empresa.</p>
            <p>Asimismo, le informamos de su:</p>
            <p>Número de Cuenta: {NumeroCuenta}</p> 
            <p>Tipo: {Tipo}</p>
        </div>
    </body>
    </html>
";


                        }





                        string asunto = "Informacion";



                        Logica.Metodos C = new Logica.Metodos();

                        C.Correos(asunto, mensajefinal, correo, Nombre);

                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }

                    else

                    {
                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;
                        res = false;

                    }




                }

                else
                {
                    persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                persona.C_Transaccion_Estado = 26;
                persona.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool GetProveedorById(ref PersonaEntity persona)
        {
            DataLayer.EntityModel.PersonaEntity Persona = new DataLayer.EntityModel.PersonaEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("GET_PERSONA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "TP");
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", this.idPersona);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", null);
				objStoreProc.Add_Par_VarChar_Input("@I_PARAMETRO", null);

				DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (data.Rows.Count > 0)
                    {

                        string Cuenta;
                        foreach (DataRow row in data.Rows)
                        {

                            persona.C_Id_Persona = Convert.ToInt32(row["ID_PERSONA"].ToString());

                           

                            Cuenta = row["ID_CUENTA"] != DBNull.Value ? row["ID_CUENTA"].ToString() : "";

                            if (Cuenta != "")
                            {
                                persona.C_Id_Cuenta = "T";
                            }
                            else 
                            { 
                                persona.C_Id_Cuenta = "F";
                            }
							persona.C_Monto = Convert.ToDecimal(row["MONTO_MAX"].ToString());
							persona.C_Id_Sucursal = Convert.ToInt32(row["ID_SL"].ToString());
                            persona.C_Id_Tipo_Cuenta = row["ID_TIPO_CUENTA"] != DBNull.Value ? (int)row["ID_TIPO_CUENTA"] : 3;
                            persona.C_Id_Genero = Convert.ToInt32(row["ID_GENERO"].ToString());
                            persona.C_Id_Rol_Persona = Convert.ToInt32(row["ID_ROL_PERSONA"].ToString());
                            persona.C_Id_Departamento = Convert.ToInt32(row["ID_DEPTO"].ToString());
                            persona.C_Id_Municipio = Convert.ToInt32(row["ID_MUN"].ToString());
                            persona.C_Primer_Nombre = row["PRIMER_NOMBRE"].ToString();
                            persona.C_Segundo_Nombre = row["SEGUNDO_NOMBRE"].ToString();
                            persona.C_Tercer_Nombre = row["TERCER_NOMBRE"] != DBNull.Value ? row["TERCER_NOMBRE"].ToString() : "";
                            persona.C_Primer_Apellido = row["PRIMER_APELLIDO"].ToString();
                            persona.C_Segundo_Apellido = row["SEGUNDO_APELLIDO"].ToString();
                            persona.C_Apellido_Casada = row["APELLIDO_CASADA"] != DBNull.Value ? row["APELLIDO_CASADA"].ToString() : "";
                            persona.C_DPI = row["DPI"].ToString();
                            persona.C_NIT = row["NIT"].ToString();
                            persona.C_Direccion = row["DIRECCION"].ToString();
                            persona.C_PNumero_Telefono = row["PTELEFONO"].ToString();
                            persona.C_SNumero_Telefono = row["STELEFONO"] != DBNull.Value ? row["STELEFONO"].ToString() : "";
                            persona.C_Correo = row["CORREO"].ToString();
                            persona.C_Fecha_Nacimiento = Convert.ToDateTime(row["FECHA_NACIMIENTO"]);
                            persona.C_Img_Base = row["URL_IMG"].ToString();
                            persona.C_Empresa = row["EMPRESA"] != DBNull.Value ? row["EMPRESA"].ToString() : "";
                            persona.C_Descripcion = row["DESCRIPCION"].ToString();




                        }

                        res = true;

                    }
                    else
                    {
                        persona.C_Transaccion_Estado = 33;
                        persona.C_Transaccion_Mensaje = "No hay registros";

                        res = false;
                    }

                }
                else
                {

                    persona.C_Transaccion_Estado = 32;
                    persona.C_Transaccion_Mensaje = msgResEjecucion;

                    res = false;
                }
            }
            catch (Exception e)
            {
                persona.C_Transaccion_Estado = 35;
                persona.C_Transaccion_Mensaje = e.Message;

                res = false;
            }
            return res;
        }


        public bool SetCliente(ref PersonaEntity persona)
        {
            bool res = false;


            try
            {


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("SP_SET_PERSONA", "", "");

                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "P");
                objStoreProc.Add_Par_VarChar_Input("@I_VALIDAR", persona.C_Validar);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", null);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_ANTERIOR", null);
                objStoreProc.Add_Par_VarChar_Input("@I_CONTRASENIA_NUEVA", null);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_ROL", null);
                objStoreProc.Add_Par_Int_Input("@I_ID_ROL_PERSONA", 2);
                objStoreProc.Add_Par_Int_Input("@I_ID_TIPO_CUENTA", persona.C_Id_Tipo_Cuenta);
                objStoreProc.Add_Par_Int_Input("@I_ID_ESTADO_CUENTA", 1);
                objStoreProc.Add_Par_Decimal_Input("@I_MONTO", persona.C_Monto);
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_Int_Input("@I_ID_SUCURSAL", persona.C_Id_Sucursal);
                objStoreProc.Add_Par_Int_Input("@I_ID_GENERO", persona.C_Id_Genero);
                objStoreProc.Add_Par_Int_Input("@I_ID_MUNICIPIO", persona.C_Id_Municipio);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_NOMBRE", persona.C_Primer_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_NOMBRE", persona.C_Segundo_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_TERCER_NOMBRE", persona.C_Tercer_Nombre);
                objStoreProc.Add_Par_VarChar_Input("@I_PRIMER_APELLIDO", persona.C_Primer_Apellido);
                objStoreProc.Add_Par_VarChar_Input("@I_SEGUNDO_APELLIDO", persona.C_Segundo_Apellido);
                objStoreProc.Add_Par_VarChar_Input("@I_APELLIDO_CASADA", persona.C_Apellido_Casada);
                objStoreProc.Add_Par_VarChar_Input("@I_DPI", persona.C_DPI);
                objStoreProc.Add_Par_VarChar_Input("@I_NIT", persona.C_NIT);
                objStoreProc.Add_Par_VarChar_Input("@I_DIRECCION", persona.C_Direccion);
                objStoreProc.Add_Par_VarChar_Input("@I_PTELEFONO", persona.C_PNumero_Telefono);
                objStoreProc.Add_Par_VarChar_Input("@I_STELEFONO", persona.C_SNumero_Telefono);
                objStoreProc.Add_Par_VarChar_Input("@I_CORREO", persona.C_Correo);
                objStoreProc.Add_Par_VarChar_Input("@I_URL_IMG", Url_Img);
                objStoreProc.Add_Par_VarChar_Input("@I_EMPRESA", persona.C_Empresa);
                objStoreProc.Add_Par_Int_Input("@I_ESTADO", 1);
                objStoreProc.Add_Par_VarChar_Input("@I_DESCRIPCION", null);
                objStoreProc.Add_Par_Datetime_Input("@I_FECHA_NACIMIENTO", persona.C_Fecha_Nacimiento);
                objStoreProc.Add_Par_VarChar_Input("@I_USUARIO_CREACION", persona.C_Usuario_Creacion);
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


                        DataRow row = data.Rows[0];

                        string Nombre = row["NOMBRE"].ToString();
                        string correo = row["CORREO"].ToString();
                        string C_Rol = row["ROL"].ToString();
                        string NumeroCuenta = row["NUMERO_CUENTA"] != DBNull.Value ? row["NUMERO_CUENTA"].ToString() : "";
                        string Tipo = row["TIPO"].ToString();

						persona.C_Id_Persona = Convert.ToInt32(row["ID_PERSONA"]);  



						string textoMensaje = "";

                        string mensajefinal;

                        if (NumeroCuenta == "")
                        {
                            mensajefinal
                         = $@"
    <html>
    <head>
        <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
        <style>
            body {{
                font-family: Arial, sans-serif;
                line-height: 1.6;
                color: #333;
                background-color: #f4f4f4;
                padding: 20px;
            }}

            .container {{
                background-color: #fff;
                padding: 20px;
                border-radius: 5px;
                box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            }}

            h1 {{
                color: #007BFF;
                margin-bottom: 20px;
            }}

            p {{
                font-size: 16px;
                margin-bottom: 10px;
            }}
        </style>
    </head>
    <body>
        <div class=""container"">
            <h1>Hola:</h1>
            <p>{Nombre}</p>
            <p>Agradecemos ser un: {C_Rol} de nuestra empresa.</p>
        </div>
    </body>
    </html>
";
                        }
                        else
                        {
                            mensajefinal
                        = $@"
    <html>
    <head>
        <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
        <style>
            body {{
                font-family: Arial, sans-serif;
                line-height: 1.6;
                color: #333;
                background-color: #f4f4f4;
                padding: 20px;
            }}

            .container {{
                background-color: #fff;
                padding: 20px;
                border-radius: 5px;
                box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            }}

            h1 {{
                color: #007BFF;
                margin-bottom: 20px;
            }}

            p {{
                font-size: 16px;
                margin-bottom: 10px;
            }}
        </style>
    </head>
    <body>
        <div class=""container"">
            <h1>Hola:</h1>
            <p>{Nombre}</p>
            <p>Agradecemos ser un: {C_Rol} de nuestra empresa.</p>
            <p>Asimismo, le informamos de su:</p>
            <p>Número de Cuenta: {NumeroCuenta}</p> 
            <p>Tipo: {Tipo}</p>
        </div>
    </body>
    </html>
";


                        }





                        string asunto = "Informacion";



                        Logica.Metodos C = new Logica.Metodos();

                        C.Correos(asunto, mensajefinal, correo, Nombre);

                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;

                        res = true;
                    }

                    else

                    {
                        persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                        persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE;
                        res = false;

                    }




                }

                else
                {
                    persona.C_Transaccion_Estado = O_TRANSACCION_ESTADO;
                    persona.C_Transaccion_Mensaje = O_TRANSACCION_MENSAJE + msgResEjecucion;
                    res = false;
                }

            }

            catch (Exception e)
            {

                persona.C_Transaccion_Estado = 26;
                persona.C_Transaccion_Mensaje = "Hubo un Error" + e.Message;
                return false;

            }

            return res;
        }

        public bool GetPersonaFacturacion(ref List<PersonaEntity> personaList)
        {
            DataLayer.EntityModel.PersonaEntity persona = new DataLayer.EntityModel.PersonaEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("GET_PERSONA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "TN");
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", 0);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", null);
				objStoreProc.Add_Par_VarChar_Input("@I_PARAMETRO", null);

				DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    List<string> listempty = new List<string>();

                    if (data.Rows.Count > 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            persona = new DataLayer.EntityModel.PersonaEntity();

                            persona.C_Id_Persona = Convert.ToInt32(row["ID_PERSONA"].ToString());
                            persona.C_Primer_Nombre = row["NOMBRE"].ToString();
                            persona.C_Img_Base = row["URL_IMG"].ToString();

                            personaList.Add(persona);

                        }

                        res = true;

                    }
                    else
                    {
                        persona.C_Transaccion_Estado = 33;
                        persona.C_Transaccion_Mensaje = "No hay registros";
                        personaList.Add(persona);
                        res = false;
                    }

                }
                else
                {

                    persona.C_Transaccion_Estado = 32;
                    persona.C_Transaccion_Mensaje = msgResEjecucion;
                    personaList.Add(persona);
                    res = false;
                }
            }
            catch (Exception e)
            {
                persona.C_Transaccion_Estado = 35;
                persona.C_Transaccion_Mensaje = e.Message;
                personaList.Add(persona);
                res = false;
            }
            return res;
        }

        
        public bool GetPersonaFacturacionById(ref PersonaEntity persona)
        {
            DataLayer.EntityModel.PersonaEntity Persona = new DataLayer.EntityModel.PersonaEntity();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjectProcAlm objStoreProc = new EjectProcAlm("GET_PERSONA", "", "");
                objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "TD");
                objStoreProc.Add_Par_Int_Input("@I_ID_PERSONA", this.idPersona);
                objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", null);
				objStoreProc.Add_Par_VarChar_Input("@I_PARAMETRO", null);

				DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
              

                    if (data.Rows.Count > 0)
                    {

                     

							DataRow row = data.Rows[0];
							persona.C_Id_Persona = Convert.ToInt32(row["ID_PERSONA"].ToString());
                            persona.C_Saldo = Convert.ToDecimal(row["SALDO"]);
                            persona.C_Tipo_Cuenta = row["DESCRIPCION"].ToString();
							persona.C_Monto = Convert.ToDecimal(row["MONTO_MAX"].ToString());
						

						res = true;

                    }
                    else
                    {
                        persona.C_Transaccion_Estado = 33;
                        persona.C_Transaccion_Mensaje = "No hay registros";

                        res = false;
                    }

                }
                else
                {

                    persona.C_Transaccion_Estado = 32;
                    persona.C_Transaccion_Mensaje = msgResEjecucion;

                    res = false;
                }
            }
            catch (Exception e)
            {
                persona.C_Transaccion_Estado = 35;
                persona.C_Transaccion_Mensaje = e.Message;

                res = false;
            }
            return res;
        }

		public bool GetPersonaFacturacionByParametro(ref List<PersonaEntity> persona)
		{
			DataLayer.EntityModel.PersonaEntity Persona = new DataLayer.EntityModel.PersonaEntity();
			bool res = false;
			try
			{
				if (Url_Img is null)
				{
					Url_Img = "/";
				}



				IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				/*Validar usuario en BD*/
				EjectProcAlm objStoreProc = new EjectProcAlm("GET_PERSONA", "", "");
				objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "PF");
				objStoreProc.Add_Par_VarChar_Input("@I_ID_PERSONA", null);
				objStoreProc.Add_Par_VarChar_Input("@I_ID_USUARIO", null);
				objStoreProc.Add_Par_VarChar_Input("@I_PARAMETRO", Url_Img );

				DataTable data = new DataTable();
				string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


				if (string.IsNullOrEmpty(msgResEjecucion))
				{


					if (data.Rows.Count > 0)
					{

						foreach (DataRow row in data.Rows)
						{
							Persona = new DataLayer.EntityModel.PersonaEntity();
							Persona.C_Id_Persona = Convert.ToInt32(row["ID_PERSONA"].ToString());
                            Persona.C_Primer_Nombre = row["NOMBRE"].ToString();
							Persona.C_Url_Img = row["URL_IMG"].ToString();


                            
                                persona.Add(Persona);
								
							
							

						}
						res = true;


					}
					else 
					{
						Persona.C_Transaccion_Estado = 33;
					    Persona.C_Transaccion_Mensaje = "C/F";
						persona.Add(Persona);
						res = false;
					}

				}
				else
				{

					Persona.C_Transaccion_Estado = 32;
					Persona.C_Transaccion_Mensaje = msgResEjecucion;
					persona.Add(Persona);
					res = false;
				}
			}
			catch (Exception e)
			{
				Persona.C_Transaccion_Estado = 35;
				Persona.C_Transaccion_Mensaje = e.Message;
				persona.Add(Persona);
				res = false;
			}
			return res;
		}
	}
}
