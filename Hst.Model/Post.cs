using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hst.Model
{
    public class Post
    {
        public int P_ID { get; set; }
        public string P_Name { get; set; }
        public int P_EId { get; set; }
        public string ElectionName { get; set; }
        public DateTime P_CreatedDate { get; set; }
        public int P_CreatedBy { get; set; }
        public DateTime P_UpdatedDate { get; set; }
        public int P_UpdatedBy { get; set; }
    }
}
