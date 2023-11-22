namespace Sistema_de_Reservas_para_Hoteis
{
    internal interface IRepositorio
    {
        public List<Reserva> ObterTodos();
        public Reserva ObterPorId(int id);
        public void Criar(Reserva reserva);
        public void Remover(int id);
        public void Atualizar(Reserva copiaReserva);
    }
}
