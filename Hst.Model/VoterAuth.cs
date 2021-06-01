using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Model
{
   public class VoterAuth
    {
       public int VA_ID { get; set; }
       public int VA_VoterID { get; set; }
        public int VA_ElectionId { get; set; }
        public string VA_LoginId { get; set; }
        public string VA_Pwd { get; set; }
        public bool VA_IsLoggedIn { get; set; }
        public DateTime VA_CreatedDate { get; set; }
        public int VA_CreatedBy { get; set; }
        public DateTime? VA_UpdatedDate { get; set; }
        public int VA_UpdatedBy { get; set; }
    }
}
