using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantitaiveTransactionDLL
{
    class Json_formater
    {
        public static string formatting_to_Json(string str)
        {
            //delete the header
            str.Remove(0,str.IndexOf("("));
            return str;
        }
    }
}
