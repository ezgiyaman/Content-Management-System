using CMS.Application.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Prensentation.Models.Components
{
    public class SmallCartViewComponent : ViewComponent
    {
        //Küçük sepetin dolu veya boş olma durumuna göre ;
        public IViewComponentResult Invoke()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            SmallCartVM model;

            if (cart == null || cart.Count == 0)
            {
                model = null;
            }
            else
            {
                model = new SmallCartVM
                {
                    NumberOfItems = cart.Sum(x => x.Quantity),
                    TotalAmount = cart.Sum(x => x.Quantity * x.Price)
                };
            }

            return View(model);
        }
    }
}
