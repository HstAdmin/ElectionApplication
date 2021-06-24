using Hst.Business.Services.Interfaces;
using Hst.Logger;
using Hst.Model;
using Hst.Model.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hst.VoterAPI.Controllers
{
    public class PostAPIController : BaseController
    {

        private readonly IPostService _service = null;

        public PostAPIController(IHttpContextAccessor httpContextAccessor, ILoggerManager loggerManager, IConfiguration con, IPostService postService
            ) : base(httpContextAccessor, loggerManager, con)
        {
            _service = postService;

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetAll")]
        public async Task<ResponseData<List<Post>>> GetPost()
        {
            var result = await _service.GetPost();
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetPostByID")]
        public async Task<ResponseData<Post>> GetPostByID(int Id)
        {
            var result = await _service.GetPostByID(Id);
            return result;
        }


        [HttpPost]
        [Route("InsertUpdate")]
        public async Task<ResponseData<Post>> InsertUpdate(Post model)
        {
            if (model == null)
            {
                return Response<Post>.CreateResponse(false, "Field should not be blank.", null);
            }
            var data = await _service.InsertUpdate(model);
            return data;
        }


        [HttpGet]
        [Route("GetElec")]
        public async Task<ResponseData<List<Election>>> GetElec()
        {
            var data = await _service.GetElec();
            return Response<List<Election>>.CreateResponse(true, string.Empty, data);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("Delete")]
        public async Task<ResponseData<Post>> Delete(int Id)
        {
            if (Id > 0)
            {
                var result = await _service.Delete(Id);
                return result;

            }
            return Response<Post>.CreateResponse(false, "No Data to delete", null);
        }
    }
}

