using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace backend.connection
{
    //Clase de conexion con la base de datos que utiliza el ORM de Dapper
    public sealed class BDManager
    {

        private static readonly object lockObj = new();
        private static BDManager? instance;

        private BDManager()
        {

        }

        //Uso del Patron de Diseño SINGLETON
        public static BDManager GetInstance
        {
            get
            {
                lock (lockObj)
                {

                    if (instance == null)
                    {
                        instance = new BDManager();
                    }
                }
                return instance;
            }
        }

        // Cadena de conexion que se obtiene externamente
        public string? ConnectionString { get; set; }

        //Metodo para obtener un listado de la base de datos (Dapper)
        public IEnumerable<T> GetData<T>(string sql)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            return connection.Query<T>(sql);
        }

        //Metodo para obtener un listado de la base de datos con parametros (Dapper)
        public IEnumerable<T> GetDataWithParameters<T>(string sql, DynamicParameters dynamicParameters)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            return connection.Query<T>(sql, dynamicParameters);
        }

        //Metodo para escribir en la base de datos
        public int SetData(string sql, DynamicParameters dynamicParameters)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            return connection.Execute(sql, dynamicParameters);
        }

        //eliminar seguni id
        public void DeleteDataWithParameters(string sql, DynamicParameters dynamicParameters)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            connection.Execute(sql, dynamicParameters);
        }



        //***************METODO SON PROCEDIMIEBTO ALMACENADOS***************

        //traer todos los dattos 
        public IEnumerable<T> ProcedureGetData<T>(string procedureName)
        {
            using var connection = new SqlConnection(ConnectionString);

            connection.Open();

            DefaultTypeMap.MatchNamesWithUnderscores = true;
            return connection.Query<T>(procedureName, commandType: CommandType.StoredProcedure);
        }

        //traer datos segun id con  procedimiento alacenado
        public IEnumerable<T> ProcedureGetDataWithParameters<T>(string storedProcedure, DynamicParameters dynamicParameters)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            return connection.Query<T>(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        //insertar datos con procedimiento almacenado
        public int ProcedureInsertData(string procedureName, DynamicParameters parameters)
        {
            using var connection = new SqlConnection(ConnectionString);

            connection.Open();

            var result = connection.Execute(procedureName, parameters,
              commandType: CommandType.StoredProcedure);

            return result;
        }

        //eliminiar datos con procedimiento almacenado
        public void ProcedureDeleteDataWithId(string storedProcedure, DynamicParameters dynamicParameters)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            connection.Execute(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);
        }


        //actualizar datos con procediminto almacenado
        public int ProcedureUpdateData(string procedureName, DynamicParameters parameters)
        {
            using var connection = new SqlConnection(ConnectionString);

            connection.Open();

            var result = connection.Execute(procedureName, parameters,
                commandType: CommandType.StoredProcedure);

            return result;
        }

    }


}