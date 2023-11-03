using LogicLayer.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LogicLayer;
using LogicLayer.Servicios;
using DataLayer.EntityModel;
using Org.BouncyCastle.Crypto;
using LogicLayer.Logica;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiciosController : ControllerBase
    {

        [HttpPost]
        [Route("SetServicios")]
       // [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<object>> SetServicios([FromBody] DataLayer.EntityModel.ServiciosEntity serviciosEntity)
        {

            string bImg = serviciosEntity.C_Img_Base, nombre = serviciosEntity.C_Nombre_Servicio;

            LogicLayer.Logica.Metodos metodos = new LogicLayer.Logica.Metodos();

            if (!bImg.Equals("0"))
            {
                serviciosEntity.C_Url_IMG = await metodos.SubirIMGAsync(bImg, nombre, "ImgServicio");
            }
            else
            {
                serviciosEntity.C_Url_IMG = "https://img.srvcentral.com/Sistema/ImagenPorDefecto/Servicios.jpg";
            }
            Servicios servicios = new Servicios();

                    if (servicios.SetServicios(ref serviciosEntity))
                    {
                        return Ok(new
                        {
                            ok = true,
                            Transaccion_Estado = serviciosEntity.C_Transaccion_Estado,
                            Transaccion_Mensaje = serviciosEntity.C_Transaccion_Mensaje,
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            ok = false,
                            Transaccion_Estado = serviciosEntity.C_Transaccion_Estado,
                            Transaccion_Mensaje = serviciosEntity.C_Transaccion_Mensaje,
                        });
                    }
        }

        [HttpPost]
        [Route("CambiarEstadoServicio")]
        public ActionResult<object>CambiarEstadoServicio(int IdS, string IdUM, string razon)
        {
        DataLayer.EntityModel.ServiciosEntity serviciosEntity = new DataLayer.EntityModel.ServiciosEntity();

        Servicios servicios = new Servicios(IdS,IdUM,razon);

         

            if (servicios.CambiarEstadoServicio(ref serviciosEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Transaccion_Estado = serviciosEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = serviciosEntity.C_Transaccion_Mensaje,

                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = serviciosEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = serviciosEntity.C_Transaccion_Mensaje,
                });
            }

        }


        [HttpPost]
        [Route("PutServicios")]
        // [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<object>> PutServicios([FromBody] DataLayer.EntityModel.ServiciosEntity serviciosEntity)

        {
            string bImg = serviciosEntity.C_Img_Base, nombre = serviciosEntity.C_Nombre_Servicio;
            LogicLayer.Logica.Metodos metodos = new LogicLayer.Logica.Metodos();
            if (!bImg.Equals("0") && !(bImg.StartsWith("http://") || bImg.StartsWith("https://")))
            {

                serviciosEntity.C_Url_IMG = await metodos.SubirIMGAsync(bImg, nombre, "ImgServicio");
            }

            Servicios servicios = new Servicios();

            if (servicios.PutServicios(ref serviciosEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Transaccion_Estado = serviciosEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = serviciosEntity.C_Transaccion_Mensaje,
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = serviciosEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = serviciosEntity.C_Transaccion_Mensaje,
                });
            }

        }


        [HttpGet]
        [Route("GetServicios")]
        public ActionResult<object> GetServicios()
        {
            List<DataLayer.EntityModel.ServiciosEntity> serviciosEntity = new List<DataLayer.EntityModel.ServiciosEntity>();
            Servicios servicios = new Servicios();



            if (servicios.GetServicios(ref serviciosEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = serviciosEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = serviciosEntity
                });
            }

        }



        [HttpGet]
        [Route("GetServiciosById")]
        public ActionResult<object> GetServiciosById(int IdS)
        {
            DataLayer.EntityModel.ServiciosEntity serviciosEntity = new DataLayer.EntityModel.ServiciosEntity();
            Servicios servicios = new Servicios(IdS);

            if (servicios.GetServiciosById(ref serviciosEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = serviciosEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = serviciosEntity
                });
            }

        }

    }

}