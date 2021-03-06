using Hst.Model.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Hst.Model
{
    
    public class Organisation
    {
        public int O_ID { get; set; }
        public string O_Name { get; set; }
        public  string O_Address { get; set; }
        public  int O_CityID { get; set; }
        public  int O_StateID { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public  DateTime O_CreatedDate { get; set; }
        public  int O_CreatedBy { get; set; }
        public  DateTime? O_UpdatedDate { get; set; }
        public  int O_UpdatedBy { get; set; }
    }

}
