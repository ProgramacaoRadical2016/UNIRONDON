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
    public partial class MvClienteTelTipo : INotifyPropertyChanged
    {
        public List<string> ListaObrigatorios { get; set; }
        private Dictionary<string, string> ListaLabelPropriedade = new Dictionary<string, string>();
        public string MensagemObrigatorio = "é obrigatório.";
        public string MensagemNullVazioEmBrancoPreenchido = "é obrigatório.";

        public MvClienteTelTipo()
        {
            ListaObrigatorios = new List<string>();
            InicializaListaLabels();
            EstadoItem = WCFEstadoItem.Original;
        }

        public bool Preenchendo { get; set; }

        private Int64? _codcliteltipo;
        public Int64? Codcliteltipo
        {
            get { return _codcliteltipo; }
            set
            {
                if (_codcliteltipo != value)
                {
                    _codcliteltipo = value;
                    OnPropertyChanged("Codcliteltipo");
                }

                if (VerificaObrigatorio("Codcliteltipo") && Codcliteltipo == null)
                    throw new Exception("O campo " + RetornaLabelPropriedade("Codcliteltipo") + " " + MensagemObrigatorio);
                if (Codcliteltipo == null && CodcliteltipoPreenchido)
                    throw new Exception("O campo " + RetornaLabelPropriedade("Codcliteltipo") + " " + MensagemNullVazioEmBrancoPreenchido);
            }
        }
        public bool CodcliteltipoPreenchido { get; set; }

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

        private Int64? _tipo;
        public Int64? Tipo
        {
            get { return _tipo; }
            set
            {
                if (_tipo != value)
                {
                    _tipo = value;
                    OnPropertyChanged("Tipo");
                }

                if (VerificaObrigatorio("Tipo") && Tipo == null)
                    throw new Exception("O campo " + RetornaLabelPropriedade("Tipo") + " " + MensagemObrigatorio);
                if (Tipo == null && TipoPreenchido)
                    throw new Exception("O campo " + RetornaLabelPropriedade("Tipo") + " " + MensagemNullVazioEmBrancoPreenchido);

                if (value == 3)
                    ListaLabelPropriedade["Fone"] = "celular";
                else
                    ListaLabelPropriedade["Fone"] = "telefone";
            }
        }
        public bool TipoPreenchido { get; set; }

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
            if (Codcliteltipo != null)
                CodcliteltipoPreenchido = true;
            else
                CodcliteltipoPreenchido = false;

            if (Codcliente != null)
                CodclientePreenchido = true;
            else
                CodclientePreenchido = false;

            if (!string.IsNullOrWhiteSpace(Ddd))
                DddPreenchido = true;
            else
                DddPreenchido = false;

            if (!string.IsNullOrWhiteSpace(Fone))
                FonePreenchido = true;
            else
                FonePreenchido = false;

            if (Tipo != null)
                TipoPreenchido = true;
            else
                TipoPreenchido = false;

            if (Codtptel != null)
                CodtptelPreenchido = true;
            else
                CodtptelPreenchido = false;

        }
    }
}