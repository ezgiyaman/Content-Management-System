using CMS.Application.Extensions;
using CMS.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Application.Models.DTOs
{
    public class UpdateProfileDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string ImagePath { get => "/images/users/default.jpg"; }

        //form'dan datayı taşıyacak.IFormFile o yüzden var.

        [NotMapped] //veritabanıında bir sütun olarak oluşturmasını var olmasını istemiyorsak.
        [FileExtension] //file extensation sınıfındaki oluşturduğum kurallara uyması için.

        public IFormFile Image { get; set; }

        //encapsulation 
        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
    }
}
