using Hst.Model;
using Hst.Model.ViewModels;
using Hst.Voter.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                UserModel filters = new UserModel();
                filters.Mobile = userViewModel.MobileNo;
                var result = await APIPostCaller<UserModel, UserModel>(ApiPath.User.GenerateOTP, filters);
                if (result != null && result.Data != null && !string.IsNullOrEmpty(result.Data.Otp))
                {
                    TempData["RegisterSM"] = "OTP sent successfully";
                }
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyOTP(string OTP)
        {
            if (ModelState.IsValid)
            {
                UserModel filters = new UserModel();
                filters.Otp = OTP;
                var result = await APIPostCaller<UserModel, UserModel>(ApiPath.User.VerifyOTP, filters);
                if (result != null && result.Data != null && result.Data.Id > 0)
                {
                    TempData["RegisterSM"] = "OTP Confirmed";
                }
            }
            return View();

        }

        //[AcceptVerbs("GET","POST")]
        //public async Task<IActionResult> IsMobileExist(string mobileNo)
        //{
        //    await APIPostCaller<UserModel, UserModel>(ApiPath.User.GenerateOTP, mobileNo);
        //}

    }
}
