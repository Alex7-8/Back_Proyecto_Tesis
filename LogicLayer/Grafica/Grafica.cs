using DataLayer.Conexion;
using DataLayer.EntityModel;
using LogicLayer.Helper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text.Json.Serialization;
namespace LogicLayer.Grafica
{
    public class Grafica
	{
		


		public bool GetGrafiaByAnio(ref List<GraficaEntity> graficaEntity)
		{
			DataLayer.EntityModel.GraficaEntity GraficaEntity = new DataLayer.EntityModel.GraficaEntity();
			bool res = false;
			try
			{
				IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				/*Validar usuario en BD*/
				EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_GRAFICAS", "", "");
				objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GA");


				DataTable data = new DataTable();
				string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


				if (string.IsNullOrEmpty(msgResEjecucion))
				{
					List<string> listempty = new List<string>();

					if (data.Rows.Count > 0)
					{

						foreach (DataRow row in data.Rows)
						{
							GraficaEntity = new DataLayer.EntityModel.GraficaEntity();
							GraficaEntity.C_Nombre = row["NOMBRE"].ToString();
							GraficaEntity.C_Total = row["TOTAL"].ToString();

							graficaEntity.Add(GraficaEntity);

						}

						res = true;

					}
					else
					{
						GraficaEntity.C_Nombre = "No hay datos";
						GraficaEntity.C_Total = "0.00";
						graficaEntity.Add(GraficaEntity);
						res = false;
					}

				}
				else
				{

					GraficaEntity.C_Transaccion_Estado = 32;
					GraficaEntity.C_Transaccion_Mensaje = msgResEjecucion;
					graficaEntity.Add(GraficaEntity);
					res = false;
				}
			}
			catch (Exception e)
			{
				GraficaEntity.C_Transaccion_Estado = 35;
				GraficaEntity.C_Transaccion_Mensaje = e.Message;
				graficaEntity.Add(GraficaEntity);
				res = false;
			}
			return res;
		}

		public bool GetGrafiaByDia(ref List<GraficaEntity> graficaEntity)
		{
			DataLayer.EntityModel.GraficaEntity GraficaEntity = new DataLayer.EntityModel.GraficaEntity();
			bool res = false;
			try
			{
				IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				/*Validar usuario en BD*/
				EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_GRAFICAS", "", "");
				objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GD");
				

				DataTable data = new DataTable();
				string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


				if (string.IsNullOrEmpty(msgResEjecucion))
				{
					List<string> listempty = new List<string>();

					if (data.Rows.Count > 0)
					{

						foreach (DataRow row in data.Rows)
						{
							GraficaEntity = new DataLayer.EntityModel.GraficaEntity();
							GraficaEntity.C_Nombre = row["NOMBRE"].ToString();
							GraficaEntity.C_Total = row["TOTAL"].ToString();

							graficaEntity.Add(GraficaEntity);

						}

						res = true;

					}
					else
					{
						GraficaEntity.C_Nombre = "No hay datos";
						GraficaEntity.C_Total = "0.00";
						graficaEntity.Add(GraficaEntity);
						res = false;
					}

				}
				else
				{

					GraficaEntity.C_Transaccion_Estado = 32;
					GraficaEntity.C_Transaccion_Mensaje = msgResEjecucion;
					graficaEntity.Add(GraficaEntity);
					res = false;
				}
			}
			catch (Exception e)
			{
				GraficaEntity.C_Transaccion_Estado = 35;
				GraficaEntity.C_Transaccion_Mensaje = e.Message;
				graficaEntity.Add(GraficaEntity);
				res = false;
			}
			return res;
		}

		public bool GetGrafiaByMes(ref List<GraficaEntity> graficaEntity)
		{
			DataLayer.EntityModel.GraficaEntity GraficaEntity = new DataLayer.EntityModel.GraficaEntity();
			bool res = false;
			try
			{
				IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				/*Validar usuario en BD*/
				EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_GRAFICAS", "", "");
				objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GM");


				DataTable data = new DataTable();
				string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


				if (string.IsNullOrEmpty(msgResEjecucion))
				{
					List<string> listempty = new List<string>();

					if (data.Rows.Count > 0)
					{

						foreach (DataRow row in data.Rows)
						{
							GraficaEntity = new DataLayer.EntityModel.GraficaEntity();
							GraficaEntity.C_Nombre = row["NOMBRE"].ToString();
							GraficaEntity.C_Total = row["TOTAL"].ToString();

							graficaEntity.Add(GraficaEntity);

						}

						res = true;

					}
					else
					{
						GraficaEntity.C_Nombre = "No hay datos";
						GraficaEntity.C_Total = "0.00";
						graficaEntity.Add(GraficaEntity);
						res = false;
					}

				}
				else
				{

					GraficaEntity.C_Transaccion_Estado = 32;
					GraficaEntity.C_Transaccion_Mensaje = msgResEjecucion;
					graficaEntity.Add(GraficaEntity);
					res = false;
				}
			}
			catch (Exception e)
			{
				GraficaEntity.C_Transaccion_Estado = 35;
				GraficaEntity.C_Transaccion_Mensaje = e.Message;
				graficaEntity.Add(GraficaEntity);
				res = false;
			}
			return res;
		}

