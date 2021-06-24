using Microsoft.Extensions.Configuration;
using Hst.Business.Services.Interfaces;
using Hst.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hst.Model.Common;
using Hst.Model;
using Microsoft.AspNetCore.Authorization;

namespace Hst.VoterAPI.Controllers
{
    public class ElectionController : BaseController
    {
        private readonly IElectionService _service = null;

        public ElectionController(IHttpContextAccessor httpContextAccessor, ILoggerManager loggerManager, IConfiguration con, IElectionService electionService
            ) : base(httpContextAccessor, loggerManager, con)
        {
            _service = electionService;

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetAll")]
        public async Task<ResponseData<List<Election>>> GetElection()
        {
            var result = await _service.GetElection();
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetElectionByID")]
        public async Task<ResponseData<Election>> GetElectionByID(int Id)
        {
            var result = await _service.GetElectionByID(Id);
            return result;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("InsertUpdate")]
        public async Task<ResponseData<Election>> InsertUpdate(Election model)
        {
            if (model == null)
            {
                return Response<Election>.CreateResponse(false, "Field should not be blank.", null);
            }
            var data = await _service.InsertUpdate(model);
            return data;
        }

        [HttpGet]
        [Route("GetOrg")]
        public async Task<ResponseData<List<Organisation>>> GetOrg()
        {
            var data = await _service.GetOrg();
            return Response<List<Organisation>>.CreateResponse(true, string.Empty, data);
        }

        

        [HttpPost]
        [AllowAnonymous]
        [Route("Delete")]
        public async Task<ResponseData<Election>> Delete(int Id)
        {
            if (Id > 0)
            {
                var result = await _service.Delete(Id);
                return result;

            }
            return Response<Election>.CreateResponse(false, "No Data to delete", null);
        }
    }
}




