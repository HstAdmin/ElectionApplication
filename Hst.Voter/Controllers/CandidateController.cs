using Hst.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Hst.Voter.Controllers
{
    public class CandidateController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CandidateList()
        {
            var result = await APIGetCaller<List<Candidate>>(ApiPath.Candidate.GetCandidate);
            if (result != null)
            {
                return View("CandidateList", result.Data);
            }
            else
            {
                return View();
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            var result = await APIGetCaller<List<Post>>(ApiPath.Candidate.GetPost);
            if (result != null)
            {
                ViewBag.Post = result.Data != null ? result.Data.Select(p => new SelectListItem() { Text = p.P_Name, Value = p.P_ID.ToString() }).ToList() : new List<SelectListItem>();
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Create(Candidate model)
        {
            if (ModelState.IsValid)
            {
                model.C_CreatedDate = model.C_ID > 0 ? model.C_CreatedDate : DateTime.Now;
                model.C_UpdatedDate = DateTime.Now;
                var result = await APIPostCaller<Candidate, Candidate>(ApiPath.Candidate.InsertUpdate, model);
                if (result != null && result.Data != null)
                    return RedirectToAction("CandidateList", result);
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
            var res = await APIGetCaller<List<Post>>(ApiPath.Candidate.GetPost);
            var result = await APIGetCaller<Candidate>(ApiPath.Candidate.GetCandidateByID(Convert.ToInt32(Id)));
            if (result != null)
            {
                if (res != null)
                {
                    ViewBag.Post = res.Data != null ? res.Data.Select(p => new SelectListItem() { Text = p.P_Name, Value = p.P_ID.ToString() }).ToList() : new List<SelectListItem>();
                }
                return View("Edit", result.Data);
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Edit(Candidate model)
        {
            if (ModelState.IsValid)
            {
                model.C_CreatedDate = model.C_ID < 0 ? model.C_CreatedDate : DateTime.Now;
                model.C_UpdatedDate = DateTime.Now;
                var result = await APIPostCaller<Candidate, Candidate>(ApiPath.Candidate.InsertUpdate, model);
                if (result != null && result.Data != null)

                {
                    return RedirectToAction("CandidateList", result);
                }
            }
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            var result = await APIPostCaller<string, string>(ApiPath.Candidate.Delete(Id), string.Empty);
            return RedirectToAction("CandidateList", result.Data);

        }
    }
}
