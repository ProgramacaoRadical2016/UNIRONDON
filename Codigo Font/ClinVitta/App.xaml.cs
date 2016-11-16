using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Net;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Threading;

namespace ClinVitta
{
    public partial class App : Application
    {

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;
            //WebRequest.RegisterPrefix("http://", System.Net.Browser.WebRequestCreator.ClientHttp);

            //Thread.CurrentThread.CurrentCulture = ClinVitta.Classes.ClinVittaAmbiente.Culture;
            //Thread.CurrentThread.CurrentUICulture = ClinVitta.Classes.ClinVittaAmbiente.Culture;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.RootVisual = new MainPage();

            //if (e.InitParams.ContainsKey("UserIP"))
            //    ClinVitta.Classes.ClinVittaAmbiente.IpUsuario = e.InitParams["UserIP"];
            //if (e.InitParams.ContainsKey("versao"))
            //    ClinVitta.Classes.ClinVittaAmbiente.Versao = e.InitParams["versao"];
            //if (e.InitParams.ContainsKey("Url"))
            //    ClinVitta.Classes.ClinVittaAmbiente.Url = e.InitParams["Url"];
        }

        private void Application_Exit(object sender, EventArgs e)
        {

        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            FrmExcecao excessao = new FrmExcecao("Erro de Execução de Aplicação.", e.ExceptionObject.Message + e.ExceptionObject.StackTrace);
            excessao.Show();

        }

        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}

