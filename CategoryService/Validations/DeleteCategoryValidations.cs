using System;
using CategoryService.DTOs.Requests;
using FluentValidation;

namespace CategoryService.Validations
{
    public class DeleteCategoryValidations : AbstractValidator<DeleteCategoryRequest>
    {
        public DeleteCategoryValidations()
        {
            RuleFor(x => x.category_id).NotNull().NotEmpty();
        }
    }
}
