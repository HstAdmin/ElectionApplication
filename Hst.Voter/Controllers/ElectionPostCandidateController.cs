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

        public async Task<IActionResult> GetElectionCandidates()
        {

            return View();
        }
    }
}
