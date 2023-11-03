using Microsoft.AspNetCore.Mvc;
using DataLayer.EntityModel;
using LogicLayer.Grafica;


namespace API_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GraficaController : ControllerBase
    {
		//Grafica de Productos
        [HttpGet]
        [Route("GetGrafiaByDia")]
        public ActionResult<object> GetGrafiaByDia()
        {
             
            List<DataLayer.EntityModel.GraficaEntity> GraficaEntity = new List<DataLayer.EntityModel.GraficaEntity>();
            Grafica Grafica = new Grafica();
          



            if (Grafica.GetGrafiaByDia(ref GraficaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = GraficaEntity
				});

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = GraficaEntity
				});
            }

        }

		[HttpGet]
		[Route("GetGrafiaBySemana")]
		public ActionResult<object> GetGrafiaBySemana()
		{

			List<DataLayer.EntityModel.GraficaEntity> GraficaEntity = new List<DataLayer.EntityModel.GraficaEntity>();
			Grafica Grafica = new Grafica();




			if (Grafica.GetGrafiaBySemana(ref GraficaEntity))
			{
				return Ok(new
				{
					ok = true,
					Response = GraficaEntity
				});

			}
			else
			{
				return Ok(new
				{
					ok = false,
					Response = GraficaEntity
				});
			}

		}


		[HttpGet]
		[Route("GetGrafiaByMes")]
		public ActionResult<object> GetGrafiaByMes()
		{

			List<DataLayer.EntityModel.GraficaEntity> GraficaEntity = new List<DataLayer.EntityModel.GraficaEntity>();
			Grafica Grafica = new Grafica();




			if (Grafica.GetGrafiaByMes(ref GraficaEntity))
			{
				return Ok(new
				{
					ok = true,
					Response = GraficaEntity
				});

			}
			else
			{
				return Ok(new
				{
					ok = false,
					Response = GraficaEntity
				});
			}

		}



		[HttpGet]
		[Route("GetGrafiaByAnio")]
		public ActionResult<object> GetGrafiaByAnio()
		{

			List<DataLayer.EntityModel.GraficaEntity> GraficaEntity = new List<DataLayer.EntityModel.GraficaEntity>();
			Grafica Grafica = new Grafica();




			if (Grafica.GetGrafiaByAnio(ref GraficaEntity))
			{
				return Ok(new
				{
					ok = true,
					Response = GraficaEntity
				});

			}
			else
			{
				return Ok(new
				{
					ok = false,
					Response = GraficaEntity
				});
			}

		}


	



		[HttpGet]
		[Route("GetGrafiaServicioByDia")]
		public ActionResult<object> GetGrafiaServicioByDia()
		{

			List<DataLayer.EntityModel.GraficaEntity> GraficaEntity = new List<DataLayer.EntityModel.GraficaEntity>();
			Grafica Grafica = new Grafica();




			if (Grafica.GetGrafiaServicioByDia(ref GraficaEntity))
			{
				return Ok(new
				{
					ok = true,
					Response = GraficaEntity
				});

			}
			else
			{
				return Ok(new
				{
					ok = false,
					Response = GraficaEntity
				});
			}

		}

		[HttpGet]
		[Route("GetGrafiaServicioBySemana")]
		public ActionResult<object> GetGrafiaServicioBySemana()
		{

			List<DataLayer.EntityModel.GraficaEntity> GraficaEntity = new List<DataLayer.EntityModel.GraficaEntity>();
			Grafica Grafica = new Grafica();




			if (Grafica.GetGrafiaServicioBySemana(ref GraficaEntity))
			{
				return Ok(new
				{
					ok = true,
					Response = GraficaEntity
				});

			}
			else
			{
				return Ok(new
				{
					ok = false,
					Response = GraficaEntity
				});
			}

		}


		[HttpGet]
		[Route("GetGrafiaServicioByMes")]
		public ActionResult<object> GetGrafiaServicioByMes()
		{

			List<DataLayer.EntityModel.GraficaEntity> GraficaEntity = new List<DataLayer.EntityModel.GraficaEntity>();
			Grafica Grafica = new Grafica();




			if (Grafica.GetGrafiaServicioByMes(ref GraficaEntity))
			{
				return Ok(new
				{
					ok = true,
					Response = GraficaEntity
				});

			}
			else
			{
				return Ok(new
				{
					ok = false,
					Response = GraficaEntity
				});
			}

		}



		[HttpGet]
		[Route("GetGrafiaServicioByAnio")]
		public ActionResult<object> GetGrafiaServicioByAnio()
		{

			List<DataLayer.EntityModel.GraficaEntity> GraficaEntity = new List<DataLayer.EntityModel.GraficaEntity>();
			Grafica Grafica = new Grafica();




			if (Grafica.GetGrafiaServicioByAnio(ref GraficaEntity))
			{
				return Ok(new
				{
					ok = true,
					Response = GraficaEntity
				});

			}
			else
			{
				return Ok(new
				{
					ok = false,
					Response = GraficaEntity
				});
			}

		}


	}

}