using Hst.Model;
using Hst.Model.Common;
using Hst.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hst.Voter.Controllers
{
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string mobile)
        {
            if (ModelState.IsValid)
            {
                Hst.Model.ViewModels.UserModel filters = new Hst.Model.ViewModels.UserModel();
                filters.Mobile = mobile;
                var result = await APIPostCaller<Hst.Model.ViewModels.UserModel, Hst.Model.ViewModels.UserModel>(ApiPath.User.GenerateOTP, filters);
                if (result != null && result.Data != null && result.Data.Id > 0)
                {
                    TempData["RegisterSM"] = "OTP sent successfully";
                }
            }
            return View();
        }


        public async Task<ActionResult> VerifyOTP(string OTP)
        {
            if (ModelState.IsValid)
            {
                Hst.Model.ViewModels.UserModel filters = new Hst.Model.ViewModels.UserModel();
                filters.Otp = OTP;
                var result = await APIPostCaller<Hst.Model.ViewModels.UserModel, Hst.Model.ViewModels.UserModel>(ApiPath.User.VerifyOTP, filters);
                if (result != null && result.Data != null && result.Data.Id > 0)
                {
                    TempData["RegisterSM"] = "OTP Confirmed";
                }
            }
            return View();

        }

    }
}
