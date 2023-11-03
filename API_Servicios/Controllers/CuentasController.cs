using LogicLayer.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LogicLayer;
using LogicLayer.Cuenta;
using DataLayer.EntityModel;
using Org.BouncyCastle.Crypto;

namespace API_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CuentasController : ControllerBase
    {

        [HttpPost]
        [Route("SetDeposito")]
       // [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<object>> SetDeposito([FromBody] DataLayer.EntityModel.CuentaEntity CuentaEntity)
        {


		 	Cuenta cuenta = new Cuenta();

                    if (cuenta.SetDeposito(ref CuentaEntity))
                    {
                        return Ok(new
                        {
                            ok = true,
                            Transaccion_Estado = CuentaEntity.C_Transaccion_Estado,
                            Transaccion_Mensaje = CuentaEntity.C_Transaccion_Mensaje,
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            ok = false,
                            Transaccion_Estado = CuentaEntity.C_Transaccion_Estado,
                            Transaccion_Mensaje = CuentaEntity.C_Transaccion_Mensaje,
                        });
                    }
        }

   //     [HttpPost]
   //     [Route("CambiarEstadoCategoria")]
   //     public ActionResult<object> CambiarEstadoCategoria(int IdS, string IdUM, string razon)
   //     {
   //        DataLayer.EntityModel.CuentaEntity cuentaEntity = new DataLayer.EntityModel.CuentaEntity();

			//Cuenta cuenta = new Cuenta(IdS,IdUM,razon);

         

   //         if (cuenta.CambiarEstadoCategoria(ref cuentaEntity))
   //         {
   //             return Ok(new
   //             {
   //                 ok = true,
   //                 Transaccion_Estado = cuentaEntity.C_Transaccion_Estado,
   //                 Transaccion_Mensaje = cuentaEntity.C_Transaccion_Mensaje,

   //             });

   //         }
   //         else
   //         {
   //             return Ok(new
   //             {
   //                 ok = false,
   //                 Transaccion_Estado = cuentaEntity.C_Transaccion_Estado,
   //                 Transaccion_Mensaje = cuentaEntity.C_Transaccion_Mensaje,
   //             });
   //         }

   //     }


   //     [HttpPost]
   //     [Route("PutCategoria")]
   //     // [Authorize(Roles = "Administrador")]
   //     public async Task<ActionResult<object>> PutCategoria([FromBody] DataLayer.EntityModel.CategoriaEntity categoriaEntity)
   //     {


			//Cuenta cuenta = new Cuenta();

   //         if (cuenta.PutCategoria(ref categoriaEntity))
   //         {
   //             return Ok(new
   //             {
   //                 ok = true,
   //                 Transaccion_Estado = categoriaEntity.C_Transaccion_Estado,
   //                 Transaccion_Mensaje = categoriaEntity.C_Transaccion_Mensaje,
   //             });
   //         }
   //         else
   //         {
   //             return Ok(new
   //             {
   //                 ok = false,
   //                 Transaccion_Estado = categoriaEntity.C_Transaccion_Estado,
   //                 Transaccion_Mensaje = categoriaEntity.C_Transaccion_Mensaje,
   //             });
   //         }

   //     }


   //     [HttpGet]
   //     [Route("GetCategorias")]
   //     public ActionResult<object> GetCategorias()
   //     {

			//DataLayer.EntityModel.CuentaEntity cuentaEntity = new DataLayer.EntityModel.CuentaEntity();
			//Cuenta cuenta = new Cuenta();



   //         if (cuenta.GetCategorias(ref cuentaEntity))
   //         {
   //             return Ok(new
   //             {
   //                 ok = true,
   //                 Response = cuentaEntity
			//	});

   //         }
   //         else
   //         {
   //             return Ok(new
   //             {
   //                 ok = false,
   //                 Response = cuentaEntity
			//	});
   //         }

   //     }



        [HttpGet]
        [Route("GetCuentaById")]
        public ActionResult<object> GetCuentaById(string IdCuenta)
        {
			DataLayer.EntityModel.CuentaEntity cuentaEntity = new DataLayer.EntityModel.CuentaEntity();
			Cuenta cuenta = new Cuenta(IdCuenta);

            if (cuenta.GetCuentaById(ref cuentaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = cuentaEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = cuentaEntity
                });
            }

        }

    }

}