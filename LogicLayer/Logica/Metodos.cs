
using System.Net.Mail;
using System.Net;
using Org.BouncyCastle.Utilities.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Data.SqlClient;
using DataLayer.Conexion;
using LogicLayer.Persona;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text.Json.Serialization;

namespace LogicLayer.Logica
{
    public class Metodos 
    {




		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string ImgNombre { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string ImagenBase64 { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string URL { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string TransaccionMensaje { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public int TransaccionEstado { get; set; }





		public bool Correos(string asunto, string mensajefinal,string correo, string nomnbre)
        {
            bool res = false;
            DataLayer.EntityModel.LogInEntity data = new DataLayer.EntityModel.LogInEntity();
            try {
               
               

                var fromAddress = new MailAddress("solution@srvcentral.com", "Central Solution");
                var toAddress = new MailAddress(correo, nomnbre);
                const string fromPassword = "7#530ltkF";

                var smtp = new SmtpClient
                {
                    Host = "mail.srvcentral.com",
                    Port = 587, // Usa el puerto que corresponda a tu servidor SMTP  
                    EnableSsl = false, // Dependiendo de tu servidor, puede que necesites habilitar SSL  
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = asunto,
                    Body = mensajefinal
                })
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);




                    res = true;

                }

            }
            catch (Exception ex)
            {
                data.C_Transaccion_Estado = 22;
                data.C_Transaccion_Mensaje ="Fallo el envio de correo "+ ex.Message;

                res = false;
            }
           
         return res;
        }





        public async Task<string> SubirIMGAsync(string? bImg, string? nombre, string? directorio)
        {
            DataLayer.EntityModel.BaseImgEntity img = new DataLayer.EntityModel.BaseImgEntity();
            try
            {
                if (!string.IsNullOrEmpty(bImg))
                {
                    // Convertir la imagen Base64 a bytes
                    byte[] imagenBytes = Convert.FromBase64String(bImg);

                    // Generar un nombre de archivo único
                    string fileName = nombre + $"{Guid.NewGuid()}.jpg";

                    // Establecer las credenciales de FTP
                    string ftpServerUrl = "ftp://ftp.srvcentral.com";
                    string ftpUsername = "img";
                    string ftpPassword = "G_r44tt21";
                    string ftpDirectory = directorio;

                    // Construir la URL completa del archivo en el servidor FTP
                    string ftpFilePath = $"{ftpServerUrl}/{ftpDirectory}/{fileName}";

                    // Crear una instancia de solicitud FTP
                    FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpFilePath);
                    ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                    ftpRequest.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

                    // Establecer el modo pasivo para la conexión FTP
                    ftpRequest.UsePassive = true;

                    // Guardar la imagen en el servidor remoto
                    using (Stream ftpStream = ftpRequest.GetRequestStream())
                    {
                        await ftpStream.WriteAsync(imagenBytes, 0, imagenBytes.Length);
                    }

                    // Obtener la URL de la imagen en el directorio remoto
                    string imageUrl = $"http://img.srvcentral.com/{ftpDirectory}/{fileName}";

                    return imageUrl;
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return $"Error al subir la imagen: {ex.Message}";
            }
        }

    }




}




