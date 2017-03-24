using System;
namespace QuantitaiveTransactionDLL
{
    class Base_crawl
    {
        HttpHelper http;
        HttpItem item;
        public Base_crawl()
        {
            http = new HttpHelper();
            item = new HttpItem()
            {
                URL = "http://yunhq.sse.com.cn:32041/v1/sh1/line/600006?callback=jQuery111204689014103430911_1488" +
                      "523846603&begin=0&end=-1&select=time%2Cprice%2Cvolume&_=1488523846611",//URL 
                Encoding = null,
                                
                Method = "get",//Get
                Timeout = 100000,
                ReadWriteTimeout = 30000,
                IsToLower = false,
                Cookie = "",
                UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)",//
                Accept = "text/html, application/xhtml+xml, */*",//    
                ContentType = "text/html",//
                Allowautoredirect = true,//
                Connectionlimit = 1024,//
                PostDataType = PostDataType.FilePath,//
                ResultType = ResultType.String,//
                CookieCollection = new System.Net.CookieCollection(),//
            };
        }
        /// <summary>
        /// run the default url  just using for test
        /// </summary>
            private void Run()
        {
            HttpResult result = http.GetHtml(item);
          
            string cookie = result.Cookie;
          
            string html = result.Html;
            Console.WriteLine(html);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
          
            }
          
            string statusCodeDescription = result.StatusDescription;
            
        }
        /// <summary>
        ///  run the crawler to get the html text from the URL givened
        /// </summary>
        /// <param name="URL">the URL wanted to crawl</param>
        /// <returns>the URL's html text</returns>
        public string Run(string URL)
        {
            item.URL = URL;
            HttpResult result = http.GetHtml(item);
            
            string cookie = result.Cookie;
            
            string html = result.Html;
        
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
        
            }
        
            string statusCodeDescription = result.StatusDescription;
        
            return html;
        }
    }

}
