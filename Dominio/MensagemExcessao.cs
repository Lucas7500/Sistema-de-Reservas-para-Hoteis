namespace Dominio
{
    public class MensagemExcessao
    {
        public const string NOME_NULO = "* Informe o Nome do cliente!";
        public const string NOME_PEQUENO = "* O Nome não pode conter menos que 3 caracteres!";
        public const string NOME_CONTEM_NUMEROS_OU_CARACTERES_INVALIDOS = "* O Nome não pode conter números ou caracteres inválidos!";
        public const string CPF_NAO_PREENCHIDO = "* Informe o CPF do cliente!";
        public const string CPF_INVALIDO = "* O CPF digitado é inválido!";
        public const string TELEFONE_NAO_PREENCHIDO = "* Informe o Telefone do cliente!";
        public const string TELEFONE_INVALIDO = "* O Telefone digitado é inválido!";
        public const string IDADE_NAO_PREENCHIDA = "* Informe a Idade do cliente!";
        public const string MENOR_DE_IDADE = "* A Idade é inválida. Cliente deve ser maior de idade!";
        public const string CHECKOUT_EM_DATAS_PASSADAS = "* O Check-out não pode ser realizado antes do Check-in!";
        public const string PRECO_DA_ESTADIA_NAO_PREENCHIDO = "* Informe o Preço da Estadia!";
        public const string TITULO_ERRO_INESPERADO = "Ocorreu um Erro Inesperado";

        public static string MensagemErroListaVazia(string acao)
        {
            return $"Seu programa não possui nenhuma reserva para {acao}.";
        }

        public static string MensagemErroNenhumaLinhaSelecionada(string acao)
        {
            return $"Selecione uma linha para {acao}!";
        }
    }
}
