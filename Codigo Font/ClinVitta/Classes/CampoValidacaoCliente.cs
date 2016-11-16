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
    public class CampoValidacaoCliente
    {
        public string NomeCampo { get; set; }
        public TabelaValidacao Tabela { get; set; }
        public int? TipoVenda { get; set; }
        public bool CadastroNovo { get; set; }
        public bool CadastroAlterar { get; set; }
        public string TipoPessoa { get; set; }

        public CampoValidacaoCliente(string pNomeCampo, TabelaValidacao pTabela, int? pTipoVenda, bool pCadastroNovo, bool pCadastroAlterar, string pTipoPessoa)
        {
            NomeCampo = pNomeCampo;
            Tabela = pTabela;
            TipoVenda = pTipoVenda;
            CadastroNovo = pCadastroNovo;
            CadastroAlterar = pCadastroAlterar;
            TipoPessoa = pTipoPessoa;
        }
    }
}