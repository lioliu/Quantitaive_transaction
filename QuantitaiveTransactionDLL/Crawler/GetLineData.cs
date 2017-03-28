using System;
using System.Collections.Generic;
using QuantitaiveTransactionDLL;
using System.Timers;
using System.Data;

namespace Crawler
{
    class GetLineData
    {
        Timer crawl;
        /// <summary>
        /// main function
        /// </summary>
        public GetLineData()
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "init the  timer.");
            }
            //init the timer
            crawl = new Timer()
            {
                Interval = 1000 * 60 * 5,  //run every  5 minute
                AutoReset = true,
                Enabled = true
            };
            crawl.Elapsed += new ElapsedEventHandler(Run);
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "timer init ended .");
            }
        }

        private void Run(object sender, ElapsedEventArgs e)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "the timer is runed.");
            }
            int saved;
            string sysdate = DateTime.Now.ToString("yyyyMMdd");
            if (TradeDay(sysdate) ==false)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "not a trade date stop run crawler.");
                }
                crawl.Enabled = false;
                crawl.Dispose();
                return;
            }
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + " crawler running.");}

            if (DateTime.Now.Hour == 16)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                { sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + " the insert line data finished convert the line data to his data."); }
                His_data.ConvertLineToHis();
                crawl.Enabled = false;
                crawl.Dispose();
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                { sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + " the crawler stoped."); }
                return;
            }
            else if (DateTime.Now.Hour == 15 && (DateTime.Now.Minute >= 30 && DateTime.Now.Minute < 35))
            {
                DBUtility.Execute_sql(string.Format("delete from stock_line_data where days ='{0}'", sysdate));
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                { sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + " the trade is end  reinsert the line data."); }
                Line_data.LoadLineData();

            }
            else
            {
                //get stock list
                DataSet ds = DBUtility.Get_stock_list();
                List<string> stockList = new List<string> { };
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    stockList.Add(ds.Tables[0].Rows[i][0].ToString());
                }
                foreach (var item in stockList)
                {
                    saved = Line_data.CountData(item, sysdate);
                    DataTable dt = Line_data.get_line_data(item);
                    Line_data.SaveData(dt, saved);
                    if (saved == 241) stockList.Remove(item);
                }
            }
        }
        private Boolean TradeDay(string sysdate)
        {
            return Line_data.GetLineDataObject("000001").Date.ToString().Equals(sysdate) ? true : false; 
        }

        
    }
}
