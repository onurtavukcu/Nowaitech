﻿using LanguageExt;
using Microsoft.EntityFrameworkCore;
using NowaitechDomain.ExcelDbContext;
using System.Linq.Expressions;

namespace NowaitechDomain.Repository.Implementor
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly EntityDbContext _dbContext;
        public GenericRepository(EntityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public Option<T> GetByIdOpt(int id)
        {
            return Prelude.Optional(_dbContext.Set<T>().Find(id));
        }
        public async void AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public async void AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate).AsNoTracking();
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public virtual IQueryable<T> GetByFilter(Expression<Func<T, bool>> filter)
        {
            //return _dbContext.Set<T>().Where(filter).AsNoTracking();
            return _dbContext.Set<T>().Where(filter);
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public async Task<Task> MYBulkSaveAsync(IEnumerable<object> entity, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_dbContext.BulkInsertAsync(entity, cancellationToken));
        }
        public int SaveEntity(T entity)
        {
            return _dbContext.SaveChanges();
        }

        public void BulkSave(T entity)
        {
            _dbContext.BulkSaveChanges();
        }
    }
}
