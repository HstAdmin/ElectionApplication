using Hst.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Hst.Utility.EmailHelper
{
    public class EmailHelper : IEmailHelper
    {
        public void SendMail(List<string> to, string subject, string body, bool isHtml = true, List<string> cc = null)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            MailMessage mailMessage = new MailMessage();
            if (!string.IsNullOrEmpty(ConfigHelper.FromEmail))
            {
                mailMessage.From = new MailAddress(ConfigHelper.FromEmail);
            }

            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            foreach (var address in to)
            {
                mailMessage.To.Add(new MailAddress(address));
            }

            if (cc != null)
            {
                foreach (var address in cc)
                {
                    mailMessage.CC.Add(new MailAddress(address));
                }
            }

            SmtpClient smtp = new SmtpClient(ConfigHelper.SmtpServer, Convert.ToInt32(ConfigHelper.SmtpPort));
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials= new NetworkCredential(ConfigHelper.FromEmail, ConfigHelper.SmtpPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(mailMessage);
            mailMessage.Dispose();
            smtp.Dispose();
        }
    }
}
