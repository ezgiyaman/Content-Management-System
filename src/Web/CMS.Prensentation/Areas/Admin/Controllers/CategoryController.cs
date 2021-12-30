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
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        #region Create Category
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {

                await _categoryService.Create(model);
                TempData["Success"] = "The category has been added..!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "The category hasn't been added..!";
                return View(model);
            }
        }

        #endregion

        #region List Category

        public async Task<IActionResult> List()
        {
            return View(await _categoryService.GetCategories());

        }
        #endregion

        #region Update

        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetById(id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Update(model);
                TempData["Success"] = "The category has been added..!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "The category hasn't been added..!";
                return View(model);
            }
        }

        #endregion


        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Delete(id);
            return RedirectToAction("List");
        }
        #endregion

    }
}
