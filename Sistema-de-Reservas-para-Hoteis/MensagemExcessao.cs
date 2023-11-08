using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Reservas_para_Hoteis
{
    static internal class MensagemExcessao
    {
        public const string NomeNulo = "* Informe o Nome do Cliente.";
        public const string NomePequeno = "* O Nome não pode conter menos que 3 caracteres.";
        public const string CpfNaoPreenchido = "* O CPF digitado é inválido.";
        public const string TelefoneNaoPreenchido = "* O Telefone digitado é inválido.";
        public const string MenorDeIdade = "* A Idade é inválida. Cliente deve ser maior de idade.";
        public const string IdadeNaoPreenchida = "* Informe a Idade do Cliente.";
        public const string CheckOutEmDatasPassadas = "* O Check-Out não pode ser realizado antes do Check-In.";
        public const string PrecoNaoPreenchido = "* Informe o Preço da Estadia.";
        public const string PrecoEmFormatoInvalido = "* O Preço da Estadia deve conter 2 casas decimais.";
        public const string PagamentoNaoInformado = "* Informe se o Preço já foi pago.";
    }
}
