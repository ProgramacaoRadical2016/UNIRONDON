using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ClinVitta
{
    public partial class FrmTentativa : ChildWindow
    {
        public object Retorno { get; set; }
        private Type _ClientType;
        private string _Acao;
        private string _MsgAdicional;
        private object[] _Parametros;
        public string DATAHORA { get; set; }

        public FrmTentativa(string pTitulo, string pMensagem, string pDetalhe, string pMsgAdicional, Type pClientType, string pAcao, object[] pParametros)
        {
            ServiceDataHora.Retorna_Data_HoraSoapClient DATAHORASERV = new ServiceDataHora.Retorna_Data_HoraSoapClient();
            DATAHORASERV.RetornaDataHoraServidor_MySqlCompleted += new EventHandler<ServiceDataHora.RetornaDataHoraServidor_MySqlCompletedEventArgs>(DatahoraCompleted);
            DATAHORASERV.RetornaDataHoraServidor_MySqlAsync(1);

            InitializeComponent();

            Title = pTitulo;
            tbMensagem.Text = pMensagem;
            txtDetalhe.Text = pDetalhe;
            _ClientType = pClientType;
            _Parametros = pParametros;
            _Acao = pAcao;
            _MsgAdicional = pMsgAdicional;

        }

        private void DatahoraCompleted(object sender, ServiceDataHora.RetornaDataHoraServidor_MySqlCompletedEventArgs e)
        {
            DATAHORA = e.Result;
            // após ter retornado a hora do servidor ele grava as informações na tabelad e erro.
            Classes.ClinVittaAmbiente.GravaLog(tbMensagem.Text, txtDetalhe.Text, DATAHORA);
        }
        public void CompletedEventArgs(object sender, object arg)
        {
            biCarregando.IsBusy = false;

            var proxy = Activator.CreateInstance(_ClientType);
            _ClientType.InvokeMember("CloseAsync", System.Reflection.BindingFlags.InvokeMethod, null, proxy, null);

            if (this.DialogResult != false)
            {
                var Error = arg.GetType().GetProperty("Error").GetValue(arg, null);

                if (Error != null)
                {
                    object Mensagem = Error.GetType().GetProperty("Message").GetValue(Error, null);
                    if (Mensagem != null)
                        tbMensagem.Text = Mensagem.ToString();

                    object Detalhe = Error.ToString();

                    return;
                }

                Retorno = arg.GetType().GetProperty("Result").GetValue(arg, null);

                this.DialogResult = true;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnTentarNovamente_Click(object sender, RoutedEventArgs e)
        {
            biCarregando.IsBusy = true;


        }
    }

}