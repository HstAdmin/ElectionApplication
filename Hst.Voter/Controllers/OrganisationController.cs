using Hst.Model;
using Hst.Model.Common;
using Hst.Model.ViewModels;
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

        [HttpGet]
        public async Task<IActionResult> OrganisationList()
        {
            
            
            var result = await APIGetCaller<List<Organisation>>(ApiPath.Organisation.GetOrganisation);
            if (result != null) {
                return View("OrganisationList", result.Data);
            }
            else {
                return View();
            }
              
        }



        public async Task<IActionResult> GetStates()
        {
            var result = await APIGetCaller<List<DropDownVM>>(ApiPath.Organisation.GetStates);
            ViewBag.Countries = result;
            return Json(result.Data ?? new List<DropDownVM>());
        }











        public async Task<IActionResult> GetCityList(int StateId)

        {
            var res = await APIGetCaller<List<City>>(ApiPath.Organisation.GetCities(Convert.ToInt32(StateId)));
            
            if (res != null)
            {
            
                return Json(res.Data ?? new List<City>());
                //List<SelectListItem> licities = new List<SelectListItem>();
                //return Json(res.Data ?? new List<City>().Where(City => City.C_StateID== StateId).ToList());
                //ViewBag.City = res.Data != null ? res.Data.Select(c => new SelectListItem() { Text = c.C_Name, Value = c.C_ID.ToString() }).ToList() : new List<SelectListItem>();
                //    foreach (var x in res)
                //    {
                //        licities.Add(new SelectListItem { Text = x.C_Name, Value = x.C_ID.ToString() });
                //    }
                //}
                //return Json(new SelectList(licities, "Text", "Value"));
            }
            return View();
        }









        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            var result = await APIGetCaller<List<State>>(ApiPath.Organisation.GetStates);
            if (result != null )
            {

                ViewBag.State = result.Data != null? result.Data.Select(c => new SelectListItem() { Text = c.S_Name, Value = c.S_ID.ToString() }).ToList():new List<SelectListItem>();

            }
            //ViewBag.State = result.Data;
            return View();
        }
       

       [AllowAnonymous]
       [HttpPost]
        public async Task<ActionResult> Create(Organisation model)
        {
            if (ModelState.IsValid)
            {
                //Organisation filters = new Organisation();
                //filters.O_ID =1;
                //var result = await APIPostCaller<Organisation, Organisation>(ApiPath.Organisation.InsertUpdate, model);
                //if (result != null && result.Data != null)
                model.O_ID = 0;
                var result = await APIPostCaller<Organisation, int>(ApiPath.Organisation.InsertUpdate, model);
                return Json(result);
                {
                    TempData["RegisterSM"] = "Data Inserted successfully";
                }
            }
            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (ModelState.IsValid)
            {
                Organisation filters = new Organisation();
                filters.O_ID = id;
                var result = await APIPostCaller<Organisation, Organisation>(ApiPath.Organisation.InsertUpdate, filters);
                if (result != null && result.Data != null)
                {
                    TempData["RegisterSM"] = "Data updated successfully";
                }
            }
            //return View();
            return RedirectToAction("OrganisationList");
        }




         public async Task<ActionResult> Delete(int id)
            {
                var result = await APIPostCaller<string, string>(ApiPath.Organisation.Delete(id), string.Empty);
                return Json(result);
            }
            
        }


    }

