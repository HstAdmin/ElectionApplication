using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Hst.Model
{

    public class Candidate
    {
        public int C_ID { get; set; }
        public string C_Name { get; set; }
        public int C_PostId { get; set; }
        public string PostName { get; set; }
        public int C_PositionId { get; set; }
        public string C_PositionName { get; set; }
        public DateTime C_CreatedDate { get; set; }
        public int C_CreatedBy { get; set; }
        public DateTime C_UpdatedDate { get; set; }
        public int C_UpdatedBy { get; set; }
        public string C_MobileNo { get; set; }
        public string C_Address { get; set; }
        public string C_Designation { get; set; }

    } 
}