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
    /// Summary description for PesquisaCEP
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PesquisaCEP : System.Web.Services.WebService
    {


        private string[] ConteudoCEP;
        string result;


        private MySqlConnection GetConnection()
        {
            string sql = Classes.Connection.Host_Bank;
            return new MySqlConnection(sql);

        }


        [WebMethod]
        public string BuscarCEP(string pNumCEP)
        {

            using (MySqlConnection con = GetConnection())
            {

                try
                {
                    con.Open();
                    string sql = "SELECT * FROM MV_CAD_CEP WHERE CEP LIKE '" + pNumCEP + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        string CmbUF = dt.Rows[0]["UF"].ToString();
                        string CmbCIDADE = dt.Rows[0]["LOCAL"].ToString();
                        string CmbBAIRRO = dt.Rows[0]["BAIRRO"].ToString();
                        string txtEND = dt.Rows[0]["ENDERECO"].ToString();
                        string CmbTPLOGRADOURO = dt.Rows[0]["LOGRADOURO"].ToString();

                        result = CmbUF + " ý " + CmbCIDADE + " ý " + CmbBAIRRO + " ý " + CmbTPLOGRADOURO + " ý " + txtEND;

                    }
                    else/// caso não encontre os dados na tabela de cep, procura no cep do correio
                    {
                        
                        ServiceAPICorreios.AtendeClienteClient apicorreios = new ServiceAPICorreios.AtendeClienteClient();
                        var resposta = apicorreios.consultaCEP(pNumCEP);
                        string UF = resposta.uf;
                        string cidade = resposta.cidade;
                        string bairro = resposta.bairro;
                        string end = resposta.end;

                        result = UF + " ý " + cidade + " ý " + bairro + " ý " + null + " ý " + end;
                    }

                }
                catch (Exception ex)
                {
                    ConteudoCEP = null;
                }
                finally
                {

                }

            }

            return result;
        }


    }
}
