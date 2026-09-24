using JobApplication.Infrastracture.Contract;
using JobApplication.Infrastracture.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.ImplimentationContract
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JobDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(JobDbContext context)
        {
            _context = context;
        }
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories.TryGetValue(typeof(TEntity), out var repo))
            {
                return (IGenericRepository<TEntity>)repo;
            }
            var repositoryInstance = new GenericRepository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repositoryInstance);
            return repositoryInstance;

        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
