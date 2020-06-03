using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.Utility.EmailSending
{
    public class EmailConfigurationSection
    {
        public string From { get; set; }
        public bool EnableSsl { get; set; }
        public string EMailAccountUserName { get; set; }
        public string EmailAccountPassword { get; set; }
        public string Port { get; set; }
        public string Host { get; set; }
    }
}
