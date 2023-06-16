using RESTBackEnd.API.Models;

namespace RESTBackEnd.API.Interfaces
{
	public interface IBaseRepository<T> where T : class
	{
		Task<PagedResults<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters);
		Task<IList<T>> GetAllAsync();
		Task<T?> GetAsync(int id);
		Task<T> AddAsync(T entity);
		Task DeleteAsync(int id);
		Task<T> UpdateAsync(T entity);
		Task<bool> ExistsAsync(int id);
	}
}