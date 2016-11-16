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
    /// Summary description for RetornaDataHora
    /// </summary>
  [WebService(Namespace = "http://tempuri.org/", Name = "Retorna_Data_Hora")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RetornaDataHora : System.Web.Services.WebService
    {
        string RetornaDataHoraServidorAtual;
        private MySqlConnection GetConnection()
        {
            string sql = Classes.Connection.Host_Bank;
            return new MySqlConnection(sql);

        }

        /// <summary>
        /// Tipo 1 = Retorna Data e Hora
        /// Tipo 2 = Retorna Data
        /// Tipo 3 = Retorna Hora
        /// Tipo 4 = Retorna Data e Hora tratado - csv
        /// </summary>
        /// <param name="Tipo"></param>
        /// <returns>RetornaDataHoraServidorAtual</returns>
        [WebMethod]
        public string RetornaDataHoraServidor_MySql(int Tipo)
        {

            using (MySqlConnection con = GetConnection())

                // Retorna Data e Hora
                if (Tipo == 1)
                {
                    con.Open();
                    string sql = "SELECT CURRENT_TIMESTAMP()";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        RetornaDataHoraServidorAtual = dt.Rows[0]["CURRENT_TIMESTAMP()"].ToString();
                    }
                }

                // Retorna Data
                else if (Tipo == 2)
                {
                    con.Open();
                    string sql = "select CURDATE() AS DATA;";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        RetornaDataHoraServidorAtual = dt.Rows[0]["DATA"].ToString().Replace("0000-00-00", "00/00/0000").Replace("00:00:00", "");
                    }
                }

                //Retorna Hora
                else if (Tipo == 3)
                {
                    con.Open();
                    string sql = "select CURRENT_TIME from rdb$database";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        RetornaDataHoraServidorAtual = dt.Rows[0]["CURRENT_TIME"].ToString();
                    }
                }
                // Retorna Data e Hora tratado - csv
                else if (Tipo == 4)
                {
                    con.Open();
                    string sql = "select current_timestamp from rdb$database";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        RetornaDataHoraServidorAtual = dt.Rows[0]["current_timestamp"].ToString().Replace(":", ".").Replace("/", "-");
                    }
                }

            return RetornaDataHoraServidorAtual;

        }
    }
}
