using System.ComponentModel.DataAnnotations;

namespace BankChallenge.Application.DTOs.TransactionDTOs
{
	public record MakeTransactionDTO()
	{
		public string SenderId { get; set; } = string.Empty;
		[Required]
		public string ReceiverDocument { get; set; } = string.Empty;
		[Required]
		public decimal Amount { get; set; }
	}
}
