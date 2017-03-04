using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json;
using QuantitaiveTransactionDLL;
using System.Diagnostics;
using System.Threading;

namespace stock_crawl
{
  
    
    class Kline
    {
        public string date { get; set; }
        public double open { set; get; }
        public double high { set; get; }
        public double low { set; get; }
        public double close { set; get; }

        public double amount { set; get; }

    }
    class His_data
    {
        public string code { set; get; }
        public string total { set; get; }
        public string begin { set; get; }
        public string end { set; get; }
        public Kline[] kline { set; get; }

        /// <summary>
        /// main funcation 
        /// </summary>
        public static void load_hisdata()
        {
            //clearn the data in database 
            ///add code here
          
            DBUtility.execute_sql("DELETE FROM STOCK_HIS_DATA");
           
            //get the stock list
            
            DataSet stock_list = get_stock_list();
            
            string total;
            for (int i = 0; i < stock_list.Tables[0].Rows.Count; i++)
            {
                //run the web crawl to get the total date for the stock
                //if (Convert.ToInt32(stock_list.Tables[0].Rows[i][0].ToString()) <= 600463) continue;
                total = get_total_date(stock_list.Tables[0].Rows[i][0].ToString());
               
           
                //run the web crawl with total data get all the his data
                
                DataTable dt = get_all_data(stock_list.Tables[0].Rows[i][0].ToString(), total);

                //save data to database
                Task.Factory.StartNew(() => save_to_database(dt));

                Console.WriteLine("success");
            }


        }

     

        public static int save_to_database(DataTable dt)
        {
            string insert = string.Empty;
            List<string> insertscript = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                insert = "INSERT INTO STOCK_HIS_DATA VALUES" + string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",dt.Rows[i]["CODE"],dt.Rows[i]["DAYS"],dt.Rows[i]["OPEN"],dt.Rows[i]["HIGH"], dt.Rows[i]["LOW"], dt.Rows[i]["CLOSE"], dt.Rows[i]["AMOUNT"]);
                insertscript.Add(insert);
                
            }
            int result = DBUtility.execute_sql(insertscript);
            Console.WriteLine(result);
            return result;

        }
        /// <summary>
        /// get the total number of the stock which the code was given
        /// </summary>
        /// <param name="code">stock code</param>
        /// <returns></returns>
        private static string get_total_date(string code)
        {
            Random rnd = new Random();
            base_crawl crawl = new base_crawl();
            string json = crawl.run("http://yunhq.sse.com.cn:32041/v1/sh1/dayk/"+code+ "?callback=&select=date%2Copen%2Chigh%2Clow%2Cclose%2Cvolume&begin=-2&end=-1&_=" +rnd.Next() );
            json = Json_formater.his_data(json);
            His_data his_data = JsonConvert.DeserializeObject<His_data>(json);
            return his_data.total;
        }
        /// <summary>
        /// get all history data(stock daily infor) into datatable
        /// </summary>
        /// <param name="code">the stock id</param>
        /// <param name="total"></param>
        /// <returns></returns>
        private static DataTable get_all_data(string code,string total)
        {
            Random rnd = new Random();
            base_crawl crawl = new base_crawl();
            string URL = String.Format("http://yunhq.sse.com.cn:32041/v1/sh1/dayk/{0}?callback=&select=date%2Copen%2Chigh%2Clow%2Cclose%2Cvolume&begin=-{1}&end=-1&_={2}", code,total,rnd.Next());
            string json = crawl.run(URL);
            json = Json_formater.his_data(json);
            His_data his_data = JsonConvert.DeserializeObject<His_data>(json);
            DataTable dt = covert_to_datatable(his_data);
            return dt;
        }

       
     
        /// <summary>
        /// get stock list
        /// </summary>
        /// <returns>the stock code list</returns>
        private static DataSet get_stock_list()
        {
            DataSet ds = DBUtility.get_data("select Code from stock_list");
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static DataTable covert_to_datatable(His_data data)
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
            foreach (var item in data.kline)
            {
                Newrow = dt.NewRow();
                Newrow["CODE"] = data.code;
                Newrow["DAYS"] = item.date;
                Newrow["OPEN"] = Convert.ToDouble(item.open);
                Newrow["HIGH"] = Convert.ToDouble(item.high);
                Newrow["LOW"] = Convert.ToDouble(item.low);
                Newrow["CLOSE"] = Convert.ToDouble(item.close);
                Newrow["AMOUNT"] = Convert.ToDouble(item.amount);
                dt.Rows.Add(Newrow);
            }
            return dt;
        }
        //His_data his_data = JsonConvert.DeserializeObject<His_data>(json);
        //Console.WriteLine(his_data.code);
        //DataTable dt = new DataTable("stock_data");
        //dt.Columns.Add("date",Type.GetType("System.String"));
        //dt.Columns.Add("start", Type.GetType("System.String"));
        //dt.Columns.Add("high", Type.GetType("System.String"));
        //dt.Columns.Add("low", Type.GetType("System.String"));
        //dt.Columns.Add("close", Type.GetType("System.String"));
        //dt.Columns.Add("amount", Type.GetType("System.String"));
        //DataRow newrow;
        //foreach (var item in his_data.kline)
        //{
        //    Console.WriteLine(string.Format("strat:{0},high:{1},low:{2},close:{3},amount:{4}", item.start, item.high, item.low, item.close, item.amount));
        //    newrow = dt.NewRow();
        //    newrow["date"] = item.date;
        //    newrow["high"] = item.high;
        //    newrow["low"] = item.low;
        //    newrow["start"] = item.start;
        //    newrow["close"] = item.close;
        //    newrow["amount"] = item.amount;
        //    dt.Rows.Add(newrow);
        //}
        //Console.WriteLine();
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    Console.WriteLine(string.Format("strat:{0},high:{1},low:{2},close:{3},amount:{4}", dt.Rows[i]["start"], dt.Rows[i]["high"], dt.Rows[i]["low"], dt.Rows[i]["close"], dt.Rows[i]["amount"]));

        //}


    }

}
