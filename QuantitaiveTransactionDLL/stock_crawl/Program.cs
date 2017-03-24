using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data;
using QuantitaiveTransactionDLL;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace stock_crawl
{
    class Program
    {
        static void Main(string[] args)
        {

            //Line_data.load_line_data();
            His_data.Convert_linedata_to_hisdata();
            // Console.WriteLine(Line_data.dataCount("600000", "20170309"));
            //    using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true)) { sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "runned success."); }
            //    string sysdate = DateTime.Now.ToString("yyyyMMdd");
            //    using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true)) { sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + sysdate); }
            //    if (TradeDay(sysdate).Equals(false)) return;//not a trade day
            //    using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true)) { sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "today is a trade date."); }
            //    int saved;
            //    DataSet ds = DBUtility.get_stock_list();
            //    List<string> stockList = new List<string> { };
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++) stockList.Add(ds.Tables[0].Rows[i][0].ToString());
            //    foreach (var item in stockList)
            //    {
            //        saved = Line_data.dataCount(item, sysdate);
            //        DataTable dt = Line_data.get_line_data(item);
            //        Line_data.save_to_database(dt, saved);
            //        if (saved == 241)
            //            stockList.Remove(item);
            //    }
            //    if (stockList.Count == 0)
            //    {
            //        using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true)) { sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "all data has been got stop the crawler."); }
            //        His_data.Convert_linedata_to_hisdata();
            //        return;
            //    }
            Console.ReadLine();

        }

    }
}
