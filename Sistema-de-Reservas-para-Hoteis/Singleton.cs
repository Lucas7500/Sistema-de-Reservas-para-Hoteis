namespace Sistema_de_Reservas_para_Hoteis
{
    public sealed class Singleton
    {
        private static List<Reserva>? _listaReservas = null;
        private static int Id { get; set; } = 0;

        public static List<Reserva> RetornaLista()
        {
            if (_listaReservas == null)
            {
                _listaReservas = new();
            }

            return _listaReservas;
        }

        public static int IncrementarId()
        {
            Id++;
            return Id;
        }
    }
}
