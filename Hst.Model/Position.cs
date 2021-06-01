using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Hst.Model
{
    [DataContract]
    public class Position
    {
        [DataMember(Name ="PId")]
        public int P_ID { get; set; }
        [DataMember(Name = "Position")]
        public int  P_Name { get; set; }
        [DataMember(Name = "EId")]
        public int  P_ElectionId { get; set; }
        [DataMember(Name = "EName")]
        public string P_ElectionName { get; set; }
        [DataMember]
        public int  P_CreatedDate { get; set; }
        [DataMember]
        public int  P_CreatedBy { get; set; }
        [DataMember]
        public int  P_UpdatedDate { get; set; }
        [DataMember]
        public int  P_UpdatedBy { get; set; }
    }
}
