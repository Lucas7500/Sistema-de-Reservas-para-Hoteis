using Sistema_de_Reservas_para_Hoteis.Enums;
using System.Data.SqlClient;

namespace Sistema_de_Reservas_para_Hoteis
{
    internal class RepositorioBancoDeDados : IRepositorio
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BDSistemaReservas"].ConnectionString;

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

            using SqlConnection connection = new(connectionString);
            {
                connection.Open();
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

            using SqlConnection connection = new(connectionString);
            {
                try
                {
                    connection.Open();
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
            using SqlConnection connection = new(connectionString);
            {
                try
                {
                    connection.Open();
                    SqlCommand inserirReservaNaTabela = new($@"
                INSERT INTO 
                    TabelaReservas 
                    (Nome, Cpf, Telefone, Idade, Sexo, CheckIn, CheckOut, PrecoEstadia, PagamentoEfetuado) 
                VALUES 
                (
                    '{reserva.Nome}', 
                    '{reserva.Cpf}', 
                    '{reserva.Telefone}', 
                    '{reserva.Idade}', 
                    '{reserva.Sexo}', 
                    '{reserva.CheckIn.Date:dd-MM-yyy}', 
                    '{reserva.CheckOut.Date:dd-MM-yyy}', 
                    '{reserva.PrecoEstadia.ToString().Replace(',', '.')}', 
                    '{reserva.PagamentoEfetuado}'
                )", connection);

                    inserirReservaNaTabela.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception(message: "Erro ao Adicionar Reserva no Banco de Dados");
                }
            }
        }
        public void Atualizar(Reserva copiaReserva)
        {
            using SqlConnection connection = new(connectionString);
            {
                try
                {
                    connection.Open();
                    SqlCommand editarReservaNaTabela = new($@"
                UPDATE 
                    TabelaReservas 
                SET 
                    Nome='{copiaReserva.Nome}', 
                    Cpf='{copiaReserva.Cpf}', 
                    Telefone='{copiaReserva.Telefone}', 
                    Idade='{copiaReserva.Idade}', 
                    Sexo='{copiaReserva.Sexo}', 
                    CheckIn='{copiaReserva.CheckIn.Date:dd-MM-yyyy}', 
                    CheckOut='{copiaReserva.CheckOut.Date:dd-MM-yyyy}', 
                    PrecoEstadia='{copiaReserva.PrecoEstadia.ToString().Replace(',', '.')}', 
                    PagamentoEfetuado='{copiaReserva.PagamentoEfetuado}' 
                    WHERE Id={copiaReserva.Id}
                ", connection);

                    editarReservaNaTabela.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception(message: "Erro ao Editar Reserva do Banco de Dados");
                }
            }
        }

        public void Remover(int id)
        {
            using SqlConnection connection = new(connectionString);
            {
                try
                {
                    connection.Open();
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
}
