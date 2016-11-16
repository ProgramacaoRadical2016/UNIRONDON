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
    public enum CampoInvalido
    {
        EnderecoResNaoInformado = 0,
        EnderecoCobNaoInformado = 1,
        EnderecoComNaoInformado = 2,
        EnderecoEntNaoInformado = 3,

        DadosEnderecoCobrancaNaoPreenchido = 5,
        DadosEnderecoEntregaNaoPreenchido = 7,

        CorrespondenciaNaoInformado = 8,

        RefBancariaNaoInformado = 9,
        RefBancariaVazio = 10,

        RefComercialNaoInformado = 11,
        RefComercialVazio = 12,

        RefPessoalNaoInformado = 13,
        RefPessoalDddNaoInformado = 14,
        RefPessoalDddInvalido = 15,
        RefPessoalFoneNaoInformado = 16,
        RefPessoalFoneInvalido = 17,
        RefPessoalNomeNaoInformado = 18,

        RefFornecedorNaoInformado = 19,
        RefFornecedorDddNaoInformado = 20,
        RefFornecedorDddInvalido = 21,
        RefFornecedorFoneNaoInformado = 22,
        RefFornecedorFoneInvalido = 23,
        RefFornecedorNomeNaoInformado = 24,

        PatrimonioNaoInformado = 25,
        PatrimonioValorVazio = 26,

        CelularNaoInformado = 27,
        CelularDDDNaoInformado = 28,
        CelularFoneNaoInformado = 29,
        CelularDDDInvalido = 30,
        CelularFoneInvalido = 31,

        BensNaoInformado = 32,
        BensVazio = 33,

        TelRepNaoInformado = 34,
        TelRepProprioNaoInformado = 35,
        TelRepCelularNaoInformado = 36,
        TelRepDDDNaoInformado = 37,
        TelRepFoneNaoInformado = 38,
        TelRepDDDInvalido = 39,
        TelRepFoneInvalido = 40,

        SocioNaoInformado = 41,
        CpfcnpjSocioNaoInformado = 42,
        CpfcnpjSocioInvalido = 43,
        NomeSocioNaoInformado = 44,
        DataSocioNaoInformado = 45,
        RetornoConsSpcSocioNaoInformado = 46,

        ObsVazio = 47,
        EmailNaoInformado = 48,
        EmailVazio = 49,

        TelEmprConjNaoInformado = 50,
        TelEmprConjDDDNaoInformado = 51,
        TelEmprConjFoneNaoInformado = 52,
        TelEmprConjDDDInvalido = 53,
        TelEmprConjFoneInvalido = 54,

        AvalistaIgualCliente = 55,

        TelResidenciaNaoInformado = 56,
        TelResidenciaDDDNaoInformado = 57,
        TelResidenciaFoneNaoInformado = 58,
        TelResidenciaTipoNaoInformado = 59,
        TelResidenciaDDDInvalido = 60,
        TelResidenciaFoneInvalido = 61,

        TelComercialNaoInformado = 62,
        TelComercialDDDNaoInformado = 63,
        TelComercialFoneNaoInformado = 64,
        TelComercialDDDInvalido = 65,
        TelComercialFoneInvalido = 66,

        CepEmpresaConjugeInexistente = 69,

        DadosEnderecoResidencialNaoPreenchido = 70,
        CepResidencialInexistente = 71,
        TelefoneEnderecoResidencialInvalidoNaoPreenchido = 72,

        DadosEnderecoComercialNaoPreenchido = 73,
        CepComercialInexistente = 74,
        TelefoneEnderecoComercialInvalidoNaoPreenchido = 75,

        Veiculinaoprechido = 76,
        DuasReferenciasPessoais = 77,

        CidadeResidenciaNaoCadastrado = 78,
        CidadeResidenciaNaoPertenceUf = 79,

        NaturalidadeNaoCadastrado = 80,
        NaturalidadeNaoPertenceUf = 81,

        NaturalidadeConjugeNaoCadastrado = 82,
        NaturalidadeConjugeNaoPertenceUf = 83,

        CidadeEmpresaNaoCadastrado = 84,
        CidadeEmpresaNaoPertenceUf = 85,

        CidadeEmpresaConjugeNaoCadastrado = 86,
        CidadeEmpresaConjugeNaoPertenceUf = 87,

        InscricaoEstadualInvalida = 88,

        ProfissaoNaoCadastrado = 89,
        ProfissaoConjugeNaoCadastrado = 90,

        ResidencialCidadeNaoPertenceCep = 91,
        ResidencialEstadoNaoPertenceCep = 92,
        ResidencialCidadeEstadoNaoPertenceCep = 93,

        CobrancaCidadeNaoPertenceCep = 94,
        CobrancaEstadoNaoPertenceCep = 95,
        CobrancaCidadeEstadoNaoPertenceCep = 96,

        ComercialCidadeNaoPertenceCep = 97,
        ComercialEstadoNaoPertenceCep = 98,
        ComercialCidadeEstadoNaoPertenceCep = 99,

        EntregaCidadeNaoPertenceCep = 100,
        EntregaEstadoNaoPertenceCep = 101,
        EntregaCidadeEstadoNaoPertenceCep = 102,

        CidadeEmpresaConjugeNaoPertenceCep = 103,
        EstadoEmpresaConjugeNaoPertenceCep = 104,
        CidadeEstadoEmpresaConjugeNaoPertenceCep = 105
    }

    public enum TabelaValidacao
    {
        Mv_Cliente = 0,
        Mv_Cliente_Compl = 1
    }

    public enum WCFOperador
    {
        Nenhum = 0,
        Igual = 1,
        Maior = 2,
        Menor = 3,
        MaiorIgual = 4,
        MenorIgual = 5,
        Diferente = 6,
        Inicie = 7,
        Contenha = 8,
        Termine = 9
    }

    public enum TipoFiltro
    {
        Nenhum,
        Valor,
        SomenteNumero,
        UpperCase,
        Data,
        DataMesAno
    }
}
