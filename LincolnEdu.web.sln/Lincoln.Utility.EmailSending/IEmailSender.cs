using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.Utility.EmailSending
{
    public interface IEmailSender
    {
        void SendHtmlEmailAsync(string subject, string body, string from, string to, string cc, string bcc, IEnumerable<string> attachments = null);
        void SendHtmlEmailAsync(string subject, string body, string from, string to, IEnumerable<string> cc, IEnumerable<string> bcc, IEnumerable<string> attachments = null);
        void SendHtmlEmailAsync(string subject, string body, string from, IEnumerable<string> to, IEnumerable<string> cc, IEnumerable<string> bcc, IEnumerable<string> attachments = null);
    }
}