		public bool GetGrafiaBySemana(ref List<GraficaEntity> graficaEntity)
		{
			DataLayer.EntityModel.GraficaEntity GraficaEntity = new DataLayer.EntityModel.GraficaEntity();
			bool res = false;
			try
			{
				IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				/*Validar usuario en BD*/
				EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_GRAFICAS", "", "");
				objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "GS");


				DataTable data = new DataTable();
				string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


				if (string.IsNullOrEmpty(msgResEjecucion))
				{
					List<string> listempty = new List<string>();

					if (data.Rows.Count > 0)
					{

						foreach (DataRow row in data.Rows)
						{
							GraficaEntity = new DataLayer.EntityModel.GraficaEntity();
							GraficaEntity.C_Nombre = row["NOMBRE"].ToString();
							GraficaEntity.C_Total = row["TOTAL"].ToString();

							graficaEntity.Add(GraficaEntity);

						}

						res = true;

					}
					else
					{
						GraficaEntity.C_Nombre = "No hay datos";
						GraficaEntity.C_Total = "0.00";
						graficaEntity.Add(GraficaEntity);
						res = false;
					}

				}
				else
				{

					GraficaEntity.C_Transaccion_Estado = 32;
					GraficaEntity.C_Transaccion_Mensaje = msgResEjecucion;
					graficaEntity.Add(GraficaEntity);
					res = false;
				}
			}
			catch (Exception e)
			{
				GraficaEntity.C_Transaccion_Estado = 35;
				GraficaEntity.C_Transaccion_Mensaje = e.Message;
				graficaEntity.Add(GraficaEntity);
				res = false;
			}
			return res;
		}

