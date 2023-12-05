using Dominio.Enums;
using Dominio;
using System.Data.SqlClient;

namespace Infraestrutura
{
    public class RepositorioSqlServer : IRepositorio
    {
        private static readonly string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BDSistemaReservas"].ConnectionString;

        private static SqlConnection Connection()
        {
            SqlConnection connection = new(_connectionString);
            connection.Open();
            return connection;
        }

        private static Reserva CriarReserva(SqlDataReader leitor)
        {
            return new Reserva()
            {
                Id = (int)leitor["Id"],
                Nome = (string)leitor["Nome"],
                Cpf = (string)leitor["Cpf"],
                Telefone = (string)leitor["Telefone"],
                Idade = (int)leitor["Idade"],
                Sexo = (GeneroEnum)Enum.Parse(typeof(GeneroEnum), (string)leitor["Sexo"]),
                CheckIn = (DateTime)leitor["CheckIn"],
                CheckOut = (DateTime)leitor["CheckOut"],
                PrecoEstadia = Math.Round((decimal)leitor["PrecoEstadia"], 2),
                PagamentoEfetuado = (bool)leitor["PagamentoEfetuado"]
            };
        }

        private static void VerificaSeCpfEhUnico(Reserva reserva)
        {
            using var connection = Connection();

            SqlCommand verificaBD = new("SELECT * FROM TabelaReservas", connection);

            var leitor = verificaBD.ExecuteReader();

            while (leitor.Read())
            {
                if (((string)leitor["Cpf"]).Equals(reserva.Cpf) && (int)leitor["Id"] != reserva.Id)
                {
                    string mensagemErro = "Esse cpf já está registrado no sistema!";
                    throw new Exception(message: mensagemErro);
                }
            }
        }

        public List<Reserva> ObterTodos()
        {
            List<Reserva> listaReservas = new();

            using (var connection = Connection())
            {
                SqlCommand obterLinhasBD = new("SELECT * FROM TabelaReservas", connection);
                var leitor = obterLinhasBD.ExecuteReader();

                while (leitor.Read())
                {
                    listaReservas.Add(CriarReserva(leitor));
                }
            }

            return listaReservas;
        }

        public Reserva ObterPorId(int id)
        {
            Reserva reservaSelecionada = new();

            using (var connection = Connection())
            {
                try
                {
                    SqlCommand obterObjetoPorId = new($"SELECT * FROM TabelaReservas WHERE Id={id}", connection);
                    var leitor = obterObjetoPorId.ExecuteReader();

                    while (leitor.Read())
                    {
                        reservaSelecionada = CriarReserva(leitor);
                    }
                }
                catch
                {
                    throw new Exception(message: "Erro ao Obter Reserva Selecionada do Banco De Dados");
                }
            }

            return reservaSelecionada;
        }

        public void Criar(Reserva reserva)
        {
            VerificaSeCpfEhUnico(reserva);
            using var connection = Connection();
            
            try
            {
                string comandoCriar = @"INSERT INTO TabelaReservas
                (Nome, Cpf, Telefone, Idade, Sexo, CheckIn, CheckOut, PrecoEstadia, PagamentoEfetuado)
                VALUES (@nome, @cpf, @telefone, @idade, @sexo, @checkin, @checkout, @precoestadia, @pagamentoefetuado)";

                SqlCommand inserirReservaNaTabela = new(comandoCriar, connection);

                inserirReservaNaTabela.Parameters.AddWithValue("@nome", reserva.Nome);
                inserirReservaNaTabela.Parameters.AddWithValue("@cpf", reserva.Cpf);
                inserirReservaNaTabela.Parameters.AddWithValue("@telefone", reserva.Telefone);
                inserirReservaNaTabela.Parameters.AddWithValue("@idade", reserva.Idade);
                inserirReservaNaTabela.Parameters.AddWithValue("@sexo", reserva.Sexo);
                inserirReservaNaTabela.Parameters.AddWithValue("@checkin", reserva.CheckIn.Date.ToString("dd-MM-yyyy"));
                inserirReservaNaTabela.Parameters.AddWithValue("@checkout", reserva.CheckOut.Date.ToString("dd-MM-yyyy"));
                inserirReservaNaTabela.Parameters.AddWithValue("@precoestadia", reserva.PrecoEstadia.ToString().Replace(',', '.'));
                inserirReservaNaTabela.Parameters.AddWithValue("@pagamentoefetuado", reserva.PagamentoEfetuado);
                
                inserirReservaNaTabela.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception(message: "Erro ao Adicionar Reserva no Banco de Dados");
            }
        }
        
        public void Atualizar(Reserva copiaReserva)
        {
            VerificaSeCpfEhUnico(copiaReserva);
            using var connection = Connection();

            try
            {
                string comandoEditar = @"UPDATE TabelaReservas
                SET Nome=@nome, Cpf=@cpf, Telefone=@telefone, Idade=@idade, Sexo=@sexo, CheckIn=@checkin,
                CheckOut=@checkout, PrecoEstadia=@precoestadia, PagamentoEfetuado=@pagamentoefetuado
                WHERE id=@id";

                SqlCommand editarReservaNaTabela = new(comandoEditar, connection);

                editarReservaNaTabela.Parameters.AddWithValue("@nome", copiaReserva.Nome);
                editarReservaNaTabela.Parameters.AddWithValue("@cpf", copiaReserva.Cpf);
                editarReservaNaTabela.Parameters.AddWithValue("@telefone", copiaReserva.Telefone);
                editarReservaNaTabela.Parameters.AddWithValue("@idade", copiaReserva.Idade);
                editarReservaNaTabela.Parameters.AddWithValue("@sexo", copiaReserva.Sexo);
                editarReservaNaTabela.Parameters.AddWithValue("@checkin", copiaReserva.CheckIn.Date.ToString("dd-MM-yyyy"));
                editarReservaNaTabela.Parameters.AddWithValue("@checkout", copiaReserva.CheckOut.Date.ToString("dd-MM-yyyy"));
                editarReservaNaTabela.Parameters.AddWithValue("@precoestadia", copiaReserva.PrecoEstadia.ToString().Replace(',', '.'));
                editarReservaNaTabela.Parameters.AddWithValue("@pagamentoefetuado", copiaReserva.PagamentoEfetuado);
                editarReservaNaTabela.Parameters.AddWithValue("@id", copiaReserva.Id);
                
                editarReservaNaTabela.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception(message: "Erro ao Editar Reserva do Banco de Dados");
            }
        }

        public void Remover(int id)
        {
            using var connection = Connection();

            try
            {
                SqlCommand deletarReserva = new($"DELETE FROM TabelaReservas WHERE Id={id}", connection);
                deletarReserva.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception(message: "Erro ao Remover Reserva do Banco de Dados");
            }
        }
    }
}
