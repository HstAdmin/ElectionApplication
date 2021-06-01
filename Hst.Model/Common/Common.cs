using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hst.Model.Common
{
    public enum ErrorCode { 
        [Description("User is not Authorized.")]
        UnAuthorized=101, 
        [Description("Record is already exist.")]
        Duplicate =-1,
        [Description("Record is is successfully inserted.")]
        Inserted = 103,
        [Description("Record is successfully updated.")]
        Updated = 104,
        [Description("Record is successfully deleted.")]
        Deleted = 105,
        [Description("Record is not found.")]
        NotFound = 106,
        [Description("Fatel Error")]
        FatelError = 107

    }
    
}
