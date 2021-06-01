using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Hst.Model
{
    [DataContract]
    public class User
    {    
        [DataMember(Name ="UserId")]
        public int U_ID { get; set; }
        [DataMember(Name = "UserName")] 
        public string U_Name { get; set; }
        [DataMember(Name = "LoginId")] 
        public string U_LoginID { get; set; }
        [DataMember(Name = "Pass")] 
        public string U_Pwd { get; set; }
        [DataMember(Name = "RoleId")]
        public int U_R_ID { get; set; }
        public DateTime U_CreatedDate { get; set; }
        public int U_CreateBy { get; set; }
        public int U_LastLogin { get; set; }
        [DataMember(Name = "IsFirst")]
        public Boolean U_IsFirstLogin { get; set; }
        public DateTime U_UpdateDate { get; set; }
        public int U_UpdatedBy { get; set; }

    }
}
