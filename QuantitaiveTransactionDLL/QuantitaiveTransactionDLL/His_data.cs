using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json;

namespace QuantitaiveTransactionDLL
{


    public class Kline
    {
        public string Date { get; set; }
        public double Open { set; get; }
        public double High { set; get; }
        public double Low { set; get; }
        public double Close { set; get; }
        public double Amount { set; get; }

    }
    public class His_data
    {
        public string Code { set; get; }
        public string Total { set; get; }
        public string Begin { set; get; }
        public string End { set; get; }
        public Kline[] Kline { set; get; }

        /// <summary>
        /// tihis should runned carefully
        /// </summary>
        public static void LoadHisData()
        {
            //clearn the data in database 
            
            DBUtility.Execute_sql("DELETE FROM STOCK_HIS_DATA");

            //get the stock list

            DataSet stockList = DBUtility.Get_stock_list();
            
            string total;
            for (int i = 0; i < stockList.Tables[0].Rows.Count; i++)
            {
                total = GetHisData(stockList.Tables[0].Rows[i][0].ToString());
             
                DataTable dt = GetAllData(stockList.Tables[0].Rows[i][0].ToString(), total);
                
                Task.Factory.StartNew(() => SaveHisData(dt));

            }

        }

     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static int SaveHisData(DataTable dt)
        {
            string insert = string.Empty;
            List<string> insertscript = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                insert = "INSERT INTO STOCK_HIS_DATA VALUES" + $"('{dt.Rows[i]["CODE"]}','{dt.Rows[i]["DAYS"]}','{dt.Rows[i]["OPEN"]}','{dt.Rows[i]["HIGH"]}','{dt.Rows[i]["LOW"]}','{dt.Rows[i]["CLOSE"]}','{dt.Rows[i]["AMOUNT"]}')";
                insertscript.Add(insert);
            }
            int result = DBUtility.Execute_sql(insertscript);
            return result;

        }
        /// <summary>
        /// get the total number of the stock which the code was given
        /// </summary>
        /// <param name="code">stock code</param>
        /// <returns></returns>
        private static string GetHisData(string code)
        {
            Random rnd = new Random();
            Base_crawl crawl = new Base_crawl();
            string json = crawl.Run($"http://yunhq.sse.com.cn:32041/v1/sh1/dayk/{code}?callback=&select=date%2Copen%2Chigh%2Clow%2Cclose%2Cvolume&begin=-2&end=-1&_={rnd.Next()}");
            json = Json_formater.FormatHisData(json);
            His_data his_data = JsonConvert.DeserializeObject<His_data>(json);
            return his_data.Total;
        }
        /// <summary>
        /// get all history data(stock daily infor) into datatable
        /// </summary>
        /// <param name="code">the stock id</param>
        /// <param name="total"></param>
        /// <returns></returns>
        private static DataTable GetAllData(string code,string total)
        {
            Random rnd = new Random();
            Base_crawl crawl = new Base_crawl();
            string URL = $"http://yunhq.sse.com.cn:32041/v1/sh1/dayk/{code}?callback=&select=date%2Copen%2Chigh%2Clow%2Cclose%2Cvolume&begin=-{total}&end=-1&_={rnd.Next()}";
            string json = crawl.Run(URL);
            json = Json_formater.FormatHisData(json);
            His_data his_data = JsonConvert.DeserializeObject<His_data>(json);
            DataTable dt = ConvertToDataTable(his_data);
            return dt;
        }

        /// <summary>
        ///  Convert His data to Data Table
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static DataTable ConvertToDataTable(His_data data)
        {
            DataTable dt = new DataTable("stock_data");
            dt.Columns.Add("CODE", Type.GetType("System.String"));
            dt.Columns.Add("DAYS", Type.GetType("System.String"));
            dt.Columns.Add("OPEN", Type.GetType("System.Double"));
            dt.Columns.Add("HIGH", Type.GetType("System.Double"));
            dt.Columns.Add("LOW", Type.GetType("System.Double"));
            dt.Columns.Add("CLOSE", Type.GetType("System.Double"));
            dt.Columns.Add("AMOUNT", Type.GetType("System.Double"));
            DataRow Newrow;
            foreach (var item in data.Kline)
            {
                Newrow = dt.NewRow();
                Newrow["CODE"] = data.Code;
                Newrow["DAYS"] = item.Date;
                Newrow["OPEN"] = Convert.ToDouble(item.Open);
                Newrow["HIGH"] = Convert.ToDouble(item.High);
                Newrow["LOW"] = Convert.ToDouble(item.Low);
                Newrow["CLOSE"] = Convert.ToDouble(item.Close);
                Newrow["AMOUNT"] = Convert.ToDouble(item.Amount);
                dt.Rows.Add(Newrow);
            }
            return dt;
        }
        /// <summary>
        /// Convert today's line data to his data
        /// </summary>
        public static void ConvertLineToHis()
        {
            DBUtility.Execute_sql("insert into STOCK_HIS_DATA " +
                                "select base.code ,base.days,op.price,max(base.price),min(base.price),cl.price ,sum(volume) from STOCK_LINE_DATA base, " +
                                "(select code, price from stock_line_data where days = to_char(sysdate, 'yyyymmdd') and time = '93000') op," +
                                "(select code, price from stock_line_data where days = to_char(sysdate, 'yyyymmdd') and time = '150000')cl" +
                                " where base.code = op.code and base.code = cl.code " +
                                "and base.days = to_char(sysdate, 'yyyymmdd') " +
                                "group by base.code ,days,op.price,cl.price");
        }


    }

}
