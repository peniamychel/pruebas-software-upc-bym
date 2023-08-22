using System.Data;
using backend.connection;
using backend.entidades;
using Dapper;

namespace backend.servicios
{
    public static class CarritoCompraServicios
    {
        //Obtener todo
        public static IEnumerable<T> ObtenerTodo<T>()
        {
            const string sql = "select * from CARRITO_COMPRA";
            return BDManager.GetInstance.GetData<T>(sql);//Dapper
        }

        //Obtener segun id
        public static T ObtenerById<T>(int id)
        {
            const string sql = "select * from CARRITO_COMPRA where ID = @Id and estado_registro = 1";

            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int64);

            var result = BDManager.GetInstance.GetDataWithParameters<T>(sql, parameters);

            return result.FirstOrDefault();
        }

        public static int InsertCarritoCompra(CarritoCompra carritoCompra)
        {
            const string sql = "INSERT INTO [dbo].[CARRITO_COMPRA]([FECHA], [ID_USUARIO]) VALUES (@fecha, @id_usuario) ";

            var parameters = new DynamicParameters();
            parameters.Add("fecha", carritoCompra.Fecha, DbType.Date);
            parameters.Add("id_usuario", carritoCompra.IdUsuario, DbType.Int64);

            var result = BDManager.GetInstance.SetData(sql, parameters);
            return result;
        }

        //************METODOS CON PROCEDIMIENTO ALMACENADO*********

        //Actualizar datos con procedimiento almacenado
        public static int PrucedureUpdateCarritoCompra(CarritoCompra carritoCompra)
        {
            const string sql = "ActualizarCarritoCompra";

            var parameters = new DynamicParameters();
            parameters.Add("@fecha", carritoCompra.Fecha, DbType.Date);
            parameters.Add("@id_usuario", carritoCompra.IdUsuario, DbType.Int64);
            parameters.Add("@id", carritoCompra.Id, DbType.Int64);

            var result = BDManager.GetInstance.ProcedureUpdateData(sql, parameters);
            return result;
        }

        //Eliminiar segun id con procedimiento almacenado
        public static void EliminarPorIdCarritoCompra(int id)
        {   const string storedProcedure = "EliminarCarritoCompra";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32);

            BDManager.GetInstance.ProcedureDeleteDataWithId(storedProcedure, parameters);
        }

    }
}
