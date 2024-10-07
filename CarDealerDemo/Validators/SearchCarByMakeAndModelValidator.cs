using CarDealerDemo.Dto;
using FluentValidation;

namespace CarDealerDemo.Validators
{
    public class SearchCarByMakeAndModelValidator : AbstractValidator<SearchCarByMakeAdModelDto>
    {
        public SearchCarByMakeAndModelValidator()
        {
            RuleFor(x => x.Make)
            .MaximumLength(30)
             .WithMessage("{PropertyName} length can't be more than 30 characters.");

            RuleFor(x => x.Model)
                    .MaximumLength(30)
                    .WithMessage("{PropertyName} length can't be more than 30 characters.");
        }
    }
}
