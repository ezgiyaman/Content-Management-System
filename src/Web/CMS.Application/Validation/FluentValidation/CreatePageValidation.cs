using CMS.Application.Models.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Validation.FluentValidation
{
    public class CreatePageValidation : AbstractValidator<CreatePageDTO>
    {
        public CreatePageValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Enter a title").MinimumLength(3).MaximumLength(30).WithMessage("Minumum 3, maximum 30 character");

            RuleFor(x => x.Content).NotEmpty().WithMessage("Enter a content").MinimumLength(3).MaximumLength(50).WithMessage("Minumum 3, maximum 50 character");

            RuleFor(x => x.Slug).NotEmpty().WithMessage("Enter a slug");
        }
    }
}
