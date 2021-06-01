using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Model
{
    public class OTPDetail
    {
       public int O_ID { get; set; }
       public string O_Number { get; set; }
        public string O_OTP { get; set; }
        public DateTime O_StartDate { get; set; }
        public DateTime? O_EndDate { get; set; }
        public int O_IsActive { get; set; }
    }
}
