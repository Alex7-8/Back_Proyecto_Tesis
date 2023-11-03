using LogicLayer.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LogicLayer;
using LogicLayer.Persona;

namespace API_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonaController : ControllerBase
    {
   

        [HttpPost]
        [Route("SetEmpleadosA")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<object>> setEmpleadosA([FromBody] DataLayer.EntityModel.PersonaEntity Persona)
        {
            

            string bImg = Persona.C_Img_Base, nombre = Persona.C_Primer_Nombre;



            if (Persona.C_Fecha_Nacimiento < DateTime.Today.AddYears(-100) || Persona.C_Fecha_Nacimiento > DateTime.Today.AddYears(-18))
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = 24,
                    Transaccion_Mensaje = "Rango de Fechas no es correcto solo se admite edades mayores de 18 y menores de 100 años",
                });
            }
            else
            {
                LogicLayer.Logica.Metodos metodos = new LogicLayer.Logica.Metodos();



                if (!bImg.Equals("0"))
                {
                    Persona.C_Url_Img = await metodos.SubirIMGAsync(bImg, nombre, "ImgEmpleado");
                }
                else
                {
                    Persona.C_Url_Img = "https://img.srvcentral.com/Sistema/ImagenPorDefecto/Registro.png";
                }



                if (!Persona.C_Url_Img.Equals("0") || !Persona.C_Url_Img.StartsWith("Error"))
                {
                    Persona persona = new Persona(Persona.C_Url_Img);

                    if (persona.setEmpleadosA(ref Persona))
                    {
                        return Ok(new
                        {
                            ok = true,
                            Transaccion_Estado = Persona.C_Transaccion_Estado,
                            Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            ok = false,
                            Transaccion_Estado = Persona.C_Transaccion_Estado,
                            Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                        });
                    }
                }
                else
                {
                    return Ok(new
                    {
                        ok = false,
                        Transaccion_Estado = 25,
                        Transaccion_Mensaje = Persona.C_Url_Img,
                    });
                }
            }
         
        }


        [HttpPost]
        [Route("CambiarEstadoEmpleado")]
		[Authorize(Roles = "ADMINISTRADOR,GERENTE")]
		public ActionResult<object> CambiarEstadoEmpleado(int IdP, string IdU, string IdUM, string razon)
        {
            DataLayer.EntityModel.PersonaEntity Persona = new DataLayer.EntityModel.PersonaEntity();
            Persona persona = new Persona(IdP, IdU, IdUM,razon);

            string fechaStr = "2023-08-28"; // Formato: "yyyy-MM-dd"
            DateTime fechaConvertida = DateTime.Parse(fechaStr);

            Persona.C_Fecha_Nacimiento = fechaConvertida;

            if (persona.CambiarEstadoEmpleado(ref Persona))
            {
                return Ok(new
                {
                    ok = true,
                    Transaccion_Estado = Persona.C_Transaccion_Estado,
                    Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,

                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = Persona.C_Transaccion_Estado,
                    Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                });
            }

        }


        [HttpPost]
        [Route("PutEmpleadosA")]
        [Authorize(Roles  = "ADMINISTRADOR,GERENTE")]
        public async Task<ActionResult<object>> putEmpleadosA([FromBody] DataLayer.EntityModel.PersonaEntity Persona)
        {


            string bImg = Persona.C_Img_Base, nombre = Persona.C_Primer_Nombre;



            if (Persona.C_Fecha_Nacimiento < DateTime.Today.AddYears(-100) || Persona.C_Fecha_Nacimiento > DateTime.Today.AddYears(-18))
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = 24,
                    Transaccion_Mensaje = "Rango de Fechas no es correcto solo se admite edades mayores de 18 y menores de 100 años",
                });
            }
            else
            {
                LogicLayer.Logica.Metodos metodos = new LogicLayer.Logica.Metodos();



                if (!bImg.Equals("0") && !(bImg.StartsWith("http://") || bImg.StartsWith("https://")))
                {
                    
                    Persona.C_Url_Img = await metodos.SubirIMGAsync(bImg, nombre, "ImgEmpleado");
                }
               



                    Persona persona = new Persona(Persona.C_Url_Img);

                    if (persona.putActualizarEmpleadosA(ref Persona))
                    {
                        return Ok(new
                        {
                            ok = true,
                            Transaccion_Estado = Persona.C_Transaccion_Estado,
                            Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            ok = false,
                            Transaccion_Estado = Persona.C_Transaccion_Estado,
                            Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                        });
                    }
            }

        }



        [HttpPost]
        [Route("PutEmpleadosByIdUsuario")]
        [Authorize]
        public async Task<ActionResult<object>> putEmpleadosByIdUsuario([FromBody] DataLayer.EntityModel.PersonaEntity Persona) 
        {


            string bImg = Persona.C_Img_Base, nombre = Persona.C_Primer_Nombre;



            if (Persona.C_Fecha_Nacimiento < DateTime.Today.AddYears(-100) || Persona.C_Fecha_Nacimiento > DateTime.Today.AddYears(-18))
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = 24,
                    Transaccion_Mensaje = "Rango de Fechas no es correcto solo se admite edades mayores de 18 y menores de 100 años",
                });
            }
            else
            {
                LogicLayer.Logica.Metodos metodos = new LogicLayer.Logica.Metodos();



                if (!bImg.Equals("0") && !(bImg.StartsWith("http://") || bImg.StartsWith("https://")))
                {

                    Persona.C_Url_Img = await metodos.SubirIMGAsync(bImg, nombre, "ImgEmpleado");
                }




                Persona persona = new Persona(Persona.C_Url_Img);

                if (persona.putEmpleadosByIdUsuario(ref Persona))
                {
                    return Ok(new
                    {
                        ok = true,
                        Transaccion_Estado = Persona.C_Transaccion_Estado,
                        Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                        Token = Persona.C_Token
                    });
                }
                else
                {
                    return Ok(new
                    {
                        ok = false,
                        Transaccion_Estado = Persona.C_Transaccion_Estado,
                        Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                    });
                }
            }

        }



       


        [HttpGet]
        [Route("GetEmpleados")]
		[Authorize(Roles = "ADMINISTRADOR")]
		public ActionResult<object> GetEmpleados()
        {
            List<DataLayer.EntityModel.PersonaEntity> PersonaList = new List<DataLayer.EntityModel.PersonaEntity>();
            Persona persona = new Persona();



            if (persona.GetEmpleados(ref PersonaList))
            {
                return Ok(new
                {
                    ok = true,
                    Response = PersonaList
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = PersonaList
                });
            }

        }



        [HttpGet]
        [Route("GetEmpleadoByIdUA")]
        [Authorize]
        public ActionResult<object> GetEmpleadoByIdUA(int IdPersona)
        {
            DataLayer.EntityModel.PersonaEntity Persona = new DataLayer.EntityModel.PersonaEntity();
            Persona persona = new Persona(IdPersona);

            if (persona.GetEmpleadoByIdUA(ref Persona))
            {
                return Ok(new
                {
                    ok = true,
                    Response = Persona
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = Persona
                });
            }

        }

        [HttpGet]
        [Route("GetEmpleadoByIdUsuario")]
        [Authorize]
        public ActionResult<object> GetEmpleadoByIdUsuario(string IdUsuario)
        {
            DataLayer.EntityModel.PersonaEntity Persona = new DataLayer.EntityModel.PersonaEntity();
            Persona persona = new Persona();

            persona.IdUsuario = IdUsuario;

            if (persona.GetEmpleadoByIdUsuario(ref Persona))
            {
                return Ok(new
                {
                    ok = true,
                    Response = Persona
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = Persona
                });
            }

        }


        [HttpPost]
        [Route("SetPersona")]
         [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<object>> setPersona([FromBody] DataLayer.EntityModel.PersonaEntity Persona)
        {


            string bImg = Persona.C_Img_Base, nombre = Persona.C_Primer_Nombre;

           

            if (Persona.C_Id_Rol_Persona == 3 && (Persona.C_Fecha_Nacimiento < DateTime.Today.AddYears(-100) || Persona.C_Fecha_Nacimiento > DateTime.Today.AddYears(-18)) )
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = 24,
                    Transaccion_Mensaje = "Rango de Fechas no es correcto solo se admite edades mayores de 18 y menores de 100 años",
                });
            }
            else
            {
                if (Persona.C_Id_Rol_Persona == 2 && (Persona.C_Fecha_Nacimiento < DateTime.Today.AddYears(-100) || Persona.C_Fecha_Nacimiento > DateTime.Today.AddYears(-18)))
                {

                    return Ok(new
                    {
                        ok = false,
                        Transaccion_Estado = 24,
                        Transaccion_Mensaje = "Rango de Fechas no es correcto solo se admite edades mayores de 6 y menores de 100 años",
                    });

                }
                else 
                {
                    LogicLayer.Logica.Metodos metodos = new LogicLayer.Logica.Metodos();



                    if (!bImg.Equals("0"))
                    {
                        Persona.C_Url_Img = await metodos.SubirIMGAsync(bImg, nombre, "ImgPersona");
                    }
                    else
                    {
                        Persona.C_Url_Img = "https://img.srvcentral.com/Sistema/ImagenPorDefecto/Registro.png";
                    }



                    if (!Persona.C_Url_Img.Equals("0") || !Persona.C_Url_Img.StartsWith("Error"))
                    {
                        Persona persona = new Persona(Persona.C_Url_Img);

                        if (persona.setPersona(ref Persona))
                        {
                            return Ok(new
                            {
                                ok = true,
                                Transaccion_Estado = Persona.C_Transaccion_Estado,
                                Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                            });
                        }
                        else
                        {
                            return Ok(new
                            {
                                ok = false,
                                Transaccion_Estado = Persona.C_Transaccion_Estado,
                                Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                            });
                        }
                    }
                    else
                    {
                        return Ok(new
                        {
                            ok = false,
                            Transaccion_Estado = 25,
                            Transaccion_Mensaje = Persona.C_Url_Img,
                        });
                    }

                }
                
            }

        }



        [HttpGet]
        [Route("GetPersona")]
		[Authorize]
		public ActionResult<object> GetPersona()
        {
            List<DataLayer.EntityModel.PersonaEntity> PersonaList = new List<DataLayer.EntityModel.PersonaEntity>();
            Persona persona = new Persona();



            if (persona.GetPersona(ref PersonaList))
            {
                return Ok(new
                {
                    ok = true,
                    Response = PersonaList
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = PersonaList
                });
            }

        }
        
        [HttpGet]
        [Route("GetPersonaById")]
		[Authorize]
		public ActionResult<object> GetPersonaById(int IdPersona)
        {
            DataLayer.EntityModel.PersonaEntity Persona = new DataLayer.EntityModel.PersonaEntity();
            Persona persona = new Persona(IdPersona);

            if (persona.GetPersonaById(ref Persona))
            {
                return Ok(new
                {
                    ok = true,
                    Response = Persona
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = Persona
                });
            }

        }

        [HttpPost]
        [Route("PutPersona")]
        // [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<object>> putPersona([FromBody] DataLayer.EntityModel.PersonaEntity Persona)
        {


            string bImg = Persona.C_Img_Base, nombre = Persona.C_Primer_Nombre;



            if (Persona.C_Fecha_Nacimiento < DateTime.Today.AddYears(-100) || Persona.C_Fecha_Nacimiento > DateTime.Today.AddYears(-18))
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = 24,
                    Transaccion_Mensaje = "Rango de Fechas no es correcto solo se admite edades mayores de 18 y menores de 100 años",
                });
            }
            else
            {
                LogicLayer.Logica.Metodos metodos = new LogicLayer.Logica.Metodos();



                if (!bImg.Equals("0") && !(bImg.StartsWith("http://") || bImg.StartsWith("https://")))
                {

                    Persona.C_Url_Img = await metodos.SubirIMGAsync(bImg, nombre, "ImgPersona");
                }




                Persona persona = new Persona(Persona.C_Url_Img);

                if (persona.putActualizarPersona(ref Persona))
                {
                    return Ok(new
                    {
                        ok = true,
                        Transaccion_Estado = Persona.C_Transaccion_Estado,
                        Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                    });
                }
                else
                {
                    return Ok(new
                    {
                        ok = false,
                        Transaccion_Estado = Persona.C_Transaccion_Estado,
                        Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                    });
                }
            }

        }

        [HttpPost]
        [Route("CambiarEstadoPersona")]
        public ActionResult<object> CambiarEstadoPersona(int IdP, string IdUM, string razon)
        {
            DataLayer.EntityModel.PersonaEntity Persona = new DataLayer.EntityModel.PersonaEntity();
            Persona persona = new Persona(IdP, IdUM, razon);

            string fechaStr = "2023-08-28"; // Formato: "yyyy-MM-dd"
            DateTime fechaConvertida = DateTime.Parse(fechaStr);

            Persona.C_Fecha_Nacimiento = fechaConvertida;

            if (persona.CambiarEstadoPersona(ref Persona))
            {
                return Ok(new
                {
                    ok = true,
                    Transaccion_Estado = Persona.C_Transaccion_Estado,
                    Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,

                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = Persona.C_Transaccion_Estado,
                    Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                });
            }

        }








        //zona gerente

        [HttpPost]
        [Route("SetEmpleadosG")]
         [Authorize(Roles = "GERENTE")]
        public async Task<ActionResult<object>> setEmpleadosG([FromBody] DataLayer.EntityModel.PersonaEntity Persona)
        {


            string bImg = Persona.C_Img_Base, nombre = Persona.C_Primer_Nombre;

            if (Persona.C_Fecha_Nacimiento < DateTime.Today.AddYears(-100) || Persona.C_Fecha_Nacimiento > DateTime.Today.AddYears(-18))
            {
                return BadRequest("La fecha de nacimiento no es válida.");
            }
            else
            {

                LogicLayer.Logica.Metodos metodos = new LogicLayer.Logica.Metodos();



                if (!bImg.Equals("0"))
                {
                    Persona.C_Url_Img = await metodos.SubirIMGAsync(bImg, nombre, "ImgEmpleado");
                }




                if (!Persona.C_Url_Img.Equals("0") || !Persona.C_Url_Img.StartsWith("Error"))
                {
                    Persona persona = new Persona(Persona.C_Url_Img);

                    if (persona.SetEmpleadoG(ref Persona))
                    {
                        return Ok(new
                        {
                            ok = true,
                            Transaccion_Estado = Persona.C_Transaccion_Estado,
                            Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            ok = false,
                            Transaccion_Estado = Persona.C_Transaccion_Estado,
                            Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                        });
                    }
                }
                else
                {
                    return Ok(new
                    {
                        ok = false,
                        Transaccion_Estado = 25,
                        Transaccion_Mensaje = Persona.C_Url_Img,
                    });
                }

            }






        }


        [HttpPost]
        [Route("PutEmpleadosG")]
        // [Authorize(Roles = "GERENTE")]
        public async Task<ActionResult<object>> PutEmpleadosG([FromBody] DataLayer.EntityModel.PersonaEntity Persona)
        {


            string bImg = Persona.C_Img_Base, nombre = Persona.C_Primer_Nombre;



            if (Persona.C_Fecha_Nacimiento < DateTime.Today.AddYears(-100) || Persona.C_Fecha_Nacimiento > DateTime.Today.AddYears(-18))
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = 24,
                    Transaccion_Mensaje = "Rango de Fechas no es correcto solo se admite edades mayores de 18 y menores de 100 años",
                });
            }
            else
            {
                LogicLayer.Logica.Metodos metodos = new LogicLayer.Logica.Metodos();



                if (!bImg.Equals("0") && !(bImg.StartsWith("http://") || bImg.StartsWith("https://")))
                {

                    Persona.C_Url_Img = await metodos.SubirIMGAsync(bImg, nombre, "ImgEmpleado");
                }




                Persona persona = new Persona(Persona.C_Url_Img);

                if (persona.putActualizarEmpleadosG(ref Persona))
                {
                    return Ok(new
                    {
                        ok = true,
                        Transaccion_Estado = Persona.C_Transaccion_Estado,
                        Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                    });
                }
                else
                {
                    return Ok(new
                    {
                        ok = false,
                        Transaccion_Estado = Persona.C_Transaccion_Estado,
                        Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                    });
                }
            }

        }

        [HttpGet]
        [Route("GetEmpleadosG")]
        public ActionResult<object> GetEmpleadosG(int Id_SL)
        {
            List<DataLayer.EntityModel.PersonaEntity> PersonaList = new List<DataLayer.EntityModel.PersonaEntity>();
            Persona persona = new Persona(Id_SL);



            if (persona.GetEmpleadosG(ref PersonaList))
            {
                return Ok(new
                {
                    ok = true,
                    Response = PersonaList
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = PersonaList
                });
            }

        }


        [HttpPost]
        [Route("SetProveedor")]
        // [Authorize(Roles = "GERENTE")]
        public async Task<ActionResult<object>> SetProveedor([FromBody] DataLayer.EntityModel.PersonaEntity Persona)
        {


            string bImg = Persona.C_Img_Base, nombre = Persona.C_Primer_Nombre;



            if (Persona.C_Id_Rol_Persona == 3 && (Persona.C_Fecha_Nacimiento < DateTime.Today.AddYears(-100) || Persona.C_Fecha_Nacimiento > DateTime.Today.AddYears(-18)))
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = 24,
                    Transaccion_Mensaje = "Rango de Fechas no es correcto solo se admite edades mayores de 18 y menores de 100 años",
                });
            }
            else
            {
                if (Persona.C_Id_Rol_Persona == 2 && (Persona.C_Fecha_Nacimiento < DateTime.Today.AddYears(-100) || Persona.C_Fecha_Nacimiento > DateTime.Today.AddYears(-18)))
                {

                    return Ok(new
                    {
                        ok = false,
                        Transaccion_Estado = 24,
                        Transaccion_Mensaje = "Rango de Fechas no es correcto solo se admite edades mayores de 6 y menores de 100 años",
                    });

                }
                else
                {
                    LogicLayer.Logica.Metodos metodos = new LogicLayer.Logica.Metodos();



                    if (!bImg.Equals("0"))
                    {
                        Persona.C_Url_Img = await metodos.SubirIMGAsync(bImg, nombre, "ImgPersona");
                    }
                    else
                    {
                        Persona.C_Url_Img = "https://img.srvcentral.com/Sistema/ImagenPorDefecto/Registro.png";
                    }



                    if (!Persona.C_Url_Img.Equals("0") || !Persona.C_Url_Img.StartsWith("Error"))
                    {
                        Persona persona = new Persona(Persona.C_Url_Img);

                        if (persona.SetProveedor(ref Persona))
                        {
                            return Ok(new
                            {
                                ok = true,
                                Transaccion_Estado = Persona.C_Transaccion_Estado,
                                Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                            });
                        }
                        else
                        {
                            return Ok(new
                            {
                                ok = false,
                                Transaccion_Estado = Persona.C_Transaccion_Estado,
                                Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                            });
                        }
                    }
                    else
                    {
                        return Ok(new
                        {
                            ok = false,
                            Transaccion_Estado = 25,
                            Transaccion_Mensaje = Persona.C_Url_Img,
                        });
                    }

                }

            }

        }

        [HttpGet]
        [Route("GetProveedor")]
        // [Authorize(Roles = "GERENTE")]
        public ActionResult<object> GetProveedor()
        {
            List<DataLayer.EntityModel.PersonaEntity> PersonaList = new List<DataLayer.EntityModel.PersonaEntity>();
            Persona persona = new Persona();



            if (persona.GetProveedor(ref PersonaList))
            {
                return Ok(new
                {
                    ok = true,
                    Response = PersonaList
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = PersonaList
                });
            }

        }

        [HttpGet]
        [Route("GetProveedorById")]
        // [Authorize(Roles = "GERENTE")]
        public ActionResult<object> GetProveedorById(int IdPersona)
        {
            DataLayer.EntityModel.PersonaEntity Persona = new DataLayer.EntityModel.PersonaEntity();
            Persona persona = new Persona(IdPersona);

            if (persona.GetProveedorById(ref Persona))
            {
                return Ok(new
                {
                    ok = true,
                    Response = Persona
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = Persona
                });
            }

        }




        [HttpPost]
        [Route("SetCliente")]
        // [Authorize(Roles = "CAJERO")]
        public async Task<ActionResult<object>> SetCliente([FromBody] DataLayer.EntityModel.PersonaEntity Persona)
        {


            string bImg = Persona.C_Img_Base, nombre = Persona.C_Primer_Nombre;



            if (Persona.C_Id_Rol_Persona == 3 && (Persona.C_Fecha_Nacimiento < DateTime.Today.AddYears(-100) || Persona.C_Fecha_Nacimiento > DateTime.Today.AddYears(-18)))
            {
                return Ok(new
                {
                    ok = false,
                    Transaccion_Estado = 24,
                    Transaccion_Mensaje = "Rango de Fechas no es correcto solo se admite edades mayores de 18 y menores de 100 años",
                });
            }
            else
            {
                if (Persona.C_Id_Rol_Persona == 2 && (Persona.C_Fecha_Nacimiento < DateTime.Today.AddYears(-100) || Persona.C_Fecha_Nacimiento > DateTime.Today.AddYears(-18)))
                {

                    return Ok(new
                    {
                        ok = false,
                        Transaccion_Estado = 24,
                        Transaccion_Mensaje = "Rango de Fechas no es correcto solo se admite edades mayores de 6 y menores de 100 años",
                    });

                }
                else
                {
                    LogicLayer.Logica.Metodos metodos = new LogicLayer.Logica.Metodos();



                    if (!bImg.Equals("0"))
                    {
                        Persona.C_Url_Img = await metodos.SubirIMGAsync(bImg, nombre, "ImgPersona");
                    }
                    else
                    {
                        Persona.C_Url_Img = "https://img.srvcentral.com/Sistema/ImagenPorDefecto/Registro.png";
                    }



                    if (!Persona.C_Url_Img.Equals("0") || !Persona.C_Url_Img.StartsWith("Error"))
                    {
                        Persona persona = new Persona(Persona.C_Url_Img);

                        if (persona.SetCliente(ref Persona))
                        {
                            return Ok(new
                            {
                                ok = true,
                                Transaccion_Estado = Persona.C_Transaccion_Estado,
                                Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
								C_Id_Persona = Persona.C_Id_Persona
							});
                        }
                        else
                        {
                            return Ok(new
                            {
                                ok = false,
                                Transaccion_Estado = Persona.C_Transaccion_Estado,
                                Transaccion_Mensaje = Persona.C_Transaccion_Mensaje,
                                
                            });
                        }
                    }
                    else
                    {
                        return Ok(new
                        {
                            ok = false,
                            Transaccion_Estado = 25,
                            Transaccion_Mensaje = Persona.C_Url_Img,
                        });
                    }

                }

            }

        }
        [HttpGet]
        [Route("GetCliente")]
        // [Authorize(Roles = "GERENTE,CAJERO")]
        public ActionResult<object> GetCliente()
        {
            List<DataLayer.EntityModel.PersonaEntity> PersonaList = new List<DataLayer.EntityModel.PersonaEntity>();
            Persona persona = new Persona();



            if (persona.GetCliente(ref PersonaList))
            {
                return Ok(new
                {
                    ok = true,
                    Response = PersonaList
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = PersonaList
                });
            }

        }


        [HttpGet]
        [Route("GetPersonaFacturacion")]
        // [Authorize(Roles = "ADMINISTRADOR,GERENTE,CAJERO")]
        public ActionResult<object> GetPersonaFacturacion()
        {
            List<DataLayer.EntityModel.PersonaEntity> PersonaList = new List<DataLayer.EntityModel.PersonaEntity>();
            Persona persona = new Persona();



            if (persona.GetPersonaFacturacion(ref PersonaList))
            {
                return Ok(new
                {
                    ok = true,
                    Response = PersonaList
                });

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = PersonaList
                });
            }

        }




       




        [HttpGet]
        [Route("GetPersonaFacturacionById")]
        public ActionResult<object> GetPersonaFacturacionById(int IdPersona)
        {
            DataLayer.EntityModel.PersonaEntity Persona = new DataLayer.EntityModel.PersonaEntity();
            Persona persona = new Persona(IdPersona);

            if (persona.GetPersonaFacturacionById(ref Persona))
            {
                return Ok(new
                {
                    ok = true,
                    Response = Persona







				});

            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    Response = Persona
                });
            }

        }



		[HttpGet]
		[Route("GetPersonaFacturacionByParametro")]
		public ActionResult<object> GetPersonaFacturacionByParametro(string valor)

		{

       
			List <DataLayer.EntityModel.PersonaEntity> Persona = new List<DataLayer.EntityModel.PersonaEntity>();
			Persona persona = new Persona(valor);

			if (persona.GetPersonaFacturacionByParametro(ref Persona))
			{
				return Ok(new
				{
					ok = true,
					Response = Persona
				});

			}
			else
			{
				return Ok(new
				{
					ok = false,
					Response = Persona
				});
			}

		}
























	}

}