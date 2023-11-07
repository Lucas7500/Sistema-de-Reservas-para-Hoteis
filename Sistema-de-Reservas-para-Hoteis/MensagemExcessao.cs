using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Reservas_para_Hoteis
{
    static internal class MensagemExcessao
    {
        public const string NomeNulo = "Preencha o campo nome corretamente.";
        public const string NomePequeno = "O campo nome não pode ter menos que 3 caracteres.";
        public const string CpfNaoPreenchido = "O CPF digitado é inválido.";
        public const string MenorDeIdade = "O cliente deve ser maior de idade para fazer a reserva.";
        public const string IdadeNaoPreenchida = "Preencha o campo idade.";
        public const string TelefoneNaoPreenchido = "O número de Telefone digitado não é válido.";
        public const string CheckInEmDatasPassadas = "Data inválida para Check-In";
        public const string CheckOutEmDatasPassadas = "Data inválida para Check-Out";
        public const string PrecoNaoPreenchido = "A estadia deve possuir um preço.";
        public const string PrecoNaoEDecimal = "O preço da estadia deve ter 2 casas decimais.";
        public const string PagamentoNaoInformado = "Informe se a estadia foi paga.";
    }
}
