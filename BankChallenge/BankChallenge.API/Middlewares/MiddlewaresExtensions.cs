namespace BankChallenge.API.Middlewares
{
	public static class MiddlewaresExtensions
	{
		public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
		{
			return app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
		}

		public static IApplicationBuilder UseHandleUnathorizedRequest(this IApplicationBuilder app)
		{
			return app.UseMiddleware<HandleUnathorizedRequestMiddleware>();
		}
	}
}
