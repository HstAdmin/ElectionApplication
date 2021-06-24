using Hst.Model;
using Hst.Model.Common;
using Hst.Model.ViewModels;
using Hst.Persistance.Reposiotry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hst.Voter.Controllers
{
    public class OrganisationController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }


        //*************GET******************
        [HttpGet]
        public async Task<IActionResult> OrganisationList()
        {
            var result = await APIGetCaller<List<Organisation>>(ApiPath.Organisation.GetOrganisation);
            if (result != null)
            {
                return View("OrganisationList", result.Data);
            }
            else
            {
                return View();
            }

        }


        public async Task<IActionResult> GetCityList(int StateId)
        {
            var res = await APIGetCaller<List<City>>(ApiPath.Organisation.GetCities(Convert.ToInt32(StateId)));

            if (res != null)
            {

                return Json(res.Data ?? new List<City>());
            }
            return View();
        }


        //*************INSERT/CREATE******************
        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            var result = await APIGetCaller<List<State>>(ApiPath.Organisation.GetStates);
            if (result != null)
            {

                ViewBag.State = result.Data != null ? result.Data.Select(c => new SelectListItem() { Text = c.S_Name, Value = c.S_ID.ToString() }).ToList() : new List<SelectListItem>();

            }
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Create(Organisation model)
        {
            if (ModelState.IsValid)
            {
                model.O_CreatedDate = model.O_ID > 0 ? model.O_CreatedDate : DateTime.Now;
                model.O_UpdatedDate = DateTime.Now;
                var result = await APIPostCaller<Organisation, Organisation>(ApiPath.Organisation.InsertUpdate, model);
                if (result != null && result.Data != null)
                    return RedirectToAction("OrganisationList", result);
                {
                    TempData["RegisterSM"] = "Data Inserted successfully";

                }
            }
            return View();
        }


        //************* EDIT******************
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id,int StateId)
        {
            
            var res= await APIGetCaller<List<State>>(ApiPath.Organisation.GetStates);
            //var cityres = await APIGetCaller<List<City>>(ApiPath.Organisation.GetCities(Convert.ToInt32(StateId)));
            var result = await APIGetCaller<Organisation>(ApiPath.Organisation.GetOrganisationByID(Convert.ToInt32(id)));
            if (result != null)
            {
                if (res != null)
                {
                    //if (cityres != null)
                    //{
                    //    return Json(cityres.Data ?? new List<City>());
                    //}
                    ViewBag.State = res.Data != null ? res.Data.Select(c => new SelectListItem() { Text = c.S_Name, Value = c.S_ID.ToString() }).ToList() : new List<SelectListItem>();
                }
                return View("Edit", result.Data);
            }
            return View();
            
        }


        [AllowAnonymous]
        public async Task<IActionResult> Edit(int id, Organisation model)
        {
            if (ModelState.IsValid)
            {
                model.O_CreatedDate = model.O_ID < 0 ? model.O_CreatedDate : DateTime.Now;
                model.O_UpdatedDate = DateTime.Now;
                var result = await APIPostCaller<Organisation, Organisation>(ApiPath.Organisation.InsertUpdate, model);
                if (result != null && result.Data != null)

                {
                    return RedirectToAction("OrganisationList", result);
                }
            }
            return View();
        }


        //*************DELETE******************
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await APIPostCaller<string, string>(ApiPath.Organisation.Delete(id), string.Empty);
            return RedirectToAction("OrganisationList", result.Data);

        }



    }


}

