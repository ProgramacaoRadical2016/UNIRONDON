﻿#pragma checksum "C:\campeonato\programacaoradical\ClinVitta\ClinVitta\FrmTentativa.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E1DD4EA5D078CE5BB4A9AB0A040C5AD7"
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
    
    
    public partial class FrmTentativa : System.Windows.Controls.ChildWindow {
        
        internal System.Windows.Controls.BusyIndicator biCarregando;
        
        internal System.Windows.Controls.TextBlock tbMensagem;
        
        internal System.Windows.Controls.TextBox txtDetalhe;
        
        internal System.Windows.Controls.Button btnTentarNovamente;
        
        internal System.Windows.Controls.Button CancelButton;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/ClinVitta;component/FrmTentativa.xaml", System.UriKind.Relative));
            this.biCarregando = ((System.Windows.Controls.BusyIndicator)(this.FindName("biCarregando")));
            this.tbMensagem = ((System.Windows.Controls.TextBlock)(this.FindName("tbMensagem")));
            this.txtDetalhe = ((System.Windows.Controls.TextBox)(this.FindName("txtDetalhe")));
            this.btnTentarNovamente = ((System.Windows.Controls.Button)(this.FindName("btnTentarNovamente")));
            this.CancelButton = ((System.Windows.Controls.Button)(this.FindName("CancelButton")));
        }
    }
}

