using Dominio.Enums;
using Dominio;
using System.Data.SqlClient;
using Dominio.Constantes;

namespace Infraestrutura
{
    public class RepositorioSqlServer : IRepositorio
    {
        private static SqlConnection Connection()
        {
            SqlConnection connection = new(ConstantesTabelaReservas.STRING_CONEXAO_BD);
            connection.Open();
            return connection;
        }

        private static Reserva CriarReserva(SqlDataReader leitor)
        {
            return new Reserva()
            {
                Id = Convert.ToInt32(leitor[ConstantesTabelaReservas.COLUNA_ID]),
                Nome = (string)leitor[ConstantesTabelaReservas.COLUNA_NOME],
                Cpf = (string)leitor[ConstantesTabelaReservas.COLUNA_CPF],
                Telefone = (string)leitor[ConstantesTabelaReservas.COLUNA_TELEFONE],
                Idade = (int)leitor[ConstantesTabelaReservas.COLUNA_IDADE],
                Sexo = (GeneroEnum)Enum.Parse(typeof(GeneroEnum), (string)leitor[ConstantesTabelaReservas.COLUNA_SEXO]),
                CheckIn = (DateTime)leitor[ConstantesTabelaReservas.COLUNA_CHECK_IN],
                CheckOut = (DateTime)leitor[ConstantesTabelaReservas.COLUNA_CHECK_OUT],
                PrecoEstadia = Math.Round((decimal)leitor[ConstantesTabelaReservas.COLUNA_PRECO_ESTADIA], ValoresPadrao.MAX_CASAS_DECIMAIS),
                PagamentoEfetuado = (bool)leitor[ConstantesTabelaReservas.COLUNA_PAGAMENTO_EFETUADO]
            };
        }

