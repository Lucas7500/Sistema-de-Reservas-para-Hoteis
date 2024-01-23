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
                Id = Convert.ToInt32(leitor[CamposTabela.COLUNA_ID]),
                Nome = (string)leitor[CamposTabela.COLUNA_NOME],
                Cpf = (string)leitor[CamposTabela.COLUNA_CPF],
                Telefone = (string)leitor[CamposTabela.COLUNA_TELEFONE],
                Idade = (int)leitor[CamposTabela.COLUNA_IDADE],
                Sexo = (GeneroEnum)Enum.Parse(typeof(GeneroEnum), (string)leitor[CamposTabela.COLUNA_SEXO]),
                CheckIn = (DateTime)leitor[CamposTabela.COLUNA_CHECK_IN],
                CheckOut = (DateTime)leitor[CamposTabela.COLUNA_CHECK_OUT],
                PrecoEstadia = Math.Round((decimal)leitor[CamposTabela.COLUNA_PRECO_ESTADIA], ValoresPadrao.MAX_CASAS_DECIMAIS),
                PagamentoEfetuado = (bool)leitor[CamposTabela.COLUNA_PAGAMENTO_EFETUADO]
            };
        }

        public List<Reserva> ObterTodos()
        {
            List<Reserva> listaReservas = new();

            using (var connection = Connection())
            {
                const string query = $"SELECT * FROM {CamposTabela.NOME_TABELA}";
                SqlCommand obterLinhasBD = new(query, connection);
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

            var reservaSelecionada = new Reserva();
            string query = $"SELECT * FROM {CamposTabela.NOME_TABELA} WHERE {CamposTabela.COLUNA_ID}={id}";

            SqlCommand obterObjetoPorId = new(query, connection);
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

            string query = @$"INSERT INTO {CamposTabela.NOME_TABELA} (
                    {CamposTabela.COLUNA_NOME}, 
                    {CamposTabela.COLUNA_CPF}, 
                    {CamposTabela.COLUNA_TELEFONE},
                    {CamposTabela.COLUNA_IDADE}, 
                    {CamposTabela.COLUNA_SEXO},     
                    {CamposTabela.COLUNA_CHECK_IN}, 
                    {CamposTabela.COLUNA_CHECK_OUT}, 
                    {CamposTabela.COLUNA_PRECO_ESTADIA}, 
                    {CamposTabela.COLUNA_PAGAMENTO_EFETUADO}
                )
                OUTPUT INSERTED.ID
                VALUES (
                    {CamposTabela.PARAMETRO_NOME}, 
                    {CamposTabela.PARAMETRO_CPF}, 
                    {CamposTabela.PARAMETRO_TELEFONE},
                    {CamposTabela.PARAMETRO_IDADE}, 
                    {CamposTabela.PARAMETRO_SEXO},     
                    {CamposTabela.PARAMETRO_CHECK_IN}, 
                    {CamposTabela.PARAMETRO_CHECK_OUT}, 
                    {CamposTabela.PARAMETRO_PRECO_ESTADIA}, 
                    {CamposTabela.PARAMETRO_PAGAMENTO_EFETUADO}
                )";

            SqlCommand inserirReservaNaTabela = new(query, connection);
            const string formatoData = "dd-MM-yyyy";
            const char virgula = ',';
            const char ponto = '.';

            inserirReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_NOME, reservaParaCriacao.Nome);
            inserirReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_CPF, reservaParaCriacao.Cpf);
            inserirReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_TELEFONE, reservaParaCriacao.Telefone);
            inserirReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_IDADE, reservaParaCriacao.Idade);
            inserirReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_SEXO, reservaParaCriacao.Sexo);
            inserirReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_CHECK_IN, reservaParaCriacao.CheckIn.Date.ToString(formatoData));
            inserirReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_CHECK_OUT, reservaParaCriacao.CheckOut.Date.ToString(formatoData));
            inserirReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_PRECO_ESTADIA, reservaParaCriacao.PrecoEstadia.ToString().Replace(virgula, ponto));
            inserirReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_PAGAMENTO_EFETUADO, reservaParaCriacao.PagamentoEfetuado);

            reservaParaCriacao.Id = Convert.ToInt32(inserirReservaNaTabela.ExecuteScalar());
        }

        public void Atualizar(Reserva reservaParaAtualizar)
        {
            using var connection = Connection();
            string query = $@"UPDATE {CamposTabela.NOME_TABELA}
                SET 
                    {CamposTabela.COLUNA_NOME} = {CamposTabela.PARAMETRO_NOME}, 
                    {CamposTabela.COLUNA_CPF} = {CamposTabela.PARAMETRO_CPF}, 
                    {CamposTabela.COLUNA_TELEFONE} = {CamposTabela.PARAMETRO_TELEFONE}, 
                    {CamposTabela.COLUNA_IDADE} = {CamposTabela.PARAMETRO_IDADE}, 
                    {CamposTabela.COLUNA_SEXO} = {CamposTabela.PARAMETRO_SEXO}, 
                    {CamposTabela.COLUNA_CHECK_IN} = {CamposTabela.PARAMETRO_CHECK_IN},
                    {CamposTabela.COLUNA_CHECK_OUT} = {CamposTabela.PARAMETRO_CHECK_OUT}, 
                    {CamposTabela.COLUNA_PRECO_ESTADIA} = {CamposTabela.PARAMETRO_PRECO_ESTADIA}, 
                    {CamposTabela.COLUNA_PAGAMENTO_EFETUADO} = {CamposTabela.PARAMETRO_PAGAMENTO_EFETUADO}
                WHERE 
                    {CamposTabela.COLUNA_ID} = {CamposTabela.PARAMETRO_ID}";

            SqlCommand editarReservaNaTabela = new(query, connection);

            editarReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_NOME, reservaParaAtualizar.Nome);
            editarReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_CPF, reservaParaAtualizar.Cpf);
            editarReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_TELEFONE, reservaParaAtualizar.Telefone);
            editarReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_IDADE, reservaParaAtualizar.Idade);
            editarReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_SEXO, reservaParaAtualizar.Sexo);
            editarReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_CHECK_IN, reservaParaAtualizar.CheckIn.Date.ToString("dd-MM-yyyy"));
            editarReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_CHECK_OUT, reservaParaAtualizar.CheckOut.Date.ToString("dd-MM-yyyy"));
            editarReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_PRECO_ESTADIA, reservaParaAtualizar.PrecoEstadia.ToString().Replace(',', '.'));
            editarReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_PAGAMENTO_EFETUADO, reservaParaAtualizar.PagamentoEfetuado);
            editarReservaNaTabela.Parameters.AddWithValue(CamposTabela.PARAMETRO_ID, reservaParaAtualizar.Id);

            editarReservaNaTabela.ExecuteNonQuery();
        }

        public void Remover(int id)
        {
            using var connection = Connection();
            string query = $"DELETE FROM {CamposTabela.NOME_TABELA} WHERE {CamposTabela.COLUNA_ID}={id}";
            SqlCommand deletarReserva = new(query, connection);
            deletarReserva.ExecuteNonQuery();
        }
    }
}
