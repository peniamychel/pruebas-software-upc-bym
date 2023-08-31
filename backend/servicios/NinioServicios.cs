using System.Data;
using backend.connection;
using backend.entidades;
using Dapper;

namespace backend.servicios
{
    public static class NinioServicios
    {
        public static IEnumerable<T> ObtenerLestTen<T>()
        {
            const string sql = "Obtener_Ultimos_Registros_Ninio";
            return BDManager.GetInstance.ProcedureGetData<T>(sql);//Dapper
        }

        public static int InsertNinioServicio(Ninio ninio)
        {
            const string storedProcedure = "InsertarNinio";

            var parameters = new DynamicParameters();
            parameters.Add("@NOMBRE", ninio.Nombre, DbType.String);
            parameters.Add("@EDAD", ninio.Edad, DbType.Int64);
            parameters.Add("@SEXO", ninio.Sexo, DbType.String);

            var result = BDManager.GetInstance.ProcedureInsertData(storedProcedure, parameters);
            return result;
        }

        public static T ObtenerById<T>(int id)
        {
            const string storedProcedure = "ObtenerNinioPorID";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            var result = BDManager.GetInstance.ProcedureGetDataWithParameters<T>(storedProcedure, parameters);

            return result.FirstOrDefault();
        }

        public static void EliminarPorIdNinio(int id)
        {
            const string storedProcedure = "CambiarEstadoRegistroCero";

            var parameters = new DynamicParameters();
            parameters.Add("@ID", id, DbType.Int32);
            BDManager.GetInstance.ProcedureDeleteDataWithId(storedProcedure, parameters);
        }

        public static int ActualizarNinio(Ninio ninio)
        {   const string storedProcedure = "EditarNinio";

            var parameters = new DynamicParameters();
            parameters.Add("@ID", ninio.Id, DbType.Int32);
            parameters.Add("@NOMBRE", ninio.Nombre, DbType.String);
            parameters.Add("@EDAD", ninio.Edad, DbType.Int32);
            parameters.Add("@SEXO", ninio.Sexo, DbType.String);
            parameters.Add("@USUARIO_REGISTRO", ninio.UsuarioRegistro, DbType.String);
            parameters.Add("@FECHA_REGISTRO", ninio.FechaRegistro, DbType.Date);
            parameters.Add("@ESTADO_REGISTRO", ninio.EstadoRegistro, DbType.Int32);

            var result = BDManager.GetInstance.ProcedureUpdateData(storedProcedure, parameters);
            return result;
        }


    }
}
