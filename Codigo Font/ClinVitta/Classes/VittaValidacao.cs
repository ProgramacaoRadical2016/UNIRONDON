using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace ClinVitta.Classes
{
    public class VittaValidacao
    {
        public static bool ValidaData(string pData)
        {
            DateTime result;
            if (DateTime.TryParse(pData, new System.Globalization.CultureInfo("pt-BR"), System.Globalization.DateTimeStyles.None, out result))
                return true;
            else
                return false;
        }

        public static bool ValidaEmail(string pEmail)
        {
            // Expressão regular que vai validar os e-mails
            string emailRegex = @"^(([^<>()[\]\\.,;áàãâäéèêëíìîïóòõôöúùûüç:\s@\""]+"
            + @"(\.[^<>()[\]\\.,;áàãâäéèêëíìîïóòõôöúùûüç:\s@\""]+)*)|(\"".+\""))@"
            + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|"
            + @"(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";

            // Instância da classe Regex, passando como 
            // argumento sua Expressão Regular 
            Regex rx = new Regex(emailRegex);

            // Método IsMatch da classe Regex que retorna
            // verdadeiro caso o e-mail passado estiver
            // dentro das regras da sua regex.
            return rx.IsMatch(pEmail);
        }

        public static bool ValidaCPF(string pCpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            pCpf = pCpf.Trim();
            pCpf = pCpf.Replace(".", "").Replace("-", "");
            if (pCpf.Length != 11)
                return false;
            tempCpf = pCpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return pCpf.EndsWith(digito);
        }

        public static bool ValidaCnpj(string pCnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            pCnpj = pCnpj.Trim();
            pCnpj = pCnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (pCnpj.Length != 14)
                return false;
            tempCnpj = pCnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return pCnpj.EndsWith(digito);
        }

        public static bool ValidaDDD(string pDDD)
        {
            int ddd;
            try
            {
                ddd = Convert.ToInt16(pDDD);
                if (ddd >= 10 && ddd <= 99)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidaNumeroTelefone(string pNumero)
        {
            string numero = pNumero.Replace("-", "");
            int iPrimeiroDigito = 0;

            if (numero.Length != 8)
                return false;

            try
            {
                iPrimeiroDigito = Convert.ToInt16(numero[0].ToString());
            }
            catch
            {
                return false;
            }

            foreach (char chr in numero)
                if (!char.IsDigit(chr))
                    return false;

            if (iPrimeiroDigito < 2 || iPrimeiroDigito > 5)
                return false;

            return true;
        }

        public static bool ValidaNumeroCelular(string pNumero)
        {
            string numero = pNumero.Replace("-", "");
            int iPrimeiroDigito = 0;

            if (numero.Length != 8 && numero.Length != 9)
                return false;

            try
            {
                iPrimeiroDigito = Convert.ToInt16(numero[0].ToString());
            }
            catch
            {
                return false;
            }

            if (iPrimeiroDigito < 6)
                return false;

            foreach (char chr in numero)
                if (!char.IsDigit(chr))
                    return false;

            return true;
        }

        public static bool ValidaNumeroTelefoneCelular(string pNumero)
        {
            string numero = pNumero.Replace("-", "");
            int iPrimeiroDigito = 0;

            try
            {
                iPrimeiroDigito = Convert.ToInt16(numero[0].ToString());
            }
            catch
            {
                return false;
            }

            if (numero.Length == 8)
            {
                if (iPrimeiroDigito < 2)
                    return false;
            }
            else if (numero.Length == 9)
            {
                if (iPrimeiroDigito < 6)
                    return false;
            }
            else
                return false;

            foreach (char chr in numero)
                if (!char.IsDigit(chr))
                    return false;

            return true;
        }
    }
}

