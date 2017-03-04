using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantitaiveTransactionDLL
{
    public class Json_formater
    {
        public static string his_data(string str)
        {
            str = str.Remove(0, str.IndexOf("(") + 1).Replace(")", "");
            //change '[' / ']' to '{'/'}' except the first and the last one
            str = str.Insert(str.IndexOf("["), "(");
            str = str.Insert(str.LastIndexOf("]") + 1, ")").Replace("[", "{").Replace("]", "}").Replace("({", "[").Replace("})", "]").Replace("\"", "").Replace(",", "\",\"").Replace(":", "\":\"").Insert(1, "\"").Replace("\"[", "[").Replace("}", "\"}").Replace("}\",\"{", "},{").Replace("]\"}", "]}");
            int state = -1;
            for (int i = 5; i < str.Length; i++)
            {

                if (str[i] == '{')
                {
                    str = str.Insert(i + 1, "\"date\":\"");
                    state = 0;
                }
                else if (str[i] == ',')
                {
                    switch (state)
                    {
                        case 0:
                            str = str.Insert(i + 1, "\"open\":");
                            state++;
                            break;
                        case 1:
                            str = str.Insert(i + 1, "\"high\":");
                            state++;
                            break;
                        case 2:
                            str = str.Insert(i + 1, "\"low\":");
                            state++;
                            break;
                        case 3:
                            str = str.Insert(i + 1, "\"close\":");
                            state++;
                            break;
                        case 4:
                            str = str.Insert(i + 1, "\"amount\":");
                            state = 0;
                            break;
                        default:
                            break;
                    }
                }
                else if (str[i] == '}') state = -1;
            }
            return str;
        }
    }
}
