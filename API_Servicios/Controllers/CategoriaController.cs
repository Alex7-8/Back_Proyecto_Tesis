using LogicLayer.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LogicLayer;
using LogicLayer.Categoria;
using DataLayer.EntityModel;
using Org.BouncyCastle.Crypto;

namespace API_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {

        [HttpPost]
        [Route("SetCategoria")]
       // [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<object>> SetCategoria([FromBody] DataLayer.EntityModel.CategoriaEntity categoriaEntity)
        {


                    Categoria categoria = new Categoria();

                    if (categoria.SetCategoria(ref categoriaEntity))
                    {
                        return Ok(new
                        {
                            ok = true,
                            Transaccion_Estado = categoriaEntity.C_Transaccion_Estado,
                            Transaccion_Mensaje = categoriaEntity.C_Transaccion_Mensaje,
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            ok = false,
                            Transaccion_Estado = categoriaEntity.C_Transaccion_Estado,
                            Transaccion_Mensaje = categoriaEntity.C_Transaccion_Mensaje,
                        });
                    }
        }

        [HttpPost]
        [Route("CambiarEstadoCategoria")]
        public ActionResult<object> CambiarEstadoCategoria(int IdS, string IdUM, string razon)
        {
           DataLayer.EntityModel.CategoriaEntity categoriaEntity = new DataLayer.EntityModel.CategoriaEntity();

            Categoria categoria = new Categoria(IdS,IdUM,razon);

         

            if (categoria.CambiarEstadoCategoria(ref categoriaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Transaccion_Estado = categoriaEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = categoriaEntity.C_Transaccion_Mensaje,

                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = categoriaEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = categoriaEntity.C_Transaccion_Mensaje,
                });
            }

        }


        [HttpPost]
        [Route("PutCategoria")]
        // [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<object>> PutCategoria([FromBody] DataLayer.EntityModel.CategoriaEntity categoriaEntity)
        {


            Categoria categoria = new Categoria();

            if (categoria.PutCategoria(ref categoriaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Transaccion_Estado = categoriaEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = categoriaEntity.C_Transaccion_Mensaje,
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = categoriaEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = categoriaEntity.C_Transaccion_Mensaje,
                });
            }

        }


        [HttpGet]
        [Route("GetCategorias")]
        public ActionResult<object> GetCategorias()
        {
             
            List<DataLayer.EntityModel.CategoriaEntity > categoriaEntity = new List<DataLayer.EntityModel.CategoriaEntity>();
            Categoria categoria = new Categoria();



            if (categoria.GetCategorias(ref categoriaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = categoriaEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = categoriaEntity
                });
            }

        }



        [HttpGet]
        [Route("GetCategoriaById")]
        public ActionResult<object> GetCategoriaById(int IdS)
        {
            DataLayer.EntityModel.CategoriaEntity categoriaEntity = new DataLayer.EntityModel.CategoriaEntity();
            Categoria categoria = new Categoria(IdS);

            if (categoria.GetCategoriaById(ref categoriaEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = categoriaEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = categoriaEntity
                });
            }

        }

    }

}