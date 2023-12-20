
namespace BankChallenge.Domain.Models
{
	public class AccountTransaction
	{
		public Guid Id { get; set; }
		public string SenderId { get; set; } = string.Empty;
		public virtual User Sender { get; set; }
		public string ReceiverId { get; set; } = string.Empty;
		public virtual User Receiver { get; set; }
		public decimal Amount { get; set; }
		public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
	}
}
