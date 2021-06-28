using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Model
{
    public class Result
    {
        public int R_ID { get; set; }
        public int R_ElectionId { get; set; }
        public int R_PostId { get; set; }
        public int R_CandidateId { get; set; }
        public int R_VoterId { get; set; }
        public DateTime R_CreatedDate { get; set; }
    }
}
