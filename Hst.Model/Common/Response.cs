using Hst.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Hst.Model.Common
{
    public static class Response<T>
    {
        public static ResponseData<T> CreateResponse(bool isSuccess, string message, T entity, int statusCode = 0, string currentUserTimeZoneId = "", int totalRecordCount = 0)
        {
            ResponseData<T> response = new ResponseData<T>();
            response.Success = isSuccess;
            response.Message = message;
            response.Data = entity;
            response.Code = statusCode;
            return response;
        }

        public static ResponceDataList<T> CreateListResponse(bool isSuccess, string message, T entity, int statusCode = 0, int TotalRecords = 0, int PageNo = 0, int TotalPage = 0)
        {
            ResponceDataList<T> response = new ResponceDataList<T>();
            response.Success = isSuccess;
            response.Message = message;
            response.Data = entity;
            response.Code = statusCode;
            response.TotalRecords = TotalRecords;
            response.CurrentPage = PageNo;
            response.TotalPage = TotalPage;
            return response;
        }
        public static ResponseData<T> ToSuccessResponse(string message, T entity, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return CreateResponse(true, message, entity, (int)httpStatusCode);
        }

        public static ResponseData<T> ToErrorResponse(string message, T entity, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            return CreateResponse(false, message, entity, (int)httpStatusCode);
        }



    }
}
