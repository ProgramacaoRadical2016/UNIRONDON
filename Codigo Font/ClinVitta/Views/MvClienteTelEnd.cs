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
        public List<string> ListaObrigatorios { get; set; }
        private Dictionary<string, string> ListaLabelPropriedade = new Dictionary<string, string>();
        public string MensagemObrigatorio = "é obrigatório.";
        public string MensagemNullVazioEmBrancoPreenchido = "é obrigatório.";

        public MvClienteTelEnd()
        {
            ListaObrigatorios = new List<string>();
            InicializaListaLabels();
        }

        public bool Preenchendo { get; set; }

        private Int64? _codclitelend;
        public Int64? Codclitelend
        {
            get { return _codclitelend; }
            set
            {
                if (_codclitelend != value)
                {
                    _codclitelend = value;
                    OnPropertyChanged("Codclitelend");
                }

                if (VerificaObrigatorio("Codclitelend") && Codclitelend == null)
                    throw new Exception("O campo " + RetornaLabelPropriedade("Codclitelend") + " " + MensagemObrigatorio);
                if (Codclitelend == null && CodclitelendPreenchido)
                    throw new Exception("O campo " + RetornaLabelPropriedade("Codclitelend") + " " + MensagemNullVazioEmBrancoPreenchido);
            }
        }
        public bool CodclitelendPreenchido { get; set; }

        private Int64? _codcliente;
        public Int64? Codcliente
        {
            get { return _codcliente; }
            set
            {
                if (_codcliente != value)
                {
                    _codcliente = value;
                    OnPropertyChanged("Codcliente");
                }

                if (VerificaObrigatorio("Codcliente") && Codcliente == null)
                    throw new Exception("O campo " + RetornaLabelPropriedade("Codcliente") + " " + MensagemObrigatorio);
                if (Codcliente == null && CodclientePreenchido)
                    throw new Exception("O campo " + RetornaLabelPropriedade("Codcliente") + " " + MensagemNullVazioEmBrancoPreenchido);
            }
        }
        public bool CodclientePreenchido { get; set; }

        private Int64? _codend;
        public Int64? Codend
        {
            get { return _codend; }
            set
            {
                if (_codend != value)
                {
                    _codend = value;
                    OnPropertyChanged("Codend");
                }

                if (VerificaObrigatorio("Codend") && Codend == null)
                    throw new Exception("O campo " + RetornaLabelPropriedade("Codend") + " " + MensagemObrigatorio);
                if (Codend == null && CodendPreenchido)
                    throw new Exception("O campo " + RetornaLabelPropriedade("Codend") + " " + MensagemNullVazioEmBrancoPreenchido);
            }
        }
        public bool CodendPreenchido { get; set; }

        private string _ddd;
        public string Ddd
        {
            get { return _ddd; }
            set
            {
                if (_ddd != value)
                {
                    _ddd = value;
                    OnPropertyChanged("Ddd");
                }

                if (!Preenchendo)
                {
                    if (VerificaObrigatorio("Ddd") && string.IsNullOrWhiteSpace(value))
                        throw new Exception("O campo " + RetornaLabelPropriedade("Ddd") + " " + MensagemObrigatorio);
                    if (string.IsNullOrWhiteSpace(value) && DddPreenchido)
                        throw new Exception("O campo " + RetornaLabelPropriedade("Ddd") + " " + MensagemNullVazioEmBrancoPreenchido);

                    ValidaDdd(value);
                }
            }
        }
        public bool DddPreenchido { get; set; }

        private string _fone;
        public string Fone
        {
            get { return _fone; }
            set
            {
                if (_fone != value)
                {
                    _fone = value;
                    OnPropertyChanged("Fone");
                }

                if (!Preenchendo)
                {
                    if (VerificaObrigatorio("Fone") && string.IsNullOrWhiteSpace(value))
                        throw new Exception("O campo " + RetornaLabelPropriedade("Fone") + " " + MensagemObrigatorio);
                    if (string.IsNullOrWhiteSpace(value) && FonePreenchido)
                        throw new Exception("O campo " + RetornaLabelPropriedade("Fone") + " " + MensagemNullVazioEmBrancoPreenchido);

                    ValidaFone(value);
                }
            }
        }
        public bool FonePreenchido { get; set; }

        private Int64? _codtptel;
        public Int64? Codtptel
        {
            get { return _codtptel; }
            set
            {
                if (_codtptel != value)
                {
                    _codtptel = value;
                    OnPropertyChanged("Codtptel");
                }

                if (!Preenchendo)
                {
                    if (VerificaObrigatorio("Codtptel") && Codtptel == null)
                        throw new Exception("O campo " + RetornaLabelPropriedade("Codtptel") + " " + MensagemObrigatorio);
                    if (Codtptel == null && CodtptelPreenchido)
                        throw new Exception("O campo " + RetornaLabelPropriedade("Codtptel") + " " + MensagemNullVazioEmBrancoPreenchido);
                }
            }
        }
        public bool CodtptelPreenchido { get; set; }

        public WCFEstadoItem EstadoItem { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(
                    this,
                    new System.ComponentModel.PropertyChangedEventArgs(propertyName));

            }
        }

        public string RetornaLabelPropriedade(string pPropriedade)
        {
            string saida = "";
            ListaLabelPropriedade.TryGetValue(pPropriedade, out saida);
            return saida != null ? saida : "";
        }

        public bool VerificaObrigatorio(string pProperty)
        {
            if (ListaObrigatorios.Where(a => a == pProperty).ToList().Count > 0)
                return true;
            return false;
        }

        public void MarcaPreenchido()
        {
            if (Codclitelend != null)
                CodclitelendPreenchido = true;
            else
                CodclitelendPreenchido = false;

            if (Codcliente != null)
                CodclientePreenchido = true;
            else
                CodclientePreenchido = false;

            if (Codend != null)
                CodendPreenchido = true;
            else
                CodendPreenchido = false;

            if (!string.IsNullOrWhiteSpace(Ddd))
                DddPreenchido = true;
            else
                DddPreenchido = false;

            if (!string.IsNullOrWhiteSpace(Fone))
                FonePreenchido = true;
            else
                FonePreenchido = false;

            if (Codtptel != null)
                CodtptelPreenchido = true;
            else
                CodtptelPreenchido = false;

        }
    }
}