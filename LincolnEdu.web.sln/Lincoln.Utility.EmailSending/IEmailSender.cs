using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.Utility.EmailSending
{
    public interface IEmailSender
    {
        Task SendHtmlEmailAsync(string subject, string body, string from, string to, IEnumerable<string> attachments = null);

       // Task SendHtmlEmailWithInMemoryAttachmentsAsync(string subject, string body, string from, string to, IEnumerable<Tuple<string, byte[]>> attachments = null);

        Task SendHtmlEmailAsync(string subject, string body, string from, IEnumerable<string> to,  IEnumerable<string> attachments = null);

       // Task SendHtmlEmailWithInMemoryAttachmentsAsync(string subject, string body, string from, IEnumerable<string> to,  IEnumerable<Tuple<string, byte[]>> attachments = null);
    }
}
