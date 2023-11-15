using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebUI.Filters;
//Class to handle possible unhandled exceptions
public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IHostEnvironment _env;

    public ApiExceptionFilterAttribute(IHostEnvironment env)
    {
        _env = env;
    }
    public override void OnException(ExceptionContext context)
    {
        //var exceptionType = context.Exception.GetType();

        //TODO: Adapt to handle all possible exceptios and its possible status codes
        var details = new ProblemDetails();
        if (_env.IsDevelopment())
        {
            details.Title = context.Exception.Message;
            details.Detail = context.Exception.StackTrace;
        }
        else
        {
            details.Title = "A server Error occurred";
            details.Detail = context.Exception.Message;
        }

        //TODO: Adapt to handle all status codes
        context.Result = new ObjectResult(details)
        {
            StatusCode = 500
        };
    }
}
