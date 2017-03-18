using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuantitaiveTransactionDLL;
using System.Timers;
using System.Data;

namespace Crawler_WinFrom
{
    class Crawler
    {
        Timer crawl;
        /// <summary>
        /// init the crawler
        /// </summary>
        public Crawler()
        {
            WriteLog.Write($"{DateTime.Now.ToString()} init the crawler");
            crawl = new Timer()
            {
                Interval = 1000*60*5 , //run every  5 minute
                AutoReset = true,/// set as false first to debug
                Enabled = true
        };
            crawl.Elapsed += Crawl_Elapsed;
            WriteLog.Write($"{DateTime.Now.ToString()} init ended");
        }
        /// <summary>
        /// crawl main
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Crawl_Elapsed(object sender, ElapsedEventArgs e)
        {
            WriteLog.Write($"{DateTime.Now.ToString()} the crawler is runed");
            int saved;
            string sysdate = DateTime.Now.ToString("yyyyMMdd");
            if (TradeDay(sysdate) == false)
            {
                WriteLog.Write($"{DateTime.Now.ToString()} not a trade date stop run crawler.");
                crawl.Enabled = false;
                crawl.Dispose();
                return;
            }
            WriteLog.Write($"{DateTime.Now.ToString()} the crawler is rnning");

            if (DateTime.Now.Hour == 16)
            {
                WriteLog.Write($"{DateTime.Now.ToString()} the insert line data finished convert the line data to his data.");
                His_data.Convert_linedata_to_hisdata();
                crawl.Enabled = false;
                crawl.Dispose();
                WriteLog.Write($"{DateTime.Now.ToString()} the crawler stoped.");
                return;
            }
            else if(DateTime.Now.Hour == 15 && (DateTime.Now.Minute >= 30 && DateTime.Now.Minute < 35))
            {
                DBUtility.execute_sql($"delete from stock_line_data where days ='{sysdate}'");
                WriteLog.Write($"{DateTime.Now.ToString()} the trade is end  reinsert the line data.");
                Line_data.load_line_data();
            }
            else
            {
                DataSet ds = DBUtility.get_stock_list();
                List<string> stockList = new List<string> { };
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
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
            }
                throw new NotImplementedException();
        }
        /// <summary>
        /// get this is a trade date or not 
        /// </summary>
        /// <param name="sysdate"></param>
        /// <returns></returns>
        private Boolean TradeDay(string sysdate)
        {
            return Line_data.GetLineDataObject("000001").date.ToString().Equals(sysdate) ? true : false; 
        }

        
    }
}
