using BankChallenge.Domain.Models;

namespace BankChallenge.Domain.Interfaces
{
	public interface IAccountTransactionRepository : IRepository<AccountTransaction>
	{
		Task<IEnumerable<AccountTransaction>> GetAllUserTransactionsAsync(string userId);
	}
}
