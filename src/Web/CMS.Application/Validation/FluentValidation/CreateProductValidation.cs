using CMS.Application.Models.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Validation.FluentValidation
{
    public class CreateProductValidation : AbstractValidator<CreateProductDTO>
    {
        public CreateProductValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Enter a name").MinimumLength(3).MaximumLength(40).WithMessage("Minumum 3, maximum 40 character");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Enter a Description").MaximumLength(200).MinimumLength(3).WithMessage("character min : 3 max : 200");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Enter a price");

        }
    }
}
