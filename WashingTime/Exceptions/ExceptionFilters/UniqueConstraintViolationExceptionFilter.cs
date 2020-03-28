using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WashingTime.Exceptions.ExceptionFilters
{
    public class UniqueConstraintViolationExceptionFilter : ExceptionFilterBase<UniqueConstraintViolationException>
    {
        public override void HandleException(UniqueConstraintViolationException exception, ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(new ProblemDetails
            {
                Title = "A unique constraint problem occured",
                Status = 400,
                Type = "UniqueConstraintViolationException",
                Detail = context.Exception.Message,
            });
        }
    }
}