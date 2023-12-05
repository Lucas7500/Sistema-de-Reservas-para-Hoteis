using Dominio;

namespace Infraestrutura
{
    public sealed class ReservaSingleton
    {
        private static List<Reserva>? _listaReservas = null;
        private static int _Id { get; set; } = 0;

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
            _Id++;
            return _Id;
        }
    }
}
