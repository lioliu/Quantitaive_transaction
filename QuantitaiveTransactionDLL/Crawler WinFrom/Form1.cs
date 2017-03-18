using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crawler_WinFrom
{
    public partial class 股票数据获取 : Form
    {
        public 股票数据获取()
        {
            InitializeComponent();
            Checker.Interval = 1000*60;
            Checker.Enabled = false;
            ChangeState();
        }
        /// <summary>
        /// Checker's  tick function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Check(object sender, EventArgs e)
        {
            Crawler crawler = null;
            if (DateTime.Now.Hour == 09 && DateTime.Now.Minute == 31)
            {
                WriteLog.Write("The Crawler Start");
                crawler = new Crawler();
            }
        }
        /// <summary>
        /// btn start clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartChecker(object sender, EventArgs e)
        {
            Checker.Enabled = true;
            ChangeState();
        }
        /// <summary>
        /// btn stop clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopChecker(object sender, EventArgs e)
        {
            Checker.Enabled = false;
            ChangeState();
        }


        private void ChangeState()
        {
            btnStart.Enabled = !Checker.Enabled;
            btnStop.Enabled = Checker.Enabled;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;    //取消"关闭窗口"事件
                this.WindowState = FormWindowState.Minimized;    //使关闭时窗口向右下角缩小的效果
                notifyIcon1.Visible = true;
                this.Hide();
                return;
            }
        }
      

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            this.Show();
            WindowState = FormWindowState.Normal;
            this.Focus();
        }
    }
}
