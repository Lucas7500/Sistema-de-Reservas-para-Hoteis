using System.Data;
using System.Data.SqlClient;

namespace Sistema_de_Reservas_para_Hoteis
{
    internal class RepositorioBancoDeDados : IRepositorio
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BDSistemaReservas"].ConnectionString;
        readonly SqlConnection connection = new(connectionString);

        public List<Reserva> ObterTodos()
        {
            
        }

        public Reserva ObterPorId(int id)
        {
            
        }

        public void Criar(Reserva reserva)
        {
            
        }
        public void Atualizar(Reserva copiaReserva)
        {
            
        }

        public void Remover(int id)
        {
            
        }
    }
}
