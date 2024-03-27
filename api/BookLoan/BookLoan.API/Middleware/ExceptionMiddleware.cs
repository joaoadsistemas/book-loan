using System.Net;
using System.Text.Json;
using BookLoan.API.Errors;

namespace BookLoan.API.Middleware
{
    public class ExceptionMiddleware
    {

        public readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;


        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }



        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;


                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode.ToString(), e.Message, e.StackTrace.ToString())
                    : new ApiException(context.Response.StatusCode.ToString(), e.Message, "Internal server error");


                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }

    }
}
