using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Reservas_para_Hoteis
{
    static internal class MensagemExcessao
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
    }
}
