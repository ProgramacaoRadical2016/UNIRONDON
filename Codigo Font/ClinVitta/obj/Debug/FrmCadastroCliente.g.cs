﻿#pragma checksum "C:\inetpub\HML\ClinVitta\ClinVitta\FrmCadastroCliente.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "79D60F37F7E340F00ABB1E0CE19F8F63"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace ClinVitta {
    
    
    public partial class FrmCadastroCliente : System.Windows.Controls.ChildWindow {
        
        internal System.Windows.Controls.BusyIndicator biCarregando;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.ValidationSummary vsValidaCampos;
        
        internal System.Windows.Controls.Button btnNovo;
        
        internal System.Windows.Controls.Button btnAlterar;
        
        internal System.Windows.Controls.Button btnGravar;
        
        internal System.Windows.Controls.Button btnCancelar;
        
        internal System.Windows.Controls.Button btnLimpar;
        
        internal System.Windows.Controls.Button btnFechar;
        
        internal System.Windows.Shapes.Rectangle _null;
        
        internal System.Windows.Controls.TextBox txtNome;
        
        internal System.Windows.Controls.TextBox txtCPF;
        
        internal System.Windows.Controls.ComboBox cmbSexo;
        
        internal System.Windows.Controls.TextBox txtDATA_nasc;
        
        internal System.Windows.Controls.TextBox qtDependentes;
        
        internal System.Windows.Controls.ComboBox cmbCivil;
        
        internal System.Windows.Controls.ComboBox cmbTpDOC;
        
        internal System.Windows.Controls.TextBox NumDoc;
        
        internal System.Windows.Controls.ComboBox ufEmisao;
        
        internal System.Windows.Controls.ComboBox cmbOEX;
        
        internal System.Windows.Controls.TextBox txtProfissao;
        
        internal System.Windows.Controls.ComboBox cbTipoTelefone;
        
        internal System.Windows.Controls.TextBox txtdddtel;
        
        internal System.Windows.Controls.TextBox txtNumerotel;
        
        internal System.Windows.Controls.Button btnRemTelefoneResidencialPF;
        
        internal System.Windows.Controls.Button btnAddTelResidencialPF;
        
        internal System.Windows.Controls.DataGrid EndListaTelefone;
        
        internal System.Windows.Controls.TextBox txtMae;
        
        internal System.Windows.Controls.TextBox txtPai;
        
        internal System.Windows.Controls.TextBox txtCEP;
        
        internal System.Windows.Controls.Button btnCEP;
        
        internal System.Windows.Controls.Button btnPesquisaCep;
        
        internal System.Windows.Controls.Button btnEditarEnd;
        
        internal System.Windows.Controls.TextBox CmbUF;
        
        internal System.Windows.Controls.TextBox CmbCIDADE;
        
        internal System.Windows.Controls.TextBox CmbBAIRRO;
        
        internal System.Windows.Controls.TextBox CmbTPLOGRADOURO;
        
        internal System.Windows.Controls.TextBox txtEND;
        
        internal System.Windows.Controls.TextBox txtNUM;
        
        internal System.Windows.Controls.TextBox txtCOMPLEMENTO;
        
        internal System.Windows.Controls.TextBox txtREFERENCIA;
        
        internal System.Windows.Controls.TextBox txtEmail;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/ClinVitta;component/FrmCadastroCliente.xaml", System.UriKind.Relative));
            this.biCarregando = ((System.Windows.Controls.BusyIndicator)(this.FindName("biCarregando")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.vsValidaCampos = ((System.Windows.Controls.ValidationSummary)(this.FindName("vsValidaCampos")));
            this.btnNovo = ((System.Windows.Controls.Button)(this.FindName("btnNovo")));
            this.btnAlterar = ((System.Windows.Controls.Button)(this.FindName("btnAlterar")));
            this.btnGravar = ((System.Windows.Controls.Button)(this.FindName("btnGravar")));
            this.btnCancelar = ((System.Windows.Controls.Button)(this.FindName("btnCancelar")));
            this.btnLimpar = ((System.Windows.Controls.Button)(this.FindName("btnLimpar")));
            this.btnFechar = ((System.Windows.Controls.Button)(this.FindName("btnFechar")));
            this._null = ((System.Windows.Shapes.Rectangle)(this.FindName("_null")));
            this.txtNome = ((System.Windows.Controls.TextBox)(this.FindName("txtNome")));
            this.txtCPF = ((System.Windows.Controls.TextBox)(this.FindName("txtCPF")));
            this.cmbSexo = ((System.Windows.Controls.ComboBox)(this.FindName("cmbSexo")));
            this.txtDATA_nasc = ((System.Windows.Controls.TextBox)(this.FindName("txtDATA_nasc")));
            this.qtDependentes = ((System.Windows.Controls.TextBox)(this.FindName("qtDependentes")));
            this.cmbCivil = ((System.Windows.Controls.ComboBox)(this.FindName("cmbCivil")));
            this.cmbTpDOC = ((System.Windows.Controls.ComboBox)(this.FindName("cmbTpDOC")));
            this.NumDoc = ((System.Windows.Controls.TextBox)(this.FindName("NumDoc")));
            this.ufEmisao = ((System.Windows.Controls.ComboBox)(this.FindName("ufEmisao")));
            this.cmbOEX = ((System.Windows.Controls.ComboBox)(this.FindName("cmbOEX")));
            this.txtProfissao = ((System.Windows.Controls.TextBox)(this.FindName("txtProfissao")));
            this.cbTipoTelefone = ((System.Windows.Controls.ComboBox)(this.FindName("cbTipoTelefone")));
            this.txtdddtel = ((System.Windows.Controls.TextBox)(this.FindName("txtdddtel")));
            this.txtNumerotel = ((System.Windows.Controls.TextBox)(this.FindName("txtNumerotel")));
            this.btnRemTelefoneResidencialPF = ((System.Windows.Controls.Button)(this.FindName("btnRemTelefoneResidencialPF")));
            this.btnAddTelResidencialPF = ((System.Windows.Controls.Button)(this.FindName("btnAddTelResidencialPF")));
            this.EndListaTelefone = ((System.Windows.Controls.DataGrid)(this.FindName("EndListaTelefone")));
            this.txtMae = ((System.Windows.Controls.TextBox)(this.FindName("txtMae")));
            this.txtPai = ((System.Windows.Controls.TextBox)(this.FindName("txtPai")));
            this.txtCEP = ((System.Windows.Controls.TextBox)(this.FindName("txtCEP")));
            this.btnCEP = ((System.Windows.Controls.Button)(this.FindName("btnCEP")));
            this.btnPesquisaCep = ((System.Windows.Controls.Button)(this.FindName("btnPesquisaCep")));
            this.btnEditarEnd = ((System.Windows.Controls.Button)(this.FindName("btnEditarEnd")));
            this.CmbUF = ((System.Windows.Controls.TextBox)(this.FindName("CmbUF")));
            this.CmbCIDADE = ((System.Windows.Controls.TextBox)(this.FindName("CmbCIDADE")));
            this.CmbBAIRRO = ((System.Windows.Controls.TextBox)(this.FindName("CmbBAIRRO")));
            this.CmbTPLOGRADOURO = ((System.Windows.Controls.TextBox)(this.FindName("CmbTPLOGRADOURO")));
            this.txtEND = ((System.Windows.Controls.TextBox)(this.FindName("txtEND")));
            this.txtNUM = ((System.Windows.Controls.TextBox)(this.FindName("txtNUM")));
            this.txtCOMPLEMENTO = ((System.Windows.Controls.TextBox)(this.FindName("txtCOMPLEMENTO")));
            this.txtREFERENCIA = ((System.Windows.Controls.TextBox)(this.FindName("txtREFERENCIA")));
            this.txtEmail = ((System.Windows.Controls.TextBox)(this.FindName("txtEmail")));
        }
    }
}

