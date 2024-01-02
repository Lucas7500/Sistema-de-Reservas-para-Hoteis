using Dominio.Enums;
using Dominio;
using System.Data.SqlClient;
using Dominio.Constantes;

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
            using var connection = Connection();

            Reserva reservaSelecionada = new();
            SqlCommand obterObjetoPorId = new($"SELECT * FROM TabelaReservas WHERE Id={id}", connection);
            var leitor = obterObjetoPorId.ExecuteReader();


            while (leitor.Read())
            {
                reservaSelecionada = CriarReserva(leitor);
            }

            return reservaSelecionada;
        }

        public void Criar(Reserva reservaParaCriacao)
        {
            using var connection = Connection();
            string comandoCriar = @"INSERT INTO TabelaReservas
                (Nome, Cpf, Telefone, Idade, Sexo, CheckIn, CheckOut, PrecoEstadia, PagamentoEfetuado)
                OUTPUT INSERTED.ID
                VALUES (@nome, @cpf, @telefone, @idade, @sexo, @checkin, @checkout, @precoestadia, @pagamentoefetuado)";

            SqlCommand inserirReservaNaTabela = new(comandoCriar, connection);

            inserirReservaNaTabela.Parameters.AddWithValue("@nome", reservaParaCriacao.Nome);
            inserirReservaNaTabela.Parameters.AddWithValue("@cpf", reservaParaCriacao.Cpf);
            inserirReservaNaTabela.Parameters.AddWithValue("@telefone", reservaParaCriacao.Telefone);
            inserirReservaNaTabela.Parameters.AddWithValue("@idade", reservaParaCriacao.Idade);
            inserirReservaNaTabela.Parameters.AddWithValue("@sexo", reservaParaCriacao.Sexo);
            inserirReservaNaTabela.Parameters.AddWithValue("@checkin", reservaParaCriacao.CheckIn.Date.ToString("dd-MM-yyyy"));
            inserirReservaNaTabela.Parameters.AddWithValue("@checkout", reservaParaCriacao.CheckOut.Date.ToString("dd-MM-yyyy"));
            inserirReservaNaTabela.Parameters.AddWithValue("@precoestadia", reservaParaCriacao.PrecoEstadia.ToString().Replace(',', '.'));
            inserirReservaNaTabela.Parameters.AddWithValue("@pagamentoefetuado", reservaParaCriacao.PagamentoEfetuado);

            reservaParaCriacao.Id = Convert.ToInt32(inserirReservaNaTabela.ExecuteScalar());
        }

        public void Atualizar(Reserva reservaParaAtualizar)
        {
            using var connection = Connection();
            string comandoEditar = @"UPDATE TabelaReservas
                SET Nome=@nome, Cpf=@cpf, Telefone=@telefone, Idade=@idade, Sexo=@sexo, CheckIn=@checkin,
                CheckOut=@checkout, PrecoEstadia=@precoestadia, PagamentoEfetuado=@pagamentoefetuado
                WHERE id=@id";

            SqlCommand editarReservaNaTabela = new(comandoEditar, connection);

            editarReservaNaTabela.Parameters.AddWithValue("@nome", reservaParaAtualizar.Nome);
            editarReservaNaTabela.Parameters.AddWithValue("@cpf", reservaParaAtualizar.Cpf);
            editarReservaNaTabela.Parameters.AddWithValue("@telefone", reservaParaAtualizar.Telefone);
            editarReservaNaTabela.Parameters.AddWithValue("@idade", reservaParaAtualizar.Idade);
            editarReservaNaTabela.Parameters.AddWithValue("@sexo", reservaParaAtualizar.Sexo);
            editarReservaNaTabela.Parameters.AddWithValue("@checkin", reservaParaAtualizar.CheckIn.Date.ToString("dd-MM-yyyy"));
            editarReservaNaTabela.Parameters.AddWithValue("@checkout", reservaParaAtualizar.CheckOut.Date.ToString("dd-MM-yyyy"));
            editarReservaNaTabela.Parameters.AddWithValue("@precoestadia", reservaParaAtualizar.PrecoEstadia.ToString().Replace(',', '.'));
            editarReservaNaTabela.Parameters.AddWithValue("@pagamentoefetuado", reservaParaAtualizar.PagamentoEfetuado);
            editarReservaNaTabela.Parameters.AddWithValue("@id", reservaParaAtualizar.Id);

            editarReservaNaTabela.ExecuteNonQuery();
        }

        public void Remover(int id)
        {
            using var connection = Connection();
            SqlCommand deletarReserva = new($"DELETE FROM TabelaReservas WHERE Id={id}", connection);
            deletarReserva.ExecuteNonQuery();
        }
    }
}
