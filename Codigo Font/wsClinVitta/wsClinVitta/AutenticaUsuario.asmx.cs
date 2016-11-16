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
    /// Summary description for AutenticaUsuario
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
   
    public class AutenticaUsuario : System.Web.Services.WebService
    {
     
        [WebMethod(Description = "pa")]//, EnableSession = false)]
        public DataTable PARAMETRO_xxx_WEBSERV() 
        {
            DataTable dt = new DataTable();
            return dt;
        }
   

        /// <summary>
        /// 0 = Usuario existente e inativo, 1 = Usuario existente e ativo, 2 Usuario inexistente.
        /// </summary>
        int bStatus;
        string projeto;
        string NomeProjeto;
        private MySqlConnection GetConnection()
        {
            string sql = Classes.Connection.Host_Bank;
            return new MySqlConnection(sql);

        }

        public class DadosUsuario
        {
            public int Status { get; set; }
            public string CODUSUARIO { get; set; }
            public string FilialUsuario { get; set; }
            public string FuncaoUsuario { get; set; }
            public string NomeUsuario { get; set; }
        }

        /// <summary>
        /// Autentica Usuario 
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="pSenha"></param>
        /// <param name="pCodProjeto"></param>
        /// <returns>0 = usuario não cadastrado
        /// 1 = usuario cadastrado e com acesso ao projeto
        /// 2 = usuario cadastrado mais não com acesso ao projeto
        /// </returns>

        [WebMethod]
        public List<DadosUsuario> BuscaUsuario(string pUsuario, string pSenha, string pCodProjeto)
        {
        
            List<DadosUsuario> DadosUsuario = new List<DadosUsuario>();
            DadosUsuario DdUsuario = new DadosUsuario();

            // valida para não dar quebra de link
            if (pUsuario == "'" && pSenha == "'")
            {

                DdUsuario.Status = 0;

                DadosUsuario.Add(DdUsuario);
                return DadosUsuario;
            }


            projeto = BuscaNomeProjeto(pCodProjeto); // verifica o codigo do projeto para pesquisa.

            MySqlConnection con = GetConnection();
            {
                try
                { // VAlida o usuario e asenha
                    con.Open();
                    sql = "Select * From MV_USUARIO Where usuario = '" + pUsuario + "' And senha = '" + pSenha + "' "; // PESQUISA POR USUARIO, CASO ESTEJA VALIDANDO O USUARIO PARA ACESSAR O SISTEMA.
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        string Status = dt.Rows[0]["Status"].ToString(); // verifica se o usuario esta Ativo ou Inativo

                        if (projeto != "XX%¨&ValidaUsuarioPTrocaSenha#$XX")
                        {
                            string Projeto = dt.Rows[0][projeto].ToString(); // verifica se o codigo do projeto cadastrado.

                            if (Status == "1" && Projeto == "1")
                            {
                                DdUsuario.Status = 1; // usuario cadastrado e com acesso ao projeto
                                DdUsuario.CODUSUARIO = dt.Rows[0]["ID"].ToString();
                                DdUsuario.FuncaoUsuario = dt.Rows[0]["FUNCAO"].ToString();
                                DdUsuario.FilialUsuario = dt.Rows[0]["FILIAL_USUARIO"].ToString();
                                DdUsuario.NomeUsuario = dt.Rows[0]["NOME"].ToString();
                            }
                            else
                            {
                                DdUsuario.Status = 2; // usuario cadastrado mais sem acesso ao projeto
                            }
                        }
                        else
                        {
                            DdUsuario.Status = 50; // Alteração de senha. ou geração do token bi
                        }
                    }
                    else
                    {
                        DdUsuario.Status = 0; // usuario não cadastrado
                    }

                }
                catch (Exception ex)
                {
                    throw;

                }

            }

            DadosUsuario.Add(DdUsuario);
            return DadosUsuario;

        }

        private string BuscaNomeProjeto(string pCODPROJETO)
        {
            if (pCODPROJETO == "10")
            {
                NomeProjeto = "PROJ_10";// codigo para liberação de acesso a aplicação mobile
            }
            else if (pCODPROJETO == "11")// codigo de projeto de sistema web
            {
                NomeProjeto = "PROJ_11";
            }
            else if (pCODPROJETO == "14")/// codigo para troca de senha.
            {
                NomeProjeto = "PROJ_14";
            }
            else if (pCODPROJETO == "XX%¨&ValidaUsuarioPTrocaSenha#$XX")
            {
                NomeProjeto = "XX%¨&ValidaUsuarioPTrocaSenha#$XX";
            }

            return NomeProjeto;
        }

        [WebMethod]
        public List<DadosUsuario> ValidaToken(string pToken, string CODPROJETO)
        {
            List<DadosUsuario> DadosUsuario = new List<DadosUsuario>();
            DadosUsuario DdUsuario = new DadosUsuario();

            projeto = BuscaNomeProjeto(CODPROJETO); // verifica o codigo do projeto para pesquisa.

            MySqlConnection con = GetConnection();
            {
                try
                { // VAlida o usuario e asenha
                    con.Open();
                    sql = "Select * From MV_USUARIO Where TOKEN_TEMP_BI = '" + pToken + "'"; // PESQUISA POR TOKEN, CASO ESTEJA VALIDANDO O USUARIO PARA ACESSAR O SISTEMA.
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        string Status = dt.Rows[0]["Status"].ToString(); // verifica se o usuario esta Ativo ou Inativo

                        string Projeto = dt.Rows[0][projeto].ToString(); // verifica se o codigo do projeto cadastrado.

                        if (Status == "1" && Projeto == "1")
                        {
                            DdUsuario.Status = 1; // usuario cadastrado e com acesso ao projeto
                            DdUsuario.CODUSUARIO = dt.Rows[0]["ID"].ToString();
                            DdUsuario.FuncaoUsuario = dt.Rows[0]["FUNCAO"].ToString();
                            DdUsuario.FilialUsuario = dt.Rows[0]["FILIAL_USUARIO"].ToString();
                            DdUsuario.NomeUsuario = dt.Rows[0]["NOME"].ToString();
                        }
                        else
                        {
                            DdUsuario.Status = 2; // usuario cadastrado mais sem acesso ao projeto
                        }
                    }
                    else
                    {
                        DdUsuario.Status = 0; // usuario não cadastrado
                    }
                }
                catch (Exception ex)
                {
                    throw;

                }

            }

            DadosUsuario.Add(DdUsuario);
            return DadosUsuario;
        }

        public string sql { get; set; }
    }

}
