
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FluentFTP;
using static System.Net.WebRequestMethods;
using System.Net.Http.Headers;
using System.Text;

namespace TuProyecto.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ImagenControllerCpia : ControllerBase
    {
        [HttpPost]
        [Route("subir")]
        public async Task<IActionResult> SubirImagen([FromBody] ImagenRequests request)
        {
            try
            {
                if (!string.IsNullOrEmpty(request.ImagenBase64))
                {
                    byte[] imagenBytes = Convert.FromBase64String(request.ImagenBase64);

                    string fileName = $"{Guid.NewGuid()}.jpg";
                    string ftpServerUrl = "ftp://ftp.srvcentral.com";
                    string ftpUsername = "img";
                    string ftpPassword = "G_r44tt21";
                    string ftpDirectory = "Images";
                    string ftpFilePath = $"{ftpServerUrl}/{ftpDirectory}/{fileName}";

                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ftpUsername}:{ftpPassword}")));

                        using (var content = new ByteArrayContent(imagenBytes))
                        {
                            HttpResponseMessage response = await httpClient.PostAsync(ftpFilePath, content);

                            if (response.IsSuccessStatusCode)
                            {
                                string imageUrl = $"http://img.srvcentral.com/{ftpDirectory}/{fileName}";
                                return Ok(new { imageUrl });
                            }
                            else
                            {
                                return StatusCode(StatusCodes.Status500InternalServerError, "Error al subir la imagen al servidor FTP.");
                            }
                        }
                    }
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

    public class ImagenRequests
    {
        public string ImagenBase64 { get; set; }
    }

}

