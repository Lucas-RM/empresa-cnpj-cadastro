using System.Text.RegularExpressions;

namespace CadastroEmpresas.Domain.Utils
{
    public static class CnpjUtils
    {
        /// <summary>
        /// Formata um CNPJ para o padrão 00.000.000/0000-00.
        /// </summary>
        public static string Formatar(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return string.Empty;

            var apenasNumeros = Regex.Replace(cnpj, @"\D", "");

            if (apenasNumeros.Length != 14)
                return cnpj;

            return Convert.ToUInt64(apenasNumeros).ToString(@"00\.000\.000\/0000\-00");
        }

        /// <summary>
        /// Remove todos os caracteres não numéricos de um CNPJ, retornando apenas os 14 dígitos.
        /// </summary>
        public static string Limpar(string cnpj)
        {
            return new string(cnpj.Where(char.IsDigit).ToArray());
        }
    }
}
