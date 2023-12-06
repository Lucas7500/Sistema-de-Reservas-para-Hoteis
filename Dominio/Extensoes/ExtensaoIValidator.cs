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
                var ex = new ValidationException(resultado.Errors);
                string mensagemErro = String.Join("\n", resultado.Errors);
                throw new ArgumentException(mensagemErro, ex);
            }
        }
    }
}
