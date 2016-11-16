using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ClinVitta.Classes
{
    public class Mensagens
    {
        public static MessageBoxResult Confirmacao(string pMensagem, string pTitulo)
        {
            return MessageBox.Show(pMensagem, pTitulo, MessageBoxButton.OKCancel);
        }

        public static void Informacao(string pMensagem, string pTitulo)
        {
            MessageBox.Show(pMensagem, pTitulo, MessageBoxButton.OK);
        }

        public static void Erro(string pMensagem, string pTitulo)
        {
            MessageBox.Show(pMensagem, pTitulo, MessageBoxButton.OK);
        }

        public static void Alerta(string pMensagem, string pTitulo)
        {
            MessageBox.Show(pMensagem, pTitulo, MessageBoxButton.OK);
        }
    }
}
