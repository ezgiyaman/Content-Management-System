using CMS.Application.Models.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Validation.FluentValidation
{
    public class LoginValidation : AbstractValidator<LoginDTO>
    {
        public LoginValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Enter a user name");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Enter a password");
        }
    }
}
