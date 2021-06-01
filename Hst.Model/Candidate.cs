using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Hst.Model
{
    [DataContract]
    public class Candidate
    {
        [DataMember(Name ="Id")]
        public int C_ID { get; set; }
        [DataMember(Name = "CandidateName")]
        public int C_Name { get; set; }
        [DataMember(Name = "ElectionId")]
        public int C_ElectionId { get; set; }
        [DataMember(Name = "ElectionName")]
        public int C_ElectionName { get; set; }
        [DataMember(Name = "PositionId")]
        public int C_PositionId { get; set; }
        [DataMember(Name = "PositionName")]
        public int C_PositionName { get; set; }
        public int C_CreatedDate { get; set; }
        public int C_CreatedBy { get; set; }
        public int C_UpdatedDate { get; set; }
        public int C_UpdatedBy { get; set; }
            
    } 
}