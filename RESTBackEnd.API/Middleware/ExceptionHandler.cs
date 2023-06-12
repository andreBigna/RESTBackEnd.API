using Newtonsoft.Json;

namespace RESTBackEnd.API.Middleware
{
	public class ExceptionHandler
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandler> _logger;

		public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception e)
			{
				await HandleException(httpContext, e);
			}
		}

		private async Task HandleException(HttpContext httpContext, Exception exception)
		{
			httpContext.Response.ContentType = "application/json";
			httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

			_logger.LogError(exception, $"Error in {httpContext.Request.Path}, path {httpContext.Response.StatusCode}");

			await httpContext.Response.WriteAsync(exception.Message);
		}
	}
}