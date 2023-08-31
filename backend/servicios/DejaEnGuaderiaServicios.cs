using System.Data;
using backend.connection;
using backend.entidades;
using Dapper;

namespace backend.servicios
{
    public static class DejaEnGuarderiaServicio
    {

        //************METODOS CON PROCEDIMIENTO ALMACENADO*********
        public static IEnumerable<T> ObtenerLestTen<T>()
        {
            const string sql = "getEndRegistros";
            return BDManager.GetInstance.ProcedureGetData<T>(sql);//Dapper
        }

        public static int InsertDejaEnGuarderiaServicio(DejaEnGuarderia dejaEnGuarderia)
        {
            const string storedProcedure = "insertaDejaEnGuarderia";

            var parameters = new DynamicParameters();
            parameters.Add("@IdNino", dejaEnGuarderia.IdNinio, DbType.Int64);
            parameters.Add("@IdUsuario", dejaEnGuarderia.IdUsuario, DbType.Int64);

            var result = BDManager.GetInstance.ProcedureInsertData(storedProcedure, parameters);
            return result;
        }
    }
}
