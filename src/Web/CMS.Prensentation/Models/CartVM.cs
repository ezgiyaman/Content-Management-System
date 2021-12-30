
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Prensentation.Models
{
    public class CartVM
    {
        //Sepette birden fazla ürün olabileceğinden,sepete atıldığı zaman ürün bizim için cartItem tipinde olacağından liste olarak;

        public List<CartItem> CartItems { get; set; }

        //Bütün sepette bulunan ürünlerin toplam fiyatı için ;

        public decimal GrandTotal { get; set; }


    }

}
