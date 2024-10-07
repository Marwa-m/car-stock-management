using CarDealerDemo.Dto;
using FluentValidation;

namespace CarDealerDemo.Validators;

public class UpdateStockLevelValidator : AbstractValidator<UpdateStockLevelDto>
{
    public UpdateStockLevelValidator()
    {
        RuleFor(x => x.ID)
            .NotEmpty().WithMessage("{PropertyName} value should not be empty")
             .LessThan(int.MaxValue).WithMessage("{PropertyName} value can't be more than 2147483647.");

        RuleFor(x => x.StockLevel)
            .NotEmpty().WithMessage("{PropertyName} value should not be empty")
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} value can't be less than 0.")
            .LessThan(int.MaxValue).WithMessage("{PropertyName} value can't be more than 2147483647.");

    }
}
