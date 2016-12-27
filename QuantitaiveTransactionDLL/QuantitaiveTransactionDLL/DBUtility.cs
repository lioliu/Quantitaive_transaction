using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace QuantitaiveTransactionDLL
{
    /// <summary>
    ///  connect to the database
    /// </summary>
    public class DBUtility 
    {
        /// <summary>
        ///  connect string  normal for Query , power connect for Update\Delete\Insert
        /// </summary>
        private static string connect_string = "Data Source=( DESCRIPTION = ( ADDRESS = ( PROTOCOL = TCP ) ( HOST = 10.18.3.229 ) ( PORT = 1521 ) ) ( CONNECT_DATA = ( SERVICE_NAME=ORCL ) ) );" +
            "user id=lioliu;password=647094;"; 
        private static string power_connect_string = "Data Source=( DESCRIPTION = ( ADDRESS = ( PROTOCOL = TCP ) ( HOST = 10.18.3.229 ) ( PORT = 1521 ) ) ( CONNECT_DATA = ( SERVICE_NAME=ORCL ) ) );" +
            "user id=lioliu;password=647094;";
        #region Execute sql
        /// <summary>
        /// Execute SQL statement 
        /// </summary>
        /// <param name="sql">Statement to be executed</param>
        /// <returns> affected rows</returns>
        public static int execute_sql(string sql)
        {
            int count = 0;
            OracleConnection con = new OracleConnection(power_connect_string);
            try
            {
                con.Open();
                OracleCommand cmd = new OracleCommand(sql, con);
                //cmd.Connection = con;
                //cmd.CommandText = sql;
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                //mod here
                LogMessage(error.Message, "log-error");
                throw;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return count;
        }
        /// <summary>
        /// Execute SQL statement
        /// </summary>
        /// <param name="sql_list">the list of Statements to be executed</param>
        /// <returns> affected rows</returns>
        public static int execute_sql(List<string> sql_list)
        {
            int count = 0;
            OracleConnection con = new OracleConnection(power_connect_string);
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            //create the transaction
            OracleTransaction transaction = con.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                foreach (string sql in sql_list)
                {
                    cmd.CommandText = sql;
                    count += cmd.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (Exception error)
            {
                transaction.Rollback();
                LogMessage(error.Message, "log-error");
                throw;
            }
            finally
            {
                con.Close();
                con.Dispose();
                transaction.Dispose();
                cmd.Dispose();
            }
            return count;
        }
        #endregion
        #region get data
        /// <summary>
        /// get the data by execute the sql
        /// </summary>
        /// <param name="sql">query statement</param>
        /// <returns>result data</returns>
        public static DataSet get_data(string sql)
        {
            DataSet data_set = new DataSet();
            DataTable data_table = new DataTable();
            OracleDataAdapter oda = null;
            try
            {
                oda = new OracleDataAdapter(sql, connect_string);
                oda.Fill(data_table);
                
                data_set.Tables.Add(data_table);
                oda.Dispose();

            }
            catch (Exception error)
            {
                LogMessage(error.Message, "log-error");
                throw;
            }
            finally
            {
                oda.Dispose();
                data_table.Dispose();
            }

            return data_set;
        }

        /// <summary>
        /// Execute SQL statement 
        /// </summary>
        /// <param name="list_sql">the list of the statements to be execute</param>
        /// <returns>result data</returns>
        public static DataSet get_data(List<string> list_sql)
        {
            DataSet data_set = new DataSet();
            DataTable data_table = new DataTable();
            OracleDataAdapter oda = null;
            OracleConnection con = new OracleConnection(connect_string);
            try
            {
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                foreach (string sql in list_sql)
                {
                    cmd.CommandText = sql;
                    oda = new OracleDataAdapter(cmd);
                    oda.Fill(data_table);
                    data_set.Tables.Add(data_table);
                    data_table = new DataTable();
                }
            }
            catch (Exception error)
            {
                LogMessage(error.Message, "log-error");
                throw;
            }
            finally
            {
                con.Dispose();
                oda.Dispose();
                data_table.Dispose();
            }

            return data_set;
        }

        #endregion
        #region execute procedure

        /// <summary>
        /// execute the procedure to get the data format as oracle parameter
        /// </summary>
        /// <param name="procedure_name">procedure to be execute</param>
        /// <param name="parms">oracle parameters</param>
        /// <returns>result parameters</returns>
        public static OracleParameter[] execute_procedure(string procedure_name,OracleParameter[] parms)
        {
            DataTable data_table = new DataTable();
            OracleConnection con = new OracleConnection(connect_string);
            OracleCommand cmd = new OracleCommand();
            DataSet data_set = new DataSet();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedure_name;
            try
            {
                con.Open();
                cmd.Connection = con;
                foreach (OracleParameter parm in parms)
                {
                    cmd.Parameters.Add(parm);
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                LogMessage(procedure_name + " : " + error.Message, "log-error");
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Dispose();
                con.Dispose();
            }
            return parms;
        }

        /// <summary>
        /// execute procedure get the data format as dataset
        /// </summary>
        /// <param name="procedure_name">procedure to be execute</param>
        /// <param name="parms">pracle parameters</param>
        /// <param name="data_set">the data set to get the data</param>
        public static void execute_procedure(string procedure_name,OracleParameter[] parms,out DataSet data_set)
        {
            DataTable data_table = new DataTable();
            OracleDataAdapter oda = new OracleDataAdapter();
            OracleConnection con = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedure_name;
            data_set = new DataSet();
            try
            {
                con.Open();
                cmd.Connection = con;
                foreach (OracleParameter parm in parms)
                {
                    cmd.Parameters.Add(parm);
                }
                oda.SelectCommand = cmd;
                oda.Fill(data_table);
                data_set.Tables.Add(data_table);
            }
            catch (Exception error)
            {
                LogMessage(procedure_name + " : " + error.Message, "log-error");
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Dispose();
                cmd.Parameters.Clear();
                cmd.Dispose();
                oda.Dispose();
            }
        }

        #endregion

        /// <summary>
        /// write log message
        /// </summary>
        /// <param name="outPutStr">log infor</param>
        /// <param name="type">log type</param>
        public static void LogMessage(string outPutStr, string type)
        {
            /*write log as file !!! not use database*/
            //try
            //{
            //    //连接日志数据库语句
            //    SqlConnection con = new SqlConnection("Server=202.121.241.77;Database=YANG\\SQLEXPRESS_SSPU;uid=datashow;pwd=TopoDA4SSPU.");
            //    string[] str = outPutStr.Split(',');
            //    string sql = string.Format("INSERT INTO [log] ([STUNO],[NAME],[IP],[Brower],[System],[Time],[other],[type]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", str[0], str[1], str[2], str[3], str[4], str[5], str[6], type);
            //    SqlCommand cmd = new SqlCommand(sql, con);
            //    con.Open();
            //    cmd.ExecuteNonQuery();
            //    con.Dispose();
            //}
            //catch (Exception e)
            //{
            //    //写入日志文件
            //    string path1 = string.Format("../log/{0}_SystemError.txt", DateTime.Now.ToString("yyyyMMdd"));
            //    string strFilePath1 = System.Web.HttpContext.Current.Server.MapPath(path1);
            //    System.IO.FileStream fs1 = new System.IO.FileStream(strFilePath1, System.IO.FileMode.Append);
            //    System.IO.StreamWriter sw1 = new System.IO.StreamWriter(fs1, System.Text.Encoding.Default);
            //    sw1.WriteLine(e.Message);
            //    sw1.Close();
            //    fs1.Close();
            //}
            //写入日志文件
            string path = string.Format("\\log\\{1}_{0}.txt", DateTime.Now.Date.ToString("yyyyMMdd"), type);
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Append);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);
            sw.WriteLine(outPutStr);
            sw.Close();
            fs.Close();
        }
    }
}
