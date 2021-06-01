using System;

namespace Hst.Model.Common
{
    public class RequestMessage
    {
        public int PageNumber { get; set; }
        public int ItemsPerPages { get; set; }
        public string  SearchText { get; set; }
        public string OrderBy { get; set; }
        public bool  ShowAll { get; set; }
        public int  UserId { get; set; }
    }
}
