using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace ClinVitta.Classes
{
    public class NewSeguranca
    {
        public static string Criptografar(string Data)
        {
            Convert.ToBase64String(new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(Data)));
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(Data));
        }

    }
}