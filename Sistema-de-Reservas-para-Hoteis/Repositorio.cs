namespace Sistema_de_Reservas_para_Hoteis
{
    internal class Repositorio : IRepositorio
    {
        protected List<Reserva> listaReservas = Singleton.RetornaLista();

        public List<Reserva> ObterTodos()
        {
            return listaReservas;
        }

        public Reserva ObterPorId(int id)
        {
            Reserva reservaSelecionada = listaReservas.Find(x => x.Id == id);

            return reservaSelecionada;
        }

        public void Criar(Reserva reserva)
        {
            reserva.Id = Singleton.IncrementarId();
            listaReservas.Add(reserva);
        }
        public void Atualizar(Reserva copiaReserva)
        {
            var reservaNaLista = listaReservas.FindIndex(x => x.Id == copiaReserva.Id);
            listaReservas[reservaNaLista] = copiaReserva;
        }

        public void Remover(int id)
        {
            Reserva reserva = ObterPorId(id);
            listaReservas.Remove(reserva);
        }
    }
}
