using BankChallenge.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace BankChallenge.Domain.Models
{
	public class User : IdentityUser
	{
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
		public string FullName => JoinNames();
		public string Document { get; set; } = string.Empty;
		public decimal Balance { get; set; }
		public AccountType AccountType { get; set; }
		public User() : base() { }

		public string JoinNames()
		{
			return $"{FirstName} {LastName}";
		}
	}
}
