using CMS.Application.Extensions;
using CMS.Application.Models.DTOs;
using CMS.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Prensentation.Controllers
{
    //Authorize => Kullanıcı sisteme login olmadan herhangi bir işlem yapmamasını istiyorsak kullanıyoruz.
    //AutoValidateAntiforgeryToken => Kullanıcı Register ya da LogIn olmak istediğinde istek atan kullanıcıya bir token gönderilir.Güvenlik gereği bu işlem yapılır.Talepte bulunan kullanıcı ile server arasında kimlik doğrulması yapılır.

    [Authorize, AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUserService;

        public AccountController(IAppUserService appUserService)
        {
            this._appUserService = appUserService;
        }

        #region Register
        [AllowAnonymous] //Yukarıda koyduğumuz Authorize kuralını burada "AllowAnonymous" vasıtasıyla kırdık.Yani kullanıcı bu action methodu request attığında buraya erişebilecek.

        public IActionResult Register()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View();
        }


        [AllowAnonymous, HttpPost]

        public async Task<IActionResult> Register(RegisterDTO model)
        {
            var result = await _appUserService.Register(model);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
            }
            return View(model);
        }
        #endregion

        #region LogIn

        [AllowAnonymous]
        public IActionResult LogIn(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost, AllowAnonymous]

        public async Task<IActionResult> LogIn(LoginDTO model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserService.Login(model);

                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt..!");
            }
            return View(model);
        }

        //Sepete ürün attıktan sonra sisteme üye değilsek bizi üye olma sayfasına gönderir.
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion


        #region Edit

        public async Task<IActionResult> Edit(string userName)
        {
            if (userName == User.Identity.Name)
            {
                //Login olmuş User'ın Id'sini yakalıyorum.Oluşturmuş olduğum claimprincipleextension methodu burada vermiş oldum.
                var user = await _appUserService.GetById(User.GetUserId());

                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        [HttpPost]

        public async Task<IActionResult> Edit(UpdateProfileDTO model)
        {
            if (ModelState.IsValid)
            {
                await _appUserService.UpdateUser(model);
                TempData["Success"] = "Your has been profile updated...!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                TempData["Error"] = "Your profile hasn't been updated..!";
                return View(model);
            }
        }
        #endregion


        #region LogOut

        public async Task<IActionResult> LogOut()
        {
            await _appUserService.LogOut();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #endregion
    }
}
