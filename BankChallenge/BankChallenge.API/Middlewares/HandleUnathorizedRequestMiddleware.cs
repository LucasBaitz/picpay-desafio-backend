using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BankChallenge.API.Middlewares
{
	public class HandleUnathorizedRequestMiddleware : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			await next(context);

			if (context.Response.StatusCode == 401 || context.Response.StatusCode == 403)
			{
				var problemDetails = new ProblemDetails
				{
					Title = context.Response.StatusCode == 401 ? "Unauthorized" : "Forbidden",
					Detail = context.Response.StatusCode == 401 ? "You are not authorized to access this resource." : "Forbidden resource.",
					Status = context.Response.StatusCode,
				};

				string json = JsonSerializer.Serialize(problemDetails);

				context.Response.ContentType = "application/json";

				await context.Response.WriteAsync(json);
			}
		}
	}
}
