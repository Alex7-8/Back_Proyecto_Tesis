using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogoController : Controller
    {

        [HttpGet]
        [Route("GetDepartamento")]
        [Authorize]
        public ActionResult<object> getDepartamento()
        {
            List<DataLayer.EntityModel.DepartamentoEntity> Depto = new List<DataLayer.EntityModel.DepartamentoEntity>();
            LogicLayer.Catalogos.Catalogo Cat = new LogicLayer.Catalogos.Catalogo();

            if (Cat.getDepartamento(ref Depto))
            {
                return Ok(new
                {
                    ok = true,
                    Response = Depto
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = Depto
                });
            }

        }


        [HttpGet]
        [Route("GetMunicipioXDepartamento")]
        [Authorize]
        public ActionResult<object> getMunicipioXDepartamento(int IdDepartamento)
        {
            List<DataLayer.EntityModel.MunicipioEntity> Mun = new List<DataLayer.EntityModel.MunicipioEntity>();
            LogicLayer.Catalogos.Catalogo Cat = new LogicLayer.Catalogos.Catalogo(IdDepartamento);

            if (Cat.getMunicipioXDepartamento(ref Mun))
            {
                return Ok(new
                {
                    ok = true,
                    Response = Mun
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = Mun
                });
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetRolPersona")]
        public ActionResult<object> getRolPersona()
        {
            List<DataLayer.EntityModel.RolPersonaEntity> Rolp = new List<DataLayer.EntityModel.RolPersonaEntity>();
            LogicLayer.Catalogos.Catalogo Cat = new LogicLayer.Catalogos.Catalogo();

            if (Cat.getRolPersona(ref Rolp))
            {
                return Ok(new
                {
                    ok = true,
                    Response = Rolp
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = Rolp
                });
            }

        }



        [HttpGet]
        [Authorize]
        [Route("GetSucursal")]
        public ActionResult<object> getSucursal()
        {
            List<DataLayer.EntityModel.SucursalEntity> SL = new List<DataLayer.EntityModel.SucursalEntity>();
            LogicLayer.Catalogos.Catalogo Cat = new LogicLayer.Catalogos.Catalogo();

            if (Cat.getSucursal(ref SL))
            {
                return Ok(new
                {
                    ok = true,
                    Response = SL
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = SL
                });
            }

        }


        [HttpGet]
        [Authorize]
        [Route("GetRolUsuario")]
        public ActionResult<object> getRolUsuario()
        {
            List<DataLayer.EntityModel.RolUsuarioEntity> Rolusu = new List<DataLayer.EntityModel.RolUsuarioEntity>();
            LogicLayer.Catalogos.Catalogo Cat = new LogicLayer.Catalogos.Catalogo();

            if (Cat.getRolUsuario(ref Rolusu))
            {
                return Ok(new
                {
                    ok = true,
                    Response = Rolusu
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = Rolusu
                });
            }

        }

        [HttpGet]
        [Authorize]
        [Route("GetGenero")]
        public ActionResult<object> getGenero()
        {
            List<DataLayer.EntityModel.GeneroEntity> Genero = new List<DataLayer.EntityModel.GeneroEntity>();
            LogicLayer.Catalogos.Catalogo Cat = new LogicLayer.Catalogos.Catalogo();

            if (Cat.getGenero(ref Genero))
            {
                return Ok(new
                {
                    ok = true,
                    Response = Genero
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = Genero
                });
            }

        }


        [HttpGet]
        [Authorize]
        [Route("GetEstadoCuenta")]
        public ActionResult<object> getEstadoCuenta()
        {
            List<DataLayer.EntityModel.EstadoCuentaEntity> EstadoC = new List<DataLayer.EntityModel.EstadoCuentaEntity>();
            LogicLayer.Catalogos.Catalogo Cat = new LogicLayer.Catalogos.Catalogo();

            if (Cat.getEstadoCuenta(ref EstadoC))
            {
                return Ok(new
                {
                    ok = true,
                    Response = EstadoC
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = EstadoC
                });
            }

        }


        [HttpGet]
       [Authorize]
        [Route("GetTipoCuenta")]
        public ActionResult<object> getTipoCuenta()
        {
            List<DataLayer.EntityModel.TipoCuentaEntity> TipoC = new List<DataLayer.EntityModel.TipoCuentaEntity>();
            LogicLayer.Catalogos.Catalogo Cat = new LogicLayer.Catalogos.Catalogo();

            if (Cat.getTipoCuenta(ref TipoC))
            {
                return Ok(new
                {
                    ok = true,
                    Response = TipoC
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = TipoC
                });
            }

        }


        [HttpGet]
        [Route("GetMunicipioCatalogoByIdServicio")]
        [Authorize]
        public ActionResult<object> GetMunicipioCatalogoByIdServicio(int IdS)
        {
            List<DataLayer.EntityModel.CatalogoServicioEntity> CS = new List<DataLayer.EntityModel.CatalogoServicioEntity>();
            LogicLayer.Catalogos.Catalogo Cat = new LogicLayer.Catalogos.Catalogo(IdS);

            if (Cat.GetCatalogoByIdServicio(ref CS))
            {
                return Ok(new
                {
                    ok = true,
                    Response = CS
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = CS
                });
            }
        }



        [HttpGet]
        [Authorize]
        [Route("GetCategoria")]
        public ActionResult<object> GetCategoria()
        {
            List<DataLayer.EntityModel.CategoriaEntity> CE = new List<DataLayer.EntityModel.CategoriaEntity>();
            LogicLayer.Catalogos.Catalogo Cat = new LogicLayer.Catalogos.Catalogo();

            if (Cat.GetCategoria(ref CE))
            {
                return Ok(new
                {
                    ok = true,
                    Response = CE
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = CE
                });
            }

        }


        [HttpGet]
        [Authorize]
        [Route("GetMarca")]
        public ActionResult<object> GetMarca()
        {
            List<DataLayer.EntityModel.MarcaEntity> ME = new List<DataLayer.EntityModel.MarcaEntity>();
            LogicLayer.Catalogos.Catalogo Cat = new LogicLayer.Catalogos.Catalogo();

            if (Cat.GetMarca(ref ME))
            {
                return Ok(new
                {
                    ok = true,
                    Response = ME
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = ME
                });
            }

        }
    }
}
