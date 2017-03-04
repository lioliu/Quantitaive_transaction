using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data;
namespace stock_crawl
{
    class Program
    {
        static void Main(string[] args)
        {
            base_crawl crawl = new base_crawl();
            string json = crawl.run("http://yunhq.sse.com.cn:32041/v1/sh1/dayk/600004?callback=&select=date%2Copen%2Chigh%2Clow%2Cclose%2Cvolume&begin=-3&end=-1&_=1488544393177");

            //Console.WriteLine();
            // string json = "{\"code\":\"600004\",\"total\":3316,\"begin\":3315,\"end\":3316,\"kline\":[{\"date\":\"20170303\",\"start\":\"14.84\",\"high\":\"14.99\",\"low\":\"14.72\",\"close\":\"14.94\",\"amount\":\"4304419\"},{\"date\":\"20170302\",\"start\":\"15.08\",\"high\":\"15.09\",\"low\":\"14.85\",\"close\":\"14.90\",\"amount\":\"4960148\"}]}";

            //delete header
            json = json.Remove(0, json.IndexOf("(") + 1).Replace(")", "");
            //change '[' / ']' to '{'/'}' except the first and the last one
            json = json.Insert(json.IndexOf("["), "(");
            json = json.Insert(json.LastIndexOf("]") + 1, ")").Replace("[", "{").Replace("]", "}").Replace("({", "[").Replace("})", "]").Replace("\"", "").Replace(",", "\",\"").Replace(":", "\":\"").Insert(1, "\"").Replace("\"[", "[").Replace("}", "\"}").Replace("}\",\"{", "},{").Replace("]\"}","]}");
            int state = -1;
            for (int i = 5; i < json.Length; i++)
            {

                if (json[i] == '{')
                {
                    json = json.Insert(i + 1, "\"date\":\"");
                    state = 0;
                }
                else if (json[i] == ',')
                {
                    switch (state)
                    {
                        case 0:
                            json = json.Insert(i + 1, "\"start\":");
                            state++;
                            break;
                        case 1:
                            json = json.Insert(i + 1, "\"high\":");
                            state++;
                            break;
                        case 2:
                            json = json.Insert(i + 1, "\"low\":");
                            state++;
                            break;
                        case 3:
                            json = json.Insert(i + 1, "\"close\":");
                            state++;
                            break;
                        case 4:
                            json = json.Insert(i + 1, "\"amount\":");
                            state = 0;
                            break;
                        default:
                            break;
                    }
                }
                else if (json[i] == '}') state = -1;
            }
            Console.WriteLine(json);
            Console.WriteLine("{\"code\":\"600004\",\"total\":\"3316\",\"begin\":\"3315\",\"end\":\"3316\",\"kline\":[{\"date\":\"20170303\",\"start\":\"14.84\",\"high\":\"14.99\",\"low\":\"14.72\",\"close\":\"14.94\",\"amount\":\"4304419\"},{\"date\":\"20170302\",\"start\":\"15.08\",\"high\":\"15.09\",\"low\":\"14.85\",\"close\":\"14.90\",\"amount\":\"4960148\"}]}");
            Console.ReadKey();
            //Console.WriteLine(json);
            //His_data his_data = JsonConvert.DeserializeObject<His_data>(json);
            //Console.WriteLine(his_data.code);
            //DataTable dt = new DataTable("stock_data");
            //dt.Columns.Add("date",Type.GetType("System.String"));
            //dt.Columns.Add("start", Type.GetType("System.String"));
            //dt.Columns.Add("high", Type.GetType("System.String"));
            //dt.Columns.Add("low", Type.GetType("System.String"));
            //dt.Columns.Add("close", Type.GetType("System.String"));
            //dt.Columns.Add("amount", Type.GetType("System.String"));
            //DataRow newrow;
            //foreach (var item in his_data.kline)
            //{
            //    Console.WriteLine(string.Format("strat:{0},high:{1},low:{2},close:{3},amount:{4}", item.start, item.high, item.low, item.close, item.amount));
            //    newrow = dt.NewRow();
            //    newrow["date"] = item.date;
            //    newrow["high"] = item.high;
            //    newrow["low"] = item.low;
            //    newrow["start"] = item.start;
            //    newrow["close"] = item.close;
            //    newrow["amount"] = item.amount;
            //    dt.Rows.Add(newrow);
            //}
            //Console.WriteLine();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    Console.WriteLine(string.Format("strat:{0},high:{1},low:{2},close:{3},amount:{4}", dt.Rows[i]["start"], dt.Rows[i]["high"], dt.Rows[i]["low"], dt.Rows[i]["close"], dt.Rows[i]["amount"]));

            //}
            //Console.ReadKey();
        }


        
    }
}
