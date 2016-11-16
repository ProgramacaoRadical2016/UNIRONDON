using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;


namespace wsClinVitta
{
    /// <summary>
    /// Summary description for ControleDb_CadastroCliente
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ControleDb_CadastroCliente : System.Web.Services.WebService
    {
        int StatusCadastro = 0;
        string strPesquisa;


        private MySqlConnection GetConnection()
        {
            string sql = Classes.Connection.Host_Bank;
            return new MySqlConnection(sql);

        }


        #region Incluir Dados Clientes Cabeçalho no Db
        [WebMethod(Description = "Cadastra Cliente Cabeçalho na Base de Dados")]
        public int IncluirCadastroClienteCabecalho(string CODPACIENTE, string NOME, string CPF,
            string RG, string EMISSOR, string UF_EMISSAO, string SEXO, string ESTADO_CIVIL,
            string NOME_MAE, string NOME_PAI, string UF, string CIDADE, string DT_EMISSAO, string BAIRRO,
            string PONTO_REFERENCIA, string TIPO_LOGRADOURO, string END_NUM, string DESC_LOGRADOURO,
           string USER_CADASTRO, string EMAIL, string TIPO_DOC,
            string QT_DEPENDENTES, string FILIAL_CAD, string DATA_NASCIMENTO, string PROFISSAO, string CEP, string DTCADASTRO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" INSERT INTO  MV_CAD_PACIENTES (CODPACIENTE, NOME, CPF, RG, EMISSOR, UF_EMISSAO, ESTADO_CIVIL,NOME_MAE, NOME_PAI, UF,  CIDADE, DT_EMISSAO, BAIRRO, PONTO_REFERENCIA, USER_CADASTRO,EMAIL, TIPO_DOC, QT_DEPENDENTES, FILIAL_CAD, DATA_NASCIMENTO, PROFISSAO, CEP, DTCADASTRO)");
            sb.AppendLine(" VALUES (@CODPACIENTE, @NOME, @CPF, @RG, @EMISSOR, @UF_EMISSAO, @ESTADO_CIVIL,@NOME_MAE, @NOME_PAI, @UF,  @CIDADE, @DT_EMISSAO, @BAIRRO, @PONTO_REFERENCIA, @USER_CADASTRO,@EMAIL, @TIPO_DOC, @QT_DEPENDENTES, @FILIAL_CAD, @DATA_NASCIMENTO, @PROFISSAO, @CEP, @DTCADASTRO)");
            MySqlParameter[] pmts = new MySqlParameter[27];
            pmts[0] = new MySqlParameter("@CODPACIENTE", CODPACIENTE);
            pmts[1] = new MySqlParameter("@NOME", NOME);
            pmts[2] = new MySqlParameter("@CPF", CPF);
            pmts[3] = new MySqlParameter("@RG", RG);
            pmts[4] = new MySqlParameter("@EMISSOR", EMISSOR);
            pmts[5] = new MySqlParameter("@UF_EMISSAO", UF_EMISSAO);
            pmts[6] = new MySqlParameter("@SEXO", SEXO);
            pmts[7] = new MySqlParameter("@ESTADO_CIVIL", ESTADO_CIVIL);
            pmts[8] = new MySqlParameter("@NOME_MAE", NOME_MAE);
            pmts[9] = new MySqlParameter("@NOME_PAI", NOME_PAI);
            pmts[10] = new MySqlParameter("@UF", UF);
            pmts[11] = new MySqlParameter("@CIDADE", CIDADE);

            if (DT_EMISSAO != "")
                pmts[12] = new MySqlParameter("@DT_EMISSAO", Classes.NewValidacao.ConvertDataMySql(DT_EMISSAO));
            else
                pmts[12] = new MySqlParameter("@DT_EMISSAO", null);


            pmts[13] = new MySqlParameter("@BAIRRO", BAIRRO);
            pmts[14] = new MySqlParameter("@PONTO_REFERENCIA", PONTO_REFERENCIA);
            pmts[15] = new MySqlParameter("@TIPO_LOGRADOURO", TIPO_LOGRADOURO);
            pmts[16] = new MySqlParameter("@END_NUM", END_NUM);
            pmts[17] = new MySqlParameter("@DESC_LOGRADOURO", DESC_LOGRADOURO);
            pmts[18] = new MySqlParameter("@USER_CADASTRO", USER_CADASTRO);
            pmts[19] = new MySqlParameter("@EMAIL", EMAIL);
            pmts[20] = new MySqlParameter("@TIPO_DOC", TIPO_DOC);
            pmts[21] = new MySqlParameter("@QT_DEPENDENTES", QT_DEPENDENTES);
            pmts[22] = new MySqlParameter("@FILIAL_CAD", FILIAL_CAD);


            if (DATA_NASCIMENTO != "")
                pmts[23] = new MySqlParameter("@DATA_NASCIMENTO", Classes.NewValidacao.ConvertDataMySql(DATA_NASCIMENTO));
            else
                pmts[23] = new MySqlParameter("@DATA_NASCIMENTO", null);


            pmts[24] = new MySqlParameter("@PROFISSAO", PROFISSAO);
            pmts[25] = new MySqlParameter("@CEP", CEP);


            if (DTCADASTRO != "")
                pmts[26] = new MySqlParameter("@DTCADASTRO", Classes.NewValidacao.ConvertDataMySql(DTCADASTRO));
            else
                pmts[26] = new MySqlParameter("@DTCADASTRO", null);


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


                    StatusCadastro = cmd.ExecuteNonQuery();


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

            return StatusCadastro;
        }

