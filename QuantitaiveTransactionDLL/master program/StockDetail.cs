using NPlot;
using QuantitaiveTransactionDLL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace master_program
{
    public partial class StockDetail : Form
    {
        int DaysCount = 30;
        public StockDetail()
        {
            
            InitializeComponent();
            KLine("600000", DaysCount);
            this.MouseWheel += new MouseEventHandler(Panel1_MouseWheel);
        }
        private void Panel1_MouseWheel(object sender, MouseEventArgs e)
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
            KlineChart.Clear();
            amountChart.Clear();
            ////////网格//////////
            Grid mygrid = new Grid()
            {
                HorizontalGridType = Grid.GridType.Fine,
                VerticalGridType = Grid.GridType.Fine
            };
            KlineChart.Add(mygrid);
            amountChart.Add(mygrid);
            KlineChart.Title = "K线图";
            ///////水平线//////////
            //LinePlot lp3 = new LinePlot();
            //lp3.OrdinateData = 16;
            //lp3.AbscissaData = 20;
            //lp3.Pen = new Pen(Color.Orange);
            //lp3.Pen.Width = 20;
            //lp3.Label = " 价格";
            //this.plotSurface2D1.Add(lp3);
            //line.OrdinateValue = 16;
           // this.plotSurface2D1.Add(line, 10);
                         ///////垂直线///////////
          //   VerticalLine line2 = new VerticalLine(10);
          //               line2.LengthScale = 0.89f;
          //               this.plotSurface2D1.Add(line2);
            #region get stock data
            string QueryStr = string.Format("select * from ( "+
                              " select days, open, high, low, close,amount, row_number() over(order by days desc ) as rn from "+ " STOCK_HIS_DATA where code = '{0}' " +
               " ) where rn between 1 and {1} order by rn desc ",code,dayRange);
            DataSet ds = DBUtility.Get_data(QueryStr);
            int rowsCount = ds.Tables[0].Rows.Count;
            double[] opens, closes, highs, lows,amount;
            DateTime[] dates = new DateTime[rowsCount];
            opens = new double[rowsCount];
            closes = new double[rowsCount];
            highs = new double[rowsCount];
            lows = new double[rowsCount];
            amount = new double[rowsCount];
            for (int i = 0; i < rowsCount; i++)
            {

                opens[i] = Convert.ToDouble(ds.Tables[0].Rows[i][1]);
                highs[i] = Convert.ToDouble(ds.Tables[0].Rows[i][2]);
                lows[i] = Convert.ToDouble(ds.Tables[0].Rows[i][3]);
                closes[i] = Convert.ToDouble(ds.Tables[0].Rows[i][4]);
                amount[i] = Convert.ToDouble(ds.Tables[0].Rows[i][5])/10000;
                //times[i] = Convert.ToString(ds.Tables[0].Rows[i][0]);
                string temp = ds.Tables[0].Rows[i][0].ToString();
                DateTime date = new DateTime(Convert.ToInt32(temp.Substring(0, 4)),Convert.ToInt32( temp.Substring(4, 2)),Convert.ToInt32( temp.Substring(6, 2)));
                dates[i] = date;
                //Console.WriteLine(date);

            }
            #endregion

            ///////蜡烛图///////////
            //string[] times = { "0", "1"," 2"," 3", "4"," 5" };
            CandlePlot cp = new CandlePlot()
            {
                Style = CandlePlot.Styles.Filled,
                CloseData = closes,
                OpenData = opens,
                LowData = lows,
                HighData = highs,
                AbscissaData = dates,
                BullishColor = Color.Red,
                BearishColor = Color.Green
            };
            HistogramPlot hp3 = new HistogramPlot()
            {
                AbscissaData = dates,
                OrdinateData = amount,
                Color = Color.Yellow,
                BaseWidth = 0.1f,

                Filled = true,
                RectangleBrush = RectangleBrushes.Solid.Blue
            };

            //amountChart.XAxis1 = hp3.SuggestXAxis();
            //amountChart.YAxis1 = hp3.SuggestYAxis();
            //hp3.AbscissaData = "test";
            cp.Centered = false;
            //cp.SuggestXAxis().Label = "日期";
            //cp.SuggestYAxis().Label = "价格";
            //  amountChart.YAxis2.Label = "金额(万)";
            KlineChart.Add(cp);
            KlineChart.XAxis1.Label = "日期";
            //plotSurface2D1.XAxis2.Label = "tst";
            KlineChart.YAxis1.Label = "价格";
            KlineChart.XAxis1.TicksLabelAngle = 45;
            //图标拖动
            //     plotSurface2D1.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
            //     plotSurface2D1.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
            //改变坐标轴比例
            //plotSurface2D1.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));


            amountChart.Add(hp3);
            amountChart.Refresh();
            KlineChart.Refresh();
        }
    }
}
