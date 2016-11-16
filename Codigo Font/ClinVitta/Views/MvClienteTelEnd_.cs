using System;
using System.Net;
using System.Windows;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using ClinVitta.Classes;


namespace ClinVitta.Views
{
    public partial class MvClienteTelEnd : INotifyPropertyChanged
    {
        private void InicializaListaLabels()
        {
            ListaLabelPropriedade.Add("Ddd", "ddd");
            ListaLabelPropriedade.Add("Fone", "número do telefone");
            ListaLabelPropriedade.Add("Codtptel", "tipo do telefone");
        }

        public bool DDDIsEnabled { get; set; }
        public bool FoneIsEnabled { get; set; }
        public bool CodtptelIsEnabled { get; set; }

        private void ValidaDdd(string pDdd)
        {
            if (!VittaValidacao.ValidaDDD(pDdd))
                throw new Exception("O ddd informado é inválido.");
        }

        private void ValidaFone(string pFone)
        {
            if (!VittaValidacao.ValidaNumeroTelefoneCelular(pFone))
                throw new Exception("O número do telefone informado é inválido.");
        }
    }
}