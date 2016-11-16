using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

namespace wsClinVitta
{
    /// <summary>
    /// Summary description for ControleCadastroUsuarios
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ControleCadastroUsuarios : System.Web.Services.WebService
    {
        #region xxxx NÃO EXCLUIR ESSA GAMBICA xxxx
        [WebMethod(Description = "pa")]//, EnableSession = false)]
        public DataTable PARAMETRO_xxx_WEBSERV() // foi criado pois estava dando erro de referencia no webservice do newjureweb, ele não faz nada somente da referencia do DATATABLE na aplicação  §§§ POR FAVOR NÃO EXCLUIR §§§
        {
            DataTable dt = new DataTable();
            return dt;
        }
        #endregion
        int pRetorno { get; set; }

        private MySqlConnection GetConnection()
        {
            string sql = Classes.Connection.Host_Bank;
            return new MySqlConnection(sql);

        }

        public class ListaTipoUsuario
        {
            public string ID { get; set; }
            public string NOME { get; set; }
            public string FUNCAO { get; set; }
            public string STATUS { get; set; }

        }

        #region Lista Usuario
        [WebMethod]
        public List<ListaTipoUsuario> ListaUsuariosCadastrados()
        {
            #region Query
            DataTable dt = new DataTable();
            using (MySqlConnection con = GetConnection())
            {
                try
                {
                    con.Open();
                    string sql = "SELECT ID,NOME,FUNCAO,STATUS FROM MV_USUARIO;";
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

            List<ListaTipoUsuario> listTIPO = new List<ListaTipoUsuario>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListaTipoUsuario tipo = new ListaTipoUsuario();
                tipo.ID = dt.Rows[i]["ID"].ToString();
                tipo.NOME = dt.Rows[i]["NOME"].ToString();
                tipo.FUNCAO = dt.Rows[i]["FUNCAO"].ToString();
                tipo.STATUS = dt.Rows[i]["STATUS"].ToString();
                listTIPO.Add(tipo);
            }

            return listTIPO;

        }

        #endregion

        #region Cadastrar Usuario
        [WebMethod]
        public int Cadastro_Usuario(string NOME_USUARIO, string FUNCAO, string USUARIO, string SENHA, string USER_CADASTRO, string STATUS)
        {
            pRetorno = 0;
            RetornaDataHora DATAHORA = new RetornaDataHora();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO MV_USUARIO (NOME,FUNCAO,USUARIO,SENHA,DTCADASTRO,USER_CADASTRO,USER_FUNCAO,STATUS)");
            sb.AppendLine("VALUES (@NOME,@FUNCAO,@USUARIO,@SENHA,@DTCADASTRO,@USER_CADASTRO,@USER_FUNCAO,STATUS)");

            MySqlParameter[] pmts = new MySqlParameter[7];

            pmts[0] = new MySqlParameter("@NOME", NOME_USUARIO);
            pmts[1] = new MySqlParameter("@FUNCAO", FUNCAO);
            pmts[2] = new MySqlParameter("@USUARIO", USUARIO);
            pmts[3] = new MySqlParameter("@USER_CADASTRO", SENHA);
            pmts[4] = new MySqlParameter("@USER_FUNCAO", USER_CADASTRO);
            pmts[5] = new MySqlParameter("@STATUS", STATUS);
            pmts[6] = new MySqlParameter("@DTCADASTRO", DATAHORA.RetornaDataHoraServidor_MySql(1));


            MySqlCommand cmd = new MySqlCommand();
            for (byte i = 0; i < pmts.Length; i++)
            {
                cmd.Parameters.Add(pmts[i]);
            }

            MySqlConnection conn = GetConnection();
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sb.ToString();

                    pRetorno = cmd.ExecuteNonQuery();

                    conn.Close();
                    conn.Dispose();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    throw new Exception("ERRO BANCO DE DADOS: " + ex.Message.ToString());
                }

            }
            return pRetorno;
        }
        #endregion

