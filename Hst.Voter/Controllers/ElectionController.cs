using Hst.Model;
using Hst.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hst.Model.ViewModels;
using Hst.Persistance.Reposiotry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Hst.Voter.Controllers
{
    public class ElectionController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ElectionList()
        {
            var result = await APIGetCaller<List<Election>>(ApiPath.Election.GetElection);
            if (result != null)
            {
                return View("ElectionList", result.Data);
            }
            else
            {
                return View();
            }
        }



        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            var result = await APIGetCaller<List<Organisation>>(ApiPath.Election.GetOrg);
            if(result!=null)
            {
                ViewBag.Org=result.Data!=null? result.Data.Select(o => new SelectListItem() { Text = o.O_Name, Value = o.O_ID.ToString() }).ToList() : new List<SelectListItem>();
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Create(Election model)
        {
            if (ModelState.IsValid)
            {
                model.StartDate = model.E_ID > 0 ? model.StartDate : DateTime.Now;
                model.EndDate = DateTime.Now;
                var result = await APIPostCaller<Election, Election>(ApiPath.Election.InsertUpdate, model);
                if (result != null && result.Data != null)
                    return RedirectToAction("ElectionList", result);
                {
                    TempData["RegisterSM"] = "Data Inserted successfully";

                }
            }
            return View();
        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Edit(int? Id)
        {
            var res = await APIGetCaller<List<Organisation>>(ApiPath.Election.GetOrg);
            var result = await APIGetCaller<Election>(ApiPath.Election.GetElectionByID(Convert.ToInt32(Id)));
            if (result != null)
            {
                if (res != null)
                {
                    ViewBag.Org = res.Data != null ? res.Data.Select(o => new SelectListItem() { Text = o.O_Name, Value = o.O_ID.ToString() }).ToList() : new List<SelectListItem>();
                }
                return View("Edit", result.Data);
            }
            return View();
        }


        [AllowAnonymous]
        public async Task<IActionResult> Edit(Election model)
        {
            if (ModelState.IsValid)
            {
                model.StartDate = model.E_ID < 0 ? model.StartDate : DateTime.Now;
                model.EndDate = DateTime.Now;
                var result = await APIPostCaller<Election, Election>(ApiPath.Election.InsertUpdate, model);
                if (result != null && result.Data != null)

                {
                    return RedirectToAction("ElectionList",result);
                }
            }
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            var result = await APIPostCaller<string, string>(ApiPath.Election.Delete(Id), string.Empty);
            return RedirectToAction("ElectionList" ,result.Data);

        }
    }
}
