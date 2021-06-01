using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Utility.Helper
{
    /// <summary>
    /// Class to manage Mail Server settings
    /// </summary>
    public class MailSetting
    {
        public string ApiKey { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
