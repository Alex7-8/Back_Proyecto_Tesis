using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataLayer.EntityModel
{
        public class ReportesEntity
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string C_Numero_Factura { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string C_Codigo { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string C_Nombre { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string C_Cantidad { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string C_Precio { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string C_SubTotal { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string C_IVA { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string C_Fecha { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string C_Total { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string C_Usuario { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string C_Tipo { get; set; }



		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string? C_Transaccion_Mensaje { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public int C_Transaccion_Estado { get; set; }

	}


   

}