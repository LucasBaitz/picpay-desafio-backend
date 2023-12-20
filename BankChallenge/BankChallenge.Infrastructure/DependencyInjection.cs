using BankChallenge.Domain.Interfaces;
using BankChallenge.Domain.Models;
using BankChallenge.Infrastructure.Data;
using BankChallenge.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BankChallenge.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
		{
			

			var connectionString = configuration.GetConnectionString("SqliteConnectionString");
			services.AddDbContext<AppDbContext>
				(opts =>
				{
					opts.UseLazyLoadingProxies().UseSqlite(connectionString, 
						b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
				});

			services.AddScoped<IAccountTransactionRepository, AccountTransactionRepository>();

			return services;
		}
	}
}
