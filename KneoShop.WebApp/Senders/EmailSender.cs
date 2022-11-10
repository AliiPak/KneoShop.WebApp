using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace KenoShop.WebApp.Senders
{
    public static class EmailSender
    {
        /// <summary>
        /// send email to user for specific reason
        /// </summary>
        /// <param name="to">email to send</param>
        /// <param name="subject">email subject</param>
        /// <param name="body">email message</param>
        /// <returns></returns>
        public static bool SendEmail(string to, string subject, string body)
        {
            try
            {
                // لینک ایجاد کلمه عبور برای ایمیل
                // https://myaccount.google.com/apppasswords

                string email = "Aliihaeripak@gmail.com";
                string password = "jgllfubhlamovstl";

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(email, subject);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential(email, password);
                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
    }
}
