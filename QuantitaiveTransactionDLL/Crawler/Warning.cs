using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using QuantitaiveTransactionDLL;
using System.Data;

namespace Crawler
{
    /// <summary>
    /// check the crawler run sccuess or not
    /// </summary>
    class Warning
    {
        /// <summary>
        /// the admin's Email Used to get the warning of same data load error 
        /// </summary>
        const string adminEmail = "lmjlio@foxmail.com";

         static string result;
        public static int Check()
        {
            result = string.Empty;
            string sysdate = DateTime.Now.ToString("yyyyMMdd");
            if (TradeDay(sysdate).Equals(false))
            {
                
                return 1;
            }
    #region check the load of line data;
            DataSet lineDataCount = DBUtility.Get_data("SELECT COUNT(*)/241 FROM STOCK_LINE_DATA WHERE DAYS = TO_CHAR(SYSDATE,'YYYYMMDD')");

            if (lineDataCount.Tables[0].Rows.Count<1) //no result was selected
            {
                result += "/nUbable to load line data";
            }
            else if (!Convert.ToInt32(lineDataCount.Tables[0].Rows[0][0]).Equals(1215))
            {
                result += "/nDidn't load line data correctly.Too many or too less data was load need to clearn the data in DB and reload manually";
            }
            #endregion
#region check load of his data;

            DataSet hisDataCount = DBUtility.Get_data("SELECT COUNT(*) FROM STOCK_HIS_DATA WHERE DAYS = TO_CHAR(SYSDATE,'YYYYMMDD')");

            if (hisDataCount.Tables[0].Rows.Count < 1)
            {
                result += "/nUbable to load his data";
            }
            else if (!Convert.ToInt32(hisDataCount.Tables[0].Rows[0][0]).Equals(1215))
            {
                result += "/nDidn't load his data correctly.Too many or too less data was load need to clearn the data in DB and reload manually";
            }
            #endregion
#region sent the email to told the checked result

            if (result.Equals(string.Empty))
            {
                result += "The Data was loaded Successfully";
            }
            Email.Sent(adminEmail, result);
#endregion 
            return 0;
        }



        private static Boolean TradeDay(string sysdate)
        {
            return Line_data.GetLineDataObject("000001").Date.ToString().Equals(sysdate) ? true : false;
        }


    }
}