using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IntacoWebDemoForm.Mailing
{
    public class MailHelper
    {
        private const string smtpUser = @"....";
        private const string smtpPassword = @"...";
        private bool UseSsl = true;
        private const int smtpPortTLS = 587;
        private const int smtpPortSSL = 465;
        private string ServerName = @"smtp.office365.com";
        private const string defaultFooter = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam at vehicula neque, et placerat nisi. In tempor eu urna et vulputate. Aliquam odio erat, malesuada nec sagittis quis, tempor ut augue. Sed efficitur arcu id sem porttitor viverra. Donec iaculis lacinia lacus vel consequat. Cras porttitor lorem ac nulla tempus sodales. Proin varius tellus sit amet viverra vehicula. Duis congue posuere aliquam. Aliquam eget justo malesuada, aliquam nisi sit amet, molestie felis.";





        public void SendMail(string reciever, string token)
        {
            using (SmtpClient cl = new SmtpClient(ServerName))
            {
                using (MailMessage msg = new MailMessage())
                {
                    if (reciever == null || reciever.Length == 0)
                        return;

                    cl.EnableSsl = UseSsl;
                    cl.DeliveryMethod = SmtpDeliveryMethod.Network;
                    cl.Port = smtpPortTLS;
                    cl.UseDefaultCredentials = false;
                    cl.Credentials = new NetworkCredential(smtpUser, smtpPassword);
                    cl.ServicePoint.MaxIdleTime = 1;

                    msg.From = new MailAddress(smtpUser);
                    msg.To.Add(reciever);

                    msg.IsBodyHtml = true;
                    //msg.Body = MailBodyString(message);
                    msg.BodyEncoding = System.Text.Encoding.UTF8;
                    msg.Subject = "Temat";
                    msg.SubjectEncoding = System.Text.Encoding.UTF8;
                    Uri uri = new Uri("url+token" + token);
                    string message = $"message";
                    msg.Body = message;

                    cl.Send(msg);
                }
            }
        }

        public void SendMailAAD(string reciever, string username, string password)
        {
            using (SmtpClient cl = new SmtpClient(ServerName))
            {
                using (MailMessage msg = new MailMessage())
                {
                    if (reciever == null || reciever.Length == 0)
                        return;

                    cl.EnableSsl = UseSsl;
                    cl.DeliveryMethod = SmtpDeliveryMethod.Network;
                    cl.Port = smtpPortTLS;
                    cl.UseDefaultCredentials = false;
                    cl.Credentials = new NetworkCredential(smtpUser, smtpPassword);
                    cl.ServicePoint.MaxIdleTime = 1;

                    msg.From = new MailAddress(smtpUser);
                    msg.To.Add(reciever);

                    msg.IsBodyHtml = true;
                    //msg.Body = MailBodyString(message);
                    msg.BodyEncoding = System.Text.Encoding.UTF8;
                    msg.Subject = "Temat";
                    msg.SubjectEncoding = System.Text.Encoding.UTF8;
                    string message = $"Dane konta";
                    msg.Body = message;
                    cl.Send(msg);
                }
            }
        }


    }
}
