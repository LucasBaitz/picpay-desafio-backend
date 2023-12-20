using System.ComponentModel.DataAnnotations;

namespace BankChallenge.Application.DTOs.UserDTOs
{
	public record UserLoginDTO
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; } = string.Empty;
	}
}
