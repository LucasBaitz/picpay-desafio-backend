using BankChallenge.Application.DTOs.UserDTOs;
using BankChallenge.Domain.Models;

namespace BankChallenge.Application.Services.Interfaces
{
	public interface IAuthService
	{
		Task Register(UserCreateAccountDTO user);
		Task<string> Login(UserLoginDTO user);
		Task DeleteUser(string userId);
		Task UpdateUser(string userId, UserUpdateInformationDTO user);
		Task<User?> GetUserByDocument(string document);
		Task<User?> GetUserById(Guid id);
		Task<IEnumerable<User>> GetAll();
		void CheckIfInformationIsUnique(string document, string email);
	}
}
