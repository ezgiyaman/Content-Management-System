using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Prensentation.Models
{
    //Küçük sepet oluşturma işlemi 
    public class SmallCartVM
    {
        public int NumberOfItems { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
