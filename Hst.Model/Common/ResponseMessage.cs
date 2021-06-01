using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Model.Common
{
    public class ResponseMessage<T>
    {
        public int? TotalItems { get; set; }
        public int? NumberOfPages { get; set; }
        public T Records { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
