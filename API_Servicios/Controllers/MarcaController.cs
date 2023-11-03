using LogicLayer.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LogicLayer;
using LogicLayer.Categoria;
using DataLayer.EntityModel;
using Org.BouncyCastle.Crypto;
using LogicLayer.Marca;
using LogicLayer.Logica;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarcaController : ControllerBase
    {

        [HttpPost]
        [Route("SetMarca")]
       // [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<object>> SetMarca([FromBody] DataLayer.EntityModel.MarcaEntity MarcaEntity)
        {

            string bImg = MarcaEntity.C_Img_Base, nombre = MarcaEntity.C_Nombre;

            LogicLayer.Logica.Metodos metodos = new LogicLayer.Logica.Metodos();
            if (!bImg.Equals("0"))
            {
                MarcaEntity.C_Url_IMG = await metodos.SubirIMGAsync(bImg, nombre, "ImgMarca");
            }
            else
            {
                MarcaEntity.C_Url_IMG = "https://img.srvcentral.com/Sistema/ImagenPorDefecto/Marca.jpg";
            }
            Marca Marca = new Marca();

                    if (Marca.SetMarca(ref MarcaEntity))
                    {
                        return Ok(new
                        {
                            ok = true,
                            Transaccion_Estado = MarcaEntity.C_Transaccion_Estado,
                            Transaccion_Mensaje = MarcaEntity.C_Transaccion_Mensaje
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            ok = false,
                            Transaccion_Estado = MarcaEntity.C_Transaccion_Estado,
                            Transaccion_Mensaje = MarcaEntity.C_Transaccion_Mensaje
                        });
                    }
        }

        [HttpPost]
        [Route("CambiarEstadoMarca")]
        public ActionResult<object> CambiarEstadoMarca(int IdMarca, string IdUM, string razon)
        {
           DataLayer.EntityModel.MarcaEntity MarcaEntity = new DataLayer.EntityModel.MarcaEntity();

            Marca Marca = new Marca(IdMarca,IdUM,razon);

         

            if (Marca.CambiarEstadoMarca(ref MarcaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Transaccion_Estado = MarcaEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = MarcaEntity.C_Transaccion_Mensaje,

                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = MarcaEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = MarcaEntity.C_Transaccion_Mensaje,
                });
            }

        }


        [HttpPost]
        [Route("PutMarca")]
       // [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<object>> PutMarca([FromBody] DataLayer.EntityModel.MarcaEntity MarcaEntity)
        {
            string bImg = MarcaEntity.C_Img_Base, nombre = MarcaEntity.C_Nombre;
            LogicLayer.Logica.Metodos metodos = new LogicLayer.Logica.Metodos();

            if (!bImg.Equals("0") && !(bImg.StartsWith("http://") || bImg.StartsWith("https://")))
            {

                MarcaEntity.C_Url_IMG = await metodos.SubirIMGAsync(bImg, nombre, "ImgMarca");
            }


            Marca Marca = new Marca();

            if (Marca.PutMarca(ref MarcaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Transaccion_Estado = MarcaEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = MarcaEntity.C_Transaccion_Mensaje,
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = MarcaEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = MarcaEntity.C_Transaccion_Mensaje,
                });
            }

        }


        [HttpGet]
        [Route("GetMarca")]
        public ActionResult<object> GetMarca()
        {
             
            List<DataLayer.EntityModel.MarcaEntity> MarcaEntity = new List<DataLayer.EntityModel.MarcaEntity>();
            Marca Marca = new Marca();



            if (Marca.GetMarca(ref MarcaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = MarcaEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = MarcaEntity
                });
            }

        }



        [HttpGet]
        [Route("GetMarcaById")]
        public ActionResult<object> GetCategoriaById(int IdS)
        {
            DataLayer.EntityModel.MarcaEntity MarcaEntity = new DataLayer.EntityModel.MarcaEntity();
            Marca Marca = new Marca(IdS);

            if (Marca.GetMarcaById(ref MarcaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = MarcaEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = MarcaEntity
                });
            }

        }

    }

}