using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Hst.Model
{

   [DataContract]     
   public class State
    {
        [DataMember(Name ="StateId")]
        public int S_ID { get; set; }
        [DataMember(Name = "StateName")]
        public string S_Name { get; set; }
    }

    public class City {
        [DataMember(Name = "CityId")]
        public int C_ID { get; set; }
        [DataMember(Name = "CityName")]
        public string C_Name { get; set; }
        [DataMember(Name = "StateID")]
        public int C_StateID { get; set; }
        [DataMember(Name = "StateName")]
        public string StateName { get; set; }
    }
}
