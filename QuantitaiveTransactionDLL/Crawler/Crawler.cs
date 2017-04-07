using System;
using System.ServiceProcess;
using System.Timers;
using System.Data;
using QuantitaiveTransactionDLL;
using System.Collections.Generic;

namespace Crawler
{
    public partial class Crawler : ServiceBase
    {
        System.Timers.Timer timer1;
        public Crawler()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer1 = new System.Timers.Timer()
            {
                Interval = 1000 * 60,  //check every  minute
                Enabled = true,
                AutoReset = true
            };
            timer1.Elapsed += new ElapsedEventHandler(Elapsed);
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ")} Service started success.");
            }
        }

        protected override void OnStop()
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ")} Service is stoped.");
            }
            this.timer1.Enabled = false;
        }
        /// <summary>
        /// Elapseder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Elapsed(object sender, ElapsedEventArgs e)
        {
            //wirte code here
            //GetLineData get = null;
            if (DateTime.Now.Hour == 11 && DateTime.Now.Minute == 47)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(@"D:\log.txt", true))
                {
                    sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ")}Crawler is runing.");
                }
                Get();
                //get =  new GetLineData();
            }
            else if (DateTime.Now.Hour == 15 && DateTime.Now.Minute == 30 )
            {
                DBUtility.Execute_sql($"delete from stock_line_data where days ='{DateTime.Now.ToString("yyyyMMdd")}'");
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                { sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + " the trade is end  reinsert the line data."); }
                Line_data.LoadLineData();
            }
            else if (DateTime.Now.Hour == 16 && DateTime.Now.Minute == 00)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                { sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + " the insert line data finished convert the line data to his data."); }
                His_data.ConvertLineToHis();
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                { sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + " the crawler stoped."); }
                return;
            }
            else if (DateTime.Now.Hour == 18 && DateTime.Now.Minute == 00 )
            {
                //sent email to told the state of System
                Warning.Check();
            }

        }
            
        private void Get()
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "the timer is runed.");
            }
            int saved;
            string sysdate = DateTime.Now.ToString("yyyyMMdd");
            if (TradeDay(sysdate) == false)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "not a trade date stop run crawler.");
                }
                return;
            }
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            { sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + " crawler running."); }

          
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
                if (stockList.Count == 0) return;
            }
            
        }

        private Boolean TradeDay(string sysdate)
        {
            return Line_data.GetLineDataObject("000001").Date.ToString().Equals(sysdate) ? true : false;
        }
    }
}
