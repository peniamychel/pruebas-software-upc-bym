namespace backend.entidades
{
    public class CarritoCompra : Common
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public int IdUsuario { get; set; }
    }
}