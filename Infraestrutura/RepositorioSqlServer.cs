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
            bool cpfNaoEhUnico = false;

            try
            {
                using var connection = Connection();

                SqlCommand verificaBD = new("SELECT * FROM TabelaReservas", connection);

                var leitor = verificaBD.ExecuteReader();

                while (leitor.Read())
                {
                    if (((string)leitor["Cpf"]).Equals(reserva.Cpf) && (int)leitor["Id"] != reserva.Id)
                    {
                        cpfNaoEhUnico = true;
                        break;
                    }
                }
            }
            catch
            {
                string mensagemErro = "Erro ao verificar se o CPF é único!";
                throw new Exception(message: mensagemErro);
            }

            if (cpfNaoEhUnico)
            {
                string mensagemErro = "Esse cpf já está registrado no sistema!";
                throw new Exception(message: mensagemErro);
            }
        }

        public List<Reserva> ObterTodos()
        {
            try
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
            catch
            {
                string mensagemErro = "Erro ao obter os elementos do banco de dados!";
                throw new Exception(message: mensagemErro);
            }

        }

        public Reserva ObterPorId(int id)
        {
            Reserva reservaSelecionada = new();

            try
            {
                using var connection = Connection();
                SqlCommand obterObjetoPorId = new($"SELECT * FROM TabelaReservas WHERE Id={id}", connection);
                var leitor = obterObjetoPorId.ExecuteReader();


                while (leitor.Read())
                {
                    reservaSelecionada = CriarReserva(leitor);
                }

                return reservaSelecionada;
            }
            catch
            {
                throw new Exception(message: "Erro ao Obter Reserva Selecionada do Banco De Dados");
            }
        }

        public void Criar(Reserva reserva)
        {
            VerificaSeCpfEhUnico(reserva);

            try
            {
                using var connection = Connection();
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

            try
            {
                using var connection = Connection();
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
            try
            {
                using var connection = Connection();
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
