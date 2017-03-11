using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace QuantitaiveTransactionDLL
{
    class Email
    {
        public static bool Sent(string EmailAdress,string Message)
        {
            MailMessage mail = new MailMessage();
            MailAddress from, to;
            from = new MailAddress("lmjlio@foxmail.com", "admin");
            to = new MailAddress(EmailAdress, "user");
            mail.From = from;
            mail.Subject = "股票预警" + DateTime.Now.ToString();
            mail.Body = Message;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            mail.To.Add(to);
            SmtpClient client = new SmtpClient("smtp.qq.com");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;

            NetworkCredential account = new NetworkCredential("744596028@qq.com", "`6y4r7u0p9o4r");
            client.Credentials = account;
            client.Send(mail);
            return true;
        }
    }
}
