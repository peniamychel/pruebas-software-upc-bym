using System.Data;
using backend.connection;
using backend.entidades;
using Dapper;


namespace backend.servicios
{
    public static class UsuarioLoginServicios
    {
        //Busca un usuario por usuario y contrasenia
        public static T ObtenerByUserAndPass<T>(string user, string pass)
        {
            const string storedProcedureName = "SearchUserByUserAndPass";
            var parameter = new DynamicParameters();
            parameter.Add("@UserName", user, DbType.String);
            parameter.Add("@Password", pass, DbType.String);
            var result = BDManager.GetInstance.ProcedureGetDataWithParameters<T>(storedProcedureName, parameter);
            return result.FirstOrDefault();
        }


        //
        public static int changeUserPassword(Usuarios usuarios, string pass)
        {
            const string storedProcedureName = "changeUserPasswordDB";
            var parameter = new DynamicParameters();
            parameter.Add("@UserID", usuarios.Id, DbType.String);
            parameter.Add("@NewPassword", pass, DbType.String);
            var result = BDManager.GetInstance.ProcedureUpdateData(storedProcedureName, parameter);

            return result;
        }
    }
}