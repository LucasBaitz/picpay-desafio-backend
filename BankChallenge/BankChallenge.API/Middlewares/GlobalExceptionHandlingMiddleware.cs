using BankChallenge.Domain.Exceptions.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace BankChallenge.API.Middlewares
{
	public class GlobalExceptionHandlingMiddleware : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.Clear();
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			var problemDetails = new ProblemDetails
			{
				Title = "An unexpected error occurred",
				Detail = exception.Message,
				Status = (int)HttpStatusCode.InternalServerError,
			};

			if (exception is BaseDomainException baseAppException)
			{
				context.Response.StatusCode = (int)baseAppException.StatusCode;

				problemDetails.Title = baseAppException.Title;
				problemDetails.Status = (int)baseAppException.StatusCode;
			}

			context.Response.ContentType = "application/json";

			await context.Response.WriteAsJsonAsync(problemDetails, new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			});
		}
	}
}
