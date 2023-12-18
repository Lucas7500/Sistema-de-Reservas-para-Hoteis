using Dominio;
using Dominio.Constantes;

namespace Infraestrutura
{
    public class RepositorioListaSingleton : IRepositorio
    {
        protected List<Reserva> _listaReservas = ReservaSingleton.RetornaLista();

        public List<Reserva> ObterTodos()
        {
            try
            {
                return _listaReservas;
            }
            catch
            {
                throw new Exception(message: MensagemExcessao.ERRO_OBTER_TODOS_BD);
            }
        }

        public Reserva ObterPorId(int id)
        {
            Reserva reservaSelecionada = new();

            try
            {
                reservaSelecionada = _listaReservas.First(x => x.Id == id);
            }
            catch
            {
                throw new Exception(message: MensagemExcessao.ERRO_OBTER_POR_ID_BD);
            }

            return reservaSelecionada;
        }

        public void Criar(Reserva reserva)
        {
            try
            {
                reserva.Id = ReservaSingleton.IncrementarId();
                _listaReservas.Add(reserva);
            }
            catch
            {
                throw new Exception(message: MensagemExcessao.ERRO_CRIAR_BD);
            }
        }
        public void Atualizar(Reserva copiaReserva)
        {
            try
            {
                var reservaNaLista = _listaReservas.FindIndex(x => x.Id == copiaReserva.Id);
                _listaReservas[reservaNaLista] = copiaReserva;
            }
            catch
            {
                throw new Exception(message: MensagemExcessao.ERRO_ATUALIZAR_BD);
            }
        }

        public void Remover(int id)
        {
            try
            {
                Reserva reserva = ObterPorId(id);
                _listaReservas.Remove(reserva);
            }
            catch
            {
                throw new Exception(message: MensagemExcessao.ERRO_REMOVER_BD);
            }
        }
    }
}
