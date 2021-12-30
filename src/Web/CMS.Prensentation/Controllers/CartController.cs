using CMS.Application.Extensions;
using CMS.Domain.Entities.Concrete;
using CMS.Infrastructure.Context;
using CMS.Prensentation.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Prensentation.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _db;

        public CartController(AppDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            //Eğer sepette ürün yok ise boş gelecek , eğer sepette ürün varsa List olarak ürünler gelsin.
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartVM model = new CartVM
            {
                CartItems = cart, //sepet boşsa
                GrandTotal = cart.Sum(x => x.Price * x.Quantity) //Sepet doluysa ürünün sayısına göre toplam ücret
            };

            return View(model);
        }

        //Sepete eklemek istenilen ürünü Id'sinden yakaladım.

        public async Task<IActionResult> Add(int id)
        {
            Product product = await _db.Products.FindAsync(id);


            //Bu kısımda sepet eğer dolu ise , önceden sepete eklenmiş olan (json) tipinde ürünleri uygulama tarafına çağırdım, uygulama Json tipini anlayamacağı için ; CartItem tipinde ürünlerimi Deserialize ettim.
            //Eğer sepette ürün yok ise boş olarak dönecektir.
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            //Sepete eklemek istenilen ürün için;
            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            //Eğer sepette bu üründen yoksa ürün sepete eklenecek.
            if (cartItem == null)
            {
                cart.Add(new CartItem(product));
            }
            else //Eğer sepette bu üründen varsa 1 arttırılacak.
            {
                cartItem.Quantity += 1;
            }

            //Ürünümü sepete atıyorum ;
            //Gelen product'ı Json'a döndürdüm.(Serialize ettim.)
            HttpContext.Session.SetJson("Cart", cart);

            return ViewComponent("SmallCart");
        }


        //Sepette olan ürünün sayısını (-) basarak azaltmak ya da (-) basarak Remove yapma işlemi

        public IActionResult Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1) //Aynı üründen birden fazla varsa 
            {
                --cartItem.Quantity; // 1 azalt
            }
            else
            {
                cart.RemoveAll(x => x.ProductId == id); //ürünü tamamen sepetten silme işlemi
            }

            //Eğer ürünün sayısı (-) basarak azaltılıp hiç kalmazsa 

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }


        //Sepette bulunan bir ürünü tamamen kaldırma işlemi 

        public IActionResult Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(x => x.ProductId == id);

            //Sepette bulunan farklı ürünler silinirse,sepette hiç ürün kalmazsa
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else //Sepette tek ürün kalırsa 
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        //Sepette bulunan ürünlerin hepsini silme işlemi

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index", "Home");
        }

    }
}
