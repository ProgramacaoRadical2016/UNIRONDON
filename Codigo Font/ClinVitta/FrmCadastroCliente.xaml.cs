using ClinVitta.Classes;
using ClinVitta.ServiceCadastroCliente;
using ClinVitta.ServiceCEP;
using ClinVitta.ServiceControleCadCliente;
using ClinVitta.ServiceDataHora;
using ClinVitta.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ClinVitta
{
    public partial class FrmCadastroCliente : ChildWindow
    {
        string CodUsuario;
        ServiceCadastroCliente.Busca_Dados_Cadastro_ClienteSoapClient ServicoCadastroCliente = new Busca_Dados_Cadastro_ClienteSoapClient();
        private List<ValidacaoClienteSummaryItem> Validacao = new List<ValidacaoClienteSummaryItem>();
        #region AtualizaHeaderValidationSummary
        private void LayoutRoot_BindingValidationError(object sender, ValidationErrorEventArgs e)
        {
            ValidationSummaryItem valsumremove = vsValidaCampos.Errors.FirstOrDefault(v => v.Context == e.OriginalSource);

            if (e.Action == ValidationErrorEventAction.Added)
            {
                if (valsumremove == null)
                {
                    ValidationSummaryItem vsi = new ValidationSummaryItem() { Message = e.Error.ErrorContent.ToString(), Context = e.OriginalSource, MessageHeader = "" };
                    vsi.ItemType = ValidationSummaryItemType.ObjectError;
                    vsi.Sources.Add(new ValidationSummaryItemSource("", e.OriginalSource as Control));
                    vsValidaCampos.Errors.Insert(vsValidaCampos.Errors.Count, vsi);
                }
            }
            else
            {
                if (valsumremove != null)
                {
                    var listaRemover = vsValidaCampos.Errors.Where(item => item == valsumremove).ToList();

                    foreach (var item in listaRemover)
                        vsValidaCampos.Errors.Remove(item);
                }
            }

            AtualizaHeaderValidationSummary();
        }

        private void AtualizaHeaderValidationSummary()
        {
            vsValidaCampos.Header = vsValidaCampos.Errors.Count + (vsValidaCampos.Errors.Count > 1 ? " problemas encontrados." : " problema encontrado.");
        }
        #endregion

        public string cmbtpDOC { get; set; }

        public string cmboex { get; set; }

        public string UfEmisao { get; set; }

        public string cmbsexo { get; set; }

        public string cmbcivil { get; set; }

        public int TipoCadastro { get; set; }
    
        public FrmCadastroCliente(int pTipo, string pCodCli)
        {
            InitializeComponent();
            ControleCamposHabiDesab(false);
            btnLimpar.IsEnabled = false;
            btnGravar.IsEnabled = false;
            btnNovo.IsEnabled = true;
            btnCancelar.IsEnabled = false;
            ListaServicos();
            CodCli = pCodCli;
            TipoCadastro = pTipo;

        }

        private void ListaServicos()
        {
             //Carrega o Campo Tipo de Documentos
            Busca_Dados_Cadastro_ClienteSoapClient ServicoCadastroCliente = new Busca_Dados_Cadastro_ClienteSoapClient();
            ServicoCadastroCliente.TipoDocumentoCompleted += new EventHandler<TipoDocumentoCompletedEventArgs>(ClienteServiceListaTipoDocCompleted);
            ServicoCadastroCliente.TipoDocumentoAsync();

            // Carrega UF
            PesquisaCEPSoapClient servicoCepUF = new PesquisaCEPSoapClient();
            ServicoCadastroCliente.BuscaListaUFCompleted += new EventHandler<BuscaListaUFCompletedEventArgs>(ClienteListaUfCompleted);
            ServicoCadastroCliente.BuscaListaUFAsync();

            // Carrega Tipo Telefone
            ServicoCadastroCliente.TipoTelefoneCompleted += new EventHandler<TipoTelefoneCompletedEventArgs>(TipoTelCompleted);
            ServicoCadastroCliente.TipoTelefoneAsync();

            // Carrega Orgao Emissor
            ServicoCadastroCliente.TipoOrgaoEmissorCompleted += new EventHandler<TipoOrgaoEmissorCompletedEventArgs>(TipoOrgaoEmissorCompleted);
            ServicoCadastroCliente.TipoOrgaoEmissorAsync();

            // Carrega Data Atual
            ServiceDataHora.Retorna_Data_HoraSoapClient serviceDataHora = new Retorna_Data_HoraSoapClient();
            serviceDataHora.RetornaDataHoraServidor_MySqlCompleted += new EventHandler<ServiceDataHora.RetornaDataHoraServidor_MySqlCompletedEventArgs>(dataHoraCompleted);
            serviceDataHora.RetornaDataHoraServidor_MySqlAsync(2);
        }
        #region Tipo de Orgão Emissor
        private void TipoOrgaoEmissorCompleted(object sender, TipoOrgaoEmissorCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                biCarregando.IsBusy = false;

                FrmTentativa frmTentativaListarDadosFormularioCliente = new FrmTentativa("Ocorreu um problema ao carregar os dados de preenchimento do formulário", e.Error.Message.ToString(), e.Error.ToString(), "", typeof(PesquisaCEPSoapClient), "listaTipoDocumentoCliente", null);
                frmTentativaListarDadosFormularioCliente.Closed += new EventHandler(frmTentativaListarDadosFormularioCliente_Closed);
                frmTentativaListarDadosFormularioCliente.Show();

                return;
            }

            cmbOEX.ItemsSource = e.Result;
        }
        #endregion
        #region Lista Tipo Telefone
        private void TipoTelCompleted(object sender, TipoTelefoneCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                biCarregando.IsBusy = false;

                FrmTentativa frmTentativaListarDadosFormularioCliente = new FrmTentativa("Ocorreu um problema ao carregar os dados de preenchimento do formulário", e.Error.Message.ToString(), e.Error.ToString(), "", typeof(Busca_Dados_Cadastro_ClienteSoapClient), "listaTipoTelefone", null);
                frmTentativaListarDadosFormularioCliente.Closed += new EventHandler(frmTentativaListarDadosFormularioCliente_Closed);
                frmTentativaListarDadosFormularioCliente.Show();

                return;
            }
            cbTipoTelefone.ItemsSource = e.Result;
        }
        #endregion
        #region Lista Uf de Emissao | Uf de Endereco
        private void ClienteListaUfCompleted(object sender, BuscaListaUFCompletedEventArgs e)
        {
            if (this.DialogResult != false)
            {
                if (e.Error != null)
                {
                    biCarregando.IsBusy = false;

                    FrmTentativa frmTentativaListarDadosFormularioCliente = new FrmTentativa("Ocorreu um problema ao carregar os dados de preenchimento do formulário", e.Error.Message.ToString(), e.Error.ToString(), "", typeof(PesquisaCEPSoapClient), "listaDadosCadastroCliente", null);
                    frmTentativaListarDadosFormularioCliente.Closed += new EventHandler(frmTentativaListarDadosFormularioCliente_Closed);
                    frmTentativaListarDadosFormularioCliente.Show();

                    return;
                }

                ufEmisao.ItemsSource = e.Result;

            }
        }
        #endregion

        void ClienteServiceListaTipoDocCompleted(object sender, TipoDocumentoCompletedEventArgs e)
        {
            if (this.DialogResult != false)
            {
                if (e.Error != null)
                {
                    biCarregando.IsBusy = false;

                    FrmTentativa frmTentativaListarDadosFormularioCliente = new FrmTentativa("Ocorreu um problema ao carregar os dados de preenchimento do formulário", e.Error.Message.ToString(), e.Error.ToString(), "", typeof(PesquisaCEPSoapClient), "listaTipoDocumentoCliente", null);
                    frmTentativaListarDadosFormularioCliente.Closed += new EventHandler(frmTentativaListarDadosFormularioCliente_Closed);
                    frmTentativaListarDadosFormularioCliente.Show();

                    return;
                }


                cmbTpDOC.ItemsSource = e.Result;
            }
        }


        private void dataHoraCompleted(object sender, RetornaDataHoraServidor_MySqlCompletedEventArgs e)
        {
            DataAtual = e.Result;
        }

        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = Mensagens.Confirmacao("Deseja realmente sair?", "Cadastro de Cliente");

            if (result == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        #region Limpa Controles Formularios
        private void LimpaDatas()
        {
            txtDATA_nasc.Text = "__/__/____";

        }
        private void LimpaCamposFormulario()
        {

            EndListaTelefone.DataContext = null;
            qtDependentes.Text = "";
            cmbTpDOC.ItemsSource = null;
            txtMae.Text = "";
            txtPai.Text = "";
            cmbSexo.ItemsSource = null;
            cmbCivil.ItemsSource = null;
            ufEmisao.ItemsSource = null;
            txtCPF.Text = "";
            NumDoc.Text = "";
            cmbOEX.ItemsSource = null;
            txtNome.Text = "";
            txtDATA_nasc.Text = "";
            txtCEP.Text = "";
            CmbUF.Text = "";
            CmbCIDADE.Text = "";
            CmbBAIRRO.Text = "";
            CmbTPLOGRADOURO.Text = "";
            txtEND.Text = "";
            txtNUM.Text = "";
            txtREFERENCIA.Text = "";
            txtCOMPLEMENTO.Text = "";
            txtEmail.Text = "";

            LimpaDatas();
        }

        #endregion


        public string DataAtual { get; set; }

        private void ControleCamposHabiDesab(bool pHabilita)
        {

            cbTipoTelefone.IsEnabled = pHabilita;
            txtProfissao.IsEnabled = pHabilita;
            btnAlterar.IsEnabled = pHabilita;
            cmbTpDOC.IsEnabled = pHabilita;
            txtPai.IsEnabled = pHabilita;
            txtMae.IsEnabled = pHabilita;
            cmbSexo.IsEnabled = pHabilita;
            cmbCivil.IsEnabled = pHabilita;
            ufEmisao.IsEnabled = pHabilita;
            txtNome.IsEnabled = pHabilita;
            txtDATA_nasc.IsEnabled = pHabilita;
            txtCEP.IsEnabled = pHabilita;
            CmbUF.IsEnabled = pHabilita;
            CmbCIDADE.IsEnabled = pHabilita;
            CmbBAIRRO.IsEnabled = pHabilita;
            CmbTPLOGRADOURO.IsEnabled = pHabilita;
            txtEND.IsEnabled = pHabilita;
            txtNUM.IsEnabled = pHabilita;
            txtREFERENCIA.IsEnabled = pHabilita;
            txtCOMPLEMENTO.IsEnabled = pHabilita;
            txtEmail.IsEnabled = pHabilita;
            txtCPF.IsEnabled = pHabilita;
            NumDoc.IsEnabled = pHabilita;
            cmbOEX.IsEnabled = pHabilita;
            qtDependentes.IsEnabled = pHabilita;


            // --- botoes ------

            btnCEP.IsEnabled = pHabilita;
            btnAddTelResidencialPF.IsEnabled = pHabilita;
            btnRemTelefoneResidencialPF.IsEnabled = pHabilita; ;


            btnPesquisaCep.IsEnabled = pHabilita;
            btnEditarEnd.IsEnabled = pHabilita;
            txtdddtel.IsEnabled = pHabilita;
            txtNumerotel.IsEnabled = pHabilita;
            EndListaTelefone.IsEnabled = pHabilita;

        }

        private void IncluirCadastroCliente()
        {
          //  GravaCadastroCliente();
        }
        private void frmTentativaListarDadosFormularioCliente_Closed(object sender, EventArgs e)
        {
            if (this.DialogResult != false)
            {
                FrmTentativa frmTentativa = (FrmTentativa)sender;
                if (frmTentativa.DialogResult == true)
                    this.Close();

            }
        }

        private void btnRemTelefoneResidencialPF_Click(object sender, RoutedEventArgs e)
        {
            if (EndListaTelefone.SelectedItem == null)
            {
                MessageBox.Show("Telefone não selecionado.");
                EndListaTelefone.Focus();
                return;
            }

            PagedCollectionView pcv = (PagedCollectionView)EndListaTelefone.ItemsSource;
            MvClienteTelEnd mv_Cliente_Tel_End = (MvClienteTelEnd)pcv[pcv.IndexOf((MvClienteTelEnd)EndListaTelefone.SelectedItem)];

            //if (mv_Cliente_Tel_End.Codclitelend != null)
            //{
            //    MessageBox.Show("Não é permitido a exclusão de um telefone já gravado.");
            //    return;
            //}

            if (mv_Cliente_Tel_End.Codclitelend == null)
                pcv.Remove(mv_Cliente_Tel_End);
            else
            {
                mv_Cliente_Tel_End.EstadoItem = WCFEstadoItem.Deletado;
                pcv.Refresh();
            }
        }

        private void btnAddTelResidencialPF_Click(object sender, RoutedEventArgs e)
        {

            if (txtdddtel.Text.Trim().Length == 0 && txtNumerotel.Text.Trim().Length == 0 && cbTipoTelefone.SelectedValue == null)
            {
                MessageBox.Show("Informe o ddd, o número e o tipo do telefone.");
                return;
            }

            if (txtdddtel.Text.Length == 0)
            {
                MessageBox.Show("Informe o ddd.");
                txtdddtel.Focus();
                return;
            }

            if (txtNumerotel.Text.Length == 0)
            {
                MessageBox.Show("Informe o número do telefone.");
                txtNumerotel.Focus();
                return;
            }

            if (cbTipoTelefone.SelectedValue == null)
            {
                MessageBox.Show("Informe o tipo do telefone.");
                cbTipoTelefone.Focus();
                return;
            }
            if (!VittaValidacao.ValidaDDD(txtdddtel.Text.Trim()))
            {
                MessageBox.Show("O ddd informado é inválido.");
                txtdddtel.Focus();
                return;
            }

            if (!VittaValidacao.ValidaNumeroTelefoneCelular(txtNumerotel.Text.Trim()))
            {
                MessageBox.Show("O número do telefone informado é inválido.");
                txtNumerotel.Focus();
                return;
            }

            PagedCollectionView pcv = (PagedCollectionView)EndListaTelefone.ItemsSource;
            ObservableCollection<MvClienteTelEnd> lstTelEnd;

            if (pcv == null)
            {
                lstTelEnd = new ObservableCollection<MvClienteTelEnd>();
                pcv = new PagedCollectionView(lstTelEnd);

                if (pcv != null && pcv.CanFilter)
                    pcv.Filter = new Predicate<object>(FilterCompletedTelEnd);

                EndListaTelefone.ItemsSource = pcv;
            }
            else
                lstTelEnd = (ObservableCollection<MvClienteTelEnd>)pcv.SourceCollection;

            foreach (var item in lstTelEnd)
            {
                if (item.EstadoItem != WCFEstadoItem.Deletado)
                {
                    if (item.Ddd == txtdddtel.Text.Trim() && item.Fone == txtNumerotel.Text.Trim())
                    {
                        MessageBox.Show("Telefone já incluído.");
                        txtdddtel.Focus();
                        return;
                    }
                }
            }

            Funcoes.ClearControlError(txtdddtel);
            Funcoes.ClearControlError(txtNumerotel);
            Funcoes.ClearControlError(cbTipoTelefone);

            MvClienteTelEnd mv_Cliente_Tel_End = new MvClienteTelEnd();
            mv_Cliente_Tel_End.Preenchendo = true;

            mv_Cliente_Tel_End.Codtptel = Convert.ToInt32(cbTipoTelefone.SelectedValue.ToString());
            mv_Cliente_Tel_End.CodtptelIsEnabled = true;


            mv_Cliente_Tel_End.Ddd = txtdddtel.Text.Trim();
            mv_Cliente_Tel_End.Fone = txtNumerotel.Text.Trim().Replace("-", "");

            mv_Cliente_Tel_End.EstadoItem = WCFEstadoItem.Novo;

            mv_Cliente_Tel_End.Preenchendo = false;

            lstTelEnd.Add(mv_Cliente_Tel_End);

            cbTipoTelefone.SelectedItem = null;
            txtdddtel.Text = "";
            txtNumerotel.Text = "";
        }
        private bool FilterCompletedTelEnd(object t)
        {
            MvClienteTelEnd mv_Cliente_Tel_End = t as MvClienteTelEnd;
            return (mv_Cliente_Tel_End.EstadoItem == WCFEstadoItem.Novo) || (mv_Cliente_Tel_End.EstadoItem == WCFEstadoItem.Modificado)
                   || (mv_Cliente_Tel_End.EstadoItem == WCFEstadoItem.Original);
        }

        private void LimpaEstadoErro()
        {

            Funcoes.ClearControlError(txtNome);
            Funcoes.ClearControlError(cmbSexo);
        }


        #region Controle e Paramentros de  CEP
        private void btnCEP_Click(object sender, RoutedEventArgs e)
        {
            if (txtCEP.Text != null)
            {
                biCarregando.IsBusy = true;

                PesquisaCEPSoapClient ClientServicoCEP = new PesquisaCEPSoapClient();
                ClientServicoCEP.BuscarCEPCompleted += new EventHandler<BuscarCEPCompletedEventArgs>(clienteServiceCEP_Completed);
                ClientServicoCEP.BuscarCEPAsync(txtCEP.Text.Trim());
            }
            else
            {
                Mensagens.Erro("Informe primeramente o CEP!", "Cadastro de Cliente");
            }
        }
        void clienteServiceCEP_Completed(object sender, BuscarCEPCompletedEventArgs e)
        {
            if (this.DialogResult != false)
            {
                if (e.Error != null)
                {
                    biCarregando.IsBusy = false;

                    FrmTentativa frmTentativaListarDadosFormularioCliente = new FrmTentativa("Ocorreu um problema ao carregar os dados de preenchimento do formulário", e.Error.Message.ToString(), e.Error.ToString(), "", typeof(PesquisaCEPSoapClient), "listaDadosCadastroCliente", null);
                    frmTentativaListarDadosFormularioCliente.Closed += new EventHandler(frmTentativaListarDadosFormularioCliente_Closed);
                    frmTentativaListarDadosFormularioCliente.Show();

                    return;
                }

                CarregaCEP(e.Result);
            }
        }

        private void CarregaCEP(string pStringCEP)
        {
            string linha = pStringCEP;

            if (linha != null)
            {
                string[] campos = linha.Split('ý');

                CmbUF.Text = campos[0].Trim();
                CmbCIDADE.Text = campos[1].Trim();
                CmbBAIRRO.Text = campos[2].Trim();
                CmbTPLOGRADOURO.Text = campos[3].Trim();
                txtEND.Text = campos[4].Trim();
                txtNUM.Focus();
                biCarregando.IsBusy = false;
            }
            else
            {

                Mensagens.Erro("O CEP informado não está cadastrado ou não exite!", "Cadatro de Cliente");
                txtCEP.Focus();
                biCarregando.IsBusy = false;
                txtCEP.Text = "";
                CmbUF.Text = "";
                CmbCIDADE.Text = "";
                CmbBAIRRO.Text = "";
                CmbTPLOGRADOURO.Text = "";
                txtEND.Text = "";
            }
        }

        private void btnPesquisaCep_Click(object sender, RoutedEventArgs e)
        {

            txtCEP.Text = "";
            CmbUF.Text = "";
            CmbCIDADE.Text = "";
            CmbBAIRRO.Text = "";
            CmbTPLOGRADOURO.Text = "";
            txtEND.Text = "";


            CmbUF.IsReadOnly = true;
            CmbCIDADE.IsReadOnly = true;
            CmbBAIRRO.IsReadOnly = true;
            CmbTPLOGRADOURO.IsReadOnly = true;
            txtEND.IsReadOnly = true;
            txtCEP.Focus();
        }

        private void btnEditarEnd_Click(object sender, RoutedEventArgs e)
        {
            txtCEP.IsReadOnly = false;
            CmbUF.IsReadOnly = false;
            CmbCIDADE.IsReadOnly = false;
            CmbBAIRRO.IsReadOnly = false;
            CmbTPLOGRADOURO.IsReadOnly = false;
            txtEND.IsReadOnly = false;
        }
        
        #endregion

        private void ChildWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (TipoCadastro == 1 || TipoCadastro == 3)
            {
                btnNovo.IsEnabled = true;
                btnCancelar.IsEnabled = false;
                btnLimpar.IsEnabled = false;
                btnGravar.IsEnabled = false;
                btnAlterar.IsEnabled = false;
            }
            else if (TipoCadastro == 2)
            {
                biCarregando.IsBusy = true;
                ServicoCadastroCliente.PesquisaCadastroClienteCompletoCompleted += new EventHandler<PesquisaCadastroClienteCompletoCompletedEventArgs>(PesquisaCadastroClienteCompleted);
                ServicoCadastroCliente.PesquisaCadastroClienteCompletoAsync(CodCli);

                ServicoCadastroCliente.PesquisaTelefoneClienteCompleted += new EventHandler<PesquisaTelefoneClienteCompletedEventArgs>(PesquisaTelefoneclienteCompleted);
                ServicoCadastroCliente.PesquisaTelefoneClienteAsync(CodCli);

                

            }
        }
        private void PesquisaTelefoneclienteCompleted(object sender, PesquisaTelefoneClienteCompletedEventArgs e)
        {
            ServicoCadastroCliente.CloseAsync();

            if (this.DialogResult != false)
            {
                if (e.Error != null)
                {
                    biCarregando.IsBusy = false;

                    FrmTentativa frmTentativaListarDadosFormularioCliente = new FrmTentativa("Ocorreu um problema ao Pesquisar Telefone cliente", e.Error.Message.ToString(), e.Error.ToString(), "", typeof(Busca_Dados_Cadastro_ClienteSoapClient), "listaTipoTelefone", null);
                    frmTentativaListarDadosFormularioCliente.Closed += new EventHandler(frmTentativaListarDadosFormularioCliente_Closed);
                    frmTentativaListarDadosFormularioCliente.Show();

                    return;
                }

                ObservableCollection<Views.MvClienteTelEnd> fone = new ObservableCollection<Views.MvClienteTelEnd>();

                foreach (var item in e.Result)
                {
                    Views.MvClienteTelEnd Clitel = new Views.MvClienteTelEnd();
                    Clitel.Preenchendo = true;
                    Funcoes.CopyPropertyValues(item, Clitel, null);

                    Clitel.EstadoItem = WCFEstadoItem.Original;

                    Clitel.DDDIsEnabled = true;
                    Clitel.FoneIsEnabled = true;
                    Clitel.CodtptelIsEnabled = true;

                    Clitel.ListaObrigatorios.Add("Ddd");
                    Clitel.ListaObrigatorios.Add("Fone");
                    Clitel.ListaObrigatorios.Add("Codtptel");

                    Clitel.Ddd = item.Ddd;
                    Clitel.Fone = item.Fone.Replace("-", "");
                    Clitel.Codtptel = Convert.ToInt32(item.DESCRICAO);

                    Clitel.Preenchendo = false;
                    Clitel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(mv_Cliente_Tel_Tipo_PropertyChanged);

                    fone.Add(Clitel);
                }

                PagedCollectionView pcvClienteTelTipoRep = new PagedCollectionView(fone);

                if (pcvClienteTelTipoRep != null && pcvClienteTelTipoRep.CanFilter)
                    pcvClienteTelTipoRep.Filter = new Predicate<object>(FilterCompletedTelTipo);

                EndListaTelefone.ItemsSource = pcvClienteTelTipoRep;

            }

        }

        private void mv_Cliente_Tel_Tipo_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Ddd" || e.PropertyName == "Fone" || e.PropertyName == "Codtptel")
            {
                MvClienteTelTipo mv_Cliente_Tel_Tipo = (MvClienteTelTipo)sender;
                if (mv_Cliente_Tel_Tipo.EstadoItem != WCFEstadoItem.Deletado && mv_Cliente_Tel_Tipo.EstadoItem != WCFEstadoItem.Novo)
                    mv_Cliente_Tel_Tipo.EstadoItem = WCFEstadoItem.Modificado;

                if (!mv_Cliente_Tel_Tipo.Preenchendo)
                {
                    int qtdDDDPreenchido = 0, qtdFonePreenchido = 0, qtdDDDInvalido = 0, qtdFoneInvalido = 0;
                    MvClienteTelTipo mv_Cliente_Tel_Tipo_Temp = null;
                    PagedCollectionView pcvClienteTel = (PagedCollectionView)EndListaTelefone.ItemsSource;
                    IList<ValidacaoClienteSummaryItem> ListVSI = null;

                    var listaClienteTelCelular = ((ObservableCollection<MvClienteTelTipo>)pcvClienteTel.SourceCollection).Where(a => a.EstadoItem != WCFEstadoItem.Deletado).ToList<MvClienteTelTipo>();

                    for (int i = 0; i < listaClienteTelCelular.Count; i++)
                    {
                        mv_Cliente_Tel_Tipo_Temp = (MvClienteTelTipo)listaClienteTelCelular[i];

                        if (!string.IsNullOrWhiteSpace(mv_Cliente_Tel_Tipo_Temp.Ddd))
                        {
                            qtdDDDPreenchido++;
                            if (!VittaValidacao.ValidaDDD(mv_Cliente_Tel_Tipo_Temp.Ddd))
                                qtdDDDInvalido++;
                        }

                        if (!string.IsNullOrWhiteSpace(mv_Cliente_Tel_Tipo_Temp.Fone))
                        {
                            qtdFonePreenchido++;
                            if (!VittaValidacao.ValidaNumeroTelefoneCelular(mv_Cliente_Tel_Tipo_Temp.Fone))
                                qtdFoneInvalido++;
                        }
                    }

                    if (e.PropertyName == "Ddd")
                    {
                        if (!string.IsNullOrWhiteSpace(mv_Cliente_Tel_Tipo.Ddd))
                        {
                            if (qtdDDDPreenchido == listaClienteTelCelular.Count)
                            {
                                ListVSI = Validacao.Where(a => a.Campo == CampoInvalido.TelRepDDDNaoInformado).ToList();
                                if (ListVSI.Count > 0)
                                {
                                    ExcluirValidacaoCampo(ListVSI[0]);
                                }
                            }

                            if (qtdDDDInvalido == 0)
                            {
                                ListVSI = Validacao.Where(a => a.Campo == CampoInvalido.TelRepDDDInvalido).ToList();
                                if (ListVSI.Count > 0)
                                {
                                    ExcluirValidacaoCampo(ListVSI[0]);
                                }
                            }
                        }
                    }
                    else if (e.PropertyName == "Fone")
                    {
                        if (!string.IsNullOrWhiteSpace(mv_Cliente_Tel_Tipo.Fone))
                        {
                            if (qtdFonePreenchido == listaClienteTelCelular.Count)
                            {
                                ListVSI = Validacao.Where(a => a.Campo == CampoInvalido.TelRepFoneNaoInformado).ToList();
                                if (ListVSI.Count > 0)
                                {
                                    ExcluirValidacaoCampo(ListVSI[0]);
                                }
                            }

                            if (qtdFoneInvalido == 0)
                            {
                                ListVSI = Validacao.Where(a => a.Campo == CampoInvalido.TelRepFoneInvalido).ToList();
                                if (ListVSI.Count > 0)
                                {
                                    ExcluirValidacaoCampo(ListVSI[0]);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ExcluirValidacaoCampo(ValidacaoClienteSummaryItem pValidacaoCampo)
        {
            var listaRemover = vsValidaCampos.Errors.Where(item => item == pValidacaoCampo.ItemValidationSummary).ToList();

            foreach (var item in listaRemover)
                vsValidaCampos.Errors.Remove(item);

            Validacao.RemoveAll(item => item == pValidacaoCampo);

            AtualizaHeaderValidationSummary();
        }

        private bool FilterCompletedTelTipo(object t)
        {
            MvClienteTelEnd mv_Cliente_Tel_Tipo = t as MvClienteTelEnd;
            return (mv_Cliente_Tel_Tipo.EstadoItem == WCFEstadoItem.Novo) || (mv_Cliente_Tel_Tipo.EstadoItem == WCFEstadoItem.Modificado)
                   || (mv_Cliente_Tel_Tipo.EstadoItem == WCFEstadoItem.Original);
        }

        private void PesquisaCadastroClienteCompleted(object sender, PesquisaCadastroClienteCompletoCompletedEventArgs e)
        {
            ServicoCadastroCliente.CloseAsync();

            if (this.DialogResult != false)
            {
                if (e.Error != null)
                {
                    biCarregando.IsBusy = false;

                    FrmTentativa frmTentativaListarDadosFormularioCliente = new FrmTentativa("Ocorreu um problema ao incluir cliente", e.Error.Message.ToString(), e.Error.ToString(), "", typeof(Busca_Dados_Cadastro_ClienteSoapClient), "listaTipoTelefone", null);
                    frmTentativaListarDadosFormularioCliente.Closed += new EventHandler(frmTentativaListarDadosFormularioCliente_Closed);
                    frmTentativaListarDadosFormularioCliente.Show();

                    return;
                }

                // Caso não tenha nelhuma exceção no sistema ele ira executar o seguinte codigo.

                TipoCamposCadPaciente DadosCadCli = e.Result;

                txtNome.Text = DadosCadCli.NOME;
                txtCPF.Text = DadosCadCli.CPF_CNPJ;
                NumDoc.Text = DadosCadCli.NUMEROREGISTRO;
                cmbOEX.SelectedValue = DadosCadCli.Emissor;
                ufEmisao.SelectedValue = DadosCadCli.Uf_Emissao;
                cmbSexo.SelectedValue = DadosCadCli.Sexo;
                cmbCivil.SelectedValue = DadosCadCli.Estado_Civil;
                txtDATA_nasc.Text = DadosCadCli.Data_Nascimento;
                txtMae.Text = DadosCadCli.Nome_Mae;
                txtPai.Text = DadosCadCli.Nome_Pai;
                txtCEP.Text = DadosCadCli.Cep;
                CmbUF.Text = DadosCadCli.Uf;
                CmbCIDADE.Text = DadosCadCli.Cidade;
                CmbBAIRRO.Text = DadosCadCli.Bairro;
                txtREFERENCIA.Text = DadosCadCli.Ponto_Referencia;
                CmbTPLOGRADOURO.Text = DadosCadCli.Tipo_Logradouro;
                txtEND.Text = DadosCadCli.Desc_Logradouro;
                txtNUM.Text = DadosCadCli.End_Num;
                txtCOMPLEMENTO.Text = DadosCadCli.Desc_Complemento;
                txtEmail.Text = DadosCadCli.Email;
                cmbTpDOC.SelectedValue = DadosCadCli.Tipo_Doc;
                qtDependentes.Text = DadosCadCli.Qt_Dependentes;
                txtProfissao.Text = DadosCadCli.Profissao;
                
                //Ações de Botões de formlurio.
                btnNovo.IsEnabled = false;
                btnCancelar.IsEnabled = false;
                btnLimpar.IsEnabled = false;
                btnGravar.IsEnabled = false;
                btnAlterar.IsEnabled = true;
                btnFechar.IsEnabled = true;

                biCarregando.IsBusy = false;

            }
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            biCarregando.IsBusy = true;
            GeraCodCli();
            ControleCamposHabiDesab(true);
            btnNovo.IsEnabled = false;
            btnLimpar.IsEnabled = false;
            btnAlterar.IsEnabled = false;
            btnCancelar.IsEnabled = true;
            btnGravar.IsEnabled = true;
            txtCPF.Focus();
            biCarregando.IsBusy = false;
        }
       
        private void GeraCodCli()
        {
           
            ServicoCadastroCliente.GerarCodCliCompleted += new EventHandler<GerarCodCliCompletedEventArgs>(GerarCodClienteCompleted);
            ServicoCadastroCliente.GerarCodCliAsync();
        }
        private void GerarCodClienteCompleted(object sender, GerarCodCliCompletedEventArgs e)
        {

            if (e.Error != null)
            {
                biCarregando.IsBusy = false;

                FrmTentativa frmTentativaListarDadosFormularioCliente = new FrmTentativa("Ocorreu um problema ao carregar os dados de preenchimento do formulário", e.Error.Message.ToString(), e.Error.ToString(), "", typeof(Busca_Dados_Cadastro_ClienteSoapClient), "listaTipoTelefone", null);
                frmTentativaListarDadosFormularioCliente.Closed += new EventHandler(frmTentativaListarDadosFormularioCliente_Closed);
                frmTentativaListarDadosFormularioCliente.Show();

                return;
            }
            CodCli = e.Result;
        }

        public string CodCli { get; set; }

        private void btnAlterar_Click(object sender, RoutedEventArgs e)
        {

            btnGravar.IsEnabled = true;
            btnCancelar.IsEnabled = true;
            btnLimpar.IsEnabled = true;
            ControleCamposHabiDesab(true);
            btnAlterar.IsEnabled = false;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = Mensagens.Confirmacao("Deseja realmente Cancelar?", "Cadastro de Cliente");

            if (result == MessageBoxResult.OK)
            {
                ControleCamposHabiDesab(false);
                btnNovo.IsEnabled = true;
                btnLimpar.IsEnabled = false;
                btnAlterar.IsEnabled = false;
                btnCancelar.IsEnabled = false;
                btnGravar.IsEnabled = false;
                CodCli = null;
                LimpaCamposFormulario();
                this.Close();

            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = Mensagens.Confirmacao("Deseja realmente EXCLUIR esse cadastro?", "Cadastro de Cliente");

            if (result == MessageBoxResult.OK)
            {
                ServiceCadcliente.ExcluirCadastroClienteCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ExclusaoCadastroClienteCompleted);
                ServiceCadcliente.ExcluirCadastroClienteAsync(CodCli);
            }
        }
        private void ExclusaoCadastroClienteCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (this.DialogResult != false)
            {
                if (e.Error != null)
                {
                    biCarregando.IsBusy = false;

                    FrmTentativa frmTentativaListarDadosFormularioCliente = new FrmTentativa("Ocorreu um problema ao excluir os dados do formulário", e.Error.Message.ToString(), e.Error.ToString(), "", typeof(PesquisaCEPSoapClient), "listaDadosCadastroCliente", null);
                    frmTentativaListarDadosFormularioCliente.Closed += new EventHandler(frmTentativaListarDadosFormularioCliente_Closed);
                    frmTentativaListarDadosFormularioCliente.Show();

                    return;
                }

                Mensagens.Informacao("Cadastro de Cliente Excluido com Sucesso!", "Cadastro de Cliente");
                this.Close();

            }
        }

        private void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            Valida_CamposFormulario();
        }

        private void Valida_CamposFormulario()
        {
            Funcoes.ClearControlError(txtNome);
            Funcoes.ClearControlError(cmbSexo);


            if (txtNome.Text == "" || txtNome.Text == null)
            {
                Funcoes.SetControlError(txtNome, "Nome do paciente não foi informado!", true, true);
                return;
            }

            if (cmbSexo.SelectedValue == null)
            {
                Funcoes.SetControlError(cmbSexo, "Tipo de Sexo do paciente não foi informado!", true, true);
                return;
            }           



            #region Validação de transformação de campo num para vazio

            if (cmbTpDOC.SelectedValue != null)
            {
                cmbtpDOC = cmbTpDOC.SelectedValue.ToString();
            }
            else
                cmbtpDOC = null;



            if (cmbOEX.SelectedValue != null)
            {
                cmboex = cmbOEX.SelectedValue.ToString();
            }
            else
                cmboex = null;


            if (ufEmisao.SelectedValue != null)
            {
                UfEmisao = ufEmisao.SelectedValue.ToString();
            }
            else
                UfEmisao = null;



            if (cmbSexo.SelectedValue != null)
            {
                cmbsexo = cmbSexo.SelectedValue.ToString();
            }
            else
                cmbsexo = null;


            if (cmbCivil.SelectedValue != null)
            {
                cmbcivil = cmbCivil.SelectedValue.ToString();

            }
            else
                cmbcivil = null;


            #endregion

            if (TipoCadastro == 1 || TipoCadastro == 3) //  incluir um novo cadastro
            {
                GravaCadastroCliente();
            }
            else // alterarando cadastro de cliente
            {
                AlterarCadastrodeCliente();
            }


        }
        ServiceControleCadCliente.ControleDb_CadastroClienteSoapClient ServiceCadcliente = new ServiceControleCadCliente.ControleDb_CadastroClienteSoapClient();
        ServiceCadastroCliente.Busca_Dados_Cadastro_ClienteSoapClient ServicoCadcliente = new Busca_Dados_Cadastro_ClienteSoapClient();
        private void AlterarCadastrodeCliente()
        {
            biCarregando.IsBusy = true;


            //percorre o data grid de telefone
            if (EndListaTelefone.ItemsSource != null)
            {
                foreach (dynamic item in EndListaTelefone.ItemsSource)
                {
                    // passa os parametros de dados para serem gravados ao banco de dados
                    ServiceCadcliente.Alterar_TELEFONEAsync(CodCli, Convert.ToString(item.Codtptel), item.Ddd, item.Fone);

                }

                if (EndListaTelefone.SelectedIndex < 0 || EndListaTelefone.SelectedIndex == -1)
                {

                    ServiceCadcliente.EXCLUIR_TELEFONEAsync(CodCli);
                }
            }
            else
            {
                ServiceCadcliente.EXCLUIR_TELEFONEAsync(CodCli);
            }


            // referenciação de retorno de comando incluir dados cabeçalho
            ServiceCadcliente.AlterarCadastroClienteCabecalhoCompleted += new EventHandler<ServiceControleCadCliente.AlterarCadastroClienteCabecalhoCompletedEventArgs>(clienteServiceClient_AlterarClienteCompleted);

            // paramentros de incluir dados de cabeçalho.
            ServiceCadcliente.AlterarCadastroClienteCabecalhoAsync(CodCli, txtNome.Text.Trim(), txtCPF.Text.Trim(), null, cmboex, UfEmisao, cmbsexo, cmbcivil, txtMae.Text.Trim(), txtPai.Text.Trim(),CmbUF.Text.Trim(), CmbCIDADE.Text.Trim(), null, CmbBAIRRO.Text.Trim(), txtREFERENCIA.Text.Trim(), CmbTPLOGRADOURO.Text.Trim(), txtNUM.Text.Trim(), txtEND.Text.Trim(), CodUsuario, txtEmail.Text.Trim(), cmbtpDOC, qtDependentes.Text.Trim(), "001", txtDATA_nasc.Text.Trim(), txtProfissao.Text.Trim(), txtCEP.Text.Trim(), DataAtual);

        }
        private void clienteServiceClient_AlterarClienteCompleted(object sender, AlterarCadastroClienteCabecalhoCompletedEventArgs e)
        {
            if (this.DialogResult != false)
            {
                if (e.Error != null)
                {
                    biCarregando.IsBusy = false;

                    FrmTentativa frmTentativaListarDadosFormularioCliente = new FrmTentativa("Ocorreu um problema ao incluir cliente", e.Error.Message.ToString(), e.Error.ToString(), "", typeof(Busca_Dados_Cadastro_ClienteSoapClient), "listaTipoTelefone", null);
                    frmTentativaListarDadosFormularioCliente.Closed += new EventHandler(frmTentativaListarDadosFormularioCliente_Closed);
                    frmTentativaListarDadosFormularioCliente.Show();

                    return;
                }

                int pRetorno = e.Result;
                if (pRetorno == 1)
                {
                    Mensagens.Informacao("Cadastro de Cliente Alterado com Sucesso!", "Cadastro de Cliente");
                    biCarregando.IsBusy = false;
                    this.Close();
                }



            }
        }


        private void GravaCadastroCliente()
        {
            biCarregando.IsBusy = true;


            //percorre o data grid de telefone
            if (EndListaTelefone.ItemsSource != null)
            {
                foreach (dynamic item in EndListaTelefone.ItemsSource)
                {
                    // passa os parametros de dados para serem gravados ao banco de dados
                    ServiceCadcliente.incluir_TELEFONEAsync(CodCli, Convert.ToString(item.Codtptel), item.Ddd, item.Fone);

                }
            }


            // referenciação de retorno de comando incluir dados cabeçalho
            ServiceCadcliente.IncluirCadastroClienteCabecalhoCompleted += new EventHandler<ServiceControleCadCliente.IncluirCadastroClienteCabecalhoCompletedEventArgs>(clienteServiceClient_incluirClienteCompleted);

            // paramentros de incluir dados de cabeçalho.
            ServiceCadcliente.IncluirCadastroClienteCabecalhoAsync(CodCli, txtNome.Text.Trim(), txtCPF.Text.Trim(), null, cmboex, UfEmisao, cmbsexo, cmbcivil, txtMae.Text.Trim(), txtPai.Text.Trim(), CmbUF.Text.Trim(), CmbCIDADE.Text.Trim(), null, CmbBAIRRO.Text.Trim(), txtREFERENCIA.Text.Trim(), CmbTPLOGRADOURO.Text.Trim(), txtNUM.Text.Trim(), txtEND.Text.Trim(), CodUsuario, txtEmail.Text.Trim(), cmbtpDOC, qtDependentes.Text.Trim(), "001", txtDATA_nasc.Text.Trim(), txtProfissao.Text.Trim(), txtCEP.Text.Trim(), DataAtual);


        }
        private void clienteServiceClient_incluirClienteCompleted(object sender, ServiceControleCadCliente.IncluirCadastroClienteCabecalhoCompletedEventArgs e)
        {
            ServiceControleCadCliente.ControleDb_CadastroClienteSoapClient servicecliente = (ServiceControleCadCliente.ControleDb_CadastroClienteSoapClient)sender;
            servicecliente.CloseAsync();

            if (this.DialogResult != false)
            {
                if (e.Error != null)
                {
                    biCarregando.IsBusy = false;

                    FrmTentativa frmTentativaListarDadosFormularioCliente = new FrmTentativa("Ocorreu um problema ao incluir cliente", e.Error.Message.ToString(), e.Error.ToString(), "", typeof(Busca_Dados_Cadastro_ClienteSoapClient), "listaTipoTelefone", null);
                    frmTentativaListarDadosFormularioCliente.Closed += new EventHandler(frmTentativaListarDadosFormularioCliente_Closed);
                    frmTentativaListarDadosFormularioCliente.Show();

                    return;
                }

                int pRetorno = e.Result;
                if (pRetorno == 1)
                {
                    Mensagens.Informacao("Cadastro de Cliente Gravado com Sucesso!", "Cadastro de Cliente");
                    biCarregando.IsBusy = false;
                    this.Close();
                    if (TipoCadastro == 3)// se o tipo informado for 3 então ira retornar que esta cadastrado para poder iniciar o forms de processo.
                    {
                        this.Close();

                    }
                }
                else if (pRetorno == 90)
                {

                    Mensagens.Informacao("Esse cliente já possue cadastro no sistema, por favor verifique!", "Cadastro de Cliente");
                    biCarregando.IsBusy = false;
                }


            }
        }

   
    }
}

