using System;
using System.Collections.ObjectModel;
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
    public class Resources
    {
        public class ClasseBase
        {
            public Int64? Codigo { get; set; }
            public string Descricao { get; set; }
        }

        public class ClasseBaseString
        {
            public string Codigo { get; set; }
            public string Descricao { get; set; }
        }

        public ObservableCollection<ClasseBaseString> ListaSexo
        {
            get
            {
                var lista = new ObservableCollection<ClasseBaseString>() { new ClasseBaseString() { Codigo = "M", Descricao = "Masculino" },
                                                                           new ClasseBaseString() { Codigo = "F", Descricao = "Feminino" }};
                return lista;
            }
        }


        public ObservableCollection<ClasseBaseString> ListaTipoProcedimento
        {
            get
            {
                var lista = new ObservableCollection<ClasseBaseString>() { new ClasseBaseString() { Codigo = "Exame", Descricao = "Exame" },
                                                                           new ClasseBaseString() { Codigo = "Retorno", Descricao = "Retorno" },
                                                                            new ClasseBaseString() { Codigo = "Consulta", Descricao = "Consulta" }};
                return lista;
            }
        }
        public ObservableCollection<ClasseBaseString> ListaTipoStatusAgendamento
        {
            get
            {
                var lista = new ObservableCollection<ClasseBaseString>() { new ClasseBaseString() { Codigo = "Aendado", Descricao = "Agendado" },
                                                                           new ClasseBaseString() { Codigo = "Confirmado", Descricao = "Masculino" },
                                                                           new ClasseBaseString() { Codigo = "Presente", Descricao = "Presente" },
                                                                           new ClasseBaseString() { Codigo = "Atendido", Descricao = "Atendido" },
                                                                            new ClasseBaseString() { Codigo = "Ausente", Descricao = "Ausente" }};
                return lista;
            }
        }

        public ObservableCollection<ClasseBaseString> ListaSexoBranco
        {
            get
            {
                var lista = new ObservableCollection<ClasseBaseString>() { new ClasseBaseString() { Codigo = "", Descricao = " " },
                                                                           new ClasseBaseString() { Codigo = "M", Descricao = "Masculino" },
                                                                           new ClasseBaseString() { Codigo = "F", Descricao = "Feminino" }};
                return lista;
            }
        }

        public ObservableCollection<ClasseBaseString> ListaTipoPessoa
        {
            get
            {
                var lista = new ObservableCollection<ClasseBaseString>() { new ClasseBaseString() { Codigo = "F", Descricao = "Física" },
                                                                           new ClasseBaseString() { Codigo = "J", Descricao = "Juridica" }};
                return lista;
            }
        }

        public ObservableCollection<ClasseBase> ListaMeses
        {
            get
            {
                var meses = new ObservableCollection<ClasseBase>() { new ClasseBase() { Descricao = "Janeiro", Codigo = 1 },
                                                                     new ClasseBase() { Descricao = "Fevereiro", Codigo = 2 },
                                                                     new ClasseBase() { Descricao = "Março", Codigo = 3 },
                                                                     new ClasseBase() { Descricao = "Abril", Codigo = 4 },
                                                                     new ClasseBase() { Descricao = "Maio", Codigo = 5 },
                                                                     new ClasseBase() { Descricao = "Junho", Codigo = 6 },
                                                                     new ClasseBase() { Descricao = "Julho", Codigo = 7 },
                                                                     new ClasseBase() { Descricao = "Agosto", Codigo = 8 },
                                                                     new ClasseBase() { Descricao = "Setembro", Codigo = 9 },
                                                                     new ClasseBase() { Descricao = "Outubro", Codigo = 10 },
                                                                     new ClasseBase() { Descricao = "Novembro", Codigo = 11 },
                                                                     new ClasseBase() { Descricao = "Dezembro", Codigo = 12 } };
                return meses;
            }
        }

        public ObservableCollection<ClasseBase> ListaMesesBranco
        {
            get
            {
                var meses = new ObservableCollection<ClasseBase>() { new ClasseBase() { Descricao = " ", Codigo = null },
                                                                     new ClasseBase() { Descricao = "Janeiro", Codigo = 1 },
                                                                     new ClasseBase() { Descricao = "Fevereiro", Codigo = 2 },
                                                                     new ClasseBase() { Descricao = "Março", Codigo = 3 },
                                                                     new ClasseBase() { Descricao = "Abril", Codigo = 4 },
                                                                     new ClasseBase() { Descricao = "Maio", Codigo = 5 },
                                                                     new ClasseBase() { Descricao = "Junho", Codigo = 6 },
                                                                     new ClasseBase() { Descricao = "Julho", Codigo = 7 },
                                                                     new ClasseBase() { Descricao = "Agosto", Codigo = 8 },
                                                                     new ClasseBase() { Descricao = "Setembro", Codigo = 9 },
                                                                     new ClasseBase() { Descricao = "Outubro", Codigo = 10 },
                                                                     new ClasseBase() { Descricao = "Novembro", Codigo = 11 },
                                                                     new ClasseBase() { Descricao = "Dezembro", Codigo = 12 } };
                return meses;
            }
        }

        public ObservableCollection<ClasseBase> ListaOperacaoPadrao
        {
            get
            {
                var lista = new ObservableCollection<ClasseBase>() { new ClasseBase() { Codigo = 1, Descricao = "Igual" },
                                                                     new ClasseBase() { Codigo = 7, Descricao = "Inicie"},
                                                                     new ClasseBase() { Codigo = 9, Descricao = "Termine"},
                                                                     new ClasseBase() { Codigo = 8, Descricao = "Contenha"} };
                return lista;
            }
        }
        public ObservableCollection<ClasseBase> ListaTipoTelefone
        {
            get
            {
                var lista = new ObservableCollection<ClasseBase>()
                {
                    new ClasseBase() { Codigo = 1, Descricao = "Pessoal" },
                    new ClasseBase() { Codigo = 2, Descricao = "Comercial" },
                    new ClasseBase() { Codigo = 3, Descricao = "Comunitário" },
                    new ClasseBase() { Codigo = 4, Descricao = "Recado" },
                    new ClasseBase() { Codigo = 5, Descricao = "Residêncial" },
                };
                return lista;
            }
        }
        public class ListaTipoPesquisaCliente
        {
            public string TipoFiltroPesquisaCliente { get; set; }
            public string DescricaoPesquisaCliente { get; set; }
        }

        public ObservableCollection<ListaTipoPesquisaCliente> ListaFiltroPesquisaCliente
        {
            get
            {
                var lista = new ObservableCollection<ListaTipoPesquisaCliente>()
                {
                    new ListaTipoPesquisaCliente() { TipoFiltroPesquisaCliente = "NOME", DescricaoPesquisaCliente = "NOME"},
                    new ListaTipoPesquisaCliente() {TipoFiltroPesquisaCliente = "CPF/CNPJ", DescricaoPesquisaCliente = "CPF/CNPJ"},
                    new ListaTipoPesquisaCliente() {TipoFiltroPesquisaCliente = "RG/IE", DescricaoPesquisaCliente = "RG/IE"}
                };
                return lista;
            }
        }
        public class ListEstadoCivil
        {
            public string Descricao { get; set; }
            public string Codigo { get; set; }
        }

        public ObservableCollection<ListEstadoCivil> Estadocivil
        {
            get
            {

                var lista = new ObservableCollection<ListEstadoCivil>() {
           
                new ListEstadoCivil() {Descricao ="Casado", Codigo ="Casado"},
                new ListEstadoCivil() {Descricao ="Desquitado", Codigo ="Desquitado"},
                new ListEstadoCivil() {Descricao ="Divorciado", Codigo ="Divorciado"},
                new ListEstadoCivil() {Descricao ="Amigado", Codigo ="Amigado"},
                new ListEstadoCivil() {Descricao ="Separado", Codigo ="Separado"},
                new ListEstadoCivil() {Descricao ="Solteiro", Codigo ="Solteiro"},
                new ListEstadoCivil() {Descricao ="Viuvo", Codigo ="Viuvo"},
                };
                return lista;
            }


        }

    }
}
