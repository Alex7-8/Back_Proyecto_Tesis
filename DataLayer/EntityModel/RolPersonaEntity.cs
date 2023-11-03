﻿using System.Text.Json.Serialization;

namespace DataLayer.EntityModel
{
        public class RolPersonaEntity
        {
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public int C_Id_Rol_Persona { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public string C_Descripcion { get; set; }
           

            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? C_Transaccion_Mensaje { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public int C_Transaccion_Estado { get; set; }

    }

}