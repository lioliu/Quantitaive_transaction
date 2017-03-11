using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data;
using QuantitaiveTransactionDLL;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace stock_crawl
{
    class Program
    {
        static void Main(string[] args)
        {

            //Line_data.load_line_data();
            His_data.Convert_linedata_to_hisdata();
           // Console.WriteLine(Line_data.dataCount("600000", "20170309"));
            Console.ReadLine();

        }
    }
}
