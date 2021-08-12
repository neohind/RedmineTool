using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RedmineTool.Common.Dac
{
    /// <summary>
    /// 소켓 기반의 데이터 개체에 접속할 때 사용할 DB 연결 Agent 최상위 클래스
    /// </summary>
    public class DbAgent
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 특정 Mode에 따라 DB 개체를 생성한다.
        /// </summary>
        /// <param name="mode">생성할 DB 개체 형식</param>
        /// <param name="sConnectionString">DB 연결 문자열</param>
        /// <returns></returns>
        public static DbAgent CreateInstance(string sDatabaseAddress, string sDbUserName, string sDbUserPassword, string sDatabasePort = "3306", string sDatabaseName = "")
        {
            string sConnectionString = GetConnectionString(sDatabaseAddress, sDbUserName, sDbUserPassword, sDatabasePort, sDatabaseName);

            DbAgent agent = new DbAgent();
            agent.ConnectionString = sConnectionString;
            
            return agent;
        }


        static public bool CanConnect(string sDatabaseAddress, string sDbUserName, string sDbUserPassword, string sDatabasePort = "3306", string sDatabaseName = "")
        {
            string sConnectionString = GetConnectionString(sDatabaseAddress, sDbUserName, sDbUserPassword, sDatabasePort, sDatabaseName);

            DbAgent agent = new DbAgent();
            agent.ConnectionString = sConnectionString;

            return agent.CanConnect();
        }


        static private string GetConnectionString(string sDatabaseAddress, string sDbUserName, string sDbUserPassword, string sDatabasePort = "3306", string sDatabaseName = "")
        {
            if (string.IsNullOrEmpty(sDatabaseName))
                sDatabaseName = "redmine";

            StringBuilder sbConnectionString = new StringBuilder();
            sbConnectionString.Append($"server={sDatabaseAddress};");
            sbConnectionString.Append($"port={sDatabasePort};");
            sbConnectionString.Append($"uid={sDbUserName};");
            sbConnectionString.Append($"pwd={sDbUserPassword};");
            sbConnectionString.Append($"database={sDatabaseName};");
            sbConnectionString.Append(";SslMode=none;");

            return sbConnectionString.ToString();
        }


        private bool CanConnect()
        {
            bool bResult = false;
            try
            {
                string sConectionSTring = this.ConnectionString + "Connect Timeout=3";
                using (MySqlConnection connection = new MySqlConnection(sConectionSTring))
                {

                    connection.Open();
                    bResult = connection.State == ConnectionState.Open;
                }
            }
            catch (InvalidOperationException ex)
            {
                log.Error(ex);
            }
            catch (InvalidCastException ex)
            {
                log.Error(ex);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return bResult;
        }

        public string ConnectionString
        {
            get;
            private set;
        }

        /// <summary>
        /// 쿼리를 실행 한뒤 DataTable 형식으로 값을 가져온다.
        /// </summary>
        /// <param name="set">쿼리값이 담긴 개체</param>
        /// <returns>DataTable 형식의 결과값</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public DataTable GetDataTable(UDataQuerySet set)
        {
            DataTable dtResult = new DataTable();

            MySqlConnection connection = new MySqlConnection(this.ConnectionString);
            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter();
            MySqlCommand sqlCommand = null;
            try
            {
                connection.Open();
                log.Debug($"Connection for get DataTable Success! Qry is : {set.Query}");
                sqlCommand = new MySqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = set.Query;
                sqlCommand.CommandTimeout = set.Timeout;
                sqlCommand.CommandType = set.CmdType;
                sqlAdapter.SelectCommand = sqlCommand;

                MakeParamter(sqlCommand.Parameters, set);

                sqlAdapter.Fill(dtResult);

                RetriveOutParamter(sqlCommand.Parameters, set);

            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            catch (InvalidCastException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (sqlCommand != null)
                        sqlCommand.Dispose();

                    if (sqlAdapter != null)
                        sqlAdapter.Dispose();

                    if (connection != null && connection.State != ConnectionState.Closed)
                        connection.Close();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
            if (dtResult != null)
            {
                log.Debug("DataTable result get Success");
            }
            return dtResult;
        }

        /// <summary>
        /// 쿼리를 실행 한뒤 DataSet 형식으로 값을 가져온다.
        /// </summary>
        /// <param name="set">쿼리값이 담긴 개체</param>
        /// <returns>DataSet 형식의 결과값</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails")]
        public DataSet GetDataSet(UDataQuerySet set)
        {
            DataSet dsResult = new DataSet();
            MySqlConnection connection = new MySqlConnection(this.ConnectionString);
            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter();
            MySqlCommand sqlCommand = null;
            try
            {
                connection.Open();
                log.Debug($"Connection for get DataSet Success! Qry is : {set.Query}");
                sqlCommand = new MySqlCommand();
                sqlCommand.CommandTimeout = set.Timeout;
                sqlCommand.CommandType = set.CmdType;
                sqlCommand.CommandText = set.Query;
                sqlCommand.Connection = connection;

                sqlAdapter.SelectCommand = sqlCommand;


                MakeParamter(sqlCommand.Parameters, set);

                sqlAdapter.Fill(dsResult);

                RetriveOutParamter(sqlCommand.Parameters, set);

            }
            catch (NullReferenceException ex)
            {
                log.Error(ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (sqlCommand != null)
                        sqlCommand.Dispose();

                    if (sqlAdapter != null)
                        sqlAdapter.Dispose();

                    if (connection != null && connection.State != ConnectionState.Closed)
                        connection.Close();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
            if (dsResult != null)
            {
                log.Debug("DataTable result get Success");
            }
            return dsResult;
        }


        /// <summary>
        /// 쿼리를 실행 한다.
        /// </summary>
        /// <param name="aryQuerySet">쿼리 셋 목록</param>
        /// <returns>실행 결과 변경된 Row 갯수</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public int ExecuteQuery(List<UDataQuerySet> aryQuerySet)
        {
            int affectedRows = 0;
            MySqlConnection connection = new MySqlConnection(this.ConnectionString);
            MySqlCommand sqlCommand = null;
            string sCurrentExecuteQuery = string.Empty;
            try
            {
                connection.Open();
                log.Debug("Connection for get ExecuteQuery Success!");
                foreach (UDataQuerySet set in aryQuerySet)
                {
                    sqlCommand = new MySqlCommand();
                    sqlCommand.CommandTimeout = set.Timeout;
                    sqlCommand.CommandType = set.CmdType;
                    sqlCommand.CommandText = set.Query;
                    sqlCommand.Connection = connection;

                    sCurrentExecuteQuery = set.Query;
                    MakeParamter(sqlCommand.Parameters, set);

                    affectedRows += sqlCommand.ExecuteNonQuery();

                    RetriveOutParamter(sqlCommand.Parameters, set);

                    sqlCommand.Parameters.Clear();
                    sqlCommand.Dispose();
                    sqlCommand = null;
                }
            }
            catch (NullReferenceException ex)
            {
                log.Error(ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (sqlCommand != null)
                        sqlCommand.Dispose();

                    if (connection != null && connection.State != ConnectionState.Closed)
                        connection.Close();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
            return affectedRows;
        }

        /// <summary>
        /// 쿼리를 실행 한 뒤 단일 값을 가져온다.
        /// </summary>
        /// <typeparam name="T">결과값 단일 값을 위한 템플릿</typeparam>
        /// <param name="set">쿼리값이 담긴 개체</param>
        /// <returns>T 로 캐스팅된 결과 값</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public T GetValue<T>(UDataQuerySet set)
        {
            T value = default(T);

            MySqlConnection connection = new MySqlConnection(this.ConnectionString);
            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter();
            MySqlCommand sqlCommand = null;
            try
            {
                connection.Open();
                sqlCommand = new MySqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = set.CmdType;
                sqlCommand.CommandText = set.Query;
                sqlCommand.CommandTimeout = set.Timeout;

                MakeParamter(sqlCommand.Parameters, set);

                object objResult = sqlCommand.ExecuteScalar();
                if (objResult != null && DBNull.Value.Equals(objResult) == false)
                {
                    value = (T)Convert.ChangeType(objResult, typeof(T));
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            catch (InvalidCastException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (sqlCommand != null)
                        sqlCommand.Dispose();

                    if (sqlAdapter != null)
                        sqlAdapter.Dispose();

                    if (connection != null && connection.State == ConnectionState.Open)
                        connection.Close();

                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
            return value;
        }

        /// <summary>
        /// UDataQuery에 담긴 Paramter를 DB 개체에 맞게 Paramter를 구성한다.
        /// </summary>
        /// <param name="parameters">DB 개체에 담긴 Paramter Collection 개체</param>
        /// <param name="set">쿼리값이 담긴 개체</param>
        private void MakeParamter(MySqlParameterCollection parameters, UDataQuerySet set)
        {
            foreach (string sKey in set.ParametersKeys)
            {
                MySqlParameter param = new MySqlParameter(sKey, set.GetParamValue(sKey));
                parameters.Add(param);
            }

            foreach (string sKey in set.OutParametersKeys)
            {
                MySqlParameter param = new MySqlParameter();
                param.ParameterName = sKey;
                param.Size = set.GetOutParamSize(sKey);
                param.Direction = ParameterDirection.Output;
                parameters.Add(param);
            }
        }

        /// <summary>
        /// 쿼리 실행 후 Return 된 Output 파라미터를 추출한다.
        /// </summary>
        /// <param name="parameter">결과값이 담긴 모든 DB 개체의Paramter Collection 개체</param>
        /// <param name="set">쿼리값이 담긴 개체</param>
        private void RetriveOutParamter(MySqlParameterCollection parameter, UDataQuerySet set)
        {
            foreach (MySqlParameter param in parameter)
            {
                if (param.Direction == ParameterDirection.Output
                    || param.Direction == ParameterDirection.InputOutput)
                {
                    set.SetOutParam(param.ParameterName, param.Value);
                }
            }
        }


    }
}
