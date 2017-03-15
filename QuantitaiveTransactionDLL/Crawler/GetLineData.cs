﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuantitaiveTransactionDLL;
using System.Timers;
using System.Data;

namespace Crawler
{
    class GetLineData
    {
        Timer crawl;
        public GetLineData()
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "init the  timer.");
            }
            //init the timer
            crawl = new Timer();
            crawl.Interval = 1000*60*5;  //run every  5 minute
            crawl.Elapsed += new ElapsedEventHandler(crawl_run);
            crawl.AutoReset = true;
            crawl.Enabled = true;
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "timer init ended .");
            }
        }

        private void crawl_run(object sender, ElapsedEventArgs e)
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
            //get stock list
            DataSet ds = DBUtility.get_stock_list();
            List<string> stockList = new List<string> { };
            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                stockList.Add(ds.Tables[0].Rows[i][0].ToString());
            }
            foreach (var item in stockList)
            {
                saved = Line_data.dataCount(item, sysdate);
                DataTable dt = Line_data.get_line_data(item);
                Line_data.save_to_database(dt, saved);
                if (saved == 241) stockList.Remove(item);
            }
            if (DateTime.Now.Hour == 15 && (DateTime.Now.Minute >=30 && DateTime.Now.Minute < 35))
            {
                DBUtility.execute_sql(string.Format("delete from stock_line_data where days ='{0}'", sysdate));
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                { sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + " the trade is end  reinsert the line data."); }
                Line_data.load_line_data();

            }
            if(DateTime.Now.Hour == 16)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                { sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + " the insert line data finished convert the line data to his data."); }
                His_data.Convert_linedata_to_hisdata();
                crawl.Enabled = false;
                crawl.Dispose();
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                { sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + " the crawler stoped."); }
                return;
            }
        }
        private Boolean TradeDay(string sysdate)
        {
            return Line_data.GetLineDataObject("000001").date.ToString().Equals(sysdate) ? true : false; 
        }

        
    }
}
