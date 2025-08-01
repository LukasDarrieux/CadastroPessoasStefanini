using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CadastroPessoasStefanini.Util.Validacoes
{
    public static class Validador
    {
        private const short TAMANHO_CPF = 11;
        private static short[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static short[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        public static bool ValideCPF(string cpf)
        {
            string auxCPF, digito;
            int soma = 0;

            cpf = RemoveMascara(cpf);

            if (cpf.Length < TAMANHO_CPF) return false;

            auxCPF = cpf.Substring(0, 9);

            for (short i = 0; i < 9; i++)
            {
                soma += short.Parse(auxCPF[i].ToString()) * multiplicador1[i];
            }

            int resto = soma % 11;
            if (resto < 2) resto = 0;
            else resto = 11 - resto;

            digito = resto.ToString();

            auxCPF = auxCPF + digito;

            soma = 0;

            for (short i = 0; i < 10; i++)
            {
                soma += short.Parse(auxCPF[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            if (resto < 2) resto = 0;
            else resto = 11 - resto;

            auxCPF = auxCPF + resto;

            return (cpf == auxCPF);
        }

        /// <summary>
        /// Remove a máscara do CPF informado, retornando apenas os números
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static string RemoveMascara(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return string.Empty;
            return cpf.Replace(".", string.Empty).Replace(",", string.Empty).Replace("-", string.Empty);
        }

        /// <summary>
        /// Verifica se o e-mail informado é valido
        /// </summary>
        /// <param name="email">e-mail</param>
        /// <returns></returns>
        public static bool EmailValido(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}
