using System.Text.RegularExpressions;

namespace CadastroEmpresas.Domain.Utils
{
    public class EmailUtils
    {
        /// <summary>
        /// Valida se o e-mail está em um formato correto.
        /// </summary>
        public static bool Validar(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^([a-zA-Z0-9._%+-]+)@([a-zA-Z0-9.-]+)\.([a-zA-Z]{2,})$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}
