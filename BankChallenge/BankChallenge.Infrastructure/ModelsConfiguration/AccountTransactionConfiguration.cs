using BankChallenge.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BankChallenge.Infrastructure.ModelsConfiguration
{
	public class AccountTransactionConfiguration : IEntityTypeConfiguration<AccountTransaction>
	{
		public void Configure(EntityTypeBuilder<AccountTransaction> builder)
		{
			builder.HasKey(at => at.Id);

			builder.Property(at => at.SenderId)
				.IsRequired();

			builder.Property(at => at.ReceiverId)
				.IsRequired();

			builder.Property(at => at.Amount)
				.IsRequired();

			builder.Property(at => at.TimeStamp)
				.IsRequired();

			builder.HasOne(at => at.Sender)
				.WithMany() 
				.HasForeignKey(at => at.SenderId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(at => at.Receiver)
				.WithMany()
				.HasForeignKey(at => at.ReceiverId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}