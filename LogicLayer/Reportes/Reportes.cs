using DataLayer.Conexion;
using DataLayer.EntityModel;
using LogicLayer.Helper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text.Json.Serialization;

namespace LogicLayer.Reportes
	
{

	public class Reportes

	{




		private string tipo;

		public Reportes(string tipo)
		{
			this.tipo = tipo;
		}

		
		public bool GetReporteFacturaByDia(ref List<ReportesEntity> reporteEntity)
		{
			DataLayer.EntityModel.ReportesEntity ReporteEntity = new DataLayer.EntityModel.ReportesEntity();
			bool res = false;
			try
			{
				IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				/*Validar usuario en BD*/
				EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_REPORTES", "", "");
				objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", this.tipo);


				DataTable data = new DataTable();
				string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


				if (string.IsNullOrEmpty(msgResEjecucion))
				{
					List<string> listempty = new List<string>();

					if (data.Rows.Count > 0)
					{
						int estado = 0;

						foreach (DataRow row in data.Rows)
						{
							ReporteEntity = new DataLayer.EntityModel.ReportesEntity();
							ReporteEntity.C_Numero_Factura = row["NUMERO_FACTURA"].ToString();
							ReporteEntity.C_Codigo = row["CODIGO"].ToString();
							ReporteEntity.C_Nombre = row["NOMBRE"].ToString();
							ReporteEntity.C_Cantidad = row["CANTIDAD"].ToString();
							ReporteEntity.C_Precio = row["PRECIO"].ToString();
							ReporteEntity.C_SubTotal = row["SUBTOTAL"].ToString();
							ReporteEntity.C_IVA = row["IVA"].ToString();
							ReporteEntity.C_Total = row["TOTAL"].ToString();
							ReporteEntity.C_Fecha = row["FECHA_CREACION"].ToString();
							ReporteEntity.C_Usuario = row["USUARIO_CREACION"].ToString();
							ReporteEntity.C_Tipo = row["TIPO"].ToString();


							reporteEntity.Add(ReporteEntity);

						}

						res = true;

					}
					else
					{
						ReporteEntity.C_Transaccion_Mensaje = "No hay datos";
						ReporteEntity.C_Transaccion_Estado = 30;
						reporteEntity.Add(ReporteEntity);
						res = false;
					}

				}
				else
				{

					ReporteEntity.C_Transaccion_Estado = 32;
					ReporteEntity.C_Transaccion_Mensaje = msgResEjecucion;
					reporteEntity.Add(ReporteEntity);
					res = false;
				}
			}
			catch (Exception e)
			{
				ReporteEntity.C_Transaccion_Estado = 35;
				ReporteEntity.C_Transaccion_Mensaje = e.Message;
				reporteEntity.Add(ReporteEntity);
				res = false;
			}
			return res;
		}
	}
}
