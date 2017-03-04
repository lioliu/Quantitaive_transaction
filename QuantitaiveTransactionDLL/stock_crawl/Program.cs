using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data;
using QuantitaiveTransactionDLL;
namespace stock_crawl
{
    class Program
    {
        static void Main(string[] args)
        {
            His_data.load_hisdata();
            Console.WriteLine("sucess");
            Console.ReadLine();

            //base_crawl crawl = new base_crawl();
            //string json = crawl.run("http://yunhq.sse.com.cn:32041/v1/sh1/dayk/600010?callback=&select=date%2Copen%2Chigh%2Clow%2Cclose%2Cvolume&begin=-100&end=-1");
            //json = Json_formater.his_data(json);
            //His_data his_data = JsonConvert.DeserializeObject<His_data>(json);
            //DataTable dt = new DataTable("stock_data");
            //dt.Columns.Add("CODE", Type.GetType("System.String"));
            //dt.Columns.Add("DAYS", Type.GetType("System.DateTime"));
            //dt.Columns.Add("OPEN", Type.GetType("System.Double"));
            //dt.Columns.Add("HIGH", Type.GetType("System.Double"));
            //dt.Columns.Add("LOW", Type.GetType("System.Double"));
            //dt.Columns.Add("CLOSE", Type.GetType("System.Double"));
            //dt.Columns.Add("AMOUNT", Type.GetType("System.Double"));
            //DataRow Newrow;
            //foreach (var item in his_data.kline)
            //{
            //    Newrow = dt.NewRow();
            //    Newrow["CODE"] = his_data.code;
            //    Newrow["DAYS"] = new DateTime(Convert.ToInt16(item.date.Substring(0, 4)),Convert.ToInt16( item.date.Substring(4, 2)),Convert.ToInt16( item.date.Substring(6, 2)));
            //    Newrow["OPEN"] = Convert.ToDouble(item.open);
            //    Newrow["HIGH"] = Convert.ToDouble(item.high);
            //    Newrow["LOW"] = Convert.ToDouble(item.low);
            //    Newrow["CLOSE"] = Convert.ToDouble(item.close);
            //    Newrow["AMOUNT"] = Convert.ToDouble(item.amount);
            //    dt.Rows.Add(Newrow);
            //}
            //Console.WriteLine("Code:" + his_data.code);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    Console.WriteLine(string.Format("date:{0},open:{1},high:{2},low:{3},close:{4},amount:{5}", dt.Rows[i]["DAYS"], dt.Rows[i]["OPEN"], dt.Rows[i]["HIGH"], dt.Rows[i]["LOW"], dt.Rows[i]["CLOSE"], dt.Rows[i]["AMOUNT"]));
            //}
            //Console.ReadKey();
            //Console.WriteLine(json);
            //Console.ReadKey();
            //Console.WriteLine(json);
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
            //Console.ReadKey();
        }



    }
}
