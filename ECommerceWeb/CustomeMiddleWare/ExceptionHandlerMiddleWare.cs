using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.CustomeMiddleWare
{
    public class ExceptionHandlerMiddleWare                                         //:IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleWare> _logger;

        public ExceptionHandlerMiddleWare(RequestDelegate next,ILogger<ExceptionHandlerMiddleWare> logger)
        {
           _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    // Log 404 errors
                    var response = new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "Error While Processing Http request  -EndPoint Not Found",
                        Detail = $"The requested resource(EndPoint) '{context.Request.Path}' was not found on the server.",
                        Instance = context.Request.Path
                    };
                    await context.Response.WriteAsJsonAsync(response);

                }
            }
            catch (Exception ex)
            {
                // Logging 
                _logger.LogError("Something Went Wrong");
                context.Response.StatusCode= StatusCodes.Status500InternalServerError;
                //return custom error response
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Enternal Server Error",
                    Detail = ex.Message,
                    Instance = context.Request.Path
                };
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
