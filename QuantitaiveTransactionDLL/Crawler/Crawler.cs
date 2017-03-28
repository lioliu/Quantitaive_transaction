using System;
using System.ServiceProcess;
using System.Timers;

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
        //private Boolean TradeDay(string sysdate)
        //{
        //    Boolean result = false;
        //    Line_data line = Line_data.GetLineDataObject("000001");
        //    result = line.date.ToString().Equals(sysdate) ? true : false;
        //    return result;
        //}
        /// <summary>
        /// Elapseder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Elapsed(object sender, ElapsedEventArgs e)
        {
            //wirte code here
            GetLineData get = null;
            if (DateTime.Now.Hour == 09 && DateTime.Now.Minute == 30)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(@"D:\log.txt", true))
                {
                    sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ")}Crawler is runing.");
                }
                get =  new GetLineData();
            }

        }
    }
}
