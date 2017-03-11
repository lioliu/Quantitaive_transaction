using System;
using System.ServiceProcess;
using System.Timers;
namespace Crawler
{
    public partial class Crawler : ServiceBase
    {
        Timer timer1;
        public Crawler()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer1 = new Timer();
            timer1.Interval = 1000 * 60;  //check every  minute
            timer1.Elapsed += new ElapsedEventHandler(timer1_Elapsed);
            timer1.Enabled = true;
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "Service started success.");
            }
        }

        protected override void OnStop()
        {
            this.timer1.Enabled = false;
        }
        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int intHour = e.SignalTime.Hour;
            int intMinute = e.SignalTime.Minute;
            //run at every day's 09:10
            if (intHour == 09 && intMinute == 10)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "runned success.");
                }
                //run the crawler
                new GetLineData();
            }
        }
    }
}
