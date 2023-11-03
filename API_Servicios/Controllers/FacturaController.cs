using Microsoft.AspNetCore.Mvc;
using DataLayer.EntityModel;
using LogicLayer.Factura;
using Microsoft.AspNetCore.Authorization;

namespace API_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacturaController : ControllerBase
    {

        [HttpPost]
        [Route("SetFacturaVenta")]
		[Authorize(Roles = "ADMINISTRADOR,CAJERO")]
		public async Task<ActionResult<object>> SetFacturaVenta([FromBody] DataLayer.EntityModel.FacturaEntity FacturaEntity)
        {


            Factura Serie = new Factura();

                    if (Serie.SetFacturaVenta(ref FacturaEntity))
                    {
                        return Ok(new
                        {
                            ok = true,

                            Transaccion_Estado = FacturaEntity.C_Transaccion_Estado,
                            Transaccion_Mensaje = FacturaEntity.C_Transaccion_Mensaje
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            ok = false,
                            Transaccion_Estado = FacturaEntity.C_Transaccion_Estado,
                            Transaccion_Mensaje = FacturaEntity.C_Transaccion_Mensaje
                        });
                    }
        }



        [HttpPost]
        [Route("SetFacturaServicios")]
		[Authorize(Roles = "ADMINISTRADOR,CAJERO")]
		public async Task<ActionResult<object>> SetFacturaServicios([FromBody] DataLayer.EntityModel.FacturaEntity FacturaEntity)
        {


            Factura Serie = new Factura();

            if (Serie.SetFacturaServicios(ref FacturaEntity))
            {
                return Ok(new
                {
                    ok = true,

                    Transaccion_Estado = FacturaEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = FacturaEntity.C_Transaccion_Mensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = FacturaEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = FacturaEntity.C_Transaccion_Mensaje
                });
            }
        }


        [HttpPost]
        [Route("SetFacturaServiciosCProductos")]
        // [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<object>> SetFacturaServiciosCProductos([FromBody] DataLayer.EntityModel.FacturaEntity FacturaEntity)
        {


            Factura Serie = new Factura();

            if (Serie.SetFacturaServiciosCProductos(ref FacturaEntity))
            {
                return Ok(new
                {
                    ok = true,

                    Transaccion_Estado = FacturaEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = FacturaEntity.C_Transaccion_Mensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = FacturaEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = FacturaEntity.C_Transaccion_Mensaje
                });
            }
        }



        //[HttpPost]
        //[Route("SetFacturaCompra")]
        //// [Authorize(Roles = "Administrador")]
        //public async Task<ActionResult<object>> SetFacturaCompra([FromBody] DataLayer.EntityModel.FacturaEntity FacturaEntity)
        //{


        //    Factura Serie = new Factura();

        //    if (Serie.SetFacturaCompra(ref FacturaEntity))
        //    {
        //        return Ok(new
        //        {
        //            ok = true,

        //            Transaccion_Estado = FacturaEntity.C_Transaccion_Estado,
        //            Transaccion_Mensaje = FacturaEntity.C_Transaccion_Mensaje
        //        });
        //    }
        //    else
        //    {
        //        return Ok(new
        //        {
        //            ok = false,
        //            Transaccion_Estado = FacturaEntity.C_Transaccion_Estado,
        //            Transaccion_Mensaje = FacturaEntity.C_Transaccion_Mensaje
        //        });
        //    }
        //}


        [HttpGet]
        [Route("GetFacturaVentaCliente")]
        // [Authorize(Roles = "Administrador")]
        public ActionResult<object> GetFacturaVenta(int IdS,int IdSL, int IdPersona)
        {
            DataLayer.EntityModel.FacturaEntity FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
            Factura Serie = new Factura(IdS,IdSL,IdPersona);

            if (Serie.GetFacturaVentaCliente(ref FacturaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = FacturaEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = FacturaEntity
                });
            }

        }


        [HttpGet]
        [Route("GetFacturaVentaCF")]
        // [Authorize(Roles = "Administrador")]
        public ActionResult<object> GetFacturaVentaCF(int IdS, int IdSL)
        {
            DataLayer.EntityModel.FacturaEntity FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
            Factura Serie = new Factura(IdS, IdSL);

            if (Serie.GetFacturaVentaCF(ref FacturaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = FacturaEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = FacturaEntity
                });
            }

        }



		[HttpPost]
		[Route("CambiarEstadoFactura")]
		public ActionResult<object> CambiarEstadoFactura(int IdFactura, string IdUM, string razon)
		{
			DataLayer.EntityModel.FacturaEntity FacturaEntity = new DataLayer.EntityModel.FacturaEntity();

			Factura Marca = new Factura(IdFactura, IdUM, razon);



			if (Marca.CambiarEstadoFactura(ref FacturaEntity))
			{
				return Ok(new
				{
					ok = true,
					Transaccion_Estado = FacturaEntity.C_Transaccion_Estado,
					Transaccion_Mensaje = FacturaEntity.C_Transaccion_Mensaje,

				});

			}
			else
			{
				return Ok(new
				{
					ok = false,
					Transaccion_Estado = FacturaEntity.C_Transaccion_Estado,
					Transaccion_Mensaje = FacturaEntity.C_Transaccion_Mensaje,
				});
			}

		}






		[HttpGet]
        [Route("GetFacturaVentaByDia")]
        // [Authorize(Roles = "Administrador")]
        public ActionResult<object> GetFacturaVentaByDia()
        {
            List<DataLayer.EntityModel.FacturaEntity> FacturaEntity = new List<DataLayer.EntityModel.FacturaEntity>();
            Factura Serie = new Factura();

            if (Serie.GetFacturaVentaByDia(ref FacturaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = FacturaEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = FacturaEntity
                });
            }

        }


        [HttpGet]
        [Route("GetFacturaServiciosByDia")]
        // [Authorize(Roles = "Administrador")]
        public ActionResult<object> GetFacturaServiciosByDia()
        {
            List <DataLayer.EntityModel.FacturaEntity> FacturaEntity = new List<DataLayer.EntityModel.FacturaEntity>();
            Factura Serie = new Factura();

            if (Serie.GetFacturaServiciosByDia(ref FacturaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = FacturaEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = FacturaEntity
                });
            }

        }




        [HttpGet]
        [Route("GetGananciasByDia")]
        // [Authorize(Roles = "Administrador")]
        public ActionResult<object> GetGananciasByDia()
        {
          DataLayer.EntityModel.FacturaEntity FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
            Factura Serie = new Factura();

            if (Serie.GetGananciasByDia(ref FacturaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = FacturaEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = FacturaEntity
                });
            }

        }


        [HttpGet]
        [Route("GetGananciasBySemana")]
        // [Authorize(Roles = "Administrador")]
        public ActionResult<object> GetGananciasBySemana()
        {
            DataLayer.EntityModel.FacturaEntity FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
            Factura Serie = new Factura();

            if (Serie.GetGananciasBySemana(ref FacturaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = FacturaEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = FacturaEntity
                });
            }

        }





        [HttpGet]
        [Route("GetGananciasByMes")]
        // [Authorize(Roles = "Administrador")]
        public ActionResult<object> GetGananciasByMes()
        {
          DataLayer.EntityModel.FacturaEntity FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
            Factura Serie = new Factura();

            if (Serie.GetGananciasByMes(ref FacturaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = FacturaEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = FacturaEntity
                });
            }

        }


        [HttpGet]
        [Route("GetGananciasByAnio")]
        // [Authorize(Roles = "Administrador")]
        public ActionResult<object> GetGananciasByAnio()
        {
            DataLayer.EntityModel.FacturaEntity FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
            Factura Serie = new Factura();

            if (Serie.GetGananciasByAnio(ref FacturaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = FacturaEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = FacturaEntity
                });
            }

        }



        [HttpGet]
        [Route("GetGananciasSByDia")]
        // [Authorize(Roles = "Administrador")]
        public ActionResult<object> GetGananciasSByDia()
        {
           DataLayer.EntityModel.FacturaEntity FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
            Factura Serie = new Factura();

            if (Serie.GetGananciasSByDia(ref FacturaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = FacturaEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = FacturaEntity
                });
            }

        }


        [HttpGet]
        [Route("GetGananciasSBySemana")]
        // [Authorize(Roles = "Administrador")]
        public ActionResult<object> GetGananciasSBySemana()
        {
            DataLayer.EntityModel.FacturaEntity FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
            Factura Serie = new Factura();

            if (Serie.GetGananciasSBySemana(ref FacturaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = FacturaEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = FacturaEntity
                });
            }

        }





        [HttpGet]
        [Route("GetGananciasSByMes")]
        // [Authorize(Roles = "Administrador")]
        public ActionResult<object> GetGananciasSByMes()
        {
           DataLayer.EntityModel.FacturaEntity FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
            Factura Serie = new Factura();

            if (Serie.GetGananciasSByMes(ref FacturaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = FacturaEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = FacturaEntity
                });
            }

        }


        [HttpGet]
        [Route("GetGananciasSByAnio")]
        // [Authorize(Roles = "Administrador")]
        public ActionResult<object> GetGananciasSByAnio()
        {
           DataLayer.EntityModel.FacturaEntity FacturaEntity = new DataLayer.EntityModel.FacturaEntity();
            Factura Serie = new Factura();

            if (Serie.GetGananciasSByAnio(ref FacturaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = FacturaEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = FacturaEntity
                });
            }

        }










        [HttpGet]
        [Route("GetDetalleFacturaById")]
        // [Authorize(Roles = "Administrador")]
        public ActionResult<object> GetDetalleFacturaById(int IdS)
        {
            List < DataLayer.EntityModel.FacturaEntity> FacturaEntity = new List<DataLayer.EntityModel.FacturaEntity>();

          
            Factura Serie = new Factura(IdS);

            if (Serie.GetDetalleFacturaById(ref FacturaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = FacturaEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = FacturaEntity
                });
            }

        }






		[HttpGet]
		[Route("GetDetalleFacturaServicioById")]
		// [Authorize(Roles = "Administrador")]
		public ActionResult<object> GetDetalleFacturaServicioById(int IdS)
		{
			List<DataLayer.EntityModel.FacturaEntity> FacturaEntity = new List<DataLayer.EntityModel.FacturaEntity>();


			Factura Serie = new Factura(IdS);

			if (Serie.GetDetalleFacturaServicioById(ref FacturaEntity))
			{
				return Ok(new
				{
					ok = true,
					Response = FacturaEntity
				});

			}
			else
			{
				return Ok(new
				{
					ok = false,
					Response = FacturaEntity
				});
			}

		}


	}

}