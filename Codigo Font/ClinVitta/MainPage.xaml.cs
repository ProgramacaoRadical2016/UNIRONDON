using SilverlightMenu.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClinVitta
{
    public partial class MainPage : UserControl
    {   
        public MainPage()
        {
            InitializeComponent();

            ServiceDataHora.Retorna_Data_HoraSoapClient servdatahora = new ServiceDataHora.Retorna_Data_HoraSoapClient();
            servdatahora.RetornaDataHoraServidor_MySqlCompleted += new EventHandler<ServiceDataHora.RetornaDataHoraServidor_MySqlCompletedEventArgs>(datahoracompleted);
       //   servdatahora.RetornaDataHoraServidor_MySqlAsync(2);
        }

   
        private void datahoracompleted(object sender, ServiceDataHora.RetornaDataHoraServidor_MySqlCompletedEventArgs e)
        {
            DataHoraServidor = Convert.ToDateTime(e.Result);
        }

        private void mnuTop_Loaded(object sender, RoutedEventArgs e)
        {
            Login er = new Login();
            er.Closed += new EventHandler(frmLoginClosedCompleted);
           // er.Show();
        }
        private void frmLoginClosedCompleted(object sender, EventArgs e)
        {
            Login er = (Login)sender;
            if (er.DialogResult == true)
            {
                LoginUsuario = er.LoginUsuario;
                SenhaUsuario = er.LoginSenha;
            }
        }
        private void Menu_MenuItemClicked(object sender, EventArgs e)
        {
            var clickedItem = (MenuItem)sender;
            if (clickedItem.Name == "mnuCadCliente")
            {
                FrmCadastroCliente cli = new FrmCadastroCliente(1,null);
                cli.Show();
                //FrmPesquisarCliente frmcadcli = new FrmPesquisarCliente();
                //frmcadcli.Show();
            }
            else if (clickedItem.Name == "mnuContasPagar")
            {

                //FrmContasaPagar contasapagar = new FrmContasaPagar();
                //contasapagar.Show();

            }


            else if ((clickedItem.Name == "mnuSair"))
            {
                Login login = new Login();
                login.Show();

            }

            else if ((clickedItem.Name == "mnuTrocarSenha"))
            {
                //frmTrocarSenha trocasenha = new frmTrocarSenha(LoginUsuario);
                //trocasenha.Show();

            }

            else if ((clickedItem.Name == "mnuAgendamento"))
            {
                FrmPesquisaAgendamento agenda = new FrmPesquisaAgendamento();
                agenda.Show();

            }

        }

        public string SenhaUsuario { get; set; }

        public DateTime DataHoraServidor { get; set; }

        public string LoginUsuario { get; set; }
    }
}
