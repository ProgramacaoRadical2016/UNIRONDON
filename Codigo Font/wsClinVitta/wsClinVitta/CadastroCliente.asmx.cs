using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace wsClinVitta
{
    /// <summary>
    /// Summary description for CadastroCliente
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/", Name = "Busca_Dados_Cadastro_Cliente")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CadastroCliente : System.Web.Services.WebService
    {
        int EstadoCadastro;

        MySqlCommand cmd;
        string codcli;
        string strPesquisa;



        public class ParametrosCliente
        {
            #region Declarações de Variaveis
            public string pNOME_RAZAO = "";
            public string pCPFCNPJ = "";
            public string pRG_IE = "";
            public string pEMISSOR = "";
            public string pUF_EMISSAO = "";
            public string pSEXO = "";
            public string pESTADO_CIVIL = "";
            public string pDATA_NASCIMENTO = "";
            public string pNOME_MAE = "";
            public string pNOME_PAI = "";
            public string pCELULAR = "";
            public string pFONE_FIXO = "";
            public string pCEP = "";
            public string pUF = "";
            public string pCIDADE = "";
            public string pDT_EMISSAO = "";
            public string pBAIRRO = "";
            public string pPONTO_REFERENCIA = "";
            public string pTIPO_LOGRADOURO = "";
            public string pDESC_LOGRADOURO = "";
            public string pEND_NUM = "";
            public string pDESC_COMPLEMENTO = "";
            public string pDESC_OBS = "";
            public string pUSER_CADASTRO = "";
            public string pDATA_CADASTRO = "";
            public string pEMAIL = "";
            public string pTIPO_DOC = "";
            public string pQT_DEPENDENTES = "";
            public string pNUM_SEG = "";
            public string pPESSOA = "";
            public string pNOME_FANTASIA = "";
            public string pTIPO_INCRICAO = "";
            public string pATIVIDADE = "";
            public string pDT_INICIO_ATIVIDADE = "";
            public string pDT_JUNTA = "";
            public string pDT_ULT_ALT_CONTRATUAL = "";
            public string pCAPITAL_SOCIAL = "";
            public string pFATURAMENTO_MENSAL = "";
            public string pRESP_COMERCIAL = "";
            public string pRESP_COMERCIAL_TEL_FIXO = "";
            public string pRESP_COMERCIAL_CELULAR = "";
            public string pDTVENCEDOC = "";
            public string pFILIAL_CAD = "";
            public string CPFCNPJ_CLIENTE;

            #endregion
        }

        private MySqlConnection GetConnection()
        {
            string sql = Classes.Connection.Host_Bank;
            return new MySqlConnection(sql);

        }


        #region Tipo de Documento
        [WebMethod(Description = "Lista Os Tipos de Documentos")]
        public List<TpDocEmitido> TipoDocumento()
        {
            #region Query Tipodocumento
            DataTable dt = new DataTable();
            using (MySqlConnection con = GetConnection())
            {

                try
                {
                    con.Open();
                    string sql = "SELECT DESCRICAO,ID FROM MV_CAD_TIPO_DOC_IDENT GROUP BY DESCRICAO  ORDER BY DESCRICAO";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

            }
            #endregion

            List<TpDocEmitido> listTIPO = new List<TpDocEmitido>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TpDocEmitido tipo = new TpDocEmitido();
                tipo.DESCTIPODOC = dt.Rows[i]["DESCRICAO"].ToString();
                tipo.IDDESCTIPODOC = Convert.ToInt32(dt.Rows[i]["ID"]);
                listTIPO.Add(tipo);

            }
            return listTIPO;
        }
        public class TpDocEmitido
        {
            public string DESCTIPODOC;
            public int IDDESCTIPODOC;

        }

        #endregion


        #region Lista Tipod de Orgão Emissor
        [WebMethod(Description = "Lista Os Tipos de Orgões Emissor")]
        public List<TipoOrgaoEmissorDoc> TipoOrgaoEmissor()
        {
            #region Query Busca Nome do Orgão
            DataTable dt = new DataTable();
            using (MySqlConnection con = GetConnection())
            {

                try
                {
                    con.Open();
                    string sql = "SELECT DESCRICAO,ID FROM MV_CAD_ORGAO_EMISSOR GROUP BY DESCRICAO  ORDER BY DESCRICAO";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

            }
            #endregion

            List<TipoOrgaoEmissorDoc> listTIPO = new List<TipoOrgaoEmissorDoc>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TipoOrgaoEmissorDoc tipo = new TipoOrgaoEmissorDoc();
                tipo.NomeOrgao = dt.Rows[i]["DESCRICAO"].ToString();
                tipo.CodNomeOrgao = Convert.ToInt32(dt.Rows[i]["ID"]);
                listTIPO.Add(tipo);

            }
            return listTIPO;
        }

        public class TipoOrgaoEmissorDoc
        {
            public string NomeOrgao;
            public int CodNomeOrgao;
        }
        #endregion


        #region Tipo de Telefone
        [WebMethod(Description = "Lista Os Tipos de Telefones")]
        public List<TpTELEFONE> TipoTelefone()
        {
            #region Query Busca Nome do Orgão
            DataTable dt = new DataTable();
            using (MySqlConnection con = GetConnection())
            {

                try
                {
                    con.Open();
                    string sql = "SELECT DESCRICAO,CODTPTEL FROM MV_CAD_TIPO_TEL GROUP BY DESCRICAO  ORDER BY DESCRICAO";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

            }
            #endregion

            List<TpTELEFONE> listTIPO = new List<TpTELEFONE>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TpTELEFONE tipo = new TpTELEFONE();
                tipo.DESCRICAO = dt.Rows[i]["DESCRICAO"].ToString();
                tipo.CODTPTEL = Convert.ToInt32(dt.Rows[i]["CODTPTEL"]);
                listTIPO.Add(tipo);


            }

            return listTIPO;

        }

        public class TpTELEFONE
        {
            public string DESCRICAO { get; set; }
            public int CODTPTEL { get; set; }
            public string Tipo { get; set; }
            public string Fone { get; set; }
            public string Ddd { get; set; }
        }
        #endregion


        #region Retorna Lista de UF
        [WebMethod(Description = "Lista de UF.")]
        public List<ListaUF> BuscaListaUF()
        {

            #region Query Tipodocumento
            DataTable dt = new DataTable();
            using (MySqlConnection con = GetConnection())
            {

                try
                {
                    con.Open();
                    string sql = "SELECT UF,ID  FROM MV_CAD_ESTADO ORDER BY UF";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

            }
            #endregion

            List<ListaUF> listTIPO = new List<ListaUF>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListaUF tipo = new ListaUF();
                tipo.UF = dt.Rows[i]["UF"].ToString();
                tipo.IDUF = Convert.ToInt32(dt.Rows[i]["ID"]);
                listTIPO.Add(tipo);

            }

            return listTIPO;
        }

        public class ListaUF
        {
            public string UF;
            public int IDUF;
        }
        #endregion

        [WebMethod(Description = "PESQUISA_PARAMETROS_CADASTRO DE CLIENTE")]//, EnableSession = false)]
        public DataTable PARAMETROS_CADASTRO_CLIENTE()
        {
            DataTable dt = new DataTable();
            ParametrosCliente paramcadcli = new ParametrosCliente();
            #region Query Busca Parametros cadastrados
            using (MySqlConnection con = GetConnection())
            {

                try
                {
                    string sql = "SELECT * FROM MV_CAD_PARAM_CLIENTES;";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    MySqlDataReader rd = cmd.ExecuteReader();
                    dt.Load(rd);
                    con.Close();

                    return dt;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

            }
            #endregion
        }

        #region Gerar CodCliente
        [WebMethod(Description = "GERA CODCLI NA MV_CAD_CLIENTE")]//, EnableSession = false)]
        public string GerarCodCli()
        {
            string Codigo;
            string soma;

            using (MySqlConnection con = GetConnection())
            {

                try
                {
                    con.Open();

                    string sql = "SELECT * FROM MAN_DBA_TABLES";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Codigo = dt.Rows[0]["CODCLI"].ToString();
                    soma = (Convert.ToDouble(Codigo) + Convert.ToDouble(1)).ToString();
                    INSERE_IDcliente(soma);
                    return codcli = soma;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

            }

        }

        private void INSERE_IDcliente(string pCODCLI)
        {
            using (MySqlConnection con = GetConnection())
            {

                try
                {
                    con.Open();
                    string sql = "UPDATE  MAN_DBA_TABLES SET CODCLI = '" + pCODCLI + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        #endregion

        #region Pesquisa Dados Cliente Simprificada
        [WebMethod(Description = "Pesquisa Dados Cliente Simplificada return = (NOME,RG_IE,CPF_CNP,CODCLIJ)")]
        public List<TipoCamposCadPaciente> PesquisaCadastroCliente(string pFiltro, string pDADOS)
        {

            #region Query Tipodocumento
            DataTable dt = new DataTable();
            using (MySqlConnection con = GetConnection())
            {
                if (pFiltro == "RG/IE")
                {
                    strPesquisa = "SELECT CODCLI,NOME_RAZAO AS NOME, RG_IE,CPFCNPJ AS CPF_CNPJ FROM MV_CAD_CLIENTES WHERE RG_IE LIKE '%" + pDADOS + "%'";
                }
                else if (pFiltro == "CPF/CNPJ")
                {
                    strPesquisa = "SELECT CODCLI, NOME_RAZAO AS NOME, RG_IE,CPFCNPJ AS CPF_CNPJ FROM MV_CAD_CLIENTES WHERE CPFCNPJ LIKE '%" + pDADOS + "%'";
                }
                else if (pFiltro == "NOME")
                {
                    strPesquisa = "SELECT CODCLI, NOME_RAZAO AS NOME, RG_IE,CPFCNPJ AS CPF_CNPJ FROM MV_CAD_CLIENTES WHERE NOME_RAZAO LIKE '%" + pDADOS + "%'";
                }

                try
                {
                    con.Open();
                    string sql = strPesquisa.ToString();
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

            }
            #endregion

            List<TipoCamposCadPaciente> listTIPO = new List<TipoCamposCadPaciente>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TipoCamposCadPaciente tipo = new TipoCamposCadPaciente();
                tipo.NOME = dt.Rows[i]["NOME"].ToString();
                tipo.RG = dt.Rows[i]["RG_IE"].ToString();
                tipo.CPF = dt.Rows[i]["CPF_CNPJ"].ToString();
                tipo.CODCLI = dt.Rows[i]["CODCLI"].ToString();
                listTIPO.Add(tipo);

            }

            return listTIPO;
        }
        #endregion

        #region Pesquisa Dados Cliente Completo
        [WebMethod(Description = "Pesquisa Dados Cliente Completo")]
        public TipoCamposCadPaciente PesquisaCadastroClienteCompleto(string pCODCLI)
        {
            TipoCamposCadPaciente tipo = new TipoCamposCadPaciente();
            MySqlDataReader leitor;
            #region Query Cadastro Cliente
            DataTable dt = new DataTable();
            using (MySqlConnection con = GetConnection())
            {

                strPesquisa = "SELECT * FROM MV_CAD_CLIENTES WHERE CODPACIENTE = '" + pCODCLI + "'";

                try
                {
                    string sql = strPesquisa.ToString();
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    leitor = cmd.ExecuteReader();

                    while (leitor.Read())
                    {

                        tipo.Nome_paciente = leitor["NOME"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

            }
            #endregion

            return tipo;
        }
        #endregion

        [Serializable]
        public class TipoCamposCadPaciente
        {
            public string NOME_paciente { get; set; }
            public string CPF_CNPJ { get; set; }
            public string RG_IE { get; set; }
            public string Emissor { get; set; }
            public string Uf_Emissao { get; set; }
            public string Sexo { get; set; }
            public string Estado_Civil { get; set; }
            public string Data_Nascimento { get; set; }
            public string Nome_Mae { get; set; }
            public string Nome_Pai { get; set; }
            public string Celular { get; set; }
            public string Fone_Fixo { get; set; }
            public string Cep { get; set; }
            public string Uf { get; set; }
            public string Cidade { get; set; }
            public string Dt_Emissao { get; set; }
            public string Bairro { get; set; }
            public string Ponto_Referencia { get; set; }
            public string Tipo_Logradouro { get; set; }
            public string Desc_Logradouro { get; set; }
            public string End_Num { get; set; }
            public string Desc_Complemento { get; set; }
            public string Desc_Obs { get; set; }
            public string User_Cadastro { get; set; }
            public string Data_Cadastro { get; set; }
            public string Email { get; set; }
            public string Tipo_Doc { get; set; }
            public string Qt_Dependentes { get; set; }
            public string Num_Seg { get; set; }
            public string Pessoa { get; set; }
            public string Nome_Fantasia { get; set; }
            public string Tipo_Incricao { get; set; }
            public string Atividade { get; set; }
            public string Dt_Inicio_Atividade { get; set; }
            public string Dt_Junta { get; set; }
            public string Dt_Ult_Alt_Contratual { get; set; }
            public string Capital_Social { get; set; }
            public string Faturamento_Mensal { get; set; }
            public string Resp_Comercial { get; set; }
            public string Resp_Comercial_Tel_Fixo { get; set; }
            public string Resp_Comercial_Celular { get; set; }
            public string dtVenceDoc { get; set; }
            public string FILIAL_CAD { get; set; }
            public string NOSSOCLIENTE { get; set; }
            public string PRIMERAHABILITACAO { get; set; }
            public string CATEGORIAHABILITACAO { get; set; }
            public string DATAVENCHABILITACAO { get; set; }
            public string RENACHE { get; set; }
            public string OBS_HABILITACAO { get; set; }
            public string NUMEROREGISTRO { get; set; }
            public string Profissao { get; set; }
            public string CODCLI { get; set; }

            public string OBSGERALCADASTRO { get; set; }

            public string NOME { get; set; }

            public string RG { get; set; }

            public string CPF { get; set; }

            public string Nome_paciente { get; set; }
        }

        #region Retorna Lista de Telefone de Cliente
        [WebMethod(Description = "Pesquisa de Telefone de Cliente.")]
        public List<TpTELEFONE> PesquisaTelefoneCliente(string pCODCLI)
        {

            #region Query Tipo TELEFONE
            DataTable dt = new DataTable();
            using (MySqlConnection con = GetConnection())
            {

                try
                {
                    con.Open();
                    string sql = "SELECT * FROM MV_CAD_CLIENTES_TELEFONE WHERE CODCLI = '" + pCODCLI + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

            }
            #endregion

            List<TpTELEFONE> listTIPO = new List<TpTELEFONE>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TpTELEFONE tipo = new TpTELEFONE();

                tipo.Ddd = dt.Rows[i]["DDD"].ToString();
                tipo.Fone = dt.Rows[i]["NUMERO"].ToString();
                tipo.DESCRICAO = dt.Rows[i]["TIPO"].ToString();
                listTIPO.Add(tipo);

            }

            return listTIPO;
        }
        #endregion


     
    }
}
