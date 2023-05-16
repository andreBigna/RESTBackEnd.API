using Microsoft.EntityFrameworkCore;
using RESTBackEnd.API.Data;
using RESTBackEnd.API.Interfaces;

namespace RESTBackEnd.API.Repository
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class

	{
		private readonly RestBackEndDbContext _context;

		public BaseRepository(RestBackEndDbContext context)
		{
			_context = context;
		}

		public async Task<T> AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await GetAsync(id);
			_context.Set<T>().Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> ExistsAsync(int id)
		{
			return await GetAsync(id) != null;
		}

		public async Task<T?> GetAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<IList<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<T> UpdateAsync(T entity)
		{
			_context.Set<T>().Update(entity);
			await _context.SaveChangesAsync();
			return entity;
		}
	}
}