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
    public class ClinVittaAmbiente
    {
        public static string Versao = "1.0.0.00";
        public static string CodProjeto = "1";

        public static string Mensagem { get; set; }
        public static string Detalhe { get; set; }
        public static string CodUsuario { get; set; }

        public static string Url = "http://localhost/clinvitta/";

        public static string IpUsuario { get; set; }
        public static string UsuarioLogin { get; set; }


        public static System.Globalization.CultureInfo Culture = new System.Globalization.CultureInfo("pt-BR")
        {
            DateTimeFormat = new System.Globalization.DateTimeFormatInfo()
            {
                ShortTimePattern = "HH:mm",
                ShortDatePattern = "dd/MM/yyyy",
                LongTimePattern = "HH:mm:ss",
                LongDatePattern = "dddd, d' de 'MMMM' de 'yyyy",
                FullDateTimePattern = "dddd, d' de 'MMMM' de 'yyyy HH:mm:ss",
                MonthDayPattern = "d' de 'MMMM",
                YearMonthPattern = "MMMM' de 'yyyy",
                FirstDayOfWeek = DayOfWeek.Sunday
            }
        };



        public static void GravaLog(string pMensagem, string pDetalhe, string DataHora)
        {

            ServiceControledeLogErro.ControledeLogExceptionSoapClient serviceErro = new ServiceControledeLogErro.ControledeLogExceptionSoapClient();


            try
            {
                if (!string.IsNullOrWhiteSpace(pMensagem))
                {
                    if (pMensagem.Length <= 4000)
                        Mensagem = pMensagem;
                    else
                        Mensagem = pMensagem.Substring(0, 4000);
                }

                if (!string.IsNullOrWhiteSpace(pDetalhe))
                {
                    if (pDetalhe.Length <= 4000)
                        Detalhe = pDetalhe;
                    else
                        Detalhe = pDetalhe.Substring(0, 4000);
                }

                Detalhe = pDetalhe;

                serviceErro.GravaLogAsync(ClinVittaAmbiente.Mensagem, ClinVittaAmbiente.Detalhe,
                                          ClinVittaAmbiente.CodUsuario, ClinVittaAmbiente.CodProjeto,
                                          DataHora,
                                          ClinVittaAmbiente.IpUsuario,
                                          ClinVittaAmbiente.Versao);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gravar log: " + ex.Message);
            }

        }

        public static string NomeUsuario { get; set; }

        public static string CodFilialUsuario { get; set; }
    }
}
