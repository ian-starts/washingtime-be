using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WashingTime.Exceptions.ExceptionFilters
{
    public abstract class ExceptionFilterBase<TException> : IExceptionFilter
        where TException : Exception
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception is AggregateException aggregateException)
            {
                exception = aggregateException
                                .InnerExceptions
                                .FirstOrDefault(o => o is TException) ?? exception;
            }

            if (exception is TException expectedException)
            {
                HandleException(expectedException, context);
            }
        }

        public abstract void HandleException(TException exception, ExceptionContext context);
    }
}