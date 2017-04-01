//using OleDb.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
        private static string connect_string = "Provider=OraOLEDB.Oracle.1;Server=localhost;Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.18.3.229)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = ORCL)));User ID = lioliu; Password = 647094; ";
        private static string PowerConnectString = "Provider=OraOLEDB.Oracle.1;Server=localhost;Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.18.3.229)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = ORCL)));User ID = lioliu; Password = 647094; ";
        #region Execute sql
        /// <summary>
        /// Execute SQL statement 
        /// </summary>
        /// <param name="sql">Statement to be executed</param>
        /// <returns> affected rows</returns>
        public static int Execute_sql(string sql)
        {
            int count = 0;
            OleDbConnection con = new OleDbConnection(PowerConnectString);
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand(sql, con);
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
        public static int Execute_sql(List<string> sql_list)
        {
            int count = 0;
            OleDbConnection con = new OleDbConnection(PowerConnectString);
            con.Open();
            OleDbCommand cmd = new OleDbCommand()
            {
                Connection = con
            };
            //create the transaction
            OleDbTransaction transaction = con.BeginTransaction();
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
                Console.Write(error);
                //LogMessage(error.Message, "log-error");
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
        public static DataSet Get_data(string sql)
        {
            DataSet data_set = new DataSet();
            DataTable data_table = new DataTable();
            OleDbDataAdapter oda = null;
            try
            {
                oda = new OleDbDataAdapter(sql, connect_string);
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
        public static DataSet Get_data(List<string> list_sql)
        {
            DataSet data_set = new DataSet();
            DataTable data_table = new DataTable();
            OleDbDataAdapter oda = null;
            OleDbConnection con = new OleDbConnection(connect_string);
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand()
                {
                    Connection = con
                };
                foreach (string sql in list_sql)
                {
                    cmd.CommandText = sql;
                    oda = new OleDbDataAdapter(cmd);
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
        /// execute the procedure to get the data format as OleDb parameter
        /// </summary>
        /// <param name="procedure_name">procedure to be execute</param>
        /// <param name="parms">OleDb parameters</param>
        /// <returns>result parameters</returns>
        public static OleDbParameter[] Execute_procedure(string procedure_name,OleDbParameter[] parms)
        {
            DataTable data_table = new DataTable();
            OleDbConnection con = new OleDbConnection(connect_string);
            OleDbCommand cmd = new OleDbCommand();
            DataSet data_set = new DataSet();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedure_name;
            try
            {
                con.Open();
                cmd.Connection = con;
                foreach (OleDbParameter parm in parms)
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
        public static void Execute_procedure(string procedure_name,OleDbParameter[] parms,out DataSet data_set)
        {
            DataTable data_table = new DataTable();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            OleDbConnection con = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = procedure_name
            };
            data_set = new DataSet();
            try
            {
                con.Open();
                cmd.Connection = con;
                foreach (OleDbParameter parm in parms)
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
          
        }
        /// <summary>
        /// get stock list
        /// </summary>
        /// <returns></returns>
        public static DataSet Get_stock_list()
        {
            return Get_data("select Code from stock_list");
        }
        /// <summary>
        /// return the stock list in 
        /// </summary>
        /// <returns></returns>
        public static string[] GetStockList()
        {
            DataSet ds = Get_data("select Code , Name from stock_list");
            string[] list = new string[ds.Tables[0].Rows.Count];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list[i] = ds.Tables[0].Rows[i][0].ToString()+" "+ ds.Tables[0].Rows[i][1].ToString();
            }
            return list;
        }

    }
}
