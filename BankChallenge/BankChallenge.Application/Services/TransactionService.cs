using AutoMapper;
using BankChallenge.Application.DTOs.TransactionDTOs;
using BankChallenge.Application.Services.Interfaces;
using BankChallenge.Domain.Enums;
using BankChallenge.Domain.Exceptions;
using BankChallenge.Domain.Interfaces;
using BankChallenge.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankChallenge.Application.Services
{
	public class TransactionService : ITransactionService
	{
		private readonly UserManager<User> _userManager;
		private readonly IAccountTransactionRepository _transactionRepository;
		private readonly IMapper _mapper;
		private readonly IAlertNotificationService _alertNotificationService;
		private const string TransactionValidationApiUrl = "https://run.mocky.io/v3/5794d450-d2e2-4412-8131-73d0293ac1cc";
		public TransactionService(UserManager<User> userManager, IAccountTransactionRepository transactionRepository, IMapper mapper, IAlertNotificationService alertNotificationService)
		{
			_userManager = userManager;
			_transactionRepository = transactionRepository;
			_mapper = mapper;
			_alertNotificationService = alertNotificationService;
		}

		public async Task<bool> MakeTransaction(MakeTransactionDTO transactionInfo)
		{
			var sender = await _userManager.Users.FirstOrDefaultAsync(us => us.Id == transactionInfo.SenderId);

			if (!ValidateTransaction(sender))
				throw new UnathorizedTransactionException("Your account doest not have permission to make transactions", "Unathorized Transaction");

			if (!UserHasNecessaryBalance(sender, transactionInfo.Amount))
				throw new UnathorizedTransactionException("You account doesnt have the necessary amount to make this transaction", "Unathorized Transaction");

			var receiver = await _userManager.Users.FirstOrDefaultAsync(ur => ur.Document == transactionInfo.ReceiverDocument);

			if (receiver is null)
				throw new ResourceNotFoundException("Receiver was not found.", "User not found");

			bool isTransactionAuthorized = await IsTransactionAuthorized();

			if (!isTransactionAuthorized)
				throw new UnathorizedTransactionException("Something went wrong while authorizing this transaction.", "Unathorized Transaction");

			receiver.Balance += transactionInfo.Amount;
			sender.Balance -= transactionInfo.Amount;

			var newTransaction = _mapper.Map<AccountTransaction>(transactionInfo);
			newTransaction.ReceiverId = receiver.Id;

			await _transactionRepository.Add(newTransaction);

			await _alertNotificationService.SendUserNotification(sender, "Transaction made successully");
			await _alertNotificationService.SendUserNotification(receiver, $"Received transaction of ${transactionInfo.Amount}");

			return isTransactionAuthorized;
		}

		public bool ValidateTransaction(User senderUser)
		{
			if (senderUser.AccountType == AccountType.Bussiness) return false;
			return true;
		}

		public async Task<bool> IsTransactionAuthorized()
		{
			using (HttpClient client = new HttpClient())
			{

				HttpResponseMessage response = await client.GetAsync(TransactionValidationApiUrl);

				if (response.IsSuccessStatusCode)
				{
					string responseBody = await response.Content.ReadAsStringAsync();
					if (responseBody.Contains("Autorizado", StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}

				return false;
			}
		}


		private bool UserHasNecessaryBalance(User sender, decimal amount)
		{
			return sender.Balance >= amount;
		}

		public async Task<IEnumerable<ReadMadeTransactionDTO>> ReadTransactionsByUserId(string userId)
		{
			var transactions = _mapper.Map<List<ReadMadeTransactionDTO>>(await _transactionRepository.GetAllUserTransactionsAsync(userId));
			return transactions;
		}

		public async Task<IEnumerable<AccountTransaction>> GetTransactions()
		{
			return await _transactionRepository.GetAll();
		}
	}
}
