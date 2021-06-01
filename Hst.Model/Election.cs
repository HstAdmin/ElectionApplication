using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Hst.Model
{
    [DataContract]
    public class Election
    {
        [DataMember(Name ="EId")]
        public int E_ID { get; set; }
        [DataMember(Name = "EName")]
        public string E_Name { get; set; }
        [DataMember(Name = "OrgId")]
        public int E_OrganisationId { get; set; }
        [DataMember(Name = "OrgName")]
        public int E_OrganisationName { get; set; }
        public DateTime E_ScheduleDate { get; set; }
        public DateTime E_CreatedDate { get; set; }
        public int E_CreatedBy { get; set; }
        public DateTime? E_UpdatedDate { get; set; }
        public int E_UpdatedBy { get; set; }
    }
}
