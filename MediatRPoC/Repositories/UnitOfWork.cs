using MediatRPoC.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MediatRPoC.Repositories
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository<T> Repository<T>() where T : class;
		Task<int> CompleteAsync();
	}

	public class UnitOfWork : IUnitOfWork
	{
		private readonly DbContext _context;
		private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

		public UnitOfWork(DbContext context)
		{
			_context = context;
		}

		public IRepository<TEntity> Repository<TEntity>() where TEntity : class
		{
			var type = typeof(TEntity);
			if (!_repositories.ContainsKey(type))
			{
				var repositoryType = typeof(Repository<>);
				var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(type), _context);
				_repositories[type] = repositoryInstance;
			}

			return (IRepository<TEntity>)_repositories[type];
		}

		public async Task<int> CompleteAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}

}