        public List<Reserva> ObterTodos()
        {
            List<Reserva> listaReservas = new();

            using (var connection = Connection())
            {
                const string query = $"SELECT * FROM {ConstantesTabelaReservas.NOME_TABELA}";
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
            string query = $"SELECT * FROM {ConstantesTabelaReservas.NOME_TABELA} WHERE {ConstantesTabelaReservas.COLUNA_ID} = {id}";

            SqlCommand obterObjetoPorId = new(query, connection);
            var leitor = obterObjetoPorId.ExecuteReader();

            while (leitor.Read())
            {
                reservaSelecionada = CriarReserva(leitor);
            }

            return reservaSelecionada;
        }

        public Reserva? ObterPorCpf(string cpf)
        {
            using var connection = Connection();

            Reserva? reservaMesmoCpf = null;
            string query = $"SELECT * FROM {ConstantesTabelaReservas.NOME_TABELA} WHERE {ConstantesTabelaReservas.COLUNA_CPF} = '{cpf}'";

            SqlCommand obterObjetoPorCpf = new(query, connection);
            var leitor = obterObjetoPorCpf.ExecuteReader();

            while (leitor.Read())
            {
                reservaMesmoCpf = CriarReserva(leitor);
            }

            return reservaMesmoCpf;
        }

        public void Criar(Reserva reservaParaCriacao)
        {
            using var connection = Connection();

            string query = @$"INSERT INTO {ConstantesTabelaReservas.NOME_TABELA} (
                    {ConstantesTabelaReservas.COLUNA_NOME}, 
                    {ConstantesTabelaReservas.COLUNA_CPF}, 
                    {ConstantesTabelaReservas.COLUNA_TELEFONE},
                    {ConstantesTabelaReservas.COLUNA_IDADE}, 
                    {ConstantesTabelaReservas.COLUNA_SEXO},     
                    {ConstantesTabelaReservas.COLUNA_CHECK_IN}, 
                    {ConstantesTabelaReservas.COLUNA_CHECK_OUT}, 
                    {ConstantesTabelaReservas.COLUNA_PRECO_ESTADIA}, 
                    {ConstantesTabelaReservas.COLUNA_PAGAMENTO_EFETUADO}
                )
                OUTPUT INSERTED.ID
                VALUES (
                    {ConstantesTabelaReservas.PARAMETRO_NOME}, 
                    {ConstantesTabelaReservas.PARAMETRO_CPF}, 
                    {ConstantesTabelaReservas.PARAMETRO_TELEFONE},
                    {ConstantesTabelaReservas.PARAMETRO_IDADE}, 
                    {ConstantesTabelaReservas.PARAMETRO_SEXO},     
                    {ConstantesTabelaReservas.PARAMETRO_CHECK_IN}, 
                    {ConstantesTabelaReservas.PARAMETRO_CHECK_OUT}, 
                    {ConstantesTabelaReservas.PARAMETRO_PRECO_ESTADIA}, 
                    {ConstantesTabelaReservas.PARAMETRO_PAGAMENTO_EFETUADO}
                )";

            SqlCommand inserirReservaNaTabela = new(query, connection);
            const string formatoData = "dd-MM-yyyy";
            const char virgula = ',';
            const char ponto = '.';

            inserirReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_NOME, reservaParaCriacao.Nome);
            inserirReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_CPF, reservaParaCriacao.Cpf);
            inserirReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_TELEFONE, reservaParaCriacao.Telefone);
            inserirReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_IDADE, reservaParaCriacao.Idade);
            inserirReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_SEXO, reservaParaCriacao.Sexo);
            inserirReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_CHECK_IN, reservaParaCriacao.CheckIn.Date.ToString(formatoData));
            inserirReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_CHECK_OUT, reservaParaCriacao.CheckOut.Date.ToString(formatoData));
            inserirReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_PRECO_ESTADIA, reservaParaCriacao.PrecoEstadia.ToString().Replace(virgula, ponto));
            inserirReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_PAGAMENTO_EFETUADO, reservaParaCriacao.PagamentoEfetuado);

            reservaParaCriacao.Id = Convert.ToInt32(inserirReservaNaTabela.ExecuteScalar());
        }

        public void Atualizar(Reserva reservaParaAtualizar)
        {
            using var connection = Connection();
            string query = $@"UPDATE {ConstantesTabelaReservas.NOME_TABELA}
                SET 
                    {ConstantesTabelaReservas.COLUNA_NOME} = {ConstantesTabelaReservas.PARAMETRO_NOME}, 
                    {ConstantesTabelaReservas.COLUNA_CPF} = {ConstantesTabelaReservas.PARAMETRO_CPF}, 
                    {ConstantesTabelaReservas.COLUNA_TELEFONE} = {ConstantesTabelaReservas.PARAMETRO_TELEFONE}, 
                    {ConstantesTabelaReservas.COLUNA_IDADE} = {ConstantesTabelaReservas.PARAMETRO_IDADE}, 
                    {ConstantesTabelaReservas.COLUNA_SEXO} = {ConstantesTabelaReservas.PARAMETRO_SEXO}, 
                    {ConstantesTabelaReservas.COLUNA_CHECK_IN} = {ConstantesTabelaReservas.PARAMETRO_CHECK_IN},
                    {ConstantesTabelaReservas.COLUNA_CHECK_OUT} = {ConstantesTabelaReservas.PARAMETRO_CHECK_OUT}, 
                    {ConstantesTabelaReservas.COLUNA_PRECO_ESTADIA} = {ConstantesTabelaReservas.PARAMETRO_PRECO_ESTADIA}, 
                    {ConstantesTabelaReservas.COLUNA_PAGAMENTO_EFETUADO} = {ConstantesTabelaReservas.PARAMETRO_PAGAMENTO_EFETUADO}
                WHERE 
                    {ConstantesTabelaReservas.COLUNA_ID} = {ConstantesTabelaReservas.PARAMETRO_ID}";

            SqlCommand editarReservaNaTabela = new(query, connection);

            editarReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_NOME, reservaParaAtualizar.Nome);
            editarReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_CPF, reservaParaAtualizar.Cpf);
            editarReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_TELEFONE, reservaParaAtualizar.Telefone);
            editarReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_IDADE, reservaParaAtualizar.Idade);
            editarReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_SEXO, reservaParaAtualizar.Sexo);
            editarReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_CHECK_IN, reservaParaAtualizar.CheckIn.Date.ToString("dd-MM-yyyy"));
            editarReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_CHECK_OUT, reservaParaAtualizar.CheckOut.Date.ToString("dd-MM-yyyy"));
            editarReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_PRECO_ESTADIA, reservaParaAtualizar.PrecoEstadia.ToString().Replace(',', '.'));
            editarReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_PAGAMENTO_EFETUADO, reservaParaAtualizar.PagamentoEfetuado);
            editarReservaNaTabela.Parameters.AddWithValue(ConstantesTabelaReservas.PARAMETRO_ID, reservaParaAtualizar.Id);

            editarReservaNaTabela.ExecuteNonQuery();
        }

        public void Remover(int id)
        {
            using var connection = Connection();
            string query = $"DELETE FROM {ConstantesTabelaReservas.NOME_TABELA} WHERE {ConstantesTabelaReservas.COLUNA_ID}={id}";
            SqlCommand deletarReserva = new(query, connection);
            deletarReserva.ExecuteNonQuery();
        }
    }
}
