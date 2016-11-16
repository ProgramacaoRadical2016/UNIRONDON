using ClinVitta.Classes;
using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ClinVitta.Views
{
    public partial class MvClienteTelTipo : INotifyPropertyChanged
    {
        private void InicializaListaLabels()
        {
            ListaLabelPropriedade.Add("Ddd", "ddd");
            ListaLabelPropriedade.Add("Fone", "número do telefone");
            ListaLabelPropriedade.Add("Codtptel", "tipo de telefone");
        }

        private bool _dddisenabled;
        public bool DDDIsEnabled
        {
            get { return _dddisenabled; }
            set
            {
                if (_dddisenabled != value)
                {
                    _dddisenabled = value;
                    OnPropertyChanged("DDDIsEnabled");
                }
            }
        }

        private bool _foneisenabled;
        public bool FoneIsEnabled
        {
            get { return _foneisenabled; }
            set
            {
                if (_foneisenabled != value)
                {
                    _foneisenabled = value;
                    OnPropertyChanged("FoneIsEnabled");
                }
            }

        }

        private bool _codtptelisenabled;
        public bool CodtptelIsEnabled
        {
            get { return _codtptelisenabled; }
            set
            {
                if (_codtptelisenabled != value)
                {
                    _codtptelisenabled = value;
                    OnPropertyChanged("CodtptelIsEnabled");
                }
            }
        }

        public string Desc_tipo_tel { get; set; }

        private void ValidaDdd(string pDdd)
        {
            if (!string.IsNullOrWhiteSpace(pDdd))
            {
                if (!VittaValidacao.ValidaDDD(pDdd))
                    throw new Exception("O ddd informado é inválido.");
            }
        }

        private void ValidaFone(string pFone)
        {
            if (!string.IsNullOrWhiteSpace(pFone))
            {
                if (Tipo == 3)
                {
                    if (!VittaValidacao.ValidaNumeroCelular(pFone))
                        throw new Exception("O número do celular informado é inválido.");
                }
                else
                {
                    if (Codtptel == 6)
                    {
                        if (!VittaValidacao.ValidaNumeroCelular(pFone))
                            throw new Exception("O número do celular informado é inválido.");
                    }
                    else if (!VittaValidacao.ValidaNumeroTelefoneCelular(pFone))
                        throw new Exception("O número do telefone informado é inválido.");
                }

            }
        }
    }
}