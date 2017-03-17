﻿namespace master_program
{
    partial class StockDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.KlineChart = new NPlot.Windows.PlotSurface2D();
            this.amountChart = new NPlot.Windows.PlotSurface2D();
            this.SuspendLayout();
            // 
            // KlineChart
            // 
            this.KlineChart.AutoScaleAutoGeneratedAxes = false;
            this.KlineChart.AutoScaleTitle = false;
            this.KlineChart.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.KlineChart.DateTimeToolTip = false;
            this.KlineChart.Legend = null;
            this.KlineChart.LegendZOrder = -1;
            this.KlineChart.Location = new System.Drawing.Point(12, 12);
            this.KlineChart.Name = "KlineChart";
            this.KlineChart.RightMenu = null;
            this.KlineChart.ShowCoordinates = true;
            this.KlineChart.Size = new System.Drawing.Size(824, 395);
            this.KlineChart.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.KlineChart.TabIndex = 0;
            this.KlineChart.Text = "plotSurface2D1";
            this.KlineChart.Title = "";
            this.KlineChart.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.KlineChart.XAxis1 = null;
            this.KlineChart.XAxis2 = null;
            this.KlineChart.YAxis1 = null;
            this.KlineChart.YAxis2 = null;
            // 
            // amountChart
            // 
            this.amountChart.AutoScaleAutoGeneratedAxes = false;
            this.amountChart.AutoScaleTitle = false;
            this.amountChart.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.amountChart.DateTimeToolTip = false;
            this.amountChart.Legend = null;
            this.amountChart.LegendZOrder = -1;
            this.amountChart.Location = new System.Drawing.Point(12, 413);
            this.amountChart.Name = "amountChart";
            this.amountChart.RightMenu = null;
            this.amountChart.ShowCoordinates = true;
            this.amountChart.Size = new System.Drawing.Size(824, 199);
            this.amountChart.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.amountChart.TabIndex = 1;
            this.amountChart.Text = "plotSurface2D2";
            this.amountChart.Title = "";
            this.amountChart.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.amountChart.XAxis1 = null;
            this.amountChart.XAxis2 = null;
            this.amountChart.YAxis1 = null;
            this.amountChart.YAxis2 = null;
            // 
            // StockDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 624);
            this.Controls.Add(this.amountChart);
            this.Controls.Add(this.KlineChart);
            this.Name = "StockDetail";
            this.Text = "StockDetail";
            this.ResumeLayout(false);

        }

        #endregion

        private NPlot.Windows.PlotSurface2D KlineChart;
        private NPlot.Windows.PlotSurface2D amountChart;
    }
}