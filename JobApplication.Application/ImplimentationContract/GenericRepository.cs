using JobApplication.Infrastracture.Contract;
using JobApplication.Infrastracture.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace JobApplication.Application.ImplimentationContract
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly JobDbContext _jobDbContext;

        public GenericRepository(JobDbContext jobDbContext)
        {
            _jobDbContext = jobDbContext;
        }
        public async Task AddAsync(TEntity entity) => await _jobDbContext.Set<TEntity>().AddAsync(entity);



        public void Delete(TEntity entity) => _jobDbContext.Set<TEntity>().Remove(entity);


        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _jobDbContext.Set<TEntity>().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(
    Expression<Func<TEntity, bool>> predicate)
        {
            return await _jobDbContext.Set<TEntity>()
                .Where(predicate)
                .ToListAsync();
        }
        public async Task<TEntity?> GetByIdAsync(int id) => await _jobDbContext.Set<TEntity>().FindAsync(id);


        public void Update(TEntity entity) => _jobDbContext.Set<TEntity>().Update(entity);
    }
}
