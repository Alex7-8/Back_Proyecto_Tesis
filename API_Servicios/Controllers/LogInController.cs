using DataLayer.EntityModel;
using LogicLayer.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Transactions;

namespace API_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogInController : ControllerBase
    {
        [HttpGet]
        [Route("GetEstatus")]
        public ActionResult<object> EchoPing()
        {
            DataLayer.EntityModel.LogInEntity LogInEntity = new DataLayer.EntityModel.LogInEntity();
            LogIn Estatus = new LogIn();
            if (Estatus.Estatus(ref LogInEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Transaccion_Estado = LogInEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = LogInEntity.C_Transaccion_Mensaje,
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = LogInEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = LogInEntity.C_Transaccion_Mensaje,
                });
            }
        }


       [HttpPost]
        [Route("SetLogin")]
        [AllowAnonymous]
        public ActionResult<object> LogIn([FromBody] DataLayer.EntityModel.LogInEntity LogInEntity)
        {
           LogIn login = new LogIn(LogInEntity.C_Id_Usuario, LogInEntity.C_Contrasenia);

            if (login.IniciarSesion(ref LogInEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Transaccion_Estado =  LogInEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = LogInEntity.C_Transaccion_Mensaje,
                    Token = LogInEntity.C_Token
                }) ;

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = LogInEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = LogInEntity.C_Transaccion_Mensaje,
                    Token_Dispositivo =  LogInEntity.C_Token

                });
            }

        }

        [HttpPost]
        [Route("SetActivarDispositivo")]
        [Authorize]
        public ActionResult<object> ValidarDispositivo([FromBody] DataLayer.EntityModel.LogInEntity LogInEntity)
        {

            LogIn Validar = new();

            if (Validar.ValidarDispositivo(ref LogInEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Transaccion_Estado = LogInEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = LogInEntity.C_Transaccion_Mensaje,
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = LogInEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = LogInEntity.C_Transaccion_Mensaje,
                });
            }

        }
    }

}