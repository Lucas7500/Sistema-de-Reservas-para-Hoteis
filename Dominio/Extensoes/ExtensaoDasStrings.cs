namespace Dominio.Extensoes
{
    public static class ExtensaoDasStrings
    {
        public static bool ContemValor(this string? valor)
        {
            return !string.IsNullOrWhiteSpace(valor);
        }
    }
}
