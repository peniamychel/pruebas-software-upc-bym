namespace backend.entidades
{
    public class Producto : Common
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int IdCategoria { get; set; }
    }
}