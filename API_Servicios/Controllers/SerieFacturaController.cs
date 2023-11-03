using Microsoft.AspNetCore.Mvc;
using DataLayer.EntityModel;
using LogicLayer.SerieFactura;


namespace API_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SerieFacturaController : ControllerBase
    {

        [HttpPost]
        [Route("SetSerie")]
       // [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<object>> SetSerie([FromBody] DataLayer.EntityModel.SerieFacturaEntity SerieEntity)
        {


            SerieFactura Serie = new SerieFactura();

                    if (Serie.SetSerie(ref SerieEntity))
                    {
                        return Ok(new
                        {
                            ok = true,
                            Transaccion_Estado = SerieEntity.C_Transaccion_Estado,
                            Transaccion_Mensaje = SerieEntity.C_Transaccion_Mensaje
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            ok = false,
                            Transaccion_Estado = SerieEntity.C_Transaccion_Estado,
                            Transaccion_Mensaje = SerieEntity.C_Transaccion_Mensaje
                        });
                    }
        }

        [HttpPost]
        [Route("CambiarEstadoSerie")]
        public ActionResult<object> CambiarEstadoSerie(int IdSerie, string IdUM, string razon)
        {
           DataLayer.EntityModel.SerieFacturaEntity SerieEntity = new DataLayer.EntityModel.SerieFacturaEntity();

            SerieFactura Serie = new SerieFactura(IdSerie,IdUM,razon);

         

            if (Serie.CambiarEstadoSerie(ref SerieEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Transaccion_Estado = SerieEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = SerieEntity.C_Transaccion_Mensaje,

                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = SerieEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = SerieEntity.C_Transaccion_Mensaje,
                });
            }

        }


        [HttpPost]
        [Route("PutSerie")]
       // [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<object>> PutSerie([FromBody] DataLayer.EntityModel.SerieFacturaEntity SerieEntity)
        {
          

            SerieFactura Serie = new SerieFactura();

            if (Serie.PutSerie(ref SerieEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Transaccion_Estado = SerieEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = SerieEntity.C_Transaccion_Mensaje,
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = SerieEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = SerieEntity.C_Transaccion_Mensaje,
                });
            }

        }


        [HttpGet]
        [Route("GetSerie")]
        public ActionResult<object> GetSerie()
        {
             
            List<DataLayer.EntityModel.SerieFacturaEntity> SerieEntity = new List<DataLayer.EntityModel.SerieFacturaEntity>();
            SerieFactura Serie = new SerieFactura();



            if (Serie.GetSerie(ref SerieEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = SerieEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = SerieEntity
                });
            }

        }



        [HttpGet]
        [Route("GetSerieById")]
        public ActionResult<object> GetSerieById(int IdS)
        {
            DataLayer.EntityModel.SerieFacturaEntity SerieEntity = new DataLayer.EntityModel.SerieFacturaEntity();
            SerieFactura Serie  = new SerieFactura(IdS);

            if (Serie.GetSerieById(ref SerieEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = SerieEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = SerieEntity
                });
            }

        }



        [HttpGet]
        [Route("GetSerieBySL")]
        public ActionResult<object> GetSerieBySL(int IdSL)
        {

            List<DataLayer.EntityModel.SerieFacturaEntity> SerieEntity = new List<DataLayer.EntityModel.SerieFacturaEntity>();
            SerieFactura Serie = new SerieFactura(IdSL);



            if (Serie.GetSerieBySL(ref SerieEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = SerieEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = SerieEntity
                });
            }

        }





    }

}