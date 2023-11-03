using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataLayer.EntityModel
{
        public class CuentaEntity
        {
		    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		    public int C_Id_Persona { get; set; }
		    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public string C_Id_Cuenta { get; set; }
		    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		    public string C_Saldo { get; set; }
		    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		    public string C_Monto_Maximo { get; set; }
		    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public string C_Nombre_Completo { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public string C_Sucursal { get; set; }
		    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		    public string C_Url_Img { get; set; }
		    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		    public int C_Id_Estado { get; set; }
		    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public int C_Id_Tipo { get; set; }
		   [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public int C_Id_Tipo_C { get; set; }
		    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
		    public decimal C_Monto { get; set; }
			[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
			public string C_Descripcion { get; set; }

			[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public string C_Usuario_Creacion { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
             public string C_Usuario_Modificacion { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? C_Fecha_Creacion { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? C_Fecha_Modificacion { get; set; }

            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? C_Transaccion_Mensaje { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public int C_Transaccion_Estado { get; set; }

    }

}