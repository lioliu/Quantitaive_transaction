using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace QuantitaiveTransactionDLL
{
    class Email
    {
        public static bool Sent(string target_email_address,string message)
        {
            MailMessage mail = new MailMessage();
            MailAddress from, to;
            from = new MailAddress("lmjlio@foxmail.com", "admin");
            to = new MailAddress(target_email_address, "user");
            mail.From = from;
            //code here
            return true;
        }
    }
}
