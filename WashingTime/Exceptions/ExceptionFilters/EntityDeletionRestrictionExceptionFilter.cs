using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WashingTime.Exceptions.ExceptionFilters
{
    public class EntityDeletionRestrictionExceptionFilter : ExceptionFilterBase<EntityDeletionRestrictionException>
    {
        public override void HandleException(EntityDeletionRestrictionException exception, ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(new ProblemDetails
            {
                Title = "Delete request can't be fulfilled because of a restriction.",
                Status = 400,
                Type = "EntityDeletionRestrictionException",
                Detail = context.Exception.Message,
            });
        }
    }
}