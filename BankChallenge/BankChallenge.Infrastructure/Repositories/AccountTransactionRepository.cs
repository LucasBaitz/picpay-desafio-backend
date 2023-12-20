using BankChallenge.Domain.Interfaces;
using BankChallenge.Domain.Models;
using BankChallenge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankChallenge.Infrastructure.Repositories
{
	public class AccountTransactionRepository : IAccountTransactionRepository
	{
		private readonly AppDbContext _context;
		public AccountTransactionRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task Add(AccountTransaction transaction)
		{
			await _context.Transactions.AddAsync(transaction);
			await SaveChanges();
		}

		public async Task Delete(AccountTransaction transaction)
		{
			_context.Remove(transaction);
			await SaveChanges();
		}

		public async Task<IEnumerable<AccountTransaction>> GetAll()
		{
			return await _context.Transactions.ToListAsync();
		}

		public async Task<AccountTransaction?> GetById(Guid id)
		{
			return await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
		}

		public async Task<IEnumerable<AccountTransaction>> GetAllUserTransactionsAsync(string userId)
		{
			return await _context.Transactions.Where(t => t.SenderId == userId || t.ReceiverId == userId).ToListAsync();
		}

		public async Task<bool> SaveChanges()
		{
			return await _context.SaveChangesAsync() > 0;
		}

		public void Update(AccountTransaction entityUpdated)
		{
			
		}
	}
}
