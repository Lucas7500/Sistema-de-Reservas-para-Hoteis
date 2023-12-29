using Dominio;
using Dominio.Constantes;

namespace Infraestrutura
{
    public class RepositorioListaSingleton : IRepositorio
    {
        protected List<Reserva> _listaReservas = ReservaSingleton.RetornaLista();

        public List<Reserva> ObterTodos()
        {
            return _listaReservas;
        }

        public Reserva ObterPorId(int id)
        {
            Reserva reservaSelecionada = new();
            reservaSelecionada = _listaReservas.First(x => x.Id == id);

            return reservaSelecionada;
        }

        public void Criar(Reserva reserva)
        {
            reserva.Id = ReservaSingleton.IncrementarId();
            _listaReservas.Add(reserva);
        }
        public void Atualizar(Reserva copiaReserva)
        {
            var reservaNaLista = _listaReservas.FindIndex(x => x.Id == copiaReserva.Id);
            _listaReservas[reservaNaLista] = copiaReserva;
        }

        public void Remover(int id)
        {
            Reserva reserva = ObterPorId(id);
            _listaReservas.Remove(reserva);
        }
    }
}
