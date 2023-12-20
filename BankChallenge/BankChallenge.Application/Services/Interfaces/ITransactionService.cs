using BankChallenge.Application.DTOs.TransactionDTOs;
using BankChallenge.Domain.Models;

namespace BankChallenge.Application.Services.Interfaces
{
	public interface ITransactionService
	{
		Task<bool> MakeTransaction(MakeTransactionDTO transactionInfo);
		bool ValidateTransaction(User senderUser);
		Task<bool> IsTransactionAuthorized();
		Task<IEnumerable<AccountTransaction>> GetTransactions();
		Task<IEnumerable<ReadMadeTransactionDTO>> ReadTransactionsByUserId(string userId);
	}
}