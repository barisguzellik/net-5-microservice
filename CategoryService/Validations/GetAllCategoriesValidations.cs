using System;
using CategoryService.DTOs.Requests;
using FluentValidation;

namespace CategoryService.Validations
{
    public class GetAllCategoriesValidations:AbstractValidator<GetAllCategoriesRequest>
    {
        public GetAllCategoriesValidations()
        {
            RuleFor(x => x.page).NotNull().NotEmpty();
            RuleFor(x => x.offset).NotNull().NotEmpty();
        }
    }
}
