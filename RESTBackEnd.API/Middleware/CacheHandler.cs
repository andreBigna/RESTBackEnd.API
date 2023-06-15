namespace RESTBackEnd.API.Middleware
{
	public class CacheHandler
	{
		private readonly RequestDelegate _next;

		public CacheHandler(RequestDelegate next)
		{
			this._next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			httpContext.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
			{
				MaxAge = TimeSpan.FromSeconds(10),
				Public = true
			};
			//THIS ALLOWS DIFFERENT FORMATS FOR CACHE RESULTS
			httpContext.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] = new string[] { "Accept-Encoding" };


			await _next(httpContext);
		}
	}
}
