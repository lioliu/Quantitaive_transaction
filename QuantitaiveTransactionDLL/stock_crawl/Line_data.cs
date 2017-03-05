using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_crawl
{
    class Line
    {
        public string time { set; get; }
        public string price { set; get; }
        public string volume { set; get; }
    }
    class Line_data
    {
        public string code { set; get; }
        public string pre_close { set; get; }
        public string date { set; get; }
        public string time { set; get; }
        public string total { set; get; }
        public string begin { set; get; }
        public string end { set; get; }
        public Line[] line { set; get; }
    }
}
