using CMS.Application.Models.DTOs;
using CMS.Application.Models.VMs;
using CMS.Application.Services.Interface;
using CMS.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.API.Controllers
{
    [Produces("application/json")] //Sadece tanımlanan türlerle sınırlanır.XML ile çalışmasını isteseydim ; "application/xml")] yazılırdı.
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<GetCategoryVM>> GetCategories()
        {
            //Getcategories methoddu zaten bana list döndürüyor o yüzden direk list döndük getcategories git bak 
            var categoryList = await _categoryService.GetCategories();

            return categoryList;
        }

        /// <summary>
        /// Get Category By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetCategoryById")]
        [ProducesResponseType(200)] //status code ok başarılı dönmesi için ekledik.

        //category ıd'sinden yakalayı getirme işlemi
        public async Task<UpdateCategoryDTO> GetCategoryById(int id)
        {
            var category = await _categoryService.GetById(id);
            return category;
        }


        /// <summary>
        /// Get one category by slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("{slug}", Name = "GetCategoryBySlug")]
        public async Task<Category> GetCategoryBySlug(string slug)
        {
            var category = await _categoryService.GetBySlug(slug);

            return category;
        }
        /// <summary>
        /// Create a category
        /// </summary>
        /// <param name="model">
        /// Please check category model in the schemas table
        /// </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] CreateCategoryDTO model)
        { //action result olarak işaretleyince bad request ve ok kabul görüyor yazmsak bize kızıyor."[FromBody]" atacağımız request'in gövdesinde veriyi göndereceğimiz zaman tercih ediyoruz.
            if (ModelState.IsValid)
            {
                await _categoryService.Create(model);
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// Update one category.Category slug must be uniqe.
        /// </summary>
        /// <param name="model">
        /// Please check category model in the shemas table
        /// </param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Category>> UpdateCategory([FromBody] UpdateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Update(model);
                return Ok(model);
            }
            return BadRequest(model);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.Delete(id);
            return Ok();
        }
    }
}
