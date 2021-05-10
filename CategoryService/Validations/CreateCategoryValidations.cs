using System;
using CategoryService.DTOs.Requests;
using FluentValidation;

namespace CategoryService.Validations
{
    public class CreateCategoryValidations : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryValidations()
        {
            RuleFor(x => x.name).NotEmpty().NotNull();
            RuleFor(x => x.slug).NotEmpty().NotNull();
            RuleFor(x => x.is_active).NotNull().LessThan(2).GreaterThan(-1);
            RuleFor(x => x.sort).NotNull().GreaterThan(0);
        }
    }
}
