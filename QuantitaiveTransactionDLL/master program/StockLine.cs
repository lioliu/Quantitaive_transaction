using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPlot;
using QuantitaiveTransactionDLL;
using System.Windows.Forms.DataVisualization.Charting;

namespace master_program
{
    public partial class StockLine : Form
    {
        public StockLine()
        {
            InitializeComponent();
            Line("600000","20170317");
           // Line();
        }
        /// <summary>
        /// load the data betwen the given date
        /// </summary>
        /// <param name="code"></param>
        /// <param name="startDay"></param>
        /// <param name="endDay"></param>
        private void Line(string code,string Day)
        {
            #region get stock data
            string queryStr = $"select days,lpad(time,6,'0'),PRICE,VOLUME from stock_line_data where code ='{code}' and days ='{Day}' order by days desc,lpad(time,6,'0')";
            DataSet ds = DBUtility.Get_data(queryStr);
            int rowsCount = ds.Tables[0].Rows.Count;
            double[] price, volume;
            price = new double[rowsCount];
            volume = new double[rowsCount];
            string[] time = new string[rowsCount];
            DateTime[] dates = new DateTime[rowsCount];
            for (int i = 0; i < rowsCount; i++)
            {
                price[i] = Convert.ToDouble(ds.Tables[0].Rows[i][2]);
                volume[i] = Convert.ToDouble(ds.Tables[0].Rows[i][3]);
                time[i] = ds.Tables[0].Rows[i][1].ToString().PadLeft(4);
            }
            #endregion
            
        }

       
    }
}