		public bool GetGrafiaServicioByAnio(ref List<GraficaEntity> graficaEntity)
		{
			DataLayer.EntityModel.GraficaEntity GraficaEntity = new DataLayer.EntityModel.GraficaEntity();
			bool res = false;
			try
			{
				IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				/*Validar usuario en BD*/
				EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_GRAFICAS", "", "");
				objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "SA");


				DataTable data = new DataTable();
				string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


				if (string.IsNullOrEmpty(msgResEjecucion))
				{
					List<string> listempty = new List<string>();

					if (data.Rows.Count > 0)
					{

						foreach (DataRow row in data.Rows)
						{
							GraficaEntity = new DataLayer.EntityModel.GraficaEntity();
							GraficaEntity.C_Nombre = row["NOMBRE"].ToString();
							GraficaEntity.C_Total = row["TOTAL"].ToString();

							graficaEntity.Add(GraficaEntity);

						}

						res = true;

					}
					else
					{
						GraficaEntity.C_Nombre = "No hay datos";
						GraficaEntity.C_Total = "0.00";
						graficaEntity.Add(GraficaEntity);
						res = false;
					}

				}
				else
				{

					GraficaEntity.C_Transaccion_Estado = 32;
					GraficaEntity.C_Transaccion_Mensaje = msgResEjecucion;
					graficaEntity.Add(GraficaEntity);
					res = false;
				}
			}
			catch (Exception e)
			{
				GraficaEntity.C_Transaccion_Estado = 35;
				GraficaEntity.C_Transaccion_Mensaje = e.Message;
				graficaEntity.Add(GraficaEntity);
				res = false;
			}
			return res;
		}

		public bool GetGrafiaServicioByDia(ref List<GraficaEntity> graficaEntity)
		{
			DataLayer.EntityModel.GraficaEntity GraficaEntity = new DataLayer.EntityModel.GraficaEntity();
			bool res = false;
			try
			{
				IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				/*Validar usuario en BD*/
				EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_GRAFICAS", "", "");
				objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "SD");


				DataTable data = new DataTable();
				string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


				if (string.IsNullOrEmpty(msgResEjecucion))
				{
					List<string> listempty = new List<string>();

					if (data.Rows.Count > 0)
					{

						foreach (DataRow row in data.Rows)
						{
							GraficaEntity = new DataLayer.EntityModel.GraficaEntity();
							GraficaEntity.C_Nombre = row["NOMBRE"].ToString();
							GraficaEntity.C_Total = row["TOTAL"].ToString();

							graficaEntity.Add(GraficaEntity);

						}

						res = true;

					}
					else
					{
						GraficaEntity.C_Nombre = "No hay datos";
						GraficaEntity.C_Total = "0.00";
						graficaEntity.Add(GraficaEntity);
						res = false;
					}

				}
				else
				{

					GraficaEntity.C_Transaccion_Estado = 32;
					GraficaEntity.C_Transaccion_Mensaje = msgResEjecucion;
					graficaEntity.Add(GraficaEntity);
					res = false;
				}
			}
			catch (Exception e)
			{
				GraficaEntity.C_Transaccion_Estado = 35;
				GraficaEntity.C_Transaccion_Mensaje = e.Message;
				graficaEntity.Add(GraficaEntity);
				res = false;
			}
			return res;
		}

		public bool GetGrafiaServicioByMes(ref List<GraficaEntity> graficaEntity)
		{
			DataLayer.EntityModel.GraficaEntity GraficaEntity = new DataLayer.EntityModel.GraficaEntity();
			bool res = false;
			try
			{
				IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				/*Validar usuario en BD*/
				EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_GRAFICAS", "", "");
				objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "SM");


				DataTable data = new DataTable();
				string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


				if (string.IsNullOrEmpty(msgResEjecucion))
				{
					List<string> listempty = new List<string>();

					if (data.Rows.Count > 0)
					{

						foreach (DataRow row in data.Rows)
						{
							GraficaEntity = new DataLayer.EntityModel.GraficaEntity();
							GraficaEntity.C_Nombre = row["NOMBRE"].ToString();
							GraficaEntity.C_Total = row["TOTAL"].ToString();

							graficaEntity.Add(GraficaEntity);

						}

						res = true;

					}
					else
					{
						GraficaEntity.C_Nombre = "No hay datos";
						GraficaEntity.C_Total = "0.00";
						graficaEntity.Add(GraficaEntity);
						res = false;
					}

				}
				else
				{

					GraficaEntity.C_Transaccion_Estado = 32;
					GraficaEntity.C_Transaccion_Mensaje = msgResEjecucion;
					graficaEntity.Add(GraficaEntity);
					res = false;
				}
			}
			catch (Exception e)
			{
				GraficaEntity.C_Transaccion_Estado = 35;
				GraficaEntity.C_Transaccion_Mensaje = e.Message;
				graficaEntity.Add(GraficaEntity);
				res = false;
			}
			return res;
		}

		public bool GetGrafiaServicioBySemana(ref List<GraficaEntity> graficaEntity)
		{
			DataLayer.EntityModel.GraficaEntity GraficaEntity = new DataLayer.EntityModel.GraficaEntity();
			bool res = false;
			try
			{
				IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				/*Validar usuario en BD*/
				EjectProcAlm objStoreProc = new EjectProcAlm("SP_GET_GRAFICAS", "", "");
				objStoreProc.Add_Par_VarChar_Input("@TIPO_TRANSACCION", "SS");


				DataTable data = new DataTable();
				string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


				if (string.IsNullOrEmpty(msgResEjecucion))
				{
					List<string> listempty = new List<string>();

					if (data.Rows.Count > 0)
					{

						foreach (DataRow row in data.Rows)
						{
							GraficaEntity = new DataLayer.EntityModel.GraficaEntity();
							GraficaEntity.C_Nombre = row["NOMBRE"].ToString();
							GraficaEntity.C_Total = row["TOTAL"].ToString();

							graficaEntity.Add(GraficaEntity);

						}

						res = true;

					}
					else
					{
						GraficaEntity.C_Nombre = "No hay datos";
						GraficaEntity.C_Total = "0.00";
						graficaEntity.Add(GraficaEntity);
						res = false;
					}

				}
				else
				{

					GraficaEntity.C_Transaccion_Estado = 32;
					GraficaEntity.C_Transaccion_Mensaje = msgResEjecucion;
					graficaEntity.Add(GraficaEntity);
					res = false;
				}
			}
			catch (Exception e)
			{
				GraficaEntity.C_Transaccion_Estado = 35;
				GraficaEntity.C_Transaccion_Mensaje = e.Message;
				graficaEntity.Add(GraficaEntity);
				res = false;
			}
			return res;
		}
	}
}
