using CMS.Domain.Entities.Interface;
using CMS.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Domain.Entities.Concrete
{
    public class AppUser : IdentityUser , IBaseEntity
    {
        public string FullName { get; set; }
        public string Imagepath { get; set; } = "/images/user/default.jpg"; //eğer herhangi bir resim yoksa default olarak belirlenen resim
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

       
    }
}
