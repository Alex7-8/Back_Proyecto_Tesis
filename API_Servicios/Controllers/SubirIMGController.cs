using DataLayer.EntityModel;
using Google.Cloud.Storage.V1;
using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace API_Servicios.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class SubirIMGController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> PostImage([FromBody] IMGEntity model)
        {
            string imageFromFirebase = await UploadImage(model);
            return Ok( new
            {
                Ok = true,
                ImageUrl = imageFromFirebase
            }
             );
        }

        private static async Task<string> UploadImage(IMGEntity model)
        {
            var imageFromBase64ToStream = IMG.ConvertBase64ToStream(model.Image);
            var imageStream = imageFromBase64ToStream.ReadAsStream();

            string imageFromFirebase = await IMG.UploadImage(imageStream, model);
            return imageFromFirebase;
        }



    }
}