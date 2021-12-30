using CMS.Application.Extensions;
using CMS.Application.Models.VMs;
using CMS.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Application.Models.DTOs
{
    public class CreateProductDTO
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

    
        public string ImagePath { get => "/images/products/default.jpg"; }

        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;

        [NotMapped]
        [FileExtension] 
        public IFormFile Image { get; set; } //yolu bunla taşıcam create kısmında

        public int CategoryId { get; set; }

        public List<GetCategoryVM> Categories { get; set; } 
     
    }
}
