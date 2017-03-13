using System;
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
        Timer timer1;
        public GetLineData()
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "init the  timer.");
            }
            //init the timer
            timer1 = new Timer();
            timer1.Interval = 1000 * 60 * 3;  //run every minute
            timer1.Elapsed += new ElapsedEventHandler(timer1_Elapsed);
            timer1.AutoReset = true;
            timer1.Enabled = true;
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "timer init ended .");
            }
        }

        private void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "the timer is runed.");
            }
            int saved;
            string sysdate = DateTime.Now.ToString("yyyyMMdd");
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "today is ."+sysdate);
            }
            if (TradeDay(sysdate) ==false)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "not a trade date stop run crawler.");
                }
                timer1.Enabled = false;
                timer1.Dispose();
                return;
            }
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + " crawler running.");
            }
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
            if(stockList.Count == 0)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "all data has been got stop the crawler.");
                }
                His_data.Convert_linedata_to_hisdata();
                timer1.Enabled = false;
                timer1.Dispose();
                return;
            }
        }
        private Boolean TradeDay(string sysdate)
        {
            Boolean result = false;
            Line_data line = Line_data.get_line_data_object("000001");
            result = line.date.ToString().Equals(sysdate) ? true : false;
            return result;
        }

    }
}
