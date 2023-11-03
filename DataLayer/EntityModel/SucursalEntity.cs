using System.Text.Json.Serialization;

namespace DataLayer.EntityModel
{
        public class SucursalEntity
        {
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public int C_Id_Sucursal { get; set;}
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public int C_Id_Municipio { get; set;}
             [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string C_Nombre { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string C_Url_Img { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string C_Direccion { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public int C_Estado { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string C_Descripcion { get; set; }

            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? C_Img_Base { get; set; }

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