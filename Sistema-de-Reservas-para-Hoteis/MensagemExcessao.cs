namespace Sistema_de_Reservas_para_Hoteis
{
    public class MensagemExcessao
    {
        public const string NomeNulo = "* Informe o Nome do cliente.";
        public const string NomePequeno = "* O Nome não pode conter menos que 3 caracteres.";
        public const string NomeContemNumero = "* O Nome não pode conter números";
        public const string CpfNaoPreenchido = "* Informe o CPF do cliente.";
        public const string CpfInvalido = "* O CPF digitado é inválido.";
        public const string TelefoneNaoPreenchido = "* Informe o Telefone do cliente.";
        public const string TelefoneInvalido = "* O Telefone digitado é inválido.";
        public const string IdadeNaoPreenchida = "* Informe a Idade do cliente.";
        public const string MenorDeIdade = "* A Idade é inválida. Cliente deve ser maior de idade.";
        public const string SexoInvalido = "* O Sexo digitado é inválido.";
        public const string CheckOutEmDatasPassadas = "* O Check-out não pode ser realizado antes do Check-in.";
        public const string PrecoNaoPreenchido = "* Informe o Preço da Estadia.";
        public const string PagamentoNaoInformado = "* Informe se o Preço já foi pago.";

        public static void MensagemErroInesperado(string mensagem)
        {
            string titulo = "Ocorreu um Erro Inesperado";
            MessageBox.Show(mensagem, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void MensagemErroListaVazia(string acao)
        {
            MessageBox.Show($"Seu programa não possui nenhuma reserva para {acao}.");
        }

        public static void MensagemErroNenhumaLinhaSelecionada(string acao)
        {
            MessageBox.Show($"Selecione uma linha para {acao}!");
        }
    }
}
