using CMS.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Prensentation.Models.Components
{
    //Prensentation katmanının kitlenmemesi için asenkron özelliği 
    //Sınıfın component gibi davranabilmesi için ViewComponent'tan kalıtım 
    public class MainMenuViewComponent : ViewComponent
    {
        private readonly IPageService _pageService;

        public MainMenuViewComponent(IPageService pageService)
        {
            this._pageService = pageService;
        }

        //InvokeAsync  : Eş zamansız olarak kullanabilmek için 
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _pageService.GetPages());
        }
    }
}
