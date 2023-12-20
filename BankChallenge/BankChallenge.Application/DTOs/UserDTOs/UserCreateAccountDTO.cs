using BankChallenge.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BankChallenge.Application.DTOs.UserDTOs
{
	public record UserCreateAccountDTO
	{
		[Required(ErrorMessage = "First name is required.")]
		[StringLength(50, ErrorMessage = "First name must be at most 50 characters.")]
		public string FirstName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Last name is required.")]
		[StringLength(50, ErrorMessage = "Last name must be at most 50 characters.")]
		public string LastName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Document is required.")]
		[StringLength(20, ErrorMessage = "Document must be at most 20 characters.")]
		public string Document { get; set; } = string.Empty;

		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email address.")]
		[StringLength(100, ErrorMessage = "Email must be at most 100 characters.")]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Phone number is required.")]
		[Phone(ErrorMessage = "Invalid phone number.")]
		[StringLength(20, ErrorMessage = "Phone number must be at most 20 characters.")]
		public string PhoneNumber { get; set; } = string.Empty;

		[Required(ErrorMessage = "Password is required.")]
		[DataType(DataType.Password)]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
		public string Password { get; set; } = string.Empty;

		[Required(ErrorMessage = "Confirm password is required.")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password and confirm password do not match.")]
		public string ConfirmPassword { get; set; } = string.Empty;

		[Required(ErrorMessage = "Account type is required.")]
		public AccountType AccountType { get; set; }

		[Required(ErrorMessage = "Initial balance is required.")]
		[Range(0, double.MaxValue, ErrorMessage = "Initial balance must be a non-negative value.")]
		public decimal InitialBalance { get; set; }
	}

}
