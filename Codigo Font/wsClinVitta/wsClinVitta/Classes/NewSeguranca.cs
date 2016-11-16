using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace wsClinVitta.Classes
{
    public static class NewSeguranca
    {
        public static string RetornaMd5Hash(string pTexto)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(pTexto));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString());
            }
            return sBuilder.ToString();
        }

        public static string RetornaMd5HashNovo(string pTexto)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(pTexto));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }
            return sBuilder.ToString();
        }

        public static bool VerificaMd5Hash(string pTexto, string pMd5Hash)
        {
            if (string.Compare(RetornaMd5Hash(pTexto), pMd5Hash, true) == 0)
            {
                return true;
            }
            else
                return false;
        }

        public static string Criptografar(string Data)
        {
            Convert.ToBase64String(new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(Data)));
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(Data));
        }

        public static string Descriptografar(string Data)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(Data));
        }


    }
}