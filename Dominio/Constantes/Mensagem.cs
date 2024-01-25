namespace Dominio.Constantes
{
    public class Mensagem
    {
        public const string SUCESSO_CRIACAO = "Reserva foi criada com Sucesso!";
        public const string SUCESSO_EDICAO = "Reserva foi editada com Sucesso!";
        public const string NOME_NAO_PREENCHIDO = "- Informe o Nome do cliente!";
        public const string NOME_CURTO = "- O nome do cliente não pode conter menos que 3 caracteres!";
        public const string NOME_LONGO = "- O nome do cliente não pode superar 50 caracteres!";
        public const string NOME_FORMATO_INCORRETO = "- O nome do cliente está em um formato incorreto!";
        public const string CPF_NAO_PREENCHIDO = "- Informe o CPF do cliente!";
        public const string CPF_INVALIDO = "- O CPF digitado é inválido!";
        public const string CPF_JA_REGISTRADO = "- O CPF digitado já está registrado no sistema!";
        public const string TELEFONE_NAO_PREENCHIDO = "- Informe o Telefone do cliente!";
        public const string TELEFONE_INVALIDO = "- O Telefone digitado é inválido!";
        public const string IDADE_NAO_PREENCHIDA = "- Informe a Idade do cliente!";
        public const string MENOR_DE_IDADE = "- A Idade é inválida. Cliente deve ser maior de idade!";
        public const string IDADE_INVALIDA = "- O cliente não pode ter mais de 200 anos!";
        public const string SEXO_NULO = "- O cliente deve possuir um sexo!";
        public const string SEXO_INVALIDO = "- O sexo do cliente é inválido!";
        public const string CHECKIN_NULO = "- A data de Check-in não pode ser nula!";
        public const string CHECKOUT_NULO = "- A data de Check-out não pode ser nula!";
        public const string CHECKOUT_ANTES_CHECK_IN = "- O Check-out não pode ser realizado antes do Check-in!";
        public const string PRECO_DA_ESTADIA_NAO_PREENCHIDO = "- Informe o Preço da Estadia!";
        public const string PRECO_DA_ESTADIA_MENOR_IGUAL_A_ZERO = "- O preço da estadia não pode ser negativo ou zero!";
        public const string PRECO_DA_ESTADIA_ACIMA_DO_VALOR_MAXIMO = "- O preço da estadia está acima do máximo permitido!";
        public const string PAGAMENTO_EFETUADO_NULO = "- Informe se o preço da estadia foi pago!";
        public const string TITULO_ERRO_INESPERADO = "Ocorreu um Erro Inesperado";
        public const string MENSAGEM_ERRO_LISTA_VAZIA_EDICAO = "Seu programa não possui nenhuma reserva para editar!";
        public const string MENSAGEM_ERRO_LISTA_VAZIA_REMOCAO = "Seu programa não possui nenhuma reserva para deletar!";
        public const string MENSAGEM_ERRO_NENHUMA_LINHA_SELECIONADA_EDICAO = "Selecione uma linha para editar!";
        public const string MENSAGEM_ERRO_NENHUMA_LINHA_SELECIONADA_REMOCAO = "Selecione uma linha para deletar!";
    }
}
