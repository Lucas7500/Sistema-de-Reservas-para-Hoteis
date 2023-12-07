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

        private static void VerificaSeCpfEhUnico(Reserva reserva)
        {
            bool cpfNaoEhUnico = false;

            try
            {
                using var conexaoLinq2Db = Connection();

                var reservaMesmoCpf = conexaoLinq2Db.GetTable<Reserva>().FirstOrDefault(x => x.Cpf == reserva.Cpf);

                if (reservaMesmoCpf == null)
                {
                    return;
                }
                else if (reservaMesmoCpf.Id != reserva.Id)
                {
                    cpfNaoEhUnico = true;
                }
            }
            catch
            {
                string mensagemErro = "Erro ao verificar se o CPF é único!";
                throw new Exception(message: mensagemErro);
            }

            if (cpfNaoEhUnico)
            {
                string mensagemErro = "Esse CPF já está registrado no sistema!";
                throw new Exception(message: mensagemErro);
            }
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
            VerificaSeCpfEhUnico(reserva);

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

        public void Atualizar(Reserva copiaReserva)
        {
            VerificaSeCpfEhUnico(copiaReserva);

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
    }
}