using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.ExceptionFilter;

public class ExceptionFilter : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandler;

    public ExceptionFilter()
    {
        _exceptionHandler = new Dictionary<Type, Action<ExceptionContext>>
        {
            {typeof(NotFoundException), HandleNotFoundException},
            {typeof(CreateUserException), HandleCreateUserException},
        };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);
        
        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();
        if (_exceptionHandler.ContainsKey(type))
        {
            _exceptionHandler[type].Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }

        if (context.Exception is BaseException)
        {
            HandleNetCoreTemplateException(context);
            return;
        }
        HandleUnknownException(context);
    }

    private static void HandleUnknownException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Coś poszło nie tak"
        };

        details.Detail = $"{context.Exception.Message} {context.Exception.Source} {context.Exception.StackTrace}";

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }

    private void HandleNetCoreTemplateException(ExceptionContext context)
    {
        var exception = context.Exception as BaseException;

        var detail = new ProblemDetails()
        {
            Title = exception?.Message
        };

        context.Result = new BadRequestObjectResult(detail);

        context.ExceptionHandled = true;
    }

    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState);

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;    
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        context.Result = new NotFoundResult();

        context.ExceptionHandled = true;
    }
    

    private void HandleCreateUserException(ExceptionContext context)
    {
        var exception = context.Exception as CreateUserException;


        var details = new ValidationProblemDetails(exception?.Errors)
        {
            Title = exception?.Message
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }
}