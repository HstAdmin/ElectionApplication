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
    public class PostController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> PostList()
        {
            var result = await APIGetCaller<List<Post>>(ApiPath.Post.GetPost);
            if (result != null)
            {
                return View("PostList", result.Data);
            }
            else
            {
                return View();
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            var result = await APIGetCaller<List<Election>>(ApiPath.Post.GetElec);
            if (result != null)
            {
                ViewBag.Election = result.Data != null ? result.Data.Select(e => new SelectListItem() { Text = e.E_Name, Value = e.E_ID.ToString() }).ToList() : new List<SelectListItem>();
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Create(Post model)
        {
            if (ModelState.IsValid)
            {
                model.P_CreatedDate = model.P_ID > 0 ? model.P_CreatedDate : DateTime.Now;
                model.P_UpdatedDate = DateTime.Now;
                var result = await APIPostCaller<Post, Post>(ApiPath.Post.InsertUpdate, model);
                if (result != null && result.Data != null)
                    return RedirectToAction("PostList", result);
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
            var res = await APIGetCaller<List<Election>>(ApiPath.Post.GetElec);
            var result = await APIGetCaller<Post>(ApiPath.Post.GetPostByID(Convert.ToInt32(Id)));
            if (result != null)
            {
                if (res != null)
                {
                    ViewBag.Election = res.Data != null ? res.Data.Select(e => new SelectListItem() { Text = e.E_Name, Value = e.E_ID.ToString() }).ToList() : new List<SelectListItem>();
                }
                return View("Edit", result.Data);
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Edit(Post model)
        {
            if (ModelState.IsValid)
            {
                model.P_CreatedDate = model.P_ID < 0 ? model.P_CreatedDate : DateTime.Now;
                model.P_UpdatedDate = DateTime.Now;
                var result = await APIPostCaller<Post, Post>(ApiPath.Post.InsertUpdate, model);
                if (result != null && result.Data != null)

                {
                    return RedirectToAction("PostList", result);
                }
            }
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            var result = await APIPostCaller<string, string>(ApiPath.Post.Delete(Id), string.Empty);
            return RedirectToAction("PostList", result.Data);

        }
    }
}

