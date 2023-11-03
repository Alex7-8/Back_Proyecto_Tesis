using LogicLayer.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LogicLayer;
using LogicLayer.Categoria;
using DataLayer.EntityModel;
using Org.BouncyCastle.Crypto;
using LogicLayer.Sucursal;
using LogicLayer.Logica;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace API_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class SucursalController : ControllerBase
    {

        [HttpPost]
        [Route("SetSucursal")]
      //  [Authorize]
        public async Task<ActionResult<object>> SetSucursal([FromBody] DataLayer.EntityModel.SucursalEntity SucursalEntity)
        {
            string bImg = SucursalEntity.C_Img_Base, nombre = SucursalEntity.C_Nombre;
          
            LogicLayer.Logica.Metodos metodos = new LogicLayer.Logica.Metodos();
          
                if (!bImg.Equals("0"))
                {
                    SucursalEntity.C_Url_Img = await metodos.SubirIMGAsync(bImg, nombre, "ImgSucursal");
                }
                else
                {
                    SucursalEntity.C_Url_Img = "https://img.srvcentral.com/Sistema/ImagenPorDefecto/Sucursal.jpg";
                }

                Sucursal sucursal = new Sucursal();

                if (sucursal.SetSucursal(ref SucursalEntity))
                {
                    return Ok(new
                    {
                        ok = true,
                        Transaccion_Estado = SucursalEntity.C_Transaccion_Estado,
                        Transaccion_Mensaje = SucursalEntity.C_Transaccion_Mensaje,
                    });
                }
                else
                {
                    return Ok(new
                    {
                        ok = false,
                        Transaccion_Estado = SucursalEntity.C_Transaccion_Estado,
                        Transaccion_Mensaje = SucursalEntity.C_Transaccion_Mensaje,
                    });
                }
   
         
        }

        [HttpPost]
        [Route("CambiarEstadoSucursal")]
        public ActionResult<object> CambiarEstadoSucursal(int IdS, string IdUM, string razon)
        {
           DataLayer.EntityModel.SucursalEntity SucursalEntity = new DataLayer.EntityModel.SucursalEntity();

            Sucursal sucursal = new Sucursal(IdS,IdUM,razon);

         

            if (sucursal.CambiarEstadoSucursal(ref SucursalEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Transaccion_Estado = SucursalEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = SucursalEntity.C_Transaccion_Mensaje,

                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = SucursalEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = SucursalEntity.C_Transaccion_Mensaje,
                });
            }

        }


        [HttpPost]
        [Route("PutSucursal")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<object>> PutSucursal([FromBody] DataLayer.EntityModel.SucursalEntity SucursalEntity)
        {
            string bImg = SucursalEntity.C_Img_Base, nombre = SucursalEntity.C_Nombre;

            Sucursal sucursal = new Sucursal();

            LogicLayer.Logica.Metodos metodos = new LogicLayer.Logica.Metodos();



            if (!bImg.Equals("0") && !(bImg.StartsWith("http://") || bImg.StartsWith("https://")))
            {

                SucursalEntity.C_Url_Img = await metodos.SubirIMGAsync(bImg, nombre, "ImgSucursal");
            }

            if (sucursal.PutSucursal(ref SucursalEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Transaccion_Estado = SucursalEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = SucursalEntity.C_Transaccion_Mensaje,
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = SucursalEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = SucursalEntity.C_Transaccion_Mensaje,
                });
            }

        }


        [HttpGet]
        [Route("GetSucursal")]
        public ActionResult<object> GetSucursal()
        {
             
            List<DataLayer.EntityModel.SucursalEntity > SucursalEntity = new List<DataLayer.EntityModel.SucursalEntity>();
            Sucursal sucursal = new Sucursal();



            if (sucursal.GetSucursal(ref SucursalEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = SucursalEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = SucursalEntity
                });
            }

        }



        [HttpGet]
        [Route("GetSucursalById")]
        public ActionResult<object> GetSucursalById(int IdS)
        {
            DataLayer.EntityModel.SucursalEntity SucursalEntity = new DataLayer.EntityModel.SucursalEntity();
            Sucursal sucursal = new Sucursal(IdS);

            if (sucursal.GetSucursalById(ref SucursalEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = SucursalEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = SucursalEntity
                });
            }

        }

    }

}