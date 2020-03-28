using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WashingTime.Exceptions.ExceptionFilters
{
    public class EntityNotFoundExceptionFilter : ExceptionFilterBase<EntityNotFoundException>
    {
        public override void HandleException(EntityNotFoundException exception, ExceptionContext context)
        {
            context.Result = new NotFoundResult();
        }
    }
}