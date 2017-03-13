using System;
using System.ServiceProcess;
using System.Timers;
using QuantitaiveTransactionDLL;
using System.Data;
using System.Collections.Generic;
using System.Threading;
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
            timer1 = new System.Timers.Timer();
            timer1.Interval = 1000 * 60;  //check every  minute
            timer1.Elapsed += new ElapsedEventHandler(timer1_Elapsed);
            timer1.Enabled = true;
            timer1.AutoReset = true;
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "Service started success.");
            }
        }

        protected override void OnStop()
        {
            this.timer1.Enabled = false;
        }
        private Boolean TradeDay(string sysdate)
        {
            Boolean result = false;
            Line_data line = Line_data.get_line_data_object("000001");
            result = line.date.ToString().Equals(sysdate) ? true : false;
            return result;
        }
        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "timer running.");
            }
            int intHour = e.SignalTime.Hour;
            int intMinute = e.SignalTime.Minute;
            //run at every day's 09:10
            if (true )
            {
                Run();
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "crawler running.");
                }
            }

        }

        private void Run()
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true)){ sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "runned success."); }
            string sysdate = DateTime.Now.ToString("yyyyMMdd");
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true)) { sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + sysdate); }
            if (TradeDay(sysdate).Equals(false)) return;//not a trade day
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true)) { sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "today is a trade date."); }
            int saved;
            DataSet ds = DBUtility.get_stock_list();
            List<string> stockList = new List<string> { };
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)stockList.Add(ds.Tables[0].Rows[i][0].ToString());
            foreach (var item in stockList)
            {
                saved = Line_data.dataCount(item, sysdate);
                DataTable dt = Line_data.get_line_data(item);
                Line_data.save_to_database(dt, saved);
                if (saved == 241) stockList.Remove(item);
            }
            if (stockList.Count == 0)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true)){sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "all data has been got stop the crawler.");}
                His_data.Convert_linedata_to_hisdata();
                return;
            }

        }
             
        







    }
}
