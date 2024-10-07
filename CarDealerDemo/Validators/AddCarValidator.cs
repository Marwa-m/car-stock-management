using CarDealerDemo.Dto;
using FluentValidation;

namespace CarDealerDemo.Validators;

public class AddCarValidator : AbstractValidator<AddCarDto>
{
    public AddCarValidator()
    {
        RuleFor(x => x.Make)
        .NotEmpty()
        .WithMessage("{PropertyName} can't be empty")
        .MaximumLength(30)
        .WithMessage("{PropertyName} length can't be more than {PropertyValue} characters.");

        RuleFor(x => x.Model)
                .NotEmpty()
                .WithMessage("{PropertyName} can't be empty")
                .MaximumLength(30)
                .WithMessage("{PropertyName} length can't be more than 30 characters.");
        RuleFor(x => x.Year)
            .LessThanOrEqualTo(DateTime.Now.Year + 1)
            .WithMessage("The year of the car is not correct")
            .GreaterThanOrEqualTo(1886)
            .WithMessage("There is no car invented before 1886");
        RuleFor(x => x.StockLevel)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} value can't be less than 0.")
            .LessThan(int.MaxValue).WithMessage("{PropertyName} value can't be more than 2147483647.");
    }
}
