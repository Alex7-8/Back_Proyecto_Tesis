using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataLayer.EntityModel
{
        public class GraficaEntity
	{
          
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string C_Nombre { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	    public string C_Total { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string? C_Transaccion_Mensaje { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public int C_Transaccion_Estado { get; set; }

	}


   

}