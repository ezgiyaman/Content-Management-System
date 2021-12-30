using CMS.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Prensentation.Models.Components
{
    //Componentler ViewComponent'den kalıtım almalı.
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryService _categorService;

        public CategoriesViewComponent(ICategoryService categorService)
        {
            this._categorService = categorService;
        }

        public async Task<IViewComponentResult> InvokeAsync() => View(await _categorService.GetCategories());

    }
}
