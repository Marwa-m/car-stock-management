using CarDealerDemo.Dto;
using FluentValidation;

namespace CarDealerDemo.Validators;

public class LoginDealerValidator : AbstractValidator<LoginDealerDto>
{
    public LoginDealerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Please enter name or email")
            .MaximumLength(30).WithMessage("Please enter a valid name");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Please enter password")
            .MaximumLength(30).WithMessage("password is too long");

    }
}
