using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankChallenge.Application.DTOs.UserDTOs
{
	public class UserUpdateInformationDTO
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
	}
}
