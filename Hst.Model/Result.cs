using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Model
{
    public class Result
    {
        public int VA_ID { get; set; }
        public int VA_VoterID { get; set; }
        public int VA_ElectionId { get; set; }
        public int VA_PositionId { get; set; }
        public int VA_CandidateID { get; set; }
        public DateTime VA_CreatedDate { get; set; }
    }
}
