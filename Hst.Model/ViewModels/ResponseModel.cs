using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Model.ViewModels
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public string Message { get; set; }
        public bool Success
        {
            get { 
                return string.IsNullOrEmpty(this.ErrorMessage); 
            }
        }
        public int TotalItems { get; set; }
    }



}
