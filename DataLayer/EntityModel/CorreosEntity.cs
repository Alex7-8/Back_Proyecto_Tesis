using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataLayer.EntityModel
{
   public class CorreosEntity
    {
       
            public string To { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }


            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
             public string? C_Transaccion_Mensaje { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public int? C_Transaccion_Estado { get; set; }

		
	}
}
