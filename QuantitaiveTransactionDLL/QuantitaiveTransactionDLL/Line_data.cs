using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Threading;
namespace QuantitaiveTransactionDLL
{
    public class Line
    {
        public string Time { set; get; }
        public string Price { set; get; }
        public string Volume { set; get; }
    }
    public class Line_data
    {
        public string Code { set; get; }
        public string PreClose { set; get; }
        public string Date { set; get; }
        public string Time { set; get; }
        public string Total { set; get; }
        public string Begin { set; get; }
        public string End { set; get; }
        public Line[] Line { set; get; }
        //take 4 mins
        public static void LoadLineData()
        {
            DataSet stock_list = DBUtility.Get_stock_list();
            for (int i = 0; i < stock_list.Tables[0].Rows.Count; i++)
            {
                DataTable dt = get_line_data(stock_list.Tables[0].Rows[i][0].ToString());
                Task.Factory.StartNew(() => SaveData(dt));
                Thread.Sleep(200);
            }
            
        }
        private static DataTable ConvertToDataTable(Line_data data)
        {
            DataTable dt = new DataTable("Line_data");
            dt.Columns.Add("CODE", Type.GetType("System.String"));
            dt.Columns.Add("DAYS", Type.GetType("System.String"));
            dt.Columns.Add("TIME", Type.GetType("System.String"));
            dt.Columns.Add("PRICE", Type.GetType("System.Double"));
            dt.Columns.Add("VOLUME", Type.GetType("System.Double"));
            DataRow Newrow;
            foreach (var item in data.Line)
            {
                Newrow = dt.NewRow();
                Newrow["CODE"] = data.Code;
                Newrow["DAYS"] = data.Date;
                Newrow["TIME"] = item.Time;;
                Newrow["PRICE"] = item.Price;
                Newrow["VOLUME"] = item.Volume;
                dt.Rows.Add(Newrow);
            }
            return dt;
        }

        public static DataTable get_line_data(string code)
        {
            Random rnd = new Random();
            Base_crawl crawl = new Base_crawl();
            string URL = $"http://yunhq.sse.com.cn:32041/v1/sh1/line/{code}?callback=&begin=0&end=-1&select=time%2Cprice%2Cvolume&_={rnd.Next()}";
            string json = crawl.Run(URL);
            //Console.WriteLine(json);
            json = Json_formater.FormatLinedata(json);
            DataTable dt = ConvertToDataTable(JsonConvert.DeserializeObject<Line_data>(json));
            return dt;
        }
        public static Line_data GetLineDataObject(string code)
        {
            Random rnd = new Random();
            Base_crawl crawl = new Base_crawl();
            string URL = $"http://yunhq.sse.com.cn:32041/v1/sh1/line/{code}?callback=&begin=0&end=-1&select=time%2Cprice%2Cvolume&_={rnd.Next()}";
            string json = crawl.Run(URL);
            //Console.WriteLine(json);
            json = Json_formater.FormatLinedata(json);
            return JsonConvert.DeserializeObject<Line_data>(json);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static int SaveData(DataTable dt)
        {
            string insert = string.Empty;
            List<string> insertscript = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                insert = $"INSERT INTO STOCK_LINE_DATA VALUES{$"('{dt.Rows[i]["CODE"]}','{dt.Rows[i]["DAYS"]}','{dt.Rows[i]["TIME"]}','{dt.Rows[i]["PRICE"]}','{dt.Rows[i]["VOLUME"]}')"}";
                insertscript.Add(insert);
            }
            int result = DBUtility.Execute_sql(insertscript);
            //Console.WriteLine(result);
            return result;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Saved"></param>
        /// <returns></returns>
        public static int SaveData(DataTable dt,int Saved)
        {
            string insert = string.Empty;
            List<string> insertscript = new List<string>();
            for (int i = Saved; i < dt.Rows.Count; i++)
            {
                insert = $"INSERT INTO STOCK_LINE_DATA VALUES{$"('{dt.Rows[i]["CODE"]}','{dt.Rows[i]["DAYS"]}','{dt.Rows[i]["TIME"]}','{dt.Rows[i]["PRICE"]}','{dt.Rows[i]["VOLUME"]}')"}";
                insertscript.Add(insert);
            }
            return DBUtility.Execute_sql(insertscript);

        }
        /// <summary>
        /// get how many data has load
        /// </summary>
        /// <param name="code">the stock code</param>
        /// <param name="date">the request date</param>
        /// <returns></returns>
        public static int CountData(string code,string date)
        {
            string sql = $"select count(*) from stock_line_data where code = {code} and days = {date}";
            DataSet ds = DBUtility.Get_data(sql);
            return Convert.ToInt32( ds.Tables[0].Rows[0][0].ToString());
        }

    }
}
