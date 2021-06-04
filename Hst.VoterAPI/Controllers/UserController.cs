using Hst.Business.Services.Interfaces;
using Hst.Logger;
using Hst.Model.Common;
using Hst.Model.ViewModels;
using Hst.VoterAPI.AuthModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hst.VoterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _service = null;
        private readonly IAuthenticationManager _authManager = null;
        public UserController(IHttpContextAccessor httpContextAccessor, ILoggerManager loggerManager, IConfiguration con, IUserService userService,
            IAuthenticationManager authManager
            ) :base(httpContextAccessor, loggerManager, con)
        {
            _service = userService;
            _authManager = authManager;
        }



        [Route("GenerateOTP")]
        [HttpPost]
        public async Task<ResponseData<UserModel>> GenerateOTP(UserModel model)
        {
            if (model != null && (string.IsNullOrEmpty(model.Mobile)))
            {
                return Response<UserModel>.CreateResponse(false, "Field should not be blank.", null);
            }
            var data = await _service.GenerateOTP(model);
            return Response<UserModel>.CreateResponse(true, string.Empty, data);
        }



        [Route("ValidateUser")]
        [HttpPost]
        public async Task<ResponseData<UserModel>> ValidateUser([FromBody] UserModel model)
        {
            if (model != null && (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password)))
            {
                return Response<UserModel>.CreateResponse(false, "User credentials should not blank.", null);
            }

            var userDetails = await _service.ValidateUser(model.UserName, model.Password);
            if (userDetails != null)
            {
                if (string.IsNullOrEmpty(userDetails.ErrorMessage))
                {
                    userDetails.Token = _authManager.GenerateToken(userDetails, false);
                    userDetails.RefreshToken = _authManager.GenerateToken(userDetails, true);
                    return Response<UserModel>.CreateResponse(true, string.Empty, userDetails);
                }
                else
                    return Response<UserModel>.CreateResponse(false, userDetails.ErrorMessage, null);
            }
            else
                return Response<UserModel>.CreateResponse(false, "Invalid username or password.", null);
        }

        [Route("SaveUser")]
        [HttpPost]
        public async Task<ResponseData<UserModel>> SaveUser([FromBody] UserModel model)
        {
            var data = await _service.SaveUser(model);
            return Response<UserModel>.CreateResponse(true, string.Empty, data);
        }


        [Route("CheckUserExisting")]
        [HttpPost]
        public async Task<ResponseData<UserModel>> CheckUserExisting(UserModel model)
        {
            var data = await _service.CheckUserExisting(model);
            return Response<UserModel>.CreateResponse(true, string.Empty, data);
        }

        [Route("SendOtp")]
        [HttpPost]
        public async Task<ResponseData<bool>> SendOtp(UserModel model)
        {
            var data = await _service.SendOtp(model);
            return Response<bool>.CreateResponse(true, string.Empty, data);
        }

        [Route("CheckOtp")]
        [HttpPost]
        public async Task<ResponseData<UserModel>> CheckOtp(UserModel model)
        {
            var data = await _service.CheckOtp(model);
            return Response<UserModel>.CreateResponse(true, string.Empty, data);
        }

        [Route("ChangePassword")]
        [HttpPost]
        public async Task<ResponseData<ChangePasswordModel>> ChangePassword([FromBody] ChangePasswordModel objModel)
        {
            var data = await _service.ChangePassword(objModel);
            return Response<ChangePasswordModel>.CreateResponse(true, string.Empty, data);
        }

        [Route("ForgotPassword")]
        [HttpPost]
        public async Task<ResponseData<ChangePasswordModel>> ForgotPassword([FromBody] ChangePasswordModel objModel)
        {
            var data = await _service.ForgotPassword(objModel);
            return Response<ChangePasswordModel>.CreateResponse(true, string.Empty, data);
        }

        [Route("CheckForgotPasswordCode")]
        [HttpPost]
        public async Task<ResponseData<UserModel>> CheckForgotPasswordCode([FromBody] ChangePasswordModel objModel)
        {
            var data = await _service.CheckForgotPasswordCode(objModel);
            return Response<UserModel>.CreateResponse(true, string.Empty, data);
        }

        [Route("GetUserById/{id}")]
        [HttpGet]
        public async Task<ResponseData<UserModel>> GetUserById(int id)
        {
            var data = await _service.GetUserById(id);
            return Response<UserModel>.CreateResponse(true, string.Empty, data);
        }

        [Route("SaveProfileImage")]
        [HttpPost]
        public async Task<ResponseData<bool>> SaveProfileImage([FromBody] UserModel objModel)
        {
            var data = await _service.SaveProfileImage(objModel);
            return Response<bool>.CreateResponse(true, string.Empty, data);
        }
    }
}
