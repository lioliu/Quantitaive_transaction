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
            Console.WriteLine("success");
            Console.ReadLine();

        }

        /// <summary>
        /// Http (GET/POST)
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="parameters">请求参数</param>
        /// <param name="method">请求方法</param>
        /// <returns>响应内容</returns>
  




    }
}
