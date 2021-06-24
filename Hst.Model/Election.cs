using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Hst.Model
{

    public class Election
    {
        public int E_ID { get; set; }
        public string E_Name { get; set; }
        public int E_OrganisationId { get; set; }
        public string OrganisationName { get; set; }
        public DateTime E_ScheduleDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive  { get; set; }
        public DateTime E_CreatedDate { get; set; }
        public int E_CreatedBy { get; set; }
        public DateTime? E_UpdatedDate { get; set; }
        public int E_UpdatedBy { get; set; }
    }
}
