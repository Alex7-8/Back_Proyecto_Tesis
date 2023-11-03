using Microsoft.AspNetCore.Mvc;
using DataLayer.EntityModel;
using LogicLayer.Reportes;


namespace API_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportesController : ControllerBase
    {
		//Grafica de Productos
        [HttpGet]
        [Route("GetReporteFacturaByDia")]
        public ActionResult<object> GetReporteFacturaByDia(string Tipo)
        {
             
            List<DataLayer.EntityModel.ReportesEntity> ReporteEntity = new List<DataLayer.EntityModel.ReportesEntity>();
           Reportes reportes = new Reportes(Tipo);
          



            if (reportes.GetReporteFacturaByDia(ref ReporteEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = ReporteEntity
				});

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = ReporteEntity
				});
            }

        }




	}

}