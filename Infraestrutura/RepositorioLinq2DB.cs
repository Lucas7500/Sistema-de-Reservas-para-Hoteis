using Dominio;
using Dominio.Constantes;
using LinqToDB;
using LinqToDB.Data;

namespace Infraestrutura
{
    public class RepositorioLinq2DB : IRepositorio
    {
        private static readonly string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BDSistemaReservas"].ConnectionString;

        private static DataConnection Connection()
        {
            var conexaoLinq2Db = new DataConnection(
                new DataOptions().UseSqlServer(_connectionString));
            return conexaoLinq2Db;
        }

        public List<Reserva> ObterTodos()
        {
            try
            { 
                List<Reserva> listaReservas = new();
                using var conexaoLinq2Db = Connection();
                listaReservas = conexaoLinq2Db.GetTable<Reserva>().ToList();
                return listaReservas;
            }
            catch
            {
                throw new Exception(message: MensagemExcessao.ERRO_OBTER_TODOS_BD);
            }
        }

        public Reserva ObterPorId(int id)
        {
            try
            {
                using var conexaoLinq2Db = Connection();
                return conexaoLinq2Db.GetTable<Reserva>().First(x => x.Id == id);
            }
            catch
            {
                throw new Exception(message: MensagemExcessao.ERRO_OBTER_POR_ID_BD);
            }
        }

        public void Criar(Reserva reserva)
        {
            try
            {
                using var conexaoLinq2Db = Connection();
                reserva.Id = conexaoLinq2Db.InsertWithInt32Identity(reserva);
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
                using var conexaoLinq2Db = Connection();
                conexaoLinq2Db.Update(copiaReserva);
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
                using var conexaoLinq2Db = Connection();
                conexaoLinq2Db.Delete(ObterPorId(id));
            }
            catch
            {
                throw new Exception(message: MensagemExcessao.ERRO_REMOVER_BD);
            }
        }
    }
}