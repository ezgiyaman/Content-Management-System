using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Models.VMs
{
    public class GetProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public decimal Price { get; set; }

        //Sadece yolunu belli edeceğim için IFromFile kullanmadım.
        public string ImagePath { get; set; }


    }
}
