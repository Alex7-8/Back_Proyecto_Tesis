using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataLayer.EntityModel
{
        public class ServiciosEntity
        {
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public int C_Id_Servicio { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public string C_Nombre_Servicio { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public string C_Descripcion { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string C_Url_IMG { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? C_Img_Base { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public int C_Estado { get; set; }

            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
            public decimal C_Precio { get; set; }


            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? C_Precio_Final { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string C_Usuario_Creacion { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
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