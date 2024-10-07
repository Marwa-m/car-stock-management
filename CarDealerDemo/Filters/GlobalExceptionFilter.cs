using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerDemo.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        // Handle UnauthorizedAccessException
        if (exception is UnauthorizedAccessException)
        {
            context.Result = new UnauthorizedObjectResult(new { message = exception.Message });
            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
        // Handle other exceptions (e.g., InvalidOperationException, ArgumentException, etc.)
        else
        {
            var statusCode = StatusCodes.Status500InternalServerError; // Default to 500
            string message = "An unexpected error occurred.";

            // Optionally handle specific types of exceptions differently
            if (exception is InvalidOperationException)
            {
                statusCode = StatusCodes.Status400BadRequest;
                message = "Invalid operation: " + exception.Message;
            }
            else if (exception is ArgumentException)
            {
                statusCode = StatusCodes.Status400BadRequest;
                message = "Invalid argument: " + exception.Message;
            }

            context.Result = new ObjectResult(new
            {
                error = message,
             //   details = exception.StackTrace // For debugging
            })
            {
                StatusCode = statusCode
            };
        }

        context.ExceptionHandled = true; 
    }
}



