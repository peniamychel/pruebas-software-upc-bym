using System.Data;
using backend.connection;
using backend.entidades;
using Dapper;

namespace backend.servicios
{
    public static class UsuariosServicios
    {
        public static IEnumerable<T> ObtenerTodo<T>()
        {
            const string sql = "select * from usuarios";
            return BDManager.GetInstance.GetData<T>(sql);//Dapper
        }

        //obtener los 10 ultimos
        public static IEnumerable<T> ObtenerTodoTopDiez<T>()
        {
            const string sql = "SELECT TOP 10 * FROM usuarios ORDER BY ID DESC";
            return BDManager.GetInstance.GetData<T>(sql);//Dapper
        }

        public static T ObtenerById<T>(int id)
        {
            const string sql = "select * from usuarios where ID = @Id and estado_registro = 1";

            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int64);

            var result = BDManager.GetInstance.GetDataWithParameters<T>(sql, parameters);

            return result.FirstOrDefault();
        }
        
        public static int InsertUsuario(Usuarios usuarios)
        {
            const string sql = "INSERT INTO [dbo].[USUARIOS]([USER_NAME], [NOMBRE_COMPLETO], [PASSWORD]) VALUES (@user_name, @nombre_completo, @password) ";

            var parameters = new DynamicParameters();
            parameters.Add("user_name", usuarios.UserName, DbType.String);
            parameters.Add("nombre_completo", usuarios.NombreCompleto, DbType.String);
            parameters.Add("password", usuarios.Password, DbType.String);

            var result = BDManager.GetInstance.SetData(sql, parameters);
            return result;
        }
        
        //*********************METODOS PROCEDIMIENTO ALMACENADOS***********

        //optener todo con procedimento almacenado
        public static IEnumerable<T> ObtenerTodo2<T>()
        {
            const string sql = "ObtenerUsuarios";
            return BDManager.GetInstance.ProcedureGetData<T>(sql);//Dapper
        }

        //OBTENER SEGUN ID con procedimento almacenado
        public static T ObtenerById2<T>(int id)
        {
            const string storedProcedure = "ObtenerUsuarioPorId";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            var result = BDManager.GetInstance.ProcedureGetDataWithParameters<T>(storedProcedure, parameters);

            return result.FirstOrDefault();
        }
        
        //INSERTA DATOS con procedimento almacenado
        public static int InsertUsuario2(Usuarios usuarios)
        {
            const string storedProcedure = "InsertUsuario";

            var parameters = new DynamicParameters();
            parameters.Add("@user_name", usuarios.UserName, DbType.String);
            parameters.Add("@nombre_completo", usuarios.NombreCompleto, DbType.String);
            parameters.Add("@password", usuarios.Password, DbType.String);

            var result = BDManager.GetInstance.ProcedureInsertData(storedProcedure, parameters);
            return result;
        }

        //ELIMINIAR SEGUN ID con procedimento almacenado 
        public static void EliminarPorIdUsuarios(int id)
        {
            const string storedProcedure = "EliminarUsuarioPorId";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32);
            BDManager.GetInstance.ProcedureDeleteDataWithId(storedProcedure, parameters);
        }

        //ACTUALIZAR SEGUN ID con procedimento almacenado 
        public static int ActualizarUsuarios(Usuarios usuarios)
        {   const string storedProcedure = "UpdateUsuario";

            var parameters = new DynamicParameters();
            parameters.Add("@user_name", usuarios.UserName, DbType.String);
            parameters.Add("@nombre_completo", usuarios.NombreCompleto, DbType.String);
            parameters.Add("@password", usuarios.Password, DbType.String);
            parameters.Add("@Id", usuarios.Id, DbType.Int32);

            var result = BDManager.GetInstance.ProcedureUpdateData(storedProcedure, parameters);
            return result;
        }

        
    }
}
