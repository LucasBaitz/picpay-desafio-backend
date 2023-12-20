using BankChallenge.Application.Services.Interfaces;
using BankChallenge.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BankChallenge.Domain.Models;
using Microsoft.AspNetCore.Identity;
using BankChallenge.Infrastructure.Data;

namespace BankChallenge.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddIdentity<User, IdentityRole>()
			.AddEntityFrameworkStores<AppDbContext>()
			.AddDefaultTokenProviders();

			services.AddAuthentication(options => options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TH3@B357!53CUR17Y--K3Y$%3VERM4DEF0RSUR3")),
					ValidateLifetime = true,
					ValidateAudience = false,
					ValidateIssuer = false,
					ClockSkew = TimeSpan.Zero
				};
			});

			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
			services.AddScoped<ITransactionService, TransactionService>();
			services.AddScoped<IAlertNotificationService, AlertNotificationService>();

			return services;
		}
	}
}
