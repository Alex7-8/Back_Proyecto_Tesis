
using Microsoft.AspNetCore.Mvc;





namespace API_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductosController : ControllerBase
    {

        [HttpPost]
        [Route("SetProductos")]
       // [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<object>> SetProductos([FromBody] DataLayer.EntityModel.ProductosEntity productosEntity)
        {

            string bImg = productosEntity.C_Img_Base, nombre = productosEntity.C_Nombre_Producto;

            LogicLayer.Logica.Metodos metodos = new LogicLayer.Logica.Metodos();

            if (!bImg.Equals("0"))
            {
                productosEntity.C_Url_IMG = await metodos.SubirIMGAsync(bImg, nombre, "ImgProductos");
            }
            else
            {
                productosEntity.C_Url_IMG = "https://img.srvcentral.com/Sistema/ImagenPorDefecto/Servicios.jpg";
            }
            LogicLayer.Productos.Productos productos = new LogicLayer.Productos.Productos();

                    if (productos.SetProductos(ref productosEntity))
                    {
                        return Ok(new
                        {
                            ok = true,
                            Transaccion_Estado = productosEntity.C_Transaccion_Estado,
                            Transaccion_Mensaje = productosEntity.C_Transaccion_Mensaje,
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            ok = false,
                            Transaccion_Estado = productosEntity.C_Transaccion_Estado,
                            Transaccion_Mensaje = productosEntity.C_Transaccion_Mensaje,
                        });
                    }
        }

        [HttpPost]
        [Route("CambiarEstadoProducto")]
        public ActionResult<object>CambiarEstadoServicio(int IdP, string IdUM, string razon)
        {
        DataLayer.EntityModel.ProductosEntity productosEntity = new DataLayer.EntityModel.ProductosEntity();

            LogicLayer.Productos.Productos productos = new LogicLayer.Productos.Productos(IdP,IdUM,razon);

         

            if (productos.CambiarEstadoProducto(ref productosEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Transaccion_Estado = productosEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = productosEntity.C_Transaccion_Mensaje,

                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = productosEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = productosEntity.C_Transaccion_Mensaje,
                });
            }

        }


        [HttpPost]
        [Route("PutProductos")]
        // [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<object>> PutProductos([FromBody] DataLayer.EntityModel.ProductosEntity productosEntity)

        {
            string bImg = productosEntity.C_Img_Base, nombre = productosEntity.C_Nombre_Producto;
            LogicLayer.Logica.Metodos metodos = new LogicLayer.Logica.Metodos();
            if (!bImg.Equals("0") && !(bImg.StartsWith("http://") || bImg.StartsWith("https://")))
            {

                productosEntity.C_Url_IMG = await metodos.SubirIMGAsync(bImg, nombre, "ImgProducto");
            }

            LogicLayer.Productos.Productos productos = new LogicLayer.Productos.Productos();

            if (productos.PutProductos(ref productosEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Transaccion_Estado = productosEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = productosEntity.C_Transaccion_Mensaje,
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = productosEntity.C_Transaccion_Estado,
                    Transaccion_Mensaje = productosEntity.C_Transaccion_Mensaje,
                });
            }

        }


        [HttpGet]
        [Route("GetProductos")]
        public ActionResult<object> GetProductos()
        {
            List<DataLayer.EntityModel.ProductosEntity> productosEntity = new List<DataLayer.EntityModel.ProductosEntity>();
            LogicLayer.Productos.Productos productos = new LogicLayer.Productos.Productos();



            if (productos.GetProductos(ref productosEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = productosEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = productosEntity
                });
            }

        }



        [HttpGet]
        [Route("GetProductosById")]
        public ActionResult<object> GetProductosById(int IdP)
        {
            DataLayer.EntityModel.ProductosEntity productosEntity = new DataLayer.EntityModel.ProductosEntity();
            LogicLayer.Productos.Productos productos = new LogicLayer.Productos.Productos(IdP);

            if (productos.GetProductosById(ref productosEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = productosEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = productosEntity
                });
            }

        }



        [HttpGet]
        [Route("GetProductosFactura")]
        public ActionResult<object> GetProductosFactura(int idSL)
        {
            List<DataLayer.EntityModel.ProductosEntity> productosEntity = new List<DataLayer.EntityModel.ProductosEntity>();
            LogicLayer.Productos.Productos productos = new LogicLayer.Productos.Productos(idSL);



            if (productos.GetProductosFactura(ref productosEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = productosEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = productosEntity
                });
            }

        }


        [HttpGet]
        [Route("GetProductosFacturaById")]
        public ActionResult<object> GetProductosFacturaById(int IdP)
        {
            DataLayer.EntityModel.ProductosEntity productosEntity = new DataLayer.EntityModel.ProductosEntity();

            LogicLayer.Productos.Productos productos = new LogicLayer.Productos.Productos(IdP);

            if (productos.GetProductosFacturaById(ref productosEntity))
            {
                return Ok(new
                {
                    ok = true,
                    Response = productosEntity
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = productosEntity
                });
            }

        }



    }

}