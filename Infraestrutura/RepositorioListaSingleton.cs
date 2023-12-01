using Dominio;

namespace Infraestrutura
{
    public class RepositorioListaSingleton : IRepositorio
    {
        protected List<Reserva> _listaReservas = Singleton.RetornaLista();

        public List<Reserva> ObterTodos()
        {
            return _listaReservas;
        }

        public Reserva ObterPorId(int id)
        {
            Reserva reservaSelecionada = new();

            try
            {
                reservaSelecionada = _listaReservas.Find(x => x.Id == id);
            }
            catch
            {
                throw new Exception(message: "Erro ao Obter Reserva Selecionada da Lista Singleton");
            }

            return reservaSelecionada;
        }

        public void Criar(Reserva reserva)
        {
            try
            {
                reserva.Id = Singleton.IncrementarId();
                _listaReservas.Add(reserva);
            }
            catch
            {
                throw new Exception(message: "Erro Ao Adicionar Reserva na Lista Singleton");
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
                throw new Exception(message: "Erro ao Editar Reserva da Lista Singleton");
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
                throw new Exception(message: "Erro ao Remover Reserva da Lista Singleton");
            }
        }
    }
}
