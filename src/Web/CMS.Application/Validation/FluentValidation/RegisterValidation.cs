using CMS.Application.Models.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Validation.FluentValidation
{
    public class RegisterValidation : AbstractValidator<RegisterDTO>
    {
        public RegisterValidation()
        {

            RuleFor(x => x.FullName).NotEmpty().WithMessage("Name can't be empty").MinimumLength(3).MaximumLength(50).WithMessage("Minumum 3, maximum 50 character");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Name can't be empty").MinimumLength(3).MaximumLength(20).WithMessage("Minumum 3, maximum 20 character");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Enter a email address").EmailAddress().WithMessage("Enter a valid email address");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Enter a password");

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Password don't match");

        }
    }
}
