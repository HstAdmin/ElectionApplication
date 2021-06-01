using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Utility.EmailHelper
{
    public interface IEmailHelper
    {
        void SendMail(List<string> to, string subject, string body, bool isHtml = true, List<string> cc = null);
    }
}
