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
    public partial class FrmExcecao : ChildWindow
    {
        string DATAHORA;

        public FrmExcecao(string pMensagem, string pDetalhe)
        {
            ServiceDataHora.Retorna_Data_HoraSoapClient DATAHORASERV = new ServiceDataHora.Retorna_Data_HoraSoapClient();
            DATAHORASERV.RetornaDataHoraServidor_MySqlCompleted += new EventHandler<ServiceDataHora.RetornaDataHoraServidor_MySqlCompletedEventArgs>(DatahoraCompleted);
            DATAHORASERV.RetornaDataHoraServidor_MySqlAsync(1);

            InitializeComponent();
            tbMensagem.Text = pMensagem;
            txtDetalhe.Text = pDetalhe;

        }

        private void DatahoraCompleted(object sender, ServiceDataHora.RetornaDataHoraServidor_MySqlCompletedEventArgs e)
        {
            if (this.DialogResult != false)
            {
                if (e.Error != null)
                {
                    biCarregando.IsBusy = false;
                }

                DATAHORA = e.Result;
                // após ter retornado a hora do servidor ele grava as informações na tabelad e erro.
                Classes.ClinVittaAmbiente.GravaLog(tbMensagem.Text, txtDetalhe.Text, DATAHORA);
            }
        }


        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            biCarregando.IsBusy = false;

        }
    }
}

