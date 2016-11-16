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
    public partial class FrmPesquisaAgendamento : ChildWindow
    {
        public FrmPesquisaAgendamento()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnEditarAgenda_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExcluirGridagendamento_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            FrmAgendaConsulta agendamento = new FrmAgendaConsulta();
            agendamento.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtDados.Text.Trim() == null)
            {
                MessageBox.Show("Não Foi informados para serem realizados pesquisas, verifique!");
                return;
            }

            biCarregando.IsBusy = true;
            ServiceAgendamento.AgendamentoSoapClient serviceagenda = new ServiceAgendamento.AgendamentoSoapClient();
            serviceagenda.ListaAgendamentoCadastradosCompleted += new EventHandler<ServiceAgendamento.ListaAgendamentoCadastradosCompletedEventArgs>(PesquisaAgendaComplet);
            serviceagenda.ListaAgendamentoCadastradosAsync(txtDados.Text.Trim());
        }
         
        private void PesquisaAgendaComplet(object sender, ServiceAgendamento.ListaAgendamentoCadastradosCompletedEventArgs e)
        {
            ServiceAgendamento.AgendamentoSoapClient servagendamento = new ServiceAgendamento.AgendamentoSoapClient();
            servagendamento.CloseAsync();

            if (this.DialogResult != false)
            {
                if (e.Error != null)
                {
                    biCarregando.IsBusy = false;

                    FrmExcecao frmTentativaListarDadosFormularioCliente = new FrmExcecao("Ocorreu um problema ao buscar os dados", e.Error.Message.ToString());
                    frmTentativaListarDadosFormularioCliente.Show();

                    return;
                }

                dgvAgendamento.DataContext = e.Result;
                biCarregando.IsBusy = false;

            }
        }
    }
}

