using BankChallenge.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankChallenge.Infrastructure.ModelsConfiguration
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasBaseType<IdentityUser>();

			builder.Property(u => u.FirstName)
				.HasMaxLength(70);

			builder.Property(u => u.LastName)
				.HasMaxLength(100);

			builder.Property(u => u.Document)
				.HasMaxLength(20);

			builder.Property(u => u.Balance)
				.IsRequired();

			builder.Property(u => u.AccountType)
				.IsRequired();

			builder.Ignore(u => u.FullName);

			builder.HasIndex(u => u.Email).IsUnique();
		}
	}
}
