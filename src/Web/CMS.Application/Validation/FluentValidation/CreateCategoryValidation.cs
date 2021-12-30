using CMS.Application.Models.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Validation.FluentValidation
{
    public class CreateCategoryValidation : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Enter a name").MinimumLength(3).MaximumLength(15).WithMessage("Minumum 3, maximum 40 character");

            RuleFor(x => x.Slug).NotEmpty().WithMessage("Enter a slug");
        }
    }
}
