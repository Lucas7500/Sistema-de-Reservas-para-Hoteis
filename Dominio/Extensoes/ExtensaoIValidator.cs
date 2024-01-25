using FluentValidation;

namespace Dominio.Extensoes
{
    public static class ExtensaoIValidator
    {
        public static void ValidateAndThrowArgumentException(this IValidator<Reserva> validator, Reserva instance)
        {
            var resultado = validator.Validate(instance);

            if (!resultado.IsValid)
            {
                const char quebraDeLinha = '\n';
                
                var erro = new ValidationException(resultado.Errors);
                string mensagemErro = String.Join(quebraDeLinha, resultado.Errors);
                
                throw new ArgumentException(mensagemErro, erro);
            }
        }
    }
}
