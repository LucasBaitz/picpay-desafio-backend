
namespace BankChallenge.Domain.Interfaces
{
	public interface IRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAll();
		Task<T?> GetById(Guid id);
		Task Add(T entity);
		Task Delete(T entity);
		void Update(T entityUpdated);
		Task<bool> SaveChanges();
	}
}
