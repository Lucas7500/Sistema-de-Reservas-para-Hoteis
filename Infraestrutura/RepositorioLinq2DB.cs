using Dominio;
using Dominio.Constantes;
using LinqToDB;
using LinqToDB.Data;

namespace Infraestrutura
{
    public class RepositorioLinq2DB : IRepositorio
    {
        private const string nomeConexao = "BDSistemaReservas";
        private static readonly string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[nomeConexao].ConnectionString;

        private static DataConnection Connection()
        {
            var conexaoLinq2Db = new DataConnection(new DataOptions().UseSqlServer(_connectionString));
            return conexaoLinq2Db;
        }

        public List<Reserva> ObterTodos()
        {
            using var conexaoLinq2Db = Connection();
            return conexaoLinq2Db.GetTable<Reserva>().ToList();
        }

        public Reserva? ObterPorId(int id)
        {
            using var conexaoLinq2Db = Connection();
            return conexaoLinq2Db.GetTable<Reserva>().FirstOrDefault(x => x.Id == id);
        }

        public void Criar(Reserva reservaParaCriacao)
        {
            using var conexaoLinq2Db = Connection();
            reservaParaCriacao.Id = conexaoLinq2Db.InsertWithInt32Identity(reservaParaCriacao);
        }

        public void Atualizar(Reserva reservaParaAtualizar)
        {
            using var conexaoLinq2Db = Connection();
            conexaoLinq2Db.Update(reservaParaAtualizar);
        }

        public void Remover(int id)
        {
            using var conexaoLinq2Db = Connection();
            conexaoLinq2Db.Delete(ObterPorId(id));
        }
    }
}