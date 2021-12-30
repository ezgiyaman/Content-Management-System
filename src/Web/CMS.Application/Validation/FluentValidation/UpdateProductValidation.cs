using CMS.Application.Models.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Validation.FluentValidation
{
    public class UpdateProductValidation : AbstractValidator<UpdateProductDTO>
    {
        public UpdateProductValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Enter a name").MinimumLength(5).MaximumLength(15).WithMessage("Minumum 5, maximum 15 character");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Enter a description").MaximumLength(5).MaximumLength(100).WithMessage("Minumum 5, maximum 100 character");

            RuleFor(x => x.ImagePath).NotEmpty().WithMessage("Enter a ImagePath");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Enter a price");
        }

    }
}
