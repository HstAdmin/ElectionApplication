using Microsoft.AspNetCore.Mvc;
using Hst.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Hst.Logger;
using Microsoft.Extensions.Configuration;
using Hst.Model;
using Hst.Model.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Hst.Model.Common;
using System;
using System.Collections.Generic;

namespace Hst.VoterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgController : BaseController
    {
        private readonly IOrgService _service = null;

        public OrgController(IHttpContextAccessor httpContextAccessor, ILoggerManager loggerManager, IConfiguration con, IOrgService orgService
            ) : base(httpContextAccessor, loggerManager, con)
        {
            _service = orgService;

        }




        [HttpGet]
        [AllowAnonymous]
        [Route("GetAll")]
        public async Task<ResponseData<List<Organisation>>> GetOrganisation()
        {
              var result =await _service.GetOrganisation();
              return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetOrgByID")]
        public async Task<ResponseData<Organisation>> GetOrganisationByID(int id)
        {
            var result = await _service.GetOrganisationByID(id);
            return result;
        }


        [Route("GetStates")]
        [HttpGet]
        public async Task<ResponseData<List<State>>> GetStates()
        {
            var data = await _service.GetStates();
            return Response<List<State>>.CreateResponse(true, string.Empty, data);
        }

        [Route("GetCities/{StateId}")]
        [HttpGet]
        public async Task<ResponseData<List<City>>> GetCities(int StateId)
        {
            var data = await _service.GetCities(StateId);
            return Response<List<City>>.CreateResponse(true, string.Empty, data);
           // return data;
        }
    











        [Route("InsertUpdate")]
        [HttpPost]
        public async Task<ResponseData<Organisation>> InsertUpdate(Organisation model)
        {
            if (model == null)
            {
                return Response<Organisation>.CreateResponse(false, "Field should not be blank.", null);
            }
            //var response = new ResponseData<Organisation>();
            var data = await _service.InsertUpdate(model);
            return data;
            
        }

        [Route("Delete")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ResponseData<Organisation>> Delete(int id)
        {

            if (id > 0)
            {
                var result = await _service.Delete(id);
                return result;
                
            }
            return Response<Organisation>.CreateResponse(false, "No Data to delete", null);
        }

        

        //[Route("Delete")]
        //[HttpPost]
        //public ResponseModel<string> Delete(int id)
        //{
        //    try
        //    {
        //        if (id > 0)
        //        {
        //            _service.Delete(id);
        //            return new ResponseModel<string>() { Data = "Successfully updated" };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseModel<string>() { Data = "Error", ErrorMessage = ex.Message, TotalItems = 0 };
        //    }
        //    return null;
        //}
    }
}
