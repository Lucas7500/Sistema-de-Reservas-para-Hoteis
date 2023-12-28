using Dominio;
using Dominio.Constantes;

namespace Infraestrutura
{
    public sealed class ReservaSingleton
    {
        private static List<Reserva>? _listaReservas = null;
        private static int Id { get; set; } = ValoresPadrao.ID_ZERO;

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
