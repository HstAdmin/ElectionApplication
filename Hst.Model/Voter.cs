using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Hst.Model
{

    [DataContract]
    public class Voter
    {
        [DataMember(Name ="VoterID")]
        public int V_ID { get; set; }
        [DataMember(Name = "VoterName")]
        public string V_Name { get; set; }
        [DataMember(Name = "OrgID")]
        public int V_OrganisationId { get; set; }
        [DataMember(Name = "OrgName")]
        public int V_OrganisationName { get; set; }
        [DataMember(Name = "EmailId")]
        public string V_EmailID { get; set; }
        [DataMember(Name = "MobileNo")]
        public string V_MobileNo { get; set; }
        public DateTime V_CreatedDate { get; set; }
        public int V_CreatedBy { get; set; }
        public DateTime? V_UpdatedDate { get; set; }
        public int V_UpdatedBy { get; set; }

    } }