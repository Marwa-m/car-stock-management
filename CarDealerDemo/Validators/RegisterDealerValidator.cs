using CarDealerDemo.Dto;
using FluentValidation;

namespace CarDealerDemo.Validators;

public class RegisterDealerValidator : AbstractValidator<RegisterDealerDto>
{
    public RegisterDealerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} can't be empty.")
            .MaximumLength(30).WithMessage("{PropertyName} length can't be more than 30 characters.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("{PropertyName} Name can't be empty.")
            .EmailAddress();
        RuleFor(request => request.Password)
    .NotEmpty()
    .MinimumLength(6);
        //.Matches("[A-Z]+").WithMessage("'{PropertyName}' must contain one or more capital letters.")
        //.Matches("[a-z]+").WithMessage("'{PropertyName}' must contain one or more lowercase letters.")
        //.Matches(@"(\d)+").WithMessage("'{PropertyName}' must contain one or more digits.")
        //.Matches(@"[""!@$%^&*(){}:;<>,.?/+\-_=|'[\]~\\]").WithMessage("'{PropertyName}' must contain one or more special characters.");

    }
}
