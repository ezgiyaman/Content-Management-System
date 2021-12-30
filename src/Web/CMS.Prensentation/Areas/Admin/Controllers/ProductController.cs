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
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            this._productService = productService;
            this._categoryService = categoryService;
        }

        #region Create Product

        //Datalar dolduğu için asenkron işaretledim.
        public async Task<IActionResult> Create()
        {
            CreateProductDTO model = new CreateProductDTO();
            model.Categories = await _categoryService.GetCategories();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO model)
        {

            if (ModelState.IsValid)
            {
                await _productService.Create(model);
                TempData["Success"] = "The product has been added...!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "The product hasn't been added..!";
                return View(model);
            }
        }

        #endregion

        #region List Product

        public async Task<IActionResult> List()
        {
            return View(await _productService.GetProducts());
        }

        #endregion

        #region Update

        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetById(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductDTO model)
        {
            if (ModelState.IsValid)
            {
                await _productService.Update(model);
                TempData["Success"] = "The product has been added...!";
                return RedirectToAction("List");

            }
            else
            {
                TempData["Error"] = "The product hasn't  been added..!";
                return View(model);
            }
        }

        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);
            return RedirectToAction("List");
        }
        #endregion
    }
}
