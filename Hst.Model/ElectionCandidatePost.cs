using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Model
{
    public class ElectionCandidatePost
    {
        public int ElectionID { get; set; }
        public string ElectionName { get; set; }
        public List<ElectionPosts> electionPosts { get;set; }
    }

    public class ElectionPosts
    {
        public int PostID { get; set; }
        public int ElectionID {get;set;}
        public string PostName{get;set;}
        public List<ElectionCandidate> electionCandidates { get; set; }
    }
    public class ElectionCandidate
    {
        public int CandidateID { get; set; }
        public string CandidateName{ get; set; }
        public int PostID { get; set; }
    }
}
