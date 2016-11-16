using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsClinVitta.Classes
{
    public class Connection
    {

        public static string Users_DB = "root";
        public static string Password_DB = "";
        public static string Porta = "4444";
        public static string Nome_Bank = "clinvitta";
        public static string PRCBANK_IP = "localhost";

        public static string Host_Bank = @"server=" + PRCBANK_IP +
      ";User Id=" + Users_DB +
      ";PORT =" + Porta +
      ";database=" + Nome_Bank +
      ";password=" + Password_DB;

        public string IpServidor = PRCBANK_IP;
    }
}