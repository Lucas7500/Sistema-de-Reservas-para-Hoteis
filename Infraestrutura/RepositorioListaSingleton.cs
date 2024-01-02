using Dominio;
using Dominio.Constantes;
using System.Linq;

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
            var reserva = _listaReservas.FirstOrDefault(x => x.Id == id);

            return reserva;
        }

        public void Criar(Reserva reservaParaCriacao)
        {
            reservaParaCriacao.Id = ReservaSingleton.IncrementarId();
            _listaReservas.Add(reservaParaCriacao);
        }
        public void Atualizar(Reserva reservaParaAtualizar)
        {
            var reservaNaLista = _listaReservas.FindIndex(x => x.Id == reservaParaAtualizar.Id);
            _listaReservas[reservaNaLista] = reservaParaAtualizar;
        }

        public void Remover(int id)
        {
            Reserva reserva = ObterPorId(id);
            _listaReservas.Remove(reserva);
        }
    }
}
