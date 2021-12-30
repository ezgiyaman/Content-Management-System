using CMS.Application.Models.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Validation.FluentValidation
{
    public class UpdatePageValidation : AbstractValidator<UpdatePageDTO>
    {
        public UpdatePageValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Enter a title").MaximumLength(30).WithMessage("Maximum 30 character");

            RuleFor(x => x.Content).NotEmpty().WithMessage("Enter a content").MaximumLength(50).WithMessage("Maximum 50 character");

            RuleFor(x => x.Slug).NotEmpty().WithMessage("Enter a slug");

        }
    }
}
