using System.Net;
using FluentValidation;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.Helpers;

namespace Standard.ToList.Api.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
	{
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
		{
            _logger = logger;
		}

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException e)
            {
                _logger.LogError(e, e.Message);
                var errorMessages = e.Errors.Select(it => it.ErrorMessage).Distinct().ToArray();
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, errorMessages);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, HttpStatusCode statusCode, string[] messages = null)
        {
            var result = new Result<string>("Request error.", ResultStatus.Error);
            result.Messages.AddRange(messages);
            await httpContext.WriteResult(result, statusCode);
        }
    }
}

