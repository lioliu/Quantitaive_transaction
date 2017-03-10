using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuantitaiveTransactionDLL;
namespace QuantitaiveTransactionDLL
{
    class base_crawl
    {
        HttpHelper http;
        HttpItem item;
        public base_crawl()
        {
            http = new HttpHelper();
            item = new HttpItem()
            {
                URL = "http://yunhq.sse.com.cn:32041/v1/sh1/line/600006?callback=jQuery111204689014103430911_1488523846603&begin=0&end=-1&select=time%2Cprice%2Cvolume&_=1488523846611",//URL     必需项
                Encoding = null,//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                                //Encoding = Encoding.Default,
                Method = "get",//URL     可选项 默认为Get
                Timeout = 100000,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Cookie = "",//字符串Cookie     可选项
                UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)",//用户的浏览器类型，版本，操作系统     可选项有默认值
                Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                ContentType = "text/html",//返回类型    可选项有默认值
                Allowautoredirect = true,//是否根据３０１跳转     可选项
                Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                PostDataType = PostDataType.FilePath,//默认为传入String类型，也可以设置PostDataType.Byte传入Byte类型数据
                ResultType = ResultType.String,//返回数据类型，是Byte还是String
                CookieCollection = new System.Net.CookieCollection(),//可以直接传一个Cookie集合进来
            };
        }
            public void run()
        {
            HttpResult result = http.GetHtml(item);
            //取出返回的Cookie
            string cookie = result.Cookie;
            //返回的Html内容
            string html = result.Html;
            Console.WriteLine(html);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //表示访问成功，具体的大家就参考HttpStatusCode类
            }
            //表示StatusCode的文字说明与描述
            string statusCodeDescription = result.StatusDescription;
            //把得到的Byte转成图片
        }
        /// <summary>
        /// run the web_crawl to get the data from the given URL
        /// </summary>
        /// <param name="URL"></param>
        public string run(string URL)
        {
            item.URL = URL;
            HttpResult result = http.GetHtml(item);
            //取出返回的Cookie
            string cookie = result.Cookie;
            //返回的Html内容
            string html = result.Html;
        //    Console.WriteLine(html);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //表示访问成功，具体的大家就参考HttpStatusCode类
            }
            //表示StatusCode的文字说明与描述
            string statusCodeDescription = result.StatusDescription;
            //把得到的Byte转成图片
            return html;
        }
    }

}
