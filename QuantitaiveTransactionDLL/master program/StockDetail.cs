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
using System.Collections;

namespace master_program
{
    public partial class StockDetail : Form
    {
        int DaysCount = 30;
        public StockDetail()
        {
            
            InitializeComponent();
            KLine("600000", DaysCount);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseWheel);
        }
        private void panel1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Console.WriteLine("滚动事件已被捕捉");
            Console.WriteLine(e.Delta);
            //write code here
            if(e.Delta<0) DaysCount--;
            else DaysCount++;
            KLine("600000", DaysCount); 
        }
        public void KLine(string code,int dayRange)
        {
            // --- Plotting ---  
            this.plotSurface2D1.Clear();
                         ////////网格//////////
              Grid mygrid = new Grid();
                         mygrid.HorizontalGridType = Grid.GridType.Fine;
                         mygrid.VerticalGridType = Grid.GridType.Fine;
                         this.plotSurface2D1.Add(mygrid);
            plotSurface2D1.Title = "K线图";
                         ///////水平线//////////
         //    HorizontalLine line = new HorizontalLine(10);
         //                line.LengthScale = 2.89f;
                         //line.OrdinateValue = 2;
         //    this.plotSurface2D1.Add(line, 10);
                         ///////垂直线///////////
          //   VerticalLine line2 = new VerticalLine(10);
          //               line2.LengthScale = 0.89f;
          //               this.plotSurface2D1.Add(line2);
            #region get stock data
            string QueryStr = string.Format("select * from ( "+
                              " select days, open, high, low, close, row_number() over(order by days desc ) as rn from "+ " STOCK_HIS_DATA where code = '{0}' " +
               " ) where rn between 1 and {1} order by rn desc ",code,dayRange);
            DataSet ds = DBUtility.get_data(QueryStr);
            int rowsCount = ds.Tables[0].Rows.Count;
            double[] opens, closes, highs, lows;
            DateTime[] dates = new DateTime[rowsCount];
            opens = new double[rowsCount];
            closes = new double[rowsCount];
            highs = new double[rowsCount];
            lows = new double[rowsCount];
            
            for (int i = 0; i < rowsCount; i++)
            {

                opens[i] = Convert.ToDouble(ds.Tables[0].Rows[i][1]);
                highs[i] = Convert.ToDouble(ds.Tables[0].Rows[i][2]);
                lows[i] = Convert.ToDouble(ds.Tables[0].Rows[i][3]);
                closes[i] = Convert.ToDouble(ds.Tables[0].Rows[i][4]);
                //times[i] = Convert.ToString(ds.Tables[0].Rows[i][0]);
                string temp = ds.Tables[0].Rows[i][0].ToString();
                DateTime date = new DateTime(Convert.ToInt32(temp.Substring(0, 4)),Convert.ToInt32( temp.Substring(4, 2)),Convert.ToInt32( temp.Substring(6, 2)));
                dates[i] = date;
                Console.WriteLine(date);

            }
            #endregion

            ///////蜡烛图///////////
            //string[] times = { "0", "1"," 2"," 3", "4"," 5" };
            CandlePlot cp = new CandlePlot();
            cp.CloseData = closes;
            cp.OpenData = opens;
            cp.LowData = lows;
            cp.HighData = highs;
            cp.AbscissaData = dates;
            cp.BullishColor = Color.Red;
            cp.BearishColor = Color.Green;
            cp.SuggestXAxis();
            cp.SuggestYAxis();
            cp.Centered = false;
             this.plotSurface2D1.Add(cp);
            
            this.plotSurface2D1.Refresh();
        }
    }
}
