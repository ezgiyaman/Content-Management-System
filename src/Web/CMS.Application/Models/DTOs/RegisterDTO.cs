using CMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Models.DTOs
{
    public class RegisterDTO
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; } //şifre güvenliği

        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;


    }
}
