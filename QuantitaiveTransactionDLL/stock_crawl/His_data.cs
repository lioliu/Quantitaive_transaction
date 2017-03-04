using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_crawl
{
    class Kline
    {
        public string date { get; set; }
        public double start { set; get; }
        public double high { set; get; }
        public double low { set; get; }
        public double close { set; get; }

        public double amount { set; get; }

    }
    class His_data
    {
        public string code { set; get; }
        public string total { set; get; }
        public string begin { set; get; }
        public string end { set; get; }
        public Kline[] kline { set; get; }
    }
   
}
