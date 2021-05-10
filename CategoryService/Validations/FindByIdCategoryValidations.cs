using System;
using CategoryService.DTOs.Requests;
using FluentValidation;

namespace CategoryService.Validations
{
    public class FindByIdCategoryValidations : AbstractValidator<FindByIdCategoryRequest>
    {
        public FindByIdCategoryValidations()
        {
            RuleFor(x => x.category_id).NotNull().NotEmpty();
        }
    }
}