        #region Alterar Usuario
        [WebMethod]
        public int Alterar_Usuario(string NOME_USUARIO, string FUNCAO, string USUARIO, string SENHA, string USER_CADASTRO, string STATUS, string ID)
        {
            pRetorno = 0;
            RetornaDataHora DATAHORA = new RetornaDataHora();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE MV_USUARIO SET NOME=@NOME,FUNCAO=@FUNCAO,USUARIO=@USUARIO,SENHA=@SENHA,USER_CADASTRO=@USER_CADASTRO,USER_FUNCAO=@USER_FUNCAO,STATUS=@STATUS");
            sb.AppendLine(" WHERE ID = @ID");

            MySqlParameter[] pmts = new MySqlParameter[7];

            pmts[0] = new MySqlParameter("@NOME", NOME_USUARIO);
            pmts[1] = new MySqlParameter("@FUNCAO", FUNCAO);
            pmts[2] = new MySqlParameter("@USUARIO", USUARIO);
            pmts[3] = new MySqlParameter("@USER_CADASTRO", SENHA);
            pmts[4] = new MySqlParameter("@USER_FUNCAO", USER_CADASTRO);
            pmts[5] = new MySqlParameter("@STATUS", STATUS);
            pmts[6] = new MySqlParameter("@ID", ID);


            MySqlCommand cmd = new MySqlCommand();
            for (byte i = 0; i < pmts.Length; i++)
            {
                cmd.Parameters.Add(pmts[i]);
            }

            MySqlConnection conn = GetConnection();
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sb.ToString();

                    pRetorno = cmd.ExecuteNonQuery();

                    conn.Close();
                    conn.Dispose();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    throw new Exception("ERRO BANCO DE DADOS: " + ex.Message.ToString());
                }

            }
            return pRetorno;
        }
        #endregion

        #region Excluir Usuario
        [WebMethod]
        public int Excluir_Usuario(string ID)
        {
            pRetorno = 0;
            RetornaDataHora DATAHORA = new RetornaDataHora();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DELETE FROM MV_USUARIO");
            sb.AppendLine("WHERE ID = @ID");

            MySqlParameter[] pmts = new MySqlParameter[1];

            pmts[0] = new MySqlParameter("@ID", ID);


            MySqlCommand cmd = new MySqlCommand();
            for (byte i = 0; i < pmts.Length; i++)
            {
                cmd.Parameters.Add(pmts[i]);
            }

            MySqlConnection conn = GetConnection();
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sb.ToString();

                    pRetorno = cmd.ExecuteNonQuery();

                    conn.Close();
                    conn.Dispose();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    throw new Exception("ERRO BANCO DE DADOS: " + ex.Message.ToString());
                }

            }
            return pRetorno;
        }
        #endregion

        #region Alterar Senha Usuario
        [WebMethod]
        public int Alterar_Senha_Usuario(string Usuario, string SenhaAtual, string Nova_Senha)
        {
            pRetorno = 0;

            AutenticaUsuario autenticaUsuario = new AutenticaUsuario();

            pRespostaAutenticaUsuario = autenticaUsuario.BuscaUsuario(Usuario, Classes.NewSeguranca.Criptografar(SenhaAtual), "XX%¨&ValidaUsuarioPTrocaSenha#$XX");//verifica retorno se o usuario é existente

            foreach (var resultBuscaUsuarioExistente in pRespostaAutenticaUsuario)
            {
                if (resultBuscaUsuarioExistente.Status == 50)
                {
                    #region Grava a nova Senha
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("UPDATE MV_USUARIO SET SENHA = @NOVASENHA");
                    sb.AppendLine("WHERE USUARIO ='" + Usuario + "' AND SENHA ='" + Classes.NewSeguranca.Criptografar(SenhaAtual) + "'");

                    MySqlParameter[] pmts = new MySqlParameter[1];

                    pmts[0] = new MySqlParameter("@NOVASENHA", Classes.NewSeguranca.Criptografar(Nova_Senha));


                    MySqlCommand cmd = new MySqlCommand();
                    for (byte i = 0; i < pmts.Length; i++)
                    {
                        cmd.Parameters.Add(pmts[i]);
                    }

                    MySqlConnection conn = GetConnection();
                    {
                        try
                        {
                            conn.Open();
                            cmd.Connection = conn;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = sb.ToString();

                            pRetorno = cmd.ExecuteNonQuery();

                            conn.Close();
                            conn.Dispose();
                            cmd.Dispose();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("ERRO BANCO DE DADOS: " + ex.Message.ToString());
                        }

                    }
                    #endregion
                }
            }

            return pRetorno;
        }


        #endregion

        #region Alterar Token_BI
        [WebMethod]
        public int Alterar_TokenBI(string Usuario, string SenhaAtual, string pToken)
        {
            pRetorno = 0;

            AutenticaUsuario autenticaUsuario = new AutenticaUsuario();

            pRespostaAutenticaUsuario = autenticaUsuario.BuscaUsuario(Usuario, Classes.NewSeguranca.Criptografar(SenhaAtual), "XX%¨&ValidaUsuarioPTrocaSenha#$XX");//verifica retorno se o usuario é existente

            foreach (var resultBuscaUsuarioExistente in pRespostaAutenticaUsuario)
            {
                if (resultBuscaUsuarioExistente.Status == 50)
                {
                    #region Grava a nova Senha
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("UPDATE MV_USUARIO SET TOKEN_TEMP_BI = @TOKEN_TEMP_BI");
                    sb.AppendLine("WHERE USUARIO ='" + Usuario + "' AND SENHA ='" + Classes.NewSeguranca.Criptografar(SenhaAtual) + "'");

                    MySqlParameter[] pmts = new MySqlParameter[1];

                    pmts[0] = new MySqlParameter("@TOKEN_TEMP_BI", pToken);


                    MySqlCommand cmd = new MySqlCommand();
                    for (byte i = 0; i < pmts.Length; i++)
                    {
                        cmd.Parameters.Add(pmts[i]);
                    }

                    MySqlConnection conn = GetConnection();
                    {
                        try
                        {
                            conn.Open();
                            cmd.Connection = conn;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = sb.ToString();

                            pRetorno = cmd.ExecuteNonQuery();

                            conn.Close();
                            conn.Dispose();
                            cmd.Dispose();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("ERRO BANCO DE DADOS: " + ex.Message.ToString());
                        }

                    }
                    #endregion
                }
            }

            return pRetorno;
        }

        #endregion



        public List<AutenticaUsuario.DadosUsuario> pRespostaAutenticaUsuario { get; set; }
    }
}
