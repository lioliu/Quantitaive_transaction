using Newtonsoft.Json;
using QuantitaiveTransactionDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace QuantitaiveTransactionDLL
{
    public class Line
    {
        public string time { set; get; }
        public string price { set; get; }
        public string volume { set; get; }
    }
    public class Line_data
    {
        public string code { set; get; }
        public string pre_close { set; get; }
        public string date { set; get; }
        public string time { set; get; }
        public string total { set; get; }
        public string begin { set; get; }
        public string end { set; get; }
        public Line[] line { set; get; }
        //take 4 mins
        public static void load_line_data()
        {
            DataSet stock_list = DBUtility.get_stock_list();
            for (int i = 0; i < stock_list.Tables[0].Rows.Count; i++)
            {
                DataTable dt = get_line_data(stock_list.Tables[0].Rows[i][0].ToString());
                Task.Factory.StartNew(() => save_to_database(dt));
                Thread.Sleep(200);
            }
            Console.WriteLine("success");
        }
        private static DataTable covert_to_datatable(Line_data data)
        {
            DataTable dt = new DataTable("Line_data");
            dt.Columns.Add("CODE", Type.GetType("System.String"));
            dt.Columns.Add("DAYS", Type.GetType("System.String"));
            dt.Columns.Add("TIME", Type.GetType("System.String"));
            dt.Columns.Add("PRICE", Type.GetType("System.Double"));
            dt.Columns.Add("VOLUME", Type.GetType("System.Double"));
            DataRow Newrow;
            foreach (var item in data.line)
            {
                Newrow = dt.NewRow();
                Newrow["CODE"] = data.code;
                Newrow["DAYS"] = data.date;
                Newrow["TIME"] = item.time;;
                Newrow["PRICE"] = item.price;
                Newrow["VOLUME"] = item.volume;
                dt.Rows.Add(Newrow);
            }
            return dt;
        }

        public static DataTable get_line_data(string code)
        {
            Random rnd = new Random();
            base_crawl crawl = new base_crawl();
            string URL = String.Format("http://yunhq.sse.com.cn:32041/v1/sh1/line/{0}?callback=&begin=0&end=-1&select=time%2Cprice%2Cvolume&_={1}", code, rnd.Next());
            string json = crawl.run(URL);
            //Console.WriteLine(json);
            json = Json_formater.line_data(json);
            Line_data line_data = JsonConvert.DeserializeObject<Line_data>(json);
            DataTable dt = covert_to_datatable(line_data);
            return dt;
        }
        public static Line_data GetLineDataObject(string code)
        {
            Random rnd = new Random();
            base_crawl crawl = new base_crawl();
            string URL = String.Format("http://yunhq.sse.com.cn:32041/v1/sh1/line/{0}?callback=&begin=0&end=-1&select=time%2Cprice%2Cvolume&_={1}", code, rnd.Next());
            string json = crawl.run(URL);
            //Console.WriteLine(json);
            json = Json_formater.line_data(json);
            Line_data line_data = JsonConvert.DeserializeObject<Line_data>(json);
            return line_data;
        }

        private static int save_to_database(DataTable dt)
        {
            string insert = string.Empty;
            List<string> insertscript = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                insert = "INSERT INTO STOCK_LINE_DATA VALUES" + string.Format("('{0}','{1}','{2}','{3}','{4}')", dt.Rows[i]["CODE"], dt.Rows[i]["DAYS"], dt.Rows[i]["TIME"], dt.Rows[i]["PRICE"], dt.Rows[i]["VOLUME"] );
                insertscript.Add(insert);

            }
            int result = DBUtility.execute_sql(insertscript);
            //Console.WriteLine(result);
            return result;

        }
        public static int save_to_database(DataTable dt,int Saved)
        {
            string insert = string.Empty;
            List<string> insertscript = new List<string>();
            for (int i = Saved; i < dt.Rows.Count; i++)
            {
                insert = "INSERT INTO STOCK_LINE_DATA VALUES" + string.Format("('{0}','{1}','{2}','{3}','{4}')", dt.Rows[i]["CODE"], dt.Rows[i]["DAYS"], dt.Rows[i]["TIME"], dt.Rows[i]["PRICE"], dt.Rows[i]["VOLUME"]);
                insertscript.Add(insert);
            }
            int result = DBUtility.execute_sql(insertscript);
            return result;

        }

        public static int dataCount(string code,string date)
        {
            string sql = string.Format("select count(*) from stock_line_data where code = {0} and days = {1}",code,date);
            DataSet ds = DBUtility.get_data(sql);
            return Convert.ToInt32( ds.Tables[0].Rows[0][0].ToString());
        }

    }
}
