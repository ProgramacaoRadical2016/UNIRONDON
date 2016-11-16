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
    /// Summary description for Agendamento
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Agendamento : System.Web.Services.WebService
    {
        private MySqlConnection GetConnection()
        {
            string sql = Classes.Connection.Host_Bank;
            return new MySqlConnection(sql);

        }

        [WebMethod(Description = "pa")]//, EnableSession = false)]
        public DataTable PARAMETRO_xxx_WEBSERV()
        {
            DataTable dt = new DataTable();
            return dt;
        }


        public class ListaTipoAgendamento
        {
            public string ID { get; set; }
            public string NOME { get; set; }
            public string PROCEDIMENTO { get; set; }
            public string MEDICO { get; set; }
            public string DATA { get; set; }
            public string HORA { get; set; }
            public string STATUS { get; set; }

        }

        #region Consulta Agendamento
        [WebMethod]
        public List<ListaTipoAgendamento> ListaAgendamentoCadastrados(string pNome)
        {
            #region Query
            DataTable dt = new DataTable();
            using (MySqlConnection con = GetConnection())
            {
                try
                {
                    con.Open();
                    string sql = "select * from mv_agen_consulta agen inner join mv_cad_pacientes cadpac on agen.codpaciente = cadpac.codpaciente where cadpac.nome like '%" + pNome + "%'";
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

            List<ListaTipoAgendamento> listTIPO = new List<ListaTipoAgendamento>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListaTipoAgendamento tipo = new ListaTipoAgendamento();
                tipo.ID = dt.Rows[i]["IDAGENDA"].ToString();
                tipo.NOME = dt.Rows[i]["NOME"].ToString();
                tipo.MEDICO = dt.Rows[i]["MEDICO"].ToString();
                tipo.PROCEDIMENTO = dt.Rows[i]["PROCEDIMENTO"].ToString();
                tipo.HORA = dt.Rows[i]["HORA"].ToString();
                tipo.DATA = dt.Rows[i]["DATA"].ToString();
                listTIPO.Add(tipo);
            }

            return listTIPO;

        }
        #endregion

        #region Cadastrar Agendamento
        [WebMethod]
        public int Cadastro_Agendamento(string CODPACIENTE, string DATA, string HORA, string STATUS, string PROCEDIMENTO, string OBSERVACAO)
        {
            pRetorno = 0;
            RetornaDataHora DATAHORA = new RetornaDataHora();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO MV_AGEN_CONSULTA (CODPACIENTE,DATA,HORA,STATUS,HORA,PROCEDIMENTO,OBSERVACAO)");
            sb.AppendLine("VALUES (@CODPACIENTE,@DATA,@HORA,@STATUS,@HORA,@PROCEDIMENTO,@OBSERVACAO)");

            MySqlParameter[] pmts = new MySqlParameter[7];

            pmts[0] = new MySqlParameter("@CODPACIENTE", CODPACIENTE);
            pmts[1] = new MySqlParameter("@DATA", Classes.NewValidacao.ConvertDataMySql(DATA));
            pmts[2] = new MySqlParameter("@HORA", HORA);
            pmts[3] = new MySqlParameter("@STATUS", STATUS);
            pmts[4] = new MySqlParameter("@HORA", HORA);
            pmts[5] = new MySqlParameter("@PROCEDIMENTO", PROCEDIMENTO);
            pmts[6] = new MySqlParameter("@OBSERVACAO", OBSERVACAO);


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

        #region Alterar Agendamento
        [WebMethod]
        public int Alterar_Agendamento(string CODPACIENTE,string ID, string DATA, string HORA, string STATUS, string PROCEDIMENTO, string OBSERVACAO)
        {
            pRetorno = 0;
            RetornaDataHora DATAHORA = new RetornaDataHora();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE MV_AGEN_CONSULTA SET CODPACIENTE = @CODPACIENTE,DATA=@DATA,HORA=@HORA,STATUS=@STATUS,HORA=@HORA,PROCEDIMENTO=@PROCEDIMENTO,OBSERVACAO=@OBSERVACAO)");
            sb.AppendLine("WHERE ID = @ID");

            MySqlParameter[] pmts = new MySqlParameter[8];

            pmts[0] = new MySqlParameter("@CODPACIENTE", CODPACIENTE);
            pmts[1] = new MySqlParameter("@DATA", Classes.NewValidacao.ConvertDataMySql(DATA));
            pmts[2] = new MySqlParameter("@HORA", HORA);
            pmts[3] = new MySqlParameter("@STATUS", STATUS);
            pmts[4] = new MySqlParameter("@HORA", HORA);
            pmts[5] = new MySqlParameter("@PROCEDIMENTO", PROCEDIMENTO);
            pmts[6] = new MySqlParameter("@OBSERVACAO", OBSERVACAO);
            pmts[7] = new MySqlParameter("@ID", ID);


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

        #region Deletar Agendamento
        [WebMethod]
        public int Deletar_Agendamento(string ID)
        {
            pRetorno = 0;
            RetornaDataHora DATAHORA = new RetornaDataHora();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DELETE FROM MV_AGEN_CONSULTA");
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


        public int pRetorno { get; set; }
    }
}
