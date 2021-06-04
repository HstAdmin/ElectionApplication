using Hst.Model;
using Hst.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Hst.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Hst.Model.ViewModels;
using Newtonsoft.Json;

namespace Hst.Voter.Controllers
{
    public class BaseController : Controller
    {
        //private readonly IUserService _service = null;
        //private readonly IAuthenticationManager _authManager = null;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected ISession _session;
       

        protected static string Token { get; set; }

        protected async Task<ResponseData<T>> APIGetCaller<T>(string url)
        {
            url = ApiPath.APIBaseUrl + url;
            string authToken = Token;
            ResponseData<T> response = await url.ToGetAPI<T>(authToken);
            return response;
        }
        protected async Task<ResponseData<TResponse>> APIPostCaller<TRequest, TResponse>(string url, TRequest request)
        {
            url = ApiPath.APIBaseUrl + url;
            string authToken = Token;
            ResponseData<TResponse> response = await url.ToPostAPI<TRequest, TResponse>(request, authToken);
            return response;
        }
    }
}
