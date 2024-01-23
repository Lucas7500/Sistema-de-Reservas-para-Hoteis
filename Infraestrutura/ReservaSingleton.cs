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
            return _listaReservas ??= new();
        }

        public static int IncrementarId()
        {
            Id++;
            return Id;
        }
    }
}