        #endregion

        #region Altera Dados Cabeçalho Clientes no Db
        [WebMethod(Description = "Cadastra Cliente Cabeçalho na Base de Dados")]
        public int AlterarCadastroClienteCabecalho(string CODPACIENTE, string NOME, string CPF,
            string RG, string EMISSOR, string UF_EMISSAO, string SEXO, string ESTADO_CIVIL,
            string NOME_MAE, string NOME_PAI, string UF, string CIDADE, string DT_EMISSAO, string BAIRRO,
            string PONTO_REFERENCIA, string TIPO_LOGRADOURO, string END_NUM, string DESC_LOGRADOURO,
           string USER_CADASTRO, string EMAIL, string TIPO_DOC,
            string QT_DEPENDENTES, string FILIAL_CAD, string DATA_NASCIMENTO, string PROFISSAO, string CEP, string DTCADASTRO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" UPDATE MV_CAD_PACIENTES SET NOME = @NOME, CPF=@CPF, RG=@RG, EMISSOR=@EMISSOR, UF_EMISSAO=@UF_EMISSAO, ESTADO_CIVIL=@ESTADO_CIVIL,NOME_MAE=@NOME_MAE, NOME_PAI=@NOME_PAI, UF=@UF,  CIDADE=@CIDADE, DT_EMISSAO=@DT_EMISSAO, BAIRRO=@BAIRRO, PONTO_REFERENCIA=@PONTO_REFERENCIA, USER_CADASTRO=@USER_CADASTRO,EMAIL=@EMAIL, TIPO_DOC=@TIPO_DOC, QT_DEPENDENTES=@QT_DEPENDENTES, FILIAL_CAD=@FILIAL_CAD, DATA_NASCIMENTO=@DATA_NASCIMENTO, PROFISSAO=@PROFISSAO, CEP=@CEP, DTCADASTRO=@DTCADASTRO)");
            sb.AppendLine(" WHERE CODPACIENTE = @CODPACIENTE");
            MySqlParameter[] pmts = new MySqlParameter[27];
            pmts[0] = new MySqlParameter("@CODPACIENTE", CODPACIENTE);
            pmts[1] = new MySqlParameter("@NOME", NOME);
            pmts[2] = new MySqlParameter("@CPF", CPF);
            pmts[3] = new MySqlParameter("@RG", RG);
            pmts[4] = new MySqlParameter("@EMISSOR", EMISSOR);
            pmts[5] = new MySqlParameter("@UF_EMISSAO", UF_EMISSAO);
            pmts[6] = new MySqlParameter("@SEXO", SEXO);
            pmts[7] = new MySqlParameter("@ESTADO_CIVIL", ESTADO_CIVIL);
            pmts[8] = new MySqlParameter("@NOME_MAE", NOME_MAE);
            pmts[9] = new MySqlParameter("@NOME_PAI", NOME_PAI);
            pmts[10] = new MySqlParameter("@UF", UF);
            pmts[11] = new MySqlParameter("@CIDADE", CIDADE);

            if (DT_EMISSAO != "")
                pmts[12] = new MySqlParameter("@DT_EMISSAO", Classes.NewValidacao.ConvertDataMySql(DT_EMISSAO));
            else
                pmts[12] = new MySqlParameter("@DT_EMISSAO", null);


            pmts[13] = new MySqlParameter("@BAIRRO", BAIRRO);
            pmts[14] = new MySqlParameter("@PONTO_REFERENCIA", PONTO_REFERENCIA);
            pmts[15] = new MySqlParameter("@TIPO_LOGRADOURO", TIPO_LOGRADOURO);
            pmts[16] = new MySqlParameter("@END_NUM", END_NUM);
            pmts[17] = new MySqlParameter("@DESC_LOGRADOURO", DESC_LOGRADOURO);
            pmts[18] = new MySqlParameter("@USER_CADASTRO", USER_CADASTRO);
            pmts[19] = new MySqlParameter("@EMAIL", EMAIL);
            pmts[20] = new MySqlParameter("@TIPO_DOC", TIPO_DOC);
            pmts[21] = new MySqlParameter("@QT_DEPENDENTES", QT_DEPENDENTES);
            pmts[22] = new MySqlParameter("@FILIAL_CAD", FILIAL_CAD);


