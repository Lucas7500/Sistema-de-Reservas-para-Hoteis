using Dominio;
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
                string mensagemErro = "Erro ao obter os elementos do banco de dados!";
                throw new Exception(message: mensagemErro);
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
                string mensagemErro = "Erro ao obter o elemento por id!";
                throw new Exception(message: mensagemErro);
            }
        }

        public void Criar(Reserva reserva)
        {
            try
            {
                using var conexaoLinq2Db = Connection();
                conexaoLinq2Db.Insert(reserva);
            }
            catch
            {
                throw new Exception(message: "Erro ao Adicionar Reserva no Banco de Dados");
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
                throw new Exception(message: "Erro ao Editar Reserva do Banco de Dados");
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
                throw new Exception(message: "Erro ao Remover Reserva do Banco de Dados");
            }
        }
    }
}