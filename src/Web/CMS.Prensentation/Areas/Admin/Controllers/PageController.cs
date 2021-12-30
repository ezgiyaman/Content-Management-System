using CMS.Application.Models.DTOs;
using CMS.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Prensentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PageController : Controller
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService)
        {
            this._pageService = pageService;
        }

        public async Task<IActionResult> List() => View(await _pageService.GetPages());
        public IActionResult Create() => View(); //Sayfa getireceği için asenkron çalışmasına gerek yoktur.

        [HttpPost]
        public async Task<IActionResult> Create(CreatePageDTO model)
        {
            if (ModelState.IsValid)
            {

                await _pageService.Create(model);
                TempData["Success"] = "The page has been added..!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Warning"] = "The page hasn't been added...!";
                return View(model);
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            var page = await _pageService.GetById(id);
            return View(page);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePageDTO model)
        {

            if (ModelState.IsValid)
            {
                await _pageService.Update(model);
                TempData["Success"] = "The page has been updated!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Warning"] = "The page hasn't been updated!";
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _pageService.Delete(id);
            return RedirectToAction("List");
        }

    }
}
