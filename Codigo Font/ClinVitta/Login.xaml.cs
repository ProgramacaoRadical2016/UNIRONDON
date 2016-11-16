using ClinVitta.Classes;
using ClinVitta.ServiceUsuario;
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
    public partial class Login : ChildWindow
    {
        public Login()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsuario.Text.Trim().Length == 0 && txtSenha.Password.Trim().Length == 0)
            {
                MessageBox.Show("Informe o usuário e senha.");
                return;
            }

            if (txtUsuario.Text.Trim().Length == 0)
            {
                MessageBox.Show("Informe o usuário.");
                txtUsuario.Focus();
                return;
            }

            if (txtSenha.Password.Trim().Length == 0)
            {
                MessageBox.Show("Informe a senha.");
                txtSenha.Focus();
                return;
            }

            biCarregando.IsBusy = true;

            AutenticaUsuarioSoapClient ClientServicoUsuario = new AutenticaUsuarioSoapClient();
            ClientServicoUsuario.BuscaUsuarioCompleted += new EventHandler<BuscaUsuarioCompletedEventArgs>(clienteServiceUsuario_AutenticarCompleted);
            ClientServicoUsuario.BuscaUsuarioAsync(txtUsuario.Text.Trim(), Classes.NewSeguranca.Criptografar(txtSenha.Password), ClinVittaAmbiente.CodProjeto);
        }
        private void clienteServiceUsuario_AutenticarCompleted(object sender, BuscaUsuarioCompletedEventArgs e)
        {
            if (this.DialogResult != false)
            {
                if (e.Error != null)
                {
                    biCarregando.IsBusy = false;

                    Mensagens.Alerta("Tempo de Conexão Excedido, tente novamente mais tarde!", "New Soft Tecnologia");
                    LoginUsuario = txtUsuario.Text.Trim();

                    return;
                }

                CarregarAutenticar(e.Result);
            }
        }

        private void frmTentativaAutenticarUsuario_Closed(object sender, EventArgs e)
        {
            if (this.DialogResult != false)
            {
                FrmTentativa frmTentativa = (FrmTentativa)sender;
                if (frmTentativa.DialogResult == true)
                {
                    //ServiceUsuario. retorno = (ServiceUsuario.Valida_UsuarioSoapClient)frmTentativa.Retorno;
                    //CarregarAutenticar(retorno);
                }
            }
        }
        private void CarregarAutenticar(DadosUsuario[] dadosUsuario)
        {
            foreach (var pRETORNO in dadosUsuario)
            {

                if (pRETORNO.Status == 2)
                {

                    biCarregando.IsBusy = false;
                    Mensagens.Erro("Usuário sem Permissão nesse Projeto!", "Usuário");
                    txtUsuario.Focus();
                }

                if (pRETORNO.Status == 1)
                {

                    biCarregando.IsBusy = true;
                    bLoginValido = true;
                    this.DialogResult = true;

                    // Dados Usuario
                    Classes.ClinVittaAmbiente.CodUsuario = pRETORNO.CODUSUARIO;
                    Classes.ClinVittaAmbiente.NomeUsuario = pRETORNO.NomeUsuario;
                    Classes.ClinVittaAmbiente.CodFilialUsuario = pRETORNO.FilialUsuario;
                    Classes.ClinVittaAmbiente.UsuarioLogin = txtUsuario.Text.Trim();
                    LoginSenha = txtSenha.Password.ToString();

                    ControleFormularioAviso();

                }

                else if (pRETORNO.Status == 0)
                {

                    biCarregando.IsBusy = false;

                    Mensagens.Erro("Usuario / Senha Inválidos ou  não Cadastrado!", "Usuário");
                    txtUsuario.Focus();
                }
                else
                {
                    biCarregando.IsBusy = false;
                    break;

                }

                if (bLoginValido == true)
                {
                    this.Close();
                }
            }
        }
        private void ChildWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !bLoginValido;
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            /* Remove aspas simples e duplas e espaço em branco. */
            if (e.PlatformKeyCode == 222 || e.Key == Key.Space)
                e.Handled = true;

            if (e.Key == Key.Enter)
                txtSenha.Focus();
        }

        private void txtUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtUsuario.TextChanged -= txtUsuario_TextChanged;
            txtUsuario.Text = txtUsuario.Text.Replace("'", "").Replace("\"", "");
            txtUsuario.TextChanged += txtUsuario_TextChanged;
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                OKButton_Click(sender, null);
        }

        private void ChildWindow_Closed(object sender, EventArgs e)
        {

        }
        private void ChildWindow_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Browser.HtmlPage.Plugin.Focus();
            RoutedEventHandler reHandler = null;

            if (txtUsuario.Text.Length > 0)
            {
                reHandler = delegate
                {
                    txtSenha.Focus();
                    this.GotFocus -= reHandler;
                };
            }
            else
            {
                reHandler = delegate
                {
                    txtUsuario.Focus();
                    this.GotFocus -= reHandler;
                };
            }

            this.GotFocus += reHandler;
        }

        private void ControleFormularioAviso()
        {

            //PopAvisoControlePrazo avisoprazo = new PopAvisoControlePrazo();
            //avisoprazo.Show();

        }


        public string LoginUsuario { get; set; }

        public bool bLoginValido { get; set; }

        public string LoginSenha { get; set; }
    }
}

