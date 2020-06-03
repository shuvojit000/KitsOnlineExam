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
                EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]),
                Port = ConfigurationManager.AppSettings["Port"],
                Host=ConfigurationManager.AppSettings["Host"]
            };
        }

        public Task SendHtmlEmailAsync(string subject, string body, string from, string to, IEnumerable<string> attachments = null)
        {
            return this.SendHtmlEmailAsync(subject, body, from, new[] { to }, attachments);
        }

        //public Task SendHtmlEmailWithInMemoryAttachmentsAsync(string subject, string body, string from, string to, IEnumerable<Tuple<string, byte[]>> attachments = null)
        //{
        //    return this.SendHtmlEmailWithInMemoryAttachmentsAsync(subject, body, from, new[] { to }, attachments);
        //}

        public async Task SendHtmlEmailAsync(string subject, string body, string from, IEnumerable<string> to, IEnumerable<string> attachments = null)
        {
            var msg = this.CreateMessage(subject, body, from, to.ToArray());

            if (attachments != null)
            {
                AddAttachmentsToMessageAsync(msg, attachments);
            }

            await this.SendEmailAsync(msg).ConfigureAwait(false);
        }

        //public async Task SendHtmlEmailWithInMemoryAttachmentsAsync(string subject, string body, string from, IEnumerable<string> to, IEnumerable<string> attachments = null)
        //{
        //    var message = this.CreateMessage(subject, body, from, to.ToArray());

        //    if (attachments != null)
        //    {
        //        AddAttachmentsToMessageAsync(message, attachments);
        //    }

        //    await this.SendEmailAsync(message).ConfigureAwait(false);
        //}


        private MailMessage CreateMessage(string subject, string body, string from, string[] TO)
        {
            // Note: SendGrid does not allow a message body with no content
            var message = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = string.IsNullOrEmpty(body) ? " " : body,
                IsBodyHtml=true,
                
            };
            foreach (var m in TO) { message.To.Add(new MailAddress(m)); }
            return message;
        }
        internal static MailMessage AddAttachmentsToMessageAsync(MailMessage message, IEnumerable<string> attachments)
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



        private async Task SendEmailAsync(MailMessage message)
        {          

            using (var smtp = new SmtpClient())
            {
                smtp.Host = EmailConfigurationSection.Host;
                smtp.EnableSsl = EmailConfigurationSection.EnableSsl;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                NetworkCred.UserName = EmailConfigurationSection.EMailAccountUserName;
                NetworkCred.Password = EmailConfigurationSection.EmailAccountPassword; 
                smtp.Port = int.Parse(EmailConfigurationSection.Port);  
                await smtp.SendMailAsync(message);
            }

            

        }
    }
}
