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
    public class CandiController : BaseController
    {
        private readonly ICandidateService _service = null;

        public CandiController(IHttpContextAccessor httpContextAccessor, ILoggerManager loggerManager, IConfiguration con, ICandidateService candidateService
            ) : base(httpContextAccessor, loggerManager, con)
        {
            _service = candidateService;

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetAll")]
        public async Task<ResponseData<List<Candidate>>> GetCandidate()
        {
            var result = await _service.GetCandidate();
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetCandidateByID")]
        public async Task<ResponseData<Candidate>> GetCandidateByID(int Id)
        {
            var result = await _service.GetCandidateByID(Id);
            return result;
        }


        [HttpPost]
        [Route("InsertUpdate")]
        public async Task<ResponseData<Candidate>> InsertUpdate(Candidate model)
        {
            if (model == null)
            {
                return Response<Candidate>.CreateResponse(false, "Field should not be blank.", null);
            }
            var data = await _service.InsertUpdate(model);
            return data;
        }


        [HttpGet]
        [Route("GetPost")]
        public async Task<ResponseData<List<Post>>> GetPost()
        {
            var data = await _service.GetPost();
            return Response<List<Post>>.CreateResponse(true, string.Empty, data);
        }

     
        [HttpPost]
        [AllowAnonymous]
        [Route("Delete")]
        public async Task<ResponseData<Candidate>> Delete(int Id)
        {
            if (Id > 0)
            {
                var result = await _service.Delete(Id);
                return result;

            }
            return Response<Candidate>.CreateResponse(false, "No Data to delete", null);
        }
    }
}



