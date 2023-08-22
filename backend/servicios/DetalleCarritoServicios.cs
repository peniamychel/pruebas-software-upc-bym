using System.Data;
using backend.connection;
using backend.entidades;
using Dapper;

namespace backend.servicios
{
    public static class DetalleCarritoServicios
    {
        public static IEnumerable<T> ObtenerTodo<T>()
        {
            const string sql = "select * from DETALLE_CARRITO";
            return BDManager.GetInstance.GetData<T>(sql);//Dapper
        }

        public static T ObtenerById<T>(int id)
        {
            const string sql = "select * from DETALLE_CARRITO where ID = @Id and estado_registro = 1";

            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int64);

            var result = BDManager.GetInstance.GetDataWithParameters<T>(sql, parameters);

            return result.FirstOrDefault();
        }

        public static int InsertDetalleCarrito(DetalleCarrito detalleCarrito)
        {
            const string sql = "INSERT INTO [dbo].[DETALLE_CARRITO]([CANTIDAD], [ID_PRODUCTO], [ID_CARRITO_COMPRA]) VALUES (@cantidad, @id_producto, @id_carrito_compra)";

            var parameters = new DynamicParameters();
            parameters.Add("cantidad", detalleCarrito.Cantidad, DbType.Int64);
            parameters.Add("id_producto", detalleCarrito.IdProducto, DbType.Int64);
            parameters.Add("id_carrito_compra", detalleCarrito.IdCarritoCompra, DbType.Int64);

            var result = BDManager.GetInstance.SetData(sql, parameters);
            return result;
        }

        public static int UpdateDetalleCarrito(DetalleCarrito detalleCarrito)
        {
            const string sql = "UPDATE [dbo].[DETALLE_CARRITO] SET [CANTIDAD] = @cantidad, [ID_PRODUCTO] = @id_producto, [ID_CARRITO_COMPRA] = @id_carrito_compra WHERE [ID] = @id";

            var parameters = new DynamicParameters();
            parameters.Add("cantidad", detalleCarrito.Cantidad, DbType.Int64);
            parameters.Add("id_producto", detalleCarrito.IdProducto, DbType.Int64);
            parameters.Add("id_carrito_compra", detalleCarrito.IdCarritoCompra, DbType.Int64);
            parameters.Add("id", detalleCarrito.Id, DbType.Int64);

            var result = BDManager.GetInstance.SetData(sql, parameters);
            return result;
        }

//************METODOS CON PROCEDIMIENTO ALMACENADO*********

        //Actualizar datos con procedimiento almacenado
        public static int PrucedureUpdateDetalleCarrito(DetalleCarrito detalleCarrito)
        {
            const string sql = "ModificarDetalleCarrito";

            var parameters = new DynamicParameters();
            parameters.Add("@id_carrito_compra", detalleCarrito.IdCarritoCompra, DbType.Int64);
            parameters.Add("@cantidad", detalleCarrito.Cantidad, DbType.Int64);
            parameters.Add("@id_producto", detalleCarrito.IdProducto, DbType.Int64);
            parameters.Add("@id", detalleCarrito.Id, DbType.Int64);

            var result = BDManager.GetInstance.ProcedureUpdateData(sql, parameters);
            return result;
        }

        //Eliminiar segun id con procedimiento almacenado
        public static void EliminarPorId(int id)
        {   const string storedProcedure = "EliminarDetalleCarritoPorId";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32);

            BDManager.GetInstance.ProcedureDeleteDataWithId(storedProcedure, parameters);
        }
    }
}
