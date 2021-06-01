using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Model.Common
{
    public enum ResultStatus
    {
        Fail = 0,
        Success = 1,
        InvalidInputs = 2,
        NoRecordsFound = 3,
        ServerError = 4
    }

    public class ResponseData<TEntity>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public TEntity Data { get; set; }
        public int Code { get; set; }
    }

    public class ResponceJsonData
    {
        public string Result { get; set; }
    }

    public class ResponceDataList<TEntity>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public TEntity Data { get; set; }
        public int Code { get; set; }
        
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }

    }

}
