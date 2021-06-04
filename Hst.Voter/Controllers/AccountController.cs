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
            Hst.Model.ViewModels.UserModel filters = new Hst.Model.ViewModels.UserModel();
            filters.Mobile = mobile;
            var result = await APIPostCaller<Hst.Model.ViewModels.UserModel, Hst.Model.ViewModels.UserModel>( ApiPath.User.GenerateOTP, filters);
            if (result != null && result.Data != null && result.Data.Id > 0)
            {
                TempData["RegisterSM"] = "OTP sent successfully";
            }
            //else
            //{
            // string message = result != null && result.Data != null && !string.IsNullOrEmpty(result.Data.ErrorMessage) ? result.Data.ErrorMessage : "Something went wrong.";
            //  ModelState.AddModelError("RegisterError", message);
            //   return View("Generate", model);
            //}

            return View();      
        }
    }
}
