

namespace BankChallenge.Application.DTOs.TransactionDTOs
{
	public record ReadMadeTransactionDTO()
	{
		public string Sender { get; set; } = string.Empty;
		public string Receiver { get; set; } = string.Empty;
		public decimal Amount { get; set; }
		public DateTime TimeStamp { get; private set; } = DateTime.UtcNow;
	}
}
