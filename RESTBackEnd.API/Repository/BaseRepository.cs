using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RESTBackEnd.API.Data;
using RESTBackEnd.API.Interfaces;
using RESTBackEnd.API.Models;

namespace RESTBackEnd.API.Repository
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class

	{
		private readonly RestBackEndDbContext _context;
		private readonly IMapper _mapper;

		public BaseRepository(RestBackEndDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
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

		public async Task<PagedResults<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters)
		{
			var totalRecord = await _context.Set<T>().CountAsync();
			var items = await _context.Set<T>()
				.Skip(queryParameters.StartIndex)
				.Take(queryParameters.PageSize)
				.ProjectTo<TResult>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return new PagedResults<TResult>()
			{
				Items = items,
				TotalRecord = totalRecord,
				RecordNumber = queryParameters.PageSize,
				Page = queryParameters.StartIndex
			};
		}

		public async Task<T> UpdateAsync(T entity)
		{
			_context.Set<T>().Update(entity);
			await _context.SaveChangesAsync();
			return entity;
		}
	}
}