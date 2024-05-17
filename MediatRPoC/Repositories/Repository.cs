using MediatRPoC.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MediatRPoC.Repositories
{
	public interface IRepository<T>
	{
		Task<T> GetByIdAsync(int id);
		Task<IEnumerable<T>> GetAllAsync();
		Task AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(int id);
		Task<IEnumerable<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification);
	}
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly DbContext _context;

		public Repository(DbContext context)
		{
			_context = context;
		}

		public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

		public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

		public async Task AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(T entity)
		{
			_context.Set<T>().Update(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await GetByIdAsync(id);
			_context.Set<T>().Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification)
		{
			IQueryable<T> query = _context.Set<T>().AsQueryable();

			if (specification.Criteria != null)
			{
				query = query.Where(specification.Criteria);
			}

			query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

			if (specification.OrderBy != null)
			{
				query = specification.OrderBy(query);
			}

			if (specification.OrderByDescending != null)
			{
				query = specification.OrderByDescending(query);
			}

			if (specification.Skip.HasValue)
			{
				query = query.Skip(specification.Skip.Value);
			}

			if (specification.Take.HasValue)
			{
				query = query.Take(specification.Take.Value);
			}

			if (specification.Select != null)
			{
				return query.Select(specification.Select); // Implicitly typed to IQueryable<TResult>
			}
			
			var results = await query.ToListAsync();

			return (IEnumerable<TResult>)results;
		}

	}
}
