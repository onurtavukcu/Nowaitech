using LanguageExt;
using System.Linq.Expressions;

namespace NowaitechDomain.Repository.Implementor
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        Option<T> GetByIdOpt(int id);
        IQueryable<T> GetAll();
        IQueryable<T> GetByFilter(Expression<Func<T, bool>> filter);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        void AddAsync(T entity);
        void AddRangeAsync(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<Task> MYBulkSaveAsync(IEnumerable<object> entity, CancellationToken cancellationToken);
        int SaveEntity(T entity);
    }
}
