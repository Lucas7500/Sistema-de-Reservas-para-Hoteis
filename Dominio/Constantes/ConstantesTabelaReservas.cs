namespace Dominio.Constantes
{
    public static class ConstantesTabelaReservas
    {
        private const string nomeConexao = "BDSistemaReservas";
        public static string STRING_CONEXAO_BD = System.Configuration.ConfigurationManager.ConnectionStrings[nomeConexao].ConnectionString;

        public const string NOME_TABELA = "TabelaReservas";

        public const string COLUNA_ID = "Id";
        public const string COLUNA_NOME = "Nome";
        public const string COLUNA_CPF = "Cpf";
        public const string COLUNA_TELEFONE = "Telefone";
        public const string COLUNA_IDADE = "Idade";
        public const string COLUNA_SEXO = "Sexo";
        public const string COLUNA_CHECK_IN = "CheckIn";
        public const string COLUNA_CHECK_OUT = "CheckOut";
        public const string COLUNA_PRECO_ESTADIA = "PrecoEstadia";
        public const string COLUNA_PAGAMENTO_EFETUADO = "PagamentoEfetuado";

        public const string PARAMETRO_ID = "@id";
        public const string PARAMETRO_NOME = "@nome";
        public const string PARAMETRO_CPF = "@cpf";
        public const string PARAMETRO_TELEFONE = "@telefone";
        public const string PARAMETRO_IDADE = "@idade";
        public const string PARAMETRO_SEXO = "@sexo";
        public const string PARAMETRO_CHECK_IN = "@checkin";
        public const string PARAMETRO_CHECK_OUT = "@checkout";
        public const string PARAMETRO_PRECO_ESTADIA = "@precoestadia";
        public const string PARAMETRO_PAGAMENTO_EFETUADO = "@pagamentoefetuado";
    }
}
