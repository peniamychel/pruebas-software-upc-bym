namespace backend.entidades
{
    public class DejaEnGuarderia : Common
    {
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public int IdNinio { get; set; }

        public DateTime? HoraEntrada { get; set; }

        public DateTime? HoraSalida { get; set; }
        
    }
}