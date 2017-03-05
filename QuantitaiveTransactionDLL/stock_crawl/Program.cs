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
            Stopwatch sw = new Stopwatch();
            Snap_data.run_snapdata();
   
            Console.WriteLine("success");
       
            Console.ReadLine();
            //string appkey = "4fc4a73a69648b1adc711638c813bfe6"; //my own app key


            ////1.沪深股市
            //string url1 = "http://web.juhe.cn:8080/finance/stock/hs";

            //var parameters1 = new Dictionary<string, string>();

            //parameters1.Add("gid", "sh600004"); //股票编号，上海股市以sh开头，深圳股市以sz开头如：sh601009
            //parameters1.Add("key", appkey);//你申请的key

            //string result1 = Snap_data.sendPost(url1, parameters1, "get");
            //Console.WriteLine(result1);
            //Snap_data snap_data = JsonConvert.DeserializeObject<Snap_data>(result1);
            //Console.WriteLine(snap_data.result[0].data.name);
            //success get the data 


            //String errorCode1 = newObj1["error_code"].Value;

            //if (errorCode1 == "0")
            //{
            //    Debug.WriteLine("成功");
            //    Debug.WriteLine(newObj1);
            //}
            //else
            //{
            //    //Debug.WriteLine("失败");
            //    Debug.WriteLine(newObj1["error_code"].Value + ":" + newObj1["reason"].Value);
            //}



            //Console.WriteLine("sucess_total");
            //Console.ReadLine();



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
