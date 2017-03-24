using System;
using System.Net.Mail;
using System.Net;

namespace QuantitaiveTransactionDLL
{
    class Email
    {
        /// <summary>
        /// Sent email to target location
        /// </summary>
        /// <param name="EmailAdress">target email adress</param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static bool Sent(string EmailAdress,string Message)
        {
            MailAddress from, to;
            from = new MailAddress("lmjlio@foxmail.com", "admin");
            to = new MailAddress(EmailAdress, "user");

            MailMessage mail = new MailMessage()
            {
                From = from,
                Subject = "股票预警" + DateTime.Now.ToString(),
                Body = Message,
                BodyEncoding = System.Text.Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess
        };
            mail.To.Add(to);

            SmtpClient client = new SmtpClient("smtp.qq.com")
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential("744596028@qq.com", "`6y4r7u0p9o4r")
        };
            client.Send(mail);
            return true;
        }
    }
}
