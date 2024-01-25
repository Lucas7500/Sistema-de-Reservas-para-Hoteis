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
                Id = Convert.ToInt32(leitor[CamposTabelaReservas.COLUNA_ID]),
                Nome = (string)leitor[CamposTabelaReservas.COLUNA_NOME],
                Cpf = (string)leitor[CamposTabelaReservas.COLUNA_CPF],
                Telefone = (string)leitor[CamposTabelaReservas.COLUNA_TELEFONE],
                Idade = (int)leitor[CamposTabelaReservas.COLUNA_IDADE],
                Sexo = (GeneroEnum)Enum.Parse(typeof(GeneroEnum), (string)leitor[CamposTabelaReservas.COLUNA_SEXO]),
                CheckIn = (DateTime)leitor[CamposTabelaReservas.COLUNA_CHECK_IN],
                CheckOut = (DateTime)leitor[CamposTabelaReservas.COLUNA_CHECK_OUT],
                PrecoEstadia = Math.Round((decimal)leitor[CamposTabelaReservas.COLUNA_PRECO_ESTADIA], ValoresPadrao.MAX_CASAS_DECIMAIS),
                PagamentoEfetuado = (bool)leitor[CamposTabelaReservas.COLUNA_PAGAMENTO_EFETUADO]
            };
        }

        public List<Reserva> ObterTodos()
        {
            List<Reserva> listaReservas = new();

            using (var connection = Connection())
            {
                const string query = $"SELECT * FROM {CamposTabelaReservas.NOME_TABELA}";
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
            string query = $"SELECT * FROM {CamposTabelaReservas.NOME_TABELA} WHERE {CamposTabelaReservas.COLUNA_ID}={id}";

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

            string query = @$"INSERT INTO {CamposTabelaReservas.NOME_TABELA} (
                    {CamposTabelaReservas.COLUNA_NOME}, 
                    {CamposTabelaReservas.COLUNA_CPF}, 
                    {CamposTabelaReservas.COLUNA_TELEFONE},
                    {CamposTabelaReservas.COLUNA_IDADE}, 
                    {CamposTabelaReservas.COLUNA_SEXO},     
                    {CamposTabelaReservas.COLUNA_CHECK_IN}, 
                    {CamposTabelaReservas.COLUNA_CHECK_OUT}, 
                    {CamposTabelaReservas.COLUNA_PRECO_ESTADIA}, 
                    {CamposTabelaReservas.COLUNA_PAGAMENTO_EFETUADO}
                )
                OUTPUT INSERTED.ID
                VALUES (
                    {CamposTabelaReservas.PARAMETRO_NOME}, 
                    {CamposTabelaReservas.PARAMETRO_CPF}, 
                    {CamposTabelaReservas.PARAMETRO_TELEFONE},
                    {CamposTabelaReservas.PARAMETRO_IDADE}, 
                    {CamposTabelaReservas.PARAMETRO_SEXO},     
                    {CamposTabelaReservas.PARAMETRO_CHECK_IN}, 
                    {CamposTabelaReservas.PARAMETRO_CHECK_OUT}, 
                    {CamposTabelaReservas.PARAMETRO_PRECO_ESTADIA}, 
                    {CamposTabelaReservas.PARAMETRO_PAGAMENTO_EFETUADO}
                )";

            SqlCommand inserirReservaNaTabela = new(query, connection);
            const string formatoData = "dd-MM-yyyy";
            const char virgula = ',';
            const char ponto = '.';

            inserirReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_NOME, reservaParaCriacao.Nome);
            inserirReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_CPF, reservaParaCriacao.Cpf);
            inserirReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_TELEFONE, reservaParaCriacao.Telefone);
            inserirReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_IDADE, reservaParaCriacao.Idade);
            inserirReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_SEXO, reservaParaCriacao.Sexo);
            inserirReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_CHECK_IN, reservaParaCriacao.CheckIn.Date.ToString(formatoData));
            inserirReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_CHECK_OUT, reservaParaCriacao.CheckOut.Date.ToString(formatoData));
            inserirReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_PRECO_ESTADIA, reservaParaCriacao.PrecoEstadia.ToString().Replace(virgula, ponto));
            inserirReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_PAGAMENTO_EFETUADO, reservaParaCriacao.PagamentoEfetuado);

            reservaParaCriacao.Id = Convert.ToInt32(inserirReservaNaTabela.ExecuteScalar());
        }

        public void Atualizar(Reserva reservaParaAtualizar)
        {
            using var connection = Connection();
            string query = $@"UPDATE {CamposTabelaReservas.NOME_TABELA}
                SET 
                    {CamposTabelaReservas.COLUNA_NOME} = {CamposTabelaReservas.PARAMETRO_NOME}, 
                    {CamposTabelaReservas.COLUNA_CPF} = {CamposTabelaReservas.PARAMETRO_CPF}, 
                    {CamposTabelaReservas.COLUNA_TELEFONE} = {CamposTabelaReservas.PARAMETRO_TELEFONE}, 
                    {CamposTabelaReservas.COLUNA_IDADE} = {CamposTabelaReservas.PARAMETRO_IDADE}, 
                    {CamposTabelaReservas.COLUNA_SEXO} = {CamposTabelaReservas.PARAMETRO_SEXO}, 
                    {CamposTabelaReservas.COLUNA_CHECK_IN} = {CamposTabelaReservas.PARAMETRO_CHECK_IN},
                    {CamposTabelaReservas.COLUNA_CHECK_OUT} = {CamposTabelaReservas.PARAMETRO_CHECK_OUT}, 
                    {CamposTabelaReservas.COLUNA_PRECO_ESTADIA} = {CamposTabelaReservas.PARAMETRO_PRECO_ESTADIA}, 
                    {CamposTabelaReservas.COLUNA_PAGAMENTO_EFETUADO} = {CamposTabelaReservas.PARAMETRO_PAGAMENTO_EFETUADO}
                WHERE 
                    {CamposTabelaReservas.COLUNA_ID} = {CamposTabelaReservas.PARAMETRO_ID}";

            SqlCommand editarReservaNaTabela = new(query, connection);

            editarReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_NOME, reservaParaAtualizar.Nome);
            editarReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_CPF, reservaParaAtualizar.Cpf);
            editarReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_TELEFONE, reservaParaAtualizar.Telefone);
            editarReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_IDADE, reservaParaAtualizar.Idade);
            editarReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_SEXO, reservaParaAtualizar.Sexo);
            editarReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_CHECK_IN, reservaParaAtualizar.CheckIn.Date.ToString("dd-MM-yyyy"));
            editarReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_CHECK_OUT, reservaParaAtualizar.CheckOut.Date.ToString("dd-MM-yyyy"));
            editarReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_PRECO_ESTADIA, reservaParaAtualizar.PrecoEstadia.ToString().Replace(',', '.'));
            editarReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_PAGAMENTO_EFETUADO, reservaParaAtualizar.PagamentoEfetuado);
            editarReservaNaTabela.Parameters.AddWithValue(CamposTabelaReservas.PARAMETRO_ID, reservaParaAtualizar.Id);

            editarReservaNaTabela.ExecuteNonQuery();
        }

        public void Remover(int id)
        {
            using var connection = Connection();
            string query = $"DELETE FROM {CamposTabelaReservas.NOME_TABELA} WHERE {CamposTabelaReservas.COLUNA_ID}={id}";
            SqlCommand deletarReserva = new(query, connection);
            deletarReserva.ExecuteNonQuery();
        }
    }
}
