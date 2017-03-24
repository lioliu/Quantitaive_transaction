using System;
using System.Collections.Generic;
using System.Text;
using SunyahSMS.SSPU.WebAPI.Message;
using System.Net;
using Newtonsoft.Json;

namespace QuantitaiveTransactionDLL
{
    public class Message
    {
        public Message()
        {
        }
      
        const string APIUri = "http://sms.sspu.edu.cn/api/sms/";

        const string SendUri = APIUri + "send";
        const string QueryReceiveUri = APIUri + "QueryReceiveStatus";
        const string QueryBalanceUri = APIUri + "QueryBalance";
        /// <summary>
        /// Sent message to the mobile phone
        /// </summary>
        /// <param name="telephone">target number</param>
        /// <param name="code">the message to sent</param>
        /// <returns></returns>
        public static bool Send(string telephone, string message)
        {
            using (WebClient wc = CreateWebClient())
            {

                List<string> list = new List<string>
                {
                    telephone
                };
                string json = null;
                SendSMMessage msg = new SendSMMessage()
                {
                    MsgContent = message,
                    Numbers = list,
                    Remark = "remark",
                    MsgId = Guid.NewGuid().ToString(),
                    Sign = "【股票预警】"
                };             
                SetAccountInfo(msg);
                json = JsonConvert.SerializeObject(msg);

                return JsonConvert.DeserializeObject<string>(InvokeAPI(wc, SendUri, json)) == null ?true:false;
            }
        }

        public static WebClient CreateWebClient()
        {
            //Web request
            string url = "http://sms.sspu.edu.cn";
            WebRequest request = WebRequest.Create(url);
            request.GetResponse();

            WebClient c = new WebClient()
            {
                Encoding = Encoding.UTF8
            };
            c.Headers[HttpRequestHeader.ContentType] = "application/json;charset=utf-8";
            return c;
        }

        /// <summary>
        /// use API
        /// </summary>
        /// <param name="wc">webClient</param>
        /// <param name="uri">api adrress</param>
        /// <param name="requestJson">api</param>
        /// <returns>api format jason</returns>
        static string InvokeAPI(WebClient wc, string uri, string requestJson)
        {
            string resultJson = wc.UploadString(uri, "post", requestJson);
            return resultJson;
        }

        /// <summary>
        /// set account 
        /// </summary>
        /// <param name="request"></param>
        static void SetAccountInfo(BaseMessage request)
        {
            //this is a given account

            request.UserName = "xxjszx";

            string password = "sspu.edu.cn";
          
            request.UserKey = MD5Util.HashString(password);

            request.UserId = new Guid("103b6108-e3b4-4bb1-88c7-beae24bb1c78");
        }
    }
}