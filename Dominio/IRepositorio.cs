using Dominio;

namespace Infraestrutura
{
    public interface IRepositorio
    {
        public List<Reserva> ObterTodos();
        public Reserva ObterPorId(int id);
        public Reserva? ObterPorCpf(string cpf);
        public void Criar(Reserva reserva);
        public void Remover(int id);
        public void Atualizar(Reserva copiaReserva);
    }
}
