using System.Net;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Shared.ExceptionsFilters;

public class ExceptionFilter: IExceptionFilter
{
    private static readonly Dictionary<Type, HttpStatusCode> ExceptionMapping = new()
    {
        { typeof(AlreadyExistsException), HttpStatusCode.Conflict},
        { typeof(NotFoundException), HttpStatusCode.NotFound}
    };
    
    public void OnException(ExceptionContext context)
    {
        var exceptionType = context.Exception.GetType();

        if (ExceptionMapping.TryGetValue(exceptionType, out var statusCode))
        {
            context.Result = new ObjectResult(new { message = context.Exception.Message })
            {
                StatusCode = (int)statusCode
            };
        }
        else
        {
            context.Result = new ObjectResult(new { message = "Произошла неизвестная ошибка." })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }

        context.ExceptionHandled = true;
    }
}