using backend.connection;
using backend.entidades;
using backend.servicios;

namespace backend_unit_test
{
    public class UnitTestUsuario
    {


        public UnitTestUsuario()
        {
            BDManager.GetInstance.ConnectionString = "workstation id=DB-mychel.mssql.somee.com;packet size=4096;user id=Mayka_SQLLogin_1;pwd=rgzwwuaxh8;data source=DB-mychel.mssql.somee.com;persist security info=False;initial catalog=DB-mychel";
        }
        [Fact]
        public void Usuario_Get_Verificar_NotNull()
        {
            var result = UsuariosServicios.ObtenerTodo2<Usuarios>();
            Assert.NotNull(result);
        }

        [Fact]
        public void Usuario_GetById_VerificarItem()
        {
            var result = UsuariosServicios.ObtenerById<Usuarios>(3);
            Assert.Equal(3, result.Id);
        }

        [Fact]
        public void Usuarios_Insertar()
        {
            Usuarios usuarioTemp = new Usuarios()
            {
                NombreCompleto = "Nombre Test",
                UserName = "UserName Test",
                Password = "password Test"
            };

            var result = UsuariosServicios.InsertUsuario(usuarioTemp);
            Assert.Equal(1, result);
        }
    }
}