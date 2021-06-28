using Hst.Model;
using Hst.Voter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hst.Voter.Controllers
{
    public class ElectionPostCandidateController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetElectionCandidates(int? Id)
        {
            Id = 1;
            var result = await APIGetCaller<ElectionCandidatePost>(ApiPath.ECP.GetElectionCandidates(Convert.ToInt32(Id)));
            if (result != null)
            {
                return View ("GetElectionCandidates", result.Data);
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> GetElectionCandidates(ElectionCandidatePost model)
        {
            if (ModelState.IsValid)
            {
                List<Result> result = new List<Result>();
                
                    foreach(ElectionPosts electionPosts in model.ElectionPosts)
                    {
                        foreach(ElectionCandidate electionCandidate in electionPosts.ElectionCandidates)
                        {
                            if (electionCandidate.IsSelected)
                            {
                                result.Add(new Result()
                                {
                                    R_CandidateId = electionCandidate.CandidateID,
                                    R_ElectionId = model.ElectionID,
                                    R_PostId = electionPosts.PostID,
                                    R_VoterId = VoterModel.VoterID,
                                    R_CreatedDate =  DateTime.Now
                            });
                            }
                        }
                    }
                
                
                var res = await APIPostCaller<List<Result>, Result>(ApiPath.ECP.SaveData, result);
                if (res != null && res.Data != null)
                    return RedirectToAction("ResultList", res);
                {
                    TempData["RegisterSM"] = "Data Inserted successfully";

                }
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ResultList()
        {
            var result = await APIGetCaller<List<Result>>(ApiPath.ECP.GetResult);
            if (result != null)
            {
                return View("ResultList", result.Data);
            }
            else
            {
                return View();
            }
        }




    }
}
