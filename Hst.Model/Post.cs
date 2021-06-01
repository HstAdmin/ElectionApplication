using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hst.Model
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public int P_Id { get; set; }
        public string P_Name { get; set; }
        public int? P_EId { get; set; }
        public virtual Election Election { get; set; }
    }
}
