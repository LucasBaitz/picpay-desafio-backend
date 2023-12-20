using BankChallenge.Domain.Models;

namespace BankChallenge.Application.Services.Interfaces
{
	public interface ITokenGeneratorService
	{
		string GenerateToken(User user);
	}
}
