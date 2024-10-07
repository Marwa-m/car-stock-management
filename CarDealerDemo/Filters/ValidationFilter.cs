using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using CarDealerDemo.Attributes;

namespace CarDealerDemo.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        var hasValidateModelAttribute = actionDescriptor?.MethodInfo
            .GetCustomAttributes(typeof(ValidateModelAttribute), false)
            .Any()??false;

        if (!hasValidateModelAttribute)
        {
            return;
        }
        var param = context.ActionArguments.Values.FirstOrDefault();
        if (param == null)
        {
            context.Result = new BadRequestObjectResult("Invalid request");
            return;
        }

        var validator = context.HttpContext.RequestServices
                        .GetService(typeof(IValidator<>).MakeGenericType(param.GetType())) as IValidator;

        if (validator != null)
        {
            var validationContext = new ValidationContext<object>(param);
            var validationResult = validator.Validate(validationContext);
            if (!validationResult.IsValid)
            {
                context.Result = new BadRequestObjectResult(validationResult.ToDictionary());
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