            if (DATA_NASCIMENTO != "")
                pmts[23] = new MySqlParameter("@DATA_NASCIMENTO", Classes.NewValidacao.ConvertDataMySql(DATA_NASCIMENTO));
            else
                pmts[23] = new MySqlParameter("@DATA_NASCIMENTO", null);


            pmts[24] = new MySqlParameter("@PROFISSAO", PROFISSAO);
            pmts[25] = new MySqlParameter("@CEP", CEP);


            if (DTCADASTRO != "")
                pmts[26] = new MySqlParameter("@DTCADASTRO", Classes.NewValidacao.ConvertDataMySql(DTCADASTRO));
            else
                pmts[26] = new MySqlParameter("@DTCADASTRO", null);


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


                    StatusCadastro = cmd.ExecuteNonQuery();


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

            return StatusCadastro;
        }

        #endregion

        #region Excluir Cadastro Cliente
        [WebMethod(Description = "Exclui Cadastro do PACIENTE do Banco de Dados")]
        public void ExcluirCadastroCliente(string CODPACIENTE)
        {
            #region Excluir Cadastro na MV_CAD_CLIENTES
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("delete from MV_CAD_PACIENTES");
            sb.AppendLine("WHERE CODPACIENTE =@CODPACIENTE");
            MySqlParameter[] pmts = new MySqlParameter[1];
            pmts[0] = new MySqlParameter("@CODPACIENTE", CODPACIENTE);
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


                    StatusCadastro = cmd.ExecuteNonQuery();


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
            #endregion

            #region EXCLUI OS DADOS DE TELEFONE
            sb = new StringBuilder();
            sb.AppendLine(" DELETE FROM MV_CAD_PACIENTES_TELEFONE");
            sb.AppendLine(" WHERE CODPACIENTE = @CODPACIENTE");
            pmts = new MySqlParameter[1];
            pmts[0] = new MySqlParameter("@CODPACIENTE", CODPACIENTE);

            cmd = new MySqlCommand();
            for (byte i = 0; i < pmts.Length; i++)
            {
                cmd.Parameters.Add(pmts[i]);
            }

            conn = GetConnection();
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sb.ToString();


                    StatusCadastro = cmd.ExecuteNonQuery();


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
            #endregion


        }
        #endregion

        #region Outros...

        [WebMethod(Description = "Inclui Somente o Telefone na MV_CAD_PACIENTES_TELEFONE")]
        public int incluir_TELEFONE(string TELCODPACIENTE, string TIPO, string DDD, string NUMERO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" INSERT INTO  MV_CAD_PACIENTES_TELEFONE (CODCLI,TIPO,DDD,NUMERO)");
            sb.AppendLine(" VALUES (@CODPACIENTE,@TIPO,@DDD,@NUMERO)");

            MySqlParameter[] pmts = new MySqlParameter[4];
            pmts[0] = new MySqlParameter("@CODPACIENTE", TELCODPACIENTE);
            pmts[1] = new MySqlParameter("@TIPO", TIPO);
            pmts[2] = new MySqlParameter("@DDD", DDD);
            pmts[3] = new MySqlParameter("@NUMERO", NUMERO);

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


                    StatusCadastro = cmd.ExecuteNonQuery();


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
                return StatusCadastro;
            }
        }



        [WebMethod(Description = "ALTERA Somente o Telefone na MV_CAD_PACIENTE_TELEFONE")]
        public int Alterar_TELEFONE(string CODPACIENTE, string TIPO, string DDD, string NUMERO)
        {
            try
            {
                EXCLUIR_TELEFONE(CODPACIENTE);
                incluir_TELEFONE(CODPACIENTE, TIPO, DDD, NUMERO);

                StatusCadastro = 1;
            }
            catch (Exception)
            {
                StatusCadastro = 0;
                throw;
            }
            return StatusCadastro;
        }



        [WebMethod(Description = "EXCLUI Somente o Telefone na MV_CAD_PACIENTE_TELEFONE")]
        public int EXCLUIR_TELEFONE(string CODPACIENTE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" DELETE FROM MV_CAD_PACIENTES_TELEFONE");
            sb.AppendLine(" WHERE CODPACIENTE = @CODPACIENTE");

            MySqlParameter[] pmts = new MySqlParameter[1];
            pmts[0] = new MySqlParameter("@CODPACIENTE", CODPACIENTE);

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


                    StatusCadastro = cmd.ExecuteNonQuery();


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
                return StatusCadastro;
            }
        }

        #endregion



    }
}
