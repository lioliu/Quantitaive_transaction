using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using QuantitaiveTransactionDLL;
using System.Diagnostics;

namespace stock_crawl
{
    
    class Data
    {
        //buy five
        public string buyFive { set; get; }
        public string buyFivePri { set; get; }
        public string buyFour { set; get; }
        public string buyFourPri { set; get; }
        public string buyThree { set; get; }
        public string buyThreePri { set; get; }
        public string buyTwo { set; get; }
        public string buyTwoPri { set; get; }
        public string buyOne { set; get; }
        public string buyOnePri { set; get; }
        //sell five
        public string sellFive { set; get; }
        public string sellFivePri { set; get; }
        public string sellFour { set; get; }
        public string sellFourPri { set; get; }
        public string sellThree { set; get; }
        public string sellThreePri { set; get; }
        public string sellTwo { set; get; }
        public string sellTwoPri { set; get; }
        public string sellOne { set; get; }
        public string sellOnePri { set; get; }
        //other data
        public string time { set; get; }
        public string todayMax { set; get; }
        public string todayMin { set; get; }
        public string todayStartPri { set; get; }
        public string traAmount { set; get; }
        public string traNumber { set; get; }
        public string yestodEndPri { set; get; }
        public string competitivePri { set; get; }
        public string date { set; get; }
        public string gid { set; get; }
        public string increPer { set; get; }
        public string increase { set; get; }
        public string name { set; get; }
        public string nowPri { set; get; }
        public string reservePri { set; get; }




    }
    class Dapandata
    {
        public string dot { set; get; }
        public string name { set; get; }
        public string nowPric { set; get; }
        public string rate { set; get; }
        public string traAmount { set; get; }
        public string traNumber { set; get; }

    }
    class Gopicture
    {
        public string minurl { set; get; }
        public string dayurl { set; get; }
        public string weekurl { set; get; }
        public string monthurl { set; get; }
    }
    class Result
    {
        public Data data { set; get; }
        public Dapandata dapandata { set; get; }
        public Gopicture gopicture { set; get; }
    }
    class Snap_data
    {
        const string  appkey = "4fc4a73a69648b1adc711638c813bfe6";//given by the websites
        const string  url1 = "http://web.juhe.cn:8080/finance/stock/hs"; //the  request url
        public string resultcode { set; get; }
        public string reason { set; get; }
        public string error_code { set; get; }
        public Result[] result { set; get; }

        public static void run_snapdata()
        {
            DataSet stock_list = DBUtility.get_stock_list();
            string[] list = new string[stock_list.Tables[0].Rows.Count];
            for (int i = 0; i < stock_list.Tables[0].Rows.Count; i++)
            {
                list[i] = stock_list.Tables[0].Rows[i][0].ToString();
            }
            
            foreach (var item in list)
            {
                Task.Factory.StartNew(()=>get_Snap(item));
            }
         
        }
        public static void get_Snap(string code)
        {
            var parameters1 = new Dictionary<string, string>();
            string query_code = "sh" + code;
            //init the parameters
            parameters1.Add("gid", query_code); 
            parameters1.Add("key", appkey);//

            string result1 = Snap_data.sendPost(url1, parameters1, "get");
            //Console.WriteLine(result1); used for debug
            Snap_data snap_data = JsonConvert.DeserializeObject<Snap_data>(result1);
            Console.WriteLine(snap_data.result[0].data.name);
        }

       

        public static string sendPost(string url, IDictionary<string, string> parameters, string method)
        {
            if (method.ToLower() == "post")
            {
                HttpWebRequest req = null;
                HttpWebResponse rsp = null;
                System.IO.Stream reqStream = null;
                try
                {
                    req = (HttpWebRequest)WebRequest.Create(url);
                    req.Method = method;
                    req.KeepAlive = false;
                    req.ProtocolVersion = HttpVersion.Version10;
                    req.Timeout = 5000;
                    req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                    byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(parameters, "utf8"));
                    reqStream = req.GetRequestStream();
                    reqStream.Write(postData, 0, postData.Length);
                    rsp = (HttpWebResponse)req.GetResponse();
                    Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
                    return GetResponseAsString(rsp, encoding);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (reqStream != null) reqStream.Close();
                    if (rsp != null) rsp.Close();
                }
            }
            else
            {
                //创建请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + BuildQuery(parameters, "utf8"));

                //GET请求
                request.Method = "GET";
                request.ReadWriteTimeout = 5000;
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

                //返回内容
                string retString = myStreamReader.ReadToEnd();
                return retString;
            }
        }
        /// <summary>
        /// 把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        static string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            System.IO.Stream stream = null;
            StreamReader reader = null;
            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                reader = new StreamReader(stream, encoding);
                return reader.ReadToEnd();
            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }
        }

        static string BuildQuery(IDictionary<string, string> parameters, string encode)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;
            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name))//&& !string.IsNullOrEmpty(value)
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }
                    postData.Append(name);
                    postData.Append("=");

                    postData.Append(value);

                    hasParam = true;
                }
            }
            return postData.ToString();
        }
    }
}
