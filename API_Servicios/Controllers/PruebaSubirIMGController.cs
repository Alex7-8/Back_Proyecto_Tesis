
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using FluentFTP;
using static System.Net.WebRequestMethods;

namespace TuProyecto.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ImagenController : ControllerBase
    {
        [HttpPost]
        [Route("subir")]
        public IActionResult SubirImagen([FromBody] ImagenRequest request)
        {
            try
            {
                if (!string.IsNullOrEmpty(request.ImagenBase64))
                {
                    // Convertir la imagen Base64 a bytes
                    byte[] imagenBytes = Convert.FromBase64String(request.ImagenBase64);

                    // Generar un nombre de archivo único
                    string fileName = $"{Guid.NewGuid()}.jpg";

                    // Establecer las credenciales de FTP
                    string ftpServerUrl = "ftp://ftp.srvcentral.com";
                    string ftpUsername = "img";
                    string ftpPassword = "G_r44tt21";
                    string ftpDirectory = "Images";

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
                        ftpStream.Write(imagenBytes, 0, imagenBytes.Length);
                    }

                    // Obtener la URL de la imagen en el directorio remoto
                    string imageUrl = $"{"img.srvcentral.com"}/{ftpDirectory}/{fileName}";

                    return Ok(new { imageUrl });
                }
                else
                {
                    return BadRequest("No se ha enviado ninguna imagen en formato Base64.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al subir la imagen: {ex.Message}");
            }
        }
    }

    public class ImagenRequest
    {
        public string ImagenBase64 { get; set; }
    }

}

