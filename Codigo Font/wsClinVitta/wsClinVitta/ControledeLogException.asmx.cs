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
    /// Summary description for ControledeLogException
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
 
    public class ControledeLogException : System.Web.Services.WebService
    {
               
        [WebMethod(Description = "pa")]//, EnableSession = false)]
        public DataTable PARAMETRO_xxx_WEBSERV()
        {
            DataTable dt = new DataTable();
            return dt;
        }


        private MySqlConnection GetConnection()
        {
            string sql = Classes.Connection.Host_Bank;
            return new MySqlConnection(sql);

        }

        [WebMethod(Description = "GravaLogBanco")]
        public void GravaLog(string pMensagem, string pDetalhe, string CodUsuario, string COD_PROJETO, string DATAHORA, string ENDERECO_REMOTO, string VERSAO)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" INSERT INTO  MV_LOG_ERRO_APLICACAO (MENSAGEM,DETALHE,CODUSUARIO,COD_PROJETO,DATA_HORA,ENDERECO_REMOTO,VERSAO)");
            sb.AppendLine(" VALUES (@MENSAGEM, @DETALHE,@CODUSUARIO,@COD_PROJETO,@DATA_HORA,@ENDERECO_REMOTO,@VERSAO)");

            MySqlParameter[] pmts = new MySqlParameter[7];
            pmts[0] = new MySqlParameter("@MENSAGEM", pMensagem);
            pmts[1] = new MySqlParameter("@DETALHE", pDetalhe);
            pmts[2] = new MySqlParameter("@CODUSUARIO", CodUsuario);
            pmts[3] = new MySqlParameter("@COD_PROJETO", COD_PROJETO);
            pmts[4] = new MySqlParameter("@DATA_HORA", Classes.NewValidacao.ConvertDataHoraMySql(DATAHORA));
            pmts[5] = new MySqlParameter("@ENDERECO_REMOTO", ENDERECO_REMOTO);
            pmts[6] = new MySqlParameter("@VERSAO", VERSAO);

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

                    cmd.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    throw new Exception("ERRO BANCO DE DADOS: " + ex.Message.ToString());
                }

                finally
                {
                    conn.Close();
                    conn.Dispose();
                    cmd.Dispose();
                }

            }

        }


        [WebMethod(Description = "Lista Log de Erros De Aplicações")]
        public List<CamposLogs> ListaLogsAplicacoes(string pCodAplicacao)
        {
            #region Query
            DataTable dt = new DataTable();
            using (MySqlConnection con = GetConnection())
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT LOGAPP.ID,LOGAPP.DATA_HORA,LOGAPP.VERSAO,LOGAPP.MENSAGEM,LOGAPP.ENDERECO_REMOTO,LOGAPP.DETALHE,CADUSER.NOME FROM MV_LOG_ERRO_APLICACAO LOGAPP LEFT JOIN MV_USUARIO CADUSER ON CADUSER.ID = LOGAPP.CODUSUARIO WHERE LOGAPP.COD_PROJETO ='" + pCodAplicacao + "';");

                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(sb.ToString(), con);
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

            List<CamposLogs> listTIPO = new List<CamposLogs>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CamposLogs tipo = new CamposLogs();

                tipo.CODIGO = dt.Rows[i]["ID"].ToString();
                tipo.DATA_HORA = dt.Rows[i]["DATA_HORA"].ToString();
                tipo.VERSAO = dt.Rows[i]["VERSAO"].ToString();
                tipo.MENSAGEM = dt.Rows[i]["MENSAGEM"].ToString();
                tipo.USUARIO = dt.Rows[i]["NOME"].ToString();
                tipo.IP_REMOTO = dt.Rows[i]["ENDERECO_REMOTO"].ToString();
                tipo.DETALHE_ERRO = dt.Rows[i]["DETALHE"].ToString();
                listTIPO.Add(tipo);
            }

            return listTIPO;
        }

        public class CamposLogs
        {
            public string CODIGO { get; set; }
            public string DATA_HORA { get; set; }
            public string MENSAGEM { get; set; }
            public string VERSAO { get; set; }
            public string USUARIO { get; set; }
            public string IP_REMOTO { get; set; }
            public string DETALHE_ERRO { get; set; }

        }
    }
}
