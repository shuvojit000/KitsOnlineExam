using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.Utility.EmailSending
{
    public abstract class BaseEmailSender : IEmailSender
    {
        internal readonly EmailConfigurationSection EmailConfigurationSection;

        protected BaseEmailSender()
        {
            this.EmailConfigurationSection = new EmailSending.EmailConfigurationSection()
            {
                EmailAccountPassword = ConfigurationManager.AppSettings["Password"],
                EMailAccountUserName = ConfigurationManager.AppSettings["UserName"],
                EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"].ToString()),
                Port = ConfigurationManager.AppSettings["Port"],
                Host = ConfigurationManager.AppSettings["Host"]
            };
        }

        public void SendHtmlEmailAsync(string subject, string body, string from, string to, string cc, string bcc, IEnumerable<string> attachments = null)
        {
            this.SendHtmlEmailAsync(subject, body, from, new[] { to }, new[] { cc }, new[] { bcc }, attachments);
        }

        public void SendHtmlEmailAsync(string subject, string body, string from, string to, IEnumerable<string> cc, IEnumerable<string> bcc, IEnumerable<string> attachments = null)
        {
            this.SendHtmlEmailAsync(subject, body, from, new[] { to }, cc?.ToArray(), bcc?.ToArray(), attachments);
        }

        public void SendHtmlEmailAsync(string subject, string body, string from, IEnumerable<string> to, IEnumerable<string> cc, IEnumerable<string> bcc, IEnumerable<string> attachments = null)
        {
            var msg = this.CreateMessage(subject, body, from, to?.ToArray(), cc?.ToArray(), bcc?.ToArray());

            if (attachments != null)
            {
                AddAttachmentsToMessage(msg, attachments);
            }
            this.SendEmailAsync(msg);
        }

        private MailMessage CreateMessage(string subject, string body, string from, string[] TO, string[] CC, string[] BCC)
        {
            var message = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = string.IsNullOrEmpty(body) ? " " : body,
                IsBodyHtml = true,

            };
            foreach (var m in TO) { message.To.Add(new MailAddress(m)); }
            if (CC != null && CC.Count() > 0)
            {
                foreach (var m in CC) { message.CC.Add(new MailAddress(m)); }
            }
            if (BCC != null && BCC.Count() > 0)
            {
                foreach (var m in BCC) { message.Bcc.Add(new MailAddress(m)); }
            }
            return message;
        }

        internal static MailMessage AddAttachmentsToMessage(MailMessage message, IEnumerable<string> attachments)
        {
            if (attachments == null)
            {
                return message;
            }

            foreach (var at in attachments)
            {
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(at);
                message.Attachments.Add(attachment);
            }
            return message;
        }

        private void SendEmailAsync(MailMessage message)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Host = EmailConfigurationSection.Host;
                smtpClient.Port = int.Parse(EmailConfigurationSection.Port);
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = EmailConfigurationSection.EMailAccountUserName;
                NetworkCred.Password = EmailConfigurationSection.EmailAccountPassword;
                smtpClient.Credentials = NetworkCred;
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //smtpClient.UseDefaultCredentials = false;
                //smtpClient.EnableSsl = EmailConfigurationSection.EnableSsl;
                try
                {
                    smtpClient.Send(message);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }
    }
}
