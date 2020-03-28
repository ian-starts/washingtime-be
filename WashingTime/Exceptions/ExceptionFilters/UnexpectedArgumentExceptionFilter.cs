using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WashingTime.Exceptions.ExceptionFilters
{
    public class UnexpectedArgumentExceptionFilter : ExceptionFilterBase<UnexpectedArgumentException>
    {
        public override void HandleException(UnexpectedArgumentException exception, ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(new ProblemDetails
            {
                Title = "An argument problem occured",
                Status = 400,
                Type = "UnexpectedArgumentException",
                Detail = context.Exception.Message,
            });
        }
    }
}