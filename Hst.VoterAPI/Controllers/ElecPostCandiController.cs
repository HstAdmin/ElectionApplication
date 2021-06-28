using Hst.Business.Services.Interfaces;
using Hst.Logger;
using Hst.Model;
using Hst.Model.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hst.VoterAPI.Controllers
{
    public class ElecPostCandiController : BaseController
    {
        private readonly IElecPostCandiService _service = null;

        public ElecPostCandiController(IHttpContextAccessor httpContextAccessor, ILoggerManager loggerManager, IConfiguration con, IElecPostCandiService EPCService
            ) : base(httpContextAccessor, loggerManager, con)
        {
            _service = EPCService;

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetElectionCandidates")]
        public async Task<ResponseData<ElectionCandidatePost>> GetElectionCandidates(int Id)
        {
            var result = await _service.GetElectionCandidates(Id);
            return result;
        }

        [HttpPost]
        [Route("SaveData")]
        public async Task<ResponseData<Result>> SaveData(List<Result> model)
        {
            if (model == null)
            {
                return Response<Result>.CreateResponse(false, "Field should not be blank.", null);
            }
            var data = await _service.SaveData(model);
            return data;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("GetAll")]
        public async Task<ResponseData<List<Result>>> GetResult()
        {
            var result = await _service.GetResult();
            return result;
        }

    }

}